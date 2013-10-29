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
/*View view_billinvoicedoc*/ 
DROP VIEW IF EXISTS view_billinvoicedoc;
DROP TABLE IF EXISTS view_billinvoicedoc;
CREATE
    VIEW view_billinvoicedoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription 
    , bi.transdate
    , bi.voidtransdate
    , bi.invoiceno
    , bi.voidno
    , bi.totaldue
    , bi.paidforinvoice
    , bi.patientcode
    , p.surname patientsurname
    , p.firstname patientfirstname
    , p.othernames patientothernames
    , CONCAT(p.firstname,' ',p.othernames,' ',p.surname) patientfullname
    , p.gender patientgender
    , p.birthdate patientbirthdate
    , DATEDIFF(bi.transdate, p.birthdate)/365.25 patientage
    , bi.currencycode
    , bi.currencydescription
    , bi.currencysymbol    
    , bi.billinggroupcode
    , bi.billinggroupdescription
    , bi.billingsubgroupcode
    , bi.billingsubgroupdescription
    , bi.billinggroupmembershipno
    , bi.userid    
    , bi.voiduserid
    , bii.itemgroupcode
    , bii.itemgroupdescription
    , bii.itemsubgroupcode
    , bii.itemsubgroupdescription
    , bii.itemcode
    , bii.itemdescription
    , bii.expirydate
    , bii.qty
    , bii.amount/bii.qty unitprice
    , bii.amount
    , bii.transtype
FROM  
    facilityoptions fo
    , billinvoices bi
    INNER JOIN billinvoiceitems bii ON (bi.invoiceno = bii.invoiceno)
	LEFT OUTER JOIN patients p ON (bi.patientcode = p.code);
        
/*View view_billreceiptdoc*/
DROP VIEW IF EXISTS view_billreceiptdoc;
DROP TABLE IF EXISTS view_billreceiptdoc;
CREATE
    VIEW view_billreceiptdoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , br.transdate
    , br.voidtransdate
    , br.receiptno
    , br.voidno
    , br.patientcode
    , p.surname patientsurname
    , p.firstname patientfirstname
    , p.othernames patientothernames
    , CONCAT(p.firstname,' ',p.othernames,' ',p.surname) patientfullname
    , p.gender patientgender
    , p.birthdate patientbirthdate
	, DATEDIFF(br.transdate, p.birthdate)/365.25 patientage
    , br.totaldue
    , br.discount
    , br.totalpaid
    , br.changeamount
    , br.currencycode
    , br.currencydescription
    , br.currencysymbol
    , br.billinggroupcode
    , br.billinggroupdescription
    , br.billingsubgroupcode
    , br.billingsubgroupdescription
    , br.billinggroupmembershipno
    , br.userid 
    , br.voiduserid
    , bri.itemgroupcode
    , bri.itemgroupdescription
    , bri.itemsubgroupcode
    , bri.itemsubgroupdescription
    , bri.itemcode
    , bri.itemdescription
    , bri.expirydate
    , bri.qty
    , bri.amount/bri.qty unitprice
    , bri.amount
    , bri.transtype
FROM
    facilityoptions fo
    , billreceipts br
    INNER JOIN billreceiptitems bri ON (br.receiptno = bri.receiptno)
    LEFT OUTER JOIN patients p ON (br.patientcode = p.code);  
        
/*View view_billinvoicepaymentdoc*/
DROP VIEW IF EXISTS view_billinvoicepaymentdoc;
DROP TABLE IF EXISTS view_billinvoicepaymentdoc;
CREATE
    VIEW view_billinvoicepaymentdoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , bip.transdate
    , bip.receiptno
    , bip.patientcode
    , p.surname patientsurname
    , p.firstname patientfirstname
    , p.othernames patientothernames
    , CONCAT(p.firstname,' ',p.othernames,' ',p.surname) patientfullname
    , p.gender patientgender
    , p.birthdate patientbirthdate
	, DATEDIFF(bip.transdate, p.birthdate)/365.25 patientage
    , bip.totaldue
    , bip.totalpaid
    , bip.currencycode
    , bip.currencydescription
    , bip.currencysymbol
    , bip.billinggroupcode
    , bip.billinggroupdescription
    , bip.userid    
    , bipd.invoiceno
    , bipd.totaldue dueforinvoice
    , bipd.totalpaid paidforinvoice
FROM
    facilityoptions fo
    , billinvoicepayments bip
    INNER JOIN billinvoicepaymentdetails bipd ON (bip.receiptno = bipd.receiptno)
    LEFT OUTER JOIN patients p ON (bip.patientcode = p.code);                       

/*View view_labpatienttestscount*/
DROP VIEW IF EXISTS view_labpatienttestscount;
DROP TABLE IF EXISTS view_labpatienttestscount;
CREATE 
	VIEW view_labpatienttestscount
	AS    
SELECT
	pt.department
	,pt.transdate
	,pt.laboratorycode
	,pt.labtechniciancode
	,pt.labtestgroupcode
	,pt.labtesttypecode
	,SUM(CASE WHEN p.gender='m' THEN 1 ELSE 0 END) males
	,SUM(CASE WHEN p.gender='f' THEN 1 ELSE 0 END) females
FROM 
	labpatienttests pt
	LEFT OUTER JOIN patients p ON (pt.patientcode = p.code)
GROUP BY
	pt.department
	,pt.transdate
	,pt.laboratorycode
	,pt.labtechniciancode
	,pt.labtestgroupcode
	,pt.labtesttypecode;

/*View view_regbookinggendercounter*/
DROP VIEW IF EXISTS view_regbookinggendercounter;
DROP TABLE IF EXISTS view_regbookinggendercounter;
CREATE
    VIEW view_regbookinggendercounter 
    AS     
SELECT 
	fb.yearpart
	, fb.monthpart
	, fb.bookdate
	, fb.refered
	, fb.referedfacility
	, fb.department
	, fb.wheretakencode
	, fb.wheretaken
	, fb.billinggroupcode
	, fb.billinggroup
	, fb.billingsubgroupcode
	, fb.billingsubgroup
	, fb.patientweight weight
	, fb.patienttemperature temperature
	, SUM(CASE WHEN p.gender='M' AND fb.registrystatus='new' THEN 1 ELSE 0 END) new_males
	, SUM(CASE WHEN p.gender='M' AND fb.registrystatus='re_visiting' THEN 1 ELSE 0 END) reattendance_males
	, SUM(CASE WHEN p.gender='F' AND fb.registrystatus='new' THEN 1 ELSE 0 END) new_females
	, SUM(CASE WHEN p.gender='F' AND fb.registrystatus='re_visiting' THEN 1 ELSE 0 END) reattendance_females
FROM 
	facilitybookinglog fb 
	LEFT OUTER JOIN patients p ON (fb.patientcode = p.code)
GROUP BY 
	fb.yearpart
	, fb.monthpart
	, fb.bookdate
	, fb.refered
	, fb.referedfacility
	, fb.department
	, fb.wheretakencode
	, fb.wheretaken
	, fb.billinggroupcode
	, fb.billinggroup
	, fb.billingsubgroupcode
	, fb.billingsubgroup
	, fb.patientweight
	, fb.patienttemperature;

/*View view_billsalesdetails*/
DROP VIEW IF EXISTS view_billsalesdetails;
DROP TABLE IF EXISTS view_billsalesdetails;
CREATE
    VIEW view_billsalesdetails 
    AS  	
SELECT
    bsi.transdate
    , bsi.receiptno
    , bsi.invoiceno
    , bsi.reference
    , bsi.billinggroupcode
    , bsi.billinggroupdescription
    , bsi.billingsubgroupcode
    , bsi.billingsubgroupdescription
    , bsi.itemgroupcode
    , bsi.itemgroupdescription
    , bsi.itemsubgroupcode
    , bsi.itemsubgroupdescription
    , bsi.itemcode
    , bsi.itemdescription
    , bsi.expirydate
    , bsi.qty
    , bsi.amount/bsi.qty unitprice
    , bsi.amount
    , bsi.userid
FROM
    billsalesitems bsi;	
        
/*View view_billinvoicesdetails*/
DROP VIEW IF EXISTS view_billinvoicesdetails;
DROP TABLE IF EXISTS view_billinvoicesdetails;
CREATE
    VIEW view_billinvoicesdetails 
    AS  	
SELECT
    bi.patientcode
    , p.surname patientsurname
    , p.firstname patientfirstname
    , p.othernames patientothernames
    , p.gender patientgender
    , p.birthdate patientbirthdate
    , bi.currencycode
    , bi.currencydescription
    , bi.currencysymbol
    , bi.billinggroupcode
    , bi.billinggroupdescription
    , bi.billingsubgroupcode
    , bi.billingsubgroupdescription
    , bi.billinggroupmembershipno
    , bi.userid
    , bi.transdate
    , bi.invoiceno
    , bi.totaldue
    , bi.totalpaid
    , bi.balancedue
    , bi.paidforinvoice
FROM
    billinvoices bi
    LEFT OUTER JOIN patients p ON (bi.patientcode = p.code);	   
    
/*View view_dxtpatientdiagnoseslog*/
DROP VIEW IF EXISTS view_dxtpatientdiagnoseslog;
DROP TABLE IF EXISTS view_dxtpatientdiagnoseslog;
CREATE
    VIEW view_dxtpatientdiagnoseslog
    AS     
SELECT
    dx.transdate
    , dx.booking
    , dx.episodecode
    , dx.patientcode
    , dx.diagnosiscode
    , dx.doctorcode
    , dx.treatments
    , dx.referaldescription
    , dx.department
    , dx.userid
    , p.surname patientsurname
    , p.firstname patientfirstname
    , p.othernames patientothernames
    , CONCAT(p.firstname,' ',p.othernames,' ',p.surname) patientfullname
    , p.gender patientgender
    , p.birthdate patientbirthdate
    , p.regdate patientregdate
    , dx.patientweight
    , dx.patienttemperature
	, DATEDIFF(dx.transdate, p.birthdate)/365.25 patientage
FROM
    dxtpatientdiagnoseslog dx
    LEFT OUTER JOIN patients p ON (dx.patientcode = p.code);  
    
/*View view_purchaseorderdoc*/
DROP VIEW IF EXISTS view_purchaseorderdoc;
DROP TABLE IF EXISTS view_purchaseorderdoc;
CREATE
    VIEW view_purchaseorderdoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , od.transdate
    , od.orderno
    , od.ordertitle
    , od.shiptodescription
    , od.suppliercode
    , od.supplierdescription
    , od.currencycode
    , od.currencydescription
    , od.exchangerate
    , od.currencysymbol
    , od.remarks
    , od.orderstatus
    , od.userid
    , odi.productcode
    , odi.productdescription
    , odi.productopmcode
    , odi.productopmdescription
    , odi.productdepartmentcode
    , odi.productdepartmentdescription
    , odi.packagingcode
    , odi.packagingdescription
    , odi.piecesinpackage
    , odi.orderedqty
    , odi.transprice
FROM
    facilityoptions fo
    , som_orders od
    INNER JOIN som_orderitems odi
        ON (od.orderno = odi.orderno);     
        
/*View view_transferindoc*/
DROP VIEW IF EXISTS view_transferindoc;
DROP TABLE IF EXISTS view_transferindoc;
CREATE
    VIEW view_transferindoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , od.transdate
    , od.transferno
    , od.transferoutno
    , od.transfertitle
    , od.fromcode
    , od.fromdescription
    , od.tocode
    , od.todescription
    , od.currencycode
    , od.currencydescription
    , od.exchangerate
    , od.currencysymbol
    , od.remarks
    , od.transferstatus
    , od.userid
    , odi.productcode
    , odi.productdescription
    , odi.productopmcode
    , odi.productopmdescription
    , odi.productdepartmentcode
    , odi.productdepartmentdescription
    , odi.packagingcode
    , odi.packagingdescription
    , odi.piecesinpackage
    , odi.transferedqty
    , odi.transprice
FROM
    facilityoptions fo
    , som_transferins od
    INNER JOIN som_transferinitems odi
        ON (od.transferno = odi.transferno);   
        
