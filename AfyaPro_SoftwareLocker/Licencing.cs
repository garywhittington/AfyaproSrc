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
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace AfyaPro_SoftwareLocker
{
    public class Licencing
    {
        #region private variables

        private Int64 pInstallationDate;
        public Int64 InstallationDate
        {
            get { return pInstallationDate; }
        }

        private string pBaseString;
        public string BaseString
        {
            get { return pBaseString; }
        }

        private string pPassword;
        public string Password
        {
            get { return pPassword; }
        }

        private string pInfoText;
        public string InfoText
        {
            get { return pInfoText; }
        }

        private int pTrialDays;
        public int TrialDays
        {
            get { return pTrialDays; }
        }

        private string pModuleKeys;
        public string ModuleKeys
        {
            get { return pModuleKeys; }
        }

        private string pLicenceIds;
        public string LicenceIds
        {
            get { return pLicenceIds; }
        }

        private string pLicenceType;
        public string LicenceType
        {
            get { return pLicenceType; }
        }

        public byte[] TripleDESKey
        {
            get
            {
                return FileReadWrite.key;
            }
            set
            {
                FileReadWrite.key = value;
            }
        }

        private static string pClassName = "AfyaPro_SoftwareLocker.Licencing";
        private string pSoftName;
        private string pHidenFilePath;
        private string pIdentifier;
        private string pSavedBaseString = "";
        private string pSavedPassword = "";

        #endregion

        #region Licencing
        public Licencing(string mSoftwareName, string mHideFilePath, string mInfoText, int mTrialDays, string mIdentifier)
        {
            pSoftName = mSoftwareName;
            pIdentifier = mIdentifier;

            this.SetDefaults();

            pTrialDays = mTrialDays;

            pHidenFilePath = mHideFilePath;
            pInfoText = mInfoText;
        }
        #endregion

        #region SetDefaults
        private void SetDefaults()
        {
            SystemInfo.UseBaseBoardManufacturer = false;
            SystemInfo.UseBaseBoardProduct = true;
            SystemInfo.UseBiosManufacturer = false;
            SystemInfo.UseBiosVersion = false; //defaulted to false instead of true
            SystemInfo.UseDiskDriveSignature = false; //defaulted to false instead of true
            SystemInfo.UsePhysicalMediaSerialNumber = false;
            SystemInfo.UseProcessorID = true;
            SystemInfo.UseVideoControllerCaption = false;
            SystemInfo.UseWindowsSerialNumber = false;

            this.MakeBaseString();
            this.MakePassword();
        }
        #endregion

        #region MakeBaseString
        private void MakeBaseString()
        {
            string mBaseString = Encryption.Boring(Encryption.InverseByBase(SystemInfo.GetSystemInfo(pSoftName), 10));

            pBaseString = "";
            int mGroupStart = 0;
            for (int mIndex = 0; mIndex < 5; mIndex++)
            {
                if (pBaseString.Trim() == "")
                {
                    pBaseString = mBaseString.Substring(mGroupStart, 5);
                }
                else
                {
                    pBaseString = pBaseString + "-" + mBaseString.Substring(mGroupStart, 5);
                }

                mGroupStart = mGroupStart + 5;
            }
        }
        #endregion

        #region MakePassword
        private void MakePassword()
        {
            string mBaseString = pBaseString.Replace("-", "");
            string mPassword = Encryption.MakePassword(mBaseString, pIdentifier);

            pPassword = "";
            int mGroupStart = 0;
            for (int mIndex = 0; mIndex < 5; mIndex++)
            {
                if (pPassword.Trim() == "")
                {
                    pPassword = mPassword.Substring(mGroupStart, 5);
                }
                else
                {
                    pPassword = pPassword + "-" + mPassword.Substring(mGroupStart, 5);
                }

                mGroupStart = mGroupStart + 5;
            }
        }
        #endregion

        #region Get_InstallationID
        public string Get_InstallationID()
        {
            return pBaseString;
        }
        #endregion

        #region Get_SavedBaseString
        public string Get_SavedBaseString()
        {
            return pSavedBaseString;
        }
        #endregion

        #region Get_SavedPassword
        public string Get_SavedPassword()
        {
            return pSavedPassword;
        }
        #endregion

        #region Save_ActivationCode
        public static bool Save_ActivationCode(string mFilePath, string mContents)
        {
            string mFunctionName = "Save_ActivationCode";

            try
            {
                FileReadWrite.WriteFile(mFilePath, mContents);

                return true;
            }
            catch (Exception ex)
            {
                Write_Error(pClassName, mFunctionName, ex.Message);
                return false;
            }
        }
        #endregion

        #region Check_Licence
        public void Check_Licence(DateTime mTransDate)
        {
            pTrialDays = DaysToEnd(mTransDate);
        }
        #endregion

        #region DaysToEnd
        private int DaysToEnd(DateTime mTransDate)
        {
            FileInfo hf = new FileInfo(pHidenFilePath);
            if (hf.Exists == false)
            {
                this.MakeHideFile();
                CheckHideFile(mTransDate);
                return pTrialDays;
            }

            return CheckHideFile(mTransDate);
        }
        #endregion

        #region MakeHideFile
        private void MakeHideFile()
        {
            pInstallationDate = DateTime.Now.Ticks;
            string mPassword = "Nill";
            string mModuleKeys = "opd,ipd,dxt,lab,rch,ivt,cus,bil,mtu,rpt,bls,ivs,rpd,gen,sec";
            string mLicenceIds = "";
            string mHideInfo = ""
                + pInstallationDate + ";"
                + pTrialDays + ";"
                + mPassword + ";"
                + pBaseString + ";"
                + mModuleKeys + ";"
                + mLicenceIds;

            FileReadWrite.WriteFile(pHidenFilePath, mHideInfo);
        }
        #endregion

        #region CheckHideFile
        private int CheckHideFile(DateTime mTransDate)
        {
            string mFileContent = FileReadWrite.ReadFile(pHidenFilePath);
            string[] mHideInfo = mFileContent.Split(';');
            long mDiffDays;
            int mDaysToEnd;

            if (pBaseString == mHideInfo[3])
            {
                mDaysToEnd = Convert.ToInt32(mHideInfo[1]);
                try
                {
                    pLicenceType = mHideInfo[6];
                }
                catch { pLicenceType = ""; }

                pInstallationDate = Convert.ToInt64(mHideInfo[0]);
                DateTime mDateTime = new DateTime(pInstallationDate);
                mDiffDays = DateAndTime.DateDiff(DateInterval.Day,
                    mDateTime.Date, mTransDate,
                    FirstDayOfWeek.Saturday,
                    FirstWeekOfYear.FirstFullWeek);

                mDaysToEnd = Convert.ToInt32(mHideInfo[1]);
                pSavedPassword = mHideInfo[2];
                pSavedBaseString = mHideInfo[3];
                pModuleKeys = mHideInfo[4];
                pLicenceIds = mHideInfo[5];

                mDiffDays = Math.Abs(mDiffDays);

                pTrialDays = mDaysToEnd - Convert.ToInt32(mDiffDays);
            }
            return pTrialDays;
        }
        #endregion

        #region -> Usage Properties

        public bool UseProcessorID
        {
            get
            {
                return SystemInfo.UseProcessorID;
            }
            set
            {
                SystemInfo.UseProcessorID = value;
            }
        }

        public bool UseBaseBoardProduct
        {
            get
            {
                return SystemInfo.UseBaseBoardProduct;
            }
            set
            {
                SystemInfo.UseBaseBoardProduct = value;
            }
        }

        public bool UseBaseBoardManufacturer
        {
            get
            {
                return SystemInfo.UseBiosManufacturer;
            }
            set
            {
                SystemInfo.UseBiosManufacturer = value;
            }
        }

        public bool UseDiskDriveSignature
        {
            get
            {
                return SystemInfo.UseDiskDriveSignature;
            }
            set
            {
                SystemInfo.UseDiskDriveSignature = value;
            }
        }

        public bool UseVideoControllerCaption
        {
            get
            {
                return SystemInfo.UseVideoControllerCaption;
            }
            set
            {
                SystemInfo.UseVideoControllerCaption = value;
            }
        }

        public bool UsePhysicalMediaSerialNumber
        {
            get
            {
                return SystemInfo.UsePhysicalMediaSerialNumber;
            }
            set
            {
                SystemInfo.UsePhysicalMediaSerialNumber = value;
            }
        }

        public bool UseBiosVersion
        {
            get
            {
                return SystemInfo.UseBiosVersion;
            }
            set
            {
                SystemInfo.UseBiosVersion = value;
            }
        }

        public bool UseBiosManufacturer
        {
            get
            {
                return SystemInfo.UseBiosManufacturer;
            }
            set
            {
                SystemInfo.UseBiosManufacturer = value;
            }
        }

        public bool UseWindowsSerialNumber
        {
            get
            {
                return SystemInfo.UseWindowsSerialNumber;
            }
            set
            {
                SystemInfo.UseWindowsSerialNumber = value;
            }
        }

        #endregion

        #region Write_Error
        public static string Write_Error(string mClassName, string mFunctionName, string mError)
        {

            string mErrorMessage = "";

            //create log if does not exist
            string mLogSource = "Mayo_Server";
            string mLogName = "Mayo_Server";
            if (EventLog.SourceExists(mLogSource) == false)
            {
                EventLog.CreateEventSource(mLogSource, mLogName);
            }

            // Create an EventLog instance and assign its source.
            EventLog mEventLog = new EventLog();
            mEventLog.Source = mLogSource;

            //buit error message
            mErrorMessage = "Error occured in the following module" + System.Environment.NewLine
            + "Class Name: " + mClassName + System.Environment.NewLine
            + "Function Name: " + mFunctionName + System.Environment.NewLine
            + "Error Message: " + mError;

            mEventLog.WriteEntry(mErrorMessage, EventLogEntryType.Error);

            return mErrorMessage;

        }
        #endregion
    }
}
