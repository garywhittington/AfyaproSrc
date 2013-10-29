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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace AfyaPro_SoftwareLocker
{
    public class FileReadWrite
    {
        // Key for TripleDES encryption
        public static byte[] key = { 21, 10, 64, 10, 100, 40, 200, 4,
                    21, 54, 65, 246, 5, 62, 1, 54,
                    54, 6, 8, 9, 65, 4, 65, 9};

        private static byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public static string ReadFile(string FilePath)
        {
            if (System.IO.File.Exists(FilePath) == false)
            {
                return string.Empty;
            }

            StringBuilder SB = new StringBuilder();

            using (FileStream fin = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                TripleDES tdes = new TripleDESCryptoServiceProvider();
                using (CryptoStream cs = new CryptoStream(fin, tdes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    int ch;
                    for (int i = 0; i < fin.Length; i++)
                    {
                        ch = cs.ReadByte();
                        if (ch == 0)
                            break;
                        SB.Append(Convert.ToChar(ch));
                    }
                }
            }

            return SB.ToString();
        }

        public static void WriteFile(string FilePath, string Data)
        {
            //if (System.IO.File.Exists(FilePath) == true)
            //{
            //    System.IO.File.Delete(FilePath);
            //}

            using (FileStream fout = new FileStream(FilePath, FileMode.Create))
            {
                TripleDES tdes = new TripleDESCryptoServiceProvider();
                using (CryptoStream cs = new CryptoStream(fout, tdes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] mDataArray = Encoding.ASCII.GetBytes(Data);
                    cs.Write(mDataArray, 0, mDataArray.Length);
                    cs.WriteByte(0);
                }
            }
        }
    }
}
