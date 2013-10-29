/*View view_billinvoicedoc*/ 
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
    , bi.patientsurname
    , bi.patientfirstname
    , bi.patientothernames
    , CONCAT(bi.patientfirstname,' ',bi.patientothernames,' ',bi.patientsurname) patientfullname
    , bi.patientgender
    , bi.patientbirthdate
    ,(YEAR(bi.transdate)-YEAR(bi.patientbirthdate))-(RIGHT(bi.transdate,5)<RIGHT(bi.patientbirthdate,5)) patientage
    , bi.patientstreet
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
    INNER JOIN billinvoiceitems bii
        ON (bi.invoiceno = bii.invoiceno);         
        
/*View view_billreceiptdoc*/
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
    , br.patientsurname
    , br.patientfirstname
    , br.patientothernames
    , CONCAT(br.patientfirstname,' ',br.patientothernames,' ',br.patientsurname) patientfullname
    , br.patientgender
    , br.patientbirthdate
    ,(YEAR(br.transdate)-YEAR(br.patientbirthdate))-(RIGHT(br.transdate,5)<RIGHT(br.patientbirthdate,5)) patientage
    , br.patientstreet
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
    INNER JOIN billreceiptitems bri
        ON (br.receiptno = bri.receiptno);    
        
/*View view_billinvoicepaymentdoc*/
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
    , bip.patientsurname
    , bip.patientfirstname
    , bip.patientothernames
    , CONCAT(bip.patientfirstname,' ',bip.patientothernames,' ',bip.patientsurname) patientfullname
    , bip.patientgender
    , bip.patientbirthdate
    ,(YEAR(bip.transdate)-YEAR(bip.patientbirthdate))-(RIGHT(bip.transdate,5)<RIGHT(bip.patientbirthdate,5)) patientage
    , bip.patientstreet
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
    INNER JOIN billinvoicepaymentdetails bipd
        ON (bip.receiptno = bipd.receiptno);                      
        
/*View view_regpatientdetails*/
CREATE
    VIEW view_regpatientdetails 
    AS         
SELECT
    pa.code
    , pa.surname
    , pa.firstname
    , pa.othernames
    , CONCAT(pa.firstname,' ',pa.othernames,' ',pa.surname) fullname
    , pa.street
    , pa.gender
    , pa.birthdate
    ,(YEAR(pa.regdate)-YEAR(pa.birthdate))-(RIGHT(pa.regdate,5)<RIGHT(pa.birthdate,5)) patientage
    , pa.regdate
    , pa.plotno
    , pa.pobox
    , pa.ward
    , pa.village
    , pa.complexion
    , pa.bloodgroup
    , pa.eyecolor
    , pa.ethnicity
    , pa.religion
    , pa.hair
    , pa.maritalstatus
    , pa.occupation
    , pa.nextofkin
FROM
    patients pa;       
    
/*View view_regbookinglist*/
CREATE
    VIEW view_regbookinglist 
    AS
SELECT  
    fb.patientcode
    , fb.patientsurname
    , fb.patientfirstname
    , fb.patientothernames
    , CONCAT(fb.patientfirstname,' ',fb.patientothernames,' ',fb.patientsurname) patientfullname
    , fb.patientgender
    , fb.patientbirthdate
    , fb.patientcountry
    , fb.patientregion
    , fb.patientdistrict
    , fb.patientward
    , fb.patientvillage
    , fb.patientwardhead
    , fb.patientvillagehead
    , fb.patientnationality
    , fb.patientstreet
    , fb.patientcomplexion
    , fb.patientbloodgroup
    , fb.patienteyecolor
    , fb.patientethnicity
    , fb.patientreligion
    , fb.patienthair
    , fb.patientoccupation
    , fb.patientnextofkin
    ,(YEAR(fb.bookdate)-YEAR(fb.patientbirthdate))-(RIGHT(fb.bookdate,5)<RIGHT(fb.patientbirthdate,5)) patientage
    , fb.patientregdate
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
    , fb.billinggroupmembershipno
    , fb.weight
    , fb.temperature
    , fb.registrystatus
