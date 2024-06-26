﻿namespace Nekres.Musician.Core.Domain
{
    public class Fraction
    {
        public Fraction(int nominator, int denominator)
        {
            Nominator = nominator;
            Denominator = denominator;
        }

        public static Fraction Parse(string nominator, string denominator)
        {
            return new Fraction(string.IsNullOrEmpty(nominator) ? 1 : int.Parse(nominator),
                string.IsNullOrEmpty(denominator) ? 1 : int.Parse(denominator));
        }

        public int Nominator { get; }

        public int Denominator { get; }

        public override string ToString()
        {
            return $"{Nominator}/{Denominator}";
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Nominator*b.Nominator, a.Denominator*b.Denominator);
        }

        public override bool Equals(object obj)
        {
            return Equals((Fraction) obj);
        }

        protected bool Equals(Fraction other)
        {
            return Nominator == other.Nominator && Denominator == other.Denominator;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Nominator*397) ^ Denominator;
            }
        }

        public static implicit operator decimal(Fraction fraction) => fraction.Nominator/(decimal) fraction.Denominator;
    }
}