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
    public class clsSystemFunctions
    {
        public enum FunctionKeys
        {
            #region generalsetup

            genfacilitysetup_edit,

            genautocodes_edit,

            gencountries_add,
            gencountries_edit,
            gencountries_delete,

            genregions_add,
            genregions_edit,
            genregions_delete,

            gendistricts_add,
            gendistricts_edit,
            gendistricts_delete,

            genmedicalstaffs_add,
            genmedicalstaffs_edit,
            genmedicalstaffs_delete,

            gencontrolinformation_add,
            gencontrolinformation_edit,
            gencontrolinformation_delete,

            gentreatmentpoints_add,
            gentreatmentpoints_edit,
            gentreatmentpoints_delete,

            gensearchengine_edit,

            genprinterssetup_edit,

            genpatientdocumentssetup_add,
            genpatientdocumentssetup_edit,
            genpatientdocumentssetup_delete,
            genpatientdocumentssetup_design,

            genpatientextrafields_add,
            genpatientextrafields_edit,
            genpatientextrafields_delete,
            genpatientextrafields_design,

            #endregion

            #region outpatientdept

            opdregistrations_changecustomergroup,
            opdregistrations_changecustomersubgroup,
            opdregistrations_promptforbilling,
            opdregistrations_changechargestatus,
            opdregistrations_customizelayout,
            opdregistrations_customizebookingform,
            opdregistrations_showsuccessmessage,
            opdregistrations_closeafterregistration,

            opdeditpatientdetails_customizelayout,

            opdpatientdocuments_customizelayout,

            #endregion

            #region inpatientdept

            ipdregistrations_changecustomergroup,
            ipdregistrations_changecustomersubgroup,
            ipdregistrations_promptforbilling,
            ipdregistrations_changechargestatus,
            ipdregistrations_promptfordiagnosis,
            ipdregistrations_changepromptfordiagnosis,
            ipdregistrations_customizelayout,
            ipdregistrations_customizeadmissionform,
            ipdregistrations_showsuccessmessage,
            ipdregistrations_closeafterregistration,

            ipdwards_add,
            ipdwards_edit,
            ipdwards_delete,

            ipdwardrooms_add,
            ipdwardrooms_edit,
            ipdwardrooms_delete,

            ipdwardroombeds_add,
            ipdwardroombeds_edit,
            ipdwardroombeds_delete,

            ipddischargestatus_add,
            ipddischargestatus_edit,
            ipddischargestatus_delete,

            ipdtransfers_customizelayout,
            ipdtransfers_showsuccessmessage,
            ipdtransfers_closeaftertransfer,

            ipddischarges_promptforbilling,
            ipddischarges_changechargestatus,
            ipddischarges_promptfordiagnosis,
            ipddischarges_changepromptfordiagnosis,
            ipddischarges_customizelayout,
            ipddischarges_showsuccessmessage,
            ipddischarges_closeafterdischarge,

            #endregion

            #region diagnosesandtreatments

            dxtgroups_add,
            dxtgroups_edit,
            dxtgroups_delete,

            dxtsubgroups_add,
            dxtsubgroups_edit,
            dxtsubgroups_delete,

            dxtdiagnoses_add,
            dxtdiagnoses_edit,
            dxtdiagnoses_delete,

            dxtmapping_add,
            dxtmapping_edit,
            dxtmapping_delete,

            dxtindicatorgroups_add,
            dxtindicatorgroups_edit,
            dxtindicatorgroups_delete,

            dxtindicators_add,
            dxtindicators_edit,
            dxtindicators_delete,

            dxtpatientdiagnoses_add,
            dxtpatientdiagnoses_edit,
            dxtpatientdiagnoses_delete,
            dxtpatientdiagnoses_prescribe,
            dxtpatientdiagnoses_opdoneentryonly,
            dxtpatientdiagnoses_ipdadmissiononeentryonly,
            dxtpatientdiagnoses_ipddiachargeoneentryonly,
            dxtpatientdiagnoses_opdadditemtocart,
            dxtpatientdiagnoses_ipdadmissionadditemtocart,
            dxtpatientdiagnoses_ipddischargeadditemtocart,
            dxtpatientdiagnoses_opdshowpatienthistory,
            dxtpatientdiagnoses_ipdadmissionshowpatienthistory,
            dxtpatientdiagnoses_ipddischargeshowpatienthistory,
            dxtpatientdiagnoses_customizelayout,

            dxtlisting_delete,

            #endregion

            #region billing

            bildepositaccounts_add,
            bildepositaccounts_edit,
            bildepositaccounts_delete,
            bildepositaccounts_transact,

            bilpostbills_changepricecategory,
            bilpostbills_changeprice,
            bilpostbills_changepaymenttype,
            bilpostbills_acceptpartialpayment,
            bilpostbills_customizelayout,
            bilpostbills_customizefinalpayment,
            bilpostbills_affectbillsdirect,
            bilpostbills_incomingbilladd,
            bilpostbills_incomingbilldelete,
            bilpostbills_billunbooked,

            bilpaybillspatients_customizelayout,
            bilpaybillsgroups_customizelayout,

            billsaleshistorycash_designreceipt,
            billsaleshistorycash_voidreceipt,

            billsaleshistoryinvoices_designinvoice,
            billsaleshistoryinvoices_voidinvoice,

            billpaymentshistory_designreceipt,
            billpaymentshistory_voidreceipt,

            billvoidedsaleshistory_designreceipt,
            billvoidedsaleshistory_designrefund,
            billvoidedsaleshistory_refund,

            #endregion

            #region MTUHA

            mtuhadiagnoses_add,
            mtuhadiagnoses_edit,
            mtuhadiagnoses_delete,

            #endregion

            #region billingsetup

            blscurrencies_add,
            blscurrencies_edit,
            blscurrencies_delete,

            blspaymenttypes_add,
            blspaymenttypes_edit,
            blspaymenttypes_delete,

            blspricecategories_edit,

            blsitemgroups_add,
            blsitemgroups_edit,
            blsitemgroups_delete,

            blsitemsubgroups_add,
            blsitemsubgroups_edit,
            blsitemsubgroups_delete,

            blsitems_add,
            blsitems_edit,
            blsitems_delete,
            blsitems_customizelayout,

            #endregion

            #region inventorysetup

            ivsstores_add,
            ivsstores_edit,
            ivsstores_delete,
            ivsstores_switchstore,
            
            ivsautocodes_edit,

            ivsproductcategories_add,
            ivsproductcategories_edit,
            ivsproductcategories_delete,

            ivspackagings_add,
            ivspackagings_edit,
            ivspackagings_delete,

            ivssourcesofstock_add,
            ivssourcesofstock_edit,
            ivssourcesofstock_delete,

            ivscustomers_add,
            ivscustomers_edit,
            ivscustomers_delete,

            ivsproducts_add,
            ivsproducts_edit,
            ivsproducts_delete,

            #endregion

            #region inventory

            ivorders_add,
            ivorders_edit,
            ivorders_receive,
            ivorders_close,
            ivorders_open,
            ivorders_design,

            ivtransferins_add,
            ivtransferins_edit,
            ivtransferins_receive,
            ivtransferins_close,
            ivtransferins_open,
            ivtransferins_design,

            ivtransferouts_add,
            ivtransferouts_edit,
            ivtransferouts_receive,
            ivtransferouts_close,
            ivtransferouts_open,
            ivtransferouts_design,

            ivphysicalinventory_count,
            ivphysicalinventory_edit,
            ivphysicalinventory_calculate,
            ivphysicalinventory_commit,
            ivphysicalinventory_design,

            ivissueshistory_design,

            ivreceiptshistory_design,

            #endregion

            #region securitysetup

            secusergroups_add,
            secusergroups_edit,
            secusergroups_delete,

            secgroupsaccess_edit,

            secreportsaccess_edit,

            secusers_add,
            secusers_edit,
            secusers_delete,

            #endregion

            #region reportdesigner

            rpdreportgroups_add,
            rpdreportgroups_edit,
            rpdreportgroups_delete,

            rpdreports_add,
            rpdreports_edit,
            rpdreports_delete,

            #endregion

            #region rch

            rchfplanmethods_add,
            rchfplanmethods_edit,
            rchfplanmethods_delete,
            rchfplanmethods_customizelayout,

            rchdangerindicators_add,
            rchdangerindicators_edit,
            rchdangerindicators_delete,
            rchdangerindicators_customizelayout,

            rchbirthmethods_add,
            rchbirthmethods_edit,
            rchbirthmethods_delete,
            rchbirthmethods_customizelayout,

            rchbirthcomplications_add,
            rchbirthcomplications_edit,
            rchbirthcomplications_delete,
            rchbirthcomplications_customizelayout,

            rchvaccines_add,
            rchvaccines_edit,
            rchvaccines_delete,
            rchvaccines_customizelayout,

            rchclients_add,
            rchclients_edit,
            rchclients_customizelayout,

            rchfamilyplanning_add,
            rchfamilyplanning_edit,
            rchfamilyplanning_delete,
            rchfamilyplanning_customizelayout,

            rchantenatal_add,
            rchantenatal_edit,
            rchantenatal_delete,
            rchantenatal_customizelayout,

            rchpostnatal_add,
            rchpostnatal_edit,
            rchpostnatal_delete,
            rchpostnatal_customizelayout,

            rchchildren_add,
            rchchildren_edit,
            rchchildren_delete,
            rchchildren_customizelayout,

            rchvaccinations_add,
            rchvaccinations_edit,
            rchvaccinations_delete,
            rchvaccinations_customizelayout,

            rchmaternity_add,
            rchmaternity_edit,
            rchmaternity_delete,
            rchmaternity_customizelayout,

            #endregion

            #region lab

            lab_add,
            lab_edit,
            lab_delete,

            labtestgroup_add,
            labtestgroup_edit,
            labtestgroup_delete,

            labtestsubgroup_add,
            labtestsubgroup_edit,
            labtestsubgroup_delete,

            labtest_add,
            labtest_edit,
            labtest_delete,

            labremarks_add,
            labremarks_delete,

            labpatienttests_add,
            labpatienttests_delete,
            labpatienttests_customizelayout,

            #endregion

            #region ctc

            ctcclients_add,
            ctcclients_edit,
            ctcclients_viewhivtests,
            ctcclients_viewcd4tests,
            ctcclients_viewart,
            ctcclients_editart,
            ctcclients_customizelayout,

            ctcmeasurements_add,
            ctcmeasurements_edit,
            ctcmeasurements_delete,
            ctcmeasurements_customizelayout,

            ctchivtests_add,
            ctchivtests_edit,
            ctchivtests_delete,
            ctchivtests_customizelayout,

            ctcpcrtests_add,
            ctcpcrtests_edit,
            ctcpcrtests_delete,
            ctcpcrtests_customizelayout,

            ctccd4tests_add,
            ctccd4tests_edit,
            ctccd4tests_delete,
            ctccd4tests_customizelayout,

            ctcartvisits_add,
            ctcartvisits_edit,
            ctcartvisits_delete,
            ctcartvisits_customizelayout,

            ctcpreart_enroll,
            ctcpreart_add,
            ctcpreart_edit,
            ctcpreart_delete,
            ctcpreart_customizelayout,

            ctcartt_enroll,
            ctcartt_add,
            ctcartt_edit,
            ctcartt_delete,
            ctcartt_newvisit,
            ctcartt_history,
            ctcartt_customizelayout,

            ctcart_enroll,
            ctcart_add,
            ctcart_edit,
            ctcart_delete,
            ctcart_customizelayout,

            ctcartp_enroll,
            ctcartp_add,
            ctcartp_edit,
            ctcartp_delete,
            ctcartp_customizelayout,

            ctcexposed_enroll,
            ctcexposed_add,
            ctcexposed_edit,
            ctcexposed_delete,
            ctcexposed_customizelayout,

            ctc_supportivecounselling,

            //ctcpmtctantenatal_enroll,
            //ctcpmtctantenatal_add,
            //ctcpmtctantenatal_edit,
            //ctcpmtctantenatal_delete,
            //ctcpmtctantenatal_customizelayout,

            //ctcpmtctpostnatal_enroll,
            //ctcpmtctpostnatal_add,
            //ctcpmtctpostnatal_edit,
            //ctcpmtctpostnatal_delete,
            //ctcpmtctpostnatal_customizelayout,

            ctcpmtctanc_enroll,
            ctcpmtctanc_add,
            ctcpmtctanc_edit,
            ctcpmtctanc_delete,
            ctcpmtctanc_customizelayout,

            ctcpmtctdelivery_enroll,
            ctcpmtctdelivery_add,
            ctcpmtctdelivery_edit,
            ctcpmtctdelivery_delete,
            ctcpmtctdelivery_customizelayout,

            ctcpmtctmotherchild_enroll,
            ctcpmtctmotherchild_add,
            ctcpmtctmotherchild_edit,
            ctcpmtctmotherchild_delete,
            ctcpmtctmotherchild_customizelayout,

            #endregion

            #region sms

            smsreceivedmessages_View_ReceivedMessages,
           
            smsreceivedmessages_customizelayout,

            //ctcmeasurements_add,
            //ctcmeasurements_edit,
            //ctcmeasurements_delete,
            //ctcmeasurements_customizelayout,

            //ctchivtests_add,
            //ctchivtests_edit,
            //ctchivtests_delete,
            //ctchivtests_customizelayout,

           
           

            #endregion
        }
    }
}