FROM
    facilitybookinglog fb;
       
/*View view_regbookinggendercounter*/
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
	, fb.patientstreet
	, fb.weight
	, fb.temperature
	, SUM(CASE WHEN fb.patientgender='M' AND fb.registrystatus='new' THEN 1 ELSE 0 END) new_males
	, SUM(CASE WHEN fb.patientgender='M' AND fb.registrystatus='re_visiting' THEN 1 ELSE 0 END) reattendance_males
	, SUM(CASE WHEN fb.patientgender='F' AND fb.registrystatus='new' THEN 1 ELSE 0 END) new_females
	, SUM(CASE WHEN fb.patientgender='F' AND fb.registrystatus='re_visiting' THEN 1 ELSE 0 END) reattendance_females
FROM 
	facilitybookinglog fb 
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
	, fb.patientstreet
	, fb.weight
	, fb.temperature;

/*View view_billsalesdetails*/
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
CREATE
    VIEW view_billinvoicesdetails 
    AS  	
SELECT
    bi.patientcode
    , bi.patientsurname
    , bi.patientfirstname
    , bi.patientothernames
    , bi.patientgender
    , bi.patientbirthdate
    , bi.patientstreet
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
    billinvoices bi;	   
    
/*View view_billdebtorslist*/
CREATE
    VIEW view_billdebtorslist
    AS 
SELECT
    db.accountcode
    , db.accountdescription
    , db.debtortype
    , db.balance
    , pa.country
    , pa.region
    , pa.district
    , pa.nationality
    , pa.ward
    , pa.village
    , pa.wardhead
    , pa.villagehead
    , pa.street
FROM
    billdebtors db
    LEFT JOIN patients pa ON db.accountcode=pa.code AND db.debtortype='individual'    
    
/*View view_ipdadmissionslog*/
CREATE
    VIEW view_ipdadmissionslog
    AS     
SELECT
    ad.patientcode
    , ad.patientsurname
    , ad.patientfirstname
    , ad.patientothernames
    , CONCAT(ad.patientfirstname,' ',ad.patientothernames,' ',ad.patientsurname) patientfullname
    , ad.patientstreet
    , ad.patientgender
    , ad.patientbirthdate
    , ad.patientcondition
    , ad.patientregdate
    , ad.patientcountry
    , ad.patientregion
    , ad.patientdistrict
    , ad.patientward
    , ad.patientvillage
    , ad.patientwardhead
    , ad.patientvillagehead
    , ad.patientnationality
    , ad.patientcomplexion
    , ad.patientbloodgroup
    , ad.patienteyecolor
    , ad.patientethnicity
    , ad.patientreligion
    , ad.patienthair
    , ad.patientoccupation
    , ad.patientnextofkin
    ,(YEAR(ad.transdate)-YEAR(ad.patientbirthdate))-(RIGHT(ad.transdate,5)<RIGHT(ad.patientbirthdate,5)) patientage
    , ad.booking
    , ad.transdate
    , ad.transcode
    , ad.wardcode
    , ad.warddescription
    , ad.roomcode
    , ad.roomdescription
    , ad.bed
    , ad.registrystatus
    , ad.userid
    , ad.yearpart
    , ad.monthpart
FROM
    ipdadmissionslog ad;   
    
/*View view_ipddischargeslog*/
CREATE
    VIEW view_ipddischargeslog
    AS     
