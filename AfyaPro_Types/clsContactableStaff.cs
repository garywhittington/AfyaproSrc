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
    public class clsContactableStaff : ISerializable
    {
        public Int16 Exe_Result;
        public String Exe_Message;
        public bool Exist;

        public string StaffName;
        public string Position;
        public string Phone;
        

        public clsContactableStaff()
        {
           
            StaffName = "";
            Position = "";
            Phone = "";
            
        }

        public clsContactableStaff(string mStaffName, string mPosition, string mPhone)
        {
           
            StaffName = mStaffName;
            Position = mPosition;
            Phone = mPhone;
            
        }

        protected clsContactableStaff(SerializationInfo info, StreamingContext context)
        {
            
            this.StaffName = info.GetString("StaffName");
            this.Position = info.GetString("Position");
            this.Phone = info.GetString("Phone");
           
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
           
            info.AddValue("StaffName", this.StaffName);
            info.AddValue("Position", this.Position);
            info.AddValue("Phone", this.Phone);
                       
        }

    }
}
