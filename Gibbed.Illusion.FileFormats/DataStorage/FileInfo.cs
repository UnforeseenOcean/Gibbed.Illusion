﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Illusion.FileFormats.DataStorage
{
    public class FileInfo
    {
        public uint TypeId;
        public uint Size; // includes headers (such as the first 30 bytes)
        public ushort Unknown08; // seems to indicate extra headers
        public uint SlotRamRequired;
        public uint SlotVramRequired;
        public uint OtherRamRequired;
        public uint OtherVramRequired;

        public void Deserialize(Stream input, bool littleEndian)
        {
            this.TypeId = input.ReadValueU32(littleEndian);
            this.Size = input.ReadValueU32(littleEndian);
            this.Unknown08 = input.ReadValueU16(littleEndian);
            this.SlotRamRequired = input.ReadValueU32(littleEndian);
            this.SlotVramRequired = input.ReadValueU32(littleEndian);
            this.OtherRamRequired = input.ReadValueU32(littleEndian);
            this.OtherVramRequired = input.ReadValueU32(littleEndian);
        }
    }
}