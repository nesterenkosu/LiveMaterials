<?php
	//Предотвращение кэширования страницы
	header("Cache-control: no-store, no-cache");
	header("Expires: ".date("r"));
	
	//Предотвращение кэширования WSDL-файла
	ini_set("soap.wsdl_cache_enabled","0");
	
	//Настройка информирования об ошибках
	ini_set("display_errors","1");
	error_reporting(E_ALL & ~E_NOTICE);
	
	$greeting="";
	
	//Если нажата кнопка
	if(isset($_POST["btn_Go"])) {
		
		//Создание объекта SOAP-клиента
		$client=new SoapClient(
			"http://localhost:55573/Service1.svc?singleWsdl",
			array("soap_version"=>SOAP_1_1)
		);
	
	
		//Вызов удалённой процедуры GreetUser
		$greeting = $client->GreetUser(
			Array("user_name"=>$_POST["tb_name"])
		)->GreetUserResult;
	}
	
	
?>
<!DOCTYPE html>
<html>
	<head></head>
	<body>
		<form action="" method="POST">
			<input name="tb_name" type="text" size="20"/> 
			<input name="btn_Go" type="submit" value="Ввод"/>
			<?=$greeting?>
		</form>
	</body>
</html>
	
