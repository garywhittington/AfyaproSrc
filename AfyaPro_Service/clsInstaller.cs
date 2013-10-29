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
using System.Configuration.Install;
using Microsoft.Win32;

namespace AfyaPro_Service
{
    [RunInstaller(true)]
    public partial class clsInstaller : Installer
    {
        private System.ServiceProcess.ServiceController pServiceController;

        public clsInstaller()
        {
            InitializeComponent();

            //this.AfterInstall += new InstallEventHandler(this.ProjectInstaller_AfterInstall);
        }

        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            //pServiceController = new System.ServiceProcess.ServiceController("AfyaPro_Service");

            //try
            //{
            //    if (pServiceController.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
            //    {
            //        pServiceController.Start();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}