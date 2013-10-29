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
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;

namespace AfyaPro_Service
{
    public partial class clsService : ServiceBase
    {
        private TcpServerChannel pChan;
        private Type pType;
        private String pClassName = "";
        private String pFunctionName = "";
        private String pErrorMessage = "";

        public clsService()
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
        }

        protected override void OnStart(string[] args)
        {
            pFunctionName = "RegisterObjects";

            try
            {
                new AfyaPro_MT.clsGlobal();
                //pChan = new TcpServerChannel(AfyaPro_MT.clsGlobal.gMiddleTierPort);
                //ChannelServices.RegisterChannel(pChan, false);

                BinaryServerFormatterSinkProvider mFormatterSink = new BinaryServerFormatterSinkProvider();
                mFormatterSink.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                IDictionary mHashTable = new Hashtable();
                mHashTable["port"] = AfyaPro_MT.clsGlobal.gMiddleTierPort;
                mHashTable["name"] = "AfyaPro_Mid";
                mHashTable["useIpAddress"] = false;
                pChan = new TcpServerChannel(mHashTable, mFormatterSink);

                #region registrations

                //clsRegistrations
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsRegistrations, AfyaPro_MT"),
                "clsRegistrations", WellKnownObjectMode.SingleCall);

                //clsPatientDocuments
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPatientDocuments, AfyaPro_MT"),
                "clsPatientDocuments", WellKnownObjectMode.SingleCall);

                #endregion

                #region inpatientdept

