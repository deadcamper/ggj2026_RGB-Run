using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SevenSegmentDisplay
{
    public enum Segment : int
    {
        Top,
        TopRight,
        BottomRight,
        Bottom,
        BottomLeft,
        TopLeft,
        Center,
    }

        [Flags]
    public enum Segments : int
    {
        None        = 0x0000000,
        Top         = 0x0000001,
        TopRight    = 0x0000010,
        BottomRight = 0x0000100,
        Bottom      = 0x0001000,
        BottomLeft  = 0x0010000,
        TopLeft     = 0x0100000,
        Center      = 0x1000000,
        Zero = Top | TopRight | BottomRight | Bottom | BottomLeft | TopLeft,
        One = TopRight | BottomRight,
        BadOne = BottomLeft | TopLeft,
        Two = Top | TopRight | Center | BottomLeft | Bottom,
        Three = Top | Center | Bottom | TopRight | BottomRight,
        Four = TopLeft | Center | TopRight | BottomRight,
        Five = Top | TopLeft | Center | BottomRight | Bottom,
        Six = Top | TopLeft | Center | BottomRight | Bottom | BottomLeft,
        Seven = Top | TopRight | BottomRight,
        Eight = Top | TopRight | BottomRight | Bottom | BottomLeft | TopLeft | Center,
        Nine = Center | TopLeft | Top | TopRight | BottomRight | Bottom,
    }



    public static class SegmentsUtility
    {
        private static System.Random Random { get; } = new System.Random();

        private readonly static Segments[] numbers = new[] { Segments.Zero, Segments.One, Segments.Two, Segments.Three, Segments.Four, Segments.Five, Segments.Six, Segments.Seven, Segments.Eight, Segments.Nine };
        private readonly static Segment[] segments = new[] { Segment.Top, Segment.Bottom, Segment.BottomRight, Segment.BottomLeft, Segment.TopLeft, Segment.TopRight, Segment.Center };

        public static IReadOnlyList<Segments> Numbers => numbers;
        public static IReadOnlyList<Segment> AllSegments => segments;

        public static bool Contains(this Segments value, Segment segment)
            => (value & segment.ToSegments()) != 0;

        public static Segments FromNumber(int number)
            => numbers[number];
        public static Segments ToSegments(this Segment segment)
            => segment switch
            {
                Segment.Top => Segments.Top,
                Segment.Bottom => Segments.Bottom,
                Segment.Center => Segments.Center,
                Segment.TopLeft => Segments.TopLeft,
                Segment.TopRight => Segments.TopRight,
                Segment.BottomLeft => Segments.BottomLeft,
                Segment.BottomRight => Segments.BottomRight,
                _ => throw new InvalidOperationException()
            };

        public static bool IsNumber(this Segments value)
            => value.ToNumber().HasValue;

        public static bool IsNumber(this Segments value, out int number)
            => value.ToNumber().TryGetValue(out number);

        public static bool IsNumberInDigits(this Segments value, Digits digits)
            => value.IsNumber(out int number) && digits.IncludesNumber(number); //if there is overlap

        public static bool IsInNumbers(this Segments value, HashSet<int> numbers)
            => value.IsNumber(out int number) && numbers.Contains(number); //if there is overlap

        public static int? ToNumber(this Segments value)
            => (value & Segments.Eight) switch
            {
                Segments.Zero => 0,
                Segments.One => 1,
                Segments.BadOne => 1,
                Segments.Two => 2,
                Segments.Three => 3,
                Segments.Four => 4,
                Segments.Five => 5,
                Segments.Six => 6,
                Segments.Seven => 7,
                Segments.Eight => 8,
                Segments.Nine => 9,
                _ => null,
            };

        public static Segments RandomSegments()
            => (Segments)Random.Next();

        public static bool TryToCreateRandomSegmentsNotInDigits(Digits digits, out PerRGBChannel<Segments> segments, out int? number)
        {
            if(digits.AllOtherDigits().TryGetRandom(out var randomNumber) && TryToRandomlySplitNumberIntoSegmentsNotInDigits(randomNumber, digits, out segments))
            {
                number = randomNumber;
                return true;
            }
            for (var i = 0; i < 100; i++)
            {
                segments = new PerRGBChannel<Segments>(RandomSegments(), RandomSegments(), RandomSegments());
                if (!(segments.R | segments.G).IsNumberInDigits(digits) 
                    && !(segments.R | segments.B).IsNumberInDigits(digits)
                    && !(segments.G | segments.B).IsNumberInDigits(digits))
                {
                    number = null;
                    return true;
                }
            }
            number = null;
            segments = default;
            return false;
        }

        public static bool TryToRandomlySplitNumberIntoSegmentsNotInDigits(int number, Digits validDigits, out PerRGBChannel<Segments> segments)
        {
            segments = default;

            var numberSegments = FromNumber(number);

            Segments overlapMask = numberSegments & RandomSegments();
            Segments validA = default;
            Segments ValidB = default;
            Segments obfuscation = default;

            for (var i = 0; i < 100; i++)
            {
                var exclusiveMask = RandomSegments();
                validA = (numberSegments & exclusiveMask) | overlapMask;
                ValidB = (numberSegments & ~exclusiveMask) | overlapMask;
                Debug.Assert((validA | ValidB).IsNumberInDigits(validDigits));
                obfuscation = RandomSegments();

                if (!(obfuscation | validA).IsNumberInDigits(validDigits) && !(obfuscation | ValidB).IsNumberInDigits(validDigits))
                {
                    segments = new PerRGBChannel<Segments>(validA, ValidB, obfuscation).Shuffled();
                    return true;
                }
            }

            return false;
        }
    }
}