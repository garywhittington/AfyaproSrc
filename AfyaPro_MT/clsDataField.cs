/*
Copyright (C) 2013 AfyaPro Foundation

This file is part of AfyaPro.

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
using System.Linq;
using System.Web;

namespace AfyaPro_MT
{
    internal enum DataTypes
    {
        dbstring,
        dbdecimal,
        dbdatetime,
        dbnumber,
    }

    internal class clsDataField
    {
        private string pFieldName = "";
        internal string FieldName
        {
            set { pFieldName = value; }
            get { return pFieldName; }
        }

        private string pFieldType = "";
        internal string FieldType
        {
            set { pFieldType = value; }
            get { return pFieldType; }
        }

        private object pFieldValue;
        internal object FieldValue
        {
            set { pFieldValue = value; }
            get { return pFieldValue; }
        }

        internal clsDataField()
        {
            pFieldName = "";
            pFieldType = DataTypes.dbstring.ToString();
            pFieldValue = null;
        }

        internal clsDataField(string mFieldName, string mFieldType, object mFieldValue)
        {
            pFieldName = mFieldName;
            pFieldType = mFieldType;
            pFieldValue = mFieldValue;
        }
    }
}
