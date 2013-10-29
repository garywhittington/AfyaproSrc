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
    public class clsRchClient : ISerializable
    {
        public Int16 Exe_Result;
        public String Exe_Message;
        public bool Exist;

        public string code;
        public string surname;
        public string firstname;
        public string othernames;
        public string gender;
        public DateTime birthdate;
        public DateTime regdate;
        public string maritalstatuscode;
        public string allergies;
        public string RegNo;
        public string Residence;
        public string Gravida;
        public string Para;
        public string LMP;
        public string EDD;

        public clsRchClient()
        {
            Exist = false;
            Exe_Result = 1;
            Exe_Message = "";

            code = "";
            surname = "";
            firstname = "";
            othernames = "";
            gender = "";
            birthdate = new DateTime();
            regdate = new DateTime();
            maritalstatuscode = "";
            allergies = "";
            RegNo = "";
            Residence ="";
            Gravida = "";
            Para = "";
            LMP = "";
            EDD = "";
        }

        public clsRchClient(string mcode, string msurname, string mfirstname,
            string mothernames, string mgender, DateTime mbirthdate, DateTime mregdate,
            string mmaritalstatuscode, string mallergies, string mRegNo, string mResidence, string mGravida, string mPara, string mLMP, string mEDD)
        {
            Exist = false;
            Exe_Result = 1;
            Exe_Message = "";

            code = mcode;
            surname = msurname;
            firstname = mfirstname;
            othernames = mothernames;
            gender = mgender;
            birthdate = mbirthdate;
            regdate = mregdate;
            maritalstatuscode = mmaritalstatuscode;
            allergies = mallergies;
            RegNo = mResidence;
            Residence = mResidence;
            Gravida = mGravida;
            Para = mPara;
            LMP = mLMP;
            EDD = mEDD;
        }

        protected clsRchClient(SerializationInfo info, StreamingContext context)
        {
            this.Exist = info.GetBoolean("exist");
            this.Exe_Result = info.GetInt16("exe_result");
            this.Exe_Message = info.GetString("exe_message");

            this.code = info.GetString("code");
            this.surname = info.GetString("surname");
            this.firstname = info.GetString("firstname");
            this.othernames = info.GetString("othernames");
            this.gender = info.GetString("gender");
            this.birthdate = info.GetDateTime("birthdate");
            this.regdate = info.GetDateTime("regdate");
            this.maritalstatuscode = info.GetString("maritalstatuscode");
            this.allergies = info.GetString("allergies");
            this.RegNo = info.GetString("regno");
            this.Residence = info.GetString("residence");
            this.Gravida = info.GetString("gravida");
            this.Para = info.GetString("para");
            this.LMP = info.GetString("lmp");
            this.EDD = info.GetString("edd");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("exe_result", this.Exe_Result);
            info.AddValue("exist", this.Exist);
            info.AddValue("exe_message", this.Exe_Message);

            info.AddValue("code", this.code);
            info.AddValue("surname", this.surname);
            info.AddValue("firstname", this.firstname);
            info.AddValue("othernames", this.othernames);
            info.AddValue("gender", this.gender);
            info.AddValue("birthdate", this.birthdate);
            info.AddValue("regdate", this.regdate);
            info.AddValue("maritalstatuscode", this.maritalstatuscode);
            info.AddValue("allergies", this.allergies);

            info.AddValue("regno", this.RegNo);
            info.AddValue("residence", this.Residence);
            info.AddValue("gravida", this.Gravida);
            info.AddValue("para", this.Para);
            info.AddValue("lmp", this.LMP);
            info.AddValue("edd", this.EDD);
            
        }

    }
}
