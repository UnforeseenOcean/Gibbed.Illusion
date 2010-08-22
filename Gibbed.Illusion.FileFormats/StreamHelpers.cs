﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Illusion.FileFormats
{
    internal static class StreamHelpers
    {
        public static MemoryStream ReadToMemoryStreamSafe(this Stream stream, long size, bool littleEndian)
        {
            MemoryStream memory = new MemoryStream();

            uint myHash = FNV32.InitialHash;

            long left = size;
            byte[] data = new byte[4096];
            while (left > 0)
            {
                int block = (int)(Math.Min(left, 4096));
                stream.Read(data, 0, block);
                myHash = FNV32.Hash(data, 0, block);
                memory.Write(data, 0, block);
                left -= block;
            }

            var theirHash = stream.ReadValueU32(littleEndian);
            if (theirHash != myHash)
            {
                throw new InvalidDataException(string.Format("hash failure ({0:X} vs {1:X})",
                    myHash, theirHash));
            }

            memory.Position = 0;
            return memory;
        }
    }
}