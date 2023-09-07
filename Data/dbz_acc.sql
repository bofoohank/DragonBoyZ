-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th9 06, 2023 lúc 04:31 PM
-- Phiên bản máy phục vụ: 10.4.28-MariaDB
-- Phiên bản PHP: 8.0.28

-- Patch 1.1.0 

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `dbz_acc`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `character`
--

CREATE TABLE `character` (
  `id` int(10) UNSIGNED NOT NULL,
  `Name` varchar(50) NOT NULL DEFAULT '',
  `Skills` longtext NOT NULL,
  `ItemBody` longtext NOT NULL,
  `ItemBag` longtext NOT NULL,
  `ItemBox` longtext NOT NULL,
  `InfoChar` longtext NOT NULL,
  `BoughtSkill` longtext NOT NULL,
  `InfoTask` longtext DEFAULT NULL,
  `PlusBag` int(11) DEFAULT 0,
  `PlusBox` int(11) DEFAULT 0,
  `Friends` longtext DEFAULT NULL,
  `Enemies` longtext DEFAULT NULL,
  `Me` varchar(500) DEFAULT '[]',
  `ClanId` int(11) DEFAULT -1,
  `LuckyBox` longtext DEFAULT NULL,
  `LastLogin` datetime DEFAULT '2022-03-05 18:25:21',
  `CreateDate` datetime DEFAULT '2022-03-05 18:25:21',
  `SpecialSkill` longtext DEFAULT NULL,
  `InfoBuff` longtext DEFAULT NULL,
  `DataEvent` int(11) NOT NULL DEFAULT 0,
  `DataMinigame` longtext DEFAULT NULL,
  `DataBlackBall` longtext DEFAULT NULL,
  `DataBoMong` longtext DEFAULT NULL,
  `DataDaiHoiVoThuat` longtext DEFAULT NULL,
  `DataTraining` longtext DEFAULT NULL,
  `DataSieuHang` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `clan`
--

CREATE TABLE `clan` (
  `id` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT '',
  `Khẩu hiệu` varchar(500) DEFAULT '',
  `ImgId` int(11) DEFAULT 0,
  `Điểm thành tích` bigint(20) DEFAULT 0,
  `LeaderName` varchar(50) DEFAULT '',
  `Thành viên hiện tại` int(11) DEFAULT 0,
  `Thành viên tối đa` int(11) DEFAULT 10,
  `Thời gian tạo bang` bigint(20) DEFAULT 0,
  `Cấp độ` int(11) DEFAULT 1,
  `Capsule Bang` int(11) DEFAULT 0,
  `Thành viên` longtext DEFAULT NULL,
  `Messages` longtext DEFAULT NULL,
  `CharacterPeas` longtext DEFAULT NULL,
  `DataBlackBall` longtext DEFAULT NULL,
  `Leader` longtext DEFAULT NULL,
  `ClanBox` longtext DEFAULT NULL,
  `Điểm Danh Vọng` varchar(255) DEFAULT NULL,
  `KhiGas` longtext DEFAULT NULL,
  `DateTime` datetime DEFAULT NULL,
  `shortName` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `disciple`
--

CREATE TABLE `disciple` (
  `id` int(11) NOT NULL,
  `Name` varchar(15) NOT NULL DEFAULT '',
  `Status` int(11) NOT NULL DEFAULT 0,
  `Skills` longtext DEFAULT NULL,
  `ItemBody` longtext DEFAULT NULL,
  `InfoChar` longtext DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `Type` int(11) DEFAULT 1,
  `Info` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `gameinfo`
--

CREATE TABLE `gameinfo` (
  `id` int(11) DEFAULT NULL,
  `main` varchar(500) DEFAULT NULL,
  `content` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

--
-- Đang đổ dữ liệu cho bảng `gameinfo`
--

INSERT INTO `gameinfo` (`id`, `main`, `content`) VALUES
(0, 'Message When Login game', 'Chúc bạn chơi game vui vẻ <3');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `giftcode`
--

CREATE TABLE `giftcode` (
  `code` varchar(255) NOT NULL,
  `count` int(11) DEFAULT 1,
  `time_expire` datetime DEFAULT NULL,
  `item` longtext DEFAULT NULL,
  `gold` int(225) NOT NULL,
  `gem` int(225) NOT NULL,
  `ruby` int(225) NOT NULL,
  `used` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci ROW_FORMAT=DYNAMIC;

--
-- Đang đổ dữ liệu cho bảng `giftcode`
--

INSERT INTO `giftcode` (`code`, `count`, `time_expire`, `item`, `gold`, `gem`, `ruby`, `used`) VALUES
('666', 9878, '2024-06-01 15:39:54', '[{\"id\":16, \"Quantity\":5, \"Options\": [{}]}]', 0, 0, 0, '[10211,10233,10217,10232,10236,10237,10057,10239,10241,10243,10247,10248,10249,10209,10252,10253,10254,10084,10137,10258,10259,10262,10263,10186,10266,10238,10264]'),
('dangusaccc', 9912, '2024-06-01 15:39:54', '[{\"id\":674, \"Quantity\":200, \"Options\": [{}]}]', 0, 0, 0, '[10137,10211,10233,10113,10217,10232,10235,10236,10124,10057,10239,10241,10243,10246,10247,10248,10249,10209,10252,10253,10254,10084,10186,10263,10266,10264]'),
('denbu', 9946, '2024-06-01 15:39:54', '[{\"id\":457, \"Quantity\":20, \"Options\": [{}]}]', 0, 0, 0, '[10188,10008,10093,10165,10096,10143,10071,10117,10088,10002,10108,10003,10015,10163,10029,10004,10055,10107,10193,10102,10075,10197,10194,10147,10200,10068,10013,10010,10006,10135,10155,10151,10053,10092,10180,10201,10202,10007,10178,10091,10204,10089,10074,10207,10157,10210,10212,10215,10216,10214,10114,10217,10218]'),
('tanthu', 9655, '2024-06-01 15:39:54', '[{\"id\":883, \"Quantity\":1, \"Options\": [{\"id\":101, \"param\": 100}]},{\"id\":1274, \"Quantity\":5, \"Options\": [{\"id\":30, \"param\": 1}]},{\"id\":457, \"Quantity\":5, \"Options\": [{}]},{\"id\":14, \"Quantity\":1, \"Options\": [{}]},{\"id\":15, \"Quantity\":1, \"Options\": [{}]},{\"id\":16, \"Quantity\":1, \"Options\": [{}]},{\"id\":17, \"Quantity\":1, \"Options\": [{}]},{\"id\":18, \"Quantity\":1, \"Options\": [{}]},{\"id\":19, \"Quantity\":1, \"Options\": [{}]},{\"id\":20, \"Quantity\":1, \"Options\": [{}]},{\"id\":381, \"Quantity\":5, \"Options\": [{}]},{\"id\":382, \"Quantity\":5, \"Options\": [{}]},{\"id\":383, \"Quantity\":5, \"Options\": [{}]},{\"id\":384, \"Quantity\":5, \"Options\": [{}]},{\"id\":457, \"Quantity\":10, \"Options\": [{}]}]', 0, 0, 0, '[10211,10233,10146,10217,10232,10234,10235,10236,10237,10057,10239,10118,10241,10227,10242,10243,10089,10246,10247,10248,10249,10209,10253,10254,10084,10137,10257,10258,10259,10202,10262,10263,10186,10266,10238,2,10007,10005,10001,10006,10008,10012,10003,10013,10011,10016,10014,10009,10015,10018,10019,10024,10023,10021,10022,10028,10010,10030,10032,10033,10026,10025,10037,10036,10044,10046,10031,10047,10045,10042,10034,10041,10029,10002,10049,10048,10052,10039,10020,10054,10053,10055,10027,10056,10058,10059,10060,10061,10063,10064,10066,10043,10065,10067,10068,10069,10070,10004,10072,10073,10074,10076,10062,10035,10038,10077,10080,10081,10079,10078,10082,10083,10075,10086,10040,10071,10087,10090,10093,10091,10096,10095,10099,10100,10101,10102,10104,10108,10110,10111,10113,10115,10116,10117,10107,10094,10120,10121,10119,10122,10125,10088,10126,10127,10128,10130,10133,10134,10136,10139,10132,10138,10140,10141,10143,10144,10145,10147,10148,10149,10150,10152,10151,10051,10154,10155,10098,10156,10157,10158,10159,10161,10160,10162,10163,10165,10171,10177,10178,10179,10180,10181,10182,10183,10131,10135,10187,10188,10191,10192,10193,10195,10196,10194,10197,10200,10204,10207,10210,10212,10213,10216,10215,10214,10114,10218]'),
('trian', 9861, '2024-06-01 15:39:54', '[{\"id\":457, \"Quantity\":20, \"Options\": [{}]}]', 0, 0, 0, '[10058,10067,10066,10045,10010,10063,10069,10042,10023,10072,10004,10039,10074,10038,10032,10008,10029,10077,10078,10080,10082,10056,10043,10081,10086,10046,10025,10019,10040,10079,10071,10037,10090,10087,10036,10022,10031,10088,10053,10021,10007,10084,10052,10095,10091,10099,10009,10100,10033,10101,10102,10012,10104,10002,10110,10111,10113,10016,10116,10117,10015,10096,10120,10020,10093,10089,10125,10126,10128,10132,10107,10134,10136,10139,10130,10140,10141,10143,10144,10145,10146,10147,10148,10149,10150,10030,10051,10151,10155,10098,10156,10157,10159,10161,10160,10094,10163,10165,10171,10177,10178,10179,10183,10182,10186,10131,10135,10187,10188,10003,10191,10192,10013,10193,10195,10075,10196,10194,10055,10108,10197,10200,10006,10092,10180,10201,10202,10204,10207,10210,10212,10213,10216,10215,10214,10114,10217,10218]');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `magictree`
--

CREATE TABLE `magictree` (
  `id` bigint(20) NOT NULL,
  `idNpc` int(11) UNSIGNED NOT NULL DEFAULT 0,
  `x` int(11) DEFAULT 0,
  `y` int(11) DEFAULT 0,
  `level` int(11) DEFAULT 1,
  `peas` int(11) DEFAULT 5,
  `maxPea` int(11) DEFAULT 5,
  `seconds` bigint(20) DEFAULT 0,
  `isUpdating` int(11) DEFAULT 0,
  `Diamond` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

--
-- Đang đổ dữ liệu cho bảng `magictree`
--

INSERT INTO `magictree` (`id`, `idNpc`, `x`, `y`, `level`, `peas`, `maxPea`, `seconds`, `isUpdating`, `Diamond`) VALUES
(10228, 84, 348, 336, 1, 5, 5, 0, 0, 0),
(10229, 371, 372, 336, 1, 5, 5, 0, 0, 0),
(10230, 84, 348, 336, 1, 5, 5, 0, 0, 0),
(10231, 378, 372, 336, 1, 5, 5, 0, 0, 0),
(10232, 371, 372, 336, 1, 5, 5, 0, 0, 0),
(10233, 84, 348, 336, 1, 5, 5, 0, 0, 0),
(10234, 371, 372, 336, 1, 5, 5, 0, 0, 0),
(10235, 378, 372, 336, 1, 5, 5, 0, 0, 0),
(10236, 378, 372, 336, 1, 5, 5, 0, 0, 0),
(10237, 378, 372, 336, 1, 5, 5, 0, 0, 0),
(10238, 371, 372, 336, 1, 5, 5, 0, 0, 0),
(10239, 84, 348, 336, 1, 5, 5, 0, 0, 0),
(10240, 371, 372, 336, 1, 5, 5, 0, 0, 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `napthe`
--

CREATE TABLE `napthe` (
  `callback_sign` varchar(255) NOT NULL,
  `status` int(11) DEFAULT NULL COMMENT '0 đang chờ, 1 thành công, 2 lỗi',
  `request_id` varchar(255) DEFAULT NULL,
  `telco` varchar(255) DEFAULT NULL,
  `serial` varchar(255) DEFAULT NULL,
  `code` varchar(255) DEFAULT NULL,
  `trans_id` bigint(20) DEFAULT NULL,
  `value` int(11) DEFAULT NULL COMMENT 'Giá trị thực của thẻ',
  `message` varchar(255) DEFAULT NULL,
  `declared_value` int(11) DEFAULT NULL COMMENT 'Số tiền gửi lên',
  `amount` int(11) DEFAULT NULL COMMENT 'Giá trị thực nhận',
  `response_code` int(11) DEFAULT NULL COMMENT 'Giá trị trả về khi gửi thẻ',
  `created_time` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_time` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `user_id` int(11) DEFAULT -1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci ROW_FORMAT=DYNAMIC;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `regexchat`
--

CREATE TABLE `regexchat` (
  `id` int(11) NOT NULL,
  `text` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `user`
--

CREATE TABLE `user` (
  `id` int(10) UNSIGNED NOT NULL,
  `username` varchar(50) DEFAULT '',
  `password` varchar(50) DEFAULT '',
  `character` bigint(20) DEFAULT 0,
  `active` tinyint(4) DEFAULT 0,
  `role` int(11) DEFAULT 0,
  `ban` tinyint(4) DEFAULT 0,
  `online` tinyint(4) DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `sdt` text DEFAULT NULL,
  `vnd` int(11) NOT NULL DEFAULT 0,
  `tongnap` varchar(255) NOT NULL DEFAULT '0',
  `email` varchar(255) DEFAULT NULL,
  `diemtichnap` int(11) NOT NULL DEFAULT 0,
  `sv_port` int(11) NOT NULL DEFAULT 14445,
  `logout_time` bigint(20) NOT NULL DEFAULT 0,
  `last_ip` varchar(24) DEFAULT NULL,
  `is_login` tinyint(4) DEFAULT 0,
  `thoivang` varchar(255) DEFAULT '0',
  `hongngoc` varchar(255) DEFAULT '0',
  `admin` int(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=DYNAMIC;

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `character`
--
ALTER TABLE `character`
  ADD PRIMARY KEY (`id`) USING BTREE;

--
-- Chỉ mục cho bảng `clan`
--
ALTER TABLE `clan`
  ADD PRIMARY KEY (`id`) USING BTREE;

--
-- Chỉ mục cho bảng `disciple`
--
ALTER TABLE `disciple`
  ADD PRIMARY KEY (`id`) USING BTREE;

--
-- Chỉ mục cho bảng `giftcode`
--
ALTER TABLE `giftcode`
  ADD PRIMARY KEY (`code`) USING BTREE;

--
-- Chỉ mục cho bảng `magictree`
--
ALTER TABLE `magictree`
  ADD PRIMARY KEY (`id`) USING BTREE;

--
-- Chỉ mục cho bảng `napthe`
--
ALTER TABLE `napthe`
  ADD PRIMARY KEY (`callback_sign`) USING BTREE;

--
-- Chỉ mục cho bảng `regexchat`
--
ALTER TABLE `regexchat`
  ADD PRIMARY KEY (`id`) USING BTREE,
  ADD UNIQUE KEY `id` (`id`) USING BTREE,
  ADD KEY `id_2` (`id`) USING BTREE;

--
-- Chỉ mục cho bảng `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`) USING BTREE,
  ADD KEY `character` (`character`) USING BTREE;

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `character`
--
ALTER TABLE `character`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10241;

--
-- AUTO_INCREMENT cho bảng `clan`
--
ALTER TABLE `clan`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT cho bảng `magictree`
--
ALTER TABLE `magictree`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10241;

--
-- AUTO_INCREMENT cho bảng `regexchat`
--
ALTER TABLE `regexchat`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `user`
--
ALTER TABLE `user`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=333;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
