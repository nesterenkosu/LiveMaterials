-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Окт 04 2024 г., 20:03
-- Версия сервера: 5.5.25
-- Версия PHP: 5.2.12

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `library`
--
CREATE DATABASE `library` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `library`;

-- --------------------------------------------------------

--
-- Структура таблицы `books`
--

CREATE TABLE IF NOT EXISTS `books` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Author` tinytext NOT NULL,
  `Title` tinytext NOT NULL,
  `Year` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=9 ;

--
-- Дамп данных таблицы `books`
--

INSERT INTO `books` (`ID`, `Author`, `Title`, `Year`) VALUES
(1, 'А.С.Пушкин', 'Евгений Онегин', 1964),
(2, 'Н.В. Гоголь', 'Мёртвые души', 1985),
(3, 'Ф.М. Достоевский', 'Преступление и наказание', 1992),
(4, 'А.С. Пушкин', 'Дубровский', 1970),
(5, 'Л.Н.Толстой', 'Война и мир', 1970),
(6, 'Ф.М. Достоевский', 'Идиот', 1966),
(7, 'И.С. Бунин', 'Тёмные аллеи', 1980),
(8, 'Дж, Роллинг', 'Гарри Поттер', 2007);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