/*View view_transferoutdoc*/
DROP VIEW IF EXISTS view_transferoutdoc;
DROP TABLE IF EXISTS view_transferoutdoc;
CREATE
    VIEW view_transferoutdoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , od.transdate
    , od.transferno
    , od.transferinno
    , od.transfertitle
    , od.fromcode
    , od.fromdescription
    , od.tocode
    , od.todescription
    , od.currencycode
    , od.currencydescription
    , od.exchangerate
    , od.currencysymbol
    , od.remarks
    , od.transferstatus
    , od.userid
    , odi.productcode
    , odi.productdescription
    , odi.productopmcode
    , odi.productopmdescription
    , odi.productdepartmentcode
    , odi.productdepartmentdescription
    , odi.packagingcode
    , odi.packagingdescription
    , odi.piecesinpackage
    , odi.transferedqty
    , odi.transprice
FROM
    facilityoptions fo
    , som_transferouts od
    INNER JOIN som_transferoutitems odi
        ON (od.transferno = odi.transferno);    
        
/*View view_ivissuedoc*/
DROP VIEW IF EXISTS view_ivissuedoc;
DROP TABLE IF EXISTS view_ivissuedoc;
CREATE
    VIEW view_ivissuedoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , od.transdate
    , od.deliveryno
    , od.transferno
    , od.transferinno
    , od.transfertitle
    , od.fromcode
    , od.fromdescription
    , od.tocode
    , od.todescription
    , od.currencycode
    , od.currencydescription
    , od.exchangerate
    , od.currencysymbol
    , od.remarks
    , od.transferstatus
    , od.userid
    , odi.productcode
    , odi.productdescription
    , odi.productopmcode
    , odi.productopmdescription
    , odi.productdepartmentcode
    , odi.productdepartmentdescription
    , odi.packagingcode
    , odi.packagingdescription
    , odi.piecesinpackage
    , odi.issuedqty
    , odi.transprice
FROM
    facilityoptions fo
    , som_stockissues od
    INNER JOIN som_stockissueitems odi
        ON (od.deliveryno = odi.deliveryno);  
        
/*View view_ivreceiptdoc*/
DROP VIEW IF EXISTS view_ivreceiptdoc;
DROP TABLE IF EXISTS view_ivreceiptdoc;
CREATE
    VIEW view_ivreceiptdoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , od.transdate
    , od.deliveryno
    , od.orderno
    , od.transferno
    , od.transferoutno
    , od.transfertitle
    , od.fromcode
    , od.fromdescription
    , od.tocode
    , od.todescription
    , od.currencycode
    , od.currencydescription
    , od.exchangerate
    , od.currencysymbol
    , od.remarks
    , od.transferstatus
    , od.userid
    , odi.productcode
    , odi.productdescription
    , odi.productopmcode
    , odi.productopmdescription
    , odi.productdepartmentcode
    , odi.productdepartmentdescription
    , odi.packagingcode
    , odi.packagingdescription
    , odi.piecesinpackage
    , odi.receivedqty
    , odi.transprice
FROM
    facilityoptions fo
    , som_stockreceipts od
    INNER JOIN som_stockreceiptitems odi
        ON (od.deliveryno = odi.deliveryno);   
        
/*View view_physicalinventorydoc*/
DROP VIEW IF EXISTS view_physicalinventorydoc;
DROP TABLE IF EXISTS view_physicalinventorydoc;
CREATE
    VIEW view_physicalinventorydoc 
    AS       
SELECT
    fo.facilitycode
    , fo.facilitydescription
    , fo.box
    , fo.street
    , fo.teleno
    , fo.headname
    , fo.headdesignation
    , fo.countrycode
    , fo.countrydescription
    , fo.regioncode
    , fo.regiondescription
    , fo.districtcode
    , fo.districtdescription  
    , od.transdate
    , od.calculateddate
    , od.closeddate
    , od.storecode
    , od.storedescription
    , od.referenceno
    , od.description
    , od.inventorystatus
    , od.userid
    , odi.productcode
    , odi.productdescription
    , odi.productopmcode
    , odi.productopmdescription
    , odi.productdepartmentcode
    , odi.productdepartmentdescription
    , odi.packagingcode
    , odi.packagingdescription
    , odi.piecesinpackage
    , odi.expirydate
    , odi.countedqty
    , odi.expectedqty
    , odi.transprice
FROM
    facilityoptions fo
    , som_physicalinventory od
    INNER JOIN som_physicalinventoryitems odi
        ON (od.referenceno = odi.referenceno);                                   	
	
/*View facility_view*/  
DROP VIEW IF EXISTS facility_view;		
DROP TABLE IF EXISTS facility_view;	
CREATE 
	VIEW facility_view 
	AS 
SELECT 
	fo.autocode autocode
	,fo.facilitydescription facilitydescription 
FROM facilityoptions fo;

/*view_debtorbalance*/
DROP VIEW IF EXISTS view_debtorbalance;
DROP TABLE IF EXISTS view_debtorbalance;
CREATE VIEW view_debtorbalance
AS
SELECT patientcode accountcode,SUM(balancedue) balancedue,'Individual' debtortype FROM billinvoices WHERE billinggroupcode='' AND voided<>1 GROUP BY patientcode
UNION
SELECT billinggroupcode accountcode,SUM(balancedue) balancedue,'Group' debtortype FROM billinvoices WHERE billinggroupcode<>'' AND voided<>1 GROUP BY billinggroupcode;

/*view_dxtdiagnoses*/  
DROP VIEW IF EXISTS view_dxtdiagnoses;		
DROP TABLE IF EXISTS view_dxtdiagnoses;	
CREATE 
	VIEW view_dxtdiagnoses 
	AS 
SELECT 
	dx.autocode
	,dx.code
	,dx.description
	,dx.groupcode
	,dxg.description groupdescription
	,dx.subgroupcode
	,dxsg.description subgroupdescription
FROM dxtdiagnoses dx
	LEFT JOIN dxtdiagnosesgroups dxg ON dx.groupcode = dxg.code
	LEFT JOIN dxtdiagnosessubgroups dxsg ON dx.subgroupcode = dxsg.code;

/*view_dxticddiagnoses*/  
DROP VIEW IF EXISTS view_dxticddiagnoses;		
DROP TABLE IF EXISTS view_dxticddiagnoses;	
CREATE 
	VIEW view_dxticddiagnoses 
	AS 
SELECT 
	dx.autocode
	,dx.code
	,dx.description
	,dx.groupcode
	,dxg.description groupdescription
	,dx.subgroupcode
	,dxsg.description subgroupdescription
FROM dxticddiagnoses dx
	LEFT JOIN dxtdiagnosesgroups dxg ON dx.groupcode = dxg.code
	LEFT JOIN dxtdiagnosessubgroups dxsg ON dx.subgroupcode = dxsg.code;

/*view_dxtdiagnosessubgroups*/  
DROP VIEW IF EXISTS view_dxtdiagnosessubgroups;		
DROP TABLE IF EXISTS view_dxtdiagnosessubgroups;	
CREATE 
	VIEW view_dxtdiagnosessubgroups 
	AS 
SELECT 
	dxsg.autocode
	,dxsg.code
	,dxsg.description
	,dxsg.groupcode
	,dxg.description groupdescription
FROM dxtdiagnosessubgroups dxsg
	LEFT JOIN dxtdiagnosesgroups dxg ON dxsg.groupcode = dxg.code;

/*view_facilitybookinglog*/  
DROP VIEW IF EXISTS view_facilitybookinglog;		
DROP TABLE IF EXISTS view_facilitybookinglog;
CREATE VIEW view_facilitybookinglog
AS
SELECT
	b.autocode
	,b.booking
	,b.patientcode
	,b.bookdate
	,b.refered
	,b.referedfacility
	,b.department
	,b.wheretakencode
	,(CASE WHEN tp.description IS NULL THEN '' ELSE tp.description END) AS wheretaken
	,b.billinggroupcode
	,(CASE 
		WHEN b.billinggroupcode='' THEN 'Individuals' 
		WHEN fc.description IS NULL THEN 'Unknown' 
		ELSE fc.description 
		END) AS billinggroup
	,b.billingsubgroupcode
	,(CASE WHEN fcs.description IS NULL THEN '' ELSE fcs.description END) AS billingsubgroup
	,b.billinggroupmembershipno
	,b.ipdstart
	,b.ipdstop
	,b.ipddischargestatus
	,b.registrystatus
	,b.userid
	,YEAR(b.bookdate) AS yearpart
	,MONTH(b.bookdate) AS monthpart
	,b.sysdate
	,b.patientweight
	,b.patienttemperature
FROM facilitybookinglog AS b LEFT OUTER JOIN
	facilitytreatmentpoints AS tp ON b.wheretakencode = tp.code LEFT OUTER JOIN
	facilitycorporates AS fc ON b.billinggroupcode = fc.code LEFT OUTER JOIN
	facilitycorporatesubgroups AS fcs ON b.billingsubgroupcode = fcs.code;

/*view_facilitybookings*/  
DROP VIEW IF EXISTS view_facilitybookings;		
DROP TABLE IF EXISTS view_facilitybookings;
CREATE VIEW view_facilitybookings
AS
SELECT
	b.autocode
	,b.patientcode
	,b.bookdate
	,b.refered
	,b.referedfacility
	,b.department
	,b.wheretakencode
	,(CASE WHEN tp.description IS NULL THEN '' ELSE tp.description END) AS wheretaken
	,b.billinggroupcode
	,(CASE 
		WHEN b.billinggroupcode='' THEN 'Individuals' 
		WHEN fc.description IS NULL THEN 'Unknown' 
		ELSE fc.description 
		END) AS billinggroup
	,b.billingsubgroupcode
	,(CASE WHEN fcs.description IS NULL THEN '' ELSE fcs.description END) AS billingsubgroup
	,b.billinggroupmembershipno
	,b.pricecategory
	,b.ipdstart
	,b.ipdstop
	,b.userid
	,YEAR(b.bookdate) AS yearpart
	,MONTH(b.bookdate) AS monthpart
	,b.sysdate
	,b.patientweight
	,b.patienttemperature
FROM facilitybookings AS b LEFT OUTER JOIN
	facilitytreatmentpoints AS tp ON b.wheretakencode = tp.code LEFT OUTER JOIN
	facilitycorporates AS fc ON b.billinggroupcode = fc.code LEFT OUTER JOIN
	facilitycorporatesubgroups AS fcs ON b.billingsubgroupcode = fcs.code;
	 
/*view_ipdadmissionslog*/  
DROP VIEW IF EXISTS view_ipdadmissionslog;	
DROP TABLE IF EXISTS view_ipdadmissionslog;	
CREATE VIEW view_ipdadmissionslog
AS
SELECT
	b.autocode
	,b.sysdate
	,b.transdate
	,b.patientcode
	,b.booking
	,b.transcode
	,b.wardcode
	,(CASE WHEN w.description IS NULL THEN '' ELSE w.description END) AS warddescription
	,b.roomcode
	,(CASE WHEN wr.description IS NULL THEN '' ELSE wr.description END) AS roomdescription
	,(CASE WHEN b.bedcode IS NULL THEN '' ELSE b.bedcode END) AS bedcode
	,(CASE WHEN wrb.description IS NULL THEN '' ELSE wrb.description END) AS beddescription
	,b.patientcondition
	,b.registrystatus
	,b.userid
	,YEAR(b.transdate) AS yearpart
	,MONTH(b.transdate) AS monthpart
	,b.patientweight
	,b.patienttemperature
FROM ipdadmissionslog AS b LEFT OUTER JOIN
	facilitywards AS w ON b.wardcode = w.code LEFT OUTER JOIN
	facilitywardrooms AS wr ON b.roomcode = wr.code LEFT OUTER JOIN
	facilitywardroombeds AS wrb ON b.bedcode = wrb.code;

/*View view_ipddischargeslog*/
DROP VIEW IF EXISTS view_ipddischargeslog;
DROP TABLE IF EXISTS view_ipddischargeslog;
CREATE
    VIEW view_ipddischargeslog
    AS     
