using Blish_HUD;
using Nekres.Musician.Core.Domain;
using Nekres.Musician.Core.Instrument;
using System;
using System.Diagnostics;
using System.Threading;

namespace Nekres.Musician.Core.Player.Algorithms
{
    public class FavorChordsAlgorithm : PlayAlgorithmBase
    {
        public FavorChordsAlgorithm(InstrumentBase instrument) : base(instrument)
        {
        }

        public override void Play(Metronome metronomeMark, ChordOffset[] melody) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var strumIndex = 0; strumIndex < melody.Length;)
            {
                if (_abort || !CanContinue()) break;

                var strum = melody[strumIndex];

                if (stopwatch.ElapsedMilliseconds > metronomeMark.WholeNoteLength.Multiply(strum.Offset).TotalMilliseconds)
                {
                    var chord = strum.Chord;

                    foreach (var note in chord.Notes)
                    {
                        this.Instrument.GoToOctave(note);
                        this.Instrument.PlayNote(note);
                    }

                    strumIndex++;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }
            }
            MusicianModule.ModuleInstance.MusicPlayer?.Stop();
        }
    }
}