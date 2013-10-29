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
    public class clsBill : ISerializable
    {
        public Int16 Exe_Result;
        public string Exe_Message;

        public string Booking;
        public bool HasInvoice;
        public bool HasReceipt;
        public string ReceiptNo;
        public string InvoiceNo;
        public string PatientCode;
        public string BillingGroupCode;
        public string BillingSubGroupCode;
        public string BillingGroupMembershipNo;
        public bool IsNewAttendance;

        public clsBill()
        {
            Exe_Result = 1;
            Exe_Message = "";

            Booking = "";
            HasInvoice = false;
            HasReceipt = false;
            ReceiptNo = "";
            InvoiceNo = "";
            PatientCode = "";
            BillingGroupCode = "";
            BillingSubGroupCode = "";
            BillingGroupMembershipNo = "";
            IsNewAttendance = false;
        }

        protected clsBill(SerializationInfo info, StreamingContext context)
        {
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");

            this.Booking = info.GetString("Booking");
            this.HasInvoice = info.GetBoolean("HasInvoice");
            this.HasReceipt = info.GetBoolean("HasReceipt");
            this.ReceiptNo = info.GetString("ReceiptNo");
            this.InvoiceNo = info.GetString("InvoiceNo");
            this.PatientCode = info.GetString("PatientCode");
            this.BillingGroupCode = info.GetString("BillingGroupCode");
            this.BillingSubGroupCode = info.GetString("BillingSubGroupCode");
            this.BillingGroupMembershipNo = info.GetString("BillingGroupMembershipNo");
            this.IsNewAttendance = info.GetBoolean("IsNewAttendance");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("exe_result", this.Exe_Result);
            info.AddValue("exe_message", this.Exe_Message);

            info.AddValue("Booking", this.Booking);
            info.AddValue("HasInvoice", this.HasInvoice);
            info.AddValue("HasReceipt", this.HasReceipt);
            info.AddValue("ReceiptNo", this.ReceiptNo);
            info.AddValue("InvoiceNo", this.InvoiceNo);
            info.AddValue("PatientCode", this.PatientCode);
            info.AddValue("BillingGroupCode", this.BillingGroupCode);
            info.AddValue("BillingSubGroupCode", this.BillingSubGroupCode);
            info.AddValue("BillingGroupMembershipNo", this.BillingGroupMembershipNo);
            info.AddValue("IsNewAttendance", this.IsNewAttendance);
        }

    }
}
