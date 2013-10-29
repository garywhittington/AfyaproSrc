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

namespace AfyaPro_Types
{
    /// <summary>
    /// Contains the definition of all Enumerated data types
    /// </summary>
    public class clsEnums
    {
        /// <summary>
        /// Lists possible values for Staff Categories
        /// </summary>
        public enum StaffCategories 
        { 
            /// <summary>
            /// Staff categorised as Lab Technicians
            /// </summary>
            LabTechnicians = 0,
            /// <summary>
            /// Staff categorised as nurses
            /// </summary>
            Nurses = 1,
            /// <summary>
            /// Staff categorised as surgeons
            /// </summary>
            Surgeons = 2,
            /// <summary>
            /// Staff categorised as anesthesiologists
            /// </summary>
            Anesthesiologists = 3,
            /// <summary>
            /// Staff categorised as birth attendants
            /// </summary>
            BirthAttendants = 4,
            /// <summary>
            /// Staff categorised as birth attendants
            /// </summary>
            MedicalDoctors = 5,
            /// <summary>
            /// Staff categorised as doctors
            /// </summary>
            Pharmacists = 6,
            /// <summary>
            /// Staff categorised as Counsellors
            /// </summary>
            Counsellors = 7,
            /// <summary>
            /// Staff categorised as pharmacists
            /// </summary>
            Others = 50,
        };

        public enum SearchObjects
        {
            Patients = 0,
            Products = 1,
            ClientGroupMembers = 2,
            ClientGroups = 3,
            BillingItems = 4,
            Debtors = 5,
            Diagnoses = 6,
            Suppliers = 7,
            Stores = 8,
            MtuhaDiagnoses = 9,
            RCHClients = 10,
            DXTIndicators = 11,
            CTCClients = 12,
        }

        public enum DebtorTypes
        {
            Individual = 0,
            Group = 1,
        }

        public enum LabTestResultTypes
        {
            positivenegative = 0,
            number = 1,
            freetext = 2,
            dropdown = 3,
        }

        public enum FieldTypes
        {
            text,
            dropdown,
            checkbox,
        }

        public enum DataTypes
        {
            general,
            money,
            number,
            datetime,
        }

        public enum IPDBedStatus
        {
            Vacant,
            Occupied,
            On_Service,
        }

        public enum MTUHA_Diagnoses
        {
            acflpr,
            acrein,
            anaoth,
            anasic,
            asthma,
            burns,
            cardca,
            cardhy,
            cardot,
            choler,
            cliaid,
            congdi,
            dentca,
            dentot,
            dentpe,
            diabme,
            diarba,
            diarno,
            dysent,
            earinf,
            emorca,
            epilep,
            eyeinf,
            fradis,
            gedisy,
            genulc,
            gynaot,
            gynape,
            haemea,
            hephav,
            hephbv,
            hepoth,
            iiides,
            intest,
            lepros,
            loboty,
            malsev,
            malunc,
            measle,
            mening,
            misuco,
            neotet,
            neopla,
            neuros,
            nigaot,
            nigape,
            ninkid,
            ninski,
            nineye,
            nskifu,
            nutrot,
            osteom,
            oeyeca,
            oeyeni,
            penena,
            plague,
            pneumo,
            poison,
            proenm,
            psycho,
            raanbi,
            rabies,
            respot,
            rheuma,
            rhejoin,
            schist,
            sextra,
            skiinf,
            skinin,
            sninbi,
            thyroi,
            tuberc,
            typhoi,
            urtrin,
            vitade,
            yellfe,
            othdia,
        }

