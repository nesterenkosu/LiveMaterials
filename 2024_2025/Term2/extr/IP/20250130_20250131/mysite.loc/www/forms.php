<?php
	$db_link=mysqli_connect("localhost","root","");
	
	if(!$db_link)
		die("Невозможно подключиться к СУБД");
		
	$res=mysqli_select_db($db_link,"db_people");
	
	if(!$res)
		die("Невозможно выбрать базу данных db_users");
	
	//Если нажата кнопка "Сохранить"
	if($_POST["btn_go"]) {
		//Получение значений, введённых в поля формы
		$username = mysqli_real_escape_string($db_link,$_POST["tb_username"]);
		$workstate = (int)$_POST["sel_workstate"];
		$languages = $_POST["sel_languages"];
		$programming= $_POST["cb_programming"];
		$city = (int)$_POST["rb_city"];
		$contacts = mysqli_real_escape_string($db_link,$_POST["t_contacts"]);
		
		/*echo "<xmp>";
		print_r($languages);
		echo "</xmp>";*/
		
		$errmsg="";
		$err_selectors=Array();
		
		if(trim($username)=="") {
			$errmsg.="Не заполнено поле [Имя пользователя] PHP<br/>";
			$err_selectors[]="#tb_username";			
		}
		
		if($workstate<0){
			$errmsg.="Не заполнено поле [Занятость]<br/>";
			$err_selectors[]="#sel_workstate";			
		}
		
						
		if(count($languages)==0) {
			$errmsg.="Не заполнено поле [Знание иностранных языков]<br/>";
			$err_selectors[]="#sel_languages";			
		}
		
		
		
		if(trim($errmsg)=="") {
			mysqli_query($db_link,"
				INSERT INTO users
				(Username,Workstate,Languages,Programming,City,Contacts) 
				VALUES(
					'$username',
					'$workstate',
					'".implode($languages,",")."',
					'".implode($programming,",")."',
					'$city',
					'$contacts'
				)
			");
			
			header("Location: $_SERVER[PHP_SELF]");
		}
	}
	
	if(count($languages)==0)
			$languages=Array();
?>
<!DOCTYPE html>
<html>
	<head>
		<link rel="stylesheet" href="style.css"/>
		<script src="jquery.js"></script>
		<!--script>
			$(function(){
				$("#btn_go").click(function(){
					$("input,select,textarea").css("border-color", "");
					
					//Получение значений, введённых в поля формы
					let username = $("#tb_username").val();
					let workstate = $("#sel_workstate").val();
					let languages = $("#sel_languages option:selected");
					let programming=$(".cb_programming:checked");
					let city = $(".rb_city:checked").val();
					let contacts = $("#t_contacts").val();
					
					let errmsg="";
					
					if(username.trim()=="") {
						errmsg+="Не заполнено поле [Имя пользователя]<br/>";
						$("#tb_username").css("border-color", "red");
					}
					
					if(workstate<0){
						errmsg+="Не заполнено поле [Занятость]<br/>";
						$("#sel_workstate").css("border-color", "red");
					}
					
									
					if(languages.length==0) {
						errmsg+="Не заполнено поле [Знание иностранных языков]<br/>";
						$("#sel_languages").css("border-color", "red");
					}
					
					$("div#errmsg").html(errmsg);
					
						
					
					/*languages.each(function(idx,item){
							//alert(item);
							alert($(item).val());
					});*/
						
					/*programming.each(function(idx,item){
							//alert(item);
							alert($(item).val());
					});*/
					
					//alert(workstate);
				});
			});
		</script-->
		<style>
		<?=implode($err_selectors,",")?>{
			border-color: red
		}
		</style>
	</head>
	<body>
		<form action="" method="POST">
			<div id="errmsg"><?=$errmsg?></div>
			Ваше имя:<br/>
			<input id="tb_username" name="tb_username" type="text" size="20" value="<?=$username?>"/><br/>
			Ваша занятость: <br/>
			<select id="sel_workstate" name="sel_workstate">
				<option value="-1">--Выберите тип занятости--</option>
				<option value="1" <?=($workstate==1)?"selected":""?>>Школьник</option>
				<option value="2" <?=($workstate==2)?"selected":""?>>Студент</option>
				<option value="3" <?=($workstate==3)?"selected":""?>>Работающий</option>
			</select><br/>
			Знание иностранных языков:<br/>
			<select id="sel_languages" name="sel_languages[]" multiple>
				<option value="1" <?=in_array(1,$languages)?"selected":""?>>Английский</option>
				<option value="2" <?=in_array(2,$languages)?"selected":""?>>Французский</option>
				<option value="3" <?=in_array(3,$languages)?"selected":""?>>Немецкий</option>
			</select><br/>
			Знание языков программирования<br/>
			<input class="cb_programming" name="cb_programming[1]" id="i1" type="checkbox" value="1"/><label for="i1">Pascal</label><br/>
			<input class="cb_programming" name="cb_programming[2]" id="i2" type="checkbox" value="2"/><label for="i2">Basic</label><br/>
			<input class="cb_programming" name="cb_programming[3]" type="checkbox" value="3"/>C<br/>
			Город: <br/>
			<input class="rb_city" name="rb_city" type="radio" value="74"/>Челябинск<br/>
			<input class="rb_city" name="rb_city" type="radio" value="10"/>Копейск<br/>
			<input class="rb_city" name="rb_city" type="radio" value="12"/>Миасс<br/>
			Контактная информация: <br/>
			<textarea id="t_contacts" name="t_contacts" rows="5" cols="70"></textarea><br/>
			<input id="btn_go" name="btn_go" type="submit" value="Сохранить"/><br/>
		</form>
	</body>
</html>
