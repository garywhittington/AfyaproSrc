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
    public class clsSystemMessages
    {
        public enum MessageIds
        {
            #region General Messages

            GRL_SettingsSavedSuccessfully,
            GRL_DeleteRecordQuestion,
            GRL_TransactionDateIsInvalid,
            GRL_MembershipIdIsInvalid,
            GRL_NameIsInvalid,
            GRL_ImageFormatIsNotSupported,
            GRL_FileNameIsInvalid,
            GRL_IgnoreSomething,
            GRL_InvalidTypeConvertion,
            GRL_DateIsInvalid,
            GRL_DateRangeIsInvalid,
            GRL_EntryIsInvalid,
            GRL_AccessDeniedToSystemFunction,

            GRL_SystemFilterNone,
            GRL_Invalid_Date_Greater,
            GRL_Invalid_Date_LessThan_1000,

            #endregion

            #region tools

            TOO_ImportationSuccessful,
            TOO_QuitWizardQuestion,
            TOO_SelectSourceTable,
            TOO_SelectDestinationTable,
            TOO_NoDataToImport,

            #endregion

            #region out/in patientdept

            OUTIN_PatientNoDoesNotExist,
            OUTIN_PatientIsNotBooked,
            OUTIN_PatientIsNotAdmitted,
            OUTIN_PatientNoIsOutOfRange,
            OUTIN_PatientNoIsInUse,
            OUTIN_PatientHasNotBeenDischarged,
            OUTIN_NoServiceMembershipTerminated,
            OUTIN_NoServiceMemberIsNotActivated,
            OUTIN_NoServiceInvalidGroupOrMembershipId,
            OUTIN_PatientNoIsInvalid,
            OUTIN_SurnameIsInvalid,
            OUTIN_FirstNameIsInvalid,
            OUTIN_BirthDateIsInvalid,
            OUTIN_AgeIsInvalid,
            OUTIN_GenderIsInvalid,
            OUTIN_CustomerGroupIsInvalid,
            OUTIN_CustomerSubGroupIsInvalid,
            OUTIN_OnlyRegisteredCanBeBooked,
            OUTIN_PatientAlreadyExistAsAdmitted,
            OUTIN_WardCodeIsInUse,
            OUTIN_WardCodeDoesNotExist,
            OUTIN_WardCodeIsInvalid,
            OUTIN_WardDescriptionIsInvalid,
            OUTIN_WardRoomCodeIsInUse,
            OUTIN_WardRoomCodeDoesNotExist,
            OUTIN_WardRoomCodeIsInvalid,
            OUTIN_WardRoomDescriptionIsInvalid,
            OUTIN_WardRoomBedCodeIsInUse,
            OUTIN_WardRoomBedCodeDoesNotExist,
            OUTIN_WardRoomBedCodeIsInvalid,
            OUTIN_WardRoomBedDescriptionIsInvalid,
            OUTIN_DischargeStatusCodeIsInUse,
            OUTIN_DischargeStatusCodeDoesNotExist,
            OUTIN_DischargeStatusCodeIsInvalid,
            OUTIN_DischargeStatusDescriptionIsInvalid,
            OUTIN_TreatmentPointCodeIsInUse,
            OUTIN_TreatmentPointCodeDoesNotExist,
            OUTIN_TreatmentPointCodeIsInvalid,
            OUTIN_TreatmentPointDescriptionIsInvalid,
            OUTIN_OnlyRegisteredCanBeAdmitted,
            OUTIN_PatientDetailsUpdated,
            OUTIN_TickDepartments,
            OUTIN_TickAttendanceTypes,
            OUTIN_BillTypeIsInvalid,
            OUTIN_BookingSuccess,
            OUTIN_TransferSuccess,
            OUTIN_AdmissionSuccess,
            OUTIN_DischargeSuccess,
            OUTIN_SourceAndDestinationWardRoomBedNotAllowed,

            #endregion

            #region Security Setup

            SEC_UserIdIsInUse,
            SEC_UserIdDoesNotExist,
            SEC_UserIdIsInvalid,
            SEC_UserFullNameIsInvalid,
            SEC_UnsupportedCharactersForUserId,

            SEC_PasswordIsInvalid,
            SEC_PasswordLettersOrNumbersOnly,
            SEC_PasswordConfirmedDoNotMatch,
            SEC_PasswordChanged,

            SEC_UserGroupCodeIsInUse,
            SEC_UserGroupCodeDoesNotExist,
            SEC_UserGroupCodeIsInvalid,
            SEC_UserGroupDescriptionIsInvalid,

            #endregion

            #region billing

            BIL_DepositAccountCodeIsInUse,
            BIL_DepositAccountCodeDoesNotExist,
            BIL_DepositAccountCodeIsInvalid,
            BIL_DepositAccountDescriptionIsInvalid,
            BIL_DepositTransactionIdIsInvalid,
            BIL_DuplicateDepositNoDetected,
            BIL_FromToWhonIsInvalid,
            BIL_MemoIsInvalid,
            BIL_AccountIsMarkedInactive,
            BIL_DestinationAccountIsMarkedInactive,
            BIL_DestinationAccountDoesNotExist,
            BIL_DestinationAccountIsInvalid,
            BIL_AccountDoesNotAllowOverDraft,
            BIL_ConfirmAccountOverDraft,
            BIL_DepositAccountMemberIsInvalid,
            BIL_DepositAccountMemberDoesNotExist,

            BIL_BillingItemGroupIsInvalid,
            BIL_BillingItemSubGroupIsInvalid,
            BIL_BillingItemIsInvalid,
            BIL_QuantityIsInvalid,
            BIL_UnitPriceIsInvalid,
            BIL_AmountIsInvalid,
            BIL_BillingItemsToChargeAreMissing,
            BIL_CurrencyIsInvalid,
            BIL_ExchangeRateIsInvalid,
            BIL_PaymentTypeIsInvalid,
            BIL_PaidAmountIsInvalid,
            BIL_PaymentTypeDoesNotAllowOverPaying,
            BIL_PaymentIsInvalidOrNoPaymentHasBeenDone,
            BIL_PaidAmountIsLessThanAmountDue,
            BIL_CreateInvoiceConfirmQuestion,
            BIL_DuplicateReceiptNoDetected,
            BIL_DuplicateInvoiceNoDetected,
            BIL_DuplicateVoidNoDetected,
            BIL_DuplicateRefundNoDetected,
            BIL_ReceiptNoIsInvalid,
            BIL_InvoiceNoIsInvalid,
            BIL_TickInvoiceStatus,
            BIL_PriceCategoryIsInvalid,
            BIL_ReceiptCannotBeVoided,
            BIL_InvoiceCannotBeVoided,
            BIL_ConfirmVoidSale,
            BIL_SaleVoidingComplete,
            BIL_PaymentRefundComplete,
            BIL_PaymentTypeDoesNotAllowRefund,

            BIL_ItemIsNotApplicableToCurrentGroup,
            BIL_RefundAmountIsInvalid,
            BIL_ConfirmRefund,
            BIL_ConfirmIncomingBillProcess,

            #endregion

            #region diagnosesandtreatments

            DXT_DoctorIsInvalid,
            DXT_DiagnosisStatusIsInvalid,
            DXT_DiagnosisCodeIsInvalid,

            DXT_DxGroupCodeIsInUse,
            DXT_DxGroupCodeDoesNotExist,
            DXT_DxGroupCodeIsInvalid,
            DXT_DxGroupDescriptionIsInvalid,

            DXT_DxCodeIsInUse,
            DXT_DxCodeDoesNotExist,
            DXT_DxCodeIsInvalid,
            DXT_DxDescriptionIsInvalid,
            DXT_DxIsMissing,

            DXT_DxIndicatorGroupCodeIsInUse,
            DXT_DxIndicatorGroupCodeDoesNotExist,
            DXT_DxIndicatorGroupCodeIsInvalid,
            DXT_DxIndicatorGroupDescriptionIsInvalid,

            DXT_DxIndicatorCodeIsInUse,
            DXT_DxIndicatorCodeDoesNotExist,
            DXT_DxIndicatorCodeIsInvalid,
            DXT_DxIndicatorDescriptionIsInvalid,

            DXT_DiagnosisIsNotApplicableToCurrentGroup,

            #endregion

            #region customers

            CUS_CustomerGroupCodeIsInUse,
            CUS_CustomerGroupCodeDoesNotExist,
            CUS_CustomerGroupCodeIsInvalid,
            CUS_CustomerSubGroupCodeIsInUse,
            CUS_CustomerSubGroupCodeDoesNotExist,
            CUS_CustomerSubGroupCodeIsInvalid,
            CUS_MemberCodeIsInUse,
            CUS_MemberCodeDoesNotExist,
            CUS_MemberCodeIsInvalid,
            CUS_MembershipIdIsInUse,
            CUS_MembershipIdDoesNotExist,
            CUS_MemberActivationSuccess,
            CUS_MemberDeActivationSuccess,
            CUS_MembershipIdIsInvalid,

            #endregion

            #region reportdesigner

            RPD_ReportGroupCodeIsInUse,
            RPD_ReportGroupCodeDoesNotExist,
            RPD_ReportGroupCodeIsInvalid,
            RPD_ReportGroupDescriptionIsInvalid,

            RPD_ReportCodeIsInUse,
            RPD_ReportCodeDoesNotExist,
            RPD_ReportCodeIsInvalid,
            RPD_ReportDescriptionIsInvalid,

            RPD_ViewNotSelected,
            RPD_ParameterCodeIsInUse,
            RPD_ParameterCodeIsInvalid,
            RPD_ParameterDescriptionIsInvalid,
            RPD_ParameterTypeIsInvalid,
            RPD_ParameterInputControlIsInvalid,

            RPD_LookupTableNameIsInvalid,
            RPD_LookupFieldNameIsInvalid,

            #endregion

            #region generalsetup

            GEN_FacilityNameIsInvalid,

            GEN_IdSeedIsInvalid,
            GEN_IdIncrementIsInvalid,
            GEN_IdLengthIsInvalid,
            GEN_IdCurrentIsInvalid,

            GEN_CountryCodeIsInUse,
            GEN_CountryCodeDoesNotExist,
            GEN_CountryCodeIsInvalid,
            GEN_CountryDescriptionIsInvalid,

            GEN_RegionCodeIsInUse,
            GEN_RegionCodeDoesNotExist,
            GEN_RegionCodeIsInvalid,
            GEN_RegionDescriptionIsInvalid,

            GEN_DistrictCodeIsInUse,
            GEN_DistrictCodeDoesNotExist,
            GEN_DistrictCodeIsInvalid,
            GEN_DistrictDescriptionIsInvalid,

            GEN_LayoutTemplateNameIsInvalid,

            GEN_PatientDocumentCodeIsInUse,
            GEN_PatientDocumentCodeDoesNotExist,
            GEN_PatientDocumentCodeIsInvalid,
            GEN_PatientDocumentDescriptionIsInvalid,

            GEN_PatientFieldNameIsInUse,
            GEN_PatientFieldNameDoesNotExist,
            GEN_PatientFieldNameIsInvalid,
            GEN_PatientFieldCaptionIsInvalid,
            GEN_PatientFieldNameExistInPatients,

            #endregion

            #region inventorysetup

            IVS_StoreCodeIsInUse,
            IVS_StoreCodeDoesNotExist,
            IVS_StoreCodeIsInvalid,
            IVS_StoreDescriptionIsInvalid,
            IVS_StoreCodeSameAsSupplierCode,

            IVS_ProductCategoryCodeIsInUse,
            IVS_ProductCategoryDoesNotExist,
            IVS_ProductCategoryCodeIsInvalid,
            IVS_ProductCategoryDescriptionIsInvalid,

            IVS_PackagingCodeIsInUse,
            IVS_PackagingDoesNotExist,
            IVS_PackagingCodeIsInvalid,
            IVS_PackagingDescriptionIsInvalid,

            IVS_SupplierCodeIsInUse,
            IVS_SupplierCodeDoesNotExist,
            IVS_SupplierCodeIsInvalid,
            IVS_SupplierDescriptionIsInvalid,
            IVS_SupplierCodeSameAsStoreCode,

            IVS_CustomerCodeIsInUse,
            IVS_CustomerDoesNotExist,
            IVS_CustomerCodeIsInvalid,
            IVS_CustomerDescriptionIsInvalid,

            IVS_ProductCodeIsInUse,
            IVS_ProductDoesNotExist,
            IVS_ProductCodeIsInvalid,
            IVS_ProductDescriptionIsInvalid,

            #endregion

            #region inventory

            IV_DuplicateOrderNoDetected,
            IV_ClosedOrderCannotbeEdited,
            IV_ClosedOrderCannotbeReceived,
            IV_OrderNoDoesNotExist,
            IV_OrderNoIsInvalid,
            IV_OrderNoIsInUse,
            IV_OrderSaved,
            IV_SelecteOrderTypePrompt,

            IV_OrderReceivingExpirySkipped,
            IV_OrderConfirmAddItemToOrder,
            IV_OrderQtyIsInvalid,
            IV_OrderReceivedQtyIsInvalid,
            IV_CloseOrderConfirmation,

            IV_DuplicateTransferNoDetected,
            IV_ClosedTransferCannotbeEdited,
            IV_ClosedTransferCannotbeReceived,
            IV_TransferNoDoesNotExist,
            IV_TransferNoIsInvalid,
            IV_TransferNoIsInUse,
            IV_TransferSaved,
            IV_CloseTransferConfirmation,

            IV_DuplicateStockTakingNoDetected,
            IV_ClosedStockTakingCannotbeEdited,
            IV_StockTakingNoDoesNotExist,
            IV_StockTakingNoIsInvalid,
            IV_StockTakingNoIsInUse,
            IV_StockTakingSaved,

            IV_StockTakingConfirmCalculate,
            IV_StockTakingConfirmCommiting,

            IV_DeliveryNoIsInvalid,

            IV_DeliveryNoIsInUseForAnotherSource,
            IV_DeliveryNoIsInUseForAnotherOrder,

            #endregion

            #region billingsetup

            BLS_CurrencyCodeIsInUse,
            BLS_CurrencyCodeDoesNotExist,
            BLS_CurrencyCodeIsInvalid,
            BLS_CurrencyDescriptionIsInvalid,
            BLS_LocalCurrencyCannotBeDeleted,

            BLS_PaymentTypeCodeIsInUse,
            BLS_PaymentTypeCodeDoesNotExist,
            BLS_PaymentTypeCodeIsInvalid,
            BLS_PaymentTypeDescriptionIsInvalid,

            BLS_PriceCategoryDescriptionIsInvalid,

            BLS_BillingItemGroupCodeIsInUse,
            BLS_BillingItemGroupCodeDoesNotExist,
            BLS_BillingItemGroupCodeIsInvalid,
            BLS_BillingItemGroupDescriptionIsInvalid,

            BLS_BillingItemSubGroupCodeIsInUse,
            BLS_BillingItemSubGroupCodeDoesNotExist,
            BLS_BillingItemSubGroupCodeIsInvalid,
            BLS_BillingItemSubGroupDescriptionIsInvalid,

            BLS_BillingItemCodeIsInUse,
            BLS_BillingItemCodeDoesNotExist,
            BLS_BillingItemCodeIsInvalid,
            BLS_BillingItemDescriptionIsInvalid,

            #endregion

            #region MTUHA

            MTU_DiagnosisCodeIsInUse,
            MTU_DiagnosisCodeDoesNotExist,
            MTU_DiagnosisCodeIsInvalid,
            MTU_DiagnosisDescriptionIsInvalid,

            #endregion

            #region laboratary

            LAB_LaboratoryCodeIsInUse,
            LAB_LaboratoryCodeDoesNotExist,
            LAB_LaboratoryCodeIsInvalid,
            LAB_LaboratorysDescriptionIsInvalid,

            LAB_LabTestGroupCodeIsInUse,
            LAB_LabTestGroupCodeDoesNotExist,
            LAB_LabTestGroupCodeIsInvalid,
            LAB_LabTestGroupDescriptionIsInvalid,

            LAB_LabTestSubGroupCodeIsInUse,
            LAB_LabTestSubGroupCodeDoesNotExist,
            LAB_LabTestSubGroupCodeIsInvalid,
            LAB_LabTestSubGroupDescriptionIsInvalid,

            LAB_LabTestCodeIsInUse,
            LAB_LabTestCodeDoesNotExist,
            LAB_LabTestCodeIsInvalid,
            LAB_LabTestDescriptionIsInvalid,

            LAB_LabTechnicianIsInvalid,
            LAB_ClinicalOfficerIsInvalid,
            LAB_PatientLabTestsSaved,

            #endregion

            #region rch

            RCH_ClientCodeIsInUse,
            RCH_ClientCodeDoesNotExist,
            RCH_ClientCodeIsInvalid,
            RCH_SurnameIsInvalid,
            RCH_FirstNameIsInvalid,
            RCH_BirthDateIsInvalid,
            RCH_AgeIsInvalid,
            RCH_GenderIsInvalid,
            ANC_Registration_Number_In_Use,

            #endregion

            #region ctc

            CTC_ClientCodeIsInUse,
            CTC_ClientCodeDoesNotExist,
            CTC_ClientCodeIsInvalid,
            CTC_SurnameIsInvalid,
            CTC_FirstNameIsInvalid,
            CTC_BirthDateIsInvalid,
            CTC_AgeIsInvalid,
            CTC_GenderIsInvalid,
            CTC_HIVNoIsInUse,
            CTC_HIVNoIsInvalid,
            CTC_CTCNoIsInUse,
            CTC_CTCNoIsInvalid,

            CTC_CD4SampleIdIsInUse,
            CTC_CD4SampleIdIsInvalid,
            CTC_CD4TestResultIsInvalid,

            CTC_CTCMustHaveHIVNo,
            CTC_CTCMustHaveTestedPositive,

            #endregion
        }
    }
}
