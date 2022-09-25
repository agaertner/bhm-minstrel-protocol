using Blish_HUD;
using Nekres.Musician.Core.Domain;
using Nekres.Musician.Core.Instrument;
using System;
using System.Linq;
using System.Threading;

namespace Nekres.Musician.Core.Player.Algorithms
{
    public class FavorNotesAlgorithm : PlayAlgorithmBase
    {
        public FavorNotesAlgorithm(InstrumentBase instrument) : base(instrument)
        {
        }

        public override void Play(Metronome metronomeMark, ChordOffset[] melody)
        {
            PrepareChordsOctave(melody[0].Chord);

            _stopwatch.Start();

            for (var strumIndex = 0; strumIndex < melody.Length;)
            {
                if (_abort || !CanContinue()) break;

                var strum = melody[strumIndex];

                if (_stopwatch.ElapsedMilliseconds > metronomeMark.WholeNoteLength.Multiply(strum.Offset).TotalMilliseconds)
                {
                    var chord = strum.Chord;

                    PlayChord(chord);

                    if (strumIndex < melody.Length - 1)
                    {
                        PrepareChordsOctave(melody[strumIndex + 1].Chord);
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

        private void PrepareChordsOctave(Chord chord)
        {
            this.Instrument.GoToOctave(chord.Notes.First());
        }

        private void PlayChord(Chord chord)
        {
            var notes = chord.Notes.ToArray();

            for (var noteIndex = 0; noteIndex < notes.Length; noteIndex++)
            {
                this.Instrument.PlayNote(notes[noteIndex]);

                if (noteIndex < notes.Length - 1)
                {
                    PrepareNoteOctave(notes[noteIndex + 1]);
                }
            }
        }

        private void PrepareNoteOctave(RealNote note)
        {
            this.Instrument.GoToOctave(note);
        }
    }
}