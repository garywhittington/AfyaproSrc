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
using System.Xml;
using System.IO;

namespace AfyaPro_NextGen
{
    public partial class frmSetup : DevExpress.XtraEditors.XtraForm
    {
        private AfyaPro_MT.clsFacilitySetup pMdtFacilitySetup;

        #region frmSetup
        public frmSetup()
        {
            InitializeComponent();
        }
        #endregion

        #region cmdOk_Click
        private void cmdOk_Click(object sender, EventArgs e)
        {
            #region test connection to server

            if (txtServerName.Text.Trim() == "")
            {
                Program.Display_Error("Invalid Server Name", false);
                txtServerName.Focus();
                return;
            }

            if (Program.IsNumeric(txtServerPort.Text) == false)
            {
                Program.Display_Error("Invalid Server Port", false);
                txtServerPort.Focus();
                return;
            }

            try
            {
                string mMiddleTier = "tcp://" + txtServerName.Text + ":" + txtServerPort.Text + "/";

                pMdtFacilitySetup = (AfyaPro_MT.clsFacilitySetup)Activator.GetObject(
                    typeof(AfyaPro_MT.clsFacilitySetup),
                    mMiddleTier + "clsFacilitySetup");

                pMdtFacilitySetup.View("", "");
            }
            catch (Exception ex)
            {
                Program.Display_Error(ex.Message, false);
                return;
            }

            #endregion

            if (this.WriteIni() == true)
            {
                this.Close();
            }
        }
        #endregion

        #region cmdExit_Click
        private void cmdExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }
        #endregion

        #region write xml configuration file
        private bool WriteIni()
        {
            try
            {
                //create xml doc and specify formating informations
                XmlTextWriter mXmlTextWriter;
                mXmlTextWriter = new XmlTextWriter(Program.gIniFileName, new System.Text.ASCIIEncoding());
                mXmlTextWriter.Formatting = Formatting.Indented;
                mXmlTextWriter.Indentation = 4;

                //write xml declaration with version 1.0 and a comments
                mXmlTextWriter.WriteStartDocument();
                mXmlTextWriter.WriteComment("Configurations Settings for AfyaPro Client");

                //xml body
                mXmlTextWriter.WriteStartElement("AfyaPro_Client_Configuration");
                //Connections Settings
                mXmlTextWriter.WriteStartElement("Connection_To_Server");

                //servername
                mXmlTextWriter.WriteStartElement("server_name");
                mXmlTextWriter.WriteElementString("servername", txtServerName.Text);
                mXmlTextWriter.WriteEndElement();

                //serverport
                mXmlTextWriter.WriteStartElement("server_port");
                mXmlTextWriter.WriteElementString("serverport", txtServerPort.Text);
                mXmlTextWriter.WriteEndElement();

                mXmlTextWriter.WriteEndElement();
                mXmlTextWriter.WriteEndElement();
                //Write End of document
                mXmlTextWriter.WriteEndDocument();

                //Close any open elements
                mXmlTextWriter.Flush();
                mXmlTextWriter.Close();

                Program.Read_Config_Settings();

                return true;
            }
            catch (Exception e)
            {
                Program.Display_Error(e.Message, false);
                return false;
            }
        }
        #endregion
    }
}