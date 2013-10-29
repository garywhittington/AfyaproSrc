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
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Localization;
using System.Data;

namespace AfyaPro_NextGen
{
    public class clsGridLocalizer : GridLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            //DataView mDvEnglish = new DataView();
            //mDvEnglish.Table = Program.gDtEnglish;
            //mDvEnglish.Sort = "controlname";

            //if (mDvEnglish.Find(id.ToString()) < 0)
            //{
            //    DataRow mNewRow = Program.gDtEnglish.NewRow();
            //    mNewRow["objecttype"] = "XtraGrid";
            //    mNewRow["controlname"] = id.ToString();
            //    mNewRow["description"] = base.GetLocalizedString(id);
            //    Program.gDtEnglish.Rows.Add(mNewRow);
            //    Program.gDtEnglish.AcceptChanges();
            //}

            //interprete
            DataView mDvLang = new DataView();
            mDvLang.Table = Program.gDtGridLang;
            mDvLang.Sort = "controlname";
            int mRowIndex = mDvLang.Find(id.ToString());
            if (mRowIndex >= 0)
            {
                return mDvLang[mRowIndex]["description"].ToString();
            }

            return base.GetLocalizedString(id);
        }
    }

}
