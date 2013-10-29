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
using System.Management;

namespace AfyaPro_SoftwareLocker
{
    static class SystemInfo
    {
        #region -> Private Variables

        public static bool UseProcessorID;
        public static bool UseBaseBoardProduct;
        public static bool UseBaseBoardManufacturer;
        public static bool UseDiskDriveSignature;
        public static bool UseVideoControllerCaption;
        public static bool UsePhysicalMediaSerialNumber;
        public static bool UseBiosVersion;
        public static bool UseBiosManufacturer;
        public static bool UseWindowsSerialNumber;

        private static string pClassName = "AfyaPro_SoftwareLocker.SystemInfo";

        #endregion

        public static string GetSystemInfo(string SoftwareName)
        {
            if (UseProcessorID == true)
                SoftwareName += RunQuery("Processor", "ProcessorId");

            if (UseBaseBoardProduct == true)
                SoftwareName += RunQuery("BaseBoard", "Product");

            if (UseBaseBoardManufacturer == true)
                SoftwareName += RunQuery("BaseBoard", "Manufacturer");

            if (UseDiskDriveSignature == true)
                SoftwareName += RunQuery("DiskDrive", "Signature");

            if (UseVideoControllerCaption == true)
                SoftwareName += RunQuery("VideoController", "Caption");

            if (UsePhysicalMediaSerialNumber == true)
                SoftwareName += RunQuery("PhysicalMedia", "SerialNumber");

            if (UseBiosVersion == true)
                SoftwareName += RunQuery("BIOS", "Version");

            if (UseWindowsSerialNumber == true)
                SoftwareName += RunQuery("OperatingSystem", "SerialNumber");

            SoftwareName = RemoveUseLess(SoftwareName);

            if (SoftwareName.Length < 25)
                return GetSystemInfo(SoftwareName);

            return SoftwareName.Substring(0, 25).ToUpper();
        }

        private static string RemoveUseLess(string st)
        {
            char ch;
            for (int i = st.Length - 1; i >= 0; i--)
            {
                ch = char.ToUpper(st[i]);

                if ((ch < 'A' || ch > 'Z') &&
                    (ch < '0' || ch > '9'))
                {
                    st = st.Remove(i, 1);
                }
            }
            return st;
        }

        private static string RunQuery(string TableName, string MethodName)
        {
            string mFunctionName = "RunQuery";

            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_" + TableName);
            foreach (ManagementObject MO in MOS.Get())
            {
                try
                {
                    return MO[MethodName].ToString();
                }
                catch (Exception ex)
                {
                    Licencing.Write_Error(pClassName, mFunctionName, ex.Message);
                }
            }
            return "";
        }
    }
}
