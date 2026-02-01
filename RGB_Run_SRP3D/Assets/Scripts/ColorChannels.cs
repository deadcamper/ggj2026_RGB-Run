using System;
using UnityEngine;

public enum ColorChannel : byte
{
    Red = 0,
    Green = 1,
    Blue = 2,
}

[Flags]
public enum ColorChannels : short
{
    None = 0,
    Red = 0x001,
    Green = 0x010,
    Blue = 0x100,
}

public static class ColorChannelUtility
{
    private readonly static ColorChannel[] allColorChannel;

    public static ColorChannels ToColorChannels(this ColorChannel colorChannel)
        => colorChannel switch
        {
            ColorChannel.Red => ColorChannels.Red,
            ColorChannel.Green => ColorChannels.Green,
            ColorChannel.Blue => ColorChannels.Blue,
            _ => throw new ArgumentOutOfRangeException(nameof(colorChannel))
        };

    public static Color ToColor(this ColorChannels colorChannels)
        => new Color(
            r: colorChannels.HasFlag(ColorChannels.Red) ? 1 : 0,
            g: colorChannels.HasFlag(ColorChannels.Green) ? 1 : 0,
            b: colorChannels.HasFlag(ColorChannels.Blue) ? 1 : 0);

    public static ColorChannels Invert(this ColorChannels colorChannels)
        => ~colorChannels;

    public static ColorChannels Invert(this ColorChannel colorChannel)
        => ~colorChannel.ToColorChannels();
}