        public enum AuditTables
        {
            billaccounts,
            billaccountusers,
            dxtdiagnoses,
            dxticddiagnoses,
            dxtdiagnosesgroups,
            dxtdiagnosessubgroups,
            dxtpatientdiagnoseslog,
            dxtpatientdiagnosesfollowup,
            dxtpatientprescriptions,
            dxtpatientreferals,
            facilityautocodes,
            facilitybillinggroups,
            facilitybillingsubgroups,
            facilitybillingitems,
            facilitybookinglog,
            facilitycorporatemembers,
            facilitycorporates,
            facilitycorporatesubgroups,
            facilitycorporateitems,
            facilitydischargestatus,
            facilityoptions,
            facilitypaymenttypes,
            facilitystaffs,
            facilitytreatmentpoints,
            facilitywards,
            facilitywardrooms,
            patients,
            ipdadmissionslog,
            ipddischargeslog,
            ipdtransferslog,
            som_orders,
            som_orderitems,
            som_orderreceiveditems,
            som_productcontrol,
            som_producttransactions,
            som_transferins,
            som_transferinitems,
            som_transferinreceiveditems,
            som_stockreceipts,
            som_stockreceiptitems,
            som_transferouts,
            som_transferoutitems,
            som_transferoutissueditems,
            som_stockissues,
            som_stockissueitems,
            som_physicalinventory,
            som_physicalinventoryitems,
            sys_reportgroups,
            sys_usergroups,
            sys_users,
            rch_fplanattendancelog,
            rch_antenatalattendancelog,
            rch_postnatalattendancelog,
            rch_childrenattendancelog,
            rch_vaccinationslog,
            labpatienttests,
            ctc_patients,
        }

        public enum AuditChangeTypes
        {
            Insert = 0,
            Update = 1,
            Deleted = 2,
        }

        public enum IPDTransTypes
        {
            Admission = 0,
            Transfer = 1,
            Discharge = 2,
        }

        public enum PaymentSources
        {
            CashSale = 0,
            InvoicePayment = 1,
        }

        public enum VoidSources
        {
            CashSale = 0,
            InvoiceSale = 1,
            InvoicePayment = 2,
        }

        public enum PatientExtraInfos
        {
            surname,
            firstname,
            othernames,
            gender,
            birthdate,
            regdate,
            weight,
            temperature,
        }

        public enum RCHClientExtraInfos
        {
            surname,
            firstname,
            othernames,
            gender,
            birthdate,
            regdate,
        }

        public enum ProductExtraInfos
        {
            opmcode,
            opmdescription,
            description,
            departmentcode,
            departmentdescription,
        }

        public enum BuiltInReports
        {
            CashBox = 1,
            DebtorStatement = 2,
            BillingItemBarcodes = 3,
            AttendanceList = 4,
            AttendanceCountAge = 5,
            AttendanceCountTreatmentPoints = 6,
            AttendanceCountCustomerGroups = 7,
            IPDDailyCensusSummary = 8,
            IPDDailyCensusDetailed = 9,
            DXTDiagnosesSummary = 10,
            DXTDiagnosesListing = 11,
            BILDebtorsList = 12,
            BILDailySalesSummary = 13,
            BILDailySalesDetails = 14,
            BILGroupBillBreakDown = 17,
            BILDebtorsListIndividual = 18,
            BILDailyIncome = 23,
            BillDebtorsList1 = 45,

            IVStockBalance = 15,
            IVGRN = 19,
            IVGIN = 20,
            IVProductHistory = 21,
            IVPriceList = 22,

            LABPatientTestsCount = 16,
            LABCountByResult = 26,
            LABListingByResult = 27,

            RCH_AttendanceList = 24,
            RCH_VaccinationCount = 25,
            RCH_FPlanMethodsCount = 28,
            RCH_AntenatalAtt = 29,
            RCH_PostnatalAtt = 30,
            RCH_ANCMonthlySummary = 35,
            RCH_MaternityMonthlySummary =43,
            RCH_ObstetricMonthlyReport =44,

            DXTPatientHistory = 31,
            LABCountMonthly = 32,

            CTCAttendanceCountAge = 33,
            CTCAttendanceCD4T = 34,
           
            CTCTestingAndCounseling = 36,
            CTCDNAPCRMonthlyReport = 37,
            CTCARTClinicQuarterlySupervision = 38,
            CTCExposedChildrenMonthlyReport =39,
            MyCashBox = 40,
            LABPatientCountByLabTechnician = 41,

            UserLoginDetailReport = 42,
        }