SELECT	
	b.autocode
	,b.sysdate
	,b.transdate
	,b.patientcode
	,b.booking
	,b.transcode
	,b.wardcode
	,(CASE WHEN w.description IS NULL THEN '' ELSE w.description END) AS warddescription
	,b.roomcode
	,(CASE WHEN wr.description IS NULL THEN '' ELSE wr.description END) AS roomdescription
	,(CASE WHEN b.bedcode IS NULL THEN '' ELSE b.bedcode END) AS bedcode
	,(CASE WHEN wrb.description IS NULL THEN '' ELSE wrb.description END) AS beddescription
	,b.patientcondition
	,(CASE WHEN b.dischargestatuscode IS NULL THEN '' ELSE b.dischargestatuscode END) AS dischargestatuscode
	,(CASE WHEN ds.description IS NULL THEN '' ELSE ds.description END) AS dischargestatusdescription
	,b.dischargeremarks
	,b.registrystatus
	,b.userid
	,YEAR(b.transdate) AS yearpart
	,MONTH(b.transdate) AS monthpart
	,b.patientweight
	,b.patienttemperature
FROM ipddischargeslog AS b LEFT OUTER JOIN
	facilitywards AS w ON b.wardcode = w.code LEFT OUTER JOIN
	facilitywardrooms AS wr ON b.roomcode = wr.code LEFT OUTER JOIN
	facilitywardroombeds AS wrb ON b.bedcode = wrb.code LEFT OUTER JOIN
	facilitydischargestatus AS ds ON b.dischargestatuscode = ds.code;      
    
/*View view_ipdtransferslog*/
DROP VIEW IF EXISTS view_ipdtransferslog;
DROP TABLE IF EXISTS view_ipdtransferslog;
CREATE
    VIEW view_ipdtransferslog
    AS  
SELECT	
	b.autocode
	,b.sysdate
	,b.transferdate
	,b.booking
	,b.patientcode
	,b.wardfromcode
	,(CASE WHEN fw.description IS NULL THEN '' ELSE fw.description END) AS wardfromdescription
	,b.roomfromcode
	,(CASE WHEN fwr.description IS NULL THEN '' ELSE fwr.description END) AS roomfromdescription
	,(CASE WHEN b.bedfromcode IS NULL THEN '' ELSE b.bedfromcode END) AS bedfromcode
	,(CASE WHEN fwrb.description IS NULL THEN '' ELSE fwrb.description END) AS bedfromdescription		
	,b.wardtocode
	,(CASE WHEN tw.description IS NULL THEN '' ELSE tw.description END) AS wardtodescription
	,b.roomtocode
	,(CASE WHEN twr.description IS NULL THEN '' ELSE twr.description END) AS roomtodescription
	,(CASE WHEN b.bedtocode IS NULL THEN '' ELSE b.bedtocode END) AS bedtocode
	,(CASE WHEN twrb.description IS NULL THEN '' ELSE twrb.description END) AS bedtodescription
	,transfertofacility
	,patientcondition
	,YEAR(b.transferdate) AS yearpart
	,MONTH(b.transferdate) AS monthpart
	,registrystatus
	,userid
	,patientweight
	,patienttemperature
FROM ipdtransferslog AS b LEFT OUTER JOIN
	facilitywards AS fw ON b.wardfromcode = fw.code LEFT OUTER JOIN
	facilitywardrooms AS fwr ON b.roomfromcode = fwr.code LEFT OUTER JOIN
	facilitywardroombeds AS fwrb ON b.bedfromcode = fwrb.code LEFT OUTER JOIN
	facilitywards AS tw ON b.wardtocode = tw.code LEFT OUTER JOIN
	facilitywardrooms AS twr ON b.roomtocode = twr.code LEFT OUTER JOIN
	facilitywardroombeds AS twrb ON b.bedtocode = twrb.code; 

/*View view_vaccineslog*/
DROP VIEW IF EXISTS view_vaccineslog;
DROP TABLE IF EXISTS view_vaccineslog;
CREATE VIEW view_vaccineslog
AS
SELECT
	l.autocode
	,l.sysdate
	,l.bookdate
	,l.booking
	,l.fplanbooking
	,l.antenatalbooking
	,l.postnatalbooking
	,l.childrenbooking
	,l.clientcode
	,l.servicetype
	,l.registrystatus
	,YEAR(l.bookdate) AS yearpart
	,MONTH(l.bookdate) AS monthpart
	,l.userid
	,b.vaccinecode
	,v.description AS vaccinedescription
	,b.remarks
FROM rch_vaccineslog AS b LEFT OUTER JOIN
	rch_vaccinationslog AS l ON b.booking=l.booking AND b.clientcode=l.clientcode LEFT OUTER JOIN
	rch_vaccines AS v ON b.vaccinecode=v.code;

/*View view_rch_fplanmethodslog*/
DROP VIEW IF EXISTS view_rch_fplanmethodslog;
DROP TABLE IF EXISTS view_rch_fplanmethodslog;
CREATE VIEW view_rch_fplanmethodslog
AS
SELECT
	al.sysdate,
	al.bookdate,
	al.booking,
	al.clientcode,
	p.gender,
	p.birthdate,
	DATEDIFF(al.bookdate,p.birthdate)/365.25 patientage,
	ml.methodcode,
	m.description AS methoddescription,
	ml.quantity,
	al.complains,
	al.registrystatus,
	YEAR(al.bookdate) AS yearpart,
	MONTH(al.bookdate) AS monthpart,
	al.userid
FROM rch_fplanmethodslog AS ml LEFT OUTER JOIN
	rch_fplanmethods AS m ON ml.methodcode = m.code LEFT OUTER JOIN
	rch_fplanattendancelog AS al ON ml.booking = al.booking AND ml.clientcode = al.clientcode LEFT OUTER JOIN
	patients AS p ON ml.clientcode = p.code;

/*View view_rch_dangerindicatorslog*/
DROP VIEW IF EXISTS view_rch_dangerindicatorslog;
DROP TABLE IF EXISTS view_rch_dangerindicatorslog;
CREATE VIEW view_rch_dangerindicatorslog
AS
SELECT
	al.sysdate,
	al.bookdate,
	al.booking,
	al.clientcode,
	p.gender,
	p.birthdate,
	DATEDIFF(al.bookdate,p.birthdate)/365.25 patientage,	
	b.methodcode,
	i.description AS methoddescription,
	al.registrystatus,
	YEAR(al.bookdate) AS yearpart,
	MONTH(al.bookdate) AS monthpart,
	al.userid
FROM rch_dangerindicatorslog AS b LEFT OUTER JOIN
	rch_dangerindicators AS i ON b.methodcode = i.code LEFT OUTER JOIN
	rch_fplanattendancelog AS al ON b.booking = al.booking AND b.clientcode = al.clientcode LEFT OUTER JOIN
	patients AS p ON b.clientcode = p.code;

/*View view_rch_birthcomplicationslog*/
DROP VIEW IF EXISTS view_rch_birthcomplicationslog;
DROP TABLE IF EXISTS view_rch_birthcomplicationslog;
CREATE VIEW view_rch_birthcomplicationslog
AS
SELECT
	p.bookdate,
	b.clientcode,
	b.methodcode,
	c.description AS methoddescription,
	b.quantity
FROM rch_birthcomplicationslog AS b LEFT OUTER JOIN
	rch_postnatalattendancelog AS p ON b.booking=p.booking AND b.clientcode=p.clientcode LEFT OUTER JOIN
	rch_birthcomplications AS c ON b.methodcode=c.code;

/*View view_rch_postnatalchildren*/
DROP VIEW IF EXISTS view_rch_postnatalchildren;
DROP TABLE IF EXISTS view_rch_postnatalchildren;
CREATE VIEW view_rch_postnatalchildren
AS
SELECT
	pa.bookdate,
	pa.noofchildren,
	pc.clientcode,
	pc.weight,
	pc.live,
	pc.maceratedbirth,
	pc.freshbirth,
	pc.deathbefore24,
	pc.deathafter24
FROM rch_postnatalchildren AS pc LEFT OUTER JOIN
	rch_postnatalattendancelog AS pa ON pc.booking=pa.booking AND pc.clientcode=pa.clientcode;

/*View view_labpatienttests*/
DROP VIEW IF EXISTS view_labpatienttests;
DROP TABLE IF EXISTS view_labpatienttests;
CREATE VIEW view_labpatienttests
AS
SELECT
	b.autocode,
	b.sysdate,
	b.transdate,
	b.booking,
	b.patientcode,
	b.laboratorycode,
	(CASE WHEN l.description IS NULL THEN '' ELSE l.description END) AS laboratorydescription,
	b.doctorcode,
	(CASE WHEN d.description IS NULL THEN '' ELSE d.description END) AS doctordescription,
	b.labtechniciancode,
	(CASE WHEN lt.description IS NULL THEN '' ELSE lt.description END) AS labtechniciandescription,
	b.labtestgroupcode,
	(CASE WHEN lg.description IS NULL THEN '' ELSE lg.description END) AS labtestgroupdescription,
	b.labtestsubgroupcode,
	(CASE WHEN lsg.description IS NULL THEN '' ELSE lsg.description END) AS labtestsubgroupdescription,
	b.labtesttypecode,
	(CASE WHEN t.description IS NULL THEN '' ELSE t.description END) AS labtesttypedescription,
	(CASE WHEN t.additionalinfo IS NULL THEN 0 ELSE t.additionalinfo END) AS additionalinfo,
	b.results,
	b.resultfigure,
	b.units,
	b.outofrange_normal,
	b.outofrange_equipment,
	b.remarks,
	b.department,
	YEAR(b.transdate) AS yearpart,
	MONTH(b.transdate) AS monthpart,
	b.userid,
	b.patientweight,
	b.patienttemperature
FROM labpatienttests AS b LEFT OUTER JOIN
  facilitystaffs AS d ON b.doctorcode = d.code LEFT OUTER JOIN
  facilitystaffs AS lt ON b.labtechniciancode = lt.code LEFT OUTER JOIN
  facilitylaboratories AS l ON b.laboratorycode = l.code LEFT OUTER JOIN
  labtestgroups AS lg ON b.labtestgroupcode = lg.code LEFT OUTER JOIN
  labtestsubgroups AS lsg ON b.labtestsubgroupcode = lsg.code LEFT OUTER JOIN
  labtests AS t ON b.labtesttypecode = t.code;

/*View view_patientsqueue*/
DROP VIEW IF EXISTS view_patientsqueue;
DROP TABLE IF EXISTS view_patientsqueue;
CREATE VIEW view_patientsqueue
AS
SELECT
  q.autocode,
  q.sysdate,
  q.transdate,
  q.treatmentpointcode,
  q.laboratorycode,
  q.patientcode,
  q.queuetype,
  CASE q.queuetype
    WHEN 0 THEN 'Consultation'
    WHEN 1 THEN 'Lab Test'
    WHEN 2 THEN 'Lab Test Results'
  END AS queuetypedescription
FROM patientsqueue AS q;

/*View view_ctc_firstartvisits*/
DROP VIEW IF EXISTS view_ctc_firstartvisits;
DROP TABLE IF EXISTS view_ctc_firstartvisits;
CREATE VIEW view_ctc_firstartvisits
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	outcomecode,
	outcomedate,
	artregimencode,
	tbstatuscode,
	pillcount,
	dosesmissed,
	arvtablets,
	arvto,
	cpttablets,
	depogiven,
	condoms,
	viralloaddate,
	viralloadresult,
	notes,
	cliniciancode,
	userid,
	MIN(transdate) transdate
FROM ctc_artlog
GROUP BY
	patientcode;

/*View view_ctc_patients*/
DROP VIEW IF EXISTS view_ctc_patients;
DROP TABLE IF EXISTS view_ctc_patients;
CREATE VIEW view_ctc_patients
AS
SELECT
	p.autocode,
	p.patientcode,
	p.hivno,
	p.hivtestno,
	p.referredfromcode,
	(CASE WHEN p.referredfromcode='oth' THEN p.referredfromother ELSE rf.description END) AS referredfromdescription,
	p.referredfromother,
	p.enrolledincaredate,
	p.medeligibledate,
	p.eligibleandreadydate,
	p.whyeligiblecode,
	we.description AS whyeligibledescription,
	p.whyeligiblecd4,
	p.whyeligibletlc,
	p.supportersurname,
	p.supporterfirstname,
	p.supporterothernames,
	p.supporteraddress,
	p.supportertelephone,
	p.supportercommunity,
	p.htcno,
	p.ctcno,
	p.arvno,
	p.priorarvexposurecode,
	p.guardianname,
	p.patientphone,
	p.guardianphone,
	p.guardianrelation,
	p.agreetofup
