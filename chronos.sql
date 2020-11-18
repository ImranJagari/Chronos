/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : localhost:3306
 Source Schema         : chronos

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 18/11/2020 08:24:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Authority` tinyint(4) NOT NULL,
  `Password` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `HDSN` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `IP_Key` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `Authority`(`Authority`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES (1, 'test', 7, '098f6bcd4621d373cade4e832627b4f6', '', '');

-- ----------------------------
-- Table structure for breeds
-- ----------------------------
DROP TABLE IF EXISTS `breeds`;
CREATE TABLE `breeds`  (
  `Job` tinyint(4) NOT NULL,
  `StartMap` int(11) NOT NULL,
  `StartX` decimal(10, 2) NOT NULL,
  `StartY` decimal(10, 2) NOT NULL,
  `StartZ` decimal(10, 2) NOT NULL,
  `StartLevel` int(11) NOT NULL,
  `StartStrenght` int(11) NOT NULL,
  `StartStamina` int(11) NOT NULL,
  `StartDexterity` int(11) NOT NULL,
  `StartIntelligence` int(11) NOT NULL,
  `StartSPI` int(11) NOT NULL,
  `StartHP` int(11) NOT NULL,
  `StartMP` int(11) NOT NULL,
  `StartMoney` int(11) NOT NULL,
  PRIMARY KEY (`Job`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of breeds
-- ----------------------------
INSERT INTO `breeds` VALUES (1, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (2, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (3, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (4, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (5, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (6, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (7, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);
INSERT INTO `breeds` VALUES (8, 1, 700.00, 100.00, 750.00, 1, 1, 1, 1, 1, 1, 100, 100, 100);

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL,
  `Slot` int(11) NOT NULL,
  `Name` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `HD_MD5` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SceneId` int(11) NOT NULL,
  `Sex` tinyint(4) NOT NULL,
  `X` decimal(10, 2) NOT NULL,
  `Y` decimal(10, 2) NOT NULL,
  `Z` decimal(10, 2) NOT NULL,
  `Level` int(11) NOT NULL,
  `Experience` bigint(20) UNSIGNED NOT NULL,
  `Job` int(11) NOT NULL,
  `Money` int(10) UNSIGNED NOT NULL,
  `HP` int(11) NOT NULL,
  `DamageTaken` int(11) NOT NULL,
  `Strenght` int(11) NOT NULL,
  `Stamina` int(11) NOT NULL,
  `Dexterity` int(11) NOT NULL,
  `Intelligence` int(11) NOT NULL,
  `SPI` int(11) NOT NULL,
  `EP` int(11) NOT NULL,
  `HairMesh` int(11) NOT NULL,
  `HairColor` int(10) UNSIGNED NOT NULL,
  `HeadMesh` int(11) NOT NULL,
  `City_Code` int(11) NOT NULL,
  `Constellation` int(11) NOT NULL,
  `Country` tinyint(4) NOT NULL,
  `SN_Card` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Card_Type` int(11) NOT NULL,
  `HD_SN` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Bin_Account` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `BlockTime` datetime(0) NOT NULL,
  `DeletedDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of characters
-- ----------------------------
INSERT INTO `characters` VALUES (1, 1, 0, 'Fallen', 'mm5fbe7117308d8e3d9e107c1588803128', 1, 1, 700.00, 100.00, 750.00, 1, 60, 7, 100, 100, 0, 1, 1, 1, 1, 1, 0, 0, 4278190080, 0, 1, 9, 1, '', 0, '', '', '0001-01-01 00:00:00', NULL);
INSERT INTO `characters` VALUES (2, 1, 1, 'Fallou', 'mm5fbe7117308d8e3d9e107c1588803128', 1, 1, 700.00, 100.00, 750.00, 1, 60, 7, 100, 100, 0, 1, 1, 1, 1, 1, 0, 0, 4278190080, 0, 1, 9, 1, '', 0, '', '', '0001-01-01 00:00:00', NULL);

-- ----------------------------
-- Table structure for characters_closetitems
-- ----------------------------
DROP TABLE IF EXISTS `characters_closetitems`;
CREATE TABLE `characters_closetitems`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerId` int(11) NOT NULL,
  `ClosetItemId` int(11) NOT NULL,
  `Slot` int(11) NOT NULL,
  `Equipped` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 11 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of characters_closetitems
-- ----------------------------
INSERT INTO `characters_closetitems` VALUES (1, 1, 992007, 1, 1);
INSERT INTO `characters_closetitems` VALUES (2, 1, 992006, 2, 1);
INSERT INTO `characters_closetitems` VALUES (3, 1, 992008, 3, 1);
INSERT INTO `characters_closetitems` VALUES (4, 1, 992009, 4, 1);
INSERT INTO `characters_closetitems` VALUES (5, 1, 992094, 5, 1);
INSERT INTO `characters_closetitems` VALUES (6, 2, 992111, 1, 1);
INSERT INTO `characters_closetitems` VALUES (7, 2, 992110, 2, 1);
INSERT INTO `characters_closetitems` VALUES (8, 2, 992112, 3, 1);
INSERT INTO `characters_closetitems` VALUES (9, 2, 992113, 4, 1);
INSERT INTO `characters_closetitems` VALUES (10, 2, 992095, 5, 1);

-- ----------------------------
-- Table structure for characters_items
-- ----------------------------
DROP TABLE IF EXISTS `characters_items`;
CREATE TABLE `characters_items`  (
  `Hitpoint` int(11) NOT NULL,
  `MaxHitpoint` int(11) NOT NULL,
  `Word` int(10) UNSIGNED NOT NULL,
  `Option` int(11) NOT NULL,
  `ItemResist` tinyint(4) NOT NULL,
  `ItemResistAbilityOption` tinyint(4) NOT NULL,
  `KeepTime` int(11) NOT NULL,
  `ItemLock` tinyint(4) NOT NULL,
  `BindEndTime` int(11) NOT NULL,
  `Stability` tinyint(4) NOT NULL,
  `Quality` tinyint(4) NOT NULL,
  `AbilityRate` tinyint(4) NOT NULL,
  `UseTime` int(11) NOT NULL,
  `BuyTm` int(11) NOT NULL,
  `Price` int(11) NOT NULL,
  `PriceToken` int(11) NOT NULL,
  `PriceFreeToken` int(11) NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerId` int(11) NOT NULL,
  `Slot` tinyint(4) NOT NULL,
  `ItemId` int(10) UNSIGNED NOT NULL,
  `Quantity` int(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of characters_items
-- ----------------------------

-- ----------------------------
-- Table structure for world_maps
-- ----------------------------
DROP TABLE IF EXISTS `world_maps`;
CREATE TABLE `world_maps`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of world_maps
-- ----------------------------
INSERT INTO `world_maps` VALUES (1, 'StartWorld');

SET FOREIGN_KEY_CHECKS = 1;
