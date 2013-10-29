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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace AfyaPro_Types
{
    [Serializable]
    public class clsLicence : ISerializable
    {
        public Int16 Exe_Result;
        public string Exe_Message;
        public Int64 InstallationDate;
        public string BaseString;
        public int TrialDays;
        public string InfoText;
        public string ModuleKeys;
        public string LicenceIds;
        public bool FullyActive;

        public clsLicence()
        {
            Exe_Result = 1;
            Exe_Message = "";
            BaseString = "";
            TrialDays = 0;
            InfoText = "";
            ModuleKeys = "";
            LicenceIds = "";
            InstallationDate = 0;
            FullyActive = false;
        }

        protected clsLicence(SerializationInfo info, StreamingContext context)
        {
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");
            this.BaseString = info.GetString("BaseString");
            this.TrialDays = info.GetInt32("TrialDays");
            this.InfoText = info.GetString("InfoText");
            this.ModuleKeys = info.GetString("ModuleKeys");
            this.LicenceIds = info.GetString("LicenceIds");
            this.InstallationDate = info.GetInt64("InstallationDate");
            this.FullyActive = info.GetBoolean("FullyActive");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("exe_result", this.Exe_Result);
            info.AddValue("exe_message", this.Exe_Message);
            info.AddValue("BaseString", this.BaseString);
            info.AddValue("TrialDays", this.TrialDays);
            info.AddValue("InfoText", this.InfoText);
            info.AddValue("ModuleKeys", this.ModuleKeys);
            info.AddValue("LicenceIds", this.LicenceIds);
            info.AddValue("InstallationDate", this.InstallationDate);
            info.AddValue("FullyActive", this.FullyActive);
        }

    }
}
