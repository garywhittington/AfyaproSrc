/*
This file is part of AfyaPro.

	Copyright (C) 2013 AfyaPro Foundation.

    AfyaPro is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    AfyaPro is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with AfyaPro.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.IO;

namespace ALTestApp36NET_CS
{
    public class CRC32
    {
        private const int BUFFER_SIZE = 0x400;
        private int[] crc32Table;

        public CRC32()
        {
            int num1;
            int num2 = -306674912;
            this.crc32Table = new int[0x101];
            int num3 = 0;
        Label_0020:
            num1 = num3;
            int num4 = 8;
            while (true)
            {
                if ((num1 & 1) > 0)
                {
                    num1 = (int)((((long)(num1 & -2)) / 2) & 0x7fffffff);
                    num1 ^= num2;
                }
                else
                {
                    num1 = (int)((((long)(num1 & -2)) / 2) & 0x7fffffff);
                }
                num4 += -1;
                if (num4 < 1)
                {
                    this.crc32Table[num3] = num1;
                    num3++;
                    if (num3 > 0xff)
                    {
                        return;
                    }
                    goto Label_0020;
                }
            }
        }

        public int GetCrc32(ref Stream stream)
        {
            int num2 = -1;
            byte[] buffer1 = new byte[0x401];
            int num6 = 0x400;
            int num1 = stream.Read(buffer1, 0, num6);

            while (num1 > 0)
            {
                int num8 = num1 - 1;
                for (int num4 = 0; num4 <= num8; num4++)
                {
                    int num5 = (num2 & 0xff) ^ buffer1[num4];
                    num2 = ((num2 & -256) / 0x100) & 0xffffff;
                    num2 ^= this.crc32Table[num5];
                }
                num1 = stream.Read(buffer1, 0, num6);
            }
            return ~num2;
        }

    }
}

