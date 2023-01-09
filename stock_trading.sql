-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 09, 2023 at 04:04 PM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `stock_trading`
--

-- --------------------------------------------------------

--
-- Table structure for table `account_type`
--

CREATE TABLE `account_type` (
  `id` int(11) NOT NULL,
  `name` varchar(32) NOT NULL,
  `tax_rate` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `account_type`
--

INSERT INTO `account_type` (`id`, `name`, `tax_rate`) VALUES
(1, 'Investeringssparkonto', 2.2),
(2, 'Aktie-Fond-Konto', 30);

-- --------------------------------------------------------

--
-- Table structure for table `active_orders`
--

CREATE TABLE `active_orders` (
  `id` int(11) NOT NULL,
  `stock_id` int(11) NOT NULL,
  `account_id` int(11) NOT NULL,
  `price_per_stock` double NOT NULL,
  `amount` int(11) NOT NULL,
  `is_buy_order` tinyint(1) NOT NULL,
  `order_time_stamp` datetime NOT NULL,
  `is_active` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `active_orders`
--

INSERT INTO `active_orders` (`id`, `stock_id`, `account_id`, `price_per_stock`, `amount`, `is_buy_order`, `order_time_stamp`, `is_active`) VALUES
(1, 1, 1, 5.3, 100, 0, '2023-01-01 19:19:00', 1),
(2, 2, 1, 3.4, 100, 0, '2023-01-01 19:19:00', 1),
(3, 3, 1, 9.9, 100, 0, '2023-01-01 19:19:00', 1),
(4, 4, 1, 3.8, 100, 0, '2023-01-01 19:19:00', 1),
(5, 5, 1, 6.9, 100, 0, '2023-01-01 19:19:00', 1),
(6, 6, 1, 8.7, 100, 0, '2023-01-01 19:19:00', 1),
(7, 7, 1, 6.7, 100, 0, '2023-01-01 19:19:00', 1),
(8, 8, 1, 5.7, 100, 0, '2023-01-01 19:19:00', 1),
(9, 9, 1, 7.4, 100, 0, '2023-01-01 19:19:00', 1),
(10, 10, 1, 7, 100, 0, '2023-01-01 19:19:00', 1),
(11, 11, 1, 6.1, 100, 0, '2023-01-01 19:19:00', 1),
(12, 12, 1, 5.1, 100, 0, '2023-01-01 19:19:00', 1),
(13, 13, 1, 3.8, 100, 0, '2023-01-01 19:19:00', 1),
(14, 14, 1, 5, 100, 0, '2023-01-01 19:19:00', 1),
(15, 15, 1, 4.2, 100, 0, '2023-01-01 19:19:00', 1),
(16, 16, 1, 9.5, 100, 0, '2023-01-01 19:19:00', 1),
(17, 17, 1, 9.6, 100, 0, '2023-01-01 19:19:00', 1),
(18, 18, 1, 4.8, 50, 0, '2023-01-01 19:19:00', 1),
(19, 19, 1, 9, 100, 0, '2023-01-01 19:19:00', 1),
(20, 20, 1, 7, 100, 0, '2023-01-01 19:19:00', 1),
(21, 21, 1, 9.5, 100, 0, '2023-01-01 19:19:00', 1),
(22, 22, 1, 5.5, 100, 0, '2023-01-01 19:19:00', 1),
(23, 23, 1, 5, 100, 0, '2023-01-01 19:19:00', 1),
(24, 24, 1, 2.2, 100, 0, '2023-01-01 19:19:00', 1),
(25, 25, 1, 5.7, 100, 0, '2023-01-01 19:19:00', 1),
(26, 26, 1, 2.3, 100, 0, '2023-01-01 19:19:00', 1),
(27, 27, 1, 4.7, 100, 0, '2023-01-01 19:19:00', 1),
(28, 28, 1, 4.7, 100, 0, '2023-01-01 19:19:00', 1),
(29, 29, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(30, 30, 1, 3.7, 100, 0, '2023-01-01 19:19:00', 1),
(31, 31, 1, 4.3, 100, 0, '2023-01-01 19:19:00', 1),
(32, 32, 1, 2.5, 100, 0, '2023-01-01 19:19:00', 1),
(33, 33, 1, 5.7, 100, 0, '2023-01-01 19:19:00', 1),
(34, 34, 1, 6.6, 100, 0, '2023-01-01 19:19:00', 1),
(35, 35, 1, 6.8, 100, 0, '2023-01-01 19:19:00', 1),
(36, 36, 1, 5.4, 100, 0, '2023-01-01 19:19:00', 1),
(37, 37, 1, 9.2, 50, 0, '2023-01-01 19:19:00', 1),
(38, 38, 1, 7, 100, 0, '2023-01-01 19:19:00', 1),
(39, 39, 1, 7.4, 100, 0, '2023-01-01 19:19:00', 1),
(40, 40, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(41, 41, 1, 7.3, 100, 0, '2023-01-01 19:19:00', 1),
(42, 42, 1, 5.2, 100, 0, '2023-01-01 19:19:00', 1),
(43, 43, 1, 5.6, 100, 0, '2023-01-01 19:19:00', 1),
(44, 44, 1, 8.8, 100, 0, '2023-01-01 19:19:00', 1),
(45, 45, 1, 9, 100, 0, '2023-01-01 19:19:00', 1),
(46, 46, 1, 4.6, 100, 0, '2023-01-01 19:19:00', 1),
(47, 47, 1, 8.1, 100, 0, '2023-01-01 19:19:00', 1),
(48, 48, 1, 8, 100, 0, '2023-01-01 19:19:00', 1),
(49, 49, 1, 6.9, 100, 0, '2023-01-01 19:19:00', 1),
(50, 50, 1, 7.8, 100, 0, '2023-01-01 19:19:00', 1),
(51, 51, 1, 3.1, 100, 0, '2023-01-01 19:19:00', 1),
(52, 52, 1, 4, 100, 0, '2023-01-01 19:19:00', 1),
(53, 53, 1, 5.7, 100, 0, '2023-01-01 19:19:00', 1),
(54, 54, 1, 6.3, 100, 0, '2023-01-01 19:19:00', 1),
(55, 55, 1, 8.8, 100, 0, '2023-01-01 19:19:00', 1),
(56, 56, 1, 9.3, 100, 0, '2023-01-01 19:19:00', 1),
(57, 57, 1, 3.1, 100, 0, '2023-01-01 19:19:00', 1),
(58, 58, 1, 7.8, 100, 0, '2023-01-01 19:19:00', 1),
(59, 59, 1, 6.5, 100, 0, '2023-01-01 19:19:00', 1),
(60, 60, 1, 5.3, 100, 0, '2023-01-01 19:19:00', 1),
(61, 61, 1, 8.3, 100, 0, '2023-01-01 19:19:00', 1),
(62, 62, 1, 7, 100, 0, '2023-01-01 19:19:00', 1),
(63, 63, 1, 3.8, 100, 0, '2023-01-01 19:19:00', 1),
(64, 64, 1, 2.3, 100, 0, '2023-01-01 19:19:00', 1),
(65, 65, 1, 2.8, 100, 0, '2023-01-01 19:19:00', 1),
(66, 66, 1, 9.9, 100, 0, '2023-01-01 19:19:00', 1),
(67, 67, 1, 6.3, 100, 0, '2023-01-01 19:19:00', 1),
(68, 68, 1, 6.9, 100, 0, '2023-01-01 19:19:00', 1),
(69, 69, 1, 6.6, 100, 0, '2023-01-01 19:19:00', 1),
(70, 70, 1, 7.3, 100, 0, '2023-01-01 19:19:00', 1),
(71, 71, 1, 2.9, 100, 0, '2023-01-01 19:19:00', 1),
(72, 72, 1, 8.2, 100, 0, '2023-01-01 19:19:00', 1),
(73, 73, 1, 5.1, 100, 0, '2023-01-01 19:19:00', 1),
(74, 74, 1, 10.75, 65, 0, '2023-01-01 19:19:00', 1),
(75, 75, 1, 2, 100, 0, '2023-01-01 19:19:00', 1),
(76, 76, 1, 8.2, 100, 0, '2023-01-01 19:19:00', 1),
(77, 77, 1, 4.6, 100, 0, '2023-01-01 19:19:00', 1),
(78, 78, 1, 9.7, 100, 0, '2023-01-01 19:19:00', 1),
(79, 79, 1, 6.6, 100, 0, '2023-01-01 19:19:00', 1),
(80, 80, 1, 2.4, 100, 0, '2023-01-01 19:19:00', 1),
(81, 81, 1, 7, 100, 0, '2023-01-01 19:19:00', 1),
(82, 82, 1, 3.5, 100, 0, '2023-01-01 19:19:00', 1),
(83, 83, 1, 6.5, 100, 0, '2023-01-01 19:19:00', 1),
(84, 84, 1, 3.1, 100, 0, '2023-01-01 19:19:00', 1),
(85, 85, 1, 3.4, 100, 0, '2023-01-01 19:19:00', 1),
(86, 86, 1, 9, 100, 0, '2023-01-01 19:19:00', 1),
(87, 87, 1, 5.4, 100, 0, '2023-01-01 19:19:00', 1),
(88, 88, 1, 5.8, 100, 0, '2023-01-01 19:19:00', 1),
(89, 89, 1, 5.5, 100, 0, '2023-01-01 19:19:00', 1),
(90, 90, 1, 2.3, 100, 0, '2023-01-01 19:19:00', 1),
(91, 91, 1, 2.2, 100, 0, '2023-01-01 19:19:00', 1),
(92, 92, 1, 4.2, 100, 0, '2023-01-01 19:19:00', 1),
(93, 93, 1, 9.1, 100, 0, '2023-01-01 19:19:00', 1),
(94, 94, 1, 4.8, 100, 0, '2023-01-01 19:19:00', 1),
(95, 95, 1, 3.4, 100, 0, '2023-01-01 19:19:00', 1),
(96, 96, 1, 7.3, 100, 0, '2023-01-01 19:19:00', 1),
(97, 97, 1, 3.5, 100, 0, '2023-01-01 19:19:00', 1),
(98, 98, 1, 3, 100, 0, '2023-01-01 19:19:00', 1),
(99, 99, 1, 4.6, 100, 0, '2023-01-01 19:19:00', 1),
(100, 100, 1, 8.3, 100, 0, '2023-01-01 19:19:00', 1),
(101, 101, 1, 6.3, 100, 0, '2023-01-01 19:19:00', 1),
(102, 102, 1, 6, 100, 0, '2023-01-01 19:19:00', 1),
(103, 103, 1, 9.2, 100, 0, '2023-01-01 19:19:00', 1),
(104, 104, 1, 2.9, 100, 0, '2023-01-01 19:19:00', 1),
(105, 105, 1, 7.5, 100, 0, '2023-01-01 19:19:00', 1),
(106, 106, 1, 3.9, 100, 0, '2023-01-01 19:19:00', 1),
(107, 107, 1, 8.1, 100, 0, '2023-01-01 19:19:00', 1),
(108, 108, 1, 10, 100, 0, '2023-01-01 19:19:00', 1),
(109, 109, 1, 6.2, 100, 0, '2023-01-01 19:19:00', 1),
(110, 110, 1, 6.1, 100, 0, '2023-01-01 19:19:00', 1),
(111, 111, 1, 4.9, 100, 0, '2023-01-01 19:19:00', 1),
(112, 112, 1, 8.7, 100, 0, '2023-01-01 19:19:00', 1),
(113, 113, 1, 8.9, 100, 0, '2023-01-01 19:19:00', 1),
(114, 114, 1, 5.6, 100, 0, '2023-01-01 19:19:00', 1),
(115, 115, 1, 8.7, 100, 0, '2023-01-01 19:19:00', 1),
(116, 116, 1, 2.9, 100, 0, '2023-01-01 19:19:00', 1),
(117, 117, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(118, 118, 1, 8, 100, 0, '2023-01-01 19:19:00', 1),
(119, 119, 1, 5.3, 100, 0, '2023-01-01 19:19:00', 1),
(120, 120, 1, 9.2, 100, 0, '2023-01-01 19:19:00', 1),
(121, 121, 1, 3.8, 100, 0, '2023-01-01 19:19:00', 1),
(122, 122, 1, 7.4, 100, 0, '2023-01-01 19:19:00', 1),
(123, 123, 1, 3.9, 100, 0, '2023-01-01 19:19:00', 1),
(124, 124, 1, 8.3, 100, 0, '2023-01-01 19:19:00', 1),
(125, 125, 1, 8.9, 100, 0, '2023-01-01 19:19:00', 1),
(126, 126, 1, 2.7, 100, 0, '2023-01-01 19:19:00', 1),
(127, 127, 1, 9.9, 100, 0, '2023-01-01 19:19:00', 1),
(128, 128, 1, 7.5, 100, 0, '2023-01-01 19:19:00', 1),
(129, 129, 1, 8.2, 100, 0, '2023-01-01 19:19:00', 1),
(130, 130, 1, 2.5, 100, 0, '2023-01-01 19:19:00', 1),
(131, 131, 1, 2.6, 100, 0, '2023-01-01 19:19:00', 1),
(132, 132, 1, 5.2, 100, 0, '2023-01-01 19:19:00', 1),
(133, 133, 1, 3.3, 100, 0, '2023-01-01 19:19:00', 1),
(134, 134, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(135, 135, 1, 8.4, 100, 0, '2023-01-01 19:19:00', 1),
(136, 136, 1, 2.2, 100, 0, '2023-01-01 19:19:00', 1),
(137, 137, 1, 5.7, 100, 0, '2023-01-01 19:19:00', 1),
(138, 138, 1, 6, 100, 0, '2023-01-01 19:19:00', 1),
(139, 139, 1, 9.3, 100, 0, '2023-01-01 19:19:00', 1),
(140, 140, 1, 4.2, 100, 0, '2023-01-01 19:19:00', 1),
(141, 141, 1, 7.2, 100, 0, '2023-01-01 19:19:00', 1),
(142, 142, 1, 4.9, 100, 0, '2023-01-01 19:19:00', 1),
(143, 143, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(144, 144, 1, 7.1, 100, 0, '2023-01-01 19:19:00', 1),
(145, 145, 1, 5.9, 100, 0, '2023-01-01 19:19:00', 1),
(146, 146, 1, 6.2, 100, 0, '2023-01-01 19:19:00', 1),
(147, 147, 1, 3.9, 100, 0, '2023-01-01 19:19:00', 1),
(148, 148, 1, 2.7, 100, 0, '2023-01-01 19:19:00', 1),
(149, 149, 1, 6.8, 100, 0, '2023-01-01 19:19:00', 1),
(150, 150, 1, 2.4, 100, 0, '2023-01-01 19:19:00', 1),
(151, 151, 1, 4.9, 100, 0, '2023-01-01 19:19:00', 1),
(152, 152, 1, 6.7, 100, 0, '2023-01-01 19:19:00', 1),
(153, 153, 1, 7.9, 100, 0, '2023-01-01 19:19:00', 1),
(154, 154, 1, 7.7, 100, 0, '2023-01-01 19:19:00', 1),
(155, 155, 1, 7.7, 100, 0, '2023-01-01 19:19:00', 1),
(156, 156, 1, 3, 100, 0, '2023-01-01 19:19:00', 1),
(157, 157, 1, 5.3, 100, 0, '2023-01-01 19:19:00', 1),
(158, 158, 1, 6.1, 100, 0, '2023-01-01 19:19:00', 1),
(159, 159, 1, 6.2, 100, 0, '2023-01-01 19:19:00', 1),
(160, 160, 1, 4, 100, 0, '2023-01-01 19:19:00', 1),
(161, 161, 1, 8.6, 100, 0, '2023-01-01 19:19:00', 1),
(162, 162, 1, 3.9, 100, 0, '2023-01-01 19:19:00', 1),
(163, 163, 1, 4.5, 100, 0, '2023-01-01 19:19:00', 1),
(164, 164, 1, 3.2, 100, 0, '2023-01-01 19:19:00', 1),
(165, 165, 1, 9.9, 100, 0, '2023-01-01 19:19:00', 1),
(166, 166, 1, 9.7, 100, 0, '2023-01-01 19:19:00', 1),
(167, 167, 1, 6.9, 100, 0, '2023-01-01 19:19:00', 1),
(168, 168, 1, 6.5, 100, 0, '2023-01-01 19:19:00', 1),
(169, 169, 1, 3.6, 100, 0, '2023-01-01 19:19:00', 1),
(170, 170, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(171, 171, 1, 5.9, 100, 0, '2023-01-01 19:19:00', 1),
(172, 172, 1, 2.4, 100, 0, '2023-01-01 19:19:00', 1),
(173, 173, 1, 5.1, 100, 0, '2023-01-01 19:19:00', 1),
(174, 174, 1, 7.3, 100, 0, '2023-01-01 19:19:00', 1),
(175, 175, 1, 3, 100, 0, '2023-01-01 19:19:00', 1),
(176, 176, 1, 7.8, 100, 0, '2023-01-01 19:19:00', 1),
(177, 177, 1, 4.8, 100, 0, '2023-01-01 19:19:00', 1),
(178, 178, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(179, 179, 1, 8.8, 100, 0, '2023-01-01 19:19:00', 1),
(180, 180, 1, 7.2, 100, 0, '2023-01-01 19:19:00', 1),
(181, 181, 1, 5.1, 100, 0, '2023-01-01 19:19:00', 1),
(182, 182, 1, 2.9, 100, 0, '2023-01-01 19:19:00', 1),
(183, 183, 1, 3.3, 100, 0, '2023-01-01 19:19:00', 1),
(184, 184, 1, 6.5, 100, 0, '2023-01-01 19:19:00', 1),
(185, 185, 1, 7.1, 100, 0, '2023-01-01 19:19:00', 1),
(186, 186, 1, 2, 100, 0, '2023-01-01 19:19:00', 1),
(187, 187, 1, 5.1, 100, 0, '2023-01-01 19:19:00', 1),
(188, 188, 1, 5.8, 100, 0, '2023-01-01 19:19:00', 1),
(189, 189, 1, 4.7, 100, 0, '2023-01-01 19:19:00', 1),
(190, 190, 1, 2, 100, 0, '2023-01-01 19:19:00', 1),
(191, 191, 1, 2.3, 87, 0, '2023-01-01 19:19:00', 1),
(192, 192, 1, 6.4, 100, 0, '2023-01-01 19:19:00', 1),
(193, 193, 1, 9.6, 100, 0, '2023-01-01 19:19:00', 1),
(194, 194, 1, 8.7, 100, 0, '2023-01-01 19:19:00', 1),
(195, 195, 1, 7.6, 100, 0, '2023-01-01 19:19:00', 1),
(196, 196, 1, 9.7, 100, 0, '2023-01-01 19:19:00', 1),
(197, 197, 1, 4.8, 100, 0, '2023-01-01 19:19:00', 1),
(198, 198, 1, 7.5, 100, 0, '2023-01-01 19:19:00', 1),
(199, 199, 1, 6.1, 100, 0, '2023-01-01 19:19:00', 1),
(200, 200, 1, 6.1, 100, 0, '2023-01-01 19:19:00', 1),
(201, 74, 3, 7, 25, 1, '2023-01-01 19:31:01', 0),
(202, 74, 1, 6.9, 25, 0, '2023-01-01 19:19:00', 0),
(203, 18, 3, 5, 50, 1, '2023-01-01 19:35:44', 0),
(204, 18, 1, 4.8, 50, 0, '2023-01-01 19:19:00', 0),
(205, 191, 3, 5, 13, 1, '2023-01-01 19:38:20', 0),
(206, 191, 1, 2.3, 13, 0, '2023-01-01 19:19:00', 0),
(207, 74, 3, 11, 10, 1, '2023-01-01 19:49:02', 0),
(208, 74, 1, 10.75, 10, 0, '2023-01-01 19:19:00', 0),
(209, 191, 3, 2, 10, 1, '2023-01-02 12:39:34', 1),
(210, 22, 3, 2, 10, 1, '2023-01-02 13:50:45', 1),
(212, 7, 1, 6, 5, 0, '2023-01-03 17:19:17', 1),
(213, 7, 3, 6, 25, 1, '2023-01-03 17:20:36', 0),
(214, 7, 1, 6, 25, 0, '2023-01-03 17:19:17', 0),
(215, 37, 3, 10, 50, 1, '2023-01-04 10:43:46', 0),
(216, 37, 1, 9.2, 50, 0, '2023-01-01 19:19:00', 0);

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `id` int(11) NOT NULL,
  `name` varchar(32) NOT NULL,
  `email_address` varchar(32) NOT NULL,
  `personal_number` varchar(13) NOT NULL,
  `password` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`id`, `name`, `email_address`, `personal_number`, `password`) VALUES
(1, 'Banken', 'banken@banken.com', '19991231-1234', 'password'),
(2, 'Henrik Johansson', 'henrik@mail.com', '19900101-1234', 'password123');

-- --------------------------------------------------------

--
-- Table structure for table `listing`
--

CREATE TABLE `listing` (
  `id` int(11) NOT NULL,
  `name` varchar(32) NOT NULL,
  `fixed_courtage_price` double NOT NULL,
  `percentage_courtage_price` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `listing`
--

INSERT INTO `listing` (`id`, `name`, `fixed_courtage_price`, `percentage_courtage_price`) VALUES
(1, 'First North Stockholm', 29, 0),
(2, 'Large Cap Stockholm', 19, 0),
(3, 'Mid Cap Stockholm', 25, 0),
(4, 'Spotlight Stock Market', 39, 0);

-- --------------------------------------------------------

--
-- Table structure for table `stocks`
--

CREATE TABLE `stocks` (
  `id` int(11) NOT NULL,
  `listing_id` int(11) NOT NULL,
  `name` varchar(32) NOT NULL,
  `ticker` varchar(16) NOT NULL,
  `sector` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `stocks`
--

INSERT INTO `stocks` (`id`, `listing_id`, `name`, `ticker`, `sector`) VALUES
(1, 1, 'AAC Clyde Space', 'AAC', 'Teknik'),
(2, 2, 'AAK', 'AAK', 'Konsument, stabil'),
(3, 2, 'ABB Ltd', 'ABB', 'Industri'),
(4, 2, 'AddLife B', 'ALIF B', 'Sjukvård'),
(5, 1, 'Advanced SolTech Sweden', 'ASAB', '-'),
(6, 1, 'AegirBio', 'AEGIR', 'Sjukvård'),
(7, 1, 'Africa Energy Corp', 'AEC', 'Energi'),
(8, 3, 'Africa Oil Corp', 'AOI', 'Energi'),
(9, 2, 'AFRY', 'AFRY', 'Industri'),
(10, 1, 'Akelius Residential Prop. D', 'AKEL D', 'Fastigheter'),
(11, 2, 'Alfa Laval', 'ALFA', 'Industri'),
(12, 2, 'Alleima', 'ALLEI', '-'),
(13, 4, 'Angler Gaming', 'ANGL', 'Konsument, cyklisk'),
(14, 3, 'Annehem Fastigheter B', 'ANNE B', 'Fastigheter'),
(15, 1, 'Arcane Crypto', 'ARCANE', 'Finans'),
(16, 2, 'Arjo B', 'ARJO B', 'Sjukvård'),
(17, 2, 'ASSA ABLOY B', 'ASSA B', 'Industri'),
(18, 2, 'AstraZeneca', 'AZN', 'Sjukvård'),
(19, 2, 'Atlas Copco A', 'ATCO A', 'Industri'),
(20, 2, 'Atlas Copco B', 'ATCO B', 'Industri'),
(21, 2, 'Avanza Bank Holding', 'AZA', 'Finans'),
(22, 2, 'Axfood', 'AXFO', 'Konsument, stabil'),
(23, 1, 'Azelio', 'AZELIO', 'Allmännyttigt'),
(24, 4, 'Bahnhof B', 'BAHN B', 'Kommunikation'),
(25, 1, 'Bambuser', 'BUSER', 'Teknik'),
(26, 2, 'Beijer Ref B', 'BEIJ B', 'Industri'),
(27, 4, 'Beowulf Mining SDB', 'BEO SDB', 'Råvaror'),
(28, 3, 'Betsson B', 'BETS B', 'Konsument, cyklisk'),
(29, 2, 'BHG Group', 'BHG', 'Konsument, cyklisk'),
(30, 2, 'BICO Group', 'BICO', 'Sjukvård'),
(31, 2, 'Bilia A', 'BILI A', 'Konsument, cyklisk'),
(32, 2, 'Billerud', 'BILL', 'Råvaror'),
(33, 3, 'BioArctic B', 'BIOA B', 'Sjukvård'),
(34, 2, 'Boliden', 'BOL', 'Råvaror'),
(35, 3, 'Bonava B', 'BONAV B', 'Konsument, cyklisk'),
(36, 2, 'Bure Equity', 'BURE', 'Finans'),
(37, 3, 'Byggmax Group', 'BMAX', 'Industri'),
(38, 3, 'Calliditas Therapeutics', 'CALTX', 'Sjukvård'),
(39, 3, 'Cantargia', 'CANTA', 'Sjukvård'),
(40, 2, 'Castellum', 'CAST', 'Fastigheter'),
(41, 3, 'Catena Media', 'CTM', 'Konsument, cyklisk'),
(42, 1, 'Cell Impact', 'CI', 'Industri'),
(43, 3, 'Cibus Nordic Real Estate', 'CIBUS', 'Fastigheter'),
(44, 3, 'Clas Ohlson B', 'CLAS B', 'Konsument, cyklisk'),
(45, 1, 'Climeon B', 'CLIME B', 'Allmännyttigt'),
(46, 3, 'Cloetta B', 'CLA B', 'Konsument, stabil'),
(47, 1, 'Coala-life Group', 'COALA', 'Konsument, cyklisk'),
(48, 3, 'Coor Service Management Hold.', 'COOR', 'Industri'),
(49, 1, 'Copperstone Resources B', 'COPP B', 'Råvaror'),
(50, 2, 'Corem Property Group B', 'CORE B', 'Fastigheter'),
(51, 1, 'Cortus Energy', 'CE', 'Allmännyttigt'),
(52, 2, 'Creades A', 'CRED A', 'Finans'),
(53, 3, 'CTEK', 'CTEK', '-'),
(54, 1, 'Diamyd Medical B', 'DMYD B', 'Sjukvård'),
(55, 3, 'Diös Fastigheter', 'DIOS', 'Fastigheter'),
(56, 2, 'Dometic Group', 'DOM', 'Teknik'),
(57, 3, 'Dustin Group', 'DUST', 'Teknik'),
(58, 2, 'Electrolux B', 'ELUX B', 'Teknik'),
(59, 2, 'Electrolux Professional B', 'EPRO B', 'Konsument, cyklisk'),
(60, 2, 'Elekta B', 'EKTA B', 'Sjukvård'),
(61, 1, 'Embracer Group B', 'EMBRAC B', 'Kommunikation'),
(62, 3, 'EnQuest PLC', 'ENQ', 'Energi'),
(63, 3, 'Eolus Vind B', 'EOLU B', 'Industri'),
(64, 2, 'Epiroc B', 'EPI B', 'Industri'),
(65, 2, 'EQT', 'EQT', 'Finans'),
(66, 2, 'Ericsson B', 'ERIC B', 'Teknik'),
(67, 2, 'Essity B', 'ESSITY B', 'Konsument, stabil'),
(68, 2, 'Evolution', 'EVO', 'Konsument, cyklisk'),
(69, 2, 'Fabege', 'FABG', 'Fastigheter'),
(70, 2, 'Fast. Balder B', 'BALD B', 'Fastigheter'),
(71, 3, 'Ferronordic', 'FNM', 'Industri'),
(72, 3, 'Fingerprint Cards B', 'FING B', 'Teknik'),
(73, 1, 'Flat Capital B', 'FLAT B', '-'),
(74, 2, 'Fortnox', 'FNOX', 'Teknik'),
(75, 3, 'G5 Entertainment', 'G5EN', 'Kommunikation'),
(76, 3, 'Garo', 'GARO', 'Industri'),
(77, 2, 'Getinge B', 'GETI B', 'Sjukvård'),
(78, 2, 'Handelsbanken A', 'SHB A', 'Finans'),
(79, 2, 'Handelsbanken B', 'SHB B', 'Finans'),
(80, 3, 'Hansa Biopharma', 'HNSA', 'Sjukvård'),
(81, 2, 'Hennes & Mauritz B', 'HM B', 'Konsument, cyklisk'),
(82, 2, 'Hexagon B', 'HEXA B', 'Teknik'),
(83, 2, 'Hexatronic Group', 'HTRO', 'Teknik'),
(84, 1, 'Hexicon', 'HEXI', 'Industri'),
(85, 2, 'Holmen B', 'HOLM B', 'Råvaror'),
(86, 2, 'Hufvudstaden A', 'HUFV A', 'Fastigheter'),
(87, 1, 'Humble Group', 'HUMBLE', 'Konsument, stabil'),
(88, 2, 'Husqvarna B', 'HUSQ B', 'Industri'),
(89, 2, 'Industrivärden A', 'INDU A', 'Finans'),
(90, 2, 'Industrivärden C', 'INDU C', 'Finans'),
(91, 2, 'Indutrade', 'INDT', 'Industri'),
(92, 2, 'Instalco', 'INSTAL', 'Industri'),
(93, 3, 'International Petroleum Corp.', 'IPCO', 'Energi'),
(94, 1, 'Intervacc', 'IVACC', 'Sjukvård'),
(95, 2, 'Intrum', 'INTRUM', 'Finans'),
(96, 2, 'Investor A', 'INVE A', 'Finans'),
(97, 2, 'Investor B', 'INVE B', 'Finans'),
(98, 3, 'Inwido', 'INWI', 'Industri'),
(99, 1, 'ISR Holding', 'ISR', 'Sjukvård'),
(100, 2, 'JM', 'JM', 'Konsument, cyklisk'),
(101, 1, 'Kambi Group Plc', 'KAMBI', 'Konsument, cyklisk'),
(102, 1, 'Kancera', 'KAN', 'Sjukvård'),
(103, 2, 'Kindred Group', 'KIND SDB', 'Konsument, cyklisk'),
(104, 2, 'Kinnevik A', 'KINV A', 'Finans'),
(105, 2, 'Kinnevik B', 'KINV B', 'Finans'),
(106, 2, 'Latour B', 'LATO B', 'Finans'),
(107, 2, 'Lifco B', 'LIFCO B', 'Industri'),
(108, 3, 'Linc', 'LINC', 'Finans'),
(109, 2, 'Lindab International', 'LIAB', 'Industri'),
(110, 2, 'Lundbergföretagen B', 'LUND B', 'Finans'),
(111, 3, 'Lundin Gold', 'LUG', 'Råvaror'),
(112, 2, 'Lundin Mining Corp', 'LUMI', 'Råvaror'),
(113, 1, 'Mantex', 'MANTEX', 'Industri'),
(114, 1, 'Metacon', 'META', 'Industri'),
(115, 1, 'MGI - Media and Games Invest', 'M8G', 'Kommunikation'),
(116, 1, 'Midsummer', 'MIDS', 'Teknik'),
(117, 2, 'Millicom Int. Cellular SDB', 'TIGO SDB', 'Kommunikation'),
(118, 1, 'Minesto', 'MINEST', 'Allmännyttigt'),
(119, 2, 'Mips', 'MIPS', 'Konsument, cyklisk'),
(120, 3, 'Modern Times Group B', 'MTG B', 'Kommunikation'),
(121, 2, 'Mycronic', 'MYCR', 'Teknik'),
(122, 2, 'NCC B', 'NCC B', 'Industri'),
(123, 3, 'New Wave B', 'NEWA B', 'Konsument, cyklisk'),
(124, 2, 'NIBE Industrier B', 'NIBE B', 'Industri'),
(125, 3, 'Nobia', 'NOBI', 'Konsument, cyklisk'),
(126, 2, 'Nordea Bank Abp', 'NDA SE', 'Finans'),
(127, 3, 'NOTE', 'NOTE', 'Teknik'),
(128, 2, 'Nyfosa', 'NYF', 'Fastigheter'),
(129, 3, 'Oncopeptides', 'ONCO', 'Sjukvård'),
(130, 1, 'OptiCept Technologies', 'OPTI', 'Industri'),
(131, 2, 'Orrön Energy', 'ORRON', 'Energi'),
(132, 2, 'OX2', 'OX2', 'Allmännyttigt'),
(133, 1, 'Paradox Interactive', 'PDX', 'Kommunikation'),
(134, 2, 'Peab B', 'PEAB B', 'Industri'),
(135, 4, 'Plejd', 'PLEJD', 'Industri'),
(136, 1, 'PowerCell Sweden', 'PCELL', 'Industri'),
(137, 2, 'Ratos B', 'RATO B', 'Finans'),
(138, 1, 'Re:NewCell', 'RENEW', 'Konsument, cyklisk'),
(139, 3, 'Resurs Holding', 'RESURS', 'Finans'),
(140, 3, 'RVRC Holding', 'RVRC', 'Konsument, cyklisk'),
(141, 2, 'SAAB B', 'SAAB B', 'Industri'),
(142, 2, 'Sagax D', 'SAGA D', 'Fastigheter'),
(143, 1, 'SaltX Technology Holding B', 'SALT B', 'Industri'),
(144, 2, 'Samhällsbyggnadsbo. i Norden B', 'SBB B', 'Fastigheter'),
(145, 2, 'Samhällsbyggnadsbo. i Norden D', 'SBB D', 'Fastigheter'),
(146, 2, 'Sandvik', 'SAND', 'Industri'),
(147, 2, 'SAS', 'SAS', 'Industri'),
(148, 2, 'SCA B', 'SCA B', 'Råvaror'),
(149, 3, 'Scandic Hotels Group', 'SHOT', 'Konsument, cyklisk'),
(150, 1, 'Scandinavian Enviro Systems', 'SES', 'Industri'),
(151, 1, 'SeaTwirl', 'STW', 'Industri'),
(152, 2, 'SEB A', 'SEB A', 'Finans'),
(153, 2, 'SEB C', 'SEB C', 'Finans'),
(154, 2, 'SECTRA B', 'SECT B', 'Sjukvård'),
(155, 2, 'Securitas B', 'SECU B', 'Industri'),
(156, 2, 'Sinch', 'SINCH', 'Kommunikation'),
(157, 3, 'Sivers Semiconductors', 'SIVE', 'Teknik'),
(158, 2, 'Skanska B', 'SKA B', 'Industri'),
(159, 2, 'SKF B', 'SKF B', 'Industri'),
(160, 3, 'SkiStar B', 'SKIS B', 'Konsument, cyklisk'),
(161, 1, 'Smart Eye', 'SEYE', 'Teknik'),
(162, 1, 'SolTech Energy Sweden', 'SOLT', 'Teknik'),
(163, 1, 'SpectraCure', 'SPEC', 'Sjukvård'),
(164, 1, 'SpectrumOne', 'SPEONE', 'Teknik'),
(165, 1, 'Speqta', 'SPEQT', 'Kommunikation'),
(166, 2, 'SSAB A', 'SSAB A', 'Råvaror'),
(167, 2, 'SSAB B', 'SSAB B', 'Råvaror'),
(168, 2, 'Stillfront Group', 'SF', 'Kommunikation'),
(169, 2, 'Stora Enso A', 'STE A', 'Råvaror'),
(170, 2, 'Stora Enso R', 'STE R', 'Råvaror'),
(171, 2, 'Storskogen Group B', 'STOR B', '-'),
(172, 1, 'Storytel B', 'STORY B', 'Kommunikation'),
(173, 3, 'Svolder B', 'SVOL B', 'Finans'),
(174, 2, 'SWECO B', 'SWEC B', 'Industri'),
(175, 2, 'Swedbank A', 'SWED A', 'Finans'),
(176, 1, 'Swedencare', 'SECARE', 'Sjukvård'),
(177, 2, 'Swedish Match', 'SWMA', 'Konsument, stabil'),
(178, 2, 'Swedish Orphan Biovitrum', 'SOBI', 'Sjukvård'),
(179, 1, 'Swedish Stirling', 'STRLNG', 'Industri'),
(180, 3, 'SynAct Pharma', 'SYNACT', 'Sjukvård'),
(181, 2, 'Tele2 B', 'TEL2 B', 'Kommunikation'),
(182, 2, 'Telia Company', 'TELIA', 'Kommunikation'),
(183, 1, 'Terranet B', 'TERRNT B', 'Teknik'),
(184, 2, 'Thule Group', 'THULE', 'Konsument, cyklisk'),
(185, 3, 'Tobii', 'TOBII', 'Teknik'),
(186, 3, 'Tobii Dynavox', 'TDVOX', '-'),
(187, 2, 'Trelleborg B', 'TREL B', 'Industri'),
(188, 2, 'Truecaller B', 'TRUE B', '-'),
(189, 3, 'VEF', 'VEFAB', 'Finans'),
(190, 1, 'Vestum', 'VESTUM', 'Industri'),
(191, 2, 'Viaplay Group B', 'VPLAY B', 'Kommunikation'),
(192, 2, 'Vitrolife', 'VITR', 'Sjukvård'),
(193, 3, 'Vivesto', 'VIVE', 'Sjukvård'),
(194, 3, 'VNV Global', 'VNV', 'Finans'),
(195, 2, 'Volvo A', 'VOLV A', 'Industri'),
(196, 2, 'Volvo B', 'VOLV B', 'Industri'),
(197, 2, 'Volvo Car B', 'VOLCAR B', '-'),
(198, 2, 'Wallenstam B', 'WALL B', 'Fastigheter'),
(199, 2, 'Wihlborgs Fastigheter', 'WIHL', 'Fastigheter'),
(200, 3, 'Öresund', 'ORES', 'Finans');

-- --------------------------------------------------------

--
-- Table structure for table `stocks_to_account`
--

CREATE TABLE `stocks_to_account` (
  `id` int(11) NOT NULL,
  `account_id` int(11) NOT NULL,
  `stock_id` int(11) NOT NULL,
  `amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `stocks_to_account`
--

INSERT INTO `stocks_to_account` (`id`, `account_id`, `stock_id`, `amount`) VALUES
(1, 1, 1, 1000),
(2, 1, 2, 1000),
(3, 1, 3, 1000),
(4, 1, 4, 1000),
(5, 1, 5, 1000),
(6, 1, 6, 1000),
(7, 1, 7, 975),
(8, 1, 8, 1000),
(9, 1, 9, 1000),
(10, 1, 10, 1000),
(11, 1, 11, 1000),
(12, 1, 12, 1000),
(13, 1, 13, 1000),
(14, 1, 14, 1000),
(15, 1, 15, 1000),
(16, 1, 16, 1000),
(17, 1, 17, 1000),
(18, 1, 18, 950),
(19, 1, 19, 1000),
(20, 1, 20, 1000),
(21, 1, 21, 1000),
(22, 1, 22, 1000),
(23, 1, 23, 1000),
(24, 1, 24, 1000),
(25, 1, 25, 1000),
(26, 1, 26, 1000),
(27, 1, 27, 1000),
(28, 1, 28, 1000),
(29, 1, 29, 1000),
(30, 1, 30, 1000),
(31, 1, 31, 1000),
(32, 1, 32, 1000),
(33, 1, 33, 1000),
(34, 1, 34, 1000),
(35, 1, 35, 1000),
(36, 1, 36, 1000),
(37, 1, 37, 950),
(38, 1, 38, 1000),
(39, 1, 39, 1000),
(40, 1, 40, 1000),
(41, 1, 41, 1000),
(42, 1, 42, 1000),
(43, 1, 43, 1000),
(44, 1, 44, 1000),
(45, 1, 45, 1000),
(46, 1, 46, 1000),
(47, 1, 47, 1000),
(48, 1, 48, 1000),
(49, 1, 49, 1000),
(50, 1, 50, 1000),
(51, 1, 51, 1000),
(52, 1, 52, 1000),
(53, 1, 53, 1000),
(54, 1, 54, 1000),
(55, 1, 55, 1000),
(56, 1, 56, 1000),
(57, 1, 57, 1000),
(58, 1, 58, 1000),
(59, 1, 59, 1000),
(60, 1, 60, 1000),
(61, 1, 61, 1000),
(62, 1, 62, 1000),
(63, 1, 63, 1000),
(64, 1, 64, 1000),
(65, 1, 65, 1000),
(66, 1, 66, 1000),
(67, 1, 67, 1000),
(68, 1, 68, 1000),
(69, 1, 69, 1000),
(70, 1, 70, 1000),
(71, 1, 71, 1000),
(72, 1, 72, 1000),
(73, 1, 73, 1000),
(74, 1, 74, 965),
(75, 1, 75, 1000),
(76, 1, 76, 1000),
(77, 1, 77, 1000),
(78, 1, 78, 1000),
(79, 1, 79, 1000),
(80, 1, 80, 1000),
(81, 1, 81, 1000),
(82, 1, 82, 1000),
(83, 1, 83, 1000),
(84, 1, 84, 1000),
(85, 1, 85, 1000),
(86, 1, 86, 1000),
(87, 1, 87, 1000),
(88, 1, 88, 1000),
(89, 1, 89, 1000),
(90, 1, 90, 1000),
(91, 1, 91, 1000),
(92, 1, 92, 1000),
(93, 1, 93, 1000),
(94, 1, 94, 1000),
(95, 1, 95, 1000),
(96, 1, 96, 1000),
(97, 1, 97, 1000),
(98, 1, 98, 1000),
(99, 1, 99, 1000),
(100, 1, 100, 1000),
(101, 1, 101, 1000),
(102, 1, 102, 1000),
(103, 1, 103, 1000),
(104, 1, 104, 1000),
(105, 1, 105, 1000),
(106, 1, 106, 1000),
(107, 1, 107, 1000),
(108, 1, 108, 1000),
(109, 1, 109, 1000),
(110, 1, 110, 1000),
(111, 1, 111, 1000),
(112, 1, 112, 1000),
(113, 1, 113, 1000),
(114, 1, 114, 1000),
(115, 1, 115, 1000),
(116, 1, 116, 1000),
(117, 1, 117, 1000),
(118, 1, 118, 1000),
(119, 1, 119, 1000),
(120, 1, 120, 1000),
(121, 1, 121, 1000),
(122, 1, 122, 1000),
(123, 1, 123, 1000),
(124, 1, 124, 1000),
(125, 1, 125, 1000),
(126, 1, 126, 1000),
(127, 1, 127, 1000),
(128, 1, 128, 1000),
(129, 1, 129, 1000),
(130, 1, 130, 1000),
(131, 1, 131, 1000),
(132, 1, 132, 1000),
(133, 1, 133, 1000),
(134, 1, 134, 1000),
(135, 1, 135, 1000),
(136, 1, 136, 1000),
(137, 1, 137, 1000),
(138, 1, 138, 1000),
(139, 1, 139, 1000),
(140, 1, 140, 1000),
(141, 1, 141, 1000),
(142, 1, 142, 1000),
(143, 1, 143, 1000),
(144, 1, 144, 1000),
(145, 1, 145, 1000),
(146, 1, 146, 1000),
(147, 1, 147, 1000),
(148, 1, 148, 1000),
(149, 1, 149, 1000),
(150, 1, 150, 1000),
(151, 1, 151, 1000),
(152, 1, 152, 1000),
(153, 1, 153, 1000),
(154, 1, 154, 1000),
(155, 1, 155, 1000),
(156, 1, 156, 1000),
(157, 1, 157, 1000),
(158, 1, 158, 1000),
(159, 1, 159, 1000),
(160, 1, 160, 1000),
(161, 1, 161, 1000),
(162, 1, 162, 1000),
(163, 1, 163, 1000),
(164, 1, 164, 1000),
(165, 1, 165, 1000),
(166, 1, 166, 1000),
(167, 1, 167, 1000),
(168, 1, 168, 1000),
(169, 1, 169, 1000),
(170, 1, 170, 1000),
(171, 1, 171, 1000),
(172, 1, 172, 1000),
(173, 1, 173, 1000),
(174, 1, 174, 1000),
(175, 1, 175, 1000),
(176, 1, 176, 1000),
(177, 1, 177, 1000),
(178, 1, 178, 1000),
(179, 1, 179, 1000),
(180, 1, 180, 1000),
(181, 1, 181, 1000),
(182, 1, 182, 1000),
(183, 1, 183, 1000),
(184, 1, 184, 1000),
(185, 1, 185, 1000),
(186, 1, 186, 1000),
(187, 1, 187, 1000),
(188, 1, 188, 1000),
(189, 1, 189, 1000),
(190, 1, 190, 1000),
(191, 1, 191, 987),
(192, 1, 192, 1000),
(193, 1, 193, 1000),
(194, 1, 194, 1000),
(195, 1, 195, 1000),
(196, 1, 196, 1000),
(197, 1, 197, 1000),
(198, 1, 198, 1000),
(199, 1, 199, 1000),
(200, 1, 200, 1000),
(201, 3, 74, 35),
(202, 3, 18, 50),
(203, 3, 191, 13),
(204, 3, 7, 25),
(205, 3, 37, 50);

-- --------------------------------------------------------

--
-- Table structure for table `stock_account`
--

CREATE TABLE `stock_account` (
  `id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `account_type_id` tinyint(4) NOT NULL,
  `balance_in_sek` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `stock_account`
--

INSERT INTO `stock_account` (`id`, `customer_id`, `account_type_id`, `balance_in_sek`) VALUES
(1, 1, 1, 1001159.9),
(2, 1, 2, 3000),
(3, 2, 1, 48840.1),
(4, 99, 1, 550);

-- --------------------------------------------------------

--
-- Table structure for table `stock_transactions`
--

CREATE TABLE `stock_transactions` (
  `id` int(11) NOT NULL,
  `buyer_account_id` int(11) NOT NULL,
  `seller_account_id` int(11) NOT NULL,
  `price_per_stock` double NOT NULL,
  `amount` int(11) NOT NULL,
  `transaction_time` datetime NOT NULL,
  `buyer_courtage` double NOT NULL,
  `seller_courtage` double NOT NULL,
  `stock_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `stock_transactions`
--

INSERT INTO `stock_transactions` (`id`, `buyer_account_id`, `seller_account_id`, `price_per_stock`, `amount`, `transaction_time`, `buyer_courtage`, `seller_courtage`, `stock_id`) VALUES
(40, 3, 1, 6.9, 25, '2023-01-01 19:31:01', 0, 0, 74),
(41, 3, 1, 4.8, 50, '2023-01-01 19:35:44', 0, 0, 18),
(42, 3, 1, 2.3, 13, '2023-01-01 19:38:20', 0, 0, 191),
(43, 3, 1, 10.75, 10, '2023-01-01 19:49:02', 0, 0, 74),
(45, 3, 1, 6, 25, '2023-01-03 17:20:36', 0, 0, 7),
(46, 3, 1, 9.2, 50, '2023-01-04 10:43:46', 0, 0, 37);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account_type`
--
ALTER TABLE `account_type`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `active_orders`
--
ALTER TABLE `active_orders`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `listing`
--
ALTER TABLE `listing`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stocks`
--
ALTER TABLE `stocks`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stocks_to_account`
--
ALTER TABLE `stocks_to_account`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stock_account`
--
ALTER TABLE `stock_account`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `stock_transactions`
--
ALTER TABLE `stock_transactions`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account_type`
--
ALTER TABLE `account_type`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `active_orders`
--
ALTER TABLE `active_orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=217;

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `listing`
--
ALTER TABLE `listing`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `stocks`
--
ALTER TABLE `stocks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=201;

--
-- AUTO_INCREMENT for table `stocks_to_account`
--
ALTER TABLE `stocks_to_account`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=206;

--
-- AUTO_INCREMENT for table `stock_account`
--
ALTER TABLE `stock_account`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `stock_transactions`
--
ALTER TABLE `stock_transactions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=47;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
