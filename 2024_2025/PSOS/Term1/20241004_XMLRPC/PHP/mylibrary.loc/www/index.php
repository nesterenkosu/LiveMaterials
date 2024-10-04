<?php require_once("$_SERVER[DOCUMENT_ROOT]/../db/db_init.inc.php");?>
<!DOCTYPE html>
<html lang="en">
<head>
	<title>Библиотека им. А.С. Пушкина</title>
	 <!-- add bootstrap css file -->

      <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
      <link rel="stylesheet" type="text/css" href="css/main.css">

</head>
<body>
  <!-- navbar -->

  <nav class="navbar navbar-expand-lg fixed-top ">
	  <a class="navbar-brand bi-house" href="#">^</a>
	  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
	    <span class="navbar-toggler-icon"></span>
	  </button>

	  <div class="collapse navbar-collapse " id="navbarSupportedContent">
	    <ul class="navbar-nav mr-4">
	      
	      <li class="nav-item">
	        <a class="nav-link" data-value="about" href="#">Главная</a>
	      </li>
	      <li class="nav-item">
	        <a class="nav-link " data-value="portfolio" href="#">Всё о библиотеке</a>
	      </li>
		  <li class="nav-item">
	        <a class="nav-link " data-value="team" href="#">
	        Читателям</a>
	      </li>
	      <li class="nav-item">
	        <a class="nav-link " data-value="blog" href="#">Каталог</a>
	      </li>
	      
	      <li class="nav-item">
	        <a class="nav-link " data-value="contact" href="#">Контакты</a>
	      </li>
	    </ul>
	    
	  </div>
</nav>
<!-- header -->
<header class="header ">
  <div class="overlay"></div>
   <div class="container">
   	  <div class="description ">
  	<h1>
  		Добро пожаловать на сайт библиотеки им А.С. Пушкина<br/><br/>
  		<p>
  		"Без чтения нет настоящего образования, нет и не может быть ни вкуса, ни слова, ни многосторонней шири понимания; Гёте и Шекспир равняются целому университету. Чтением человек переживает века." А.Герцен<br/>
		"Различие между ядами вещественными и умственными в том, что большинство ядов вещественных противны на вкус, яды же умственные, в виде... дурных книг, к несчастью, часто привлекательны." Л.Толстой
		</p><br/>
  		<button class="btn btn-outline-secondary btn-lg">Больше информации</button>
  	</h1>
  </div>
   </div>
  
</header>


<!-- Posts section -->
<div class="blog" id="blog">
	<div class="container">
	<h1 class="text-center">Каталог</h1>
	<?$res=mysqli_query($db_link,"SELECT * FROM books");$i=1;?>
	<?while($book=mysqli_fetch_array($res,MYSQLI_BOTH)):?>
		<?if($i==1){?><div class="row"><?}?>
			<div class="col-md-4 col-lg-4 col-sm-12">
				<div class="card">
					<div class="card-img">
						<img src="images/bookcover/<?=$book["ID"]?>.jpg" class="img-fluid">
					</div>
					
					<div class="card-body">
					<p class="card-text"><?=$book["Author"]?></p>
					<h4 class="card-title"><?=$book["Title"]?></h4>
					<p class="card-text"><?=$book["Year"]?></p>
					</div>
					<div class="card-footer">
						<a href="" class="card-link">Записать в формуляр</a>
					</div>
				</div>
			</div>
		<?if($i==3){?></div><?$i=0;};$i++?>
	<?endwhile;?>	
		
			
	</div>
</div>

<!-- add Javasscript file from js file -->
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/bootstrap.min.js"></script>
<script type="text/javascript" src='js/main.js'></script>

</body>
</html>