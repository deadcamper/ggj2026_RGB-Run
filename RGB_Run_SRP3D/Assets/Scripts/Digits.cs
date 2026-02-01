using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

namespace SevenSegmentDisplay
{
    public enum Digit : byte
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
    }

    [Flags]
    public enum Digits : short
    {
        Zero    = 0b0000000001,
        One     = 0b0000000010,
        Two     = 0b0000000100,
        Three   = 0b0000001000,
        Four    = 0b0000010000,
        Five    = 0b0000100000,
        Six     = 0b0001000000,
        Seven   = 0b0010000000,
        Eight   = 0b0100000000,
        Nine    = 0b1000000000,
        All     = 0b1111111111,
    }

    public static class DigitsUtility
    {

        private readonly static Digits[] numbers = new[] { Digits.Zero, Digits.One, Digits.Two, Digits.Three, Digits.Four, Digits.Five, Digits.Six, Digits.Seven, Digits.Eight, Digits.Nine };

        public static void GetRandomDigits(int count, List<Digit> result)
        {
            result.Clear();
            var list = ListPool<int>.Get();
            list.Clear();
            list.AddRange(Enumerable.Range(0, 10));
            for (int i = 0; i < count && list.Count > 0; i++)
            {
                result .Add((Digit)list[UnityEngine.Random.Range(0, list.Count)]);
            }
            list.Clear();
            ListPool<int>.Release(list);
        }

        public static Digits GetRandomDigits(int count)
        {
            var list = ListPool<int>.Get();
            list.Clear();
            list.AddRange(Enumerable.Range(0, 10));
            Digits result = default;
            for (int i = 0; i < count && list.Count > 0; i++)
            {
                result |= FromInteger(list[UnityEngine.Random.Range(0, list.Count)]);
            }
            list.Clear();
            ListPool<int>.Release(list);
            return result;
        }

        public static void GetIndividualNumbers(this Digits digits, List<int> result)
        {
            result.Clear();
            for (int i = 0; i < 16; i++)
            {
                if (digits.IsBitSet(i))
                {
                    result.Add(i);
                }
            }
        }

        public static void GetIndividualDigits(this Digits digits, List<Digit> result)
        {
            result.Clear();
            for (int i = 0; i < 16; i++)
            {
                if (digits.IsBitSet(i))
                {
                    result.Add((Digit)i);
                }
            }
        }

        public static int ToInteger(this Digit value)
            => (int)value;

        public static Digits FromInteger(int value)
            => numbers[value];
        public static Digits FromDigit(Digit value)
            => numbers[(int)value];

        public static Digits FromIntegers(IEnumerable<int> values)
        {
            Digits result = default;
            foreach (var number in values)
            {
                result |= FromInteger(number);
            }
            return result;
        }
        public static Digits FromDigits(IEnumerable<Digit> values)
        {
            Digits result = default;
            foreach (var number in values)
            {
                result |= FromDigit(number);
            }
            return result;
        }

        public static bool IncludesNumber(this Digits digits, int value)
            => (FromInteger(value) & digits) != 0; //if there is overlap

        public static Digits AllOtherDigits(this Digits digits)
            => (~digits) & Digits.All;

        public static bool TryGetRandom(this Digits digits, out int value)
        {
            digits &= Digits.All;
            value = default;

            int count = digits.Count();
            if (count <= 0)
                return false;

            int rand = UnityEngine.Random.Range(0, count);
            value = GetNthDigit(digits, rand);
            return true;
        }

        private static bool IsBitSet(this Digits digits, int index)
            => (((short)digits >> index) & 1) == 1;

        private static int Count(this Digits digits)
        {
            int count = 0;
            for (int i = 0; i < 16; i++)
            {
                if (digits.IsBitSet(i))
                {
                    count++;
                }
            }
            return count;
        }

        private static int GetNthDigit(Digits digits, int n)
        {
            int count = 0;
            for (int i = 0; i < 16; i++)
            {
                if (digits.IsBitSet(i))
                {
                    if (count == n)
                    {
                        return i;
                    }
                    count++;
                }
            }
            // Should not be reached if called correctly with a valid n
            throw new InvalidOperationException("Bit not found.");
        }
    }
}