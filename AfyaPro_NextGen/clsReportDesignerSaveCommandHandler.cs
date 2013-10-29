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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UserDesigner;
using System.IO;
using System.Xml;

namespace AfyaPro_NextGen
{
    public class clsReportDesignerSaveCommandHandler : ICommandHandler
    {
        XRDesignPanel DesignPanel;
        private AfyaPro_MT.clsReporter pMdtReporter;
        internal string pFileName = "";

        public clsReportDesignerSaveCommandHandler(XRDesignPanel mDesignPanel, string mFileName)
        {
            this.DesignPanel = mDesignPanel;
            this.pFileName = mFileName;
            pMdtReporter = (AfyaPro_MT.clsReporter)Activator.GetObject(
                typeof(AfyaPro_MT.clsReporter),
                Program.gMiddleTier + "clsReporter");
        }

        public virtual void HandleCommand(ReportCommand command, object[] args, ref bool handled)
        {
            if (!CanHandleCommand(command)) return;

            // Save a report.
            SaveReport();

            // Set handled to true to avoid the standard saving procedure to be called.
            handled = true;
        }

        public virtual bool CanHandleCommand(ReportCommand command)
        {
            // This handler is used for SaveFile, SaveFileAs and Closing commands.
            //return command == ReportCommand.SaveFile ||
            //    command == ReportCommand.SaveFileAs ||
            //    command == ReportCommand.Closing;
            return command == ReportCommand.SaveFile; //only for save
        }

        void SaveReport()
        {
            MemoryStream mMemoryStream = new MemoryStream();
            DesignPanel.Report.SaveLayout(mMemoryStream);

            pMdtReporter.Save_ReportTemplate(pFileName, mMemoryStream.ToArray(), 0, 0, false);

            // Prevent the "Report has been changed" dialog from being shown.
            DesignPanel.ReportState = ReportState.Saved;
        }
    }
}