        public enum ConditionOperators
        {
            Equals = 0,
            DoesNotEqual = 1,
            IsGreaterThan = 2,
            IsGreaterThanOrEqual = 3,
            IsLessThan = 4,
            IsLessThanOrEqual = 5,
            IsBetween = 6,
            IsNotBetween = 7,
            Contains = 8,
            DoesNotContain = 9,
            BeginsWith = 10,
            EndsWith = 11,
        }

        public enum PrintedWhens
        {
            NewPatientIsRegistered = 0,
            PatientIsReAttending = 1,
            PatientIsAdmitted = 2,
            PatientIsDischarged = 3,
        }

        public enum AccountUserTypes
        {
            Patient = 0,
        }

        public enum FacilityTypes
        {
            Dispensary = 0,
            HealthCentre = 1,
            DistrictHospital = 2,
            RegionalHospital = 3,
            NationalHospital = 4,
            ReferalHospital = 5,
            Laboratory = 6,             //new lab type
            RCH = 7,                    //new rch type
        }

        public enum IdPositions
        {
            IdLeft = 0,
            IdCenter = 1,
            IdRight = 2,
        }

        /// <summary>
        /// Lists possible values for client groups
        /// </summary>
        public enum BuiltInClientGroups
        {
            /// <summary>
            /// Client group defined as National Health Insurance (NHIF)
            /// </summary>
            NHIF = 0,
            /// <summary>
            /// Client group defined as Community Health Fund (CHF)
            /// </summary>
            CHF = 1,
            /// <summary>
            /// Client group defined as Exemptions
            /// </summary>
            Exempt = 2,
        };

        public enum RegistryStatus
        {
            Re_Visiting = 0,
            New = 1,
        };

        public enum SomOrderStatus
        {
            Open = 0,
            Closed = 1,
            Partial = 2,
        };

        public enum SomTransferStatus
        {
            Open = 0,
            Closed = 1,
            Partial = 2,
        };

        public enum SomTransferTypes
        {
            Normal = 0,
            StoreToStore = 1,
            Patient = 2,
        };

        public enum SomInventoryStatus
        {
            Open = 0,
            Calculated = 1,
            Closed = 2,
        };

        public enum SomOrderTransCodes
        {
            NewEntry = 0,
            ReverseEntry = 1,
        };

        public enum SomCustomerTypes
        {
            Internal = 0,
            External = 1,
            Patient = 2,
        };

        public enum AccountEntrySides
        {
            Debit = 1,
            Credit = 2,
            Transfer = 3,
        };

        public enum SomRequestTransCodes
        {
            NewEntry = 0,
            ReverseEntry = 1,
        };

        public enum SomIssueTransCodes
        {
            NewEntry = 0,
            ReverseEntry = 1,
        };

        public enum InvoiceStatus
        {
            Open = 1,
            Closed = 2,
            Cancelled = 3,
        };

        public enum DebtReliefRequestStatus
        {
            Open = 1,
            Approved = 2,
            Rejected = 3,
        };

        public enum CartNames
        {
            Orders = 0,
            Products = 1,
            ProductBalances = 2,
            Patients = 3,
        };

        public enum InvoiceTransTypes
        {
            NewInvoice = 1,
            Cancellation = 2,
            Payment = 3,
        };

        public enum BillingTransTypes
        {
            Cash = 1,
            Invoice = 2,
            Refund = 3,
            Payment = 4,
            PaymentCancellation = 5,
            SaleCancellation = 6,
            CreditNote = 7,
            CreditNoteCancellation = 8,
            Deposit = 9,
            Sales = 10,
        };

        public enum ProductTransTypes
        {
            Receive = 1,
            Issue = 2,
            Adjustment = 3,
            SystemAdjustment = 4,
        };

        public enum IPDDischargeStatus
        {
            /// <summary>
            /// Death
            /// </summary>
            Death = 0,
            /// <summary>
            /// Abscondee
            /// </summary>
            Abscondee = 1,
        };

        public enum PatientCategories
        {
            OPD = 0,
            IPD = 1,
        };

        public enum PatientsQueueTypes
        {
            Consultation = 0,
            LabTests = 1,
            Results = 2,
        };

        public enum BillingGroupMembershipStatus
        {
            Terminated = 0,
            Active = 1,
        };

