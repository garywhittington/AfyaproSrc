
/* Alter table in target */
ALTER TABLE `ctc_patients` 
	ADD COLUMN `motherclientno` VARCHAR(50)  COLLATE latin1_swedish_ci NULL AFTER `supportercommunity`, COMMENT='';

/* Create table in target */
CREATE TABLE `ctc_pcrtestreasons`(
	`autocode` INT(11) NOT NULL  AUTO_INCREMENT , 
	`code` VARCHAR(50) COLLATE latin1_swedish_ci NULL  , 
	`description` VARCHAR(255) COLLATE latin1_swedish_ci NULL  , 
	`active` INT(11) NULL  DEFAULT '1' , 
	PRIMARY KEY (`autocode`) 
) ENGINE=INNODB DEFAULT CHARSET='latin1';


/* Alter table in target */
ALTER TABLE `ctc_pcrtests` 
	ADD COLUMN `cptgiven` INT(11)   NULL DEFAULT '0' AFTER `userid`, 
	ADD COLUMN `testresultrcvddate` DATETIME   NULL AFTER `cptgiven`, COMMENT='';



/*  Create Function in target  */

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `func_age_month`( targetdate DATETIME,  birthdate DATETIME) RETURNS INT(11)
BEGIN
	-- Function logic here
	DECLARE age_month INTEGER DEFAULT 0;
	SET age_month = (DATEDIFF(targetdate,birthdate)/365.25 MOD 1) * 12 - (DATEDIFF(targetdate,birthdate)/365.25 MOD 1) * 12 MOD 1;
	
	RETURN age_month;
    END$$
DELIMITER ;


/*  Create Function in target  */

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `func_age_year`( targetdate DATETIME,  birthdate DATETIME) RETURNS INT(11)
BEGIN
	-- Function logic here
	DECLARE age_year INTEGER DEFAULT 0;
	SET age_year = DATEDIFF(targetdate,birthdate)/365.25 -  DATEDIFF(targetdate,birthdate)/365.25 MOD 1;
	
	RETURN age_year;
    END$$
DELIMITER ;


/*  Create Function in target  */

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `func_cohort`(transdate DATETIME,cohort INT) RETURNS DATE
BEGIN
	 DECLARE start_date DATE;
	 DECLARE cohort_month INTEGER DEFAULT 0;
	
	 SET cohort_month = CASE  
	 WHEN cohort = 2 THEN   3
	 WHEN cohort = 12 THEN  14
	 WHEN cohort = 24 THEN   26
	 END; 
	
	 SET start_date = DATE_SUB(transdate,INTERVAL cohort_month MONTH);
	
	RETURN start_date;
    END$$
DELIMITER ;


/*  Create Function in target  */

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `func_cohort_start`(transdate DATETIME,month_interval INT) RETURNS DATE
BEGIN
	 DECLARE start_date DATE;
	 SET start_date = DATE_SUB(DATE_SUB(transdate,INTERVAL DAY(transdate)-1 DAY), INTERVAL month_interval MONTH);
	
	RETURN start_date;
    END$$
DELIMITER ;

