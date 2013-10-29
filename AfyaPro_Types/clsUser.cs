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
    public class clsUser : ISerializable
    {
        public Int16 Exe_Result;
        public String Exe_Message;

        public Boolean ValidCode;
        public Boolean ValidPassword;
        public String Code;
        public String Description;
        public String Password;
        public String UserGroupCode;
        public String UserGroupFormsLayoutTemplateName;
        public String StoreCode;
        public String Address;
        public String Phone;
        public String Occupation;
        public string DefaultCustomerGroupCode;
        public string DefaultCustomerSubGroupCode;
        public string DefaultPaymentTypeCode;
        public string DefaultPriceCategoryCode;
        public int AllowChangingTransDate;

        public clsUser()
        {
            Exe_Result = 1;
            Exe_Message = "";
            ValidCode = false;
            ValidPassword = false;
            Code = "";
            Description = "";
            Password = "";
            UserGroupCode = "";
            UserGroupFormsLayoutTemplateName = "";
            StoreCode = "";
            Address = "";
            Phone = "";
            Occupation = "";
            DefaultCustomerGroupCode = "";
            DefaultCustomerSubGroupCode = "";
            DefaultPaymentTypeCode = "";
            DefaultPriceCategoryCode = "";
            AllowChangingTransDate = 0;
        }

        protected clsUser(SerializationInfo info, StreamingContext context)
        {
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");
            this.ValidCode = info.GetBoolean("validCode");
            this.ValidPassword = info.GetBoolean("ValidPassword");
            this.Code = info.GetString("Code");
            this.Description = info.GetString("Description");
            this.Password = info.GetString("Password");
            this.UserGroupCode = info.GetString("UserGroupCode");
            this.UserGroupFormsLayoutTemplateName = info.GetString("UserGroupFormsLayoutTemplateName");
            this.StoreCode = info.GetString("StoreCode");
            this.Address = info.GetString("Address");
            this.Phone = info.GetString("Phone");
            this.Occupation = info.GetString("Occupation");
            this.DefaultCustomerGroupCode = info.GetString("DefaultCustomerGroupCode");
            this.DefaultCustomerSubGroupCode = info.GetString("DefaultCustomerSubGroupCode");
            this.DefaultPaymentTypeCode = info.GetString("DefaultPaymentTypeCode");
            this.DefaultPriceCategoryCode = info.GetString("DefaultPriceCategoryCode");
            this.AllowChangingTransDate = info.GetInt32("AllowChangingTransDate");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("exe_result", this.Exe_Result);
            info.AddValue("exe_message", this.Exe_Message);
            info.AddValue("validCode", this.ValidCode);
            info.AddValue("ValidPassword", this.ValidPassword);
            info.AddValue("Code", this.Code);
            info.AddValue("Description", this.Description);
            info.AddValue("Password", this.Password);
            info.AddValue("UserGroupCode", this.UserGroupCode);
            info.AddValue("UserGroupFormsLayoutTemplateName", this.UserGroupFormsLayoutTemplateName);
            info.AddValue("StoreCode", this.StoreCode);
            info.AddValue("Address", this.Address);
            info.AddValue("Phone", this.Phone);
            info.AddValue("Occupation", this.Occupation);
            info.AddValue("DefaultCustomerGroupCode", this.DefaultCustomerGroupCode);
            info.AddValue("DefaultCustomerSubGroupCode", this.DefaultCustomerSubGroupCode);
            info.AddValue("DefaultPaymentTypeCode", this.DefaultPaymentTypeCode);
            info.AddValue("DefaultPriceCategoryCode", this.DefaultPriceCategoryCode);
            info.AddValue("AllowChangingTransDate", this.AllowChangingTransDate);
        }
    }
}
