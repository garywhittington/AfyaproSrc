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
    public class clsCode
    {
        private Int16 pExe_Result;
        public Int16 Exe_Result
        {
            get { return pExe_Result; }
            set { pExe_Result = value; }
        }

        private String pExe_Message;
        public String Exe_Message
        {
            get { return pExe_Message; }
            set { pExe_Message = value; }
        }

        private Int16 pCodeKey;
        public Int16 CodeKey
        {
            get { return pCodeKey; }
            set { pCodeKey = value; }
        }

        private String pGeneratedCode;
        public String GeneratedCode
        {
            get { return pGeneratedCode; }
            set { pGeneratedCode = value; }
        }

        public clsCode()
        {
            pExe_Result = 1;
            pExe_Message = "";
            pCodeKey = 0;
            pGeneratedCode = "";
        }
    }
}
