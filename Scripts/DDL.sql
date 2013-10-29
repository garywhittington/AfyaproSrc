/*  Alter Table */
	
/*  Create Table */
CREATE TABLE `sys_userlogin`(
	`autocde` INT(11) NOT NULL  AUTO_INCREMENT , 
	`usercode` VARCHAR(50)  , 
	`machinename` VARCHAR(255)  , 
	`logintime` DATETIME NULL  , 
	`logouttime` DATETIME NULL  , 
	`processid` INT(11) NULL  , 
	PRIMARY KEY (`autocde`) 
);

CREATE TABLE `ctc_hivappointmentreason`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`code` VARCHAR(10)  NULL, 
	`description` VARCHAR(50)   NULL, 
	PRIMARY KEY (`autocode`) 
);


/* Create table in target */
CREATE TABLE `ctc_hivclientsupport`(
	`autocde` INT(11) NOT NULL  AUTO_INCREMENT , 
	`code` VARCHAR(50)  NULL, 
	`description` VARCHAR(50)  NULL, 
	PRIMARY KEY (`autocde`) 
) ;


/* Create table in target */
CREATE TABLE `ctc_hivclientsupportlog`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`booking` VARCHAR(50)  NULL, 
	`patientcode` VARCHAR(50) NULL, 
	`supportcode` VARCHAR(50) NULL, 
	PRIMARY KEY (`autocode`) 
) ;


/* Create table in target */
CREATE TABLE `ctc_hivcounselling`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`booking` VARCHAR(50) NULL, 
	`patientcode` VARCHAR(50) NULL, 
	`problemnotes` VARCHAR(200) NULL, 
	`observation` VARCHAR(200) NULL, 
	`medicalsymptoms` VARCHAR(200) NULL, 
	`reactionpositive` VARCHAR(100) NULL, 
	`reactionnegative` VARCHAR(100)  NULL, 
	`shareresultwith` VARCHAR(100) NULL, 
	`preclientassessmentnotes` VARCHAR(200)  NULL, 
	`clientdecision` TINYINT(1) NULL  DEFAULT '0' , 
	`preinterventionnotes` VARCHAR(200)  NULL  , 
	`understoodinformation` TINYINT(1) NULL  DEFAULT '0' , 
	`willingtoaccept` TINYINT(1) NULL  DEFAULT '0' , 
	`resultcollectiondate` DATETIME NULL  , 
	`accompaniedby` TINYINT(1) NULL  DEFAULT '0' , 
	`postproblemnotes` VARCHAR(200)  NULL  , 
	`willingtoreceiveresult` TINYINT(1) NULL  , 
	`postobservation` VARCHAR(5) NULL  , 
	`postobservationnotes` VARCHAR(200) NULL  , 
	`postintervention` TINYINT(1) NULL  , 
	`postintervetionnotes` VARCHAR(200) NULL  , 
	`appointmentreasoncode` VARCHAR(5) NULL  , 
	PRIMARY KEY (`autocode`) 
);


/* Create table in target */
CREATE TABLE `ctc_hivcounsellingreason`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`code` VARCHAR(10)   NULL, 
	`description` VARCHAR(50) NULL, 
	PRIMARY KEY (`autocode`) 
) ;


/* Create table in target */
CREATE TABLE `ctc_hivcounsellingreasonlog`(
	`autocode` INT(50) NOT NULL  AUTO_INCREMENT , 
	`booking` VARCHAR(50)  NULL, 
	`patientcode` VARCHAR(50)  NULL, 
	`reasoncode` VARCHAR(50)   NULL, 
	PRIMARY KEY (`autocode`) 
) ;


/* Create table in target */
CREATE TABLE `ctc_hivcounsellorobservation`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`code` VARCHAR(10)   NULL, 
	`description` VARCHAR(100)   NULL, 
	PRIMARY KEY (`autocode`) 
) ;


/* Create table in target */
CREATE TABLE `ctc_hiveducation`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`code` VARCHAR(50)   , 
	`description` VARCHAR(50), 
	PRIMARY KEY (`autocode`) 
) ;


