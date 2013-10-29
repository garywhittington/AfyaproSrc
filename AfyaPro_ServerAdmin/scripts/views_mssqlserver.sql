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
CREATE
    VIEW view_billinvoicedoc 
    AS       
SELECT
    bi.transdate
    , bi.invoiceno
    , bi.patientcode
    , bi.patientsurname
    , bi.patientfirstname
    , bi.patientothernames
    , bi.patientfirstname + ' ' + bi.patientothernames + ' ' + bi.patientsurname patientfullname
    , bi.patientgender
    , bi.patientbirthdate
    , Datediff(day, bi.patientbirthdate, bi.transdate)/365.25 patientage
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
    billinvoices bi
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
    , br.receiptno
    , br.patientcode
    , br.patientsurname
    , br.patientfirstname
    , br.patientothernames
    , br.patientfirstname + ' ' + br.patientothernames + ' ' + br.patientsurname patientfullname
    , br.patientgender
    , br.patientbirthdate
    , CONVERT(INT,Datediff(day, br.patientbirthdate, br.transdate)/365.25) patientage
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
    , bip.patientfirstname + ' ' + bip.patientothernames + ' ' + bip.patientsurname patientfullname
    , bip.patientgender
    , bip.patientbirthdate
    , CONVERT(INT,Datediff(day, bip.patientbirthdate, bip.transdate)/365.25) patientage
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
    , pa.firstname + ' ' + pa.othernames + ' ' + pa.surname fullname
    , pa.street
    , pa.gender
    , pa.birthdate
    , CONVERT(INT,Datediff(day, pa.birthdate, pa.regdate)/365.25) patientage
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
    , fb.patientfirstname + ' ' + fb.patientothernames + ' ' + fb.patientsurname patientfullname
    , fb.patientstreet
    , fb.patientgender
    , fb.patientbirthdate
    , CONVERT(INT,Datediff(day, fb.patientbirthdate, fb.bookdate)/365.25) patientage
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
    bs.patientcode
    , bs.patientsurname
    , bs.patientfirstname
    , bs.patientothernames
    , bs.patientgender
    , bs.patientbirthdate
    , bs.patientstreet
    , bsi.transdate
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
    billsalesitems bsi
    INNER JOIN billsales bs
        ON (bsi.receiptno = bs.receiptno);	