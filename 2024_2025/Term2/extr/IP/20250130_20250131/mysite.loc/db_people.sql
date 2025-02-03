-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Фев 03 2025 г., 19:57
-- Версия сервера: 5.5.25
-- Версия PHP: 5.2.12

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `db_people`
--

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` tinytext NOT NULL,
  `Workstate` int(11) NOT NULL,
  `Languages` tinytext NOT NULL,
  `Programming` tinytext NOT NULL,
  `City` int(11) NOT NULL,
  `Contacts` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=8 ;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`ID`, `Username`, `Workstate`, `Languages`, `Programming`, `City`, `Contacts`) VALUES
(2, 'Andrew', 1, '1,2', '2,3', 74, 0),
(3, 'Bob', 3, '1,2,3', '1,2,3', 74, 0),
(4, 'Bob', 3, '1,2,3', '1,2,3', 74, 0),
(5, 'Bob', 3, '1,2,3', '1,2,3', 74, 0),
(6, 'Bob', 3, '1,2,3', '1,2,3', 74, 0),
(7, 'Serge', 3, '1', '3', 74, 111);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