/* Create table in target */
CREATE TABLE `ctc_hiveducationlog`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`booking` VARCHAR(50)   , 
	`patientcode` VARCHAR(50)  NULL  , 
	`hiveducationcode` VARCHAR(50)   , 
	PRIMARY KEY (`autocode`) 
) ;


/*  Create Function */


/*  Creat view     */
DELIMITER $$
CREATE  OR REPLACE VIEW `view_billpatientbilleditems` AS SELECT 1 AS `identifier`,`billsalesitems`.`autocode` AS `autocode`,`billsalesitems`.`patientcode` AS `patientcode`,`billsalesitems`.`transdate` AS `transdate`,`billsalesitems`.`itemgroupcode` AS `itemgroupcode`,`billsalesitems`.`itemgroupdescription` AS `itemgroupdescription`,`billsalesitems`.`itemsubgroupcode` AS `itemsubgroupcode`,`billsalesitems`.`itemsubgroupdescription` AS `itemsubgroupdescription`,`billsalesitems`.`itemcode` AS `itemcode`,`billsalesitems`.`itemdescription` AS `itemdescription`,`billsalesitems`.`qty` AS `qty`,`billsalesitems`.`actualamount` AS `actualamount`,`billsalesitems`.`amount` AS `amount`,1 AS `display` FROM `billsalesitems` UNION SELECT 2 AS `identifier`,`billincomingitems`.`autocode` AS `autocode`,`billincomingitems`.`patientcode` AS `patientcode`,`billincomingitems`.`transdate` AS `transdate`,`billincomingitems`.`itemgroupcode` AS `itemgroupcode`,`billincomingitems`.`itemgroupdescription` AS `itemgroupdescription`,`billincomingitems`.`itemsubgroupcode` AS `itemsubgroupcode`,`billincomingitems`.`itemsubgroupdescription` AS `itemsubgroupdescription`,`billincomingitems`.`itemcode` AS `itemcode`,`billincomingitems`.`itemdescription` AS `itemdescription`,`billincomingitems`.`qty` AS `qty`,`billincomingitems`.`actualamount` AS `actualamount`,`billincomingitems`.`amount` AS `amount`,`billincomingitems`.`display` AS `display` FROM `billincomingitems`$$
DELIMITER ;z


DELIMITER $$
CREATE  OR REPLACE VIEW  `view_labpatienttestscount` AS SELECT `pt`.`department` AS `department`,`pt`.`transdate` AS `transdate`,`pt`.`laboratorycode` AS `laboratorycode`,`pt`.`labtechniciancode` AS `labtechniciancode`,`pt`.`labtestgroupcode` AS `labtestgroupcode`,`pt`.`labtesttypecode` AS `labtesttypecode`,SUM((CASE WHEN (`p`.`gender` = 'm') THEN 1 ELSE 0 END)) AS `males`,SUM((CASE WHEN (`p`.`gender` = 'f') THEN 1 ELSE 0 END)) AS `females` FROM (`labpatienttests` `pt` LEFT JOIN `patients` `p` ON((`pt`.`patientcode` = `p`.`code`))) GROUP BY `pt`.`department`,`pt`.`transdate`,`pt`.`laboratorycode`,`pt`.`labtechniciancode`,`pt`.`labtestgroupcode`,`pt`.`labtesttypecode`$$
DELIMITER ;


DELIMITER $$
CREATE  OR REPLACE VIEW  `view_userlogindetails` AS SELECT `ul`.`machinename` AS `machinename`,`ul`.`usercode` AS `usercode`,`ul`.`logintime` AS `logintime`,`ul`.`logouttime` AS `logouttime`,`u`.`description` AS `description`,`g`.`description` AS `usergroup` FROM ((`sys_userlogin` `ul` LEFT JOIN `sys_users` `u` ON((`u`.`CODE` = `ul`.`usercode`))) LEFT JOIN `sys_usergroups` `g` ON((`u`.`usergroupcode` = `g`.`CODE`)))$$
DELIMITER ;