SELECT
    ds.patientcode
    , ds.patientsurname
    , ds.patientfirstname
    , ds.patientothernames
    , CONCAT(ds.patientfirstname,' ',ds.patientothernames,' ',ds.patientsurname) patientfullname
    , ds.patientstreet
    , ds.patientgender
    , ds.patientbirthdate
    , ds.patientcondition
    , ds.patientregdate
    , ds.patientcountry
    , ds.patientregion
    , ds.patientdistrict
    , ds.patientward
    , ds.patientvillage
    , ds.patientwardhead
    , ds.patientvillagehead
    , ds.patientnationality
    , ds.patientcomplexion
    , ds.patientbloodgroup
    , ds.patienteyecolor
    , ds.patientethnicity
    , ds.patientreligion
    , ds.patienthair
    , ds.patientoccupation
    , ds.patientnextofkin
    ,(YEAR(ds.transdate)-YEAR(ds.patientbirthdate))-(RIGHT(ds.transdate,5)<RIGHT(ds.patientbirthdate,5)) patientage
    , ds.booking
    , ds.transdate
    , ds.transcode
    , ds.wardcode
    , ds.warddescription
    , ds.roomcode
    , ds.roomdescription
    , ds.bed
    , ds.registrystatus
    , ds.userid
    , ds.dischargestatuscode
    , ds.dischargestatusdescription
    , ds.dischargeremarks
    , ds.yearpart
    , ds.monthpart
FROM
    ipddischargeslog ds;        
    
/*View view_ipdtransferslog*/
CREATE
    VIEW view_ipdtransferslog
    AS  
SELECT
    tl.transferdate
    , tl.booking
    , tl.patientcode
    , tl.patientsurname
    , tl.patientfirstname
    , tl.patientothernames
    , CONCAT(tl.patientfirstname,' ',tl.patientothernames,' ',tl.patientsurname) patientfullname
    , tl.patientstreet
    , tl.patientgender
    , tl.patientbirthdate
    , tl.patientregdate
    , tl.patientcountry
    , tl.patientregion
    , tl.patientdistrict
    , tl.patientward
    , tl.patientvillage
    , tl.patientwardhead
    , tl.patientvillagehead
    , tl.patientnationality
    , tl.patientcomplexion
    , tl.patientbloodgroup
    , tl.patienteyecolor
    , tl.patientethnicity
    , tl.patientreligion
    , tl.patienthair
    , tl.patientoccupation
    , tl.patientnextofkin
    ,(YEAR(tl.transferdate)-YEAR(tl.patientbirthdate))-(RIGHT(tl.transferdate,5)<RIGHT(tl.patientbirthdate,5)) patientage
    , tl.wardfromcode
    , tl.wardfromdescription
    , tl.wardtocode
    , tl.wardtodescription
    , tl.roomfromcode
    , tl.roomfromdescription
    , tl.roomtocode
    , tl.roomtodescription
    , tl.bedfrom
    , tl.bedto
    , tl.transfertofacility
    , tl.patientcondition
    , tl.yearpart
    , tl.monthpart
    , tl.registrystatus
    , tl.userid
FROM
    ipdtransferslog tl;     
    
/*View view_dxtpatientdiagnoseslog*/
CREATE
    VIEW view_dxtpatientdiagnoseslog
    AS     
SELECT
    dx.transdate
    , dx.booking
    , dx.incidencekey
    , dx.patientcode
    , dx.diagnosiscode
    , dx.diagnosisdescription
    , dx.followup
    , dx.doctorcode
    , dx.doctordescription
    , dx.history
    , dx.examination
    , dx.investigation
    , dx.treatments
    , dx.deathstatus
    , dx.referalno
    , dx.referaldescription
    , dx.department
    , dx.yearpart
    , dx.monthpart
    , dx.userid
    , dx.patientsurname
    , dx.patientfirstname
    , dx.patientothernames
    , CONCAT(dx.patientfirstname,' ',dx.patientothernames,' ',dx.patientsurname) patientfullname
    , dx.patientgender
    , dx.patientbirthdate
    , dx.patientregdate
    , dx.patientcountry
    , dx.patientregion
    , dx.patientdistrict
    , dx.patientward
    , dx.patientvillage
    , dx.patientwardhead
    , dx.patientvillagehead
    , dx.patientstreet
    , dx.patientnationality
    , dx.patientcomplexion
    , dx.patientbloodgroup
    , dx.patienteyecolor
    , dx.patientethnicity
    , dx.patientreligion
    , dx.patienthair
    , dx.patientoccupation
    , dx.patientnextofkin
    , dx.patientweight
    , dx.patienttemperature
    ,(YEAR(dx.transdate)-YEAR(dx.patientbirthdate))-(RIGHT(dx.transdate,5)<RIGHT(dx.patientbirthdate,5)) patientage
