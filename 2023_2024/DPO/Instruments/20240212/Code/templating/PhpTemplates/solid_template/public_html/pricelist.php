<? function get_head() {?>
<?} function get_content() { global $db_link;?>
<h2>Стоимость обучения</h2>
<?$res=mysql_query("SELECT * FROM Prices",$db_link);?>
<table border="1" cellpadding="7">
	<tr>
		<td>Изучаемый язык</td>
		<td>Стоимость за ак.час</td>
	<tr>
	<tr>
		<td>Английский</td>
		<td>200р</td>
	<tr>
	<tr>
		<td>Французский</td>
		<td>300р</td>
	<tr>
	<tr>
		<td>Немецкий</td>
		<td>100р</td>
	<tr>
</table>
<?}require_once("$_SERVER[DOCUMENT_ROOT]/../templates/template.inc.php");?>