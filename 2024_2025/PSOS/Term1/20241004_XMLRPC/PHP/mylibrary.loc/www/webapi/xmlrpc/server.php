<?php
	//Подключение библиотеки для работы с протоколом XML-RPC
	require_once("XML/RPC/Server.php");
	require_once("XML/RPC.php");
	require_once("$_SERVER[DOCUMENT_ROOT]/../db/db_init.inc.php");
	
	function greet_user($args) {
		$user_name = $args->getParam(0)->getval();
		
		return new XML_RPC_Response(
			new XML_RPC_Value("Здравствуй, $user_name !","string")
		);
	}	
	
	function log_in_user($params) {
		global $XML_RPC_erruser;
		
		$name=$params->getParam(0)->getval();
		$age=$params->getParam(1)->getval();
		
		
		if($age<18) 			
			return new XML_RPC_Response(0,$XML_RPC_erruser+1,"Ты маленький, тебе сюда нельзя");
		else 
			return new XML_RPC_Response(
				new XML_RPC_Value("Здравствуй, $name !","string")
			);		
	}
	
	function list_books(){
		global $db_link;
		
		//Запрос на выборку всех книг из каталога
		$res=mysqli_query($db_link,"SELECT * FROM books");
		
		//Объявление пустого массива
		$books=Array();
		
		//Выгрузка всех выбранных из базы данных книг в массив books
		while($book=mysqli_fetch_array($res,MYSQLI_ASSOC)) {
			$book["ID"]=(int)$book["ID"];
			$book["Year"]=(int)$book["Year"];
			$books[]=$book;
		}
		
		//Отправка полученного массива книг клиенту в формате XML-RPC
		return new XML_RPC_Response(
			XML_RPC_encode($books)
		);
	}
	
	function update_book($params) {
		global $db_link;
		
		$id = (int)$params->getParam(0)->getval();
		$author = mysqli_real_escape_string($db_link,$params->getParam(1)->getval());
		$title = mysqli_real_escape_string($db_link,$params->getParam(2)->getval());
		$year = (int)$params->getParam(3)->getval();
		
		mysqli_query($db_link,"
			UPDATE books
			SET 
				`Author`='$author',
				`Title`='$title',
				`Year`=$year
			WHERE
				ID=$id
		");
		
		file_put_contents("log.log","
			UPDATE books
			SET Author=`$author`,
				Title=`$title`,
				`Year`=`$year`
			WHERE
				ID=`$id`
		");
		
		return new XML_RPC_Response(
			new XML_RPC_Value(0,"int")
		);
	}
	
	//Конфигурация сайтового API
	$map=Array(
		"mylibrary::greet_user"=>Array(
			"function"=>"greet_user",
			"signature"=>Array(
				Array("string","string") //Формат аргументов и возвращаемого значения				
			),
			"docstring"=>"Функция, выводящая именное приветствие"
		),
		"mylibrary::log_in_user"=>Array(
			"function"=>"log_in_user",
			"signature"=>Array(
				Array("string","string","int") //Формат аргументов и возвращаемого значения				
			),
			"docstring"=>"Функция, контролирующая возраст"
		),
		"mylibrary::list_books"=>Array(
			"function"=>"list_books",
			"signature"=>Array(
				Array("array") //Формат аргументов и возвращаемого значения				
			),
			"docstring"=>"Функция, возвращающая список книг"
		),
		"mylibrary::update_book"=>Array(
			"function"=>"update_book",
			"signature"=>Array(
				Array("int","int","string","string","int") //Формат аргументов и возвращаемого значения				
			),
			"docstring"=>"Функция, возвращающая список книг"
		)
	);
	
	//Запуск XML-RPC - сервиса
	$srv=new XML_RPC_Server($map,1,0);
