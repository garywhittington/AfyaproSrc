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
    public class clsAdmission : ISerializable
    {
        public Int16 Exe_Result;
        public string Exe_Message;

        public bool IsAdmitted;
        public int AdmissionId;
        public string Booking;
        public string PatientCode;
        public DateTime TransDate;
        public string WardCode;
        public string WardDescription;
        public string RoomCode;
        public string RoomDescription;
        public string BedCode;
        public string BedDescription;
        public string PatientCondition;
        public string RegistryStatus;

        public clsAdmission()
        {
            Exe_Result = 1;
            Exe_Message = "";

            IsAdmitted = false;
            AdmissionId = 0;
            Booking = "";
            PatientCode = "";
            TransDate = new DateTime();
            WardCode = "";
            WardDescription = "";
            RoomCode = "";
            RoomDescription = "";
            BedCode = "";
            BedDescription = "";
            PatientCondition = "";
            RegistryStatus = "";
        }

        protected clsAdmission(SerializationInfo info, StreamingContext context)
        {
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");

            this.IsAdmitted = info.GetBoolean("IsAdmitted");
            this.AdmissionId = info.GetInt32("AdmissionId");
            this.Booking = info.GetString("Booking");
            this.PatientCode = info.GetString("PatientCode");
            this.TransDate = info.GetDateTime("TransDate");
            this.WardCode = info.GetString("WardCode");
            this.WardDescription = info.GetString("WardDescription");
            this.RoomCode = info.GetString("RoomCode");
            this.RoomDescription = info.GetString("RoomDescription");
            this.BedCode = info.GetString("BedCode");
            this.BedDescription = info.GetString("BedDescription");
            this.PatientCondition = info.GetString("PatientCondition");
            this.RegistryStatus = info.GetString("RegistryStatus");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("exe_result", this.Exe_Result);
            info.AddValue("exe_message", this.Exe_Message);

            info.AddValue("IsAdmitted", this.IsAdmitted);
            info.AddValue("AdmissionId", this.AdmissionId);
            info.AddValue("Booking", this.Booking);
            info.AddValue("PatientCode", this.PatientCode);
            info.AddValue("TransDate", this.TransDate);
            info.AddValue("WardCode", this.WardCode);
            info.AddValue("WardDescription", this.WardDescription);
            info.AddValue("RoomCode", this.RoomCode);
            info.AddValue("RoomDescription", this.RoomDescription);
            info.AddValue("BedCode", this.BedCode);
            info.AddValue("BedDescription", this.BedDescription);
            info.AddValue("PatientCondition", this.PatientCondition);
            info.AddValue("RegistryStatus", this.RegistryStatus);
        }

    }
}