/*  Create View in target  */
DELIMITER $$
CREATE  VIEW `view_ctc_dnapcrtests_summary` AS SELECT YEAR(`cp`.`transdate`) AS `trans_year`,MONTH(`cp`.`transdate`) AS `trans_month`,MONTHNAME(`cp`.`transdate`) AS `trans_month_description`,COUNT(`cp`.`sysdate`) AS `no_of_sample_collected`,SUM((CASE WHEN ((`cp`.`testresult` <> 0) OR (`cp`.`testresultrcvddate` IS NOT NULL)) THEN 1 ELSE 0 END)) AS `no_of_results_returned_from_lab`,SUM((CASE WHEN (`cp`.`testresultdate` IS NOT NULL) THEN 1 ELSE 0 END)) AS `no_of_results_given`,SUM((CASE WHEN (`cp`.`testresult` = 1) THEN 1 ELSE 0 END)) AS `total_test_positive`,SUM((CASE WHEN (`cp`.`testresult` = 2) THEN 1 ELSE 0 END)) AS `total_test_negative`,SUM((CASE WHEN (`cp`.`testresult` = 3) THEN 1 ELSE 0 END)) AS `total_invalid`,COUNT(`hr`.`referalcode`) AS `linked_to_hiv`,COUNT(DISTINCT `cptm`.`hivno`) AS `mothers_tested_positive`,COUNT(DISTINCT `cptm`.`ctcno`) AS `mothers_on_art`,COUNT(DISTINCT `cptm`.`patientcode`) AS `mothers`,(COUNT(DISTINCT `art`.`patientcode`) + COUNT(DISTINCT `part`.`patientcode`)) AS `mothers_on_cpt`,COUNT(DISTINCT `cht`.`patientcode`) AS `no_patient_rapid_test` FROM (((((((`ctc_pcrtests` `cp` LEFT JOIN `patients` `p` ON((`cp`.`patientcode` = `p`.`code`))) LEFT JOIN `ctc_patients` `cpt` ON((`cp`.`patientcode` = `cpt`.`patientcode`))) LEFT JOIN `ctc_hivtestreferals` `hr` ON(((`hr`.`booking` = `cp`.`booking`) AND (`hr`.`referalcode` = 'ART')))) LEFT JOIN `ctc_patients` `cptm` ON((`cpt`.`motherclientno` = `cptm`.`patientcode`))) LEFT JOIN `ctc_hivtests` `cht` ON((`cp`.`patientcode` = `cht`.`patientcode`))) LEFT JOIN `ctc_artlog` `art` ON(((`cpt`.`motherclientno` = `art`.`patientcode`) AND (`art`.`cpttablets` > 0)))) LEFT JOIN `ctc_preartlog` `part` ON(((`cpt`.`motherclientno` = `part`.`patientcode`) AND (`part`.`cpttablets` > 0)))) GROUP BY YEAR(`cp`.`transdate`),MONTH(`cp`.`transdate`)$$
DELIMITER ;

/*  Create View in target  */