        /// <summary>
        /// Lists the pre-defined codes that can automatically be generated by the system
        /// </summary>
        public enum SystemGeneratedCodes
        {
            /// <summary>
            /// Patient numbers
            /// </summary>
            patientno = 0,
            /// <summary>
            /// RCH customer numbers
            /// </summary>
            rchcustomerno = 1,
            /// <summary>
            /// Billing receipt numbers
            /// </summary>
            receiptno = 2,
            /// <summary>
            /// Billing invoice numbers
            /// </summary>
            invoiceno = 3,
            /// <summary>
            /// Surgery numbers
            /// </summary>
            surgeryno = 4,
            /// <summary>
            /// Deposit numbers
            /// </summary>
            deposittransactionid = 5,
            /// <summary>
            /// Deposit transfer numbers
            /// </summary>
            deposittransferno = 6,
            /// <summary>
            /// Bill payment numbers
            /// </summary>
            pymtno = 7,
            /// <summary>
            /// Bill credit note numbers
            /// </summary>
            crnoteno = 8,
            /// <summary>
            /// Bill payment cancellation numbers
            /// </summary>
            pycanno = 9,
            /// <summary>
            /// Sale cancellation numbers
            /// </summary>
            slcanno = 10,
            /// <summary>
            /// User group codes
            /// </summary>
            usergroupcode = 11,
            /// <summary>
            /// Country codes
            /// </summary>
            countrycode = 12,
            /// <summary>
            /// Region codes
            /// </summary>
            regioncode = 13,
            /// <summary>
            /// District codes
            /// </summary>
            districtcode = 14,
            /// <summary>
            /// Medical staff codes
            /// </summary>
            staffcode = 15,
            /// <summary>
            /// Treatment point codes
            /// </summary>
            treatmentpointcode = 16,
            /// <summary>
            /// Currency codes
            /// </summary>
            currencycode = 17,
            /// <summary>
            /// Payment type codes
            /// </summary>
            paymenttypecode = 18,
            /// <summary>
            /// Client group codes
            /// </summary>
            clientgroupcode = 19,
            /// <summary>
            /// Billing item group codes
            /// </summary>
            billingitemgroupcode = 20,
            /// <summary>
            /// Billing item sub-group codes
            /// </summary>
            billingitemsubgroupcode = 21,
            /// <summary>
            /// Billing item codes
            /// </summary>
            billingitemcode = 22,
            /// <summary>
            /// Client Sub-Group codes
            /// </summary>
            clientsubgroupcode = 23,
            /// <summary>
            /// IPD ward codes
            /// </summary>
            ipdwardcode = 24,
            /// <summary>
            /// IPD Room Codes
            /// </summary>
            ipdroomcode = 25,
            /// <summary>
            /// IPD Death Status
            /// </summary>
            ipddischargestatus = 26,
            /// <summary>
            /// Product Codes
            /// </summary>
            productcode = 27,
            /// <summary>
            /// Supplier Codes
            /// </summary>
            suppliercode = 28,
            /// <summary>
            /// product category numbers
            /// </summary>
            productcategorycode = 29,
            /// <summary>
            /// packaging numbers
            /// </summary>
            packagingcode = 30,
            /// <summary>
            /// customer numbers
            /// </summary>
            customerid = 31,
            /// <summary>
            /// client group member codes
            /// </summary>
            clientgroupmembercode = 32,
            /// <summary>
            /// client group membership Id
            /// </summary>
            clientgroupmembershipId = 33,
            /// <summary>
            /// invoice payment #
            /// </summary>
            paymentno = 34,
            /// <summary>
            /// patient document
            /// </summary>
            patientdocumentcode = 35,
            /// <summary>
            /// reportgroupcode
            /// </summary>
            reportgroupcode = 36,
            /// <summary>
            /// reportcode
            /// </summary>
            reportcode = 37,
            /// <summary>
            /// depositaccountcode
            /// </summary>
            depositaccountcode = 38,
            /// <summary>
            /// refundno
            /// </summary>
            refundno = 39,
            /// <summary>
            /// dxincidencekey
            /// </summary>
            dxincidencekey = 40,
            /// <summary>
            /// patientreferalcodes
            /// </summary>
            patientreferalcodes = 41,
            /// <summary>
            /// dxgroupcode
            /// </summary>
            dxgroupcode = 42,
            /// <summary>
            /// Order numbers
            /// </summary>
            orderno = 43,
            /// <summary>
            /// Delivery numbers
            /// </summary>
            deliveryno = 44,
            /// <summary>
            /// transferin numbers
            /// </summary>
            transferinno = 45,
            /// <summary>
            /// transferout numbers
            /// </summary>
            transferoutno = 46,
            /// <summary>
            /// stocktakingno numbers
            /// </summary>
            stocktakingno = 47,
            /// <summary>
            /// Lab numbers
            /// </summary>
            laboritoryno = 48,
            /// <summary>
            /// Lab TestGroup Code
            /// </summary>
            labtestgroupcode = 49,
            /// <summary>
            /// Lab Test Code
            /// </summary>
            labtestcode = 50,
            /// <summary>
            /// Ward room bed Code
            /// </summary>
            wardroombedcode = 51,
            /// <summary>
            /// Lab test sub group
            /// </summary>
            labtestsubgroupcode = 52,
            /// <summary>
            /// Debt relief request numbers
            /// </summary>
            debtreliefrequestno = 53,
            /// <summary>
            /// Booking numbers
            /// </summary>
            bookingno = 56,
            /// <summary>
            /// Indicator Groups
            /// </summary>
            indicatorgroups = 57,
            /// <summary>
            /// Indicators
            /// </summary>
            indicators = 58,
            /// <summary>
            /// VTC #
            /// </summary>
            vctnumbers = 59,
            /// <summary>
            /// HIV #
            /// </summary>
            hivnumbers = 60,
            /// <summary>
            /// CTC #
            /// </summary>
            ctcnumbers = 61,
            /// <summary>
            /// CTC #
            /// </summary>
            ctcbookingno = 62,
            /// <summary>
            /// Sample Id
            /// </summary>
            ctcsampleid = 63,
            /// <summary>
            /// pmtct antenatal incidence code
            /// </summary>
            ctcpmtctindecence = 64,
            /// <summary>
            /// pmtct antenatal incidence code
            /// </summary>
            ctchivtestnumber = 65,
            /// <summary>
            /// arv #
            /// </summary>
            ctcarvnumber = 66,
        };

