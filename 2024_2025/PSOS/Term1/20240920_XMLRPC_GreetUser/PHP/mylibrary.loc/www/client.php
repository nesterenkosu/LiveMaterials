<?php
//Подключение библиотеки для работы с XML-RPC
require_once("XML/RPC.php");

$resp="";

if(isset($_POST["btn_go"])) {
	//Если нажата кнопка
		
	$client=new XML_RPC_Client("/webapi/xmlrpc/server.ashx","http://localhost:44301");
	
	$resp=$client->send(
		new XML_RPC_Message("mylibraryaspnet::greet_user",
			Array(
				new XML_RPC_Value($_POST["username"],"string")
			)
		)
	)->value()->scalarval();	
	
}
?>
<!DOCTYPE html>
<html>
	<head>
		<link rel="stylesheet" href="style.css"/>
		<script src="jquery.js"></script>
		<script></script>
	</head>
	<body>
		<form action="" method="POST">
			Введите ваше имя: <br/>
			<input name="username" type="text" size="50"/><br/>
			<input name="btn_go" type="submit" value="Нажмите сюда"/><br/>
			<?=$resp?>
		</form>
	</body>
</html>
