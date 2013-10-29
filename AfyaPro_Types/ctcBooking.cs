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
    public class ctcBooking : ISerializable
    {
        public Int16 Exe_Result;
        public string Exe_Message;

        public bool IsBooked;
        public string Booking;
        public string PatientCode;
        public DateTime BookDate;
        public string WhereTakenCode;
        public bool IsNewAttendance;

        public ctcBooking()
        {
            Exe_Result = 1;
            Exe_Message = "";

            IsBooked = false;
            Booking = "";
            PatientCode = "";
            BookDate = new DateTime();
            WhereTakenCode = "";
            IsNewAttendance = false;
        }

        protected ctcBooking(SerializationInfo info, StreamingContext context)
        {
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");

            this.IsBooked = info.GetBoolean("IsBooked");
            this.Booking = info.GetString("Booking");
            this.PatientCode = info.GetString("PatientCode");
            this.BookDate = info.GetDateTime("BookDate");
            this.WhereTakenCode = info.GetString("WhereTakenCode");
            this.IsNewAttendance = info.GetBoolean("IsNewAttendance");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("exe_result", this.Exe_Result);
            info.AddValue("exe_message", this.Exe_Message);

            info.AddValue("IsBooked", this.IsBooked);
            info.AddValue("Booking", this.Booking);
            info.AddValue("PatientCode", this.PatientCode);
            info.AddValue("BookDate", this.BookDate);
            info.AddValue("WhereTakenCode", this.WhereTakenCode);
            info.AddValue("IsNewAttendance", this.IsNewAttendance);
        }

    }
}