FROM ctc_patients p LEFT OUTER JOIN
  ctc_referedfrom rf ON p.referredfromcode = rf.code LEFT OUTER JOIN
  ctc_artwhyeligible we ON p.whyeligiblecode = we.code;

/*View view_ctc_hivtests*/
DROP VIEW IF EXISTS view_ctc_hivtests;
DROP TABLE IF EXISTS view_ctc_hivtests;
CREATE VIEW view_ctc_hivtests
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.patientcode,
	p.booking,
	p.pregnant,
	p.evertested,
	p.withpartner,
	p.testresult1,
	CASE p.testresult1
		WHEN 0 THEN 'Unknown'
		WHEN 1 THEN 'Positive'
		WHEN 2 THEN 'Negative'
	END AS testresult1description,
	p.testresult2,
	CASE p.testresult2
		WHEN 0 THEN 'Unknown'
		WHEN 1 THEN 'Positive'
		WHEN 2 THEN 'Negative'
	END AS testresult2description,
	p.testresult3,
	CASE p.testresult3
		WHEN 0 THEN 'Unknown'
		WHEN 1 THEN 'Positive'
		WHEN 2 THEN 'Negative'
	END AS testresult3description,
	p.testresultgiven,
	CASE p.testresultgiven
		WHEN 0 THEN 'Unknown'
		WHEN 1 THEN 'Positive'
		WHEN 2 THEN 'Negative'
		WHEN 3 THEN 'Exposed Infant'
		WHEN 4 THEN 'Inconclusive'
	END AS testresultgivendescription,
	p.testresultgivendate,
	p.comments,
	p.counsellorcode,
	s.description AS counsellordescription,
	a.apptdate AS nextapptdate,
	p.userid
FROM ctc_hivtests p LEFT OUTER JOIN
  facilitystaffs s ON p.counsellorcode = s.code LEFT OUTER JOIN
  ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 0;

/*View view_ctc_pcrtests*/
DROP VIEW IF EXISTS view_ctc_pcrtests;
DROP TABLE IF EXISTS view_ctc_pcrtests;
CREATE VIEW view_ctc_pcrtests
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.patientcode,
	p.booking,
	p.sampledate,
	p.reasoncode,
	tr.description AS reasondescription,
	p.testresult,
	CASE p.testresult
		WHEN 0 THEN 'Unknown'
		WHEN 1 THEN 'Positive'
		WHEN 2 THEN 'Negative'
		WHEN 3 THEN 'Exposed Infant'
		WHEN 4 THEN 'Inconclusive'
	END AS testresultdescription,
	p.testresultdate,
	p.breastfeeding,
	CASE p.breastfeeding
		WHEN 0 THEN 'No'
		WHEN 1 THEN 'Yes'
	END AS breastfeedingdescription,
	p.comments,
	p.counsellorcode,
	s.description AS counsellordescription,
	a.apptdate AS nextapptdate,
	p.userid
FROM ctc_pcrtests p LEFT OUTER JOIN
  facilitystaffs s ON p.counsellorcode = s.code LEFT OUTER JOIN
  ctc_hivtestreasons tr ON p.reasoncode = tr.code LEFT OUTER JOIN
  ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 0;

/*View view_ctc_cd4tests*/
DROP VIEW IF EXISTS view_ctc_cd4tests;
DROP TABLE IF EXISTS view_ctc_cd4tests;
CREATE VIEW view_ctc_cd4tests
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.patientcode,
	p.booking,
	p.sampleid,
	p.testresult,
	p.testresultpercent,
	p.testresultdate,
	p.comments,
	p.labtechniciancode,
	fs.description AS labtechniciandescription,
	p.userid
FROM ctc_cd4tests p LEFT OUTER JOIN
	facilitystaffs fs ON p.labtechniciancode = fs.code;

/*View view_ctc_lastcd4tests*/
DROP VIEW IF EXISTS view_ctc_lastcd4tests;
DROP TABLE IF EXISTS view_ctc_lastcd4tests;
CREATE VIEW view_ctc_lastcd4tests
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	sampleid,
	testresult,
	testresultpercent,
	testresultdate,
	comments,
	MAX(transdate) transdate
FROM ctc_cd4tests
GROUP BY
	patientcode;

/*View view_ctc_lasthivtests*/
DROP VIEW IF EXISTS view_ctc_lasthivtests;
DROP TABLE IF EXISTS view_ctc_lasthivtests;
CREATE VIEW view_ctc_lasthivtests
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	pregnant,
	evertested,
	withpartner,
	testresult1,
	testresult2,
	testresult3,
	testresultgiven,
	testresultgivendate,
	comments,
	counsellorcode,
	userid,
	MAX(transdate) transdate
FROM ctc_hivtests
GROUP BY
	patientcode;

/*View view_ctc_lastpcrtests*/
DROP VIEW IF EXISTS view_ctc_lastpcrtests;
DROP TABLE IF EXISTS view_ctc_lastpcrtests;
CREATE VIEW view_ctc_lastpcrtests
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	reasoncode,
	testresult,
	testresultdate,
	breastfeeding
	comments,
	counsellorcode,
	userid,
	MAX(transdate) transdate
FROM ctc_pcrtests
GROUP BY
	patientcode;

/*View view_ctc_appointments*/
DROP VIEW IF EXISTS view_ctc_appointments;
DROP TABLE IF EXISTS view_ctc_appointments;
CREATE VIEW view_ctc_appointments
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.patientcode,
	p.booking,
	p.appttype,
	CASE p.appttype
		WHEN 0 THEN 'HIV Test'
		WHEN 1 THEN 'ART Visit'
	END AS appttypedescription,
	p.reason,
	p.wheretakencode,
	t.description AS wheretakendescription,
	p.apptdate,
	p.attdate,
	p.apptstatus,
	CASE p.apptstatus
		WHEN 0 THEN 'NEW'
		WHEN 1 THEN 'MET'
	END AS apptstatusdescription,
	p.userid
FROM ctc_appointments p LEFT OUTER JOIN
  facilitytreatmentpoints t ON p.wheretakencode = t.code;

/*View view_ctc_triage*/
DROP VIEW IF EXISTS view_ctc_triage;
DROP TABLE IF EXISTS view_ctc_triage;
CREATE VIEW view_ctc_triage
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.patientcode,
	p.booking,
	p.weight,
	p.height,
	p.pulse,
	p.bloodpressure,
	p.resprate,
	p.temperature,
	p.userid
FROM ctc_triage p;

/*View view_ctc_patientaidsillness*/
DROP VIEW IF EXISTS view_ctc_patientaidsillness;
DROP TABLE IF EXISTS view_ctc_patientaidsillness;
CREATE VIEW view_ctc_patientaidsillness
AS
SELECT
	p.autocode,
	p.booking,
	p.patientcode,
	p.illnesscode,
	sef.description AS illnessdescription
FROM ctc_patientaidsillness p LEFT OUTER JOIN
	ctc_aidsillness sef ON p.illnesscode = sef.code;

/*View view_ctc_patientcomeds*/
DROP VIEW IF EXISTS view_ctc_patientcomeds;
DROP TABLE IF EXISTS view_ctc_patientcomeds;
CREATE VIEW view_ctc_patientcomeds
AS
SELECT
	p.autocode,
	p.booking,
	p.patientcode,
	p.medcode,
	rec.description AS meddescription
FROM ctc_patientcomeds p LEFT OUTER JOIN
	ctc_relevantcomeds rec ON p.medcode = rec.code;

/*View view_ctc_firsthivtests*/
DROP VIEW IF EXISTS view_ctc_firsthivtests;
DROP TABLE IF EXISTS view_ctc_firsthivtests;
CREATE VIEW view_ctc_firsthivtests
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	pregnant,
	evertested,
	withpartner,
	testresult1,
	testresult2,
	testresult3,
	testresultgiven,
	testresultgivendate,
	comments,
	counsellorcode,
	userid,
	MIN(transdate) transdate
FROM ctc_hivtests
GROUP BY
	patientcode;

/*View view_ctc_confirmedhivtests*/
DROP VIEW IF EXISTS view_ctc_confirmedhivtests;
DROP TABLE IF EXISTS view_ctc_confirmedhivtests;
CREATE VIEW view_ctc_confirmedhivtests
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	pregnant,
	evertested,
	withpartner,
	testresult1,
	testresult2,
	testresult3,
	testresultgiven,
	testresultgivendate,
	comments,
	counsellorcode,
	userid,
	MAX(transdate) transdate
FROM ctc_hivtests
WHERE testresultgiven = 1
GROUP BY
	patientcode;

/*View view_ctc_lasttriage*/
DROP VIEW IF EXISTS view_ctc_lasttriage;
DROP TABLE IF EXISTS view_ctc_lasttriage;
CREATE VIEW view_ctc_lasttriage
AS
SELECT
	autocode,
	SYSDATE,
	patientcode,
	booking,
	weight,
	height,
	pulse,
	bloodpressure,
	resprate,
	temperature,
	userid,
	MAX(transdate) transdate
FROM ctc_triage
GROUP BY
	patientcode;

/*View view_ctc_preart*/
DROP VIEW IF EXISTS view_ctc_preart;
DROP TABLE IF EXISTS view_ctc_preart;
CREATE VIEW view_ctc_preart
AS
SELECT
	p.autocode,
	p.patientcode,
	p.booking,
	p.referedfrom,
	p.transferindate,
	p.hivcareclinicno,
	p.confirmsite,
	p.confirmdate,
	p.confirmtesttype,
	p.clinicalstage,
	p.clinicalconditions,
	p.takenipt,
	p.iptdate,
	p.takentbtreatment,
	p.tbtreatmentdate,
	p.tbregno,
	p.takenarv,
	p.arvdate,
	p.arvtype
FROM ctc_preart p;

/*View view_ctc_preartlog*/
DROP VIEW IF EXISTS view_ctc_preartlog;
DROP TABLE IF EXISTS view_ctc_preartlog;
CREATE VIEW view_ctc_preartlog
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.wastingcode,
	w.description AS wastingdescription,
	p.tbstatuscode,
	tbs.description tbstatusdescription,
	p.clinicalstage,
	p.pregnant,
	p.ipttablets,
	p.cpttablets,
	p.depogiven,
	p.condoms,
	CASE WHEN ct.testresult IS NULL THEN 0 ELSE ct.testresult END AS cd4count,
	CASE WHEN ct.testresultpercent IS NULL THEN 0 ELSE ct.testresultpercent END AS cd4countpercent,
	a.apptdate AS nextvisitdate,
	p.followupstatuscode,
	fl.description followupstatusdescription,
	p.outcomedate,
	p.cliniciancode,
	p.notes,
	p.userid
FROM ctc_preartlog p LEFT OUTER JOIN
	ctc_wastingcodes w ON p.wastingcode = w.code LEFT OUTER JOIN
	ctc_tbstatus tbs ON p.tbstatuscode = tbs.code LEFT OUTER JOIN
	ctc_followupstatus fl ON p.followupstatuscode = fl.code LEFT OUTER JOIN
	ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 1 LEFT OUTER JOIN
	ctc_cd4tests ct ON p.booking = ct.booking AND p.patientcode = ct.patientcode LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_ctc_art*/
DROP VIEW IF EXISTS view_ctc_art;
DROP TABLE IF EXISTS view_ctc_art;
CREATE VIEW view_ctc_art
AS
SELECT
	p.autocode,
	p.patientcode,
	p.booking,
	p.transferindate,
	p.hivtestsite,
	p.hivtestdate,
	p.hivtesttype,
	p.clinicalstage,
	p.clinicalconditions,
	p.cd4count,
	p.cd4countpercent,
	p.cd4date,
	p.weight,
	p.height,
	p.tbinitialstatus,
	p.tbtreatmentdate,
	p.tbregno,
	p.artedu,
	p.artedudate,
	p.ks,
	p.pregnant,
	p.takenarv,
	p.arvdate,
	p.arvtype,
	p.artregimencode1,
	p.artregimendate1,
	p.artregimencode2,
	p.artregimendate2
