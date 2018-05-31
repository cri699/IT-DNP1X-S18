/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50722
 Source Host           : localhost:3306
 Source Schema         : cats_system

 Target Server Type    : MySQL
 Target Server Version : 50722
 File Encoding         : 65001

 Date: 31/05/2018 18:37:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for carts
-- ----------------------------
DROP TABLE IF EXISTS `carts`;
CREATE TABLE `carts`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `user_id` int(10) NOT NULL,
  `cat_id` int(10) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `cat_id`(`cat_id`) USING BTREE,
  INDEX `user_id`(`user_id`) USING BTREE,
  CONSTRAINT `carts_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `carts_ibfk_2` FOREIGN KEY (`cat_id`) REFERENCES `cats` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of carts
-- ----------------------------
INSERT INTO `carts` VALUES (3, 2, 1);
INSERT INTO `carts` VALUES (4, 2, 2);
INSERT INTO `carts` VALUES (5, 2, 2);
INSERT INTO `carts` VALUES (8, 3, 2);
INSERT INTO `carts` VALUES (9, 3, 2);
INSERT INTO `carts` VALUES (10, 3, 1);
INSERT INTO `carts` VALUES (11, 3, 1);

-- ----------------------------
-- Table structure for cats
-- ----------------------------
DROP TABLE IF EXISTS `cats`;
CREATE TABLE `cats`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `price` decimal(10, 2) NOT NULL,
  `photo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `status` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cats
-- ----------------------------
INSERT INTO `cats` VALUES (1, 'Tommy', 10.00, '7050f692-cc3f-4b8c-bf03-57d58c2ec134.jpg', 'available');
INSERT INTO `cats` VALUES (2, 'Rambo', 20.00, '773f93b8-3b3c-44f4-be95-3e6c1b70d8c0.jpg', 'available');

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES (1, 'admin');
INSERT INTO `roles` VALUES (2, 'user');

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `first_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `last_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `role_id` int(10) NOT NULL,
  `created_at` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `role_id`(`role_id`) USING BTREE,
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (2, 'cristian', 'herisu', 'xbyteslimited@gmail.com', 'test', 1, '2018-05-31 18:25:16');
INSERT INTO `users` VALUES (3, 'cristian', 'herisu', 'test@test.com', 'test', 2, '2018-05-28 17:59:13');

SET FOREIGN_KEY_CHECKS = 1;