        #region RCH states

        /// <summary>
        /// Different types of Family Planning Methods
        /// </summary>
        public enum RCHFamilyPlanningMethods
        {

            implanon = 0,
            oralpills = 1,
            injection = 2,
            diaphragm = 3,
            iucd = 4,
            condoms = 5,
            foamingtablets = 6,
            bilateraltubeligation = 7,
            naturalmethods = 8,
        }

        /// <summary>
        /// Different types of Antenatal Danger Indicators
        /// </summary>
        public enum RCHDangerIndicators
        {
            abortion = 0,
            caesariansection = 1,
            anaemia = 2,
            oedema = 3,
            protenuria = 4,
            highbloodpressure = 5,
            retardedweight = 6,
            bleeding = 7,
            badbabyposition = 8,
        }

        /// <summary>
        /// Different types of Birth Methods
        /// </summary>
        public enum RCHBirthMethods
        {
            bba = 0,
            normaldelivery = 1,
            vacuum = 2,
            caesariansection = 3,
            abortion = 4,
        }

        /// <summary>
        /// Different types of Birth Complication
        /// </summary>
        public enum RCHBirthComplication
        {
            postpartumhaemorrg = 0,
            retainedplacenta = 1,
            thirddegreetear = 2,
            death = 3,
        }

        /// <summary>
        /// Different types of Still Births
        /// </summary>
        public enum RCHStillBirths
        {
            freshbirth = 0,
            maceratedbirth = 1,
        }

        /// <summary>
        /// Different types of child conditions
        /// </summary>
        public enum RCHChildConditions
        {
            live = 0,
            deathbefore24 = 1,
            deathafter24 = 2,
        }