FROM ctc_art p;

/*View view_ctc_artlog*/
DROP VIEW IF EXISTS view_ctc_artlog;
DROP TABLE IF EXISTS view_ctc_artlog;
CREATE VIEW view_ctc_artlog
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.outcomecode,
	o.description AS outcomedescription,
	p.outcomedate,
	p.artregimencode,
	com.description AS artregimendescription,
	p.tbstatuscode,
	tbs.description tbstatusdescription,
	p.pillcount,
	p.dosesmissed,
	p.arvtablets,
	p.arvto,
	CASE p.arvto
		WHEN 0 THEN 'Patient'
		WHEN 1 THEN 'Guardian'
	END AS arvtotdescription,
	p.cpttablets,
	p.depogiven,
	p.condoms,
	p.viralloaddate,
	p.viralloadresult,
	CASE WHEN ct.testresult IS NULL THEN 0 ELSE ct.testresult END AS cd4count,
	CASE WHEN ct.testresultpercent IS NULL THEN 0 ELSE ct.testresultpercent END AS cd4countpercent,
	a.apptdate AS nextvisitdate,
	p.cliniciancode,
	p.notes,
	p.userid
FROM ctc_artlog p LEFT OUTER JOIN
	ctc_artoutcomes o ON p.outcomecode = o.code LEFT OUTER JOIN
	ctc_tbstatus tbs ON p.tbstatuscode = tbs.code LEFT OUTER JOIN
	ctc_arvcombregimens com ON p.artregimencode = com.code LEFT OUTER JOIN
	ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 1 LEFT OUTER JOIN
	ctc_cd4tests ct ON p.booking = ct.booking AND p.patientcode = ct.patientcode LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_ctc_artt*/
DROP VIEW IF EXISTS view_ctc_artt;
DROP TABLE IF EXISTS view_ctc_artt;
CREATE VIEW view_ctc_artt
AS
SELECT
	p.autocode,
	p.patientcode,
	p.booking,
	p.onarv,
	p.referredfromcode,
	p.referredfromother,
	p.joinedsupport,
	p.supportername,
	p.supporteraddress,
	p.supportertelephone,
	p.supportercommunity,
	p.priorarvexposurecode,
	p.tbregno,
	p.hbcno,
	p.confirmedhivdate,
	p.enrolledincaredate,
	p.medeligibledate,
	p.eligibleandreadydate,
	p.startedartdate,
	p.whyeligibleclinicalstage,
	p.whyeligiblecd4count,
	p.whyeligiblecd4countpercent,
	p.startclinicalstage,
	p.startcd4count,
	p.startcd4countpercent,
	p.startfunctionalstatuscode,
	p.startweight
FROM ctc_artt p;

/*View view_ctc_arttlog*/
DROP VIEW IF EXISTS view_ctc_arttlog;
DROP TABLE IF EXISTS view_ctc_arttlog;
CREATE VIEW view_ctc_arttlog
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.visittypecode,
	p.clinicalstage,
	p.pregnant,
	p.edddate,
	p.ancno,
	p.tbscreeningcode,
	p.tbrxcode,
	p.functionalstatuscode,
	p.arvstatuscode,
	p.arvreasoncode,
	p.arvcombregimencode,
	com.description AS arvcombregimendescription,
	p.arvcombregimendays,
	p.arvadherencecode,
	p.arvpooradherencereasoncode,
	p.hb,
	p.alt,
	p.nutritionalstatuscode,
	p.nutritionalsuppcode,
	p.followupstatuscode,
	CASE WHEN ct.testresult IS NULL THEN 0 ELSE ct.testresult END AS cd4count,
	CASE WHEN ct.testresultpercent IS NULL THEN 0 ELSE ct.testresultpercent END AS cd4countpercent,
	a.apptdate AS nextvisitdate,
	p.cliniciancode,
	fs.description AS cliniciandescription,
	p.userid
FROM ctc_arttlog p LEFT OUTER JOIN
	facilitystaffs fs ON p.cliniciancode = fs.code LEFT OUTER JOIN
	ctc_arvcombregimens com ON p.arvcombregimencode = com.code LEFT OUTER JOIN
	ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 1 LEFT OUTER JOIN
	ctc_cd4tests ct ON p.booking = ct.booking AND p.patientcode = ct.patientcode LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_ctc_artp*/
DROP VIEW IF EXISTS view_ctc_artp;
DROP TABLE IF EXISTS view_ctc_artp;
CREATE VIEW view_ctc_artp
AS
SELECT
	p.autocode,
	p.patientcode,
	p.booking,
	p.transferindate,
	p.hivtestsite1,
	p.hivtestdate1,
	p.hivtesttype1,
	p.hivtestsite2,
	p.hivtestdate2,
	p.hivtesttype2,
	p.hivtestsite3,
	p.hivtestdate3,
	p.hivtesttype3,
	p.clinicalstage,
	p.clinicalconditions,
	p.cd4count,
	p.cd4countpercent,
	p.cd4date,
	p.weight,
	p.height,
	p.tbinitialstatus,
	p.tbtreatmentdate,
	p.tbregno,
	p.artedu,
	p.artedudate,
	p.ks,
	p.arvdate,
	p.arvtype,
	p.artregimencode1,
	p.artregimendate1,
	p.artregimencode2,
	p.artregimendate2
FROM ctc_artp p;

/*View view_ctc_artplog*/
DROP VIEW IF EXISTS view_ctc_artplog;
DROP TABLE IF EXISTS view_ctc_artplog;
CREATE VIEW view_ctc_artplog
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.outcomecode,
	o.description AS outcomedescription,
	p.outcomedate,
	p.artregimencode,
	com.description AS artregimendescription,
	p.tbstatuscode,
	tbs.description tbstatusdescription,
	p.pillcount,
	p.dosesmissed,
	p.arvtablets,
	p.arvto,
	CASE p.arvto
		WHEN 0 THEN 'Patient'
		WHEN 1 THEN 'Guardian'
	END AS arvtotdescription,
	p.cpttablets,
	p.viralloaddate,
	p.viralloadresult,
	CASE WHEN ct.testresult IS NULL THEN 0 ELSE ct.testresult END AS cd4count,
	CASE WHEN ct.testresultpercent IS NULL THEN 0 ELSE ct.testresultpercent END AS cd4countpercent,
	a.apptdate AS nextvisitdate,
	p.cliniciancode,
	p.notes,
	p.userid
FROM ctc_artplog p LEFT OUTER JOIN
	ctc_artoutcomes o ON p.outcomecode = o.code LEFT OUTER JOIN
	ctc_tbstatus tbs ON p.tbstatuscode = tbs.code LEFT OUTER JOIN
	ctc_arvcombregimens com ON p.artregimencode = com.code LEFT OUTER JOIN
	ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 1 LEFT OUTER JOIN
	ctc_cd4tests ct ON p.booking = ct.booking AND p.patientcode = ct.patientcode LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_ctc_exposed*/
DROP VIEW IF EXISTS view_ctc_exposed;
DROP TABLE IF EXISTS view_ctc_exposed;
CREATE VIEW view_ctc_exposed
AS
SELECT
	p.autocode,
	p.patientcode,
	p.booking,
	p.weight,
	p.enrolmentdate,
	p.transferindate,
	p.arvpregcode,
	p.arvlabourcode,
	p.arvbabybirthcode,
	p.arvbabycontcode,
	p.arvbabyadhere,
	p.deliveryplace,
	p.hivtestrapid,
	p.hivtestrapidage,
	p.hivtestpcr,
	p.hivtestpcrage,
	p.confirmedhiv,
	p.motherstatuscode,
	p.motherartno,
	p.birthcohortyear,
	p.birthcohortmonth
FROM ctc_exposed p;

/*View view_ctc_exposedlog*/
DROP VIEW IF EXISTS view_ctc_exposedlog;
DROP TABLE IF EXISTS view_ctc_exposedlog;
CREATE VIEW view_ctc_exposedlog
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.muac,
	p.wastingcode,
	w.description AS wastingdescription,
	p.breastfeedingcode,
	bf.description AS breastfeedingdescription,
	p.motherstatuscode,
	ms.description AS montherstatusdescription,
	p.tbstatuscode,
	tbs.description tbstatusdescription,
	p.clinmonit,
	cm.description AS clinmonitdescription,
	p.hivinfectioncode,
	hi.description AS hivinfectiondescription,
	p.cptmills,
	p.cpttablets,
	p.outcomecode,
	o.description AS outcomedescription,
	p.outcomedate,
	a.apptdate AS nextvisitdate,
	p.cliniciancode,
	p.notes,
	p.userid
FROM ctc_exposedlog p LEFT OUTER JOIN
	ctc_wastingcodes w ON p.wastingcode = w.code LEFT OUTER JOIN
	ctc_breastfeedings bf ON p.breastfeedingcode = bf.code LEFT OUTER JOIN
	ctc_motherstatus ms ON p.motherstatuscode = ms.code LEFT OUTER JOIN
	ctc_tbstatus tbs ON p.tbstatuscode = tbs.code LEFT OUTER JOIN
	ctc_hivinfections hi ON p.hivinfectioncode = hi.code LEFT OUTER JOIN
	ctc_clinmonit cm ON p.clinmonit = cm.code LEFT OUTER JOIN
	ctc_artoutcomes o ON p.outcomecode = o.code LEFT OUTER JOIN
	ctc_appointments a ON p.booking = a.booking AND p.patientcode = a.patientcode AND a.appttype = 1 LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_dxtfirstencounters*/
DROP VIEW IF EXISTS view_dxtfirstencounters;
DROP TABLE IF EXISTS view_dxtfirstencounters;
CREATE VIEW view_dxtfirstencounters
AS
SELECT
	epd.autocode,
	epd.sysdate,
	epd.booking,
	epd.episodecode,
	epd.patientcode,
	epd.isprimary,
	epd.ipddiagnosistype,
	epd.diagnosiscode,
	dx.description AS diagnosisdescription,
	epd.indicatorcode,
	epd.billinggroupcode,
	epd.billingsubgroupcode,
	epd.history,
	epd.examination,
	epd.investigation,
	epd.treatments,
	epd.doctorcode,
	fs.description AS doctordescription,
	epd.referaldescription,
	epd.department,
	epd.patientweight,
	epd.patienttemperature,
	epd.wardcode,
	epd.roomcode,
	epd.bedcode,
	epd.userid,
	MIN(transdate) transdate
FROM dxtpatientdiagnoseslog AS epd
LEFT OUTER JOIN dxticddiagnoses dx ON epd.diagnosiscode=dx.code
LEFT OUTER JOIN facilitystaffs fs ON epd.doctorcode=fs.code
GROUP BY
	epd.episodecode;

/*View view_dxtlastencounters*/
DROP VIEW IF EXISTS view_dxtlastencounters;
DROP TABLE IF EXISTS view_dxtlastencounters;
CREATE VIEW view_dxtlastencounters
AS
SELECT
	epd.autocode,
	epd.sysdate,
	epd.booking,
	epd.episodecode,
	epd.patientcode,
	epd.isprimary,
	epd.ipddiagnosistype,
	epd.diagnosiscode,
	dx.description AS diagnosisdescription,
	epd.indicatorcode,
	epd.billinggroupcode,
	epd.billingsubgroupcode,
	epd.history,
	epd.examination,
	epd.investigation,
	epd.treatments,
	epd.doctorcode,
	fs.description AS doctordescription,
	epd.referaldescription,
	epd.department,
	epd.patientweight,
	epd.patienttemperature,
	epd.wardcode,
	epd.roomcode,
	epd.bedcode,
	epd.userid,
	MAX(transdate) transdate
FROM dxtpatientdiagnoseslog AS epd
LEFT OUTER JOIN dxticddiagnoses dx ON epd.diagnosiscode=dx.code
LEFT OUTER JOIN facilitystaffs fs ON epd.doctorcode=fs.code
GROUP BY
	epd.episodecode;

