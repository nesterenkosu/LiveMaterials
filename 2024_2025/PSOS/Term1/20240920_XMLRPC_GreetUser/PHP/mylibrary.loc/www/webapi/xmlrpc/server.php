<?php
	//Подключение библиотеки для работы с протоколом XML-RPC
	require_once("XML/RPC/Server.php");
	require_once("XML/RPC.php");
	
	//Объявление функции
	function GreetUser($params) {
		
		$user_name = $params->getParam(0)->getval();
		
		return new XML_RPC_Response(
			new XML_RPC_Value("Здравствуй, $user_name !","string")
		);
	}
	
	//Предоставление удалённого доступа к функции (настройка параметров)
	$map=Array(
		"mylibraryservice::greet_user"=>Array(
				"function"=>"GreetUser",
				"signature"=>Array(
					Array("string","string")	//	Array("тип_возвр_знач","тип_арг1","тип_арг2","тип_аргN")		
				),
				"docstring"=>"Вывод именного приветствия"
			)
		);
	
	//Запуск XML-RPC-сервера
	$srv=new XML_RPC_Server($map,1,0);