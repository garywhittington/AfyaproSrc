/*
SQLyog Enterprise - MySQL GUI v8.05 
MySQL - 5.0.21-community-nt : Database - nkhoma_data
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `afya_dhis_period` */

CREATE TABLE `afya_dhis_period` (
  `periodid` int(11) NOT NULL auto_increment,
  `periodtypeid` int(11) default NULL,
  `startdate` date NOT NULL,
  `enddate` date NOT NULL,
  PRIMARY KEY  (`periodid`),
  UNIQUE KEY `periodtypeid` (`periodtypeid`,`startdate`,`enddate`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billaccountfromtowhom` */

CREATE TABLE `billaccountfromtowhom` (
  `autocode` int(11) NOT NULL auto_increment,
  `description` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billaccounts` */

CREATE TABLE `billaccounts` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `code` varchar(50) default NULL,
  `description` varchar(255) default NULL,
  `balance` double(16,2) default '0.00',
  `allowoverdraft` int(11) default '0',
  `inactive` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billaccountslog` */

CREATE TABLE `billaccountslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `reference` varchar(50) default NULL,
  `accountcode` varchar(50) default NULL,
  `accountdescription` varchar(255) default NULL,
  `fromwhomtowhomcode` varchar(50) default NULL,
  `fromwhomtowhom` varchar(255) default NULL,
  `entryside` int(11) default '0',
  `transdescription` varchar(255) default NULL,
  `debitamount` double(16,2) default '0.00',
  `creditamount` double(16,2) default '0.00',
  `userid` varchar(50) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billaccounttransactions` */

CREATE TABLE `billaccounttransactions` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transactionid` varchar(50) default NULL,
  `accountcode` varchar(50) default NULL,
  `accountdescription` varchar(255) default NULL,
  `fromwhomtowhomcode` varchar(50) default NULL,
  `fromwhomtowhom` varchar(255) default NULL,
  `entryside` int(11) default '0',
  `transdescription` varchar(255) default NULL,
  `transamount` double(16,2) default '0.00',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billaccountusers` */

CREATE TABLE `billaccountusers` (
  `autocode` int(11) NOT NULL auto_increment,
  `accountcode` varchar(50) default '',
  `membercode` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billcollections` */

CREATE TABLE `billcollections` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `receiptno` varchar(50) default NULL,
  `paymentsource` int(11) default '0',
  `refunded` int(11) default '0',
  `refundsysdate` datetime default NULL,
  `refundtransdate` datetime default NULL,
  `refunduserid` varchar(50) default '',
  `patientcode` varchar(50) default NULL,
  `patientsurname` varchar(255) default NULL,
  `patientfirstname` varchar(255) default NULL,
  `patientothernames` varchar(255) default NULL,
  `patientgender` varchar(10) default NULL,
  `patientbirthdate` varchar(255) default NULL,
  `patientstreet` varchar(255) default NULL,
  `chequeno` varchar(255) default NULL,
  `bank` varchar(255) default NULL,
  `branch` varchar(255) default NULL,
  `holder` varchar(255) default NULL,
  `billinggroupcode` varchar(50) default NULL,
  `billinggroupdescription` varchar(255) default NULL,
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `transtype` int(11) default '0',
  `userid` varchar(50) default NULL,
  `paytype000001` double(16,2) default '0.00',
  `paytype000002` double(16,2) default '0.00',
  `paytype000003` double(16,2) default '0.00',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `paytype000004` double(16,2) default '0.00',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billdebtors` */

CREATE TABLE `billdebtors` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `accountcode` varchar(50) default '',
  `accountdescription` varchar(255) default '',
  `debtortype` varchar(50) default '',
  `balance` double(16,2) default '0.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billdebtorslog` */

CREATE TABLE `billdebtorslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `reference` varchar(50) default '',
  `accountcode` varchar(50) default '',
  `accountdescription` varchar(255) default '',
  `fromwhomtowhomcode` varchar(50) default '',
  `fromwhomtowhom` varchar(255) default '',
  `debtortype` varchar(50) default '',
  `transtype` int(11) default '2',
  `transdescription` varchar(255) default '',
  `debitamount` double(16,2) default '0.00',
  `creditamount` double(16,2) default '0.00',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billincomingitems` */

CREATE TABLE `billincomingitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `patientcode` varchar(50) default NULL,
  `itemgroupcode` varchar(50) default NULL,
  `itemgroupdescription` varchar(255) default NULL,
  `itemsubgroupcode` varchar(50) default NULL,
  `itemsubgroupdescription` varchar(255) default NULL,
  `itemcode` varchar(50) default NULL,
  `itemdescription` varchar(255) default NULL,
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default NULL,
  `storedescription` varchar(255) default NULL,
  `qty` double(11,2) default '0.00',
  `actualamount` double(16,2) default '0.00',
  `amount` double(16,2) default '0.00',
  `itemopmcode` varchar(50) default NULL,
  `itemopmdescription` varchar(255) default NULL,
  `packagingcode` varchar(50) default NULL,
  `packagingdescription` varchar(255) default NULL,
  `piecesinpackage` int(11) default '0',
  `userid` varchar(50) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billinvoiceitems` */

CREATE TABLE `billinvoiceitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `invoiceno` varchar(50) default '',
  `reference` varchar(50) default '',
  `voidno` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `itemgroupcode` varchar(50) default '',
  `itemgroupdescription` varchar(255) default '',
  `itemsubgroupcode` varchar(50) default '',
  `itemsubgroupdescription` varchar(255) default '',
  `itemcode` varchar(50) default '',
  `itemdescription` varchar(255) default '',
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default '',
  `qty` double(11,2) default '0.00',
  `actualamount` double(16,2) default '0.00',
  `amount` double(16,2) default '0.00',
  `transtype` int(11) default '0',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billinvoicepaymentdetails` */

CREATE TABLE `billinvoicepaymentdetails` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `invoiceno` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `totaldue` double(16,2) default '0.00',
  `totalpaid` double(16,2) default '0.00',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billinvoicepayments` */

CREATE TABLE `billinvoicepayments` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `voided` int(11) default '0',
  `voidsysdate` datetime default NULL,
  `voidtransdate` datetime default NULL,
  `voidno` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `totaldue` double(16,2) default '0.00',
  `totalpaid` double(16,2) default '0.00',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `currencysymbol` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `paytype000001` double(16,2) default '0.00',
  `paytype000002` double(16,2) default '0.00',
  `paytype000003` double(16,2) default '0.00',
  `paytype000004` double(16,2) default '0.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billinvoices` */

CREATE TABLE `billinvoices` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `invoiceno` varchar(50) default '',
  `totaldue` double(16,2) default '0.00',
  `totalpaid` double(16,2) default '0.00',
  `balancedue` double(16,2) default '0.00',
  `paidforinvoice` double(16,2) default '0.00',
  `status` int(11) default '0',
  `voided` int(11) default '0',
  `voidsysdate` datetime default NULL,
  `voidtransdate` datetime default NULL,
  `voidno` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `currencysymbol` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billinvoiceslog` */

CREATE TABLE `billinvoiceslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `invoiceno` varchar(50) default '',
  `reference` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `transtype` int(11) default '0',
  `transdescription` varchar(255) default '',
  `debitamount` double(16,2) default '0.00',
  `creditamount` double(16,2) default '0.00',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billreceiptitems` */

CREATE TABLE `billreceiptitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `reference` varchar(50) default '',
  `voidno` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `itemgroupcode` varchar(50) default '',
  `itemgroupdescription` varchar(255) default '',
  `itemsubgroupcode` varchar(50) default '',
  `itemsubgroupdescription` varchar(255) default '',
  `itemcode` varchar(50) default '',
  `itemdescription` varchar(255) default '',
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default '',
  `qty` double(11,2) default '0.00',
  `actualamount` double(16,2) default '0.00',
  `amount` double(16,2) default '0.00',
  `transtype` int(11) default '0',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billreceipts` */

CREATE TABLE `billreceipts` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `voided` int(11) default '0',
  `voidsysdate` datetime default NULL,
  `voidtransdate` datetime default NULL,
  `voidno` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `totaldue` double(16,2) default '0.00',
  `discount` double(16,2) default '0.00',
  `totalpaid` double(16,2) default '0.00',
  `changeamount` double(16,2) default '0.00',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `currencysymbol` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `paytypecash` double(16,2) default '0.00',
  `paytypecheq` double(16,2) default '0.00',
  `paytypefdep` double(16,2) default '0.00',
  `paytype000001` double(16,2) default '0.00',
  `paytype000002` double(16,2) default '0.00',
  `paytype000003` double(16,2) default '0.00',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `paytype000004` double(16,2) default '0.00',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billrefunds` */

CREATE TABLE `billrefunds` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `chequeno` varchar(255) default '',
  `bank` varchar(255) default '',
  `branch` varchar(255) default '',
  `holder` varchar(255) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `transtype` int(11) default '0',
  `userid` varchar(50) default '',
  `paytype000001` double(16,2) default '0.00',
  `paytype000002` double(16,2) default '0.00',
  `paytype000003` double(16,2) default '0.00',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `refunded` int(11) default '0',
  `refundsysdate` datetime default NULL,
  `refundtransdate` datetime default NULL,
  `refunduserid` varchar(50) default '',
  `paymentsource` int(11) default '0',
  `paytype000004` double(16,2) default '0.00',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billsales` */

CREATE TABLE `billsales` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `invoiceno` varchar(50) default '',
  `voided` int(11) default '0',
  `voidsysdate` datetime default NULL,
  `voidtransdate` datetime default NULL,
  `voidno` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `totaldue` double(16,2) default '0.00',
  `discount` double(16,2) default '0.00',
  `totalpaid` double(16,2) default '0.00',
  `changeamount` double(16,2) default '0.00',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `transtype` int(11) default '0',
  `userid` varchar(50) default '',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billsalesitems` */

CREATE TABLE `billsalesitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `receiptno` varchar(50) default '',
  `invoiceno` varchar(50) default '',
  `reference` varchar(50) default '',
  `voidno` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `itemgroupcode` varchar(50) default '',
  `itemgroupdescription` varchar(255) default '',
  `itemsubgroupcode` varchar(50) default '',
  `itemsubgroupdescription` varchar(255) default '',
  `itemcode` varchar(50) default '',
  `itemdescription` varchar(255) default '',
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default '',
  `qty` double(11,2) default '0.00',
  `actualamount` double(16,2) default '0.00',
  `amount` double(16,2) default '0.00',
  `transtype` int(11) default '0',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billvoidedsales` */

CREATE TABLE `billvoidedsales` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `invoiceno` varchar(50) default '',
  `totaldue` double(16,2) default '0.00',
  `totalpaid` double(16,2) default '0.00',
  `balancedue` double(16,2) default '0.00',
  `paidforinvoice` double(16,2) default '0.00',
  `status` int(11) default '0',
  `voidsource` int(11) default '0',
  `voided` int(11) default '0',
  `voidsysdate` datetime default NULL,
  `voidtransdate` datetime default NULL,
  `voidno` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `currencysymbol` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `receiptno` varchar(50) default '',
  `discount` double(16,2) default '0.00',
  `changeamount` double(16,2) default '0.00',
  `paytypecash` double(16,2) default '0.00',
  `paytypecheq` double(16,2) default '0.00',
  `paytypefdep` double(16,2) default '0.00',
  `paytype000001` double(16,2) default '0.00',
  `paytype000002` double(16,2) default '0.00',
  `paytype000003` double(16,2) default '0.00',
  `paytype000004` double(16,2) default '0.00',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `billvoidedsalesitems` */

CREATE TABLE `billvoidedsalesitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `transdate` datetime default NULL,
  `sysdate` datetime default NULL,
  `invoiceno` varchar(50) default '',
  `reference` varchar(50) default '',
  `voidsource` int(11) default '0',
  `voidno` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroupdescription` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroupdescription` varchar(255) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `itemgroupcode` varchar(50) default '',
  `itemgroupdescription` varchar(255) default '',
  `itemsubgroupcode` varchar(50) default '',
  `itemsubgroupdescription` varchar(255) default '',
  `itemcode` varchar(50) default '',
  `itemdescription` varchar(255) default '',
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default '',
  `qty` double(16,2) default '0.00',
  `actualamount` double(16,2) default '0.00',
  `amount` double(16,2) default '0.00',
  `transtype` int(11) default '0',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `userid` varchar(50) default '',
  `receiptno` varchar(50) default '',
  `voiduserid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `countries` */

CREATE TABLE `countries` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `districts` */

CREATE TABLE `districts` (
  `autocode` int(11) NOT NULL auto_increment,
  `countrycode` varchar(50) default '',
  `regioncode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtdiagnoses` */

CREATE TABLE `dxtdiagnoses` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtdiagnosesgroups` */

CREATE TABLE `dxtdiagnosesgroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtmtuhadiagnoses` */

CREATE TABLE `dxtmtuhadiagnoses` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtmtuhadiagnosesmapping` */

CREATE TABLE `dxtmtuhadiagnosesmapping` (
  `autocode` int(11) NOT NULL auto_increment,
  `mtuhacode` varchar(50) default '',
  `diagnosiscode` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtpatientdiagnosesincidences` */

CREATE TABLE `dxtpatientdiagnosesincidences` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `incidencekey` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `diagnosiscode` varchar(50) default '',
  `doctorcode` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtpatientdiagnoseslog` */

CREATE TABLE `dxtpatientdiagnoseslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `booking` varchar(50) default '',
  `incidencekey` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `diagnosiscode` varchar(50) default '',
  `diagnosisdescription` varchar(255) default '',
  `mtuhadiagnosiscode` varchar(50) default '',
  `mtuhadiagnosisdescription` varchar(255) default '',
  `followup` int(11) default '0',
  `doctorcode` varchar(50) default '',
  `doctordescription` varchar(255) default '',
  `history` varchar(255) default '',
  `examination` varchar(255) default '',
  `investigation` varchar(255) default '',
  `treatments` varchar(500) default '',
  `deathstatus` int(11) default '0',
  `referalno` varchar(50) default '',
  `referaldescription` varchar(255) default '',
  `department` varchar(50) default '',
  `yearpart` int(11) default NULL,
  `monthpart` int(11) default NULL,
  `userid` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(255) default '',
  `patientbirthdate` datetime default NULL,
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` double(11,2) default '0.00',
  `patienttemperature` double(11,2) default '0.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtpatientprescriptions` */

CREATE TABLE `dxtpatientprescriptions` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `booking` varchar(50) default NULL,
  `incidencekey` varchar(50) default NULL,
  `patientcode` varchar(50) default NULL,
  `diagnosiscode` varchar(50) default NULL,
  `diagnosisdescription` varchar(255) default NULL,
  `doctorcode` varchar(50) default NULL,
  `doctordescription` varchar(255) default NULL,
  `itemgroupcode` varchar(50) default NULL,
  `itemgroupdescription` varchar(255) default NULL,
  `itemsubgroupcode` varchar(50) default NULL,
  `itemsubgroupdescription` varchar(255) default NULL,
  `itemcode` varchar(50) default NULL,
  `itemdescription` varchar(255) default NULL,
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default NULL,
  `storedescription` varchar(255) default NULL,
  `qty` double(11,2) default '0.00',
  `actualamount` double(16,2) default '0.00',
  `amount` double(16,2) default '0.00',
  `itemopmcode` varchar(50) default NULL,
  `itemopmdescription` varchar(255) default NULL,
  `packagingcode` varchar(50) default NULL,
  `packagingdescription` varchar(255) default NULL,
  `piecesinpackage` int(11) default '0',
  `userid` varchar(50) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `dxtpatientreferals` */

CREATE TABLE `dxtpatientreferals` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `referalno` varchar(50) default '',
  `incidencekey` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `diagnosiscode` varchar(50) default '',
  `diagnosisdescription` varchar(255) default '',
  `doctorcode` varchar(50) default '',
  `doctordescription` varchar(255) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `englishlanguage` */

CREATE TABLE `englishlanguage` (
  `autocode` int(11) NOT NULL auto_increment,
  `objecttype` varchar(50) default NULL,
  `controlname` varchar(255) default NULL,
  `description` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `englishlanguageaccessfunctions` */

CREATE TABLE `englishlanguageaccessfunctions` (
  `autocode` int(11) NOT NULL auto_increment,
  `objecttype` varchar(50) default NULL,
  `controlname` varchar(255) default NULL,
  `description` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `englishlanguagemessages` */

CREATE TABLE `englishlanguagemessages` (
  `autocode` int(11) NOT NULL auto_increment,
  `objecttype` varchar(50) default NULL,
  `controlname` varchar(255) default NULL,
  `description` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilityautocodes` */

CREATE TABLE `facilityautocodes` (
  `autocode` int(11) NOT NULL auto_increment,
  `codekey` int(11) default NULL,
  `autogenerate` int(11) default '1',
  `idseed` int(11) default '1',
  `idincrement` int(11) default '1',
  `idlength` int(11) default '6',
  `idcurrent` int(11) default '1',
  `preftype` int(11) default '1',
  `preftext` varchar(50) default '',
  `prefsep` varchar(50) default '',
  `surftype` int(11) default '1',
  `surftext` varchar(50) default '',
  `surfsep` varchar(50) default '',
  `position` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitybillinggroups` */

CREATE TABLE `facilitybillinggroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitybillingitems` */

CREATE TABLE `facilitybillingitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default NULL,
  `subgroupcode` varchar(50) default NULL,
  `code` varchar(50) default NULL,
  `printbarcode` int(11) default '0',
  `description` varchar(255) default NULL,
  `price1` double(11,2) default '0.00',
  `price2` double(11,2) default '0.00',
  `price3` double(11,2) default '0.00',
  `price4` double(11,2) default '0.00',
  `price5` double(11,2) default '0.00',
  `price6` double(11,2) default '0.00',
  `price7` double(11,2) default '0.00',
  `price8` double(11,2) default '0.00',
  `price9` double(11,2) default '0.00',
  `price10` double(11,2) default '0.00',
  `defaultqty` int(11) default '0',
  `addtocart` int(11) default '0',
  `foripdadmission` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitybillingsubgroups` */

CREATE TABLE `facilitybillingsubgroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitybookinglog` */

CREATE TABLE `facilitybookinglog` (
  `autocode` int(11) NOT NULL auto_increment,
  `booking` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `patientgender` varchar(2) default '',
  `patientbirthdate` datetime default NULL,
  `patientregdate` datetime default NULL,
  `bookdate` datetime default NULL,
  `refered` int(11) default '0',
  `referedfacility` varchar(255) default '',
  `department` varchar(50) default '',
  `wheretakencode` varchar(50) default '',
  `wheretaken` varchar(255) default '',
  `billinggroupcode` varchar(50) default '',
  `billinggroup` varchar(255) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billingsubgroup` varchar(255) default '',
  `billinggroupmembershipno` varchar(100) default '',
  `ipdstart` datetime default NULL,
  `ipdstop` datetime default NULL,
  `ipddischargestatus` varchar(50) default '',
  `registrystatus` varchar(50) default '',
  `weight` double(16,2) default '0.00',
  `temperature` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `sysdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitybookings` */

CREATE TABLE `facilitybookings` (
  `autocode` int(11) NOT NULL auto_increment,
  `patientcode` varchar(50) default '',
  `bookdate` datetime default NULL,
  `refered` int(11) default '0',
  `referedfacility` varchar(255) default '',
  `department` varchar(50) default '',
  `wheretakencode` varchar(50) default '',
  `wheretaken` varchar(255) default '',
  `billinggroupcode` varchar(50) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billinggroupmembershipno` varchar(100) default '',
  `ipdstart` datetime default NULL,
  `ipdstop` datetime default NULL,
  `weight` double(16,2) default '0.00',
  `temperature` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `sysdate` datetime default NULL,
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(255) default '',
  `patientbirthdate` datetime default NULL,
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycontrolinformation` */

CREATE TABLE `facilitycontrolinformation` (
  `autocode` int(11) NOT NULL auto_increment,
  `parentinformationtype` int(11) default '-1',
  `parentinformationvalue` varchar(255) default NULL,
  `informationtype` int(11) default '0',
  `description` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycontrolinformationproperties` */

CREATE TABLE `facilitycontrolinformationproperties` (
  `autocode` int(11) NOT NULL auto_increment,
  `informationtype` int(11) default '0',
  `compulsory` int(11) default '0',
  `freetext` int(11) default '1',
  `charactercasingoption` int(11) default '0',
  `defaultvalue` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycorporateitems` */

CREATE TABLE `facilitycorporateitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default '',
  `itemcode` varchar(50) default '',
  `pricingpercent` double(11,2) default '100.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycorporatemembers` */

CREATE TABLE `facilitycorporatemembers` (
  `autocode` int(11) NOT NULL auto_increment,
  `regdate` datetime default NULL,
  `termdate` datetime default NULL,
  `expirydate` datetime default NULL,
  `code` varchar(50) default '',
  `surname` varchar(255) default '',
  `firstname` varchar(255) default '',
  `othernames` varchar(255) default '',
  `gender` varchar(2) default '',
  `birthdate` datetime default NULL,
  `ceilingamount` double(16,2) default '0.00',
  `billinggroupcode` varchar(50) default '',
  `billingsubgroupcode` varchar(50) default '',
  `billinggroupmembershipno` varchar(255) default '',
  `inactive` int(11) default '1',
  `picturename` varchar(50) default '',
  `membershipstatus` int(11) default '1',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycorporates` */

CREATE TABLE `facilitycorporates` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `hasid` int(11) default '0',
  `hasceiling` int(11) default '0',
  `ceilingamount` double(11,2) default '0.00',
  `promptpayment` int(11) default '0',
  `itemssensitive` int(11) default '0',
  `pricecategory` varchar(50) default '',
  `hassubgroups` int(11) default '0',
  `strictactivation` int(11) default '0',
  `inactive` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycorporatesubgroups` */

CREATE TABLE `facilitycorporatesubgroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitycurrencies` */

CREATE TABLE `facilitycurrencies` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `exchangerate` double(11,2) default '1.00',
  `currencysymbol` varchar(10) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitydischargestatus` */

CREATE TABLE `facilitydischargestatus` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilityoptions` */

CREATE TABLE `facilityoptions` (
  `autocode` int(11) NOT NULL auto_increment,
  `facilitycode` varchar(50) default '',
  `facilitydescription` varchar(255) default '',
  `box` varchar(255) default '',
  `street` varchar(255) default '',
  `teleno` varchar(255) default '',
  `regioncode` varchar(50) default '',
  `regiondescription` varchar(255) default NULL,
  `districtcode` varchar(50) default '',
  `districtdescription` varchar(255) default NULL,
  `countrycode` varchar(50) default '',
  `countrydescription` varchar(255) default NULL,
  `headname` varchar(255) default '',
  `headdesignation` varchar(255) default '',
  `facilitytype` int(11) default '0',
  `roundingoption` int(11) default '0',
  `roundingfigure` int(11) default '0',
  `roundingdecimals` int(11) default '0',
  `roundingstrictness` int(11) default '0',
  `roundingmidpointoption` int(11) default '0',
  `affectstockatcashier` int(11) default '1',
  `doubleentryissuing` int(11) default '1',
  `transferoutrefreshinterval` int(11) default '1',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitypatientdocuments` */

CREATE TABLE `facilitypatientdocuments` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `printorder` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitypaymenttypes` */

CREATE TABLE `facilitypaymenttypes` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `preventoverpay` int(11) default '0',
  `checkdepositbalance` int(11) default '0',
  `ischeque` int(11) default '0',
  `allowrefund` int(11) default '0',
  `visibilitysales` int(11) default '1',
  `visibilitydebtorpayments` int(11) default '1',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitypricecategories` */

CREATE TABLE `facilitypricecategories` (
  `autocode` int(11) NOT NULL auto_increment,
  `useprice1` int(11) default '1',
  `useprice2` int(11) default '0',
  `useprice3` int(11) default '0',
  `useprice4` int(11) default '0',
  `useprice5` int(11) default '0',
  `useprice6` int(11) default '0',
  `useprice7` int(11) default '0',
  `useprice8` int(11) default '0',
  `useprice9` int(11) default '0',
  `useprice10` int(11) default '0',
  `price1` varchar(60) default 'price 1',
  `price2` varchar(60) default 'price 2',
  `price3` varchar(60) default 'price 3',
  `price4` varchar(60) default 'price 4',
  `price5` varchar(60) default 'price 5',
  `price6` varchar(60) default 'price 6',
  `price7` varchar(60) default 'price 7',
  `price8` varchar(60) default 'price 8',
  `price9` varchar(60) default 'price 9',
  `price10` varchar(60) default 'price 10',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitystaffs` */

CREATE TABLE `facilitystaffs` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `category` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitytreatmentpoints` */

CREATE TABLE `facilitytreatmentpoints` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitywardroombeds` */

CREATE TABLE `facilitywardroombeds` (
  `autocode` int(11) NOT NULL auto_increment,
  `roomcode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `regdate` datetime default NULL,
  `termdate` datetime default NULL,
  `bedstatus` varchar(50) default '',
  `patients` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitywardrooms` */

CREATE TABLE `facilitywardrooms` (
  `autocode` int(11) NOT NULL auto_increment,
  `wardcode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `facilitywards` */

CREATE TABLE `facilitywards` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `maternity` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `ipdadmissionslog` */

CREATE TABLE `ipdadmissionslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` datetime default NULL,
  `booking` varchar(50) default '',
  `transcode` varchar(50) default '',
  `wardcode` varchar(50) default '',
  `warddescription` varchar(255) default '',
  `roomcode` varchar(50) default '',
  `roomdescription` varchar(255) default '',
  `bed` varchar(50) default '',
  `patientcondition` varchar(255) default '',
  `registrystatus` varchar(50) default '',
  `userid` varchar(50) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `ipddischargeslog` */

CREATE TABLE `ipddischargeslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` datetime default NULL,
  `booking` varchar(50) default '',
  `transcode` varchar(50) default '',
  `wardcode` varchar(50) default '',
  `warddescription` varchar(255) default '',
  `roomcode` varchar(50) default '',
  `roomdescription` varchar(255) default '',
  `bed` varchar(50) default '',
  `patientcondition` varchar(255) default '',
  `dischargestatuscode` varchar(50) default '',
  `dischargestatusdescription` varchar(255) default '',
  `dischargeremarks` varchar(255) default '',
  `registrystatus` varchar(50) default '',
  `userid` varchar(50) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `ipdtransferslog` */

CREATE TABLE `ipdtransferslog` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transferdate` datetime default NULL,
  `booking` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientsurname` varchar(255) default '',
  `patientfirstname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientstreet` varchar(255) default '',
  `patientgender` varchar(10) default '',
  `patientbirthdate` datetime default NULL,
  `wardfromcode` varchar(50) default '',
  `wardfromdescription` varchar(255) default '',
  `wardtocode` varchar(50) default '',
  `wardtodescription` varchar(255) default '',
  `roomfromcode` varchar(50) default '',
  `roomfromdescription` varchar(255) default '',
  `roomtocode` varchar(50) default '',
  `roomtodescription` varchar(255) default '',
  `bedfrom` varchar(50) default '',
  `bedto` varchar(50) default '',
  `transfertofacility` varchar(255) default '',
  `patientcondition` varchar(255) default '',
  `yearpart` int(11) default '0',
  `monthpart` int(11) default '0',
  `registrystatus` varchar(50) default '',
  `userid` varchar(50) default '',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `patientdetailgroupfields` */

CREATE TABLE `patientdetailgroupfields` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default '',
  `fieldname` varchar(255) default '',
  `displayorder` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `patientdetailgroups` */

CREATE TABLE `patientdetailgroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `displayorder` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `patients` */

CREATE TABLE `patients` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `surname` varchar(255) default '',
  `firstname` varchar(255) default '',
  `othernames` varchar(255) default '',
  `street` varchar(255) default '',
  `gender` varchar(2) default '',
  `birthdate` datetime default NULL,
  `chronic` varchar(255) default '',
  `severe` varchar(255) default '',
  `operations` varchar(255) default '',
  `regdate` datetime default NULL,
  `plotno` varchar(255) default '',
  `pobox` varchar(255) default '',
  `country` varchar(255) default '',
  `region` varchar(255) default '',
  `district` varchar(255) default '',
  `nationality` varchar(255) default '',
  `ward` varchar(255) default '',
  `village` varchar(255) default '',
  `wardhead` varchar(255) default '',
  `villagehead` varchar(255) default '',
  `idtype` varchar(50) default '',
  `idno` varchar(50) default '',
  `idserial` varchar(50) default '',
  `idcountry` varchar(255) default '',
  `idexpirydate` datetime default NULL,
  `idissuedate` datetime default NULL,
  `idissueplace` varchar(255) default '',
  `complexion` varchar(255) default '',
  `bloodgroup` varchar(255) default '',
  `eyecolor` varchar(255) default '',
  `ethnicity` varchar(255) default '',
  `religion` varchar(255) default '',
  `hair` varchar(255) default '',
  `maritalstatus` varchar(50) default '',
  `occupation` varchar(255) default '',
  `nextofkin` varchar(255) default '',
  `weight` double(11,2) default '0.00',
  `temperature` double(11,2) default '0.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `patientsqueue` */

CREATE TABLE `patientsqueue` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `treatmentpointcode` varchar(50) default '',
  `treatmentpointdescription` varchar(255) default '',
  `laboratorycode` varchar(50) default '',
  `patientcode` varchar(50) default '',
  `patientfirstname` varchar(255) default '',
  `patientsurname` varchar(255) default '',
  `patientothernames` varchar(255) default '',
  `patientgender` varchar(255) default '',
  `patientbirthdate` datetime default NULL,
  `patientstreet` varchar(255) default '',
  `weight` double(16,2) default '0.00',
  `temperature` double(16,2) default '0.00',
  `booking` int(11) default '0',
  `queuetype` int(11) default '0',
  `patientregdate` datetime default NULL,
  `patientcountry` varchar(255) default '',
  `patientregion` varchar(255) default '',
  `patientdistrict` varchar(255) default '',
  `patientward` varchar(255) default '',
  `patientvillage` varchar(255) default '',
  `patientwardhead` varchar(255) default '',
  `patientvillagehead` varchar(255) default '',
  `patientnationality` varchar(255) default '',
  `patientcomplexion` varchar(255) default '',
  `patientbloodgroup` varchar(255) default '',
  `patienteyecolor` varchar(255) default '',
  `patientethnicity` varchar(255) default '',
  `patientreligion` varchar(255) default '',
  `patienthair` varchar(255) default '',
  `patientoccupation` varchar(255) default '',
  `patientnextofkin` varchar(255) default '',
  `patientweight` varchar(255) default '',
  `patienttemperature` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `regions` */

CREATE TABLE `regions` (
  `autocode` int(11) NOT NULL auto_increment,
  `countrycode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `revisitingnumbers` */

CREATE TABLE `revisitingnumbers` (
  `autocode` int(11) NOT NULL auto_increment,
  `patientcode` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_customers` */

CREATE TABLE `som_customers` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(20) default NULL,
  `description` varchar(255) default NULL,
  `phone` varchar(255) default NULL,
  `address` varchar(255) default NULL,
  `fax` varchar(255) default NULL,
  `email` varchar(255) default NULL,
  `website` varchar(255) default NULL,
  `visible_ms` int(11) default '0',
  `visible_sst` int(11) default '0',
  `visible_pharm` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_orderitems` */

CREATE TABLE `som_orderitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `orderno` varchar(50) default '',
  `itemorderid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `orderedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_orderreceiveditems` */

CREATE TABLE `som_orderreceiveditems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `orderno` varchar(50) default '',
  `deliveryno` varchar(50) default '',
  `itemorderid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `storecode` varchar(50) default '',
  `receivedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_orders` */

CREATE TABLE `som_orders` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `orderno` varchar(50) default '',
  `ordertitle` varchar(255) default '',
  `shiptodescription` varchar(255) default '',
  `suppliercode` varchar(50) default '',
  `supplierdescription` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `exchangerate` double(11,2) default '0.00',
  `currencysymbol` varchar(50) default '',
  `remarks` varchar(255) default '',
  `orderstatus` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_packagings` */

CREATE TABLE `som_packagings` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `pieces` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_physicalinventory` */

CREATE TABLE `som_physicalinventory` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `calculateddate` datetime default NULL,
  `closeddate` datetime default NULL,
  `storecode` varchar(50) default '',
  `storedescription` varchar(255) default '',
  `referenceno` varchar(50) default '',
  `description` varchar(255) default '',
  `inventorystatus` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_physicalinventoryitems` */

CREATE TABLE `som_physicalinventoryitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `calculateddate` datetime default NULL,
  `closeddate` datetime default NULL,
  `storecode` varchar(50) default '',
  `referenceno` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `countedqty` double(11,2) default '0.00',
  `expectedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_productcategories` */

CREATE TABLE `som_productcategories` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_productcontrol` */

CREATE TABLE `som_productcontrol` (
  `autocode` int(11) NOT NULL auto_increment,
  `productcode` varchar(50) default NULL,
  `expirydate` datetime default NULL,
  `qty_ms` double(16,2) default '0.00',
  `qty_sst` double(16,2) default '0.00',
  `qty_pharm` double(16,2) default '0.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_products` */

CREATE TABLE `som_products` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default NULL,
  `description` varchar(255) default NULL,
  `opmcode` varchar(50) default NULL,
  `opmdescription` varchar(255) default NULL,
  `departmentcode` varchar(50) default NULL,
  `departmentdescription` varchar(255) default NULL,
  `packagingcode` varchar(50) default NULL,
  `packagingdescription` varchar(255) default NULL,
  `piecesinpackage` int(11) default '1',
  `costprice` double(11,2) default '0.00',
  `price1` double(11,2) default '0.00',
  `price2` double(11,2) default '0.00',
  `price3` double(11,2) default '0.00',
  `price4` double(11,2) default '0.00',
  `price5` double(11,2) default '0.00',
  `price6` double(11,2) default '0.00',
  `price7` double(11,2) default '0.00',
  `price8` double(11,2) default '0.00',
  `price9` double(11,2) default '0.00',
  `price10` double(11,2) default '0.00',
  `addtocart` int(11) default '0',
  `defaultqty` double(11,2) default '0.00',
  `hasexpiry` int(11) default '0',
  `expirynotice` int(11) default '0',
  `display` int(11) default '1',
  `visible_ms` int(11) default '0',
  `minlevel_ms` int(11) default '0',
  `maxlevel_ms` int(11) default '0',
  `avgconsumption_ms` int(11) default '0',
  `leadtime_ms` int(11) default '0',
  `orderqty_ms` int(11) default '0',
  `visible_sst` int(11) default '0',
  `minlevel_sst` int(11) default '0',
  `maxlevel_sst` int(11) default '0',
  `avgconsumption_sst` int(11) default '0',
  `leadtime_sst` int(11) default '0',
  `orderqty_sst` int(11) default '0',
  `visible_pharm` int(11) default '0',
  `minlevel_pharm` int(11) default '0',
  `maxlevel_pharm` int(11) default '0',
  `avgconsumption_pharm` int(11) default '0',
  `leadtime_pharm` int(11) default '0',
  `orderqty_pharm` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_producttransactions` */

CREATE TABLE `som_producttransactions` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `sourcecode` varchar(50) default NULL,
  `sourcedescription` varchar(255) default NULL,
  `productcode` varchar(50) default NULL,
  `packagingcode` varchar(50) default NULL,
  `packagingdescription` varchar(255) default NULL,
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `reference` varchar(50) default NULL,
  `transprice` double(11,2) default '0.00',
  `transtype` varchar(50) default NULL,
  `transdescription` varchar(255) default NULL,
  `userid` varchar(50) default NULL,
  `qtyin_ms` double(16,2) default '0.00',
  `qtyout_ms` double(16,2) default '0.00',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  `qtyin_sst` double(16,2) default '0.00',
  `qtyout_sst` double(16,2) default '0.00',
  `qtyin_pharm` double(16,2) default '0.00',
  `qtyout_pharm` double(16,2) default '0.00',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_stockissueitems` */

CREATE TABLE `som_stockissueitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferinno` varchar(50) default '',
  `deliveryno` varchar(50) default '',
  `itemtransferid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `fromstorecode` varchar(50) default '',
  `tostorecode` varchar(50) default '',
  `issuedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_stockissues` */

CREATE TABLE `som_stockissues` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `deliveryno` varchar(50) default '',
  `transferno` varchar(50) default '',
  `transferinno` varchar(50) default '',
  `transfertitle` varchar(255) default '',
  `fromcode` varchar(50) default '',
  `fromdescription` varchar(255) default '',
  `tocode` varchar(50) default '',
  `todescription` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `exchangerate` double(11,2) default '0.00',
  `currencysymbol` varchar(50) default '',
  `remarks` varchar(255) default '',
  `transferstatus` varchar(50) default '',
  `transfertype` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_stockreceiptitems` */

CREATE TABLE `som_stockreceiptitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `orderno` varchar(50) default '',
  `transferno` varchar(50) default '',
  `transferoutno` varchar(50) default '',
  `deliveryno` varchar(50) default '',
  `itemtransferid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `fromstorecode` varchar(50) default '',
  `tostorecode` varchar(50) default '',
  `receivedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_stockreceipts` */

CREATE TABLE `som_stockreceipts` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `deliveryno` varchar(50) default '',
  `orderno` varchar(50) default '',
  `transferno` varchar(50) default '',
  `transferoutno` varchar(50) default '',
  `transfertitle` varchar(255) default '',
  `fromcode` varchar(50) default '',
  `fromdescription` varchar(255) default '',
  `tocode` varchar(50) default '',
  `todescription` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `exchangerate` double(11,2) default '0.00',
  `currencysymbol` varchar(50) default '',
  `remarks` varchar(255) default '',
  `transferstatus` varchar(50) default '',
  `transfertype` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_stores` */

CREATE TABLE `som_stores` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default NULL,
  `description` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_suppliers` */

CREATE TABLE `som_suppliers` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(20) default NULL,
  `description` varchar(255) default NULL,
  `phone` varchar(255) default NULL,
  `address` varchar(255) default NULL,
  `fax` varchar(255) default NULL,
  `email` varchar(255) default NULL,
  `website` varchar(255) default NULL,
  `suppliertype` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_transferinitems` */

CREATE TABLE `som_transferinitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferoutno` varchar(50) default '',
  `itemtransferid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `transferedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_transferinreceiveditems` */

CREATE TABLE `som_transferinreceiveditems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferoutno` varchar(50) default '',
  `deliveryno` varchar(50) default '',
  `itemtransferid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `fromstorecode` varchar(50) default '',
  `tostorecode` varchar(50) default '',
  `receivedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_transferins` */

CREATE TABLE `som_transferins` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferoutno` varchar(50) default '',
  `transfertitle` varchar(255) default '',
  `fromcode` varchar(50) default '',
  `fromdescription` varchar(255) default '',
  `tocode` varchar(50) default '',
  `todescription` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `exchangerate` double(11,2) default '0.00',
  `currencysymbol` varchar(50) default '',
  `remarks` varchar(255) default '',
  `transferstatus` varchar(50) default '',
  `transfertype` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_transferoutissueditems` */

CREATE TABLE `som_transferoutissueditems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferinno` varchar(50) default '',
  `deliveryno` varchar(50) default '',
  `itemtransferid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `expirydate` datetime default NULL,
  `fromstorecode` varchar(50) default '',
  `tostorecode` varchar(50) default '',
  `issuedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_transferoutitems` */

CREATE TABLE `som_transferoutitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferinno` varchar(50) default '',
  `itemtransferid` varchar(50) default '',
  `productcode` varchar(50) default '',
  `packagingcode` varchar(50) default '',
  `packagingdescription` varchar(255) default '',
  `piecesinpackage` int(11) default '0',
  `transferedqty` double(11,2) default '0.00',
  `transprice` double(16,2) default '0.00',
  `userid` varchar(50) default '',
  `productdescription` varchar(255) default '',
  `productopmcode` varchar(255) default '',
  `productopmdescription` varchar(255) default '',
  `productdepartmentcode` varchar(255) default '',
  `productdepartmentdescription` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `som_transferouts` */

CREATE TABLE `som_transferouts` (
  `autocode` int(11) NOT NULL auto_increment,
  `sysdate` datetime default NULL,
  `transdate` datetime default NULL,
  `transferno` varchar(50) default '',
  `transferinno` varchar(50) default '',
  `transfertitle` varchar(255) default '',
  `fromcode` varchar(50) default '',
  `fromdescription` varchar(255) default '',
  `tocode` varchar(50) default '',
  `todescription` varchar(255) default '',
  `currencycode` varchar(50) default '',
  `currencydescription` varchar(255) default '',
  `exchangerate` double(11,2) default '0.00',
  `currencysymbol` varchar(50) default '',
  `remarks` varchar(255) default '',
  `transferstatus` varchar(50) default '',
  `transfertype` varchar(50) default '',
  `userid` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_formsizes` */

CREATE TABLE `sys_formsizes` (
  `autocode` int(11) NOT NULL auto_increment,
  `formname` varchar(80) default '',
  `formwidth` int(11) default '0',
  `formheight` int(11) default '0',
  `layoutname` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_reportcharts` */

CREATE TABLE `sys_reportcharts` (
  `autocode` int(11) NOT NULL auto_increment,
  `reportcode` varchar(50) default '',
  `value1` double(16,2) default '0.00',
  `value2` double(16,2) default '-1.00',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_reportfilterconditions` */

CREATE TABLE `sys_reportfilterconditions` (
  `autocode` int(11) NOT NULL auto_increment,
  `reportcode` varchar(50) default '',
  `conditionfieldname` varchar(255) default '',
  `conditionoperator` varchar(50) default '',
  `conditionvalue1` varchar(255) default '',
  `conditionvalue2` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_reportgroups` */

CREATE TABLE `sys_reportgroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_reportparameters` */

CREATE TABLE `sys_reportparameters` (
  `autocode` int(11) NOT NULL auto_increment,
  `reportcode` varchar(50) default '',
  `parametercode` varchar(50) default '',
  `parameterdescription` varchar(255) default '',
  `parametertype` varchar(50) default '',
  `parametercontrol` varchar(50) default '',
  `parametersavevalue` int(11) default '0',
  `lookuptablename` varchar(255) default '',
  `valuestring` varchar(255) default '',
  `valuedatetime` datetime default NULL,
  `valueint` int(11) default '0',
  `valuedouble` double(16,2) default '0.00',
  `optional` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_reports` */

CREATE TABLE `sys_reports` (
  `autocode` int(11) NOT NULL auto_increment,
  `groupcode` varchar(50) default '',
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `tablename` varchar(255) default '',
  `commandtext` varchar(2000) default '',
  `filterstring` varchar(1000) default '',
  `groupbyfields` varchar(1000) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_searchfields` */

CREATE TABLE `sys_searchfields` (
  `autocode` int(11) NOT NULL auto_increment,
  `tablename` varchar(80) default NULL,
  `fieldname` varchar(80) default NULL,
  `fielddisplayname` varchar(255) default NULL,
  `defaultfield` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_storeusers` */

CREATE TABLE `sys_storeusers` (
  `autocode` int(11) NOT NULL auto_increment,
  `usercode` varchar(50) default NULL,
  `storecode` varchar(50) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_useractions` */

CREATE TABLE `sys_useractions` (
  `autocode` int(11) NOT NULL auto_increment,
  `userid` varchar(50) default '',
  `searchproductswhiletyping` int(11) default '0',
  `searchproductsoption` int(11) default '0',
  `searchproductfieldname` varchar(255) default '',
  `searchpatientswhiletyping` int(11) default '0',
  `searchpatientsoption` int(11) default '0',
  `searchpatientfieldname` varchar(255) default '',
  `searchclientgroupmemberswhiletyping` int(11) default '0',
  `searchclientgroupmembersoption` int(11) default '0',
  `searchclientgroupmemberfieldname` varchar(255) default '',
  `searchclientgroupswhiletyping` int(11) default '0',
  `searchclientgroupsoption` int(11) default '0',
  `searchclientgroupfieldname` varchar(255) default '',
  `searchbillingitemgroupswhiletyping` int(11) default '0',
  `searchbillingitemgroupsoption` int(11) default '0',
  `searchbillingitemgroupfieldname` varchar(255) default '',
  `searchdebtorswhiletyping` int(11) default '0',
  `searchdebtorsoption` int(11) default '0',
  `searchdebtorfieldname` varchar(255) default '',
  `searchdiagnoseswhiletyping` int(11) default '0',
  `searchdiagnosesoption` int(11) default '0',
  `searchdiagnosisfieldname` varchar(255) default '',
  `searchsupplierswhiletyping` int(11) default '0',
  `searchsuppliersoption` int(11) default '0',
  `searchsupplierfieldname` varchar(255) default '',
  `searchstoreswhiletyping` int(11) default '0',
  `searchstoresoption` int(11) default '0',
  `searchstorefieldname` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_usergroupfunctions` */

CREATE TABLE `sys_usergroupfunctions` (
  `autocode` int(11) NOT NULL auto_increment,
  `usergroupcode` varchar(50) default '',
  `functionaccesskey` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_usergroupmoduleitems` */

CREATE TABLE `sys_usergroupmoduleitems` (
  `autocode` int(11) NOT NULL auto_increment,
  `usergroupcode` varchar(50) default '',
  `moduleitemkey` varchar(50) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_usergroupprinters` */

CREATE TABLE `sys_usergroupprinters` (
  `autocode` int(11) NOT NULL auto_increment,
  `machinename` varchar(50) default '',
  `usergroupcode` varchar(50) default '',
  `documenttypecode` varchar(50) default '',
  `printername` varchar(255) default '',
  `printedwhen` int(11) default '-1',
  `autoprint` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_usergroupreports` */

CREATE TABLE `sys_usergroupreports` (
  `autocode` int(11) NOT NULL auto_increment,
  `usergroupcode` varchar(50) default '',
  `reportcode` varchar(50) default '',
  `reportview` int(11) default '1',
  `reportdesign` int(11) default '0',
  `reportformcustomization` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_usergroups` */

CREATE TABLE `sys_usergroups` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `formlayouttemplatename` varchar(255) default '',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_users` */

CREATE TABLE `sys_users` (
  `autocode` int(11) NOT NULL auto_increment,
  `code` varchar(50) default '',
  `description` varchar(255) default '',
  `password` varchar(50) default '',
  `usergroupcode` varchar(50) default '',
  `address` varchar(255) default '',
  `phone` varchar(50) default '',
  `occupation` varchar(255) default '',
  `storecode` varchar(50) default '',
  `defaultlanguage` varchar(255) default '',
  `defaultskinname` varchar(255) default '',
  `mainnavbarwidth` int(11) default '257',
  `displaystore` int(11) default '0',
  `allowchangingstore` int(11) default '0',
  `defaultclientgroupcode` varchar(50) default NULL,
  `defaultclientsubgroupcode` varchar(50) default NULL,
  `allowchangingclientgroup` int(11) default '1',
  `defaultpaymenttypecode` varchar(50) default NULL,
  `allowchangingpaymenttype` int(11) default '1',
  `defaultpricecategorycode` varchar(50) default NULL,
  `allowchangingpricecategory` int(11) default '1',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_versioncontrol` */

CREATE TABLE `sys_versioncontrol` (
  `autocode` int(11) NOT NULL auto_increment,
  `majornumber` int(11) default '0',
  `minornumber` int(11) default '0',
  `buildnumber` int(11) default '0',
  `revisionnumber` int(11) default '0',
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sys_workstations` */

CREATE TABLE `sys_workstations` (
  `autocode` int(11) NOT NULL auto_increment,
  `machinename` varchar(255) default NULL,
  `defaultskinname` varchar(255) default NULL,
  `defaultlanguage` varchar(255) default NULL,
  PRIMARY KEY  (`autocode`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;