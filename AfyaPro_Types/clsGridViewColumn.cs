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

namespace AfyaPro_Types
{
    public class clsGridViewColumn
    {
        private string pColumnName;
        public string ColumnName
        {
            get { return pColumnName; }
            set { pColumnName = value; }
        }

        private string pColumnHeader;
        public string ColumnHeader
        {
            get { return pColumnHeader; }
            set { pColumnHeader = value; }
        }

        private string pLanguageId;
        public string LanguageId
        {
            get { return pLanguageId; }
            set { pLanguageId = value; }
        }

        private Type pDataType;
        public Type DataType
        {
            get { return pDataType; }
            set { pDataType = value; }
        }

        private string pDisplayFormat;
        public string DisplayFormat
        {
            get { return pDisplayFormat; }
            set { pDisplayFormat = value; }
        }

        private double pColumnWidth;
        public double ColumnWidth
        {
            get { return pColumnWidth; }
            set { pColumnWidth = value; }
        }

        public clsGridViewColumn()
        {
            pColumnName = "";
            pColumnHeader = "";
            pLanguageId = "";
            pColumnWidth = 0;
            pDataType = typeof(System.String);
            pDisplayFormat = "";
        }

        public clsGridViewColumn(string mColumnName, string mColumnHeader, double mColumnWidth)
        {
            pColumnName = mColumnName;
            pColumnHeader = mColumnHeader;
            pColumnWidth = mColumnWidth;
            pDataType = typeof(System.String);
        }

        public clsGridViewColumn(string mColumnName, string mColumnHeader, double mColumnWidth, Type mDataType)
        {
            pColumnName = mColumnName;
            pColumnHeader = mColumnHeader;
            pColumnWidth = mColumnWidth;
            pDataType = mDataType;
        }

        public clsGridViewColumn(string mColumnName, string mColumnHeader, string mLanguageId, double mColumnWidth)
        {
            pColumnName = mColumnName;
            pColumnHeader = mColumnHeader;
            pLanguageId = mLanguageId;
            pColumnWidth = mColumnWidth;
            pDataType = typeof(System.String);
            pDisplayFormat = "";
        }

        public clsGridViewColumn(string mColumnName, string mColumnHeader, string mLanguageId, double mColumnWidth, Type mDataType)
        {
            pColumnName = mColumnName;
            pColumnHeader = mColumnHeader;
            pLanguageId = mLanguageId;
            pColumnWidth = mColumnWidth;
            pDataType = mDataType;
            pDisplayFormat = "";
        }

        public clsGridViewColumn(string mColumnName, string mColumnHeader, string mLanguageId, double mColumnWidth, Type mDataType, string mDisplayFormat)
        {
            pColumnName = mColumnName;
            pColumnHeader = mColumnHeader;
            pLanguageId = mLanguageId;
            pColumnWidth = mColumnWidth;
            pDataType = mDataType;
            pDisplayFormat = mDisplayFormat;
        }
    }
}