        /// <summary>
        /// Different types of dpts
        /// </summary>
        public enum RCHDPTs
        {
            dpt1 = 0,
            dpt2 = 1,
            dpt3 = 2,
        }

        /// <summary>
        /// Different types of opvs
        /// </summary>
        public enum RCHOPVs
        {
            opv0 = 0,
            opv1 = 1,
            opv2 = 2,
            opv3 = 3,
        }

        /// <summary>
        /// services within RCH
        /// </summary>
        public enum RCHServices
        {
            familyplanning = 0,
            antenatalcare = 1,
            postnatalcare = 2,
            childrenhealth = 3,
            vaccinations = 4,
            maternity = 5,
        }

        /// <summary>
        /// Different types of Vaccines
        /// </summary>
        public enum RCHVaccines
        {
            tt = 0,
            bcg = 1,
            sp1 = 2,
            sp2 = 3,
            dpt1 = 4,
            dpt2 = 5,
            dpt3 = 6,
            opv0 = 7,
            opv1 = 8,
            opv2 = 9,
            opv3 = 10,
            measles = 11,
            vitasupp = 12,
        }

        #endregion 

        #region CTC

        /// <summary>
        /// Codes within CTC
        /// </summary>
        public enum CTCCodes
        {
            MaritalStatus = 0,
            FunctionalStatus = 1,
            TBStatus = 2,
            ARVStatus = 3,
            AIDSIllness = 4,
            ARVCombRegimens = 5,
            ARVAdherence = 6,
            ARVPoorAdherenceReasons = 7,
            ReferedTo = 8,
            ARVReasons = 9,
            FollowUpStatus = 10,
            ReferedFrom = 11,
            PriorARVExposure = 12,
            ARTWhyEligible = 13,
            RelevantComeds = 14,
            pmtctcomb = 15,
            pmtctdisclosedto = 16,
            abnormallabresults = 17,
            wastingcodes = 18,
            artoutcomes = 19,
            breastfeedings = 20,
            motherstatus = 21,
            hivinfections = 22,
            clinmonit = 23
        }

        public enum CTC_HIVTestResults
        {
            Unknown = 0,
            Positive = 1,
            Negative = 2,
        }

        public enum CTC_HIVTestResultsGiven
        {
            Unknown = 0,
            Positive = 1,
            Negative = 2,
            Exposed = 3,
            Inconclusive = 4,
        }

        public enum CTC_PCRTestResultsGiven
        {
            Unknown = 0,
            Positive = 1,
            Negative = 2,
            Redraw = 3,
        }
        public enum CTC_ApptTypes
        {
            HIVTest = 0,
            ARTVisit = 1,
            PMTCTVisit = 2,
        }

        public enum CTC_ApptStatus
        {
            New = 0,
            Met = 1,
            Missed = 2,
        }

        public enum CTC_PreventiveTherapy
        {
            Cotrimoxazole = 0,
            Diflucan = 1,
        }

        public enum CTC_PMTCTInfantFeedingIntention
        {
            Breast = 0,
            Replace = 1,
            Undecided = 2,
        }

        public enum CTC_PMTCTWhenStartedHAART
        {
            BeforePreg = 0,
            DuringPreg = 1,
            NA = 2,
        }

        public enum CTC_ClientDecision
        {
            TestToday = 0,
            TestAnotherDay = 1,
            Other = 2,
        }

        #endregion

        #region month
        /// <summary>
        /// Lists months of an year.
        /// </summary>
       public enum MONTH
        {
           JANUARY = 1,
           FEBRUARY = 2,
           MARCH = 3,
           APRIL = 4,
           MAY = 5,
           JUNE = 6,
           JULY = 7,
           AUGUST = 8,
           SEPTEMBER = 9,
           OCTOBER = 10,
           NOVEMBER = 11,
           DECEMBER = 12,
        };
        #endregion

       #region quarter
       /// <summary>
       /// Lists the quarters of an year
       /// </summary>
       public enum QUARTER
       {

           Q1 = 1,
           Q2 = 4,
           Q3 = 7,
           Q4 = 10,
       };
       #endregion
    }
}