/*View view_dxtpatientdiagnoses*/
DROP VIEW IF EXISTS view_dxtpatientdiagnoses;
DROP TABLE IF EXISTS view_dxtpatientdiagnoses;
CREATE VIEW view_dxtpatientdiagnoses
AS
SELECT
	ep.autocode,
	ep.episodecode,	
	ep.patientcode,	
	ep.isprimary,
	CASE WHEN ep.isprimary=1 THEN 'YES' ELSE 'NO' END AS isprimarydescription,
	ep.ipddiagnosistype,
	ep.diagnosiscode,
	dx.description AS diagnosisdescription,
	ep.indicatorcode,	
	fe.transdate AS firstencounterdate,
	le.transdate AS lastencounterdate,
	le.billinggroupcode,
	le.billingsubgroupcode,
	le.doctorcode,
	fs.description AS doctordescription,
	le.department,
	le.patientweight,
	le.patienttemperature,
	le.wardcode,
	fw.description AS warddescription,
	le.roomcode,
	fr.description AS roomdescription,
	le.bedcode,
	fb.description AS beddescription,
	le.userid
FROM dxtpatientdiagnoses AS ep
LEFT OUTER JOIN dxticddiagnoses AS dx ON ep.diagnosiscode=dx.code
LEFT OUTER JOIN view_dxtfirstencounters AS fe ON ep.episodecode=fe.episodecode
LEFT OUTER JOIN view_dxtlastencounters AS le ON ep.episodecode=le.episodecode
LEFT OUTER JOIN facilitystaffs AS fs ON le.doctorcode=fs.code
LEFT OUTER JOIN facilitywards AS fw ON le.wardcode=fw.code
LEFT OUTER JOIN facilitywardrooms AS fr ON le.roomcode=fr.code
LEFT OUTER JOIN facilitywardroombeds AS fb ON le.bedcode=fb.code;

/*View view_ipddischargewithdx*/
DROP VIEW IF EXISTS view_ipddischargewithdx;
DROP TABLE IF EXISTS view_ipddischargewithdx;
CREATE
    VIEW view_ipddischargewithdx
    AS   
SELECT 
	ds.sysdate,
	ds.transdate,
	ds.booking,
	ds.patientcode,
	DATE_ADD(ad.transdate, INTERVAL (EXTRACT(HOUR FROM ad.sysdate)*60+EXTRACT(MINUTE FROM ad.sysdate)) MINUTE) admissiondatetime,
	DATE_ADD(ds.transdate, INTERVAL (EXTRACT(HOUR FROM ds.sysdate)*60+EXTRACT(MINUTE FROM ds.sysdate)) MINUTE) dischargedatetime,
	DATEDIFF(ds.transdate, p.birthdate) patientage_days,
	ds.wardcode,
	fw.description AS warddescription,
	ds.dischargestatuscode,
	ds.diagnosiscode,
	dx.description	diagnosisdescription	
FROM ipddischargeslog ds
LEFT OUTER JOIN ipdadmissionslog ad ON ds.admissionid=ad.autocode
LEFT OUTER JOIN facilitywards AS fw ON ds.wardcode=fw.code
LEFT OUTER JOIN dxticddiagnoses dx ON ds.diagnosiscode=dx.code
LEFT OUTER JOIN patients p ON ds.patientcode=p.code
WHERE 
ds.transcode='Discharge';

/*View view_ipddeathsummary*/ 
DROP VIEW IF EXISTS view_ipddeathsummary;
DROP TABLE IF EXISTS view_ipddeathsummary;
CREATE
    VIEW view_ipddeathsummary 
    AS 
SELECT
	ds.transdate,
	ds.wardcode,
	ds.warddescription,
	ds.diagnosiscode,
	ds.diagnosisdescription,
	ds.admissiondatetime,
	ds.dischargedatetime,
	ds.patientage_days,
	ds.dischargestatuscode,
	DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
	+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
	+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime) minutesstayed,
	
	/*under 30days within 24hours*/
	CASE WHEN 	
		(DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
		+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
		+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime))<=24*60
		AND ds.patientage_days<30
	THEN 1
	ELSE 0
	END AS u30w24,
	
	/*under 30days after 24hours*/
	CASE WHEN 	
		(DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
		+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
		+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime))>24*60
		AND ds.patientage_days<30
	THEN 1
	ELSE 0
	END AS u30a24,
	
	/*30days-5yrs within 24hours*/
	CASE WHEN 	
		(DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
		+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
		+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime))<=24*60
		AND ds.patientage_days>=30 AND ds.patientage_days<5*365.25
	THEN 1
	ELSE 0
	END AS f30t5w24,
	
	/*30days-5yrs after 24hours*/
	CASE WHEN 	
		(DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
		+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
		+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime))>24*60
		AND ds.patientage_days>=30 AND ds.patientage_days<5*365.25
	THEN 1
	ELSE 0
	END AS f30t5a24,
	
	/*f5yrs within 24hours*/
	CASE WHEN 	
		(DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
		+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
		+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime))<=24*60
		AND ds.patientage_days>=5*365.25
	THEN 1
	ELSE 0
	END AS f5w24,
	
	/*f5yrs after 24hours*/
	CASE WHEN 	
		(DATEDIFF(ds.dischargedatetime,ds.admissiondatetime)*24*60
		+ EXTRACT(HOUR FROM ds.dischargedatetime)*60-EXTRACT(HOUR FROM ds.admissiondatetime)*60
		+ EXTRACT(MINUTE FROM ds.dischargedatetime)-EXTRACT(MINUTE FROM ds.admissiondatetime))>24*60
		AND ds.patientage_days>=5*365.25
	THEN 1
	ELSE 0
	END AS f5a24	
FROM view_ipddischargewithdx AS ds;

/*View view_dxtpatientdiagnoseslog*/
DROP VIEW IF EXISTS view_dxtpatientdiagnoseslog;
DROP TABLE IF EXISTS view_dxtpatientdiagnoseslog;
CREATE VIEW view_dxtpatientdiagnoseslog
AS
SELECT
	epd.autocode,
	epd.sysdate,
	epd.transdate,
	epd.booking,
	epd.episodecode,	
	epd.patientcode,	
	epd.isprimary,
	epd.ipddiagnosistype,
	epd.diagnosiscode,
	dx.description AS diagnosisdescription,
	epd.indicatorcode,
	epd.billinggroupcode,
	epd.billingsubgroupcode,
	epd.history,
	epd.examination,
	epd.investigation,
	epd.treatments,
	epd.doctorcode,
	fs.description AS doctordescription,
	epd.referaldescription,
	epd.department,
	epd.patientweight,
	epd.patienttemperature,
	epd.wardcode,
	fw.description AS warddescription,
	epd.roomcode,
	fr.description AS roomdescription,
	epd.bedcode,
	fb.description AS beddescription,
	epd.userid
FROM dxtpatientdiagnoseslog AS epd
LEFT OUTER JOIN dxticddiagnoses AS dx ON epd.diagnosiscode=dx.code
LEFT OUTER JOIN facilitystaffs AS fs ON epd.doctorcode=fs.code
LEFT OUTER JOIN facilitywards AS fw ON epd.wardcode=fw.code
LEFT OUTER JOIN facilitywardrooms AS fr ON epd.roomcode=fr.code
LEFT OUTER JOIN facilitywardroombeds AS fb ON epd.bedcode=fb.code;

/*View view_dxtpatientprescriptions*/
DROP VIEW IF EXISTS view_dxtpatientprescriptions;
DROP TABLE IF EXISTS view_dxtpatientprescriptions;
CREATE VIEW view_dxtpatientprescriptions
AS
SELECT
    fo.facilitycode,
    fo.facilitydescription,
    fo.box,
    fo.street,
    fo.teleno,
    fo.headname,
    fo.headdesignation,
    fo.countrycode,
    fo.countrydescription,
    fo.regioncode,
    fo.regiondescription,
    fo.districtcode,
    fo.districtdescription,
	p.autocode,
	p.sysdate,
	p.transdate,
	p.patientcode,
	pt.firstname,
	pt.surname,
	pt.othernames,
	CONCAT(pt.firstname,' ',pt.othernames,' ',pt.surname) patientfullname,
	pt.gender,
	CAST(CONCAT(CAST(DATEDIFF(p.transdate,pt.birthdate)/365.25 - (DATEDIFF(p.transdate,pt.birthdate)/365.25 MOD 1) AS SIGNED), ' yrs ', CAST((DATEDIFF(p.transdate,pt.birthdate)/365.25 MOD 1) * 12 - ((DATEDIFF(p.transdate,pt.birthdate)/365.25 MOD 1) * 12 MOD 1) AS SIGNED), ' mts') AS CHAR) AS patientage,	
	p.diagnosiscode,
	dx.description AS diagnosisdescription,
	p.doctorcode,
	fs.description AS doctordescription,
	p.storecode,
	st.description AS storedescription,
	p.itemcode,
	pr.description AS itemdescription,
	CASE WHEN pr.displayname='' OR pr.displayname IS NULL THEN pr.description ELSE pr.displayname END AS itemdisplayname,
	p.qty,
	CASE WHEN p.qty=0 THEN 0 ELSE p.amount/p.qty END AS unitprice,
	p.amount,
	p.dosage,
	p.printed,
	p.userid
FROM facilityoptions AS fo, dxtpatientprescriptions AS p
LEFT OUTER JOIN patients AS pt ON p.patientcode=pt.code
LEFT OUTER JOIN dxticddiagnoses AS dx ON p.diagnosiscode=dx.code
LEFT OUTER JOIN facilitystaffs AS fs ON p.doctorcode=fs.code
LEFT OUTER JOIN som_stores AS st ON p.storecode=st.code
LEFT OUTER JOIN som_products AS pr ON p.itemcode=pr.code;

/*View view_dxtprescribedpatients*/
DROP VIEW IF EXISTS view_dxtprescribedpatients;
DROP TABLE IF EXISTS view_dxtprescribedpatients;
CREATE VIEW view_dxtprescribedpatients
AS
SELECT
	DISTINCT(patientcode) patientcode,
	firstname,
	othernames,
	surname,
	patientfullname,
	gender,
	patientage,
	transdate,
	printed,
	CASE WHEN printed=1 THEN 'YES' ELSE 'NO' END AS printeddescription
FROM view_dxtpatientprescriptions AS d
ORDER BY d.sysdate;

/*View view_labmonthlyresultscount*/
DROP VIEW IF EXISTS view_labmonthlyresultscount;
DROP TABLE IF EXISTS view_labmonthlyresultscount;
CREATE VIEW view_labmonthlyresultscount
AS
SELECT
	YEAR(l.transdate) AS yearpart,
	MONTH(l.transdate) AS monthpart,
	MONTHNAME(l.transdate) AS monthpartname,
	l.labtestgroupcode,
	g.description AS labtestgroupdescription,
	l.labtestsubgroupcode,
	CASE WHEN sg.description IS NULL THEN '' ELSE sg.description END AS labtestsubgroupdescription,
	l.labtesttypecode,
	t.description AS labtesttypedescription,
	l.results,
	SUM(CASE WHEN DATEDIFF(l.transdate, p.birthdate)/365.25<18 AND p.gender='M' THEN 1 ELSE 0 END) AS maleu18,
	SUM(CASE WHEN DATEDIFF(l.transdate, p.birthdate)/365.25<18 AND p.gender='F' THEN 1 ELSE 0 END) AS femaleu18,
	SUM(CASE WHEN DATEDIFF(l.transdate, p.birthdate)/365.25<18 THEN 1 ELSE 0 END) AS totalu18,
	SUM(CASE WHEN DATEDIFF(l.transdate, p.birthdate)/365.25>=18 AND p.gender='M' THEN 1 ELSE 0 END) AS male18,
	SUM(CASE WHEN DATEDIFF(l.transdate, p.birthdate)/365.25>=18 AND p.gender='F' THEN 1 ELSE 0 END) AS female18,
	SUM(CASE WHEN DATEDIFF(l.transdate, p.birthdate)/365.25>=18 THEN 1 ELSE 0 END) AS total18,
	SUM(CASE WHEN p.gender='M' THEN 1 ELSE 0 END) AS totalmale,
	SUM(CASE WHEN p.gender='F' THEN 1 ELSE 0 END) AS totalfemale,
	SUM(1) AS total	
