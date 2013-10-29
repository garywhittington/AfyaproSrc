/*Table structure for table afya_dhis_period */

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

CREATE TABLE afya_dhis_period (
  periodid INT(11) NOT NULL AUTO_INCREMENT,
  periodtypeid INT(11) DEFAULT NULL,
  startdate DATE NOT NULL,
  enddate DATE NOT NULL,
  PRIMARY KEY  (periodid),
  UNIQUE KEY periodtypeid (periodtypeid,startdate,enddate)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billaccountfromtowhom */

CREATE TABLE billaccountfromtowhom (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billaccounts */

CREATE TABLE billaccounts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  CODE VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  balance DOUBLE(16,2) DEFAULT '0.00',
  allowoverdraft INT(11) DEFAULT '0',
  inactive INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billaccountslog */

CREATE TABLE billaccountslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  reference VARCHAR(50) DEFAULT NULL,
  accountcode VARCHAR(50) DEFAULT NULL,
  accountdescription VARCHAR(255) DEFAULT NULL,
  fromwhomtowhomcode VARCHAR(50) DEFAULT NULL,
  fromwhomtowhom VARCHAR(255) DEFAULT NULL,
  entryside INT(11) DEFAULT '0',
  transdescription VARCHAR(255) DEFAULT NULL,
  debitamount DOUBLE(16,2) DEFAULT '0.00',
  creditamount DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billaccounttransactions */

CREATE TABLE billaccounttransactions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transactionid VARCHAR(50) DEFAULT NULL,
  accountcode VARCHAR(50) DEFAULT NULL,
  accountdescription VARCHAR(255) DEFAULT NULL,
  fromwhomtowhomcode VARCHAR(50) DEFAULT NULL,
  fromwhomtowhom VARCHAR(255) DEFAULT NULL,
  entryside INT(11) DEFAULT '0',
  transdescription VARCHAR(255) DEFAULT NULL,
  transamount DOUBLE(16,2) DEFAULT '0.00',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billaccountusers */

CREATE TABLE billaccountusers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  accountcode VARCHAR(50) DEFAULT '',
  membercode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billcollections */

CREATE TABLE billcollections (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT NULL,
  paymentsource INT(11) DEFAULT '0',
  refunded INT(11) DEFAULT '0',
  refundsysdate DATETIME DEFAULT NULL,
  refundtransdate DATETIME DEFAULT NULL,
  refunduserid VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT NULL,
  chequeno VARCHAR(255) DEFAULT NULL,
  bank VARCHAR(255) DEFAULT NULL,
  branch VARCHAR(255) DEFAULT NULL,
  holder VARCHAR(255) DEFAULT NULL,
  billinggroupcode VARCHAR(50) DEFAULT NULL,
  billinggroupdescription VARCHAR(255) DEFAULT NULL,
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  transtype INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT NULL,
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billdebtors */

CREATE TABLE billdebtors (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  accountcode VARCHAR(50) DEFAULT '',
  accountdescription VARCHAR(255) DEFAULT '',
  debtortype VARCHAR(50) DEFAULT '',
  balance DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billdebtorslog */

CREATE TABLE billdebtorslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  reference VARCHAR(50) DEFAULT '',
  accountcode VARCHAR(50) DEFAULT '',
  accountdescription VARCHAR(255) DEFAULT '',
  fromwhomtowhomcode VARCHAR(50) DEFAULT '',
  fromwhomtowhom VARCHAR(255) DEFAULT '',
  debtortype VARCHAR(50) DEFAULT '',
  transtype INT(11) DEFAULT '2',
  transdescription VARCHAR(255) DEFAULT '',
  debitamount DOUBLE(16,2) DEFAULT '0.00',
  creditamount DOUBLE(16,2) DEFAULT '0.00',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billdebtreliefrequestinvoices */

CREATE TABLE billdebtreliefrequestinvoices (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  requestno VARCHAR(50) DEFAULT '',
  transdate DATETIME DEFAULT NULL,
  invoiceno VARCHAR(50) DEFAULT '',
  booking VARCHAR(50) DEFAULT '',
  balancedue DOUBLE(16,2) DEFAULT '0.00',
  requestedamount DOUBLE(16,2) DEFAULT '0.00',
  approvedamount DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billdebtreliefrequests */

CREATE TABLE billdebtreliefrequests (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  requestno VARCHAR(50) DEFAULT '',
  accountcode VARCHAR(50) DEFAULT '',
  accountdescription VARCHAR(255) DEFAULT '',
  debtortype VARCHAR(50) DEFAULT '',
  reasonforrelief VARCHAR(255) DEFAULT '',
  totalbalancedue DOUBLE(16,2) DEFAULT '0.00',
  totalrequestedamount DOUBLE(16,2) DEFAULT '0.00',
  totalapprovedamount DOUBLE(16,2) DEFAULT '0.00',
  approvalremarks VARCHAR(255) DEFAULT '',
  requestedby VARCHAR(50) DEFAULT '',
  approveddate DATETIME DEFAULT NULL,
  approvedby VARCHAR(50) DEFAULT '',
  requeststatus INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billincomingitems */

CREATE TABLE billincomingitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT NULL,
  itemgroupcode VARCHAR(50) DEFAULT NULL,
  itemgroupdescription VARCHAR(255) DEFAULT NULL,
  itemsubgroupcode VARCHAR(50) DEFAULT NULL,
  itemsubgroupdescription VARCHAR(255) DEFAULT NULL,
  itemcode VARCHAR(50) DEFAULT NULL,
  itemdescription VARCHAR(255) DEFAULT NULL,
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT NULL,
  storedescription VARCHAR(255) DEFAULT NULL,
  qty DOUBLE(11,2) DEFAULT '0.00',
  actualamount DOUBLE(16,2) DEFAULT '0.00',
  amount DOUBLE(16,2) DEFAULT '0.00',
  itemopmcode VARCHAR(50) DEFAULT NULL,
  itemopmdescription VARCHAR(255) DEFAULT NULL,
  packagingcode VARCHAR(50) DEFAULT NULL,
  packagingdescription VARCHAR(255) DEFAULT NULL,
  piecesinpackage INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT NULL,
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  deletedbyuserid VARCHAR(50) DEFAULT '',
  display INT(11) DEFAULT '0',
  deleteddate DATETIME DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billinvoiceitems */

CREATE TABLE billinvoiceitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  invoiceno VARCHAR(50) DEFAULT '',
  reference VARCHAR(50) DEFAULT '',
  voidno VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  itemgroupcode VARCHAR(50) DEFAULT '',
  itemgroupdescription VARCHAR(255) DEFAULT '',
  itemsubgroupcode VARCHAR(50) DEFAULT '',
  itemsubgroupdescription VARCHAR(255) DEFAULT '',
  itemcode VARCHAR(50) DEFAULT '',
  itemdescription VARCHAR(255) DEFAULT '',
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  qty DOUBLE(11,2) DEFAULT '0.00',
  actualamount DOUBLE(16,2) DEFAULT '0.00',
  amount DOUBLE(16,2) DEFAULT '0.00',
  transtype INT(11) DEFAULT '0',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billinvoicepaymentdetails */

CREATE TABLE billinvoicepaymentdetails (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  invoiceno VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  totaldue DOUBLE(16,2) DEFAULT '0.00',
  totalpaid DOUBLE(16,2) DEFAULT '0.00',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billinvoicepayments */

CREATE TABLE billinvoicepayments (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  voided INT(11) DEFAULT '0',
  voidsysdate DATETIME DEFAULT NULL,
  voidtransdate DATETIME DEFAULT NULL,
  voidno VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  totaldue DOUBLE(16,2) DEFAULT '0.00',
  totalpaid DOUBLE(16,2) DEFAULT '0.00',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  currencysymbol VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billinvoices */

CREATE TABLE billinvoices (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  invoiceno VARCHAR(50) DEFAULT '',
  totaldue DOUBLE(16,2) DEFAULT '0.00',
  totalpaid DOUBLE(16,2) DEFAULT '0.00',
  balancedue DOUBLE(16,2) DEFAULT '0.00',
  paidforinvoice DOUBLE(16,2) DEFAULT '0.00',
  STATUS INT(11) DEFAULT '0',
  voided INT(11) DEFAULT '0',
  voidsysdate DATETIME DEFAULT NULL,
  voidtransdate DATETIME DEFAULT NULL,
  voidno VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  currencysymbol VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  booking VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billinvoiceslog */

CREATE TABLE billinvoiceslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  invoiceno VARCHAR(50) DEFAULT '',
  reference VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  transtype INT(11) DEFAULT '0',
  transdescription VARCHAR(255) DEFAULT '',
  debitamount DOUBLE(16,2) DEFAULT '0.00',
  creditamount DOUBLE(16,2) DEFAULT '0.00',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billreceiptitems */

CREATE TABLE billreceiptitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  reference VARCHAR(50) DEFAULT '',
  voidno VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  itemgroupcode VARCHAR(50) DEFAULT '',
  itemgroupdescription VARCHAR(255) DEFAULT '',
  itemsubgroupcode VARCHAR(50) DEFAULT '',
  itemsubgroupdescription VARCHAR(255) DEFAULT '',
  itemcode VARCHAR(50) DEFAULT '',
  itemdescription VARCHAR(255) DEFAULT '',
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  qty DOUBLE(11,2) DEFAULT '0.00',
  actualamount DOUBLE(16,2) DEFAULT '0.00',
  amount DOUBLE(16,2) DEFAULT '0.00',
  transtype INT(11) DEFAULT '0',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billreceipts */

CREATE TABLE billreceipts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  voided INT(11) DEFAULT '0',
  voidsysdate DATETIME DEFAULT NULL,
  voidtransdate DATETIME DEFAULT NULL,
  voidno VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  totaldue DOUBLE(16,2) DEFAULT '0.00',
  discount DOUBLE(16,2) DEFAULT '0.00',
  totalpaid DOUBLE(16,2) DEFAULT '0.00',
  changeamount DOUBLE(16,2) DEFAULT '0.00',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  currencysymbol VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billrefunds */

CREATE TABLE billrefunds (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  chequeno VARCHAR(255) DEFAULT '',
  bank VARCHAR(255) DEFAULT '',
  branch VARCHAR(255) DEFAULT '',
  holder VARCHAR(255) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  transtype INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  refunded INT(11) DEFAULT '0',
  refundsysdate DATETIME DEFAULT NULL,
  refundtransdate DATETIME DEFAULT NULL,
  refunduserid VARCHAR(50) DEFAULT '',
  paymentsource INT(11) DEFAULT '0',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billsales */

CREATE TABLE billsales (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  invoiceno VARCHAR(50) DEFAULT '',
  voided INT(11) DEFAULT '0',
  voidsysdate DATETIME DEFAULT NULL,
  voidtransdate DATETIME DEFAULT NULL,
  voidno VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  totaldue DOUBLE(16,2) DEFAULT '0.00',
  discount DOUBLE(16,2) DEFAULT '0.00',
  totalpaid DOUBLE(16,2) DEFAULT '0.00',
  changeamount DOUBLE(16,2) DEFAULT '0.00',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  transtype INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billsalesitems */

CREATE TABLE billsalesitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  receiptno VARCHAR(50) DEFAULT '',
  invoiceno VARCHAR(50) DEFAULT '',
  reference VARCHAR(50) DEFAULT '',
  voidno VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  itemgroupcode VARCHAR(50) DEFAULT '',
  itemgroupdescription VARCHAR(255) DEFAULT '',
  itemsubgroupcode VARCHAR(50) DEFAULT '',
  itemsubgroupdescription VARCHAR(255) DEFAULT '',
  itemcode VARCHAR(50) DEFAULT '',
  itemdescription VARCHAR(255) DEFAULT '',
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  qty DOUBLE(11,2) DEFAULT '0.00',
  actualamount DOUBLE(16,2) DEFAULT '0.00',
  amount DOUBLE(16,2) DEFAULT '0.00',
  transtype INT(11) DEFAULT '0',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billvoidedsales */

CREATE TABLE billvoidedsales (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  invoiceno VARCHAR(50) DEFAULT '',
  totaldue DOUBLE(16,2) DEFAULT '0.00',
  totalpaid DOUBLE(16,2) DEFAULT '0.00',
  balancedue DOUBLE(16,2) DEFAULT '0.00',
  paidforinvoice DOUBLE(16,2) DEFAULT '0.00',
  STATUS INT(11) DEFAULT '0',
  voidsource INT(11) DEFAULT '0',
  voided INT(11) DEFAULT '0',
  voidsysdate DATETIME DEFAULT NULL,
  voidtransdate DATETIME DEFAULT NULL,
  voidno VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  currencysymbol VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  receiptno VARCHAR(50) DEFAULT '',
  discount DOUBLE(16,2) DEFAULT '0.00',
  changeamount DOUBLE(16,2) DEFAULT '0.00',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  booking VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table billvoidedsalesitems */

CREATE TABLE billvoidedsalesitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  invoiceno VARCHAR(50) DEFAULT '',
  reference VARCHAR(50) DEFAULT '',
  voidsource INT(11) DEFAULT '0',
  voidno VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroupdescription VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupdescription VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  itemgroupcode VARCHAR(50) DEFAULT '',
  itemgroupdescription VARCHAR(255) DEFAULT '',
  itemsubgroupcode VARCHAR(50) DEFAULT '',
  itemsubgroupdescription VARCHAR(255) DEFAULT '',
  itemcode VARCHAR(50) DEFAULT '',
  itemdescription VARCHAR(255) DEFAULT '',
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  qty DOUBLE(16,2) DEFAULT '0.00',
  actualamount DOUBLE(16,2) DEFAULT '0.00',
  amount DOUBLE(16,2) DEFAULT '0.00',
  transtype INT(11) DEFAULT '0',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  receiptno VARCHAR(50) DEFAULT '',
  voiduserid VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table countries */

CREATE TABLE countries (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table districts */

CREATE TABLE districts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  countrycode VARCHAR(50) DEFAULT '',
  regioncode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtdiagnoses */

CREATE TABLE dxtdiagnoses (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  subgroupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxticddiagnoses */

CREATE TABLE dxticddiagnoses (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(10) DEFAULT NULL,
  subgroupcode VARCHAR(10) DEFAULT NULL,
  CODE VARCHAR(10) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtdiagnosesgroups */

CREATE TABLE dxtdiagnosesgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtdiagnosessubgroups */

CREATE TABLE dxtdiagnosessubgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtindicatordiagnoses */

CREATE TABLE dxtindicatordiagnoses (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  indicatorcode VARCHAR(50) DEFAULT '',
  diagnosiscode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtindicatorgroups */

CREATE TABLE dxtindicatorgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtindicators */

CREATE TABLE dxtindicators (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtmtuhadiagnoses */

CREATE TABLE dxtmtuhadiagnoses (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtmtuhadiagnosesmapping */

CREATE TABLE dxtmtuhadiagnosesmapping (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  mtuhacode VARCHAR(50) DEFAULT '',
  diagnosiscode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtpatientdiagnosesfollowup */

CREATE TABLE dxtpatientdiagnosesfollowup (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  booking VARCHAR(50) DEFAULT '',
  episodecode VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  isprimary INT(11) DEFAULT '0',
  reasonforencounter VARCHAR(255) DEFAULT '',
  ipddiagnosistype VARCHAR(50) DEFAULT 'admission',
  diagnosiscode VARCHAR(50) DEFAULT '',
  diagnosisdescription VARCHAR(255) DEFAULT '',
  treatments VARCHAR(500) DEFAULT '',
  mtuhadiagnosiscode VARCHAR(50) DEFAULT '',
  mtuhadiagnosisdescription VARCHAR(255) DEFAULT '',
  deathstatus INT(11) DEFAULT '0',
  doctorcode VARCHAR(50) DEFAULT '',
  doctordescription VARCHAR(255) DEFAULT '',
  referalno VARCHAR(50) DEFAULT '',
  referaldescription VARCHAR(255) DEFAULT '',
  department VARCHAR(50) DEFAULT '',
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT '',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtpatientdiagnoseslog */

CREATE TABLE dxtpatientdiagnoseslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  booking VARCHAR(50) DEFAULT '',
  episodecode VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  isprimary INT(11) DEFAULT '0',
  reasonforencounter VARCHAR(255) DEFAULT '',
  ipddiagnosistype VARCHAR(50) DEFAULT 'admission',
  diagnosiscode VARCHAR(50) DEFAULT '',
  icdcode VARCHAR(50) DEFAULT '',
  indicatorcode VARCHAR(50) DEFAULT '',
  history VARCHAR(500) DEFAULT '',
  examination VARCHAR(500) DEFAULT '',
  investigation VARCHAR(500) DEFAULT '',
  diagnosisdescription VARCHAR(255) DEFAULT '',
  firstencounterdate DATETIME DEFAULT NULL,
  lastencounterdate DATETIME DEFAULT NULL,
  treatments VARCHAR(500) DEFAULT '',
  deathstatus INT(11) DEFAULT '0',
  doctorcode VARCHAR(50) DEFAULT '',
  doctordescription VARCHAR(255) DEFAULT '',
  referalno VARCHAR(50) DEFAULT '',
  referaldescription VARCHAR(255) DEFAULT '',
  department VARCHAR(50) DEFAULT '',
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT '',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtpatientprescriptions */

CREATE TABLE dxtpatientprescriptions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  booking VARCHAR(50) DEFAULT NULL,
  incidencekey VARCHAR(50) DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT NULL,
  diagnosiscode VARCHAR(50) DEFAULT NULL,
  diagnosisdescription VARCHAR(255) DEFAULT NULL,
  doctorcode VARCHAR(50) DEFAULT NULL,
  doctordescription VARCHAR(255) DEFAULT NULL,
  itemgroupcode VARCHAR(50) DEFAULT NULL,
  itemgroupdescription VARCHAR(255) DEFAULT NULL,
  itemsubgroupcode VARCHAR(50) DEFAULT NULL,
  itemsubgroupdescription VARCHAR(255) DEFAULT NULL,
  itemcode VARCHAR(50) DEFAULT NULL,
  itemdescription VARCHAR(255) DEFAULT NULL,
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT NULL,
  storedescription VARCHAR(255) DEFAULT NULL,
  qty DOUBLE(11,2) DEFAULT '0.00',
  actualamount DOUBLE(16,2) DEFAULT '0.00',
  amount DOUBLE(16,2) DEFAULT '0.00',
  itemopmcode VARCHAR(50) DEFAULT NULL,
  itemopmdescription VARCHAR(255) DEFAULT NULL,
  packagingcode VARCHAR(50) DEFAULT NULL,
  packagingdescription VARCHAR(255) DEFAULT NULL,
  piecesinpackage INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table dxtpatientreferals */

CREATE TABLE dxtpatientreferals (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  referalno VARCHAR(50) DEFAULT '',
  incidencekey VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  diagnosiscode VARCHAR(50) DEFAULT '',
  diagnosisdescription VARCHAR(255) DEFAULT '',
  doctorcode VARCHAR(50) DEFAULT '',
  doctordescription VARCHAR(255) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table englishlanguage */

CREATE TABLE englishlanguage (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  objecttype VARCHAR(50) DEFAULT NULL,
  controlname VARCHAR(255) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table englishlanguageaccessfunctions */

CREATE TABLE englishlanguageaccessfunctions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  objecttype VARCHAR(50) DEFAULT NULL,
  controlname VARCHAR(255) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table englishlanguagemessages */

CREATE TABLE englishlanguagemessages (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  objecttype VARCHAR(50) DEFAULT NULL,
  controlname VARCHAR(255) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_langcolumnheaders */

CREATE TABLE sys_langcolumnheaders (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  gridname VARCHAR(50) DEFAULT NULL,
  columnname VARCHAR(255) DEFAULT NULL,
  english VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilityautocodes */

CREATE TABLE facilityautocodes (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  codekey INT(11) DEFAULT NULL,
  autogenerate INT(11) DEFAULT '1',
  idseed INT(11) DEFAULT '1',
  idincrement INT(11) DEFAULT '1',
  idlength INT(11) DEFAULT '6',
  idcurrent INT(11) DEFAULT '1',
  preftype INT(11) DEFAULT '1',
  preftext VARCHAR(50) DEFAULT '',
  prefsep VARCHAR(50) DEFAULT '',
  surftype INT(11) DEFAULT '1',
  surftext VARCHAR(50) DEFAULT '',
  surfsep VARCHAR(50) DEFAULT '',
  POSITION INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitybillinggroups */

CREATE TABLE facilitybillinggroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitybillingitems */

CREATE TABLE facilitybillingitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT NULL,
  subgroupcode VARCHAR(50) DEFAULT NULL,
  CODE VARCHAR(50) DEFAULT NULL,
  printbarcode INT(11) DEFAULT '0',
  description VARCHAR(255) DEFAULT NULL,
  price1 DOUBLE(11,2) DEFAULT '0.00',
  price2 DOUBLE(11,2) DEFAULT '0.00',
  price3 DOUBLE(11,2) DEFAULT '0.00',
  price4 DOUBLE(11,2) DEFAULT '0.00',
  price5 DOUBLE(11,2) DEFAULT '0.00',
  price6 DOUBLE(11,2) DEFAULT '0.00',
  price7 DOUBLE(11,2) DEFAULT '0.00',
  price8 DOUBLE(11,2) DEFAULT '0.00',
  price9 DOUBLE(11,2) DEFAULT '0.00',
  price10 DOUBLE(11,2) DEFAULT '0.00',
  defaultqty INT(11) DEFAULT '0',
  addtocart INT(11) DEFAULT '0',
  foripdadmission INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitybillingsubgroups */

CREATE TABLE facilitybillingsubgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitybookinglog */

CREATE TABLE facilitybookinglog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  bookdate DATETIME DEFAULT NULL,
  refered INT(11) DEFAULT '0',
  referedfacility VARCHAR(255) DEFAULT '',
  department VARCHAR(50) DEFAULT '',
  wheretakencode VARCHAR(50) DEFAULT '',
  wheretaken VARCHAR(255) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billinggroup VARCHAR(255) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billingsubgroup VARCHAR(255) DEFAULT '',
  billinggroupmembershipno VARCHAR(100) DEFAULT '',
  ipdstart DATETIME DEFAULT NULL,
  ipdstop DATETIME DEFAULT NULL,
  ipddischargestatus VARCHAR(50) DEFAULT '',
  registrystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  SYSDATE DATETIME DEFAULT NULL,
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitybookings */

CREATE TABLE facilitybookings (
  autocode VARCHAR(50) NOT NULL DEFAULT '',
  SYSDATE DATETIME DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT '',
  refered INT(11) DEFAULT '0',
  referedfacility VARCHAR(255) DEFAULT '',
  department VARCHAR(50) DEFAULT '',
  wheretakencode VARCHAR(50) DEFAULT '',
  wheretaken VARCHAR(255) DEFAULT '',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billinggroupmembershipno VARCHAR(100) DEFAULT '',
  pricecategory VARCHAR(50) DEFAULT '',
  ipdstart DATETIME DEFAULT NULL,
  ipdstop DATETIME DEFAULT NULL,
  userid VARCHAR(50) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitycorporatediagnoses */

CREATE TABLE facilitycorporatediagnoses (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  diagnosiscode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitycorporateitems */

CREATE TABLE facilitycorporateitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  itemcode VARCHAR(50) DEFAULT '',
  pricingpercent DOUBLE(11,2) DEFAULT '100.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitycorporatemembers */

CREATE TABLE facilitycorporatemembers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  regdate DATETIME DEFAULT NULL,
  termdate DATETIME DEFAULT NULL,
  expirydate DATETIME DEFAULT NULL,
  CODE VARCHAR(50) DEFAULT '',
  surname VARCHAR(255) DEFAULT '',
  firstname VARCHAR(255) DEFAULT '',
  othernames VARCHAR(255) DEFAULT '',
  gender VARCHAR(2) DEFAULT '',
  birthdate DATETIME DEFAULT NULL,
  ceilingamount DOUBLE(16,2) DEFAULT '0.00',
  billinggroupcode VARCHAR(50) DEFAULT '',
  billingsubgroupcode VARCHAR(50) DEFAULT '',
  billinggroupmembershipno VARCHAR(255) DEFAULT '',
  inactive INT(11) DEFAULT '1',
  picturename VARCHAR(50) DEFAULT '',
  membershipstatus INT(11) DEFAULT '1',
  memberpicture BLOB,
  patientcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitycorporates */

CREATE TABLE facilitycorporates (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  hasid INT(11) DEFAULT '0',
  hasceiling INT(11) DEFAULT '0',
  ceilingamount DOUBLE(11,2) DEFAULT '0.00',
  promptpayment INT(11) DEFAULT '0',
  itemssensitive INT(11) DEFAULT '0',
  diagnosessensitive INT(11) DEFAULT '0',
  pricecategory VARCHAR(50) DEFAULT '',
  hassubgroups INT(11) DEFAULT '0',
  strictactivation INT(11) DEFAULT '0',
  inactive INT(11) DEFAULT '0',
  generateinvoicewhenpreparingbill INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitycorporatesubgroups */

CREATE TABLE facilitycorporatesubgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitycurrencies */

CREATE TABLE facilitycurrencies (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  exchangerate DOUBLE(11,2) DEFAULT '1.00',
  currencysymbol VARCHAR(10) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitydischargestatus */

CREATE TABLE facilitydischargestatus (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitylaboratories */

CREATE TABLE facilitylaboratories (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilityoptions */

CREATE TABLE facilityoptions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  facilitycode VARCHAR(50) DEFAULT '',
  facilitydescription VARCHAR(255) DEFAULT '',
  box VARCHAR(255) DEFAULT '',
  street VARCHAR(255) DEFAULT '',
  teleno VARCHAR(255) DEFAULT '',
  regioncode VARCHAR(50) DEFAULT '',
  regiondescription VARCHAR(255) DEFAULT NULL,
  districtcode VARCHAR(50) DEFAULT '',
  districtdescription VARCHAR(255) DEFAULT NULL,
  countrycode VARCHAR(50) DEFAULT '',
  countrydescription VARCHAR(255) DEFAULT NULL,
  headname VARCHAR(255) DEFAULT '',
  headdesignation VARCHAR(255) DEFAULT '',
  facilitytype INT(11) DEFAULT '0',
  roundingoption INT(11) DEFAULT '0',
  roundingfigure INT(11) DEFAULT '0',
  roundingdecimals INT(11) DEFAULT '0',
  roundingstrictness INT(11) DEFAULT '0',
  roundingmidpointoption INT(11) DEFAULT '0',
  affectstockatcashier INT(11) DEFAULT '1',
  doubleentryissuing INT(11) DEFAULT '1',
  transferoutrefreshinterval INT(11) DEFAULT '1',
  charactercasingoptionpatientnames INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitypatientdocuments */

CREATE TABLE facilitypatientdocuments (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  printorder INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitypaymenttypes */

CREATE TABLE facilitypaymenttypes (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  preventoverpay INT(11) DEFAULT '0',
  checkdepositbalance INT(11) DEFAULT '0',
  ischeque INT(11) DEFAULT '0',
  allowrefund INT(11) DEFAULT '0',
  visibilitysales INT(11) DEFAULT '1',
  visibilitydebtorpayments INT(11) DEFAULT '1',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitypricecategories */

CREATE TABLE facilitypricecategories (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  useprice1 INT(11) DEFAULT '1',
  useprice2 INT(11) DEFAULT '0',
  useprice3 INT(11) DEFAULT '0',
  useprice4 INT(11) DEFAULT '0',
  useprice5 INT(11) DEFAULT '0',
  useprice6 INT(11) DEFAULT '0',
  useprice7 INT(11) DEFAULT '0',
  useprice8 INT(11) DEFAULT '0',
  useprice9 INT(11) DEFAULT '0',
  useprice10 INT(11) DEFAULT '0',
  price1 VARCHAR(60) DEFAULT 'price 1',
  price2 VARCHAR(60) DEFAULT 'price 2',
  price3 VARCHAR(60) DEFAULT 'price 3',
  price4 VARCHAR(60) DEFAULT 'price 4',
  price5 VARCHAR(60) DEFAULT 'price 5',
  price6 VARCHAR(60) DEFAULT 'price 6',
  price7 VARCHAR(60) DEFAULT 'price 7',
  price8 VARCHAR(60) DEFAULT 'price 8',
  price9 VARCHAR(60) DEFAULT 'price 9',
  price10 VARCHAR(60) DEFAULT 'price 10',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitystaffs */

CREATE TABLE facilitystaffs (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  category INT(11) DEFAULT '0',
  treatmentpointcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitysurgerytypes */

CREATE TABLE facilitysurgerytypes (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(20) DEFAULT NULL,
  description VARCHAR(60) DEFAULT NULL,
  category INT(11) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitytheatres */

CREATE TABLE facilitytheatres (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(20) DEFAULT NULL,
  description VARCHAR(60) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitytreatmentpoints */

CREATE TABLE facilitytreatmentpoints (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitywardroombeds */

CREATE TABLE facilitywardroombeds (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  roomcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  regdate DATETIME DEFAULT NULL,
  lastadmissiondate DATETIME DEFAULT NULL,
  bedstatus VARCHAR(50) DEFAULT '',
  patients INT(11) DEFAULT '0',
  wardcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitywardrooms */

CREATE TABLE facilitywardrooms (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  wardcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table facilitywards */

CREATE TABLE facilitywards (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  maternity INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ipdadmissionslog */

CREATE TABLE ipdadmissionslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT '',
  booking VARCHAR(50) DEFAULT '',
  transcode VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  warddescription VARCHAR(255) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  roomdescription VARCHAR(255) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  beddescription VARCHAR(255) DEFAULT '',
  patientcondition VARCHAR(255) DEFAULT '',
  registrystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ipddischargeslog */

CREATE TABLE ipddischargeslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT '',
  booking VARCHAR(50) DEFAULT '',
  transcode VARCHAR(50) DEFAULT '',
  wardcode VARCHAR(50) DEFAULT '',
  warddescription VARCHAR(255) DEFAULT '',
  roomcode VARCHAR(50) DEFAULT '',
  roomdescription VARCHAR(255) DEFAULT '',
  bedcode VARCHAR(50) DEFAULT '',
  beddescription VARCHAR(255) DEFAULT '',
  patientcondition VARCHAR(255) DEFAULT '',
  dischargestatuscode VARCHAR(50) DEFAULT '',
  dischargestatusdescription VARCHAR(255) DEFAULT '',
  dischargeremarks VARCHAR(255) DEFAULT '',
  registrystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ipdtransferslog */

CREATE TABLE ipdtransferslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transferdate DATETIME DEFAULT NULL,
  booking VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  wardfromcode VARCHAR(50) DEFAULT '',
  wardfromdescription VARCHAR(255) DEFAULT '',
  wardtocode VARCHAR(50) DEFAULT '',
  wardtodescription VARCHAR(255) DEFAULT '',
  roomfromcode VARCHAR(50) DEFAULT '',
  roomfromdescription VARCHAR(255) DEFAULT '',
  roomtocode VARCHAR(50) DEFAULT '',
  roomtodescription VARCHAR(255) DEFAULT '',
  bedfrom VARCHAR(50) DEFAULT '',
  bedto VARCHAR(50) DEFAULT '',
  transfertofacility VARCHAR(255) DEFAULT '',
  patientcondition VARCHAR(255) DEFAULT '',
  yearpart INT(11) DEFAULT '0',
  monthpart INT(11) DEFAULT '0',
  registrystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  bedfromcode VARCHAR(50) DEFAULT '',
  bedfromdescription VARCHAR(50) DEFAULT '',
  bedtocode VARCHAR(50) DEFAULT '',
  bedtodescription VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labpatienttests */

CREATE TABLE labpatienttests (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  booking VARCHAR(50) DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT NULL,
  laboratorycode VARCHAR(50) DEFAULT NULL,
  doctorcode VARCHAR(50) DEFAULT NULL,
  labtechniciancode VARCHAR(50) DEFAULT NULL,
  labtestgroupcode VARCHAR(50) DEFAULT NULL,
  labtestsubgroupcode VARCHAR(50) DEFAULT NULL,
  labtesttypecode VARCHAR(50) DEFAULT NULL,
  results VARCHAR(255) DEFAULT NULL,
  resultfigure VARCHAR(255) DEFAULT NULL,
  units VARCHAR(50) DEFAULT NULL,
  outofrange_normal INT(11) DEFAULT '0',
  outofrange_equipment INT(11) DEFAULT '0',
  remarks VARCHAR(255) DEFAULT NULL,
  department VARCHAR(50) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT NULL,
  patientweight DOUBLE(16,2) DEFAULT '0.00',
  patienttemperature DOUBLE(16,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labpatientprescriptions */

CREATE TABLE labpatientprescriptions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) DEFAULT NULL,
  laboratorycode VARCHAR(50) DEFAULT NULL,
  doctorcode VARCHAR(50) DEFAULT NULL,
  labtestgroupcode VARCHAR(50) DEFAULT NULL,
  labtestsubgroupcode VARCHAR(50) DEFAULT NULL,
  labtesttypecode VARCHAR(50) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labremarks */

CREATE TABLE labremarks (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labtestdropdownvalues */

CREATE TABLE labtestdropdownvalues (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  labtesttypecode VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labtestgroups */

CREATE TABLE labtestgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labtests */

CREATE TABLE labtests (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) NOT NULL DEFAULT '',
  description VARCHAR(255) DEFAULT NULL,
  displayname VARCHAR(255) DEFAULT NULL,
  groupcode VARCHAR(50) DEFAULT NULL,
  resulttype INT(11) DEFAULT '1',
  rejectoutofrange INT(11) DEFAULT '0',
  units VARCHAR(50) DEFAULT NULL,
  subgroupcode VARCHAR(50) DEFAULT '',
  restricttodropdownlist INT(11) DEFAULT '0',
  additionalinfo INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labtestranges */

CREATE TABLE labtestranges (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  labtesttypecode VARCHAR(50) NOT NULL DEFAULT '',
  age_loweryears INT(11) DEFAULT '0',
  age_lowermonths INT(11) DEFAULT '0',
  age_upperyears INT(11) DEFAULT '0',
  age_uppermonths INT(11) DEFAULT '0',
  equipment_malelowerrange DOUBLE(11,2) DEFAULT '0.00',
  equipment_maleupperrange DOUBLE(11,2) DEFAULT '0.00',
  equipment_femalelowerrange DOUBLE(11,2) DEFAULT '0.00',
  equipment_femaleupperrange DOUBLE(11,2) DEFAULT '0.00',
  normal_malelowerrange DOUBLE(11,2) DEFAULT '0.00',
  normal_maleupperrange DOUBLE(11,2) DEFAULT '0.00',
  normal_femalelowerrange DOUBLE(11,2) DEFAULT '0.00',
  normal_femaleupperrange DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table labtestsubgroups */

CREATE TABLE labtestsubgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table patientextrafieldlookup */

CREATE TABLE patientextrafieldlookup (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  fieldname VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  filtervalue VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table patientextrafields */

CREATE TABLE patientextrafields (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  fieldname VARCHAR(50) DEFAULT NULL,
  fieldcaption VARCHAR(255) DEFAULT NULL,
  fieldtype VARCHAR(255) DEFAULT NULL,
  datatype VARCHAR(255) DEFAULT NULL,
  defaultvalue VARCHAR(255) DEFAULT NULL,
  filteronvaluefrom VARCHAR(255) DEFAULT NULL,
  allowinput INT(11) DEFAULT '0',
  restricttodropdownlist INT(11) DEFAULT '0',
  rememberentries INT(11) DEFAULT '0',
  charactercasingoption INT(11) DEFAULT '0',
  compulsory INT(11) DEFAULT '0',
  erroronempty VARCHAR(255) DEFAULT NULL,
  erroroninvalidinput VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table patientfirstnames */

CREATE TABLE patientfirstnames (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table patientothernames */

CREATE TABLE patientothernames (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table patients */

CREATE TABLE patients (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT '',
  chronic VARCHAR(255) DEFAULT '',
  severe VARCHAR(255) DEFAULT '',
  operations VARCHAR(255) DEFAULT '',
  surname VARCHAR(255) DEFAULT '',
  firstname VARCHAR(255) DEFAULT '',
  othernames VARCHAR(255) DEFAULT '',
  gender VARCHAR(255) DEFAULT '',
  birthdate DATETIME DEFAULT NULL,
  regdate DATETIME DEFAULT NULL,
  maritalstatuscode VARCHAR(50) DEFAULT '',
  weight DOUBLE(16,2) DEFAULT '0.00',
  temperature DOUBLE(16,2) DEFAULT '0.00',
  mothername VARCHAR(255) DEFAULT '',
  mothernntvaccine INT(11) DEFAULT '0',
  servicetype INT(11) DEFAULT '-1',
  allergies VARCHAR(255) DEFAULT '',
  fromopd INT(11) DEFAULT '1',
  fromrch INT(11) DEFAULT '0',
  fromctc INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode),
  FULLTEXT KEY firstname (firstname,othernames,surname)
) ENGINE=MYISAM DEFAULT CHARSET=latin1;

/*Table structure for table patientsqueue */

CREATE TABLE patientsqueue (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  treatmentpointcode VARCHAR(50) DEFAULT '',
  laboratorycode VARCHAR(50) DEFAULT '',
  patientcode VARCHAR(50) DEFAULT '',
  queuetype INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table patientsurnames */

CREATE TABLE patientsurnames (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_antenatalattendances */

CREATE TABLE rch_antenatalattendances (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  clientcode VARCHAR(50) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  cardpresented INT(11) DEFAULT NULL,
  pregage INT(11) DEFAULT NULL,
  noofpreg INT(11) DEFAULT NULL,
  height DOUBLE(11,2) DEFAULT '0.00',
  discount INT(11) DEFAULT NULL,
  syphilistest INT(11) DEFAULT NULL,
  lastbirthyear INT(11) DEFAULT NULL,
  lastborndeath INT(11) DEFAULT NULL,
  referedto VARCHAR(255) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT NULL,
  pmtcttest INT(11) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_antenatalattendancelog */

CREATE TABLE rch_antenatalattendancelog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(50) DEFAULT NULL,
  clientcode VARCHAR(50) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  cardpresented INT(11) DEFAULT NULL,
  pregage INT(11) DEFAULT NULL,
  noofpreg INT(11) DEFAULT NULL,
  height DOUBLE(11,2) DEFAULT '0.00',
  discount INT(11) DEFAULT NULL,
  syphilistest INT(11) DEFAULT NULL,
  lastbirthyear INT(11) DEFAULT NULL,
  lastborndeath INT(11) DEFAULT NULL,
  referedto VARCHAR(255) DEFAULT NULL,
  registrystatus VARCHAR(50) DEFAULT 'New',
  userid VARCHAR(50) DEFAULT NULL,
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  sysdate DATETIME DEFAULT NULL,
  pmtcttest INT(11) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_childrenattendancelog */

CREATE TABLE rch_childrenattendancelog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(50) DEFAULT NULL,
  clientcode VARCHAR(50) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  registrystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT NULL,
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  sysdate DATETIME DEFAULT NULL,
  weight DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_childrenattendances */

CREATE TABLE rch_childrenattendances (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  clientcode VARCHAR(50) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  userid VARCHAR(50) DEFAULT NULL,
  weight DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_fplanattendances */

CREATE TABLE rch_fplanattendances (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  bookdate DATETIME DEFAULT NULL,
  clientcode VARCHAR(20) DEFAULT NULL,
  complains VARCHAR(255) DEFAULT NULL,
  userid VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_fplanattendancelog */

CREATE TABLE rch_fplanattendancelog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  booking VARCHAR(50) DEFAULT NULL,
  clientcode VARCHAR(50) DEFAULT NULL,
  registrystatus VARCHAR(50) DEFAULT 'New',
  complains VARCHAR(255) DEFAULT NULL,
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_fplanmethods */

CREATE TABLE rch_fplanmethods (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  quantityentry INT(11) DEFAULT '0',
  inactive INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_dangerindicators */

CREATE TABLE rch_dangerindicators (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  quantityentry INT(11) DEFAULT '0',
  inactive INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_birthmethods */

CREATE TABLE rch_birthmethods (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  inactive INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_birthcomplications */

CREATE TABLE rch_birthcomplications (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  inactive INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_fplanmethodslog */

CREATE TABLE rch_fplanmethodslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(50) DEFAULT NULL,
  clientcode VARCHAR(50) DEFAULT NULL,
  methodcode VARCHAR(50) DEFAULT NULL,
  quantity DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_dangerindicatorslog */

CREATE TABLE rch_dangerindicatorslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(50) DEFAULT NULL,
  clientcode VARCHAR(50) DEFAULT NULL,
  methodcode VARCHAR(50) DEFAULT NULL,
  quantity DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_birthcomplicationslog */

CREATE TABLE rch_birthcomplicationslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(50) DEFAULT NULL,
  clientcode VARCHAR(50) DEFAULT NULL,
  methodcode VARCHAR(50) DEFAULT NULL,
  quantity DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_postnatalattendances */

CREATE TABLE rch_postnatalattendances (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  clientcode VARCHAR(20) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  gravida INT(11) DEFAULT NULL,
  para INT(11) DEFAULT NULL,
  admissiondate DATETIME DEFAULT NULL,
  noofchildren INT(11) DEFAULT NULL,
  dischargestatus VARCHAR(255) DEFAULT NULL,
  attendantid VARCHAR(50) DEFAULT NULL,
  attendantname VARCHAR(255) DEFAULT NULL,
  userid VARCHAR(20) DEFAULT NULL,
  birthmethod VARCHAR(50) DEFAULT NULL,
  fromantenatal INT(11) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_postnatalattendancelog */

CREATE TABLE rch_postnatalattendancelog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(20) DEFAULT NULL,
  clientcode VARCHAR(20) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  gravida INT(11) DEFAULT NULL,
  para INT(11) DEFAULT NULL,
  admissiondate DATETIME DEFAULT NULL,
  noofchildren INT(11) DEFAULT NULL,
  dischargestatus VARCHAR(255) DEFAULT NULL,
  attendantid VARCHAR(50) DEFAULT NULL,
  attendantname VARCHAR(255) DEFAULT NULL,
  registrystatus VARCHAR(50) DEFAULT 'New',
  userid VARCHAR(20) DEFAULT NULL,
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  birthmethod VARCHAR(50) DEFAULT NULL,
  fromantenatal INT(11) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_postnatalchildren */

CREATE TABLE rch_postnatalchildren (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(20) DEFAULT NULL,
  clientcode VARCHAR(20) DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  deliverydate DATETIME DEFAULT NULL,
  gender VARCHAR(10) DEFAULT NULL,
  weight DOUBLE(11,2) DEFAULT '0.00',
  apgarscore DOUBLE(11,2) DEFAULT '0.00',
  freshbirth INT(11) DEFAULT NULL,
  maceratedbirth INT(11) DEFAULT NULL,
  childproblems VARCHAR(255) DEFAULT NULL,
  live INT(11) DEFAULT NULL,
  deathbefore24 INT(11) DEFAULT NULL,
  deathafter24 INT(11) DEFAULT NULL,
  userid VARCHAR(20) DEFAULT NULL,
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  SYSDATE DATETIME DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_vaccines */

CREATE TABLE rch_vaccines (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  displayorder INT(11) DEFAULT '0',
  inactive INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_vaccinations */

CREATE TABLE rch_vaccinations (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  clientcode VARCHAR(20) DEFAULT NULL,
  servicetype INT(11) DEFAULT '0',
  userid VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_vaccinationslog */

CREATE TABLE rch_vaccinationslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  booking VARCHAR(20) DEFAULT NULL,
  fplanbooking VARCHAR(20) DEFAULT NULL,
  antenatalbooking VARCHAR(20) DEFAULT NULL,
  postnatalbooking VARCHAR(20) DEFAULT NULL,
  childrenbooking VARCHAR(20) DEFAULT NULL,
  clientcode VARCHAR(20) DEFAULT NULL,
  servicetype INT(11) DEFAULT '0',
  registrystatus VARCHAR(50) DEFAULT 'New',
  yearpart INT(11) DEFAULT NULL,
  monthpart INT(11) DEFAULT NULL,
  userid VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table rch_vaccineslog */

CREATE TABLE rch_vaccineslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  booking VARCHAR(20) DEFAULT NULL,
  clientcode VARCHAR(20) DEFAULT NULL,
  vaccinecode VARCHAR(50) DEFAULT '',
  remarks VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table regions */

CREATE TABLE regions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  countrycode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table revisitingnumbers */

CREATE TABLE revisitingnumbers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  patientcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_customers */

CREATE TABLE som_customers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(20) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  phone VARCHAR(255) DEFAULT NULL,
  address VARCHAR(255) DEFAULT NULL,
  fax VARCHAR(255) DEFAULT NULL,
  email VARCHAR(255) DEFAULT NULL,
  website VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_orderitems */

CREATE TABLE som_orderitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  orderno VARCHAR(50) DEFAULT '',
  itemorderid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  orderedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_orderreceiveditems */

CREATE TABLE som_orderreceiveditems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  orderno VARCHAR(50) DEFAULT '',
  deliveryno VARCHAR(50) DEFAULT '',
  itemorderid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  receivedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_orders */

CREATE TABLE som_orders (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  orderno VARCHAR(50) DEFAULT '',
  ordertitle VARCHAR(255) DEFAULT '',
  shiptodescription VARCHAR(255) DEFAULT '',
  suppliercode VARCHAR(50) DEFAULT '',
  supplierdescription VARCHAR(255) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  exchangerate DOUBLE(11,2) DEFAULT '0.00',
  currencysymbol VARCHAR(50) DEFAULT '',
  remarks VARCHAR(255) DEFAULT '',
  orderstatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_packagings */

CREATE TABLE som_packagings (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  pieces INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_physicalinventory */

CREATE TABLE som_physicalinventory (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  calculateddate DATETIME DEFAULT NULL,
  closeddate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  storedescription VARCHAR(255) DEFAULT '',
  referenceno VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  inventorystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_physicalinventoryitems */

CREATE TABLE som_physicalinventoryitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  calculateddate DATETIME DEFAULT NULL,
  closeddate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  referenceno VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  countedqty DOUBLE(11,2) DEFAULT '0.00',
  expectedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_physicalstockbalances */

CREATE TABLE som_physicalstockbalances (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  transdate DATETIME DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  quantity DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_productcategories */

CREATE TABLE som_productcategories (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_productcontrol */

CREATE TABLE som_productcontrol (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  productcode VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_productexpirydates */

CREATE TABLE som_productexpirydates (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  storecode VARCHAR(50) DEFAULT NULL,
  productcode VARCHAR(50) DEFAULT NULL,
  expirydate DATETIME DEFAULT NULL,
  quantity DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_productcostslog */

CREATE TABLE som_productcostslog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  productcode VARCHAR(50) DEFAULT '',
  costprice DOUBLE(11,2) DEFAULT '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_products */

CREATE TABLE som_products (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  opmcode VARCHAR(50) DEFAULT '',
  opmdescription VARCHAR(255) DEFAULT '',
  departmentcode VARCHAR(50) DEFAULT NULL,
  departmentdescription VARCHAR(255) DEFAULT NULL,
  packagingcode VARCHAR(50) DEFAULT NULL,
  packagingdescription VARCHAR(255) DEFAULT NULL,
  piecesinpackage INT(11) DEFAULT '1',
  prevcost DOUBLE(11,2) DEFAULT '0.00',
  costprice DOUBLE(11,2) DEFAULT '0.00',
  averagecost DOUBLE(11,2) DEFAULT '0.00',
  price1 DOUBLE(11,2) DEFAULT '0.00',
  price2 DOUBLE(11,2) DEFAULT '0.00',
  price3 DOUBLE(11,2) DEFAULT '0.00',
  price4 DOUBLE(11,2) DEFAULT '0.00',
  price5 DOUBLE(11,2) DEFAULT '0.00',
  price6 DOUBLE(11,2) DEFAULT '0.00',
  price7 DOUBLE(11,2) DEFAULT '0.00',
  price8 DOUBLE(11,2) DEFAULT '0.00',
  price9 DOUBLE(11,2) DEFAULT '0.00',
  price10 DOUBLE(11,2) DEFAULT '0.00',
  addtocart INT(11) DEFAULT '0',
  defaultqty DOUBLE(11,2) DEFAULT '0.00',
  hasexpiry INT(11) DEFAULT '0',
  expirynotice INT(11) DEFAULT '0',
  display INT(11) DEFAULT '1',
  minlevel double(11,2) default '0.00',
  orderqty double(11,2) default '0.00',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_producttransactions */

CREATE TABLE som_producttransactions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  sourcecode VARCHAR(50) DEFAULT NULL,
  sourcedescription VARCHAR(255) DEFAULT NULL,
  productcode VARCHAR(50) DEFAULT NULL,
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT NULL,
  packagingdescription VARCHAR(255) DEFAULT NULL,
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  reference VARCHAR(50) DEFAULT NULL,
  transprice DOUBLE(11,2) DEFAULT '0.00',
  transtype VARCHAR(50) DEFAULT NULL,
  transdescription VARCHAR(255) DEFAULT NULL,
  userid VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_stockissueitems */

CREATE TABLE som_stockissueitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferinno VARCHAR(50) DEFAULT '',
  deliveryno VARCHAR(50) DEFAULT '',
  itemtransferid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  fromstorecode VARCHAR(50) DEFAULT '',
  tostorecode VARCHAR(50) DEFAULT '',
  issuedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_stockissues */

CREATE TABLE som_stockissues (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  deliveryno VARCHAR(50) DEFAULT '',
  transferno VARCHAR(50) DEFAULT '',
  transferinno VARCHAR(50) DEFAULT '',
  transfertitle VARCHAR(255) DEFAULT '',
  fromcode VARCHAR(50) DEFAULT '',
  fromdescription VARCHAR(255) DEFAULT '',
  tocode VARCHAR(50) DEFAULT '',
  todescription VARCHAR(255) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  exchangerate DOUBLE(11,2) DEFAULT '0.00',
  currencysymbol VARCHAR(50) DEFAULT '',
  remarks VARCHAR(255) DEFAULT '',
  transferstatus VARCHAR(50) DEFAULT '',
  transfertype VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_stockreceiptitems */

CREATE TABLE som_stockreceiptitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  orderno VARCHAR(50) DEFAULT '',
  transferno VARCHAR(50) DEFAULT '',
  transferoutno VARCHAR(50) DEFAULT '',
  deliveryno VARCHAR(50) DEFAULT '',
  itemtransferid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  fromstorecode VARCHAR(50) DEFAULT '',
  tostorecode VARCHAR(50) DEFAULT '',
  receivedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_stockreceipts */

CREATE TABLE som_stockreceipts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  deliveryno VARCHAR(50) DEFAULT '',
  orderno VARCHAR(50) DEFAULT '',
  transferno VARCHAR(50) DEFAULT '',
  transferoutno VARCHAR(50) DEFAULT '',
  transfertitle VARCHAR(255) DEFAULT '',
  fromcode VARCHAR(50) DEFAULT '',
  fromdescription VARCHAR(255) DEFAULT '',
  tocode VARCHAR(50) DEFAULT '',
  todescription VARCHAR(255) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  exchangerate DOUBLE(11,2) DEFAULT '0.00',
  currencysymbol VARCHAR(50) DEFAULT '',
  remarks VARCHAR(255) DEFAULT '',
  transferstatus VARCHAR(50) DEFAULT '',
  transfertype VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_stores */

CREATE TABLE som_stores (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_suppliers */

CREATE TABLE som_suppliers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(20) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  phone VARCHAR(255) DEFAULT NULL,
  address VARCHAR(255) DEFAULT NULL,
  fax VARCHAR(255) DEFAULT NULL,
  email VARCHAR(255) DEFAULT NULL,
  website VARCHAR(255) DEFAULT NULL,
  suppliertype INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_transferinitems */

CREATE TABLE som_transferinitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferoutno VARCHAR(50) DEFAULT '',
  itemtransferid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  transferedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_transferinreceiveditems */

CREATE TABLE som_transferinreceiveditems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferoutno VARCHAR(50) DEFAULT '',
  deliveryno VARCHAR(50) DEFAULT '',
  itemtransferid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  fromstorecode VARCHAR(50) DEFAULT '',
  tostorecode VARCHAR(50) DEFAULT '',
  receivedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_transferins */

CREATE TABLE som_transferins (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferoutno VARCHAR(50) DEFAULT '',
  transfertitle VARCHAR(255) DEFAULT '',
  fromcode VARCHAR(50) DEFAULT '',
  fromdescription VARCHAR(255) DEFAULT '',
  tocode VARCHAR(50) DEFAULT '',
  todescription VARCHAR(255) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  exchangerate DOUBLE(11,2) DEFAULT '0.00',
  currencysymbol VARCHAR(50) DEFAULT '',
  remarks VARCHAR(255) DEFAULT '',
  transferstatus VARCHAR(50) DEFAULT '',
  transfertype VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_transferoutissueditems */

CREATE TABLE som_transferoutissueditems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferinno VARCHAR(50) DEFAULT '',
  deliveryno VARCHAR(50) DEFAULT '',
  itemtransferid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  expirydate DATETIME DEFAULT NULL,
  fromstorecode VARCHAR(50) DEFAULT '',
  tostorecode VARCHAR(50) DEFAULT '',
  issuedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_transferoutitems */

CREATE TABLE som_transferoutitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferinno VARCHAR(50) DEFAULT '',
  itemtransferid VARCHAR(50) DEFAULT '',
  productcode VARCHAR(50) DEFAULT '',
  productdescription VARCHAR(255) DEFAULT '',
  productopmcode VARCHAR(50) DEFAULT '',
  productopmdescription VARCHAR(255) DEFAULT '',
  productdepartmentcode VARCHAR(50) DEFAULT '',
  productdepartmentdescription VARCHAR(255) DEFAULT '',
  packagingcode VARCHAR(50) DEFAULT '',
  packagingdescription VARCHAR(255) DEFAULT '',
  piecesinpackage INT(11) DEFAULT '0',
  transferedqty DOUBLE(11,2) DEFAULT '0.00',
  transprice DOUBLE(16,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table som_transferouts */

CREATE TABLE som_transferouts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  SYSDATE DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  transferno VARCHAR(50) DEFAULT '',
  transferinno VARCHAR(50) DEFAULT '',
  transfertitle VARCHAR(255) DEFAULT '',
  fromcode VARCHAR(50) DEFAULT '',
  fromdescription VARCHAR(255) DEFAULT '',
  tocode VARCHAR(50) DEFAULT '',
  todescription VARCHAR(255) DEFAULT '',
  currencycode VARCHAR(50) DEFAULT '',
  currencydescription VARCHAR(255) DEFAULT '',
  exchangerate DOUBLE(11,2) DEFAULT '0.00',
  currencysymbol VARCHAR(50) DEFAULT '',
  remarks VARCHAR(255) DEFAULT '',
  transferstatus VARCHAR(50) DEFAULT '',
  transfertype VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_formsizes */

CREATE TABLE sys_formsizes (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  formname VARCHAR(80) DEFAULT '',
  formwidth INT(11) DEFAULT '0',
  formheight INT(11) DEFAULT '0',
  layoutname VARCHAR(255) DEFAULT '',
  formlayout LONGBLOB,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_gridlayouts */

CREATE TABLE sys_gridlayouts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  gridname VARCHAR(80) DEFAULT '',
  userid VARCHAR(80) DEFAULT '',
  gridlayout LONGBLOB,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_migrationlog */

CREATE TABLE sys_migrationlog (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  tablename VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_reportcharts */

CREATE TABLE sys_reportcharts (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  reportcode VARCHAR(50) DEFAULT '',
  value1 DOUBLE(16,2) DEFAULT '0.00',
  value2 DOUBLE(16,2) DEFAULT '-1.00',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_reportfilterconditions */

CREATE TABLE sys_reportfilterconditions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  reportcode VARCHAR(50) DEFAULT '',
  conditionfieldname VARCHAR(255) DEFAULT '',
  conditionoperator VARCHAR(50) DEFAULT '',
  conditionvalue1 VARCHAR(255) DEFAULT '',
  conditionvalue2 VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_reportgroups */

CREATE TABLE sys_reportgroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_reportparameters */

CREATE TABLE sys_reportparameters (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  reportcode VARCHAR(50) DEFAULT '',
  parametercode VARCHAR(50) DEFAULT '',
  parameterdescription VARCHAR(255) DEFAULT '',
  parametertype VARCHAR(50) DEFAULT '',
  parametercontrol VARCHAR(50) DEFAULT '',
  parametersavevalue INT(11) DEFAULT '0',
  lookuptablename VARCHAR(255) DEFAULT '',
  valuestring VARCHAR(255) DEFAULT '',
  valuedatetime DATETIME DEFAULT NULL,
  valueint INT(11) DEFAULT '0',
  valuedouble DOUBLE(16,2) DEFAULT '0.00',
  optional INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_reports */

CREATE TABLE sys_reports (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  groupcode VARCHAR(50) DEFAULT '',
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  tablename VARCHAR(255) DEFAULT '',
  commandtext VARCHAR(2000) DEFAULT '',
  filterstring VARCHAR(1000) DEFAULT '',
  groupbyfields VARCHAR(1000) DEFAULT '',
  reporttemplate LONGBLOB,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_searchfields */

CREATE TABLE sys_searchfields (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  tablename VARCHAR(80) DEFAULT NULL,
  fieldname VARCHAR(80) DEFAULT NULL,
  fielddisplayname VARCHAR(255) DEFAULT NULL,
  defaultfield INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_storeusers */

CREATE TABLE sys_storeusers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  usercode VARCHAR(50) DEFAULT NULL,
  storecode VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_useractions */

CREATE TABLE sys_useractions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  userid VARCHAR(50) DEFAULT '',
  searchproductswhiletyping INT(11) DEFAULT '0',
  searchproductsoption INT(11) DEFAULT '0',
  searchproductfieldname VARCHAR(255) DEFAULT '',
  searchpatientswhiletyping INT(11) DEFAULT '0',
  searchpatientsoption INT(11) DEFAULT '0',
  searchpatientfieldname VARCHAR(255) DEFAULT '',
  searchclientgroupmemberswhiletyping INT(11) DEFAULT '0',
  searchclientgroupmembersoption INT(11) DEFAULT '0',
  searchclientgroupmemberfieldname VARCHAR(255) DEFAULT '',
  searchclientgroupswhiletyping INT(11) DEFAULT '0',
  searchclientgroupsoption INT(11) DEFAULT '0',
  searchclientgroupfieldname VARCHAR(255) DEFAULT '',
  searchbillingitemgroupswhiletyping INT(11) DEFAULT '0',
  searchbillingitemgroupsoption INT(11) DEFAULT '0',
  searchbillingitemgroupfieldname VARCHAR(255) DEFAULT '',
  searchdebtorswhiletyping INT(11) DEFAULT '0',
  searchdebtorsoption INT(11) DEFAULT '0',
  searchdebtorfieldname VARCHAR(255) DEFAULT '',
  searchdiagnoseswhiletyping INT(11) DEFAULT '0',
  searchdiagnosesoption INT(11) DEFAULT '0',
  searchdiagnosisfieldname VARCHAR(255) DEFAULT '',
  searchsupplierswhiletyping INT(11) DEFAULT '0',
  searchsuppliersoption INT(11) DEFAULT '0',
  searchsupplierfieldname VARCHAR(255) DEFAULT '',
  searchstoreswhiletyping INT(11) DEFAULT '0',
  searchstoresoption INT(11) DEFAULT '0',
  searchstorefieldname VARCHAR(255) DEFAULT '',
  searchrchclientswhiletyping INT(11) DEFAULT '0',
  searchrchclientsoption INT(11) DEFAULT '0',
  searchrchclientfieldname VARCHAR(255) DEFAULT '',
  searchindicatorswhiletyping INT(11) DEFAULT '0',
  searchindicatorsoption INT(11) DEFAULT '0',
  searchindicatorfieldname VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_usergroupfunctions */

CREATE TABLE sys_usergroupfunctions (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  usergroupcode VARCHAR(50) DEFAULT '',
  functionaccesskey VARCHAR(255) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_usergroupmoduleitems */

CREATE TABLE sys_usergroupmoduleitems (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  usergroupcode VARCHAR(50) DEFAULT '',
  moduleitemkey VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_usergroupprinters */

CREATE TABLE sys_usergroupprinters (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  machinename VARCHAR(50) DEFAULT '',
  usergroupcode VARCHAR(50) DEFAULT '',
  documenttypecode VARCHAR(50) DEFAULT '',
  printername VARCHAR(255) DEFAULT '',
  printedwhen INT(11) DEFAULT '-1',
  autoprint INT(11) DEFAULT '0',
  printtoscreen INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_usergroupreports */

CREATE TABLE sys_usergroupreports (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  usergroupcode VARCHAR(50) DEFAULT '',
  reportcode VARCHAR(50) DEFAULT '',
  reportview INT(11) DEFAULT '1',
  reportdesign INT(11) DEFAULT '0',
  reportformcustomization INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_usergroups */

CREATE TABLE sys_usergroups (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  formlayouttemplatename VARCHAR(255) DEFAULT '',
  staffcategorycode VARCHAR(50) DEFAULT '',
  synchronizewithstaffs INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_users */

CREATE TABLE sys_users (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  CODE VARCHAR(50) DEFAULT '',
  description VARCHAR(255) DEFAULT '',
  PASSWORD VARCHAR(50) DEFAULT '',
  usergroupcode VARCHAR(50) DEFAULT '',
  address VARCHAR(255) DEFAULT '',
  phone VARCHAR(50) DEFAULT '',
  occupation VARCHAR(255) DEFAULT '',
  storecode VARCHAR(50) DEFAULT '',
  defaultlanguage VARCHAR(255) DEFAULT '',
  defaultskinname VARCHAR(255) DEFAULT '',
  mainnavbarwidth INT(11) DEFAULT '257',
  displaystore INT(11) DEFAULT '0',
  allowchangingstore INT(11) DEFAULT '0',
  defaultclientgroupcode VARCHAR(50) DEFAULT NULL,
  defaultclientsubgroupcode VARCHAR(50) DEFAULT NULL,
  allowchangingclientgroup INT(11) DEFAULT '1',
  defaultpaymenttypecode VARCHAR(50) DEFAULT NULL,
  allowchangingpaymenttype INT(11) DEFAULT '1',
  defaultpricecategorycode VARCHAR(50) DEFAULT NULL,
  allowchangingpricecategory INT(11) DEFAULT '1',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_versioncontrol */

CREATE TABLE sys_versioncontrol (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  majornumber INT(11) DEFAULT '0',
  minornumber INT(11) DEFAULT '0',
  buildnumber INT(11) DEFAULT '0',
  revisionnumber INT(11) DEFAULT '0',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table sys_workstations */

CREATE TABLE sys_workstations (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  machinename VARCHAR(255) DEFAULT NULL,
  defaultskinname VARCHAR(255) DEFAULT NULL,
  defaultlanguage VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table maritalstatus */

CREATE TABLE maritalstatus (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_functionalstatus */

CREATE TABLE ctc_functionalstatus (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_tbstatus */

CREATE TABLE ctc_tbstatus (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_arvstatus */

CREATE TABLE ctc_arvstatus (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_aidsillness */

CREATE TABLE ctc_aidsillness (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_arvcombregimens */

CREATE TABLE ctc_arvcombregimens (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  category VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_arvadherence */

CREATE TABLE ctc_arvadherence (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_arvpooradherencereasons */

CREATE TABLE ctc_arvpooradherencereasons (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_referedto */

CREATE TABLE ctc_referedto (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_arvreason */

CREATE TABLE ctc_arvreason (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  category VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_followupstatus */

CREATE TABLE ctc_followupstatus (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_referedfrom */

CREATE TABLE ctc_referedfrom (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_priorarvexposure */

CREATE TABLE ctc_priorarvexposure (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_artwhyeligible */

CREATE TABLE ctc_artwhyeligible (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_relevantcomeds */

CREATE TABLE ctc_relevantcomeds (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_abnormallabresults */

CREATE TABLE ctc_abnormallabresults (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  code VARCHAR(50) DEFAULT NULL,
  description VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_patients */

CREATE TABLE ctc_patients (
	autocode INT(11) NOT NULL AUTO_INCREMENT UNIQUE,
	patientcode VARCHAR(50) NOT NULL,
	hivno VARCHAR(50) DEFAULT '',
	ctcno VARCHAR(50) DEFAULT '',
	referredfromcode VARCHAR(50) DEFAULT '',
	referredfromother VARCHAR(255) DEFAULT '',
	priorarvexposurecode VARCHAR(50) DEFAULT '',
	enrolledincaredate DATETIME DEFAULT NULL,
	medeligibledate DATETIME DEFAULT NULL,
	whyeligiblecode VARCHAR(50) DEFAULT '',
	whyeligiblecd4 DOUBLE(11,2) DEFAULT '0.00',
	whyeligibletlc DOUBLE(11,2) DEFAULT '0.00',
	eligibleandreadydate DATETIME DEFAULT NULL,
	supportersurname VARCHAR(255) DEFAULT '',
	supporterfirstname VARCHAR(255) DEFAULT '',
	supporterothernames VARCHAR(255) DEFAULT '',
	supporteraddress VARCHAR(255) DEFAULT '',
	supportertelephone VARCHAR(255) DEFAULT '',
	supportercommunity VARCHAR(255) DEFAULT '',
	PRIMARY KEY  (patientcode),
	FOREIGN KEY (patientcode) REFERENCES patients(code)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_bookings */

CREATE TABLE ctc_bookings (
  autocode VARCHAR(50) NOT NULL DEFAULT '',
  sysdate DATETIME DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) NOT NULL,
  wheretakencode VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_bookinglog */

CREATE TABLE ctc_bookinglog (
  autocode INT(11) NOT NULL AUTO_INCREMENT UNIQUE,
  sysdate DATETIME DEFAULT NULL,
  bookdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) NOT NULL,
  booking VARCHAR(50) NOT NULL,
  wheretakencode VARCHAR(50) DEFAULT '',
  registrystatus VARCHAR(50) DEFAULT '',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_appointments */

CREATE TABLE ctc_appointments (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) NOT NULL,
  booking VARCHAR(50) NOT NULL,
  appttype INT(11) DEFAULT '0',
  reason VARCHAR(50) DEFAULT '',
  wheretakencode VARCHAR(50) NULL,
  apptdate DATETIME DEFAULT NULL,
  attdate DATETIME DEFAULT NULL,
  apptstatus INT(11) DEFAULT '0',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode),
  FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_triage */

CREATE TABLE ctc_triage (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  sysdate DATETIME DEFAULT NULL,
  transdate DATETIME DEFAULT NULL,
  patientcode VARCHAR(50) NOT NULL,
  booking VARCHAR(50) NOT NULL,
  weight DOUBLE(11,2) DEFAULT '0.00',
  height DOUBLE(11,2) DEFAULT '0.00',
  pulse DOUBLE(11,2) DEFAULT '0.00',
  bloodpressure DOUBLE(11,2) DEFAULT '0.00',
  resprate DOUBLE(11,2) DEFAULT '0.00',
  temperature DOUBLE(11,2) DEFAULT '0.00',
  userid VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode),
  FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_hivtests */

CREATE TABLE ctc_hivtests (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	testresult INT(11) DEFAULT '0',
	counsellorcode VARCHAR(50) DEFAULT '',
	isfirsttest INT(11) DEFAULT '0',
	isconfirmtest INT(11) DEFAULT '0',
	userid VARCHAR(50) DEFAULT '',
    PRIMARY KEY  (autocode),
    FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_cd4tests */

CREATE TABLE ctc_cd4tests (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	sampleid VARCHAR(50) DEFAULT '',
	testresult DOUBLE(11,2) DEFAULT '0.00',
	testresultdate DATETIME DEFAULT NULL,
	comments VARCHAR(255) DEFAULT '',
	labtechniciancode VARCHAR(50) DEFAULT '',
	userid VARCHAR(50) DEFAULT '',
    PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_preventivetherapies */

CREATE TABLE ctc_preventivetherapies (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	therapyid INT(11) DEFAULT '0',
	userid VARCHAR(50) DEFAULT '',
    PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_revisitingnumbers */

CREATE TABLE ctc_revisitingnumbers (
  autocode INT(11) NOT NULL AUTO_INCREMENT,
  patientcode VARCHAR(50) DEFAULT '',
  PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_artvisits */

CREATE TABLE ctc_artvisits (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	clinicalstage INT(11) DEFAULT '0',
	pregnant INT(11) DEFAULT '0',
	lactating INT(11) DEFAULT '0',
	functionalstatuscode VARCHAR(50) DEFAULT '',
	tbstatuscode VARCHAR(50) DEFAULT '',
	arvstatuscode VARCHAR(50) DEFAULT '',
	arvreasoncode VARCHAR(50) DEFAULT '',
	arvcombregimencode VARCHAR(50) DEFAULT '',
	arvadherencecode VARCHAR(50) DEFAULT '',
	arvpooradherencereasoncode VARCHAR(50) DEFAULT '',
	hb DOUBLE(11,2) DEFAULT '0.00',
	alt DOUBLE(11,2) DEFAULT '0.00',
	nutritionalsupport INT(11) DEFAULT '0',
	referredtocode VARCHAR(50) DEFAULT '',
	followupstatuscode VARCHAR(50) DEFAULT '',
	cliniciancode VARCHAR(50) DEFAULT '',
	isfirstvisit INT(11) DEFAULT '0',
	userid VARCHAR(50) DEFAULT '',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_patientaidsillness */

CREATE TABLE ctc_patientaidsillness (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	illnesscode VARCHAR(50) DEFAULT '',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_patientcomeds */

CREATE TABLE ctc_patientcomeds (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	medcode VARCHAR(50) DEFAULT '',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_patientabnormallabresults */

CREATE TABLE ctc_patientabnormallabresults (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	resultcode VARCHAR(50) DEFAULT '',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctdisclosedto */

CREATE TABLE ctc_pmtctdisclosedto (
	autocode INT(11) NOT NULL AUTO_INCREMENT UNIQUE,
	CODE VARCHAR(50) DEFAULT '',
	description VARCHAR(255) DEFAULT '',
	PRIMARY KEY  (CODE)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctantenatal */

CREATE TABLE ctc_pmtctantenatal (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	incidencecode VARCHAR(50) DEFAULT '',
	seenatanothersite INT(11) DEFAULT '0',
	anothersitename VARCHAR(255) DEFAULT '',
	gestationage DOUBLE(11,2) DEFAULT '0.00',
	disclosedtocode VARCHAR(50) DEFAULT '',
	onhaartforlife INT(11) DEFAULT '0',
	whenstartedhaartforlife INT(11) DEFAULT '0',
	infantfeedingintention INT(11) DEFAULT '0',
	plantodeliverinfacility INT(11) DEFAULT '0',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking),
	FOREIGN KEY (disclosedtocode) REFERENCES ctc_pmtctdisclosedto(`code`)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctcomb */

CREATE TABLE ctc_pmtctcomb (
	autocode INT(11) NOT NULL AUTO_INCREMENT UNIQUE,
	CODE VARCHAR(50) DEFAULT '',
	description VARCHAR(255) DEFAULT '',
	PRIMARY KEY  (CODE)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctantenatallog */

CREATE TABLE ctc_pmtctantenatallog (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	incidencecode VARCHAR(50) DEFAULT '',
	clinicalstage INT(11) DEFAULT '0',
	bactrimstarted INT(11) DEFAULT '0',
	pmtctcombcode VARCHAR(50) DEFAULT '',
	cameincouple INT(11) DEFAULT '0',
	partnerresult INT(11) DEFAULT '0',
	referedto VARCHAR(255) DEFAULT '',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking),
	FOREIGN KEY (pmtctcombcode) REFERENCES ctc_pmtctcomb(CODE)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctotherchildren */
CREATE TABLE ctc_pmtctotherchildren (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	patientcode VARCHAR(50) DEFAULT '',
	birthdate DATETIME DEFAULT NULL,
	hivstatus INT(11) DEFAULT '0',
	PRIMARY KEY  (autocode)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctpostnatal */

CREATE TABLE ctc_pmtctpostnatal (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	incidencecode VARCHAR(50) DEFAULT '',
	seenatanothersite INT(11) DEFAULT '0',
	anothersitename VARCHAR(255) DEFAULT '',
	babybirthdate DATETIME DEFAULT NULL,
	disclosedtocode VARCHAR(50) DEFAULT '',
	motheronhaartforlife INT(11) DEFAULT '0',
	babyonhaartforlife INT(11) DEFAULT '0',
	deliverinfacility INT(11) DEFAULT '0',
	babyhivdate DATETIME DEFAULT NULL,
	babyhivresult INT(11) DEFAULT '0',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking),
	FOREIGN KEY (disclosedtocode) REFERENCES ctc_pmtctdisclosedto(`code`)
) ENGINE=INNODB DEFAULT CHARSET=latin1;

/*Table structure for table ctc_pmtctpostnatallog */

CREATE TABLE ctc_pmtctpostnatallog (
	autocode INT(11) NOT NULL AUTO_INCREMENT,
	sysdate DATETIME DEFAULT NULL,
	transdate DATETIME DEFAULT NULL,
	patientcode VARCHAR(50) DEFAULT '',
	booking VARCHAR(50) DEFAULT '',
	incidencecode VARCHAR(50) DEFAULT '',
	clinicalstage INT(11) DEFAULT '0',
	babybactrim INT(11) DEFAULT '0',
	motherlabourcombcode VARCHAR(50) DEFAULT '',
	motherpostcombcode VARCHAR(50) DEFAULT '',
	babycombcode VARCHAR(50) DEFAULT '',
	cameincouple INT(11) DEFAULT '0',
	partnerresult INT(11) DEFAULT '0',
	familyplanmethod INT(11) DEFAULT '0',
	infantfeedingoption INT(11) DEFAULT '0',
	referedto VARCHAR(255) DEFAULT '',
	PRIMARY KEY  (autocode),
	FOREIGN KEY (patientcode,booking) REFERENCES ctc_bookinglog(patientcode,booking),
	FOREIGN KEY (motherlabourcombcode) REFERENCES ctc_pmtctcomb(code),
	FOREIGN KEY (motherpostcombcode) REFERENCES ctc_pmtctcomb(code),
	FOREIGN KEY (babycombcode) REFERENCES ctc_pmtctcomb(code)
) ENGINE=INNODB DEFAULT CHARSET=latin1;




