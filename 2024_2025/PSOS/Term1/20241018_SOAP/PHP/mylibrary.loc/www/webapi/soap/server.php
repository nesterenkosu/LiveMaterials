<?php
	//Определение типа отдаваемого содержимого
	header("Content-type: text/xml;charset=utf-8");
	
	//Предотвращение кэширования страницы
	header("Cache-Control: no-store, no-cache");
	header("Expires: ".date("r"));
	
	//Предотвращение кэширования WSDL-файла
	ini_set("soap.wsdl_cache_enabled","0");
	
	
	class myservice {
		public function GreetUser($params) {
			$user_name = $params->user_name;
			
			return Array(
				"answer" => "Здравствуй, $user_name !"
			);
		}
	}
	
	$server=new SoapServer("http://$_SERVER[HTTP_HOST]/webapi/soap/myservice.wsdl.php");
	
	$server->setClass("myservice");
	
	$server->handle();