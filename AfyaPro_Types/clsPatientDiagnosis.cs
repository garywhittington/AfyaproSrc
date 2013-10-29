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
    public class clsPatientDiagnosis : ISerializable
    {
        public string diagnosiscode;
        public int isprimary;
        public string doctorcode;
        public string history;
        public string examination;
        public string investigation;
        public string treatments;
        public string referaldescription;
        public string episodecode;

        public clsPatientDiagnosis()
        {
            diagnosiscode = "";
            isprimary = 0;
            doctorcode = "";
            history = "";
            examination = "";
            investigation = "";
            treatments = "";
            referaldescription = "";
            episodecode = "";
        }

        protected clsPatientDiagnosis(SerializationInfo info, StreamingContext context)
        {
            this.diagnosiscode = info.GetString("diagnosiscode");
            this.isprimary = info.GetInt32("isprimary");
            this.doctorcode = info.GetString("doctorcode");
            this.history = info.GetString("history");
            this.examination = info.GetString("examination");
            this.investigation = info.GetString("investigation");
            this.treatments = info.GetString("treatments");
            this.referaldescription = info.GetString("referaldescription");
            this.episodecode = info.GetString("episodecode");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("diagnosiscode", this.diagnosiscode);
            info.AddValue("isprimary", this.isprimary);
            info.AddValue("doctorcode", this.doctorcode);
            info.AddValue("history", this.history);
            info.AddValue("examination", this.examination);
            info.AddValue("investigation", this.investigation);
            info.AddValue("treatments", this.treatments);
            info.AddValue("referaldescription", this.referaldescription);
            info.AddValue("episodecode", this.episodecode);
        }

    }
}