DELIMITER $$
CREATE  VIEW `view_ctc_exposedchildren_summary` AS SELECT YEAR(`exl`.`transdate`) AS `trans_year`,MONTH(`exl`.`transdate`) AS `trans_month`,MONTHNAME(`exl`.`transdate`) AS `trans_month_description`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`hivinfectioncode` = 'A')) THEN 1 ELSE 0 END)) AS `hivinfection_Not_Infected_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`hivinfectioncode` = 'B')) THEN 1 ELSE 0 END)) AS `hivinfection_Infected_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`hivinfectioncode` = 'C')) THEN 1 ELSE 0 END)) AS `hivinfection_Not_ART_Eligible_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`hivinfectioncode` = 'D')) THEN 1 ELSE 0 END)) AS `hivinfection_PSHD_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND ((`exl`.`cptmills` <> 0) OR (`exl`.`cpttablets` <> 0))) THEN 1 ELSE 0 END)) AS `cpt_given_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`cptmills` = 0) AND (`exl`.`cpttablets` = 0)) THEN 1 ELSE 0 END)) AS `no_cpt_given_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`outcomecode` = 'TO')) THEN 1 ELSE 0 END)) AS `transfer_out_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`outcomecode` = 'Con')) THEN 1 ELSE 0 END)) AS `continue_follow_up_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`outcomecode` = 'ART')) THEN 1 ELSE 0 END)) AS `started_ART_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`outcomecode` = 'Def')) THEN 1 ELSE 0 END)) AS `defaulted_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`outcomecode` = 'D')) THEN 1 ELSE 0 END)) AS `died_before_ART_2_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,2))) AND (`exl`.`outcomecode` = 'Dis')) THEN 1 ELSE 0 END)) AS `discharged_uninfected_2_month`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`hivinfectioncode` = 'A')) THEN 1 ELSE 0 END)) AS `hivinfection_Not_Infected_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`hivinfectioncode` = 'B')) THEN 1 ELSE 0 END)) AS `hivinfection_Infected_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`hivinfectioncode` = 'C')) THEN 1 ELSE 0 END)) AS `hivinfection_Not_ART_Eligible_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`hivinfectioncode` = 'D')) THEN 1 ELSE 0 END)) AS `hivinfection_PSHD_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND ((`exl`.`cptmills` <> 0) OR (`exl`.`cpttablets` <> 0))) THEN 1 ELSE 0 END)) AS `cpt_given_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`cptmills` = 0) AND (`exl`.`cpttablets` = 0)) THEN 1 ELSE 0 END)) AS `no_cpt_given_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`outcomecode` = 'TO')) THEN 1 ELSE 0 END)) AS `transfer_out_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`outcomecode` = 'Con')) THEN 1 ELSE 0 END)) AS `continue_follow_up_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`outcomecode` = 'ART')) THEN 1 ELSE 0 END)) AS `started_ART_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`outcomecode` = 'Def')) THEN 1 ELSE 0 END)) AS `defaulted_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`outcomecode` = 'D')) THEN 1 ELSE 0 END)) AS `died_before_ART_12_months`,SUM((CASE WHEN ((CAST(`p`.`birthdate` AS DATE) > LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,12))) AND (`exl`.`outcomecode` = 'Dis')) THEN 1 ELSE 0 END)) AS `discharged_uninfected_12_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`hivinfectioncode` = 'A')) THEN 1 ELSE 0 END)) AS `hivinfection_Not_Infected_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`hivinfectioncode` = 'B')) THEN 1 ELSE 0 END)) AS `hivinfection_Infected_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`hivinfectioncode` = 'C')) THEN 1 ELSE 0 END)) AS `hivinfection_Not_ART_Eligible_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`hivinfectioncode` = 'D')) THEN 1 ELSE 0 END)) AS `hivinfection_PSHD_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND ((`exl`.`cptmills` <> 0) OR (`exl`.`cpttablets` <> 0))) THEN 1 ELSE 0 END)) AS `cpt_given_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`cptmills` = 0) AND (`exl`.`cpttablets` = 0)) THEN 1 ELSE 0 END)) AS `no_cpt_given_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`outcomecode` = 'TO')) THEN 1 ELSE 0 END)) AS `transfer_out_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`outcomecode` = 'Con')) THEN 1 ELSE 0 END)) AS `continue_follow_up_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`outcomecode` = 'ART')) THEN 1 ELSE 0 END)) AS `started_ART_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`outcomecode` = 'Def')) THEN 1 ELSE 0 END)) AS `defaulted_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`outcomecode` = 'D')) THEN 1 ELSE 0 END)) AS `died_before_ART_24_months`,SUM((CASE WHEN ((`p`.`birthdate` <= LAST_DAY(`func_cohort`(`exl`.`transdate`,24))) AND (`exl`.`outcomecode` = 'Dis')) THEN 1 ELSE 0 END)) AS `discharged_uninfected_24_months` FROM (`ctc_exposedlog` `exl` LEFT JOIN `patients` `p` ON((`exl`.`patientcode` = `p`.`code`))) GROUP BY YEAR(`exl`.`transdate`),MONTH(`exl`.`transdate`)$$
DELIMITER ;

/*  Create View in target  */