                //clsWards
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsWards, AfyaPro_MT"),
                "clsWards", WellKnownObjectMode.SingleCall);

                //clsWardRooms
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsWardRooms, AfyaPro_MT"),
                "clsWardRooms", WellKnownObjectMode.SingleCall);

                //clsWardRoomBeds
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsWardRoomBeds, AfyaPro_MT"),
                "clsWardRoomBeds", WellKnownObjectMode.SingleCall);

                //clsDischargeStatus
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDischargeStatus, AfyaPro_MT"),
                "clsDischargeStatus", WellKnownObjectMode.SingleCall);

                #endregion

                #region billing

                //clsCurrencies
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsCurrencies, AfyaPro_MT"),
                "clsCurrencies", WellKnownObjectMode.SingleCall);

                //clsPaymentTypes
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPaymentTypes, AfyaPro_MT"),
                "clsPaymentTypes", WellKnownObjectMode.SingleCall);

                //clsPriceCategories
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPriceCategories, AfyaPro_MT"),
                "clsPriceCategories", WellKnownObjectMode.SingleCall);

                //clsClientGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsClientGroups, AfyaPro_MT"),
                "clsClientGroups", WellKnownObjectMode.SingleCall);

                //clsClientSubGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsClientSubGroups, AfyaPro_MT"),
                "clsClientSubGroups", WellKnownObjectMode.SingleCall);

                //clsClientGroupMembers
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsClientGroupMembers, AfyaPro_MT"),
                "clsClientGroupMembers", WellKnownObjectMode.SingleCall);

                //clsBillItemGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsBillItemGroups, AfyaPro_MT"),
                "clsBillItemGroups", WellKnownObjectMode.SingleCall);

                //clsBillItemSubGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsBillItemSubGroups, AfyaPro_MT"),
                "clsBillItemSubGroups", WellKnownObjectMode.SingleCall);

                //clsBillItems
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsBillItems, AfyaPro_MT"),
                "clsBillItems", WellKnownObjectMode.SingleCall);

                //clsBilling
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsBilling, AfyaPro_MT"),
                "clsBilling", WellKnownObjectMode.SingleCall);

                //clsDepositAccounts
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDepositAccounts, AfyaPro_MT"),
                "clsDepositAccounts", WellKnownObjectMode.SingleCall);

                //clsBillDebtReliefs
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsBillDebtReliefs, AfyaPro_MT"),
                "clsBillDebtReliefs", WellKnownObjectMode.SingleCall);

                #endregion

                #region generalsetup


                //clsFacilitySetup
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsFacilitySetup, AfyaPro_MT"),
                "clsFacilitySetup", WellKnownObjectMode.SingleCall);
                //clsAutoCodes
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsAutoCodes, AfyaPro_MT"),
                "clsAutoCodes", WellKnownObjectMode.SingleCall);
                //clsCountries
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsCountries, AfyaPro_MT"),
                "clsCountries", WellKnownObjectMode.SingleCall);
                //clsCountries
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsCountries, AfyaPro_MT"),
                "clsCountries", WellKnownObjectMode.SingleCall);
                //clsRegions
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsRegions, AfyaPro_MT"),
                "clsRegions", WellKnownObjectMode.SingleCall);
                //clsDistricts
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDistricts, AfyaPro_MT"),
                "clsDistricts", WellKnownObjectMode.SingleCall);
                //clsMedicalStaffs
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsMedicalStaffs, AfyaPro_MT"),
                "clsMedicalStaffs", WellKnownObjectMode.SingleCall);
                //clsTreatmentPoints
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsTreatmentPoints, AfyaPro_MT"),
                "clsTreatmentPoints", WellKnownObjectMode.SingleCall);
                //clsSearchEngine
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSearchEngine, AfyaPro_MT"),
                "clsSearchEngine", WellKnownObjectMode.SingleCall);
                //clsPrintersSetup
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPrintersSetup, AfyaPro_MT"),
                "clsPrintersSetup", WellKnownObjectMode.SingleCall);
                //clsPatientExtraFields
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPatientExtraFields, AfyaPro_MT"),
                "clsPatientExtraFields", WellKnownObjectMode.SingleCall);

                #endregion

                #region securitysetup

                //clsUserGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsUserGroups, AfyaPro_MT"),
                "clsUserGroups", WellKnownObjectMode.SingleCall);

                //clsUserGroupPrinters
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsUserGroupPrinters, AfyaPro_MT"),
                "clsUserGroupPrinters", WellKnownObjectMode.SingleCall);

                //clsUsers
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsUsers, AfyaPro_MT"),
                "clsUsers", WellKnownObjectMode.SingleCall);

                #endregion

                #region reports

                //clsReporter
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsReporter, AfyaPro_MT"),
                "clsReporter", WellKnownObjectMode.SingleCall);

                //clsReportGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsReportGroups, AfyaPro_MT"),
                "clsReportGroups", WellKnownObjectMode.SingleCall);

                //clsReports
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsReports, AfyaPro_MT"),
                "clsReports", WellKnownObjectMode.SingleCall);

                #endregion

                #region others

                //clsLanguage
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsLanguage, AfyaPro_MT"),
                "clsLanguage", WellKnownObjectMode.SingleCall);

                //clsGridColumns
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsGridColumns, AfyaPro_MT"),
                "clsGridColumns", WellKnownObjectMode.SingleCall);

                //clsUserActions
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsUserActions, AfyaPro_MT"),
                "clsUserActions", WellKnownObjectMode.SingleCall);

                //clsDataImportExport
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDataImportExport, AfyaPro_MT"),
                "clsDataImportExport", WellKnownObjectMode.SingleCall);

                #endregion

                #region inventorysetup

                //clsSomStores
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomStores, AfyaPro_MT"),
                "clsSomStores", WellKnownObjectMode.SingleCall);

                //clsSomProductCategories
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomProductCategories, AfyaPro_MT"),
                "clsSomProductCategories", WellKnownObjectMode.SingleCall);

                //clsSomPackagings
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomPackagings, AfyaPro_MT"),
                "clsSomPackagings", WellKnownObjectMode.SingleCall);

                //clsSomSuppliers
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomSuppliers, AfyaPro_MT"),
                "clsSomSuppliers", WellKnownObjectMode.SingleCall);

                //clsSomCustomers
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomCustomers, AfyaPro_MT"),
                "clsSomCustomers", WellKnownObjectMode.SingleCall);

                //clsSomProducts
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomProducts, AfyaPro_MT"),
                "clsSomProducts", WellKnownObjectMode.SingleCall);

                #endregion

                #region inventory

                //clsSomOrders
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomOrders, AfyaPro_MT"),
                "clsSomOrders", WellKnownObjectMode.SingleCall);

                //clsSomTransferIns
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomTransferIns, AfyaPro_MT"),
                "clsSomTransferIns", WellKnownObjectMode.SingleCall);

                //clsSomTransferOuts
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomTransferOuts, AfyaPro_MT"),
                "clsSomTransferOuts", WellKnownObjectMode.SingleCall);

                //clsSomPhysicalInventory
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsSomPhysicalInventory, AfyaPro_MT"),
                "clsSomPhysicalInventory", WellKnownObjectMode.SingleCall);

                #endregion

                #region Diagnoses and Treatments

                //clsDiagnosesGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDiagnosesGroups, AfyaPro_MT"),
                "clsDiagnosesGroups", WellKnownObjectMode.SingleCall);

                //clsDiagnosesSubGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDiagnosesSubGroups, AfyaPro_MT"),
                "clsDiagnosesSubGroups", WellKnownObjectMode.SingleCall);

                //clsDiagnoses
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDiagnoses, AfyaPro_MT"),
                "clsDiagnoses", WellKnownObjectMode.SingleCall);

                //clsPatientDiagnoses
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPatientDiagnoses, AfyaPro_MT"),
                "clsPatientDiagnoses", WellKnownObjectMode.SingleCall);

                //clsDXTIndicatorGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDXTIndicatorGroups, AfyaPro_MT"),
                "clsDXTIndicatorGroups", WellKnownObjectMode.SingleCall);

                //clsDXTIndicators
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsDXTIndicators, AfyaPro_MT"),
                "clsDXTIndicators", WellKnownObjectMode.SingleCall);

                #endregion

                #region MTUHA

                //clsMtuhaDiagnoses
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsMtuhaDiagnoses, AfyaPro_MT"),
                "clsMtuhaDiagnoses", WellKnownObjectMode.SingleCall);

                #endregion

                #region Laboratories
                //clsLaboratories
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsLaboratories, AfyaPro_MT"),
                "clsLaboratories", WellKnownObjectMode.SingleCall);

                //clsLabTestGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsLabTestGroups, AfyaPro_MT"),
                "clsLabTestGroups", WellKnownObjectMode.SingleCall);

                //clsLabTestSubGroups
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsLabTestSubGroups, AfyaPro_MT"),
                "clsLabTestSubGroups", WellKnownObjectMode.SingleCall);

                //clsLabTests
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsLabTests, AfyaPro_MT"),
                "clsLabTests", WellKnownObjectMode.SingleCall);

                //clsLabRemarks
                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsLabRemarks, AfyaPro_MT"),
                "clsLabRemarks", WellKnownObjectMode.SingleCall);

                //clsPatientLabTests 

                RemotingConfiguration.RegisterWellKnownServiceType(
                System.Type.GetType("AfyaPro_MT.clsPatientLabTests, AfyaPro_MT"),
                "clsPatientLabTests", WellKnownObjectMode.SingleCall);


                #endregion

                #region RCH

                //clsRCHFPlanMethods
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHFPlanMethods, AfyaPro_MT"),
               "clsRCHFPlanMethods", WellKnownObjectMode.SingleCall);

                //clsRCHDangerIndicators
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHDangerIndicators, AfyaPro_MT"),
               "clsRCHDangerIndicators", WellKnownObjectMode.SingleCall);

                //clsRCHBirthMethods
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHBirthMethods, AfyaPro_MT"),
               "clsRCHBirthMethods", WellKnownObjectMode.SingleCall);

                //clsRCHBirthComplications
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHBirthComplications, AfyaPro_MT"),
               "clsRCHBirthComplications", WellKnownObjectMode.SingleCall);

                //clsRCHVaccines
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHVaccines, AfyaPro_MT"),
               "clsRCHVaccines", WellKnownObjectMode.SingleCall);

                //clsRCHClients
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHClients, AfyaPro_MT"),
               "clsRCHClients", WellKnownObjectMode.SingleCall);

                //clsRCHFamilyPlanning
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHFamilyPlanning, AfyaPro_MT"),
               "clsRCHFamilyPlanning", WellKnownObjectMode.SingleCall);

                //clsRCHAnteNatalCare
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHAnteNatalCare, AfyaPro_MT"),
               "clsRCHAnteNatalCare", WellKnownObjectMode.SingleCall);

                //clsRCHPostNatalCare
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHPostNatalCare, AfyaPro_MT"),
               "clsRCHPostNatalCare", WellKnownObjectMode.SingleCall);

                //clsRCHChildren
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHChildren, AfyaPro_MT"),
               "clsRCHChildren", WellKnownObjectMode.SingleCall);

                //clsRCHVaccinations
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHVaccinations, AfyaPro_MT"),
               "clsRCHVaccinations", WellKnownObjectMode.SingleCall);

                //clsRCHMaternity
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsRCHMaternity, AfyaPro_MT"),
               "clsRCHMaternity", WellKnownObjectMode.SingleCall);

                #endregion

                #region CTC

                //clsCTCCodes
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsCTCCodes, AfyaPro_MT"),
               "clsCTCCodes", WellKnownObjectMode.SingleCall);

                //clsCTCClients
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsCTCClients, AfyaPro_MT"),
               "clsCTCClients", WellKnownObjectMode.SingleCall);

                //clsCTCContactableStaff
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsQuarterlySupervisionReport, AfyaPro_MT"),
               "clsQuarterlySupervisionReport", WellKnownObjectMode.SingleCall);

                #endregion

                #region SMS

                //clsSMS
                RemotingConfiguration.RegisterWellKnownServiceType(
               System.Type.GetType("AfyaPro_MT.clsSMS, AfyaPro_MT"),
               "clsSMS", WellKnownObjectMode.SingleCall);
                             

                #endregion
            }
            catch (Exception ex)
            {
                pErrorMessage = AfyaPro_MT.clsGlobal.Write_Error(pClassName, pFunctionName, ex.Message);
                return;
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