FROM labpatienttests AS l
LEFT OUTER JOIN patients AS p ON l.patientcode=p.code
LEFT OUTER JOIN labtestgroups AS g ON l.labtestgroupcode=g.code
LEFT OUTER JOIN labtestsubgroups AS sg ON l.labtestsubgroupcode=sg.code
LEFT OUTER JOIN labtests AS t ON l.labtesttypecode=t.code
GROUP BY
	YEAR(l.transdate),
	MONTH(l.transdate),
	l.labtestgroupcode,
	l.labtestsubgroupcode,
	l.labtesttypecode,
	l.results;

/*View view_labresultslist*/
DROP VIEW IF EXISTS view_labresultslist;
DROP TABLE IF EXISTS view_labresultslist;
CREATE VIEW view_labresultslist
AS
SELECT pt.labtesttypecode, t.description AS labtesttypedescription, pt.description, 1 AS displayorder FROM labtestdropdownvalues AS pt
LEFT OUTER JOIN labtests AS t ON pt.labtesttypecode=t.code WHERE pt.description<>''

UNION

SELECT CODE AS labtesttypecode, description AS labtesttypedescription, 'Positive' AS description, 2 AS displayorder FROM labtests WHERE resulttype=0

UNION

SELECT CODE AS labtesttypecode, description AS labtesttypedescription, 'Negative' AS description, 3 AS displayorder FROM labtests WHERE resulttype=0

UNION

SELECT DISTINCT(pt.labtesttypecode) AS labtesttypecode, t.description AS labtesttypedescription, pt.results AS description, 4 AS displayorder FROM labpatienttests AS pt
LEFT OUTER JOIN labtests AS t ON pt.labtesttypecode=t.code
WHERE t.resulttype<>0 AND t.resulttype<>3;

/*View view_ctc_attendancecount*/
DROP VIEW IF EXISTS view_ctc_attendancecount;
DROP TABLE IF EXISTS view_ctc_attendancecount;
CREATE VIEW view_ctc_attendancecount
AS
SELECT
	'NEW' AS visittype,
	b.bookdate AS transdate,
	DATEDIFF(b.bookdate, p.birthdate)/365.25 AS patientage,
	CASE WHEN t.testresultgiven=1 AND p.gender='m' THEN 1 ELSE 0 END AS pmale,
	CASE WHEN t.testresultgiven=1 AND p.gender='f' THEN 1 ELSE 0 END AS pfemale,
	CASE WHEN t.testresultgiven=1 THEN 1 ELSE 0 END AS ptotal,
	CASE WHEN (t.testresultgiven=2 OR t.testresultgiven IS NULL) AND p.gender='m' THEN 1 ELSE 0 END AS nmale,
	CASE WHEN (t.testresultgiven=2 OR t.testresultgiven IS NULL) AND p.gender='f' THEN 1 ELSE 0 END AS nfemale,
	CASE WHEN (t.testresultgiven=2 OR t.testresultgiven IS NULL) THEN 1 ELSE 0 END AS ntotal,
	CASE WHEN p.gender='m' THEN 1 ELSE 0 END AS totalmale,
	CASE WHEN p.gender='f' THEN 1 ELSE 0 END AS totalfemale,
	CASE WHEN p.gender IN ('m','f') THEN 1 ELSE 0 END AS total	
FROM ctc_bookinglog AS b
LEFT OUTER JOIN patients AS p ON b.patientcode=p.code
LEFT OUTER JOIN ctc_hivtests AS t ON b.patientcode=t.patientcode AND b.booking=t.booking
WHERE b.registrystatus='new'

UNION

SELECT
	'RE-ATTENDANCE' AS visittype,
	b.bookdate AS transdate,
	DATEDIFF(b.bookdate, p.birthdate)/365.25 AS patientage,
	CASE WHEN t.testresultgiven=1 AND p.gender='m' THEN 1 ELSE 0 END AS pmale,
	CASE WHEN t.testresultgiven=1 AND p.gender='f' THEN 1 ELSE 0 END AS pfemale,
	CASE WHEN t.testresultgiven=1 THEN 1 ELSE 0 END AS ptotal,
	CASE WHEN (t.testresultgiven=2 OR t.testresultgiven IS NULL) AND p.gender='m' THEN 1 ELSE 0 END AS nmale,
	CASE WHEN (t.testresultgiven=2 OR t.testresultgiven IS NULL) AND p.gender='f' THEN 1 ELSE 0 END AS nfemale,
	CASE WHEN (t.testresultgiven=2 OR t.testresultgiven IS NULL) THEN 1 ELSE 0 END AS ntotal,
	CASE WHEN p.gender='m' THEN 1 ELSE 0 END AS totalmale,
	CASE WHEN p.gender='f' THEN 1 ELSE 0 END AS totalfemale,
	CASE WHEN p.gender IN ('m','f') THEN 1 ELSE 0 END AS total	
FROM ctc_bookinglog AS b
LEFT OUTER JOIN patients AS p ON b.patientcode=p.code
LEFT OUTER JOIN ctc_hivtests AS t ON b.patientcode=t.patientcode AND b.booking=t.booking
WHERE b.registrystatus='re_visiting';

/*View view_ctc_attendancecd4t*/
DROP VIEW IF EXISTS view_ctc_attendancecd4t;
DROP TABLE IF EXISTS view_ctc_attendancecd4t;
CREATE VIEW view_ctc_attendancecd4t
AS
SELECT
	t.transdate,
	DATEDIFF(t.transdate, p.birthdate)/365.25 AS patientage,
	CASE WHEN t.onarv=1 AND p.gender='m' THEN 1 ELSE 0 END AS pmale,
	CASE WHEN t.onarv=1 AND p.gender='f' THEN 1 ELSE 0 END AS pfemale,
	CASE WHEN t.onarv=1 THEN 1 ELSE 0 END AS ptotal,
	CASE WHEN t.onarv=0 AND p.gender='m' THEN 1 ELSE 0 END AS nmale,
	CASE WHEN t.onarv=0 AND p.gender='f' THEN 1 ELSE 0 END AS nfemale,
	CASE WHEN t.onarv=0 THEN 1 ELSE 0 END AS ntotal,
	CASE WHEN p.gender='m' THEN 1 ELSE 0 END AS totalmale,
	CASE WHEN p.gender='f' THEN 1 ELSE 0 END AS totalfemale,
	CASE WHEN p.gender IN ('m','f') THEN 1 ELSE 0 END AS total	
FROM ctc_cd4tests AS t
LEFT OUTER JOIN patients AS p ON t.patientcode=p.code;

/*View view_ctc_pmtct*/
DROP VIEW IF EXISTS view_ctc_pmtct;
DROP TABLE IF EXISTS view_ctc_pmtct;
CREATE VIEW view_ctc_pmtct
AS
SELECT
	p.autocode,
	p.patientcode,
	p.booking,
	p.startdate,
	p.anccardno,
	p.gestationage,
	p.knownhivtestresult,
	p.precounsellingdate,
	p.hivtestdate,
	p.hivtestresult,
	p.postcounsellingdate,
	p.partnerhivtestdate,
	p.partnerhivtestresult,
	p.remarks
FROM ctc_pmtct p;


/*View view_ctc_pmtctcarevisits*/
DROP VIEW IF EXISTS view_ctc_pmtctcarevisits;
DROP TABLE IF EXISTS view_ctc_pmtctcarevisits;
CREATE VIEW view_ctc_pmtctcarevisits
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.isfirstvisit,
	CASE p.isfirstvisit
		WHEN 0 THEN 'NO'
		WHEN 1 THEN 'YES'
	END AS isfirstvisitdescription,
	p.gestationage,
	p.transferindate,
	p.infantfeedchoice,
	CASE p.infantfeedchoice
		WHEN 0 THEN 'BF'
		WHEN 1 THEN 'RF'
	END AS infantfeedchoicedescription,
	p.infantfeedcounseling,
	CASE p.infantfeedcounseling
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS infantfeedcounselingdescription,
	p.artregimencode,
	com.description artregimendescription,
	p.adherenceanddisclosure,
	CASE p.adherenceanddisclosure
		WHEN 0 THEN 'A'
		WHEN 1 THEN 'D'
	END AS adherenceanddisclosuredescription,
	p.adherence,
	CASE p.adherence
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS adherencedescription,
	p.tbrxstartdate,
	p.tbrxstopdate,
	p.ctxstartdate,
	p.ctxstopdate,
	p.clinicalstage,
	p.clinicalstagedate,
	p.whyeligible,
	CASE p.whyeligible
		WHEN 0 THEN 'Clinical'
		WHEN 1 THEN 'CD4 #'
		WHEN 2 THEN 'TLC'
	END AS whyeligibledescription,
	p.cd4count,
	p.remarks,
	p.userid
FROM ctc_pmtctcarevisits p LEFT OUTER JOIN
	ctc_pmtctcomb com ON p.artregimencode = com.code LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_ctc_pmtctlabouranddelivery*/
DROP VIEW IF EXISTS view_ctc_pmtctlabouranddelivery;
DROP TABLE IF EXISTS view_ctc_pmtctlabouranddelivery;
CREATE VIEW view_ctc_pmtctlabouranddelivery
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN 0 ELSE ta.weight END AS patientweight,
	CASE WHEN ta.height IS NULL THEN 0 ELSE ta.height END AS patientheight,
	p.admissiondate,
	p.hivstatusfromanc,
	hsa.description AS hivstatusfromancdescription,
	p.hivresultlnd,
	hslnd.description AS hivresultlnddescription,
	p.arvduringanc,
	com.description AS arvduringancdescription,
	p.arvduringlabour,
	coml.description AS arvduringlabourdescription,
	p.infantdosereceived,
	idr.description AS infantdosereceiveddescription,
	p.infantdosedispensed,
	idd.description AS infantdosedispenseddescription,
	p.infantfeeding,
	inf.description AS infantfeedingdescription,
	p.linkage,
	CASE p.linkage
		WHEN -1 THEN 'None'
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS linkagedescription,
	p.remarks,
	p.userid
FROM ctc_pmtctlabouranddelivery p LEFT OUTER JOIN
	ctc_pmtcthivstatus hsa ON p.hivstatusfromanc = hsa.code LEFT OUTER JOIN
	ctc_pmtcthivstatus hslnd ON p.hivresultlnd = hslnd.code LEFT OUTER JOIN
	ctc_pmtctcomb com ON p.arvduringanc = com.code LEFT OUTER JOIN
	ctc_pmtctcomblabour coml ON p.arvduringlabour = coml.code LEFT OUTER JOIN
	ctc_pmtctinfantdosereceived idr ON p.infantdosereceived = idr.code LEFT OUTER JOIN
	ctc_pmtctinfantdosedispensed idd ON p.infantdosedispensed = idd.code LEFT OUTER JOIN
	ctc_pmtctinfantfeeding inf ON p.infantfeeding = inf.code LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;