FROM
    dxtpatientdiagnoseslog dx;    
    
/*View view_purchaseorderdoc*/
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
   
/*View afyapro_dhis_view_under5*/       
CREATE 
	VIEW afyapro_dhis_view_under5 
	AS 
SELECT 
	mt.description element
	, mt.autocode elementID
	, (YEAR(CURDATE())-YEAR(dx.patientbirthdate))-(RIGHT(CURDATE(),5)<RIGHT(dx.patientbirthdate,5)) age
	, dx.transdate diagnosisdate
	, pr.periodid periodID
	, pr.startdate startdate
	, pr.enddate enddate
	, dx.patientgender patientgender
	, COUNT(dx.mtuhadiagnosiscode) cases 
FROM 
	((dxtmtuhadiagnoses mt JOIN dxtpatientdiagnoseslog dx) JOIN afya_dhis_period pr) 
WHERE 
	((dx.mtuhadiagnosiscode = mt.code AND dx.followup=0) 
	AND (CAST(dx.transdate AS DATE) >= pr.startdate) 
	AND (CAST(dx.transdate AS DATE) <= pr.enddate) 
	AND (((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5))) <= 5)) 
GROUP BY 
	((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5))),
	mt.description 
ORDER BY 
	((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5)));  

/*View afyapro_dhis_view_above5*/   	
CREATE 
	VIEW afyapro_dhis_view_above5 
	AS 
SELECT 
	mt.description element
	, mt.autocode elementID
	, (YEAR(CURDATE())-YEAR(dx.patientbirthdate))-(RIGHT(CURDATE(),5)<RIGHT(dx.patientbirthdate,5)) age
	, dx.transdate diagnosisdate
	, pr.periodid periodID
	, pr.startdate startdate
	, pr.enddate enddate
	, dx.patientgender patientgender
	, COUNT(dx.mtuhadiagnosiscode) cases 
FROM 
	((dxtmtuhadiagnoses mt JOIN dxtpatientdiagnoseslog dx) JOIN afya_dhis_period pr) 
WHERE 
	((dx.mtuhadiagnosiscode = mt.code AND dx.followup=0) 
	AND (CAST(dx.transdate AS DATE) >= pr.startdate) 
	AND (CAST(dx.transdate AS DATE) <= pr.enddate) 
	AND (((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5))) > 5)) 
GROUP BY 
	((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5))),
	mt.description 
ORDER BY 
	((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5)));	       
	
/*View afyapro_dhis_view*/  	
CREATE 
	VIEW afyapro_dhis_view 
	AS 
SELECT 
	mt.description element
	, mt.autocode elementID
	, (YEAR(CURDATE())-YEAR(dx.patientbirthdate))-(RIGHT(CURDATE(),5)<RIGHT(dx.patientbirthdate,5)) age
	, dx.transdate diagnosisdate
	, pr.periodid periodID
	, pr.startdate startdate
	, pr.enddate enddate
	, dx.patientgender patientgender
	, COUNT(dx.mtuhadiagnosiscode) cases 
FROM 
	((dxtmtuhadiagnoses mt JOIN dxtpatientdiagnoseslog dx) JOIN afya_dhis_period pr) 
WHERE 
	((dx.mtuhadiagnosiscode = mt.code AND dx.followup=0) 
	AND (CAST(dx.transdate AS DATE) >= pr.startdate) 
	AND (CAST(dx.transdate AS DATE) <= pr.enddate)) 
GROUP BY 
	((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5))),
	mt.description 
ORDER BY 
	((YEAR(CURDATE()) - YEAR(dx.patientbirthdate)) - (RIGHT(CURDATE(),5) < RIGHT(dx.patientbirthdate,5)));	
	
/*View facility_view*/  		
CREATE 
	VIEW facility_view 
	AS 
SELECT 
	fo.autocode autocode
	,fo.facilitydescription facilitydescription 
FROM facilityoptions fo;
	 