DELIMITER $$
CREATE  VIEW `view_ctc_hivtests_summary` AS SELECT YEAR(`ch`.`transdate`) AS `trans_year`,MONTH(`ch`.`transdate`) AS `trans_month`,MONTHNAME(`ch`.`transdate`) AS `trans_month_description`,SUM((CASE WHEN (`p`.`gender` = 'M') THEN 1 ELSE 0 END)) AS `males`,SUM((CASE WHEN ((`p`.`gender` = 'F') AND (`ch`.`pregnant` = 1)) THEN 1 ELSE 0 END)) AS `pregnant`,SUM((CASE WHEN ((`p`.`gender` = 'F') AND (`ch`.`pregnant` = 0)) THEN 1 ELSE 0 END)) AS `non_pregnant`,SUM((CASE WHEN (`ch`.`testresultgiven` = 1) THEN 1 ELSE 0 END)) AS `result_given_positive`,SUM((CASE WHEN (`ch`.`testresultgiven` = 2) THEN 1 ELSE 0 END)) AS `result_given_negative`,SUM((CASE WHEN (`ch`.`testresultgiven` = 3) THEN 1 ELSE 0 END)) AS `result_given_exposed`,SUM((CASE WHEN ((`ch`.`testresultgiven` = 4) OR (`ch`.`testresultgiven` = 0)) THEN 1 ELSE 0 END)) AS `result_given_inconclusive`,SUM((CASE WHEN (`ch`.`evertested` = 1) THEN 1 ELSE 0 END)) AS `ever_tested`,SUM((CASE WHEN (`ch`.`withpartner` = 1) THEN 1 ELSE 0 END)) AS `tested_with_partner`,SUM((CASE WHEN (`ch`.`testresult1` = 2) THEN 1 ELSE 0 END)) AS `result_single_negative`,SUM((CASE WHEN ((`ch`.`testresult1` = 1) AND (`ch`.`testresult2` = 1)) THEN 1 ELSE 0 END)) AS `result_t1_and_t2_pos`,SUM((CASE WHEN (((`ch`.`testresult1` = 1) AND ((`ch`.`testresult2` = 2) OR (`ch`.`testresult2` = 0))) OR (`ch`.`testresult1` = 0)) THEN 1 ELSE 0 END)) AS `result_t1_and_t2_dis`,SUM((CASE WHEN ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) > 24) AND (`p`.`gender` = 'M')) THEN 1 ELSE 0 END)) AS `age_group_dm`,SUM((CASE WHEN ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) > 24) AND (`p`.`gender` = 'F')) THEN 1 ELSE 0 END)) AS `age_group_df`,SUM((CASE WHEN ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) > 14) AND (`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) <= 24) AND (`p`.`gender` = 'M')) THEN 1 ELSE 0 END)) AS `age_group_cm`,SUM((CASE WHEN ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) > 14) AND (`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) <= 24) AND (`p`.`gender` = 'F')) THEN 1 ELSE 0 END)) AS `age_group_cf`,SUM((CASE WHEN (((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) > 1) OR ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) = 1) AND (`func_age_month`(`ch`.`transdate`,`p`.`birthdate`) > 5))) AND (`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) <= 14) AND (`p`.`gender` = 'M')) THEN 1 ELSE 0 END)) AS `age_group_bm`,SUM((CASE WHEN (((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) > 1) OR ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) = 1) AND (`func_age_month`(`ch`.`transdate`,`p`.`birthdate`) > 5))) AND (`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) <= 14) AND (`p`.`gender` = 'F')) THEN 1 ELSE 0 END)) AS `age_group_bf`,SUM((CASE WHEN (((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) = 0) OR ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) = 1) AND (`func_age_month`(`ch`.`transdate`,`p`.`birthdate`) <= 5))) AND (`p`.`gender` = 'M')) THEN 1 ELSE 0 END)) AS `age_group_am`,SUM((CASE WHEN (((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) = 0) OR ((`func_age_year`(`ch`.`transdate`,`p`.`birthdate`) = 1) AND (`func_age_month`(`ch`.`transdate`,`p`.`birthdate`) <= 5))) AND (`p`.`gender` = 'F')) THEN 1 ELSE 0 END)) AS `age_group_af`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '1') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `PMTCT`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '2') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `HBC`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '3') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `PLHIV_SUPPORT_GROUP`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '4') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `ORPHAN_AND_VULNERABLE_CHILDREN_GROUP`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '5') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `MEDICAL_SPECIALITY`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '6') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `NUTRITIONAL_SUPPORT`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = '7') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `LEGAL`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = 'TB') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `TB_CLINIC`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = 'FP') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `FP_SERVICES`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = 'ART') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `ART`,SUM((SELECT SUM((CASE WHEN (`chr`.`referalcode` = 'OTH') THEN 1 ELSE 0 END)) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `OTHER`,SUM((SELECT COUNT(DISTINCT `chr`.`booking`) FROM `ctc_hivtestreferals` `chr` WHERE ((`ch`.`patientcode` = `chr`.`patientcode`) AND (`chr`.`booking` = `ch`.`booking`)))) AS `total_clients_referred` FROM (`ctc_hivtests` `ch` LEFT JOIN `patients` `p` ON((`ch`.`patientcode` = `p`.`code`))) GROUP BY YEAR(`ch`.`transdate`),MONTH(`ch`.`transdate`)$$
DELIMITER ;


/*  Alter View in target  */
DELIMITER $$
DROP VIEW IF EXISTS `view_ctc_patients`$$

CREATE  VIEW `view_ctc_patients` AS SELECT `p`.`autocode` AS `autocode`,`p`.`patientcode` AS `patientcode`,`p`.`hivno` AS `hivno`,`p`.`hivtestno` AS `hivtestno`,`p`.`referredfromcode` AS `referredfromcode`,(CASE WHEN (`p`.`referredfromcode` = 'oth') THEN `p`.`referredfromother` ELSE `rf`.`description` END) AS `referredfromdescription`,`p`.`referredfromother` AS `referredfromother`,`p`.`enrolledincaredate` AS `enrolledincaredate`,`p`.`medeligibledate` AS `medeligibledate`,`p`.`eligibleandreadydate` AS `eligibleandreadydate`,`p`.`whyeligiblecode` AS `whyeligiblecode`,`we`.`description` AS `whyeligibledescription`,`p`.`whyeligiblecd4` AS `whyeligiblecd4`,`p`.`whyeligibletlc` AS `whyeligibletlc`,`p`.`supportersurname` AS `supportersurname`,`p`.`supporterfirstname` AS `supporterfirstname`,`p`.`supporterothernames` AS `supporterothernames`,`p`.`supporteraddress` AS `supporteraddress`,`p`.`supportertelephone` AS `supportertelephone`,`p`.`supportercommunity` AS `supportercommunity`,`p`.`htcno` AS `htcno`,`p`.`ctcno` AS `ctcno`,`p`.`arvno` AS `arvno`,`p`.`priorarvexposurecode` AS `priorarvexposurecode`,`p`.`guardianname` AS `guardianname`,`p`.`patientphone` AS `patientphone`,`p`.`guardianphone` AS `guardianphone`,`p`.`guardianrelation` AS `guardianrelation`,`p`.`agreetofup` AS `agreetofup`,`p`.`motherclientno` AS `motherclientno` FROM ((`ctc_patients` `p` LEFT JOIN `ctc_referedfrom` `rf` ON((`p`.`referredfromcode` = `rf`.`code`))) LEFT JOIN `ctc_artwhyeligible` `we` ON((`p`.`whyeligiblecode` = `we`.`code`)))$$
DELIMITER ;


/*  Alter View in target  */
DELIMITER $$
DROP VIEW IF EXISTS `view_ctc_pcrtests`$$

CREATE  VIEW `view_ctc_pcrtests` AS SELECT `p`.`autocode` AS `autocode`,`p`.`sysdate` AS `sysdate`,`p`.`transdate` AS `transdate`,`p`.`patientcode` AS `patientcode`,`p`.`booking` AS `booking`,`p`.`sampledate` AS `sampledate`,`p`.`reasoncode` AS `reasoncode`,`tr`.`description` AS `reasondescription`,`p`.`testresult` AS `testresult`,(CASE `p`.`testresult` WHEN 0 THEN 'Unknown' WHEN 1 THEN 'Positive' WHEN 2 THEN 'Negative' WHEN 3 THEN 'Exposed Infant' WHEN 4 THEN 'Inconclusive' END) AS `testresultdescription`,`p`.`testresultdate` AS `testresultdate`,`p`.`breastfeeding` AS `breastfeeding`,(CASE `p`.`breastfeeding` WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) AS `breastfeedingdescription`,`p`.`comments` AS `comments`,`p`.`counsellorcode` AS `counsellorcode`,`s`.`description` AS `counsellordescription`,`a`.`apptdate` AS `nextapptdate`,`p`.`userid` AS `userid`,`p`.`testresultrcvddate` AS `testresultrcvddate`,`p`.`cptgiven` AS `cptgiven`,(CASE `p`.`cptgiven` WHEN 0 THEN 'No' ELSE 'Yes' END) AS `cptgivendescription`,`pt`.`motherclientno` AS `motherclientno` FROM ((((`ctc_pcrtests` `p` LEFT JOIN `facilitystaffs` `s` ON((`p`.`counsellorcode` = `s`.`CODE`))) LEFT JOIN `ctc_hivtestreasons` `tr` ON((`p`.`reasoncode` = `tr`.`code`))) LEFT JOIN `ctc_patients` `pt` ON((`p`.`patientcode` = `pt`.`patientcode`))) LEFT JOIN `ctc_appointments` `a` ON(((`p`.`booking` = `a`.`booking`) AND (`p`.`patientcode` = `a`.`patientcode`) AND (`a`.`appttype` = 0))))$$
DELIMITER ;
