<?php
	//Попытка установки соединения с MySQL-сервером
	$db_link=mysqli_connect("localhost","root","");
	//В случае ошибки вывести сообщение
	//и завершить программу
	if(!$db_link)
		die("Ошибка соединения с базой данных");
	//Попытка выбора базы данных
	//В случае ошибки вывести сообщение
	//и завершить программу
	if(!mysqli_select_db($db_link,"library"))
		die("Ошибка выбора базы данных");
	
	//Чтобы не было проблем с кириллицей
	mysqli_query(
		$db_link,
		"SET NAMES utf8 COLLATE utf8_general_ci"
	);
	