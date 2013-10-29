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
    public class clsBooking : ISerializable
    {
        public Int16 Exe_Result;
        public string Exe_Message;

        public bool IsBooked;
        public string Booking;
        public string PatientCode;
        public DateTime BookDate;
        public string BillingGroupCode;
        public string BillingGroupDescription;
        public string BillingSubGroupCode;
        public string BillingSubGroupDescription;
        public string BillingGroupMembershipNo;
        public string PriceName;
        public string PriceDescription;
        public double CeilingAmount;
        public double BalanceDue;
        public double DepositBalance;
        public string Department;
        public string WhereTakenCode;
        public string WhereTaken;
        public double Weight;
        public double Temperature;
        public Int16 Refered;
        public string ReferedFacility;
        public bool IsNewAttendance;

        public clsBooking()
        {
            Exe_Result = 1;
            Exe_Message = "";

            IsBooked = false;
            Booking = "";
            PatientCode = "";
            BookDate = new DateTime();
            BillingGroupCode = "";
            BillingGroupDescription = "";
            BillingSubGroupCode = "";
            BillingSubGroupDescription = "";
            BillingGroupMembershipNo = "";
            PriceName = "";
            PriceDescription = "";
            CeilingAmount = 0;
            BalanceDue = 0;
            DepositBalance = 0;
            Department = AfyaPro_Types.clsEnums.PatientCategories.OPD.ToString();
            WhereTakenCode = "";
            WhereTaken = "";
            Weight = 0;
            Temperature = 0;
            Refered = 0;
            ReferedFacility = "";
            IsNewAttendance = false;
        }

        protected clsBooking(SerializationInfo info, StreamingContext context)
        {
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");

            this.IsBooked = info.GetBoolean("IsBooked");
            this.Booking = info.GetString("Booking");
            this.PatientCode = info.GetString("PatientCode");
            this.BookDate = info.GetDateTime("BookDate");
            this.BillingGroupCode = info.GetString("BillingGroupCode");
            this.BillingGroupDescription = info.GetString("BillingGroupDescription");
            this.BillingSubGroupCode = info.GetString("BillingSubGroupCode");
            this.BillingSubGroupDescription = info.GetString("BillingSubGroupDescription");
            this.BillingGroupMembershipNo = info.GetString("BillingGroupMembershipNo");
            this.PriceName = info.GetString("PriceName");
            this.PriceDescription = info.GetString("PriceDescription");
            this.CeilingAmount = info.GetDouble("CeilingAmount");
            this.BalanceDue = info.GetDouble("BalanceDue");
            this.DepositBalance = info.GetDouble("DepositBalance");
            this.Department = info.GetString("Department");
            this.WhereTakenCode = info.GetString("WhereTakenCode");
            this.WhereTaken = info.GetString("WhereTaken");
            this.Refered = info.GetInt16("Refered");
            this.Weight = info.GetDouble("Weight");
            this.Temperature = info.GetDouble("Temperature");
            this.ReferedFacility = info.GetString("ReferedFacility");
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
            info.AddValue("BillingGroupCode", this.BillingGroupCode);
            info.AddValue("BillingGroupDescription", this.BillingGroupDescription);
            info.AddValue("BillingSubGroupCode", this.BillingSubGroupCode);
            info.AddValue("BillingSubGroupDescription", this.BillingSubGroupDescription);
            info.AddValue("BillingGroupMembershipNo", this.BillingGroupMembershipNo);
            info.AddValue("PriceName", this.PriceName);
            info.AddValue("PriceDescription", this.PriceDescription);
            info.AddValue("CeilingAmount", this.CeilingAmount);
            info.AddValue("BalanceDue", this.BalanceDue);
            info.AddValue("DepositBalance", this.DepositBalance);
            info.AddValue("Department", this.Department);
            info.AddValue("WhereTakenCode", this.WhereTakenCode);
            info.AddValue("WhereTaken", this.WhereTaken);
            info.AddValue("Refered", this.Refered);
            info.AddValue("Weight", this.Weight);
            info.AddValue("Temperature", this.Temperature);
            info.AddValue("ReferedFacility", this.ReferedFacility);
            info.AddValue("IsNewAttendance", this.IsNewAttendance);
        }

    }
}
