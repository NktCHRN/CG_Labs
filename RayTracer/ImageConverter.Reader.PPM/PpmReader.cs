﻿using ImageConverter.Common;
using System.Drawing;

namespace ImageConverter.Reader.PPM;
public sealed class PpmReader : IImageReader
{
    public string ImageFormat => throw new NotImplementedException();

    public bool CanRead(string fileName)
    {
        throw new NotImplementedException();
    }

    public Color[,] Read(string fileName)
    {
        throw new NotImplementedException();
    }
}