/*
Copyright (C) 2013 AfyaPro Foundation

This file is part of AfyaPro.

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
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace AfyaPro_MT
{
    /// <summary>
    /// CryptoService is a wrapper of System.Security.Cryptography.SymmetricAlgorithm classes
    /// and simplifies the interface. It supports customized SymmetricAlgorithm as well.
    /// </summary>
    public class clsCryptoService
    {
        /// <remarks>
        /// Supported .Net intrinsic SymmetricAlgorithm classes.
        /// </remarks>
        public enum SymmProvEnum : int
        {
            DES, RC2, Rijndael
        }

        private SymmetricAlgorithm mobjCryptoService;

        /// <remarks>
        /// Constructor for using an intrinsic .Net SymmetricAlgorithm class.
        /// </remarks>
        public clsCryptoService(SymmProvEnum NetSelected)
        {
            switch (NetSelected)
            {
                case SymmProvEnum.DES:
                    mobjCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmProvEnum.RC2:
                    mobjCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmProvEnum.Rijndael:
                    mobjCryptoService = new RijndaelManaged();
                    break;
            }
        }

        /// <remarks>
        /// Constructor for using a customized SymmetricAlgorithm class.
        /// </remarks>
        public clsCryptoService(SymmetricAlgorithm ServiceProvider)
        {
            mobjCryptoService = ServiceProvider;
        }

        /// <remarks>
        /// Depending on the legal key size limitations of a specific CryptoService provider
        /// and length of the private key provided, padding the secret key with space character
        /// to meet the legal size of the algorithm.
        /// </remarks>
        private byte[] GetLegalKey(string Key)
        {
            string sTemp;
            if (mobjCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0;
                int moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;

                // key sizes are in bits
                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
                sTemp = Key;



            // convert the secret key to byte array
            //return ASCIIEncoding.ASCII.GetBytes(sTemp);

            return UTF8Encoding.UTF8.GetBytes(sTemp);
        }

        public string Encrypt(string Source, string Key)
        {
            byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
            // create a MemoryStream so that the process can be done without I/O files
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] bytKey = GetLegalKey(Key);

            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create an Encryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

            // create Crypto Stream that transforms a stream using the encryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            // write out encrypted content into MemoryStream
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            //Replaced these with the codes below it to resolve Decryption error
            //// get the output and trim the '\0' bytes
            //byte[] bytOut = ms.GetBuffer();
            //int i = 0;
            //for (i = 0; i < bytOut.Length; i++)
            //    if (bytOut[i] == 0)
            //        break;

            //// convert into Base64 so that the result can be used in xml
            //return System.Convert.ToBase64String(bytOut, 0, i);

            // get the output
            byte[] bytOut = ms.ToArray();
            return System.Convert.ToBase64String(bytOut);
        }

        public string Decrypt(string Source, string Key)
        {
            // convert from Base64 to binary
            byte[] bytIn = System.Convert.FromBase64String(Source);
            // create a MemoryStream with the input
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

            byte[] bytKey = GetLegalKey(Key);

            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create a Decryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

            // create Crypto Stream that transforms a stream using the decryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

            // read out the result from the Crypto Stream
            System.IO.StreamReader sr = new System.IO.StreamReader(cs);
            return sr.ReadToEnd();
        }

        internal String Xor_DecryptEncrypt(String mData)
        {
            String mTempKey = "1234567890";
            String mKey = "1234567890";
            int mIntLocation = 0;
            int mAsc1 = 0;
            int mAsc2 = 0;
            String mTemp = "";

            //create valid key
            int mIndex = 0;
            while (mKey.Length <= mData.Length)
            {
                mKey = mKey + mTempKey.Substring(mIndex, 1);
                if (mIndex == (mTempKey.Length - 1))
                {
                    mIndex = 0;
                }
                else
                {
                    mIndex++;
                }
            }

            Char[] mKeyArr = mKey.ToCharArray();
            Char[] mDataArr = mData.ToCharArray();

            try
            {
                for (int i = 0; i < mData.Length; i++)
                {
                    Math.DivRem(i, mKey.Length, out mIntLocation);
                    mIntLocation++;
                    mAsc1 = (int)mDataArr[i];
                    mAsc2 = (int)mKeyArr[mIntLocation];
                    mTemp = mTemp + (char)(mAsc1 ^ mAsc2);
                }

                return mTemp;
            }
            catch (Exception ex)
            {
                clsGlobal.Write_Error("", "", ex.Message);
                return mTemp;
            }
        }
    }
}