/*View view_ctc_pmtctmotherchild*/
DROP VIEW IF EXISTS view_ctc_pmtctmotherchild;
DROP TABLE IF EXISTS view_ctc_pmtctmotherchild;
CREATE VIEW view_ctc_pmtctmotherchild
AS
SELECT
	p.autocode,
	p.booking,
	p.patientcode,
	p.arvmotherlabour,
	CASE p.arvmotherlabour
		WHEN 0 THEN ''
		WHEN 1 THEN 'NVP'
		WHEN 2 THEN 'AZT+3TC+NVP'
		WHEN 3 THEN 'ART'
		WHEN 4 THEN 'None'
	END AS arvmotherlabourdescription,	
	p.familyplanmethod,
	CASE p.familyplanmethod
		WHEN 0 THEN ''
		WHEN 1 THEN 'Hormonal'
		WHEN 2 THEN 'Condoms'
		WHEN 3 THEN 'Other'
		WHEN 4 THEN 'None'
	END AS familyplanmethoddescription,	
	p.outcomeat6months,
	CASE p.outcomeat6months
		WHEN 0 THEN ''
		WHEN 1 THEN 'Alive'
		WHEN 2 THEN 'Lost to follow up'
		WHEN 3 THEN 'Transfer out'
		WHEN 4 THEN 'Dead'
	END AS outcomeat6monthsdescription,		
	p.under5regno,
	p.childbirthdate,
	p.childbirthweight,
	p.infantfeeding,
	CASE p.infantfeeding
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS infantfeedingdescription,
	p.infantfeedingpractice,
	CASE p.infantfeedingpractice
		WHEN 0 THEN 'NONE'
		WHEN 1 THEN 'EBF'
		WHEN 2 THEN 'RF'
		WHEN 3 THEN 'Other'
	END AS infantfeedingpracticedescription,		
	p.oncotrim,
	CASE p.oncotrim
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS oncotrimdescription,	
	p.cotrimdate,
	CAST(CONCAT(CAST(DATEDIFF(p.cotrimdate,p.childbirthdate)/365.25 - (DATEDIFF(p.cotrimdate,p.childbirthdate)/365.25 MOD 1) AS SIGNED), ' yrs ', CAST((DATEDIFF(p.cotrimdate,p.childbirthdate)/365.25 MOD 1) * 12 - ((DATEDIFF(p.cotrimdate,p.childbirthdate)/365.25 MOD 1) * 12 MOD 1) AS SIGNED), ' mts') AS CHAR) AS cotrimage,
	p.infantarv,
	CASE p.infantarv
		WHEN 0 THEN ''
		WHEN 1 THEN 'NVP'
		WHEN 2 THEN 'NVP+AZT'
		WHEN 3 THEN 'None'
	END AS infantarvdescription,
	p.firsttestdate,
	p.firstPCRTest,
	CASE p.firstPCRTest
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS firstPCRTestdescription,	
	p.firstTestResult,
	CASE p.firstTestResult
		WHEN 0 THEN ''
		WHEN 1 THEN 'P'
		WHEN 2 THEN 'N'
		WHEN 3 THEN 'U'
	END AS firstTestResultdescription,	
	CAST(CONCAT(CAST(DATEDIFF(p.firstTestResult,p.childbirthdate)/365.25 - (DATEDIFF(p.firstTestResult,p.childbirthdate)/365.25 MOD 1) AS SIGNED), ' yrs ', CAST((DATEDIFF(p.firstTestResult,p.childbirthdate)/365.25 MOD 1) * 12 - ((DATEDIFF(p.firstTestResult,p.childbirthdate)/365.25 MOD 1) * 12 MOD 1) AS SIGNED), ' mts') AS CHAR) AS firstTestAge
FROM ctc_pmtctmotherchild p;


/*View view_ctc_pmtctmotherchildvisits*/
DROP VIEW IF EXISTS view_ctc_pmtctmotherchildvisits;
DROP TABLE IF EXISTS view_ctc_pmtctmotherchildvisits;
CREATE VIEW view_ctc_pmtctmotherchildvisits
AS
SELECT
	p.autocode,
	p.sysdate,
	p.transdate,
	p.booking,
	p.patientcode,
	CASE WHEN ta.weight IS NULL THEN p.weight ELSE ta.weight END AS patientweight,
	p.infantfeeding,
	CASE p.infantfeeding
		WHEN 0 THEN ''
		WHEN 1 THEN 'EBF'
		WHEN 2 THEN 'RF'
		WHEN 3 THEN 'Other'
	END AS infantfeedingdescription,
	p.oncotrim,
	CASE p.oncotrim
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS oncotrimdescription,
	p.hivtesttype,
	CASE p.hivtesttype
		WHEN 0 THEN ''
		WHEN 1 THEN 'First'
		WHEN 2 THEN 'Second'
		WHEN 3 THEN 'Confirm'
	END AS hivtesttypedescription,
	p.hivtest,
	CASE p.hivtest
		WHEN 0 THEN ''
		WHEN 1 THEN 'Ab'
		WHEN 2 THEN 'PCR'
		WHEN 3 THEN 'None'
	END AS hivtestdescription,
	p.hivtestresult,
	CASE p.hivtestresult
		WHEN 0 THEN ''
		WHEN 1 THEN 'P'
		WHEN 2 THEN 'N'
		WHEN 3 THEN 'U'
	END AS hivtestresultdescription,
	p.hivtestdate,
	p.referedtoctc,
	CASE p.referedtoctc
		WHEN 0 THEN 'N'
		WHEN 1 THEN 'Y'
	END AS referedtoctcdescription,	
	p.remarks,
	p.userid
FROM ctc_pmtctmotherchildvisits p LEFT OUTER JOIN
	ctc_triage ta ON p.booking = ta.booking AND p.patientcode = ta.patientcode;


/*View view_facilityoptions*/
DROP VIEW IF EXISTS view_facilityoptions;
DROP TABLE IF EXISTS view_facilityoptions;
CREATE VIEW view_facilityoptions
AS
SELECT
	f.autocode,
	f.facilitycode,
	f.box,
	f.street,
	f.teleno,
	f.countrycode,
	c.description AS countrydescription,
	f.regioncode,
	r.description AS regiondescription,
	f.districtcode,
	d.description AS districtdescription,
	f.headname,
	f.headdesignation,
	f.facilitytype,
	f.roundingoption,
	f.roundingfigure,
	f.roundingdecimals,
	f.roundingstrictness,
	f.roundingmidpointoption,
	f.affectstockatcashier,
	f.doubleentryissuing,
	f.transferoutrefreshinterval,
	f.charactercasingoptionpatientnames
FROM facilityoptions f
LEFT OUTER JOIN countries c ON f.countrycode=c.code
LEFT OUTER JOIN regions r ON f.regioncode=r.code
LEFT OUTER JOIN districts d ON f.districtcode=d.code;

/*View afyapro_dhis_view_under5*/  
DROP VIEW IF EXISTS afyapro_dhis_view_under5;     
DROP TABLE IF EXISTS afyapro_dhis_view_under5;     
CREATE 
	VIEW afyapro_dhis_view_under5 
	AS 
SELECT 
	mt.description element
	, mt.autocode elementID
	, DATEDIFF(dx.transdate, p.birthdate)/365.25 patientage
	, dx.transdate diagnosisdate
	, pr.periodid periodID
	, pr.startdate startdate
	, pr.enddate enddate
	, p.gender patientgender
	, COUNT(dx.indicatorcode) cases 
FROM 
	((dxtindicators mt JOIN view_dxtfirstencounters dx) JOIN afya_dhis_period pr) 
	LEFT OUTER JOIN patients p ON (dx.patientcode = p.code)
WHERE 
	((dx.indicatorcode = mt.code AND dx.ipddiagnosistype<>'admission') 
	AND (CAST(dx.transdate AS DATE) >= pr.startdate) 
	AND (CAST(dx.transdate AS DATE) <= pr.enddate) 
	AND (DATEDIFF(dx.transdate, p.birthdate)/365.25 <= 5)) 
GROUP BY 
	DATEDIFF(dx.transdate, p.birthdate)/365.25,
	mt.description 
ORDER BY 
	DATEDIFF(dx.transdate, p.birthdate)/365.25;  

/*View afyapro_dhis_view_above5*/   
DROP VIEW IF EXISTS afyapro_dhis_view_above5;	
DROP TABLE IF EXISTS afyapro_dhis_view_above5;	
CREATE 
	VIEW afyapro_dhis_view_above5 
	AS 
SELECT 
	mt.description element
	, mt.autocode elementID
	, DATEDIFF(dx.transdate, p.birthdate)/365.25 age
	, dx.transdate diagnosisdate
	, pr.periodid periodID
	, pr.startdate startdate
	, pr.enddate enddate
	, p.gender patientgender
	, COUNT(dx.indicatorcode) cases 
FROM 
	((dxtindicators mt JOIN view_dxtfirstencounters dx) JOIN afya_dhis_period pr) 
	LEFT OUTER JOIN patients p ON (dx.patientcode = p.code)
WHERE 
	((dx.indicatorcode = mt.code AND dx.ipddiagnosistype<>'admission') 
	AND (CAST(dx.transdate AS DATE) >= pr.startdate) 
	AND (CAST(dx.transdate AS DATE) <= pr.enddate) 
	AND (DATEDIFF(dx.transdate, p.birthdate)/365.25 > 5)) 
GROUP BY 
	DATEDIFF(dx.transdate, p.birthdate)/365.25,
	mt.description 
ORDER BY 
	DATEDIFF(dx.transdate, p.birthdate)/365.25;	       
	
/*View afyapro_dhis_view*/  	
DROP VIEW IF EXISTS afyapro_dhis_view;
DROP TABLE IF EXISTS afyapro_dhis_view;
CREATE 
	VIEW afyapro_dhis_view 
	AS 
SELECT 
	mt.description element
	, mt.autocode elementID
	, DATEDIFF(dx.transdate, p.birthdate)/365.25 age
	, dx.transdate diagnosisdate
	, pr.periodid periodID
	, pr.startdate startdate
	, pr.enddate enddate
	, p.gender patientgender
	, COUNT(dx.indicatorcode) cases 
FROM 
	((dxtindicators mt JOIN view_dxtfirstencounters dx) JOIN afya_dhis_period pr) 
	LEFT OUTER JOIN patients p ON (dx.patientcode = p.code)
WHERE 
	((dx.indicatorcode = mt.code AND dx.ipddiagnosistype<>'admission') 
	AND (CAST(dx.transdate AS DATE) >= pr.startdate) 
	AND (CAST(dx.transdate AS DATE) <= pr.enddate)) 
GROUP BY 
	DATEDIFF(dx.transdate, p.birthdate)/365.25,
	mt.description 
ORDER BY 
	DATEDIFF(dx.transdate, p.birthdate)/365.25;
	
/*view_dxtcountcasesbymonth*/
DROP VIEW IF EXISTS view_dxtcountcasesbymonth;
DROP TABLE IF EXISTS view_dxtcountcasesbymonth;
CREATE VIEW view_dxtcountcasesbymonth
	AS
SELECT
	YEAR(dx.transdate) transyear
	,dx.department
	,ind.code indicatorcode
	,ind.description indicatordescription
	,DATEDIFF(dx.transdate, p.birthdate)/365.25 patientage
	,SUM(CASE WHEN MONTH(dx.transdate)=1 THEN 1 ELSE 0 END) january
	,SUM(CASE WHEN MONTH(dx.transdate)=2 THEN 1 ELSE 0 END) february
	,SUM(CASE WHEN MONTH(dx.transdate)=3 THEN 1 ELSE 0 END) march
	,SUM(CASE WHEN MONTH(dx.transdate)=4 THEN 1 ELSE 0 END) april
	,SUM(CASE WHEN MONTH(dx.transdate)=5 THEN 1 ELSE 0 END) may
	,SUM(CASE WHEN MONTH(dx.transdate)=6 THEN 1 ELSE 0 END) june
	,SUM(CASE WHEN MONTH(dx.transdate)=7 THEN 1 ELSE 0 END) july
	,SUM(CASE WHEN MONTH(dx.transdate)=8 THEN 1 ELSE 0 END) augost
	,SUM(CASE WHEN MONTH(dx.transdate)=9 THEN 1 ELSE 0 END) september
	,SUM(CASE WHEN MONTH(dx.transdate)=10 THEN 1 ELSE 0 END) october
	,SUM(CASE WHEN MONTH(dx.transdate)=11 THEN 1 ELSE 0 END) november
	,SUM(CASE WHEN MONTH(dx.transdate)=12 THEN 1 ELSE 0 END) december
	,SUM(CASE WHEN MONTH(dx.transdate) IN (1,2,3) THEN 1 ELSE 0 END) quarter1
	,SUM(CASE WHEN MONTH(dx.transdate) IN (4,5,6) THEN 1 ELSE 0 END) quarter2
	,SUM(CASE WHEN MONTH(dx.transdate) IN (7,8,9) THEN 1 ELSE 0 END) quarter3
	,SUM(CASE WHEN MONTH(dx.transdate) IN (10,11,12) THEN 1 ELSE 0 END) quarter4
	,SUM(CASE WHEN MONTH(dx.transdate) IN (1,2,3,4,5,6,7,8,9,10,11,12) THEN 1 ELSE 0 END) yeartotal
FROM dxtindicators ind JOIN view_dxtfirstencounters dx ON ind.code=dx.indicatorcode
	LEFT OUTER JOIN patients p ON (dx.patientcode = p.code)
GROUP BY 
	transyear
	,dx.department
	,ind.code
	,patientage;	