﻿using ImageConverter.Common;
using ImageConverter.Core.Abstractions;

namespace ImageConverter.Core;
public sealed class ImageConverter
{
    private readonly IPluginManager _pluginManager;

    public ImageConverter(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
    }

    public FileInfo Convert(string fileName, string resultFormat)
    {
        throw new NotImplementedException();
    }
}
