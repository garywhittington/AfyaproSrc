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
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace AfyaPro_ServerAdmin
{
    public partial class frmDataMigration : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private Type pType;
        private string pClassName = "";

        private bool pErrorOnPage0 = false;
        private bool pErrorOnPage1 = false;
        private bool pErrorOnPage2 = false;

        private DataTable pDtCountries = new DataTable("countries");
        private DataTable pDtRegions = new DataTable("regions");
        private DataTable pDtDistricts = new DataTable("districts");
        private DataTable pDtEthnicities = new DataTable("ethnicities");
        private DataTable pDtReligions = new DataTable("religions");
        private DataTable pDtBillingItemGroups = new DataTable("billingitemgroups");
        private DataTable pDtBillingItemSubGroups = new DataTable("billingitemsubgroups");
        private DataTable pDtBillingItems = new DataTable("billingitems");
        private DataTable pDtCurrencies = new DataTable("currencies");
        private DataTable pDtIPDDischargeStatus = new DataTable("ipddischargestatus");
        private DataTable pDtDoctors = new DataTable("doctors");
        private DataTable pDtStaffs = new DataTable("staffs");
        private DataTable pDtLaboratories = new DataTable("laboratories");
        private DataTable pDtLaboratoryTestGroups = new DataTable("labtestgroups");
        private DataTable pDtLaboratoryTests = new DataTable("labtests");
        private DataTable pDtIPDWards = new DataTable("ipdwards");
        private DataTable pDtIPDWardRooms = new DataTable("ipdwardrooms");
        private DataTable pDtTreatmentPoints = new DataTable("treatmentpoints");
        private DataTable pDtFacilityOptions = new DataTable("facilityoptions");
        private DataTable pDtCustomerGroups = new DataTable("customergroups");
        private DataTable pDtCustomerSubGroups = new DataTable("customersubgroups");
        private DataTable pDtPatients = new DataTable("patients");
        private DataTable pDtReAttendanceNumbers = new DataTable("revisitingnumbers");
        private DataTable pDtCustomerGroupMembers = new DataTable("customergroupmembers");
        private DataTable pDtDebtorsIndividual = new DataTable("debtorsindividual");
        private DataTable pDtDebtorsGroup = new DataTable("debtorsgroup");
        private DataTable pDtPatientRecentVisits = new DataTable("recentvisits");
        private DataTable pDtPatientArchiveVisits = new DataTable("archivevisits");
        private DataTable pDtPatientIPDAdmissions = new DataTable("ipdadmissions");
        private DataTable pDtPatientIPDTransfers = new DataTable("ipdtransfers");
        private DataTable pDtPatientIPDDischarges = new DataTable("ipddischarges");
        private DataTable pDtPatientDiagnoses = new DataTable("patientdiagnoses");
        private DataTable pDtPatientLabTests = new DataTable("patientlabtests");
        private DataTable pDtPaymentTypes = new DataTable("paymenttypes");
        private DataTable pDtSurgeryTypes = new DataTable("surgerytypes");
        private DataTable pDtTheatres = new DataTable("theatres");
        private DataTable pDtStreets = new DataTable("streets");
        private DataTable pDtPriceCategories = new DataTable("pricecategories");
        private DataTable pDtPatientSurnames = new DataTable("patientsurnames");
        private DataTable pDtPatientFirstNames = new DataTable("patientfirstnames");
        private DataTable pDtPatientOthernames = new DataTable("patientothernames");
        private DataTable pDtVillages = new DataTable("villages");
        private DataTable pDtOccupations = new DataTable("occupations");
        private DataTable pDtDiagnosesGroups = new DataTable("diagnosesgroups");
        private DataTable pDtDiagnoses = new DataTable("diagnoses");
        private DataTable pDtBirthComplications = new DataTable("birthcomplications");
        private DataTable pDtBirthMethods = new DataTable("birthmethods");
        private DataTable pDtDangerIndicators = new DataTable("dangerindicators");
        private DataTable pDtFamilyPlanningMethods = new DataTable("familyplanningmethods");
        private DataTable pDtRchClients = new DataTable("rchclients");
        private DataTable pDtAntenatalAttendances = new DataTable("antenatalattendances");
        private DataTable pDtAntenatalAttendanceLog = new DataTable("antenatalattendancelog");
        private DataTable pDtPostnatalAttendances = new DataTable("postnatalattendances");
        private DataTable pDtPostnatalAttendanceLog = new DataTable("postnatalattendancelog");
        private DataTable pDtPostnatalChildren = new DataTable("postnatalchildren");
        private DataTable pDtChildrenAttendances = new DataTable("childrenattendances");
        private DataTable pDtChildrenAttendanceLog = new DataTable("childrenattendancelog");
        private DataTable pDtFPlanAttendances = new DataTable("fplanattendances");
        private DataTable pDtFPlanAttendanceLog = new DataTable("fplanattendancelog");
        private DataTable pDtFPlanMethodsUsed = new DataTable("fplanmethodsused");
        private DataTable pDtFPlanMethods = new DataTable("fplanmethods");

        private DataTable pDtObjects = new DataTable("objects");
        private DataTable pDtMigrating = new DataTable("migrating");

        private Boolean pFinishedCollecting = false;
        private Boolean pFinishedMigrating = false;
        private BackgroundWorker pBackgroundWorkder;

        private string pActivity = "Collecting";

        #endregion

        #region frmDataMigration
        public frmDataMigration()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;

            pDtObjects.Columns.Add("selected", typeof(System.Boolean));
            pDtObjects.Columns.Add("description", typeof(System.String));

            pDtMigrating.Columns.Add("selected", typeof(System.Boolean));
            pDtMigrating.Columns.Add("description", typeof(System.String));

            grdObjects.DataSource = pDtObjects;
            grdMigrating.DataSource = pDtMigrating;

            this.Fill_Objects();
            this.Fill_Migrating();

            pBackgroundWorkder = new BackgroundWorker();
            pBackgroundWorkder.WorkerReportsProgress = true;
            pBackgroundWorkder.DoWork += new DoWorkEventHandler(pBackgroundWorkder1_DoWork);
            pBackgroundWorkder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(pBackgroundWorkder1_RunWorkerCompleted);
            pBackgroundWorkder.ProgressChanged += new ProgressChangedEventHandler(pBackgroundWorkder1_ProgressChanged);
        }
        #endregion

        #region Fill_Objects
        private void Fill_Objects()
        {
            DataRow mNewRow;

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Countries";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Regions";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Districts";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Ethnicities";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Religions";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Billing item groups";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Billing item sub groups";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Billing items";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Currencies";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "IPD discharge status";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Doctors";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Staffs";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Laboratories";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Laboratory test groups";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Laboratory tests";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "IPD wards";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "IPD ward rooms";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Treatment points";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Facility setup";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Customer groups";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Customer sub groups";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Price categories";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Payment types";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Surgery types";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Theatres";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Streets";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient common surnames";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient common firstnames";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient common other names";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Villages";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Occupations";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Diagnoses Groups";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Diagnoses";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patients";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Re attendance numbers";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Customer group members";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Debtors";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient recent visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient archive visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient IPD admissions";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient IPD transfers";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient IPD discharges";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient diagnoses and treatments";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient laboratory tests";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patients recent visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patients archive visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "RCH clients";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Antenatal recent visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Antenatal archive visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Postnatal recent visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Postnatal archive visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Postnatal children";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Children recent visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Children archive visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Family planning recent visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Family planning archive visits";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Family planning methods";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();

            mNewRow = pDtObjects.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Family planning methods used";
            pDtObjects.Rows.Add(mNewRow);
            pDtObjects.AcceptChanges();
        }
        #endregion

        #region Fill_Migrating
        private void Fill_Migrating()
        {
            DataRow mNewRow;

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Countries";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Regions";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Districts";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Ethnicities";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Religions";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Billing item groups";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Billing item sub groups";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Billing items";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Currencies";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "IPD discharge status";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Doctors";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Staffs";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Laboratories";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Laboratory test groups";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Laboratory tests";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "IPD wards";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "IPD ward rooms";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Treatment points";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Facility setup";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Customer groups";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Customer sub groups";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Price categories";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Payment types";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Surgery types";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Theatres";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Streets";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient common surnames";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient common firstnames";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient common other names";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Villages";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Occupations";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Diagnoses Groups";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Diagnoses";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patients";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Re attendance numbers";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Customer group members";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Debtors";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patients recent visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patients archive visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient IPD admissions";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient IPD transfers";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient IPD discharges";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient diagnoses and treatments";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Patient laboratory tests";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "RCH clients";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Antenatal recent visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Antenatal archive visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Postnatal recent visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Postnatal archive visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Postnatal children";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Children recent visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Children archive visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Family planning recent visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();

            mNewRow = pDtMigrating.NewRow();
            mNewRow["selected"] = false;
            mNewRow["description"] = "Family planning archive visits";
            pDtMigrating.Rows.Add(mNewRow);
            pDtMigrating.AcceptChanges();
        }
        #endregion

        #region frmDataMigration_Load
        private void frmDataMigration_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region wizardControl1_NextClick
        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            string mFunctionName = "wizardControl1_NextClick";

            try
            {
                switch (e.Page.Name.ToLower())
                {
                    case "page0":
                        {
                            pErrorOnPage0 = false;

                            pActivity = "collecting";

                            this.Cursor = Cursors.WaitCursor;

                            progObjects.Properties.Step = 1;
                            progObjects.Properties.PercentView = true;
                            progObjects.Properties.ShowTitle = true;

                            pFinishedCollecting = false;

                            pBackgroundWorkder.RunWorkerAsync();

                            while (pFinishedCollecting == false)
                            {
                                Application.DoEvents();
                            }

                            if (pFinishedCollecting == false)
                            {
                                pErrorOnPage0 = true;
                            }
                        }
                        break;

                    case "page1":
                        {
                            pErrorOnPage1 = false;

                            pActivity = "migrating";

                            this.Cursor = Cursors.WaitCursor;

                            pFinishedMigrating = false;

                            progMigrating.Properties.Step = 1;
                            progMigrating.Properties.PercentView = true;
                            progMigrating.Properties.ShowTitle = true;

                            pBackgroundWorkder.RunWorkerAsync();

                            while (pFinishedMigrating == false)
                            {
                                Application.DoEvents();
                            }

                            if (pFinishedMigrating == false)
                            {
                                pErrorOnPage1 = true;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region wizardControl1_FinishClick
        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region wizardControl1_SelectedPageChanging
        private void wizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                switch (e.PrevPage.Name.ToLower())
                {
                    case "page0":
                        {
                            if (pErrorOnPage0 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page1":
                        {
                            if (pErrorOnPage1 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                    case "page2":
                        {
                            if (pErrorOnPage2 == true)
                            {
                                e.Cancel = true;
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region wizardControl1_CancelClick
        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Run in a separate thread

        #region pBackgroundWorkder1_DoWork

        private void pBackgroundWorkder1_DoWork(object sender, DoWorkEventArgs e)
        {
            OdbcConnection mConn = new OdbcConnection();
            OdbcDataAdapter mDataAdapter = new OdbcDataAdapter();
            OdbcTransaction mTrans = null;
            OdbcCommand mCommand = new OdbcCommand();

            string mFunctionName = "pBackgroundWorkder1_DoWork";

            try
            {
                switch (pActivity.Trim().ToLower())
                {
                    case "collecting":
                        {
                            mConn.ConnectionString = Program.Get_ConnString("afyapro");
                            mConn.Open();

                            mCommand.Connection = mConn;

                            int mTotalRows = pDtObjects.Rows.Count;

                            for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            {
                                this.MarkNotDone(mRowCount);
                            }

                            int mRowIndex = 0;

                            #region retrieve data

                            #region countries
                            mCommand.CommandText = "select * from countries";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtCountries);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region regions
                            mCommand.CommandText = "select * from regions";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtRegions);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region districts
                            mCommand.CommandText = "select * from districts";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtDistricts);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region ethnicities
                            mCommand.CommandText = "select * from ethnicities";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtEthnicities);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region religions
                            mCommand.CommandText = "select * from religions";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtReligions);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitybillinggroups
                            mCommand.CommandText = "select * from facilitybillinggroups";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtBillingItemGroups);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitybillingsubgroups
                            mCommand.CommandText = "select * from facilitybillingsubgroups";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtBillingItemSubGroups);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitybillingitems
                            mCommand.CommandText = "select * from facilitybillingitems";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtBillingItems);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitycurrencies
                            mCommand.CommandText = "select * from facilitycurrencies";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtCurrencies);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitydischargestatus
                            mCommand.CommandText = "select * from facilitydischargestatus";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtIPDDischargeStatus);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitydoctors
                            mCommand.CommandText = "select * from facilitydoctors";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtDoctors);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitystaffs
                            mCommand.CommandText = "select * from facilitystaffs";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtStaffs);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitylaboratories
                            mCommand.CommandText = "select * from facilitylaboratories";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtLaboratories);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitylaboratorytestgroups
                            mCommand.CommandText = "select * from facilitylaboratorytestgroups";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtLaboratoryTestGroups);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitylaboratorytests
                            mCommand.CommandText = "select * from facilitylaboratorytests";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtLaboratoryTests);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitywards
                            mCommand.CommandText = "select * from facilitywards";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtIPDWards);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitywardrooms
                            mCommand.CommandText = "select * from facilitywardrooms";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtIPDWardRooms);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilityclinics
                            mCommand.CommandText = "select * from facilityclinics";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtTreatmentPoints);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilityoptions
                            mCommand.CommandText = "select * from facilityoptions";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtFacilityOptions);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitycorporates
                            mCommand.CommandText = "select * from facilitycorporates";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtCustomerGroups);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilityexemptiongroups
                            mCommand.CommandText = "select * from facilityexemptiongroups";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtCustomerSubGroups);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitypricecategories
                            mCommand.CommandText = "select * from facilitypricecategories";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPriceCategories);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitypaymenttypes
                            mCommand.CommandText = "select * from facilitypaymenttypes";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPaymentTypes);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitysurgerytypes
                            mCommand.CommandText = "select * from facilitysurgerytypes";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtSurgeryTypes);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitytheatres
                            mCommand.CommandText = "select * from facilitytheatres";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtTheatres);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region streets
                            mCommand.CommandText = "select * from streets";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtStreets);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patient common surnames
                            mCommand.CommandText = "select distinct(surname) surname from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientSurnames);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patient common first names
                            mCommand.CommandText = "select distinct(description) firstname from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientFirstNames);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patient common other names
                            mCommand.CommandText = "select distinct(othernames) othernames from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientOthernames);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region villages
                            mCommand.CommandText = "select distinct(village) village from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtVillages);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region occupations
                            mCommand.CommandText = "select distinct(occupation) occupation from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtOccupations);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region diagnoses groups
                            pDtDiagnosesGroups.Columns.Add("code", typeof(System.String));
                            pDtDiagnosesGroups.Columns.Add("description", typeof(System.String));

                            DataRow mNewRow = pDtDiagnosesGroups.NewRow();
                            mNewRow["code"] = "gen";
                            mNewRow["description"] = "General";
                            pDtDiagnosesGroups.Rows.Add(mNewRow);
                            pDtDiagnosesGroups.AcceptChanges();
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region diagnoses
                            mCommand.CommandText = "select * from facilitydiagnosis";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtDiagnoses);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patients
                            mCommand.CommandText = "select * from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatients);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region revisitingnumbers
                            mCommand.CommandText = "select * from revisitingnumbers";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtReAttendanceNumbers);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patientcorporates
                            mCommand.CommandText = "select * from patientcorporates";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtCustomerGroupMembers);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region debtors
                            mCommand.CommandText = "select patientcode,sum(balance) balance from debtors "
                            + "where corporatecode='' or corporatecode is null group by patientcode";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtDebtorsIndividual);
                            mCommand.CommandText = "select corporatecode,sum(balance) balance from debtors "
                            + "where not (corporatecode='' or corporatecode is null) group by corporatecode";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtDebtorsGroup);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitybookings
                            mCommand.CommandText = "select * from facilitybookings";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientRecentVisits);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region facilitybookinglog
                            mCommand.CommandText = "select * from facilitybookinglog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientArchiveVisits);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patientadmissions
                            mCommand.CommandText = "select * from patientwardlog where transcode=1 or transcode=2";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientIPDAdmissions);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patienttransfers
                            mCommand.CommandText = "select * from patienttransferlog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientIPDTransfers);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patientdischarges
                            mCommand.CommandText = "select * from patientwardlog where not (admissionstop is null)";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientIPDDischarges);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patientdiagnosis
                            mCommand.CommandText = "select * from patientdiagnosis";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientDiagnoses);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patientlaboratorytests
                            mCommand.CommandText = "select * from patientlaboratorytests";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPatientLabTests);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region patients
                            mCommand.CommandText = "select * from rch_customers";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtRchClients);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_antenatalattendances
                            mCommand.CommandText = "select * from rch_antenatalattendances";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtAntenatalAttendances);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_antenatalattendancelog
                            mCommand.CommandText = "select * from rch_antenatalattendancelog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtAntenatalAttendanceLog);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_postnatalattendances
                            mCommand.CommandText = "select * from rch_postnatalattendances";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPostnatalAttendances);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_postnatalattendancelog
                            mCommand.CommandText = "select * from rch_postnatalattendancelog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPostnatalAttendanceLog);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_postnatalchildren
                            mCommand.CommandText = "select * from rch_postnatalchildren";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtPostnatalChildren);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_childrenattendances
                            mCommand.CommandText = "select * from rch_childrenattendances";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtChildrenAttendances);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_childrenattendancelog
                            mCommand.CommandText = "select * from rch_childrenattendancelog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtChildrenAttendanceLog);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_fplanattendances
                            mCommand.CommandText = "select * from rch_fplanattendances";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtFPlanAttendances);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_fplanattendancelog
                            mCommand.CommandText = "select * from rch_fplanattendancelog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtFPlanAttendanceLog);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_fplanmethods
                            mCommand.CommandText = "select * from rch_fplanmethods";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtFPlanMethods);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #region rch_fplanmethodsusedlog
                            mCommand.CommandText = "select * from rch_fplanmethodsusedlog";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(pDtFPlanMethodsUsed);
                            this.MarkDone(mRowIndex);
                            mRowIndex++;
                            pBackgroundWorkder.ReportProgress((mRowIndex * 100) / mTotalRows);
                            #endregion

                            #endregion
                        }
                        break;
                    case "migrating":
                        {
                            int mTotalRows = 0;
                            int mRowIndex = 0;

                            mConn.ConnectionString = Program.Get_ConnString(Program.gDatabaseName);
                            mConn.Open();
                            mCommand.Connection = mConn;

                            mTotalRows = pDtMigrating.Rows.Count;
                            for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            {
                                this.MarkNotDone(mRowCount);
                            }

                            #region migrate data

                            DataTable mDtMigrationLog = new DataTable("migrationlog");
                            mCommand.CommandText = "select * from sys_migrationlog order by autocode desc";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtMigrationLog);

                            if (mDtMigrationLog.Rows.Count > 0)
                            {
                                int mAutoCode = Program.DbValue_ToInt32(mDtMigrationLog.Rows[0]["autocode"]);
                                DataRow mRowToDelete = mDtMigrationLog.Rows[0];
                                mRowToDelete.Delete();
                                mDtMigrationLog.AcceptChanges();

                                mCommand.CommandText = "delete from sys_migrationlog where autocode=" + mAutoCode;
                                mCommand.ExecuteNonQuery();
                            }

                            DataView mDvMigrationLog = new DataView();
                            mDvMigrationLog.Table = mDtMigrationLog;
                            mDvMigrationLog.Sort = "tablename";

                            #region countries

                            if (mDvMigrationLog.Find("countries") < 0)
                            {
                                mTotalRows = pDtCountries.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from countries";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtCountries.Rows[mRowCount];

                                    mCommand.CommandText = "insert into countries(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('countries')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region regions

                            if (mDvMigrationLog.Find("regions") < 0)
                            {
                                mTotalRows = pDtRegions.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from regions";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtRegions.Rows[mRowCount];

                                    mCommand.CommandText = "insert into regions(countrycode,code,description) values('"
                                    + mDataRow["countrycode"].ToString().Trim() + "','" + mDataRow["code"].ToString().Trim()
                                    + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('regions')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region districts

                            if (mDvMigrationLog.Find("districts") < 0)
                            {
                                mTotalRows = pDtDistricts.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from districts";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtDistricts.Rows[mRowCount];

                                    mCommand.CommandText = "insert into districts(countrycode,regioncode,code,description) values('"
                                    + mDataRow["countrycode"].ToString().Trim() + "','" + mDataRow["regioncode"].ToString().Trim()
                                    + "','" + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('districts')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region ethnicities

                            if (mDvMigrationLog.Find("ethnicities") < 0)
                            {
                                mTotalRows = pDtEthnicities.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientextrafieldlookup where fieldname='ethnicity'";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtEthnicities.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description) "
                                    + "values('ethnicity','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('ethnicities')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region religions

                            if (mDvMigrationLog.Find("religions") < 0)
                            {
                                mTotalRows = pDtReligions.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientextrafieldlookup where fieldname='religion'";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtReligions.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description) "
                                    + "values('religion','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('religions')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region billingitemgroups

                            if (mDvMigrationLog.Find("billingitemgroups") < 0)
                            {
                                mTotalRows = pDtBillingItemGroups.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitybillinggroups";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtBillingItemGroups.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitybillinggroups(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('billingitemgroups')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region billingitemsubgroups

                            if (mDvMigrationLog.Find("billingitemsubgroups") < 0)
                            {
                                mTotalRows = pDtBillingItemSubGroups.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitybillingsubgroups";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtBillingItemSubGroups.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitybillingsubgroups(groupcode,code,description) values('"
                                    + mDataRow["groupcode"].ToString().Trim() + "','" + mDataRow["code"].ToString().Trim() + "','"
                                    + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('billingitemsubgroups')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region billingitems

                            if (mDvMigrationLog.Find("billingitems") < 0)
                            {
                                mTotalRows = pDtBillingItems.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitybillingitems";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtBillingItems.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitybillingitems(groupcode,subgroupcode,code,description,"
                                    + "price1,price2,price3,price4,price5,price6,price7,price8,price9,price10) values('"
                                    + mDataRow["groupcode"].ToString().Trim() + "','" + mDataRow["subgroupcode"].ToString().Trim() + "','"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "',"
                                    + Program.DbValue_ToDouble(mDataRow["price1"]) + "," + Program.DbValue_ToDouble(mDataRow["price2"]) + ","
                                    + Program.DbValue_ToDouble(mDataRow["price3"]) + "," + Program.DbValue_ToDouble(mDataRow["price4"]) + ","
                                    + Program.DbValue_ToDouble(mDataRow["price5"]) + "," + Program.DbValue_ToDouble(mDataRow["price6"]) + ","
                                    + Program.DbValue_ToDouble(mDataRow["price7"]) + "," + Program.DbValue_ToDouble(mDataRow["price8"]) + ","
                                    + Program.DbValue_ToDouble(mDataRow["price9"]) + "," + Program.DbValue_ToDouble(mDataRow["price10"]) + ")";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('billingitems')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region currencies

                            if (mDvMigrationLog.Find("currencies") < 0)
                            {
                                mTotalRows = pDtCurrencies.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitycurrencies";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtCurrencies.Rows[mRowCount];

                                    string mCode = mDataRow["code"].ToString().Trim();
                                    int mDefaultCurrency = 0;
                                    try
                                    {
                                        mDefaultCurrency = Program.DbValue_ToInt16(mDataRow["defaultcurrency"]);
                                    }
                                    catch { }
                                    if (mDefaultCurrency == 1)
                                    {
                                        mCode = "LocalCurr";
                                    }

                                    mCommand.CommandText = "insert into facilitycurrencies(code,description,exchangerate,currencysymbol) "
                                    + "values('" + mCode + "','" + mDataRow["description"].ToString().Trim()
                                    + "'," + Program.DbValue_ToDouble(mDataRow["exchangerate"]) + ",'" + mDataRow["currencysymbol"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('currencies')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region ipddischargestatus

                            if (mDvMigrationLog.Find("ipddischargestatus") < 0)
                            {
                                mTotalRows = pDtIPDDischargeStatus.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitydischargestatus";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtIPDDischargeStatus.Rows[mRowCount];

                                    string mCode = mDataRow["code"].ToString().Trim();
                                    int mDeathStatus = 0;
                                    try
                                    {
                                        mDeathStatus = Program.DbValue_ToInt16(mDataRow["deathstatus"]);
                                    }
                                    catch { }
                                    if (mDeathStatus == 1)
                                    {
                                        mCode = "Death";
                                    }

                                    mCommand.CommandText = "insert into facilitydischargestatus(code,description) values('"
                                    + mCode + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('ipddischargestatus')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region doctors

                            if (mDvMigrationLog.Find("doctors") < 0)
                            {
                                mTotalRows = pDtDoctors.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitystaffs where category="
                                + Program.DbValue_ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors);
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtDoctors.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitystaffs(code,description,category) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim()
                                    + "'," + Program.DbValue_ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors) + ")";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('doctors')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region staffs

                            if (mDvMigrationLog.Find("staffs") < 0)
                            {
                                mTotalRows = pDtStaffs.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitystaffs where category<>"
                                + Program.DbValue_ToInt16(AfyaPro_Types.clsEnums.StaffCategories.MedicalDoctors);
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtStaffs.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitystaffs(code,description,category) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim()
                                    + "'," + Program.DbValue_ToInt16(mDataRow["category"]) + ")";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('staffs')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region laboratories

                            if (mDvMigrationLog.Find("laboratories") < 0)
                            {
                                mTotalRows = pDtLaboratories.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitylaboratories";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtLaboratories.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitylaboratories(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('laboratories')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region labtestgroups

                            if (mDvMigrationLog.Find("labtestgroups") < 0)
                            {
                                mTotalRows = pDtLaboratoryTestGroups.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from labtestgroups";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtLaboratoryTestGroups.Rows[mRowCount];

                                    mCommand.CommandText = "insert into labtestgroups(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('labtestgroups')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region labtests

                            if (mDvMigrationLog.Find("labtests") < 0)
                            {
                                mTotalRows = pDtLaboratoryTests.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from labtests";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtLaboratoryTests.Rows[mRowCount];

                                    mCommand.CommandText = "insert into labtests(groupcode,code,description,resulttype,units) values('"
                                    + mDataRow["groupcode"].ToString().Trim() + "','" + mDataRow["code"].ToString().Trim() + "','"
                                    + mDataRow["description"].ToString().Trim() + "'," + Program.DbValue_ToInt16(mDataRow["resulttype"]) + ",'"
                                    + mDataRow["units"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('labtests')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region ipdwards

                            if (mDvMigrationLog.Find("ipdwards") < 0)
                            {
                                mTotalRows = pDtIPDWards.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitywards";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtIPDWards.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitywards(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('ipdwards')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region ipdwardrooms

                            if (mDvMigrationLog.Find("ipdwardrooms") < 0)
                            {
                                mTotalRows = pDtIPDWardRooms.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitywardrooms";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtIPDWardRooms.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitywardrooms(wardcode,code,description) values('"
                                    + mDataRow["wardcode"].ToString().Trim() + "','" + mDataRow["code"].ToString().Trim()
                                    + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('ipdwardrooms')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region treatmentpoints

                            if (mDvMigrationLog.Find("treatmentpoints") < 0)
                            {
                                mTotalRows = pDtTreatmentPoints.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitytreatmentpoints";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtTreatmentPoints.Rows[mRowCount];

                                    string mCode = mDataRow["autocode"].ToString().Trim().PadLeft(6, '0');

                                    mCommand.CommandText = "insert into facilitytreatmentpoints(code,description) values('"
                                    + mCode + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('treatmentpoints')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            DataTable mDtCountries = new DataTable("countries");
                            mCommand.CommandText = "select * from countries";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtCountries);

                            DataTable mDtRegions = new DataTable("regions");
                            mCommand.CommandText = "select * from regions";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtRegions);

                            DataTable mDtDistricts = new DataTable("districts");
                            mCommand.CommandText = "select * from districts";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtDistricts);

                            DataView mDvCountries = new DataView();
                            mDvCountries.Table = mDtCountries;
                            mDvCountries.Sort = "code";

                            DataView mDvRegions = new DataView();
                            mDvRegions.Table = mDtRegions;
                            mDvRegions.Sort = "code";

                            DataView mDvDistricts = new DataView();
                            mDvDistricts.Table = mDtDistricts;
                            mDvDistricts.Sort = "code";

                            DataView mDvEthnicities = new DataView();
                            mDvEthnicities.Table = pDtEthnicities;
                            mDvEthnicities.Sort = "code";

                            DataView mDvReligions = new DataView();
                            mDvReligions.Table = pDtReligions;
                            mDvReligions.Sort = "code";

                            #region facilityoptions and autocodes

                            if (mDvMigrationLog.Find("facilityoptions") < 0)
                            {
                                mTotalRows = pDtFacilityOptions.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilityoptions";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtFacilityOptions.Rows[mRowCount];

                                    #region facilityoptions

                                    string mCountryCode = mDataRow["countrycode"].ToString().Trim();
                                    string mRegionCode = mDataRow["regioncode"].ToString().Trim();
                                    string mDistrictCode = mDataRow["districtcode"].ToString().Trim();
                                    string mCountry = "";
                                    string mRegion = "";
                                    string mDistrict = "";

                                    int mRecordIndex = mDvCountries.Find(mCountryCode);
                                    if (mRecordIndex >= 0)
                                    {
                                        mCountry = mDvCountries[mRecordIndex]["description"].ToString().Trim();
                                    }

                                    mRecordIndex = mDvRegions.Find(mRegionCode);
                                    if (mRecordIndex >= 0)
                                    {
                                        mRegion = mDvRegions[mRecordIndex]["description"].ToString().Trim();
                                    }

                                    mRecordIndex = mDvDistricts.Find(mDistrictCode);
                                    if (mRecordIndex >= 0)
                                    {
                                        mDistrict = mDvDistricts[mRecordIndex]["description"].ToString().Trim();
                                    }

                                    mCommand.CommandText = "insert into facilityoptions(facilitycode,facilitydescription,"
                                    + "box,street,teleno,regioncode,regiondescription,districtcode,districtdescription,"
                                    + "countrycode,countrydescription,headname,headdesignation,affectstockatcashier) values('"
                                    + mDataRow["facilitycode"].ToString().Trim() + "','" + mDataRow["facilitydescription"].ToString().Trim()
                                    + "','" + mDataRow["box"].ToString().Trim() + "','" + mDataRow["street"].ToString().Trim()
                                    + "','" + mDataRow["teleno"].ToString().Trim() + "','" + mRegionCode + "','" + mRegion + "','"
                                    + mDistrictCode + "','" + mDistrict + "','" + mCountryCode + "','" + mCountry + "','"
                                    + mDataRow["headname"].ToString().Trim() + "','" + mDataRow["headdesignation"].ToString().Trim() + "',1)";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region facilityautocodes

                                    DataTable mDtFacilityAutoCodes = new DataTable("facilityautocodes");
                                    mCommand.CommandText = "select * from facilityautocodes where codekey="
                                    + Program.DbValue_ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno);
                                    mDataAdapter.SelectCommand = mCommand;
                                    mDataAdapter.Fill(mDtFacilityAutoCodes);

                                    if (mDtFacilityAutoCodes.Rows.Count == 0)
                                    {
                                        mCommand.CommandText = "insert into facilityautocodes(codekey) values("
                                        + Program.DbValue_ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno) + ")";
                                        mCommand.ExecuteNonQuery();
                                    }

                                    mCommand.CommandText = "update facilityautocodes"
                                    + " set autogenerate=" + Program.DbValue_ToInt16(mDataRow["autogenerateid"])
                                    + ",idseed=" + Program.DbValue_ToInt16(mDataRow["seed"])
                                    + ",idincrement=" + Program.DbValue_ToInt16(mDataRow["increment"])
                                    + ",idlength=" + Program.DbValue_ToInt16(mDataRow["length"])
                                    + ",idcurrent=" + Program.DbValue_ToInt16(mDataRow["current"])
                                    + ",preftype=" + Program.DbValue_ToInt16(mDataRow["preftype"])
                                    + ",preftext='" + mDataRow["preftext"].ToString().Trim() + "'"
                                    + ",prefsep='" + mDataRow["prefsep"].ToString().Trim() + "'"
                                    + ",surftype=" + Program.DbValue_ToInt16(mDataRow["surftype"])
                                    + ",surftext='" + mDataRow["surftext"].ToString().Trim() + "'"
                                    + ",surfsep='" + mDataRow["surfsep"].ToString().Trim() + "'"
                                    + ",position=" + Program.DbValue_ToInt16(mDataRow["position"])
                                    + " where codekey=" + Program.DbValue_ToInt16(AfyaPro_Types.clsEnums.SystemGeneratedCodes.patientno);
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('facilityoptions')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            string mExemptionGroupcode = "";

                            #region customergroups

                            if (mDvMigrationLog.Find("customergroups") < 0)
                            {
                                mTotalRows = pDtCustomerGroups.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitycorporates";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtCustomerGroups.Rows[mRowCount];

                                    int mHasSubGroups = 0;
                                    try
                                    {
                                        mHasSubGroups = Program.DbValue_ToInt16(mDataRow["exemptions"]);
                                    }
                                    catch { }

                                    if (mHasSubGroups == 1)
                                    {
                                        mExemptionGroupcode = mDataRow["code"].ToString().Trim();
                                    }

                                    mCommand.CommandText = "insert into facilitycorporates(code,description,hasid,hasceiling,"
                                    + "ceilingamount,pricecategory,hassubgroups) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "',"
                                    + Program.DbValue_ToInt16(mDataRow["hasid"]) + "," + Program.DbValue_ToInt16(mDataRow["hasceiling"]) + ","
                                    + Program.DbValue_ToDouble(mDataRow["ceilingamount"]) + ",'" + mDataRow["pricecategory"].ToString().Trim()
                                    + "'," + mHasSubGroups + ")";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('customergroups')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region customersubgroups

                            if (mDvMigrationLog.Find("customersubgroups") < 0)
                            {
                                mTotalRows = pDtCustomerSubGroups.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitycorporatesubgroups";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtCustomerSubGroups.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitycorporatesubgroups(groupcode,code,description) values('"
                                    + mExemptionGroupcode + "','" + mDataRow["code"].ToString().Trim()
                                    + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('customersubgroups')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region pricecategories

                            if (mDvMigrationLog.Find("pricecategories") < 0)
                            {
                                mTotalRows = pDtPriceCategories.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitypricecategories";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPriceCategories.Rows[mRowCount];

                                    Int16 mUsePrice1 = Program.DbValue_ToInt16(mDataRow["useprice1"]);
                                    Int16 mUsePrice2 = Program.DbValue_ToInt16(mDataRow["useprice2"]);
                                    Int16 mUsePrice3 = Program.DbValue_ToInt16(mDataRow["useprice3"]);
                                    Int16 mUsePrice4 = Program.DbValue_ToInt16(mDataRow["useprice4"]);
                                    Int16 mUsePrice5 = Program.DbValue_ToInt16(mDataRow["useprice5"]);
                                    Int16 mUsePrice6 = Program.DbValue_ToInt16(mDataRow["useprice6"]);
                                    Int16 mUsePrice7 = Program.DbValue_ToInt16(mDataRow["useprice7"]);
                                    Int16 mUsePrice8 = Program.DbValue_ToInt16(mDataRow["useprice8"]);
                                    Int16 mUsePrice9 = Program.DbValue_ToInt16(mDataRow["useprice9"]);
                                    Int16 mUsePrice10 = Program.DbValue_ToInt16(mDataRow["useprice10"]);
                                    string mPrice1 = mDataRow["price1"].ToString().Trim();
                                    string mPrice2 = mDataRow["price2"].ToString().Trim();
                                    string mPrice3 = mDataRow["price3"].ToString().Trim();
                                    string mPrice4 = mDataRow["price4"].ToString().Trim();
                                    string mPrice5 = mDataRow["price5"].ToString().Trim();
                                    string mPrice6 = mDataRow["price6"].ToString().Trim();
                                    string mPrice7 = mDataRow["price7"].ToString().Trim();
                                    string mPrice8 = mDataRow["price8"].ToString().Trim();
                                    string mPrice9 = mDataRow["price9"].ToString().Trim();
                                    string mPrice10 = mDataRow["price10"].ToString().Trim();

                                    mCommand.CommandText = "insert into facilitypricecategories(useprice1,useprice2,useprice3,useprice4,"
                                    + "useprice5,useprice6,useprice7,useprice8,useprice9,useprice10,price1,price2,price3,price4,price5,"
                                    + "price6,price7,price8,price9,price10) values(" + mUsePrice1 + "," + mUsePrice2 + "," + mUsePrice3
                                    + "," + mUsePrice4 + "," + mUsePrice5 + "," + mUsePrice6 + "," + mUsePrice7 + "," + mUsePrice8
                                    + "," + mUsePrice9 + "," + mUsePrice10 + ",'" + mPrice1 + "','" + mPrice2 + "','" + mPrice3 + "','"
                                    + mPrice4 + "','" + mPrice5 + "','" + mPrice6 + "','" + mPrice7 + "','" + mPrice8 + "','" + mPrice9
                                    + "','" + mPrice10 + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('pricecategories')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region paymenttypes

                            if (mDvMigrationLog.Find("paymenttypes") < 0)
                            {
                                mTotalRows = pDtPaymentTypes.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitypaymenttypes";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPaymentTypes.Rows[mRowCount];

                                    Int16 mPreventOverPay = 0;
                                    try
                                    {
                                        mPreventOverPay = Program.DbValue_ToInt16(mDataRow["preventoverpay"]);
                                    }
                                    catch { }

                                    Int16 mCheckDepositBalance = 0;
                                    try
                                    {
                                        mCheckDepositBalance = Program.DbValue_ToInt16(mDataRow["checkdepositbalance"]);
                                    }
                                    catch { }

                                    Int16 mIsCheque = 0;
                                    Int16 mAllowRefund = 1;
                                    try
                                    {
                                        if (Program.DbValue_ToInt16(mDataRow["requireextra"]) == 1)
                                        {
                                            mIsCheque = 1;
                                            mAllowRefund = 0;
                                        }
                                    }
                                    catch { }

                                    mCommand.CommandText = "insert into facilitypaymenttypes(code,description,preventoverpay,"
                                    + "checkdepositbalance,ischeque,allowrefund,visibilitysales,visibilitydebtorpayments) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "',"
                                    + mPreventOverPay + "," + mCheckDepositBalance + "," + mIsCheque + "," + mAllowRefund + ",1,1)";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('paymenttypes')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region surgerytypes

                            if (mDvMigrationLog.Find("surgerytypes") < 0)
                            {
                                mTotalRows = pDtSurgeryTypes.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitysurgerytypes";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtSurgeryTypes.Rows[mRowCount];

                                    Int16 mCategory = 0;
                                    try
                                    {
                                        mCategory = Program.DbValue_ToInt16(mDataRow["category"]);
                                    }
                                    catch { }

                                    mCommand.CommandText = "insert into facilitysurgerytypes(code,description,category) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "'," + mCategory + ")";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('surgerytypes')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region theatres

                            if (mDvMigrationLog.Find("theatres") < 0)
                            {
                                mTotalRows = pDtTheatres.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitytheatres";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtTheatres.Rows[mRowCount];

                                    mCommand.CommandText = "insert into facilitytheatres(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('theatres')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region streets

                            if (mDvMigrationLog.Find("streets") < 0)
                            {
                                mTotalRows = pDtStreets.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientextrafieldlookup where fieldname='street'";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtStreets.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description) "
                                    + "values('street','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('streets')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patientcommonsurnames

                            if (mDvMigrationLog.Find("patientcommonsurnames") < 0)
                            {
                                mTotalRows = pDtPatientSurnames.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientsurnames";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientSurnames.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientsurnames(description) values('"
                                    + mDataRow["surname"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('patientcommonsurnames')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patientcommonfirstnames

                            if (mDvMigrationLog.Find("patientcommonfirstnames") < 0)
                            {
                                mTotalRows = pDtPatientFirstNames.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientfirstnames";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientFirstNames.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientfirstnames(description) values('"
                                    + mDataRow["firstname"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('patientcommonfirstnames')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patientcommonothernames

                            if (mDvMigrationLog.Find("patientcommonothernames") < 0)
                            {
                                mTotalRows = pDtPatientOthernames.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientothernames";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientOthernames.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientothernames(description) values('"
                                    + mDataRow["othernames"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('patientcommonothernames')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region villages

                            if (mDvMigrationLog.Find("villages") < 0)
                            {
                                mTotalRows = pDtVillages.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientextrafieldlookup where fieldname='village'";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtVillages.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description) "
                                    + "values('village','" + mDataRow["village"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('villages')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region occupations

                            if (mDvMigrationLog.Find("occupations") < 0)
                            {
                                mTotalRows = pDtOccupations.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patientextrafieldlookup where fieldname='occupation'";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtOccupations.Rows[mRowCount];

                                    mCommand.CommandText = "insert into patientextrafieldlookup(fieldname,description) "
                                    + "values('occupation','" + mDataRow["occupation"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('occupations')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region diagnosesgroups

                            if (mDvMigrationLog.Find("diagnosesgroups") < 0)
                            {
                                mTotalRows = pDtDiagnosesGroups.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from dxtdiagnosesgroups";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtDiagnosesGroups.Rows[mRowCount];

                                    mCommand.CommandText = "insert into dxtdiagnosesgroups(code,description) values('"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('diagnosesgroups')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region diagnoses

                            if (mDvMigrationLog.Find("diagnoses") < 0)
                            {
                                mTotalRows = pDtDiagnoses.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from dxtdiagnoses";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtDiagnoses.Rows[mRowCount];

                                    string mdata = mDataRow["code"].ToString().Trim();
                                    mCommand.CommandText = "insert into dxtdiagnoses(groupcode,code,description) values('gen','"
                                    + mDataRow["code"].ToString().Trim() + "','" + mDataRow["description"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();

                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('diagnoses')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patients

                            if (mDvMigrationLog.Find("patients") < 0)
                            {
                                mTotalRows = pDtPatients.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from patients";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatients.Rows[mRowCount];

                                    string mEthnicity = "";
                                    string mReligion = "";

                                    int mIndex = mDvEthnicities.Find(mDataRow["ethnicitycode"].ToString().Trim());
                                    if (mIndex >= 0)
                                    {
                                        mEthnicity = mDvEthnicities[mIndex]["description"].ToString().Trim();
                                    }

                                    mIndex = mDvReligions.Find(mDataRow["religioncode"].ToString().Trim());
                                    if (mIndex >= 0)
                                    {
                                        mReligion = mDvReligions[mIndex]["description"].ToString().Trim();
                                    }

                                    try
                                    {
                                        mCommand.CommandText = "insert into patients(code,surname,firstname,othernames,gender,birthdate,"
                                        + "chronic,severe,operations,regdate,weight,temperature,ethnicity,religion,village,street,occupation,"
                                        + "nextofkin) values('" + mDataRow["code"].ToString().Trim() + "','" + mDataRow["surname"].ToString().Trim()
                                        + "','" + mDataRow["description"].ToString().Trim() + "','" + mDataRow["othernames"].ToString().Trim() + "','"
                                        + mDataRow["gender"].ToString().Trim() + "'," + Program.DateValueNullable(mDataRow["birthdate"]) + ",'"
                                        + mDataRow["chronic"].ToString().Trim() + "','" + mDataRow["severe"].ToString().Trim() + "','"
                                        + mDataRow["operations"].ToString().Trim() + "'," + Program.DateValueNullable(mDataRow["regdate"]) + ",0,0,'"
                                        + mEthnicity + "','" + mReligion + "','" + mDataRow["village"].ToString().Trim() + "','"
                                        + mDataRow["street"].ToString().Trim() + "','" + mDataRow["occupation"].ToString().Trim() + "','"
                                        + mDataRow["nextofkin"].ToString().Trim() + "')";
                                        mCommand.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                        string mCode = mDataRow["code"].ToString().Trim();
                                        Program.Display_Error(pClassName, mFunctionName, mDataRow["code"].ToString().Trim() + ", " + mDataRow["description"].ToString().Trim());
                                        return;
                                    }
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('patients')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region revisitingnumbers

                            if (mDvMigrationLog.Find("revisitingnumbers") < 0)
                            {
                                mTotalRows = pDtReAttendanceNumbers.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from revisitingnumbers";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtReAttendanceNumbers.Rows[mRowCount];

                                    mCommand.CommandText = "insert into revisitingnumbers(patientcode) values('"
                                        + mDataRow["patientcode"].ToString().Trim() + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('revisitingnumbers')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            DataTable mDtPatients = new DataTable("patients");
                            mCommand.CommandText = "select * from patients";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtPatients);

                            DataView mDvPatients = new DataView();
                            mDvPatients.Table = mDtPatients;
                            mDvPatients.Sort = "code";

                            #region customergroupmembers

                            if (mDvMigrationLog.Find("customergroupmembers") < 0)
                            {
                                mTotalRows = pDtCustomerGroupMembers.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitycorporatemembers";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtCustomerGroupMembers.Rows[mRowCount];

                                    string mSurname = "";
                                    string mFirstName = "";
                                    string mOtherNames = "";
                                    string mGender = "";
                                    string mBirthDate = "Null";

                                    int mIndex = mDvPatients.Find(mDataRow["patientcode"].ToString().Trim());
                                    if (mIndex >= 0)
                                    {
                                        mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                                        mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                                        mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                                        mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                                        mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                                    }

                                    mCommand.CommandText = "insert into facilitycorporatemembers(regdate,termdate,code,surname,"
                                    + "firstname,othernames,gender,birthdate,ceilingamount,billinggroupcode,billingsubgroupcode,"
                                    + "billinggroupmembershipno,inactive,membershipstatus) values("
                                    + Program.DateValueNullable(mDataRow["regdate"]) + "," + Program.DateValueNullable(mDataRow["termdate"])
                                    + ",'" + mDataRow["patientcode"].ToString().Trim() + "','" + mSurname + "','" + mFirstName + "','"
                                    + mOtherNames + "','" + mGender + "'," + mBirthDate + ",0,'"
                                    + mDataRow["billtypecode"].ToString().Trim() + "','" + mDataRow["exemptiongroupcode"].ToString().Trim() + "','"
                                    + mDataRow["billtypenumber"].ToString().Trim() + "',0," + mDataRow["membershipstatus"].ToString().Trim() + ")";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('customergroupmembers')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            DataTable mDtCustomerGroups = new DataTable("customergroups");
                            mCommand.CommandText = "select * from facilitycorporates";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtCustomerGroups);

                            DataView mDvCustomerGroups = new DataView();
                            mDvCustomerGroups.Table = mDtCustomerGroups;
                            mDvCustomerGroups.Sort = "code";

                            DataTable mDtCustomerSubGroups = new DataTable("customersubgroups");
                            mCommand.CommandText = "select * from facilitycorporatesubgroups";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtCustomerSubGroups);

                            DataView mDvCustomerSubGroups = new DataView();
                            mDvCustomerSubGroups.Table = mDtCustomerSubGroups;
                            mDvCustomerSubGroups.Sort = "code";

                            #region debtors

                            if (mDvMigrationLog.Find("debtors") < 0)
                            {
                                mTotalRows = pDtDebtorsIndividual.Rows.Count + pDtDebtorsGroup.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                #region empty tables

                                mCommand.CommandText = "delete from billdebtors";
                                mCommand.ExecuteNonQuery();

                                mCommand.CommandText = "delete from billdebtorslog";
                                mCommand.ExecuteNonQuery();

                                mCommand.CommandText = "delete from billinvoices";
                                mCommand.ExecuteNonQuery();

                                mCommand.CommandText = "delete from billinvoiceitems";
                                mCommand.ExecuteNonQuery();

                                mCommand.CommandText = "delete from billinvoiceslog";
                                mCommand.ExecuteNonQuery();

                                #endregion

                                int mInvoiceCounter = 1;

                                #region individual

                                for (int mRowCount = 0; mRowCount < pDtDebtorsIndividual.Rows.Count; mRowCount++)
                                {
                                    DataRow mDataRow = pDtDebtorsIndividual.Rows[mRowCount];

                                    DateTime mSysDate = DateTime.Now;
                                    DateTime mTransDate = DateTime.Now.Date;
                                    string mAccountCode = mDataRow["patientcode"].ToString().Trim();
                                    string mAccountDescription = "";
                                    string mDebtorType = "Individual";
                                    double mBalance = 0;
                                    try
                                    {
                                        mBalance = Program.DbValue_ToDouble(mDataRow["balance"]);
                                    }
                                    catch { }
                                    string mInvoiceNo = mInvoiceCounter.ToString().PadLeft(6, '0');
                                    int mYearPart = mTransDate.Year;
                                    int mMonthPart = mTransDate.Month;
                                    string mUserId = "admin";
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    string mSurname = "";
                                    string mFirstName = "";
                                    string mOtherNames = "";

                                    #region get patient details
                                    int mIndex = mDvPatients.Find(mPatientCode);
                                    if (mIndex >= 0)
                                    {
                                        mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                                        mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                                        mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();

                                        mAccountDescription = mFirstName;
                                        if (mOtherNames.Trim() != "")
                                        {
                                            mAccountDescription = mAccountDescription + " " + mOtherNames;
                                        }
                                        mAccountDescription = mAccountDescription + " " + mSurname;
                                    }
                                    #endregion

                                    #region billdebtors

                                    if (mDebtorType.Trim().ToLower() == "group")
                                    {
                                        DataTable mDtDebtors = new DataTable("debtors");
                                        mCommand.CommandText = "select * from billdebtors where accountcode='"
                                        + mAccountCode + "' and debtortype='Group'";
                                        mDataAdapter.SelectCommand = mCommand;
                                        mDataAdapter.Fill(mDtDebtors);

                                        if (mDtDebtors.Rows.Count > 0)
                                        {
                                            mCommand.CommandText = "update billdebtors set balance=balance+" + mBalance
                                            + " where accountcode='" + mAccountCode + "' and debtortype='Group'";
                                            mCommand.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            mCommand.CommandText = "insert into billdebtors(sysdate,transdate,accountcode,accountdescription,"
                                            + "debtortype,balance) values(" + Program.DateValueNullable(mSysDate) + ","
                                            + Program.DateValueNullable(mTransDate) + ",'" + mAccountCode + "','" + mAccountDescription
                                            + "','" + mDebtorType + "'," + mBalance + ")";
                                            mCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        mCommand.CommandText = "insert into billdebtors(sysdate,transdate,accountcode,accountdescription,"
                                        + "debtortype,balance) values(" + Program.DateValueNullable(mSysDate) + ","
                                        + Program.DateValueNullable(mTransDate) + ",'" + mAccountCode + "','" + mAccountDescription
                                        + "','" + mDebtorType + "'," + mBalance + ")";
                                        mCommand.ExecuteNonQuery();
                                    }

                                    #endregion

                                    #region billdebtorslog

                                    mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                                    + "accountdescription,fromwhomtowhomcode,fromwhomtowhom,debtortype,transtype,transdescription,"
                                    + "debitamount,yearpart,monthpart,userid) values(" + Program.DateValueNullable(mSysDate) + ","
                                    + Program.DateValueNullable(mTransDate) + ",'" + mInvoiceNo + "','" + mAccountCode + "','"
                                    + mAccountDescription + "','" + mAccountCode + "','" + mAccountDescription + "','" + mDebtorType
                                    + "',2,'Opening Balance'," + mBalance + "," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region billinvoices

                                    mCommand.CommandText = "insert into billinvoices(sysdate,transdate,invoiceno,patientcode,"
                                    + "currencycode,currencydescription,currencysymbol,totaldue,balancedue,status,yearpart,"
                                    + "monthpart,userid) values(" + Program.DateValueNullable(mSysDate) + ","
                                    + Program.DateValueNullable(mTransDate) + ",'" + mInvoiceNo + "','" + mPatientCode
                                    + "','LocalCurr','Tsh','Tsh'," + mBalance + "," + mBalance + ",1," + mYearPart + "," 
                                    + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region billinvoiceitems

                                    mCommand.CommandText = "insert into billinvoiceitems(sysdate,transdate,invoiceno,reference,patientcode,"
                                    + "itemcode,itemdescription,qty,actualamount,amount,transtype,yearpart,monthpart,userid) values(" 
                                    + Program.DateValueNullable(mSysDate) + "," + Program.DateValueNullable(mTransDate) + ",'" + mInvoiceNo 
                                    + "','" + mInvoiceNo + "','" + mPatientCode + "','OB','Opening Balance',1," + mBalance + "," + mBalance 
                                    + ",1," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region billinvoiceslog

                                    mCommand.CommandText = "insert into billinvoiceslog(sysdate,transdate,invoiceno,reference,"
                                    + "patientcode,transtype,transdescription,debitamount,yearpart,monthpart,userid) values(" 
                                    + Program.DateValueNullable(mSysDate) + "," + Program.DateValueNullable(mTransDate) + ",'"
                                    + mInvoiceNo + "','" + mInvoiceNo + "','" + mPatientCode + "',1,'Opening Balance'," + mBalance + "," 
                                    + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    pBackgroundWorkder.ReportProgress(((mInvoiceCounter) * 100) / mTotalRows);

                                    mInvoiceCounter++;
                                }

                                #endregion

                                #region group

                                for (int mRowCount = 0; mRowCount < pDtDebtorsGroup.Rows.Count; mRowCount++)
                                {
                                    DataRow mDataRow = pDtDebtorsGroup.Rows[mRowCount];

                                    DateTime mSysDate = DateTime.Now;
                                    DateTime mTransDate = DateTime.Now.Date;
                                    string mAccountCode = mDataRow["corporatecode"].ToString().Trim();
                                    string mAccountDescription = "";
                                    string mDebtorType = "Group";
                                    double mBalance = 0;
                                    try
                                    {
                                        mBalance = Program.DbValue_ToDouble(mDataRow["balance"]);
                                    }
                                    catch { }
                                    string mInvoiceNo = mInvoiceCounter.ToString().PadLeft(6, '0');
                                    int mYearPart = mTransDate.Year;
                                    int mMonthPart = mTransDate.Month;
                                    string mUserId = "admin";

                                    #region get corporate details
                                    int mIndex = mDvCustomerGroups.Find(mAccountCode);
                                    if (mIndex >= 0)
                                    {
                                        mAccountDescription = mDvCustomerGroups[mIndex]["description"].ToString().Trim();
                                    }
                                    #endregion

                                    #region billdebtors

                                    if (mDebtorType.Trim().ToLower() == "group")
                                    {
                                        DataTable mDtDebtors = new DataTable("debtors");
                                        mCommand.CommandText = "select * from billdebtors where accountcode='"
                                        + mAccountCode + "' and debtortype='Group'";
                                        mDataAdapter.SelectCommand = mCommand;
                                        mDataAdapter.Fill(mDtDebtors);

                                        if (mDtDebtors.Rows.Count > 0)
                                        {
                                            mCommand.CommandText = "update billdebtors set balance=balance+" + mBalance
                                            + " where accountcode='" + mAccountCode + "' and debtortype='Group'";
                                            mCommand.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            mCommand.CommandText = "insert into billdebtors(sysdate,transdate,accountcode,accountdescription,"
                                            + "debtortype,balance) values(" + Program.DateValueNullable(mSysDate) + ","
                                            + Program.DateValueNullable(mTransDate) + ",'" + mAccountCode + "','" + mAccountDescription
                                            + "','" + mDebtorType + "'," + mBalance + ")";
                                            mCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        mCommand.CommandText = "insert into billdebtors(sysdate,transdate,accountcode,accountdescription,"
                                        + "debtortype,balance) values(" + Program.DateValueNullable(mSysDate) + ","
                                        + Program.DateValueNullable(mTransDate) + ",'" + mAccountCode + "','" + mAccountDescription
                                        + "','" + mDebtorType + "'," + mBalance + ")";
                                        mCommand.ExecuteNonQuery();
                                    }

                                    #endregion

                                    #region billdebtorslog

                                    mCommand.CommandText = "insert into billdebtorslog(sysdate,transdate,reference,accountcode,"
                                    + "accountdescription,fromwhomtowhomcode,fromwhomtowhom,debtortype,transtype,transdescription,"
                                    + "debitamount,yearpart,monthpart,userid) values(" + Program.DateValueNullable(mSysDate) + ","
                                    + Program.DateValueNullable(mTransDate) + ",'" + mInvoiceNo + "','" + mAccountCode + "','"
                                    + mAccountDescription + "','" + mAccountCode + "','" + mAccountDescription + "','" + mDebtorType
                                    + "',2,'Opening Balance'," + mBalance + "," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region billinvoices

                                    mCommand.CommandText = "insert into billinvoices(sysdate,transdate,invoiceno,billinggroupcode,"
                                    + "billinggroupdescription,currencycode,currencydescription,currencysymbol,totaldue,balancedue,"
                                    + "status,yearpart,monthpart,userid) values(" + Program.DateValueNullable(mSysDate) + ","
                                    + Program.DateValueNullable(mTransDate) + ",'" + mInvoiceNo + "','" + mAccountCode + "','"
                                    + mAccountDescription + "','LocalCurr','Tsh','Tsh'," + mBalance + "," + mBalance + ",1," 
                                    + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region billinvoiceitems

                                    mCommand.CommandText = "insert into billinvoiceitems(sysdate,transdate,invoiceno,reference,billinggroupcode,"
                                    + "billinggroupdescription,itemcode,itemdescription,qty,actualamount,amount,transtype,yearpart,monthpart,"
                                    + "userid) values(" + Program.DateValueNullable(mSysDate) + "," + Program.DateValueNullable(mTransDate) + ",'" 
                                    + mInvoiceNo + "','" + mInvoiceNo + "','" + mAccountCode + "','" + mAccountDescription 
                                    + "','OB','Opening Balance',1," + mBalance + "," + mBalance + ",1," + mYearPart + "," + mMonthPart + ",'" 
                                    + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    #region billinvoiceslog

                                    mCommand.CommandText = "insert into billinvoiceslog(sysdate,transdate,invoiceno,reference,"
                                    + "billinggroupcode,billinggroupdescription,transtype,transdescription,debitamount,yearpart,"
                                    + "monthpart,userid) values(" + Program.DateValueNullable(mSysDate) + "," + Program.DateValueNullable(mTransDate)
                                    + ",'" + mInvoiceNo + "','" + mInvoiceNo + "','" + mAccountCode + "','" + mAccountDescription 
                                    + "',1,'Opening Balance'," + mBalance + "," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    #endregion

                                    pBackgroundWorkder.ReportProgress(((mInvoiceCounter) * 100) / mTotalRows);

                                    mInvoiceCounter++;
                                }

                                #endregion

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('debtors')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            DataTable mDtTreatmentPoints = new DataTable("treatmentpoints");
                            mCommand.CommandText = "select * from facilitytreatmentpoints";
                            mDataAdapter.SelectCommand = mCommand;
                            mDataAdapter.Fill(mDtTreatmentPoints);

                            DataView mDvTreatmentPoints = new DataView();
                            mDvTreatmentPoints.Table = mDtTreatmentPoints;
                            mDvTreatmentPoints.Sort = "description";

                            #region patients recent visits

                            if (mDvMigrationLog.Find("bookings") < 0)
                            {
                                mTotalRows = pDtPatientRecentVisits.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitybookings";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientRecentVisits.Rows[mRowCount];

                                    int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);
                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    int mRefered = Program.DbValue_ToInt16(mDataRow["refered"]);
                                    string mReferedFacility = mDataRow["referedfacility"].ToString().Trim();
                                    string mDepartment = "OPD";
                                    string mWhereTakenCode = "";
                                    string mWhereTaken = mDataRow["wheretaken"].ToString().Trim();
                                    string mBillingGroupCode = mDataRow["billtypecode"].ToString().Trim();
                                    string mBillingSubGroupCode = mDataRow["exemptiongroupcode"].ToString().Trim();
                                    string mBillingGroupMembershipNo = mDataRow["billtypenumber"].ToString().Trim();
                                    string mIpdStart = Program.DateValueNullable(mDataRow["ipdstart"]);
                                    string mIpdStop = Program.DateValueNullable(mDataRow["ipdstop"]);
                                    int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();

                                    if (Program.DbValue_ToInt16(mDataRow["department"]) == 1)
                                    {
                                        mDepartment = "IPD";
                                    }

                                    int mIndex = mDvTreatmentPoints.Find(mWhereTaken);
                                    if (mIndex >= 0)
                                    {
                                        mWhereTakenCode = mDvTreatmentPoints[mIndex]["code"].ToString().Trim();
                                    }

                                    mCommand.CommandText = "insert into facilitybookings(autocode,sysdate,bookdate,patientcode,refered,referedfacility,"
                                    + "department,wheretakencode,wheretaken,billinggroupcode,billingsubgroupcode,billinggroupmembershipno,"
                                    + "ipdstart,ipdstop,yearpart,monthpart,userid) values(" + mAutoCode + "," + mSysDate + "," + mTransDate + ",'" 
                                    + mPatientCode + "'," + mRefered + ",'" + mReferedFacility + "','" + mDepartment
                                    + "','" + mWhereTakenCode + "','" + mWhereTaken + "','" + mBillingGroupCode + "','"
                                    + mBillingSubGroupCode + "','" + mBillingGroupMembershipNo + "'," + mIpdStart + "," + mIpdStop + ","
                                    + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('bookings')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patients archive visits

                            if (mDvMigrationLog.Find("bookinglog") < 0)
                            {
                                mTotalRows = pDtPatientArchiveVisits.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from facilitybookinglog";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientArchiveVisits.Rows[mRowCount];

                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                                    string mBooking = mDataRow["booking"].ToString().Trim();
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    int mRefered = Program.DbValue_ToInt16(mDataRow["refered"]);
                                    string mReferedFacility = mDataRow["referedfacility"].ToString().Trim();
                                    string mDepartment = "OPD";
                                    string mWhereTakenCode = "";
                                    string mWhereTaken = mDataRow["wheretaken"].ToString().Trim();
                                    string mBillingGroupCode = mDataRow["billtypecode"].ToString().Trim();
                                    string mBillingGroup = "";
                                    string mBillingSubGroupCode = mDataRow["exemptiongroupcode"].ToString().Trim();
                                    string mBillingSubGroup = "";
                                    string mBillingGroupMembershipNo = mDataRow["billtypenumber"].ToString().Trim();
                                    string mIpdStart = Program.DateValueNullable(mDataRow["ipdstart"]);
                                    string mIpdStop = Program.DateValueNullable(mDataRow["ipdstop"]);
                                    int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();
                                    string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                                    string mIpdDischargeStatus = mDataRow["ipddischargestatus"].ToString().Trim();

                                    if (Program.DbValue_ToInt16(mDataRow["department"]) == 1)
                                    {
                                        mDepartment = "IPD";
                                    }

                                    if (Program.DbValue_ToInt16(mDataRow["registrystatus"]) == 1)
                                    {
                                        mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString();
                                    }

                                    int mIndex = mDvTreatmentPoints.Find(mWhereTaken);
                                    if (mIndex >= 0)
                                    {
                                        mWhereTakenCode = mDvTreatmentPoints[mIndex]["code"].ToString().Trim();
                                    }

                                    mIndex = mDvCustomerGroups.Find(mBillingGroupCode);
                                    if (mIndex >= 0)
                                    {
                                        mBillingGroup = mDvCustomerGroups[mIndex]["description"].ToString().Trim();
                                    }

                                    mIndex = mDvCustomerSubGroups.Find(mBillingSubGroupCode);
                                    if (mIndex >= 0)
                                    {
                                        mBillingSubGroup = mDvCustomerSubGroups[mIndex]["description"].ToString().Trim();
                                    }

                                    mCommand.CommandText = "insert into facilitybookinglog(sysdate,bookdate,booking,patientcode,refered,referedfacility,"
                                    + "department,wheretakencode,wheretaken,billinggroupcode,billinggroup,billingsubgroupcode,billingsubgroup,"
                                    + "billinggroupmembershipno,ipdstart,ipdstop,ipddischargestatus,registrystatus,yearpart,monthpart,userid) values(" 
                                    + mSysDate + "," + mTransDate + ",'" + mBooking + "','" + mPatientCode + "'," + mRefered + ",'" 
                                    + mReferedFacility + "','" + mDepartment + "','" + mWhereTakenCode + "','" + mWhereTaken + "','"
                                    + mBillingGroupCode + "','" + mBillingGroup + "','" + mBillingSubGroupCode + "','" + mBillingSubGroup + "','" 
                                    + mBillingGroupMembershipNo + "'," + mIpdStart + "," + mIpdStop + ",'" + mIpdDischargeStatus + "','"
                                    + mRegistryStatus + "'," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('bookinglog')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region ipd admissions

                            if (mDvMigrationLog.Find("admissions") < 0)
                            {
                                mTotalRows = pDtPatientIPDAdmissions.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from ipdadmissionslog";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientIPDAdmissions.Rows[mRowCount];

                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["admissionstart"]);
                                    string mBooking = mDataRow["booking"].ToString().Trim();
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    string mWardCode = mDataRow["wardcode"].ToString().Trim();
                                    string mWardDescription = mDataRow["warddescription"].ToString().Trim();
                                    string mRoomCode = mDataRow["roomcode"].ToString().Trim();
                                    string mRoomDescription = mDataRow["roomdescription"].ToString().Trim();
                                    string mBed = "";
                                    string mPatientCondition = mDataRow["patientcondition"].ToString().Trim();
                                    int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();
                                    string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                                    string mTransCode = AfyaPro_Types.clsEnums.IPDTransTypes.Admission.ToString();
                                    if (Program.DbValue_ToInt16(mDataRow["transcode"]) == 2)
                                    {
                                        mTransCode = AfyaPro_Types.clsEnums.IPDTransTypes.Transfer.ToString();
                                    }

                                    mCommand.CommandText = "insert into ipdadmissionslog(sysdate,transdate,booking,patientcode,transcode,wardcode,warddescription,"
                                    + "roomcode,roomdescription,bedcode,patientcondition,registrystatus,yearpart,monthpart,userid) values("
                                    + mSysDate + "," + mTransDate + ",'" + mBooking + "','" + mPatientCode + "','" + mTransCode + "','"
                                    + mWardCode + "','" + mWardDescription + "','" + mRoomCode + "','" + mRoomDescription + "','" + mBed + "','" 
                                    + mPatientCondition + "','" + mRegistryStatus + "'," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('admissions')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region ipd transfers

                            if (mDvMigrationLog.Find("transfers") < 0)
                            {
                                mTotalRows = pDtPatientIPDTransfers.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from ipdtransferslog";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientIPDTransfers.Rows[mRowCount];

                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["transferdate"]);
                                    string mBooking = mDataRow["booking"].ToString().Trim();
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    string mWardFromCode = mDataRow["wardfromcode"].ToString().Trim();
                                    string mWardFromDescription = mDataRow["wardfromdescription"].ToString().Trim();
                                    string mRoomFromCode = mDataRow["roomfromcode"].ToString().Trim();
                                    string mRoomFromDescription = mDataRow["roomfromdescription"].ToString().Trim();
                                    string mBedFrom = "";
                                    string mWardToCode = mDataRow["wardtocode"].ToString().Trim();
                                    string mWardToDescription = mDataRow["wardtodescription"].ToString().Trim();
                                    string mRoomToCode = mDataRow["roomtocode"].ToString().Trim();
                                    string mRoomToDescription = mDataRow["roomtodescription"].ToString().Trim();
                                    string mBedTo = "";
                                    string mPatientCondition = mDataRow["patientcondition"].ToString().Trim();
                                    int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();
                                    string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();

                                    mCommand.CommandText = "insert into ipdtransferslog(sysdate,transferdate,booking,patientcode,wardfromcode,wardfromdescription,"
                                    + "roomfromcode,roomfromdescription,bedfromcode,wardtocode,wardtodescription,roomtocode,roomtodescription,bedtocode,"
                                    + "patientcondition,registrystatus,yearpart,monthpart,userid) values(" + mSysDate + "," + mTransDate + ",'" 
                                    + mBooking + "','" + mPatientCode + "','" + mWardFromCode + "','" + mWardFromDescription 
                                    + "','" + mRoomFromCode + "','" + mRoomFromDescription + "','" + mBedFrom + "','" + mWardToCode + "','"
                                    + mWardToDescription + "','" + mRoomToCode + "','" + mRoomToDescription + "','" + mBedTo + "','"
                                    + mPatientCondition + "','" + mRegistryStatus + "'," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('transfers')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            DataView mDvDischargeStatus = new DataView();
                            mDvDischargeStatus.Table = pDtIPDDischargeStatus;
                            mDvDischargeStatus.Sort = "code";

                            #region ipd discharges

                            if (mDvMigrationLog.Find("discharges") < 0)
                            {
                                mTotalRows = pDtPatientIPDDischarges.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from ipddischargeslog";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientIPDDischarges.Rows[mRowCount];

                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["admissionstop"]);
                                    string mBooking = mDataRow["booking"].ToString().Trim();
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    string mWardCode = mDataRow["wardcode"].ToString().Trim();
                                    string mWardDescription = mDataRow["warddescription"].ToString().Trim();
                                    string mRoomCode = mDataRow["roomcode"].ToString().Trim();
                                    string mRoomDescription = mDataRow["roomdescription"].ToString().Trim();
                                    string mBed = "";
                                    string mPatientCondition = mDataRow["patientcondition"].ToString().Trim();
                                    int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();
                                    string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                                    string mTransCode = AfyaPro_Types.clsEnums.IPDTransTypes.Discharge.ToString();
                                    string mDischargeStatusCode = mDataRow["dischargestatuscode"].ToString().Trim();
                                    string mDischargeStatusDescription = mDataRow["dischargestatusdescription"].ToString().Trim();
                                    string mDischargeRemarks = mDataRow["dischargeremarks"].ToString().Trim();

                                    int mIndex = mDvDischargeStatus.Find(mDischargeStatusCode);
                                    if (mIndex >= 0)
                                    {
                                        mDischargeStatusCode = AfyaPro_Types.clsEnums.IPDDischargeStatus.Death.ToString();
                                    }

                                    mCommand.CommandText = "insert into ipddischargeslog(sysdate,transdate,booking,patientcode,transcode,wardcode,warddescription,"
                                    + "roomcode,roomdescription,bedcode,patientcondition,dischargestatuscode,dischargestatusdescription,dischargeremarks,"
                                    + "registrystatus,yearpart,monthpart,userid) values(" + mSysDate + "," + mTransDate + ",'" + mBooking + "','" 
                                    + mPatientCode + "','" + mTransCode + "','" + mWardCode + "','" + mWardDescription + "','" + mRoomCode 
                                    + "','" + mRoomDescription + "','" + mBed + "','" + mPatientCondition + "','" + mDischargeStatusCode + "','"
                                    + mDischargeStatusDescription + "','" + mDischargeRemarks + "','" + mRegistryStatus + "'," + mYearPart 
                                    + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('discharges')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patient diagnoses and treatments

                            if (mDvMigrationLog.Find("dxtpatientdiagnoseslog") < 0)
                            {
                                mTotalRows = pDtPatientDiagnoses.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from dxtpatientdiagnoseslog";
                                mCommand.ExecuteNonQuery();

                                int mIncidenceCount = 1;
                                int mReferalNoCount = 1;
                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientDiagnoses.Rows[mRowCount];

                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["diagnosisdate"]);
                                    string mBooking = mDataRow["booking"].ToString().Trim();
                                    string mIncidenceKey = mIncidenceCount.ToString().PadLeft(6, '0');
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    string mDiagnosisCode = mDataRow["diagnosiscode"].ToString().Trim();
                                    string mDiagnosisDescription = mDataRow["diagnosisdescription"].ToString().Trim();
                                    int mFollowUp = 0;
                                    string mDoctorCode = mDataRow["doctorcode"].ToString().Trim();
                                    string mDoctorDescription = mDataRow["doctordescription"].ToString().Trim();
                                    string mHistory = mDataRow["history"].ToString().Trim();
                                    string mExamination = mDataRow["examination"].ToString().Trim();
                                    string mInvestigation = mDataRow["investigation"].ToString().Trim();
                                    string mTreatments = mDataRow["treatmentdescription"].ToString().Trim();
                                    int mDeathStatus = Program.DbValue_ToInt16(mDataRow["dead"]);
                                    string mReferalNo = "";
                                    string mReferalDescription = mDataRow["referedto"].ToString().Trim();
                                    string mDepartment = mDataRow["category"].ToString().Trim();
                                    int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();

                                    if (mDataRow["referedto"].ToString().Trim() != "")
                                    {
                                        mReferalNo = mReferalNoCount.ToString().PadLeft(6, '0');
                                        mReferalNoCount++;
                                    }

                                    mCommand.CommandText = "insert into dxtpatientdiagnoseslog(sysdate,transdate,booking,patientcode,incidencekey,followup,doctorcode,"
                                    + "doctordescription,diagnosiscode,diagnosisdescription,history,examination,investigation,treatments,deathstatus,"
                                    + "referalno,referaldescription,department,yearpart,monthpart,userid) values(" + mSysDate + "," + mTransDate + ",'" 
                                    + mBooking + "','" + mPatientCode + "','" + mIncidenceKey + "'," + mFollowUp + ",'" + mDoctorCode + "','" 
                                    + mDoctorDescription + "','" + mDiagnosisCode + "','" + mDiagnosisDescription + "','" + mHistory + "','" 
                                    + mExamination + "','" + mInvestigation + "','" + mTreatments + "'," + mDeathStatus + ",'" + mReferalNo + "','" 
                                    + mReferalDescription + "','" + mDepartment + "'," + mYearPart + "," + mMonthPart + ",'" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    mCommand.CommandText = "insert into dxtpatientdiagnosesincidences(sysdate,transdate,incidencekey,patientcode,"
                                    + "doctorcode,diagnosiscode,userid) values(" + mSysDate + "," + mTransDate + ",'" + mIncidenceKey + "','"
                                    + mPatientCode + "','" + mDoctorCode + "','" + mDiagnosisCode + "','" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();
                                    
                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);

                                    mIncidenceCount++;
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('dxtpatientdiagnoseslog')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region patient lab test

                            if (mDvMigrationLog.Find("labpatienttests") < 0)
                            {
                                mTotalRows = pDtPatientLabTests.Rows.Count;

                                //initialize transaction
                                mTrans = mConn.BeginTransaction();
                                mCommand.Transaction = mTrans;

                                mCommand.CommandText = "delete from labpatienttests";
                                mCommand.ExecuteNonQuery();

                                for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                                {
                                    DataRow mDataRow = pDtPatientLabTests.Rows[mRowCount];

                                    string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                                    string mTransDate = Program.DateValueNullable(mDataRow["laboratorytestdate"]);
                                    string mBooking = mDataRow["booking"].ToString().Trim();
                                    string mPatientCode = mDataRow["patientcode"].ToString().Trim();
                                    string mLabCode = mDataRow["laboratorycode"].ToString().Trim();
                                    //string mLabDescription = mDataRow["laboratorydescription"].ToString().Trim();
                                    string mDoctorCode = mDataRow["doctorcode"].ToString().Trim();
                                    //string mDoctorDescription = mDataRow["doctordescription"].ToString().Trim();
                                    string mLabTechnicianCode = mDataRow["labtechniciancode"].ToString().Trim();
                                    //string mLabTechnicianDescription = mDataRow["labtechniciandescription"].ToString().Trim();
                                    string mLabTestGroupCode = mDataRow["labtestgroupcode"].ToString().Trim();
                                    //string mLabTestGroupDescription = mDataRow["labtestgroupdescription"].ToString().Trim();
                                    string mLabTestTypeCode = mDataRow["labtesttypecode"].ToString().Trim();
                                    //string mLabTestTypeDescription = mDataRow["labtesttypedescription"].ToString().Trim();
                                    string mResults = mDataRow["results"].ToString().Trim();
                                    string mRemarks = mDataRow["remarks"].ToString().Trim();
                                    string mDepartment = mDataRow["category"].ToString().Trim();
                                    //int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                                    //int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                                    string mUserId = mDataRow["userid"].ToString().Trim();

                                    mCommand.CommandText = "insert into labpatienttests(sysdate,transdate,booking,patientcode,laboratorycode,"
                                    + "doctorcode,labtechniciancode,labtestgroupcode,labtesttypecode,results,remarks,department,userid) values(" 
                                    + mSysDate + "," + mTransDate + ",'" + mBooking + "','" + mPatientCode + "','" + mLabCode + "','" 
                                    + mDoctorCode + "','" + mLabTechnicianCode + "','" + mLabTestGroupCode + "','" 
                                    + mLabTestTypeCode + "','" + mResults + "','" + mRemarks + "','"
                                    + mDepartment + "','" + mUserId + "')";
                                    mCommand.ExecuteNonQuery();

                                    pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                                }

                                mCommand.CommandText = "insert into sys_migrationlog(tablename) values('labpatienttests')";
                                mCommand.ExecuteNonQuery();

                                //commit changes to database
                                mTrans.Commit();
                            }

                            this.MarkDone(mRowIndex);
                            mRowIndex++;

                            #endregion

                            #region rch remarked

                            //#region rch_patients

                            //if (mDvMigrationLog.Find("rch_patients") < 0)
                            //{
                            //    mTotalRows = pDtRchClients.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_patients";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtRchClients.Rows[mRowCount];

                            //        string mEthnicity = "";
                            //        string mReligion = "";

                            //        int mIndex = mDvEthnicities.Find(mDataRow["ethnicitycode"].ToString().Trim());
                            //        if (mIndex >= 0)
                            //        {
                            //            mEthnicity = mDvEthnicities[mIndex]["description"].ToString().Trim();
                            //        }

                            //        mIndex = mDvReligions.Find(mDataRow["religioncode"].ToString().Trim());
                            //        if (mIndex >= 0)
                            //        {
                            //            mReligion = mDvReligions[mIndex]["description"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into patients(code,surname,firstname,othernames,gender,birthdate,"
                            //        + "regdate,ethnicity,religion,village,street,occupation,nextofkin) values('R"
                            //        + mDataRow["code"].ToString().Trim() + "','" + mDataRow["surname"].ToString().Trim()
                            //        + "','" + mDataRow["description"].ToString().Trim() + "','" + mDataRow["othernames"].ToString().Trim() + "','"
                            //        + mDataRow["gender"].ToString().Trim() + "'," + Program.DateValueNullable(mDataRow["birthdate"]) + ","
                            //        + Program.DateValueNullable(mDataRow["regdate"]) + ",'"
                            //        + mEthnicity + "','" + mReligion + "','" + mDataRow["village"].ToString().Trim() + "','"
                            //        + mDataRow["street"].ToString().Trim() + "','" + mDataRow["occupation"].ToString().Trim() + "','"
                            //        + mDataRow["nextofkin"].ToString().Trim() + "')";
                            //        mCommand.ExecuteNonQuery();
                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_patients')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //DataTable mDtRchPatients = new DataTable("rch_patients");
                            //mCommand.CommandText = "select * from rch_patients";
                            //mDataAdapter.SelectCommand = mCommand;
                            //mDataAdapter.Fill(mDtRchPatients);

                            //DataView mDvRchPatients = new DataView();
                            //mDvRchPatients.Table = mDtRchPatients;
                            //mDvRchPatients.Sort = "code";

                            //#region rch_antenatalattendances

                            //if (mDvMigrationLog.Find("rch_antenatalattendances") < 0)
                            //{
                            //    mTotalRows = pDtAntenatalAttendances.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_antenatalattendances";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtAntenatalAttendances.Rows[mRowCount];

                            //        int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        Int16 mCardPresented = Program.DbValue_ToInt16(mDataRow["cardpresented"]);
                            //        Int16 mPregAge = Program.DbValue_ToInt16(mDataRow["pregage"]);
                            //        Int16 mNoOfPreg = Program.DbValue_ToInt16(mDataRow["noofpreg"]);
                            //        double mHeight = Program.DbValue_ToDouble(mDataRow["height"]);
                            //        Int16 mDiscount = Program.DbValue_ToInt16(mDataRow["pregage"]);
                            //        Int16 mSyphilisTest = Program.DbValue_ToInt16(mDataRow["syphilistest"]);
                            //        string mTetanusVaccineDate = Program.DateValueNullable(mDataRow["tetanusvaccinedate"]);
                            //        Int16 mLastBirthYear = Program.DbValue_ToInt16(mDataRow["lastbirthyear"]);
                            //        Int16 mLastBornDeath = Program.DbValue_ToInt16(mDataRow["lastborndeath"]);
                            //        string mSp1 = Program.DateValueNullable(mDataRow["sp1"]);
                            //        string mSp2 = Program.DateValueNullable(mDataRow["sp2"]);
                            //        string mReferedTo = mDataRow["referedto"].ToString().Trim();
                            //        Int16 mPmtct = Program.DbValue_ToInt16(mDataRow["pmtcttest"]);
                            //        Int16 mAbortion = Program.DbValue_ToInt16(mDataRow["abortiveoutcomes"]);
                            //        Int16 mCaesarianSection = Program.DbValue_ToInt16(mDataRow["caesariansection"]);
                            //        Int16 mAnaemia = Program.DbValue_ToInt16(mDataRow["anaemia"]);
                            //        Int16 mOedema = Program.DbValue_ToInt16(mDataRow["oedema"]);
                            //        Int16 mProtenuria = Program.DbValue_ToInt16(mDataRow["protenuria"]);
                            //        Int16 mHighBloodPressure = Program.DbValue_ToInt16(mDataRow["hbp"]);
                            //        Int16 mRetardedWeight = Program.DbValue_ToInt16(mDataRow["retardedweight"]);
                            //        Int16 mBleeding = Program.DbValue_ToInt16(mDataRow["blooddischarge"]);
                            //        Int16 mBadBabyPosition = Program.DbValue_ToInt16(mDataRow["badbabyposition"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();

                            //        int mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_antenatalattendances(autocode,bookdate,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,cardpresented,pregage,noofpreg,"
                            //        + "syphilistest,tetanusvaccinedate,lastbirthyear,lastborndeath,sp1,sp2,referedto,pmtcttest,abortion,"
                            //        + "caesariansection,anaemia,oedema,protenuria,highbloodpressure,retardedweight,bleeding,badbabyposition,"
                            //        + "userid) values(" + mAutoCode + "," + mTransDate + ",'" + mPatientCode + "','" + mSurname + "','" + mFirstName
                            //        + "','" + mOtherNames + "','" + mGender + "'," + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" 
                            //        + mStreet + "','" + mEthnicity + "','" + mReligion + "','" + mOccupation + "','" + mNextOfKin + "'," 
                            //        + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," + mSyphilisTest + "," + mTetanusVaccineDate + ","
                            //        + mLastBirthYear + "," + mLastBornDeath + "," + mSp1 + "," + mSp2 + ",'" + mReferedTo + "',"
                            //        + mPmtct + "," + mAbortion + "," + mCaesarianSection + "," + mAnaemia + "," + mOedema + "," + mProtenuria
                            //        + "," + mHighBloodPressure + "," + mRetardedWeight + "," + mBleeding + "," + mBadBabyPosition + ",'" 
                            //        + mUserId + "')";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_antenatalattendances')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_antenatalattendancelog

                            //if (mDvMigrationLog.Find("rch_antenatalattendancelog") < 0)
                            //{
                            //    mTotalRows = pDtAntenatalAttendanceLog.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_antenatalattendancelog";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtAntenatalAttendanceLog.Rows[mRowCount];

                            //        string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                            //        string mBooking = mDataRow["booking"].ToString().Trim();
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        Int16 mCardPresented = Program.DbValue_ToInt16(mDataRow["cardpresented"]);
                            //        Int16 mPregAge = Program.DbValue_ToInt16(mDataRow["pregage"]);
                            //        Int16 mNoOfPreg = Program.DbValue_ToInt16(mDataRow["noofpreg"]);
                            //        double mHeight = Program.DbValue_ToDouble(mDataRow["height"]);
                            //        Int16 mDiscount = Program.DbValue_ToInt16(mDataRow["pregage"]);
                            //        Int16 mSyphilisTest = Program.DbValue_ToInt16(mDataRow["syphilistest"]);
                            //        string mTetanusVaccineDate = Program.DateValueNullable(mDataRow["tetanusvaccinedate"]);
                            //        Int16 mLastBirthYear = Program.DbValue_ToInt16(mDataRow["lastbirthyear"]);
                            //        Int16 mLastBornDeath = Program.DbValue_ToInt16(mDataRow["lastborndeath"]);
                            //        string mSp1 = Program.DateValueNullable(mDataRow["sp1"]);
                            //        string mSp2 = Program.DateValueNullable(mDataRow["sp2"]);
                            //        string mReferedTo = mDataRow["referedto"].ToString().Trim();
                            //        Int16 mPmtct = Program.DbValue_ToInt16(mDataRow["pmtcttest"]);
                            //        Int16 mAbortion = Program.DbValue_ToInt16(mDataRow["abortiveoutcomes"]);
                            //        Int16 mCaesarianSection = Program.DbValue_ToInt16(mDataRow["caesariansection"]);
                            //        Int16 mAnaemia = Program.DbValue_ToInt16(mDataRow["anaemia"]);
                            //        Int16 mOedema = Program.DbValue_ToInt16(mDataRow["oedema"]);
                            //        Int16 mProtenuria = Program.DbValue_ToInt16(mDataRow["protenuria"]);
                            //        Int16 mHighBloodPressure = Program.DbValue_ToInt16(mDataRow["hbp"]);
                            //        Int16 mRetardedWeight = Program.DbValue_ToInt16(mDataRow["retardedweight"]);
                            //        Int16 mBleeding = Program.DbValue_ToInt16(mDataRow["blooddischarge"]);
                            //        Int16 mBadBabyPosition = Program.DbValue_ToInt16(mDataRow["badbabyposition"]);
                            //        int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                            //        int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();
                            //        string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                            //        if (Program.DbValue_ToInt16(mDataRow["registrystatus"]) == 1)
                            //        {
                            //            mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString();
                            //        }

                            //        int mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_antenatalattendancelog(sysdate,bookdate,booking,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,cardpresented,pregage,noofpreg,"
                            //        + "syphilistest,tetanusvaccinedate,lastbirthyear,lastborndeath,sp1,sp2,referedto,pmtcttest,abortion,"
                            //        + "caesariansection,anaemia,oedema,protenuria,highbloodpressure,retardedweight,bleeding,badbabyposition,"
                            //        + "userid,registrystatus,yearpart,monthpart) values(" + mSysDate + "," + mTransDate + ",'" + mBooking + "','" 
                            //        + mPatientCode + "','" + mSurname + "','" + mFirstName + "','" + mOtherNames + "','" + mGender + "'," 
                            //        + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" + mStreet + "','" + mEthnicity + "','" + mReligion 
                            //        + "','" + mOccupation + "','" + mNextOfKin + "'," + mCardPresented + "," + mPregAge + "," + mNoOfPreg + "," 
                            //        + mSyphilisTest + "," + mTetanusVaccineDate + "," + mLastBirthYear + "," + mLastBornDeath + "," + mSp1 + "," 
                            //        + mSp2 + ",'" + mReferedTo + "'," + mPmtct + "," + mAbortion + "," + mCaesarianSection + "," + mAnaemia + "," 
                            //        + mOedema + "," + mProtenuria + "," + mHighBloodPressure + "," + mRetardedWeight + "," + mBleeding + "," 
                            //        + mBadBabyPosition + ",'" + mUserId + "','" + mRegistryStatus + "'," + mYearPart + "," + mMonthPart + ")";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_antenatalattendancelog')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_postnatalattendances

                            //if (mDvMigrationLog.Find("rch_postnatalattendances") < 0)
                            //{
                            //    mTotalRows = pDtPostnatalAttendances.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_postnatalattendances";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtPostnatalAttendances.Rows[mRowCount];

                            //        int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        Int16 mGravida = Program.DbValue_ToInt16(mDataRow["gravida"]);
                            //        Int16 mPara = Program.DbValue_ToInt16(mDataRow["para"]);
                            //        string mAdmissionDate = Program.DateValueNullable(mDataRow["admissiondate"]);
                            //        Int16 mNoOfChildren = Program.DbValue_ToInt16(mDataRow["noofchildren"]);
                            //        string mDischargeStatus = mDataRow["dischargestatus"].ToString().Trim();
                            //        string mAttendantId = mDataRow["attendantid"].ToString().Trim();
                            //        string mAttendantName = mDataRow["attendantname"].ToString().Trim();
                            //        Int16 mFromAntenatal = Program.DbValue_ToInt16(mDataRow["fromantenatal"]);
                            //        Int16 mPPH = Program.DbValue_ToInt16(mDataRow["pph"]);
                            //        Int16 mRetainedPlacenta = Program.DbValue_ToInt16(mDataRow["retainedplacenta"]);
                            //        Int16 mThirsDegreeTear = Program.DbValue_ToInt16(mDataRow["thirddegreetear"]);
                            //        Int16 mDeath = Program.DbValue_ToInt16(mDataRow["death"]);
                            //        Int16 mOtherComplications = Program.DbValue_ToInt16(mDataRow["othermethods"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();

                            //        string mBirthMethod = "";
                            //        switch (mDataRow["birthmethod"].ToString().Trim().ToLower())
                            //        {
                            //            case "bba": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.bba.ToString(); break;
                            //            case "normal": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.normaldelivery.ToString(); break;
                            //            case "vaccum": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.vacuum.ToString(); break;
                            //            case "caesarian section": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.caesariansection.ToString(); break;
                            //            case "abortion": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.abortion.ToString(); break;
                            //        }

                            //        int mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_postnatalattendances(autocode,bookdate,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,gravida,para,admissiondate,"
                            //        + "noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,postpartumhaemorrg,"
                            //        + "retainedplacenta,thirddegreetear,death,othercomplications,userid) values(" + mAutoCode + "," + mTransDate + ",'" 
                            //        + mPatientCode + "','" + mSurname + "','" + mFirstName + "','" + mOtherNames + "','" + mGender + "'," 
                            //        + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" + mStreet + "','" + mEthnicity + "','" + mReligion 
                            //        + "','" + mOccupation + "','" + mNextOfKin + "'," + mGravida + "," + mPara + "," + mAdmissionDate + "," 
                            //        + mNoOfChildren + ",'" + mDischargeStatus + "','" + mAttendantId + "','" + mAttendantName + "','" 
                            //        + mBirthMethod + "'," + mFromAntenatal + "," + mPPH + "," + mRetainedPlacenta + "," + mThirsDegreeTear
                            //        + "," + mDeath + "," + mOtherComplications + ",'" + mUserId + "')";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_postnatalattendances')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_postnatalattendancelog

                            //if (mDvMigrationLog.Find("rch_postnatalattendancelog") < 0)
                            //{
                            //    mTotalRows = pDtPostnatalAttendanceLog.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_postnatalattendancelog";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtPostnatalAttendanceLog.Rows[mRowCount];

                            //        string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                            //        string mBooking = mDataRow["booking"].ToString().Trim();
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        Int16 mGravida = Program.DbValue_ToInt16(mDataRow["gravida"]);
                            //        Int16 mPara = Program.DbValue_ToInt16(mDataRow["para"]);
                            //        string mAdmissionDate = Program.DateValueNullable(mDataRow["admissiondate"]);
                            //        Int16 mNoOfChildren = Program.DbValue_ToInt16(mDataRow["noofchildren"]);
                            //        string mDischargeStatus = mDataRow["dischargestatus"].ToString().Trim();
                            //        string mAttendantId = mDataRow["attendantid"].ToString().Trim();
                            //        string mAttendantName = mDataRow["attendantname"].ToString().Trim();
                            //        Int16 mFromAntenatal = Program.DbValue_ToInt16(mDataRow["fromantenatal"]);
                            //        Int16 mPPH = Program.DbValue_ToInt16(mDataRow["pph"]);
                            //        Int16 mRetainedPlacenta = Program.DbValue_ToInt16(mDataRow["retainedplacenta"]);
                            //        Int16 mThirsDegreeTear = Program.DbValue_ToInt16(mDataRow["thirddegreetear"]);
                            //        Int16 mDeath = Program.DbValue_ToInt16(mDataRow["death"]);
                            //        Int16 mOtherComplications = Program.DbValue_ToInt16(mDataRow["othermethods"]);
                            //        int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                            //        int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();
                            //        string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                            //        if (Program.DbValue_ToInt16(mDataRow["registrystatus"]) == 1)
                            //        {
                            //            mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString();
                            //        }

                            //        string mBirthMethod = "";
                            //        switch (mDataRow["birthmethod"].ToString().Trim().ToLower())
                            //        {
                            //            case "bba": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.bba.ToString(); break;
                            //            case "normal": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.normaldelivery.ToString(); break;
                            //            case "vaccum": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.vacuum.ToString(); break;
                            //            case "caesarian section": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.caesariansection.ToString(); break;
                            //            case "abortion": mBirthMethod = AfyaPro_Types.clsEnums.RCHBirthMethods.abortion.ToString(); break;
                            //        }

                            //        int mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_postnatalattendancelog(sysdate,bookdate,booking,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,gravida,para,admissiondate,"
                            //        + "noofchildren,dischargestatus,attendantid,attendantname,birthmethod,fromantenatal,postpartumhaemorrg,"
                            //        + "retainedplacenta,thirddegreetear,death,othercomplications,userid,registrystatus,yearpart,monthpart) values(" 
                            //        + mSysDate + "," + mTransDate + ",'" + mBooking + "','" + mPatientCode + "','" + mSurname + "','" + mFirstName 
                            //        + "','" + mOtherNames + "','" + mGender + "'," + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" + mStreet 
                            //        + "','" + mEthnicity + "','" + mReligion + "','" + mOccupation + "','" + mNextOfKin + "'," + mGravida + "," 
                            //        + mPara + "," + mAdmissionDate + "," + mNoOfChildren + ",'" + mDischargeStatus + "','" + mAttendantId + "','" 
                            //        + mAttendantName + "','" + mBirthMethod + "'," + mFromAntenatal + "," + mPPH + "," + mRetainedPlacenta + "," 
                            //        + mThirsDegreeTear + "," + mDeath + "," + mOtherComplications + ",'" + mUserId + "','" + mRegistryStatus 
                            //        + "'," + mYearPart + "," + mMonthPart + ")";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_postnatalattendancelog')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_postnatalchildren

                            //if (mDvMigrationLog.Find("rch_postnatalchildren") < 0)
                            //{
                            //    mTotalRows = pDtPostnatalChildren.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_postnatalchildren";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtPostnatalChildren.Rows[mRowCount];

                            //        string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                            //        string mBooking = mDataRow["booking"].ToString().Trim();
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mDeliveryDate = Program.DateValueNullable(mDataRow["deliverydate"]);
                            //        string mGender = mDataRow["gender"].ToString().Trim();
                            //        double mWeight = Program.DbValue_ToDouble(mDataRow["weight"]);
                            //        double mApgarScore = Program.DbValue_ToDouble(mDataRow["apgarscore"]);
                            //        Int16 mFreshBirth = Program.DbValue_ToInt16(mDataRow["freshbirth"]);
                            //        Int16 mMaceratedBirth = Program.DbValue_ToInt16(mDataRow["maceratedbirth"]);
                            //        string mChildProblems = mDataRow["childproblems"].ToString().Trim();
                            //        Int16 mLive = Program.DbValue_ToInt16(mDataRow["live"]);
                            //        Int16 mDeathBefore24 = Program.DbValue_ToInt16(mDataRow["deathbefore24"]);
                            //        Int16 mDeathAfter24 = Program.DbValue_ToInt16(mDataRow["deathafter24"]);
                            //        int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                            //        int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();

                            //        mCommand.CommandText = "insert into rch_postnatalchildren(sysdate,bookdate,booking,clientcode,deliverydate,"
                            //        + "gender,weight,apgarscore,freshbirth,maceratedbirth,childproblems,live,deathbefore24,deathafter24,"
                            //        + "userid,yearpart,monthpart) values(" + mSysDate + "," + mTransDate + ",'" + mBooking + "','" 
                            //        + mPatientCode + "'," + mDeliveryDate + ",'" + mGender + "'," + mWeight + "," + mApgarScore + "," 
                            //        + mFreshBirth + "," + mMaceratedBirth + ",'" + mChildProblems + "'," + mLive + "," + mDeathBefore24
                            //        + "," + mDeathAfter24 + ",'" + mUserId + "'," + mYearPart + "," + mMonthPart + ")";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_postnatalchildren')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_childrenattendances

                            //if (mDvMigrationLog.Find("rch_childrenattendances") < 0)
                            //{
                            //    mTotalRows = pDtChildrenAttendances.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_childrenattendances";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtChildrenAttendances.Rows[mRowCount];

                            //        int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        string mBCG = Program.DateValueNullable(mDataRow["bcg"]);
                            //        string mDPT1 = Program.DateValueNullable(mDataRow["dpt1"]);
                            //        string mDPT2 = Program.DateValueNullable(mDataRow["dpt2"]);
                            //        string mDPT3 = Program.DateValueNullable(mDataRow["dpt3"]);
                            //        string mMeasles = Program.DateValueNullable(mDataRow["measles"]);
                            //        Int16 mVitaminA = Program.DbValue_ToInt16(mDataRow["vitamina"]);
                            //        string mOPV0 = Program.DateValueNullable(mDataRow["opv0"]);
                            //        string mOPV1 = Program.DateValueNullable(mDataRow["opv1"]);
                            //        string mOPV2 = Program.DateValueNullable(mDataRow["opv2"]);
                            //        string mOPV3 = Program.DateValueNullable(mDataRow["opv3"]);
                            //        double mWeight = Program.DbValue_ToDouble(mDataRow["weight"]);
                            //        double mMeaslesWeight = Program.DbValue_ToDouble(mDataRow["measlesweight"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();

                            //        int mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_childrenattendances(autocode,bookdate,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,bcg,dpt1,dpt2,dpt3,"
                            //        + "measles,vitamina,opv0,opv1,opv2,opv3,weight,measlesweight,userid) values(" + mAutoCode + "," + mTransDate + ",'"
                            //        + mPatientCode + "','" + mSurname + "','" + mFirstName + "','" + mOtherNames + "','" + mGender + "',"
                            //        + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" + mStreet + "','" + mEthnicity + "','" + mReligion
                            //        + "','" + mOccupation + "','" + mNextOfKin + "'," + mBCG + "," + mDPT1 + "," + mDPT2 + ","
                            //        + mDPT3 + "," + mMeasles + "," + mVitaminA + "," + mOPV0 + "," + mOPV1 + "," + mOPV2 + "," + mOPV3 + "," 
                            //        + mWeight + "," + mMeaslesWeight + ",'" + mUserId + "')";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_childrenattendances')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_childrenattendancelog

                            //if (mDvMigrationLog.Find("rch_childrenattendancelog") < 0)
                            //{
                            //    mTotalRows = pDtChildrenAttendanceLog.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_childrenattendancelog";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtChildrenAttendanceLog.Rows[mRowCount];

                            //        string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                            //        string mBooking = mDataRow["booking"].ToString().Trim();
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        string mBCG = Program.DateValueNullable(mDataRow["bcg"]);
                            //        string mDPT1 = Program.DateValueNullable(mDataRow["dpt1"]);
                            //        string mDPT2 = Program.DateValueNullable(mDataRow["dpt2"]);
                            //        string mDPT3 = Program.DateValueNullable(mDataRow["dpt3"]);
                            //        string mMeasles = Program.DateValueNullable(mDataRow["measles"]);
                            //        Int16 mVitaminA = Program.DbValue_ToInt16(mDataRow["vitamina"]);
                            //        string mOPV0 = Program.DateValueNullable(mDataRow["opv0"]);
                            //        string mOPV1 = Program.DateValueNullable(mDataRow["opv1"]);
                            //        string mOPV2 = Program.DateValueNullable(mDataRow["opv2"]);
                            //        string mOPV3 = Program.DateValueNullable(mDataRow["opv3"]);
                            //        double mWeight = Program.DbValue_ToDouble(mDataRow["weight"]);
                            //        double mMeaslesWeight = Program.DbValue_ToDouble(mDataRow["measlesweight"]);
                            //        int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                            //        int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();
                            //        string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                            //        if (Program.DbValue_ToInt16(mDataRow["registrystatus"]) == 1)
                            //        {
                            //            mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString();
                            //        }

                            //        int mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_childrenattendancelog(sysdate,bookdate,booking,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,bcg,dpt1,dpt2,dpt3,"
                            //        + "measles,vitamina,opv0,opv1,opv2,opv3,weight,measlesweight,userid,registrystatus,yearpart,monthpart) values(" 
                            //        + mSysDate + "," + mTransDate + ",'" + mBooking + "','" + mPatientCode + "','" + mSurname + "','" + mFirstName 
                            //        + "','" + mOtherNames + "','" + mGender + "'," + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" 
                            //        + mStreet + "','" + mEthnicity + "','" + mReligion + "','" + mOccupation + "','" + mNextOfKin + "'," + mBCG 
                            //        + "," + mDPT1 + "," + mDPT2 + "," + mDPT3 + "," + mMeasles + "," + mVitaminA + "," + mOPV0 + "," + mOPV1 + "," 
                            //        + mOPV2 + "," + mOPV3 + "," + mWeight + "," + mMeaslesWeight + ",'" + mUserId + "','" + mRegistryStatus + "',"
                            //        + mYearPart + "," + mMonthPart + ")";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_childrenattendancelog')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //DataView mDvFPlanMethodsUsed = new DataView();
                            //mDvFPlanMethodsUsed.Table = pDtFPlanMethodsUsed;
                            //mDvFPlanMethodsUsed.Sort = "customercode,booking,methoddescription";

                            //#region rch_fplanattendances

                            //if (mDvMigrationLog.Find("rch_fplanattendances") < 0)
                            //{
                            //    mTotalRows = pDtFPlanAttendances.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_fplanattendances";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtFPlanAttendances.Rows[mRowCount];

                            //        int mAutoCode = Convert.ToInt32(mDataRow["autocode"]);
                            //        string mBooking = mDataRow["autocode"].ToString().Trim();
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        string mComplains = mDataRow["complains"].ToString().Trim();
                            //        string mUserId = mDataRow["userid"].ToString().Trim();

                            //        int mImplanon = 0;
                            //        int mOralPills = 0;
                            //        int mInjection = 0;
                            //        int mDiaphragm = 0;
                            //        int mIUCD = 0;
                            //        int mCondoms = 0;
                            //        int mFoamingTablets = 0;
                            //        int mBTL = 0;
                            //        int mNaturalMethods = 0;
                            //        int mOtherMethods = 0;

                            //        int mIndex = -1;
                            //        foreach (DataRow mMethodRow in pDtFPlanMethods.Rows)
                            //        {
                            //            string[] mMethodUsed = new string[3];
                            //            mMethodUsed[0] = mPatientCode;
                            //            mMethodUsed[1] = mBooking;
                            //            mMethodUsed[2] = mMethodRow["description"].ToString().Trim();

                            //            mIndex = mDvFPlanMethodsUsed.Find(mMethodUsed);
                            //            if (mIndex >= 0)
                            //            {
                            //                if (Convert.ToInt16(mMethodRow["implanon"]) == 1)
                            //                {
                            //                    mImplanon = mImplanon + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["oralpills"]) == 1)
                            //                {
                            //                    mOralPills = mOralPills + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["injection"]) == 1)
                            //                {
                            //                    mInjection = mInjection + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["diaphragm"]) == 1)
                            //                {
                            //                    mDiaphragm = mDiaphragm + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["iucd"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["condoms"]) == 1)
                            //                {
                            //                    mCondoms = mCondoms + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["foamingtabs"]) == 1)
                            //                {
                            //                    mFoamingTablets = mFoamingTablets + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["btl"]) == 1)
                            //                {
                            //                    mBTL = mBTL + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["naturalmethods"]) == 1)
                            //                {
                            //                    mNaturalMethods = mNaturalMethods + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["others"]) == 1)
                            //                {
                            //                    mOtherMethods = mOtherMethods + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //            }
                            //        }

                            //        mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_fplanattendances(autocode,bookdate,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,complains,implanon,oralpills,injection,"
                            //        + "diaphragm,iucd,condoms,foamingtablets,bilateraltubeligation,naturalmethods,othermethods,userid) values(" 
                            //        + mAutoCode + "," + mTransDate + ",'" + mPatientCode + "','" + mSurname + "','" + mFirstName + "','" 
                            //        + mOtherNames + "','" + mGender + "'," + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" + mStreet 
                            //        + "','" + mEthnicity + "','" + mReligion + "','" + mOccupation + "','" + mNextOfKin + "','" + mComplains 
                            //        + "'," + mImplanon + "," + mOralPills + "," + mInjection + "," +  mDiaphragm + "," + mIUCD + "," 
                            //        + mCondoms + "," + mFoamingTablets + "," + mBTL + "," + mNaturalMethods + "," + mOtherMethods + ",'" + mUserId + "')";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_fplanattendances')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            //#region rch_fplanattendancelog

                            //if (mDvMigrationLog.Find("rch_fplanattendancelog") < 0)
                            //{
                            //    mTotalRows = pDtFPlanAttendanceLog.Rows.Count;

                            //    //initialize transaction
                            //    mTrans = mConn.BeginTransaction();
                            //    mCommand.Transaction = mTrans;

                            //    mCommand.CommandText = "delete from rch_fplanattendancelog";
                            //    mCommand.ExecuteNonQuery();

                            //    for (int mRowCount = 0; mRowCount < mTotalRows; mRowCount++)
                            //    {
                            //        DataRow mDataRow = pDtFPlanAttendanceLog.Rows[mRowCount];

                            //        string mSysDate = Program.DateValueNullable(mDataRow["sysdate"]);
                            //        string mBooking = mDataRow["booking"].ToString().Trim();
                            //        string mTransDate = Program.DateValueNullable(mDataRow["bookdate"]);
                            //        string mPatientCode = mDataRow["customercode"].ToString().Trim();
                            //        string mSurname = "";
                            //        string mFirstName = "";
                            //        string mOtherNames = "";
                            //        string mGender = "";
                            //        string mBirthDate = "Null";
                            //        string mRegDate = "Null";
                            //        string mVillage = "";
                            //        string mStreet = "";
                            //        string mEthnicity = "";
                            //        string mReligion = "";
                            //        string mOccupation = "";
                            //        string mNextOfKin = "";
                            //        string mComplains = mDataRow["complains"].ToString().Trim();
                            //        int mYearPart = Program.DbValue_ToInt16(mDataRow["yearpart"]);
                            //        int mMonthPart = Program.DbValue_ToInt16(mDataRow["monthpart"]);
                            //        string mUserId = mDataRow["userid"].ToString().Trim();
                            //        string mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.New.ToString();
                            //        if (Program.DbValue_ToInt16(mDataRow["registrystatus"]) == 1)
                            //        {
                            //            mRegistryStatus = AfyaPro_Types.clsEnums.RegistryStatus.Re_Visiting.ToString();
                            //        }

                            //        int mImplanon = 0;
                            //        int mOralPills = 0;
                            //        int mInjection = 0;
                            //        int mDiaphragm = 0;
                            //        int mIUCD = 0;
                            //        int mCondoms = 0;
                            //        int mFoamingTablets = 0;
                            //        int mBTL = 0;
                            //        int mNaturalMethods = 0;
                            //        int mOtherMethods = 0;

                            //        int mIndex = -1;
                            //        foreach (DataRow mMethodRow in pDtFPlanMethods.Rows)
                            //        {
                            //            string[] mMethodUsed = new string[3];
                            //            mMethodUsed[0] = mPatientCode;
                            //            mMethodUsed[1] = mBooking;
                            //            mMethodUsed[2] = mMethodRow["description"].ToString().Trim();

                            //            mIndex = mDvFPlanMethodsUsed.Find(mMethodUsed);
                            //            if (mIndex >= 0)
                            //            {
                            //                if (Convert.ToInt16(mMethodRow["implanon"]) == 1)
                            //                {
                            //                    mImplanon = mImplanon + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["oralpills"]) == 1)
                            //                {
                            //                    mOralPills = mOralPills + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["injection"]) == 1)
                            //                {
                            //                    mInjection = mInjection + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["diaphragm"]) == 1)
                            //                {
                            //                    mDiaphragm = mDiaphragm + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["iucd"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["condoms"]) == 1)
                            //                {
                            //                    mCondoms = mCondoms + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["foamingtabs"]) == 1)
                            //                {
                            //                    mFoamingTablets = mFoamingTablets + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["btl"]) == 1)
                            //                {
                            //                    mBTL = mBTL + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["naturalmethods"]) == 1)
                            //                {
                            //                    mNaturalMethods = mNaturalMethods + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //                else if (Convert.ToInt16(mMethodRow["others"]) == 1)
                            //                {
                            //                    mOtherMethods = mOtherMethods + Convert.ToInt16(mDvFPlanMethodsUsed[mIndex]["qty"]);
                            //                }
                            //            }
                            //        }

                            //        mIndex = mDvRchPatients.Find(mPatientCode);
                            //        if (mIndex >= 0)
                            //        {
                            //            mSurname = mDvPatients[mIndex]["surname"].ToString().Trim();
                            //            mFirstName = mDvPatients[mIndex]["firstname"].ToString().Trim();
                            //            mOtherNames = mDvPatients[mIndex]["othernames"].ToString().Trim();
                            //            mGender = mDvPatients[mIndex]["gender"].ToString().Trim();
                            //            mBirthDate = Program.DateValueNullable(mDvPatients[mIndex]["birthdate"]);
                            //            mRegDate = Program.DateValueNullable(mDvPatients[mIndex]["regdate"]);
                            //            mVillage = mDvPatients[mIndex]["village"].ToString().Trim();
                            //            mStreet = mDvPatients[mIndex]["street"].ToString().Trim();
                            //            mEthnicity = mDvPatients[mIndex]["ethnicity"].ToString().Trim();
                            //            mReligion = mDvPatients[mIndex]["religion"].ToString().Trim();
                            //            mOccupation = mDvPatients[mIndex]["occupation"].ToString().Trim();
                            //            mNextOfKin = mDvPatients[mIndex]["nextofkin"].ToString().Trim();
                            //        }

                            //        mCommand.CommandText = "insert into rch_fplanattendancelog(sysdate,bookdate,booking,clientcode,clientsurname,"
                            //        + "clientfirstname,clientothernames,clientgender,clientbirthdate,clientregdate,clientvillage,clientstreet,"
                            //        + "clientethnicity,clientreligion,clientoccupation,clientnextofkin,complains,implanon,oralpills,injection,"
                            //        + "diaphragm,iucd,condoms,foamingtablets,bilateraltubeligation,naturalmethods,othermethods,userid,"
                            //        + "registrystatus,yearpart,monthpart) values("
                            //        + mSysDate + "," + mTransDate + ",'" + mBooking + "','" + mPatientCode + "','" + mSurname + "','" + mFirstName + "','"
                            //        + mOtherNames + "','" + mGender + "'," + mBirthDate + "," + mRegDate + ",'" + mVillage + "','" + mStreet
                            //        + "','" + mEthnicity + "','" + mReligion + "','" + mOccupation + "','" + mNextOfKin + "','" + mComplains
                            //        + "'," + mImplanon + "," + mOralPills + "," + mInjection + "," + mDiaphragm + "," + mIUCD + ","
                            //        + mCondoms + "," + mFoamingTablets + "," + mBTL + "," + mNaturalMethods + "," + mOtherMethods + ",'" 
                            //        + mUserId + "','" + mRegistryStatus + "'," + mYearPart + "," + mMonthPart + ")";
                            //        mCommand.ExecuteNonQuery();

                            //        pBackgroundWorkder.ReportProgress(((mRowCount + 1) * 100) / mTotalRows);
                            //    }

                            //    mCommand.CommandText = "insert into sys_migrationlog(tablename) values('rch_fplanattendancelog')";
                            //    mCommand.ExecuteNonQuery();

                            //    //commit changes to database
                            //    mTrans.Commit();
                            //}

                            //this.MarkDone(mRowIndex);
                            //mRowIndex++;

                            //#endregion

                            #endregion

                            #endregion
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //rollback changes made to the database
                    mTrans.Rollback();
                }
                catch { }
                switch (pActivity.Trim().ToLower())
                {
                    case "collecting":
                        {
                            pFinishedCollecting = false;
                            pErrorOnPage0 = true;
                        }
                        break;
                    case "migrating":
                        {
                            pFinishedMigrating = false;
                            pErrorOnPage1 = true;
                        }
                        break;
                }
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                try
                {
                    mConn.Close();
                }
                catch { }
            }
        }

        #endregion

        #region MarkNotDone

        private delegate void Mark_NotDone(int mRowIndex);
        private void MarkNotDone(int mRowIndex)
        {
            switch (pActivity.Trim().ToLower())
            {
                case "collecting":
                    {
                        if (grdObjects.InvokeRequired)
                        {
                            grdObjects.Invoke(new Mark_NotDone(MarkNotDone), mRowIndex);
                        }
                        else
                        {
                            DataRow mDataRow = pDtObjects.Rows[mRowIndex];
                            mDataRow.BeginEdit();
                            pDtObjects.Rows[mRowIndex]["selected"] = false;
                            mDataRow.EndEdit();
                        }
                    }
                    break;
                case "migrating":
                    {
                        if (grdMigrating.InvokeRequired)
                        {
                            grdMigrating.Invoke(new Mark_NotDone(MarkNotDone), mRowIndex);
                        }
                        else
                        {
                            DataRow mDataRow = pDtMigrating.Rows[mRowIndex];
                            mDataRow.BeginEdit();
                            pDtMigrating.Rows[mRowIndex]["selected"] = false;
                            mDataRow.EndEdit();
                        }
                    }
                    break;
            }
        }

        #endregion

        #region MarkDone
        private delegate void Mark_Done(int mRowIndex);
        private void MarkDone(int mRowIndex)
        {
            switch (pActivity.Trim().ToLower())
            {
                case "collecting":
                    {
                        if (grdObjects.InvokeRequired)
                        {
                            grdObjects.Invoke(new Mark_Done(MarkDone), mRowIndex);
                        }
                        else
                        {
                            DataRow mDataRow = pDtObjects.Rows[mRowIndex];
                            mDataRow.BeginEdit();
                            pDtObjects.Rows[mRowIndex]["selected"] = true;
                            mDataRow.EndEdit();

                            viewObjects.FocusedRowHandle = mRowIndex;
                        }
                    }
                    break;
                case "migrating":
                    {
                        if (grdMigrating.InvokeRequired)
                        {
                            grdMigrating.Invoke(new Mark_Done(MarkDone), mRowIndex);
                        }
                        else
                        {
                            DataRow mDataRow = pDtMigrating.Rows[mRowIndex];
                            mDataRow.BeginEdit();
                            pDtMigrating.Rows[mRowIndex]["selected"] = true;
                            mDataRow.EndEdit();

                            viewMigrating.FocusedRowHandle = mRowIndex;
                        }
                    }
                    break;
            }
        }

        #endregion

        #region pBackgroundWorkder1_ProgressChanged

        private void pBackgroundWorkder1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (pActivity.Trim().ToLower())
            {
                case "collecting":
                    {
                        progObjects.EditValue = e.ProgressPercentage;
                    }
                    break;
                case "migrating":
                    {
                        progMigrating.EditValue = e.ProgressPercentage;
                    }
                    break;
            }
        }

        #endregion

        #region pBackgroundWorkder1_RunWorkerCompleted

        private void pBackgroundWorkder1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (pActivity.Trim().ToLower())
            {
                case "collecting":
                    {
                        if (e.Error == null)
                        {
                            pFinishedCollecting = true;
                            page1.DescriptionText = "The wizard has finished preparing data. Click Next to begin migration";
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            pFinishedCollecting = false;
                            Program.Display_Error(e.Error.Message);
                            this.Cursor = Cursors.Default;
                        }
                    }
                    break;
                case "migrating":
                    {
                        if (e.Error == null)
                        {
                            pFinishedMigrating = true;
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            pFinishedMigrating = false;
                            Program.Display_Error(e.Error.Message);
                            this.Cursor = Cursors.Default;
                        }
                    }
                    break;
            }
        }

        #endregion

        #endregion
    }
}