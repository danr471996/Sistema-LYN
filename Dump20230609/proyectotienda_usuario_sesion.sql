-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: proyectotienda
-- ------------------------------------------------------
-- Server version	8.0.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `usuario_sesion`
--

DROP TABLE IF EXISTS `usuario_sesion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario_sesion` (
  `Idusuario_sesion` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Idusuario` int NOT NULL,
  `Estado` int NOT NULL COMMENT '1-activo, 2-inactivo',
  PRIMARY KEY (`Idusuario_sesion`),
  KEY `fr_usuarios_tiendas_idx` (`Idusuario`),
  CONSTRAINT `fr_usuarios_tiendas` FOREIGN KEY (`Idusuario`) REFERENCES `usuarios_tienda` (`Idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=460 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario_sesion`
--

LOCK TABLES `usuario_sesion` WRITE;
/*!40000 ALTER TABLE `usuario_sesion` DISABLE KEYS */;
INSERT INTO `usuario_sesion` VALUES (1,'2022-09-13 13:39:39','danr','2022-09-13 13:39:57','danr',1,2),(2,'2022-09-13 13:41:31','danr','2022-09-13 13:41:59','danr',1,2),(3,'2022-09-13 13:47:59','danr','2022-09-13 13:41:59','danr',1,2),(4,'2022-09-13 13:53:02','danr',NULL,NULL,1,2),(5,'2022-09-13 14:34:02','danr',NULL,NULL,1,2),(6,'2022-09-13 14:52:53','danr',NULL,NULL,1,2),(7,'2022-09-13 15:03:01','danr',NULL,NULL,1,2),(8,'2022-09-13 15:05:53','danr',NULL,NULL,1,2),(9,'2022-09-13 15:17:55','danr',NULL,NULL,1,2),(10,'2022-09-16 09:24:33','danr',NULL,NULL,1,2),(11,'2022-09-16 10:42:09','danr',NULL,NULL,1,2),(12,'2022-09-16 10:49:30','danr',NULL,NULL,1,2),(13,'2022-09-16 10:52:41','danr',NULL,NULL,1,2),(14,'2022-09-16 11:02:34','danr',NULL,NULL,1,2),(15,'2022-09-16 11:16:49','danr',NULL,NULL,1,2),(16,'2022-09-16 11:18:29','danr','2022-09-16 11:19:56','danr',1,2),(17,'2022-09-16 11:20:05','danr',NULL,NULL,1,2),(18,'2022-09-16 12:18:49','danr',NULL,NULL,1,2),(19,'2022-09-16 12:22:51','danr','2022-09-16 12:23:05','danr',1,2),(20,'2022-09-16 12:23:21','danr','2022-09-16 12:26:14','danr',1,2),(21,'2022-09-16 12:26:23','danr','2022-09-16 12:29:10','danr',1,2),(22,'2022-09-16 12:29:19','danr',NULL,NULL,1,2),(23,'2022-09-16 12:32:09','danr','2022-09-16 12:37:26','danr',1,2),(24,'2022-09-16 12:38:41','danr',NULL,NULL,1,2),(25,'2022-09-16 13:28:19','danr',NULL,NULL,1,2),(26,'2022-09-16 14:00:32','danr','2022-09-16 14:05:30','danr',1,2),(27,'2022-09-16 14:30:40','danr',NULL,NULL,1,2),(28,'2022-09-16 14:53:11','danr',NULL,NULL,1,2),(29,'2022-09-16 15:25:48','danr',NULL,NULL,1,2),(30,'2022-09-16 15:50:57','danr','2022-09-16 15:56:47','danr',1,2),(31,'2022-09-16 16:36:07','danr','2022-09-16 17:03:45','danr',1,2),(32,'2022-09-17 12:58:27','danr','2022-09-17 13:16:17','danr',1,2),(33,'2022-09-17 13:20:25','danr','2022-09-17 13:23:32','danr',1,2),(34,'2022-09-17 13:23:40','danr','2022-09-17 13:24:10','danr',1,2),(35,'2022-09-17 14:10:09','danr',NULL,NULL,1,2),(36,'2022-09-17 14:34:11','danr','2022-09-17 14:54:19','danr',1,2),(37,'2022-09-17 17:21:10','danr','2022-09-17 17:22:29','danr',1,2),(38,'2022-09-17 19:36:32','danr','2022-09-17 19:46:08','danr',1,2),(39,'2022-09-18 09:10:11','danr',NULL,NULL,1,2),(40,'2022-09-18 09:32:49','danr','2022-09-18 09:49:42','danr',1,2),(41,'2022-09-18 16:13:44','danr','2022-09-18 16:41:21','danr',1,2),(42,'2022-09-19 21:05:02','danr','2022-09-19 21:11:03','danr',1,2),(43,'2022-09-19 21:12:22','danr','2022-09-19 21:17:28','danr',1,2),(44,'2022-09-20 20:02:26','danr','2022-09-20 20:04:40','danr',1,2),(45,'2022-09-20 20:04:52','danr','2022-09-20 20:13:58','danr',1,2),(46,'2022-09-23 20:27:04','danr','2022-09-23 20:43:20','danr',1,2),(47,'2022-09-24 12:22:19','danr','2022-09-24 12:22:29','danr',1,2),(48,'2022-09-24 12:22:35','danr','2022-09-24 12:22:45','danr',1,2),(49,'2022-09-24 13:19:12','danr',NULL,NULL,1,2),(50,'2022-09-24 13:40:37','danr',NULL,NULL,1,2),(51,'2022-09-24 13:59:31','danr',NULL,NULL,1,2),(52,'2022-09-24 14:10:33','danr',NULL,NULL,1,2),(53,'2022-09-24 14:22:51','danr',NULL,NULL,1,2),(54,'2022-09-24 14:50:01','danr',NULL,NULL,1,2),(55,'2022-09-24 15:13:20','danr',NULL,NULL,1,2),(56,'2022-09-24 15:43:43','danr','2022-09-24 16:14:42','danr',1,2),(57,'2022-09-24 19:18:00','danr',NULL,NULL,1,2),(58,'2022-09-24 19:41:07','danr','2022-09-24 19:49:01','danr',1,2),(59,'2022-09-24 19:54:53','danr',NULL,NULL,1,2),(60,'2022-09-24 20:19:40','danr','2022-09-24 20:33:43','danr',1,2),(61,'2022-09-24 20:34:16','danr','2022-09-24 20:45:14','danr',1,2),(62,'2022-09-24 20:50:40','danr','2022-09-24 20:54:14','danr',1,2),(63,'2022-09-24 20:56:51','danr',NULL,NULL,1,2),(64,'2022-09-24 21:14:58','danr','2022-09-24 21:19:06','danr',1,2),(65,'2022-09-24 21:19:33','danr',NULL,NULL,1,2),(66,'2022-09-24 21:21:36','danr','2022-09-24 21:24:52','danr',1,2),(67,'2022-09-25 10:51:11','danr',NULL,NULL,1,2),(68,'2022-09-25 10:58:24','danr',NULL,NULL,1,2),(69,'2022-09-25 11:43:51','danr','2022-09-25 11:56:38','danr',1,2),(70,'2022-09-28 20:54:39','danr',NULL,NULL,1,2),(71,'2022-09-28 21:00:46','danr',NULL,NULL,1,2),(72,'2022-09-29 05:39:09','danr',NULL,NULL,1,2),(73,'2022-09-29 05:45:41','danr',NULL,NULL,1,2),(74,'2022-09-29 05:50:08','danr',NULL,NULL,1,2),(75,'2022-09-29 05:58:03','danr',NULL,NULL,1,2),(76,'2022-09-29 06:00:52','danr',NULL,NULL,1,2),(77,'2022-09-29 06:04:31','danr',NULL,NULL,1,2),(78,'2022-09-29 17:34:53','danr',NULL,NULL,1,2),(79,'2022-09-29 17:42:53','danr',NULL,NULL,1,2),(80,'2022-09-29 17:49:59','danr',NULL,NULL,1,2),(81,'2022-09-29 18:10:28','danr',NULL,NULL,1,2),(82,'2022-09-29 18:13:57','danr',NULL,NULL,1,2),(83,'2022-09-29 18:20:00','danr',NULL,NULL,1,2),(84,'2022-09-29 18:32:25','danr',NULL,NULL,1,2),(85,'2022-09-29 18:44:37','danr',NULL,NULL,1,2),(86,'2022-09-29 18:59:47','danr',NULL,NULL,1,2),(87,'2022-09-29 19:11:21','danr','2022-09-29 19:12:07','danr',1,2),(88,'2022-09-29 19:13:11','danr',NULL,NULL,1,2),(89,'2022-09-29 19:33:16','danr','2022-09-29 19:34:58','danr',1,2),(90,'2022-09-29 19:37:29','danr',NULL,NULL,1,2),(91,'2022-09-29 19:43:51','danr','2022-09-29 19:44:34','danr',1,2),(92,'2022-09-29 19:50:03','danr','2022-09-29 19:52:44','danr',1,2),(93,'2022-09-29 19:56:00','danr','2022-09-29 19:58:55','danr',1,2),(94,'2022-09-29 20:00:09','danr','2022-09-29 20:05:19','danr',1,2),(95,'2022-09-29 20:14:00','danr','2022-09-29 20:18:19','danr',1,2),(96,'2022-09-29 20:19:13','danr','2022-09-29 20:20:41','danr',1,2),(97,'2022-09-29 20:24:22','danr',NULL,NULL,1,2),(98,'2022-09-29 20:26:30','danr','2022-09-29 20:32:34','danr',1,2),(99,'2022-09-29 21:29:56','danr',NULL,NULL,1,2),(100,'2022-09-29 21:34:07','danr','2022-09-29 21:38:46','danr',1,2),(101,'2022-09-29 21:39:34','danr','2022-09-29 21:40:49','danr',1,2),(102,'2022-09-29 21:41:38','danr','2022-09-29 21:42:45','danr',1,2),(103,'2022-09-29 21:43:19','danr','2022-09-29 21:45:09','danr',1,2),(104,'2022-09-29 21:45:56','danr','2022-09-29 21:46:30','danr',1,2),(105,'2022-09-29 21:47:03','danr','2022-09-29 21:47:36','danr',1,2),(106,'2022-09-29 21:48:11','danr',NULL,NULL,1,2),(107,'2022-09-29 21:59:48','danr',NULL,NULL,1,2),(108,'2022-09-30 05:54:26','danr','2022-09-30 05:57:01','danr',1,2),(109,'2022-09-30 05:58:27','danr','2022-09-30 06:05:04','danr',1,2),(110,'2022-09-30 06:05:42','danr','2022-09-30 06:06:38','danr',1,2),(111,'2022-09-30 06:07:16','danr','2022-09-30 06:07:54','danr',1,2),(112,'2022-09-30 18:25:07','danr','2022-09-30 18:30:30','danr',1,2),(113,'2022-09-30 18:36:47','danr','2022-09-30 18:38:13','danr',1,2),(114,'2022-09-30 18:39:09','danr','2022-09-30 18:40:47','danr',1,2),(115,'2022-09-30 18:43:26','danr','2022-09-30 18:48:29','danr',1,2),(116,'2022-09-30 18:57:02','danr','2022-09-30 19:00:53','danr',1,2),(117,'2022-09-30 19:01:36','danr','2022-09-30 19:04:15','danr',1,2),(118,'2022-09-30 19:05:13','danr','2022-09-30 19:12:39','danr',1,2),(119,'2022-09-30 19:13:51','danr',NULL,NULL,1,2),(120,'2022-09-30 19:20:09','danr','2022-09-30 19:33:36','danr',1,2),(121,'2022-09-30 19:34:12','danr','2022-09-30 19:36:48','danr',1,2),(122,'2022-09-30 19:40:12','danr','2022-09-30 19:40:57','danr',1,2),(123,'2022-09-30 19:45:27','danr','2022-09-30 19:52:50','danr',1,2),(124,'2022-09-30 19:53:59','danr','2022-09-30 19:54:46','danr',1,2),(125,'2022-10-09 11:17:18','danr',NULL,NULL,1,2),(126,'2022-10-09 11:39:05','danr',NULL,NULL,1,2),(127,'2022-10-09 11:39:05','danr',NULL,NULL,1,2),(128,'2022-10-09 11:55:13','danr',NULL,NULL,1,2),(129,'2022-10-09 12:13:14','danr',NULL,NULL,1,2),(130,'2022-10-09 12:30:58','danr',NULL,NULL,1,2),(131,'2022-10-09 12:34:25','danr',NULL,NULL,1,2),(132,'2022-10-09 13:29:12','danr','2022-10-09 13:44:09','danr',1,2),(133,'2022-10-09 15:58:59','danr',NULL,NULL,1,2),(134,'2022-10-09 16:18:07','danr','2022-10-09 16:27:20','danr',1,2),(135,'2022-10-09 20:21:40','danr','2022-10-09 20:28:06','danr',1,2),(136,'2022-10-16 16:13:09','danr','2022-10-16 16:19:11','danr',1,2),(137,'2022-10-16 19:30:33','danr','2022-10-16 19:44:54','danr',1,2),(138,'2022-10-16 19:45:04','danr','2022-10-16 19:45:21','danr',1,2),(139,'2022-10-16 19:46:12','danr','2022-10-16 19:46:49','danr',1,2),(140,'2022-10-22 19:32:04','danr','2022-10-22 19:36:06','danr',1,2),(141,'2022-10-22 19:36:54','danr','2022-10-22 19:40:53','danr',1,2),(142,'2022-10-22 19:41:17','danr','2022-10-22 20:07:31','danr',1,2),(143,'2022-10-22 20:08:01','danr','2022-10-22 20:11:13','danr',1,2),(144,'2022-10-22 20:11:54','danr','2022-10-22 20:12:20','danr',1,2),(145,'2022-10-23 13:12:08','danr','2022-10-23 13:14:12','danr',1,2),(146,'2022-10-23 13:14:42','danr',NULL,NULL,1,2),(147,'2022-10-23 13:29:56','danr','2022-10-23 13:36:33','danr',1,2),(148,'2022-10-23 13:56:06','danr','2022-10-23 13:58:47','danr',1,2),(149,'2022-10-28 10:26:15','danr','2022-10-28 10:33:31','danr',1,2),(150,'2022-10-28 10:33:38','danr','2022-10-28 10:34:55','danr',1,2),(151,'2022-10-28 10:40:30','danr','2022-10-28 10:40:44','danr',1,2),(152,'2022-10-28 10:42:19','danr','2022-10-28 10:47:38','danr',1,2),(153,'2022-10-28 10:50:20','danr','2022-10-28 10:57:15','danr',1,2),(154,'2022-10-28 10:58:59','danr','2022-10-28 11:00:51','danr',1,2),(155,'2022-10-28 11:02:38','danr','2022-10-28 11:03:40','danr',1,2),(156,'2022-10-28 11:05:23','danr','2022-10-28 11:07:50','danr',1,2),(157,'2022-10-28 11:11:07','danr',NULL,NULL,1,2),(158,'2022-10-28 11:17:38','danr',NULL,NULL,1,2),(159,'2022-10-28 11:25:20','danr',NULL,NULL,1,2),(160,'2022-10-28 11:31:08','danr',NULL,NULL,1,2),(161,'2022-10-28 11:31:08','danr',NULL,NULL,1,2),(162,'2022-10-28 11:43:51','danr',NULL,NULL,1,2),(163,'2022-10-28 12:13:15','danr','2022-10-28 12:15:20','danr',1,2),(164,'2022-10-28 12:15:28','danr',NULL,NULL,1,2),(165,'2022-10-28 12:21:10','danr','2022-10-28 12:30:43','danr',1,2),(166,'2022-10-28 12:34:46','danr','2022-10-28 12:42:59','danr',1,2),(167,'2022-10-28 12:47:55','danr','2022-10-28 12:49:02','danr',1,2),(168,'2022-10-28 12:49:22','danr','2022-10-28 12:50:40','danr',1,2),(169,'2022-10-28 12:57:10','danr',NULL,NULL,1,2),(170,'2022-10-28 15:36:07','danr','2022-10-28 15:47:40','danr',1,2),(171,'2022-10-28 15:48:08','danr','2022-10-28 15:51:22','danr',1,2),(172,'2022-10-28 15:52:03','danr','2022-10-28 16:12:03','danr',1,2),(173,'2022-10-28 16:13:25','danr','2022-10-28 16:19:06','danr',1,2),(174,'2022-10-28 16:19:34','danr','2022-10-28 16:24:15','danr',1,2),(175,'2022-10-28 16:29:35','danr','2022-10-28 16:30:51','danr',1,2),(176,'2022-10-28 16:30:56','danr','2022-10-28 16:39:09','danr',1,2),(177,'2022-10-28 16:39:27','danr','2022-10-28 17:01:03','danr',1,2),(178,'2022-10-28 17:10:41','danr','2022-10-28 17:11:38','danr',1,2),(179,'2022-10-28 17:13:41','danr','2022-10-28 17:16:59','danr',1,2),(180,'2022-10-28 17:19:18','danr','2022-10-28 17:31:04','danr',1,2),(181,'2022-10-28 17:43:06','danr','2022-10-28 17:46:40','danr',1,2),(182,'2022-10-28 17:47:47','danr',NULL,NULL,1,2),(183,'2022-10-28 18:00:18','danr','2022-10-28 18:02:37','danr',1,2),(184,'2022-10-28 18:53:15','danr','2022-10-28 18:57:25','danr',1,2),(185,'2022-10-28 19:05:56','danr','2022-10-28 19:17:39','danr',1,2),(186,'2022-10-28 19:32:19','danr','2022-10-28 19:40:53','danr',1,2),(187,'2022-10-28 21:00:46','danr','2022-10-28 21:10:32','danr',1,2),(188,'2022-10-28 21:21:02','danr','2022-10-28 21:31:11','danr',1,2),(189,'2022-10-28 21:31:35','danr','2022-10-28 21:35:29','danr',1,2),(190,'2022-10-28 21:37:21','danr','2022-10-28 21:38:58','danr',1,2),(191,'2022-10-28 21:43:08','danr','2022-10-28 21:44:20','danr',1,2),(192,'2022-10-28 22:02:52','danr','2022-10-28 22:04:09','danr',1,2),(193,'2022-10-28 22:05:55','danr','2022-10-28 22:06:46','danr',1,2),(194,'2022-10-29 08:07:19','danr',NULL,NULL,1,2),(195,'2022-10-29 08:07:19','danr',NULL,NULL,1,2),(196,'2022-10-29 08:30:22','danr',NULL,NULL,1,2),(197,'2022-10-29 08:45:15','danr',NULL,NULL,1,2),(198,'2022-10-29 09:07:37','danr','2022-10-29 09:24:36','danr',1,2),(199,'2022-10-29 09:40:06','danr','2022-10-29 09:41:16','danr',1,2),(200,'2022-10-29 09:41:22','danr','2022-10-29 09:42:07','danr',1,2),(201,'2022-10-29 09:43:08','danr',NULL,NULL,1,2),(202,'2022-10-29 10:08:41','danr',NULL,NULL,1,2),(203,'2022-10-29 11:15:29','danr','2022-10-29 11:27:54','danr',1,2),(204,'2022-10-29 11:28:47','danr','2022-10-29 11:33:12','danr',1,2),(205,'2022-10-29 11:33:55','danr','2022-10-29 11:35:13','danr',1,2),(206,'2022-10-29 11:37:25','danr','2022-10-29 11:55:00','danr',1,2),(207,'2022-10-29 11:55:42','danr','2022-10-29 12:05:07','danr',1,2),(208,'2022-10-29 12:07:31','danr','2022-10-29 12:17:02','danr',1,2),(209,'2022-10-29 12:28:25','danr','2022-10-29 12:41:01','danr',1,2),(210,'2022-10-29 17:44:58','danr','2022-10-29 18:07:25','danr',1,2),(211,'2022-10-29 18:26:55','danr','2022-10-29 18:31:58','danr',1,2),(212,'2022-10-29 18:39:45','danr','2022-10-29 18:41:58','danr',1,2),(213,'2022-10-29 18:56:26','danr','2022-10-29 18:57:54','danr',1,2),(214,'2022-10-29 18:59:36','danr','2022-10-29 19:00:20','danr',1,2),(215,'2022-10-29 19:33:49','danr','2022-10-29 19:34:43','danr',1,2),(216,'2022-10-29 19:38:33','danr','2022-10-29 19:40:38','danr',1,2),(217,'2022-10-29 19:50:59','danr','2022-10-29 19:52:59','danr',1,2),(218,'2022-10-29 20:00:59','danr','2022-10-29 20:04:55','danr',1,2),(219,'2022-10-29 20:09:15','danr','2022-10-29 20:16:23','danr',1,2),(220,'2022-10-29 20:41:05','danr','2022-10-29 20:42:25','danr',1,2),(221,'2022-10-29 22:27:04','danr','2022-10-29 22:28:14','danr',1,2),(222,'2022-10-29 22:29:52','danr','2022-10-29 22:30:34','danr',1,2),(223,'2022-10-29 22:31:34','danr','2022-10-29 22:53:29','danr',1,2),(224,'2022-10-30 09:26:40','danr',NULL,NULL,1,2),(225,'2022-10-30 09:57:10','danr','2022-10-30 10:13:06','danr',1,2),(226,'2022-10-30 10:13:15','danr',NULL,NULL,1,2),(227,'2022-10-30 10:16:25','danr',NULL,NULL,1,2),(228,'2022-10-30 10:42:34','danr','2022-10-30 10:58:00','danr',1,2),(229,'2022-10-30 11:11:45','danr','2022-10-30 11:22:38','danr',1,2),(230,'2022-10-30 11:27:18','danr','2022-10-30 11:40:23','danr',1,2),(231,'2022-10-30 12:35:11','danr',NULL,NULL,1,2),(232,'2022-10-30 13:07:14','danr','2022-10-30 13:25:33','danr',1,2),(233,'2022-10-30 14:27:12','danr',NULL,NULL,1,2),(234,'2022-10-30 15:03:21','danr',NULL,NULL,1,2),(235,'2022-10-30 15:33:41','danr',NULL,NULL,1,2),(236,'2022-11-05 15:41:02','danr',NULL,NULL,1,2),(237,'2022-11-05 15:57:28','danr',NULL,NULL,1,2),(238,'2022-11-05 16:14:50','danr','2022-11-05 16:24:19','danr',1,2),(239,'2022-11-06 11:31:27','danr','2022-11-06 11:39:24','danr',1,2),(240,'2022-11-26 12:06:52','danr','2022-11-26 12:09:12','danr',1,2),(241,'2022-11-26 16:06:00','danr',NULL,NULL,1,2),(242,'2022-11-26 16:14:09','danr','2022-11-26 16:15:05','danr',1,2),(243,'2022-11-26 16:17:02','danr','2022-11-26 16:26:59','danr',1,2),(244,'2022-11-26 19:40:03','danr','2022-11-26 19:53:17','danr',1,2),(245,'2022-11-26 20:28:41','danr','2022-11-26 20:31:25','danr',1,2),(246,'2022-11-26 20:35:29','danr','2022-11-26 20:41:20','danr',1,2),(247,'2022-11-26 20:43:15','danr','2022-11-26 20:52:25','danr',1,2),(248,'2022-11-26 20:54:30','danr','2022-11-26 20:58:01','danr',1,2),(249,'2022-11-26 21:02:38','danr','2022-11-26 21:09:31','danr',1,2),(250,'2022-11-26 21:12:50','danr','2022-11-26 21:16:34','danr',1,2),(251,'2022-11-26 21:23:49','danr','2022-11-26 21:27:04','danr',1,2),(252,'2022-11-26 21:28:15','danr','2022-11-26 21:32:19','danr',1,2),(253,'2022-11-26 21:34:42','danr','2022-11-26 21:36:36','danr',1,2),(254,'2022-11-26 21:38:52','danr','2022-11-26 21:53:39','danr',1,2),(255,'2022-11-26 22:53:51','danr','2022-11-26 22:55:27','danr',1,2),(256,'2022-11-26 23:25:32','danr','2022-11-26 23:50:26','danr',1,2),(257,'2022-11-27 16:10:43','danr','2022-11-27 16:16:40','danr',1,2),(258,'2022-11-27 16:32:53','danr','2022-11-27 16:40:00','danr',1,2),(259,'2022-11-27 16:44:15','danr',NULL,NULL,1,2),(260,'2022-11-27 17:02:28','danr','2022-11-27 17:16:06','danr',1,2),(261,'2022-11-27 17:16:12','danr','2022-11-27 17:18:18','danr',1,2),(262,'2022-11-27 17:54:21','danr','2022-11-27 17:59:17','danr',1,2),(263,'2022-11-27 18:00:46','danr','2022-11-27 18:01:52','danr',1,2),(264,'2022-11-27 18:05:46','danr','2022-11-27 18:08:01','danr',1,2),(265,'2022-11-27 18:27:21','danr',NULL,NULL,1,2),(266,'2022-11-27 18:32:14','danr','2022-11-27 18:32:58','danr',1,2),(267,'2022-11-27 18:33:57','danr','2022-11-27 18:34:27','danr',1,2),(268,'2022-11-27 18:37:11','danr','2022-11-27 18:42:08','danr',1,2),(269,'2022-11-27 18:47:00','danr','2022-11-27 18:48:55','danr',1,2),(270,'2022-11-27 18:52:10','danr','2022-11-27 18:59:53','danr',1,2),(271,'2022-12-01 11:04:44','danr','2022-12-01 11:06:40','danr',1,2),(272,'2022-12-01 11:48:43','danr','2022-12-01 11:54:59','danr',1,2),(273,'2022-12-01 11:55:51','danr',NULL,NULL,1,2),(274,'2022-12-01 11:58:40','danr','2022-12-01 12:01:52','danr',1,2),(275,'2022-12-01 12:06:40','danr','2022-12-01 12:09:32','danr',1,2),(276,'2022-12-01 12:14:02','danr','2022-12-01 12:26:35','danr',1,2),(277,'2022-12-01 12:33:14','danr','2022-12-01 12:35:34','danr',1,2),(278,'2022-12-01 12:36:57','danr','2022-12-01 12:38:12','danr',1,2),(279,'2022-12-01 12:42:47','danr',NULL,NULL,1,2),(280,'2022-12-01 14:12:31','danr',NULL,NULL,1,2),(281,'2022-12-08 09:46:32','danr',NULL,NULL,1,2),(282,'2022-12-11 11:46:56','danr','2022-12-11 11:48:32','danr',1,2),(283,'2022-12-11 11:50:15','danr','2022-12-11 11:54:47','danr',1,2),(284,'2022-12-11 11:56:02','danr','2022-12-11 11:58:03','danr',1,2),(285,'2022-12-11 11:59:56','danr','2022-12-11 12:01:45','danr',1,2),(286,'2022-12-11 12:02:28','danr','2022-12-11 12:04:34','danr',1,2),(287,'2022-12-11 12:06:32','danr','2022-12-11 12:09:25','danr',1,2),(288,'2022-12-11 13:33:55','danr','2022-12-11 13:39:50','danr',1,2),(289,'2022-12-11 13:41:37','danr','2022-12-11 13:43:21','danr',1,2),(290,'2022-12-11 13:46:26','danr','2022-12-11 13:48:24','danr',1,2),(291,'2023-01-02 18:08:33','danr','2023-01-02 18:12:05','danr',1,2),(292,'2023-01-02 18:24:22','danr','2023-01-02 18:25:37','danr',1,2),(293,'2023-01-02 18:30:20','danr','2023-01-02 18:36:30','danr',1,2),(294,'2023-01-05 14:45:05','danr','2023-01-05 14:46:10','danr',1,2),(295,'2023-01-05 14:50:27','danr','2023-01-05 14:50:52','danr',1,2),(296,'2023-01-16 17:44:48','danr','2023-01-16 17:49:42','danr',1,2),(297,'2023-01-16 19:15:57','danr','2023-01-16 19:16:02','danr',1,2),(298,'2023-01-16 20:07:57','danr',NULL,NULL,1,2),(299,'2023-01-29 08:45:13','danr','2023-01-29 08:47:12','danr',1,2),(300,'2023-01-29 09:34:24','danr','2023-01-29 09:36:03','danr',1,2),(301,'2023-04-08 16:42:15','danr',NULL,NULL,1,2),(302,'2023-04-30 15:33:18','danr','2023-04-30 15:36:33','danr',1,2),(303,'2023-04-30 15:39:00','danr','2023-04-30 15:47:26','danr',1,2),(304,'2023-04-30 15:52:36','danr','2023-04-30 15:58:37','danr',1,2),(305,'2023-04-30 15:59:32','danr','2023-04-30 16:17:42','danr',1,2),(306,'2023-04-30 16:21:52','danr','2023-04-30 16:23:17','danr',1,2),(307,'2023-04-30 16:24:53','danr','2023-04-30 16:40:52','danr',1,2),(308,'2023-04-30 16:41:26','danr','2023-04-30 16:47:14','danr',1,2),(309,'2023-04-30 16:47:18','danr','2023-04-30 17:08:36','danr',1,2),(310,'2023-05-01 11:43:55','danr',NULL,NULL,1,2),(311,'2023-05-01 12:19:34','danr','2023-05-01 12:28:09','danr',1,2),(312,'2023-05-01 15:23:58','danr','2023-05-01 15:27:22','danr',1,2),(313,'2023-05-06 11:21:56','danr','2023-05-06 11:42:16','danr',1,2),(314,'2023-05-06 14:34:00','danr','2023-05-06 14:36:58','danr',1,2),(315,'2023-05-06 15:02:59','danr','2023-05-06 15:03:45','danr',1,2),(316,'2023-05-07 18:47:37','danr','2023-05-07 19:15:54','danr',1,2),(317,'2023-05-07 19:20:56','danr','2023-05-07 19:21:34','danr',1,2),(318,'2023-05-07 19:22:26','danr','2023-05-07 19:51:04','danr',1,2),(319,'2023-05-13 19:06:17','danr','2023-05-13 19:20:14','danr',1,2),(320,'2023-05-20 18:29:41','danr','2023-05-20 18:31:24','danr',1,2),(321,'2023-05-20 18:58:53','danr',NULL,NULL,1,2),(322,'2023-05-20 19:56:11','danr','2023-05-20 19:56:31','danr',1,2),(323,'2023-05-20 20:07:58','danr',NULL,NULL,1,2),(324,'2023-05-20 20:50:34','danr','2023-05-20 21:06:49','danr',1,2),(325,'2023-05-22 20:44:47','danr','2023-05-22 21:00:07','danr',1,2),(326,'2023-05-22 21:06:37','danr','2023-05-22 21:09:20','danr',1,2),(327,'2023-05-24 06:19:47','danr','2023-05-24 06:20:59','danr',1,2),(328,'2023-05-24 06:22:19','danr','2023-05-24 06:28:35','danr',1,2),(329,'2023-05-27 10:35:51','danr','2023-05-27 10:40:01','danr',1,2),(330,'2023-05-27 18:52:35','danr','2023-05-27 18:52:42','danr',1,2),(331,'2023-05-27 18:53:09','danr','2023-05-27 18:53:12','danr',1,2),(332,'2023-05-27 18:58:57','danr',NULL,NULL,1,2),(333,'2023-05-27 19:52:17','danr',NULL,NULL,1,2),(334,'2023-05-27 19:53:11','danr','2023-05-27 19:53:20','danr',1,2),(335,'2023-05-27 19:53:24','danr','2023-05-27 19:53:28','danr',1,2),(336,'2023-05-27 19:53:39','danr','2023-05-27 19:53:43','danr',1,2),(337,'2023-05-28 13:31:17','LG',NULL,NULL,2,2),(338,'2023-05-28 13:45:31','danr','2023-05-28 13:46:18','danr',1,2),(339,'2023-05-28 14:17:26','danr',NULL,NULL,1,2),(340,'2023-05-28 14:21:16','danr','2023-05-28 14:23:16','danr',1,2),(341,'2023-05-28 14:26:20','danr',NULL,NULL,1,2),(342,'2023-05-28 14:26:48','danr',NULL,NULL,1,2),(343,'2023-05-28 15:07:30','danr',NULL,NULL,1,2),(344,'2023-05-28 15:08:12','danr','2023-05-28 15:09:42','danr',1,2),(345,'2023-05-28 16:53:25','danr',NULL,NULL,1,2),(346,'2023-05-28 16:54:10','danr','2023-05-28 16:54:52','danr',1,2),(347,'2023-05-28 16:55:34','danr',NULL,NULL,1,2),(348,'2023-05-28 16:55:34','danr',NULL,NULL,1,2),(349,'2023-05-28 16:56:00','danr','2023-05-28 16:59:18','danr',1,2),(350,'2023-05-28 17:01:32','danr',NULL,NULL,1,2),(351,'2023-05-28 17:01:51','danr','2023-05-28 17:05:31','danr',1,2),(352,'2023-05-28 17:09:58','danr',NULL,NULL,1,2),(353,'2023-05-28 17:09:58','danr',NULL,NULL,1,2),(354,'2023-05-28 17:10:22','danr','2023-05-28 17:12:53','danr',1,2),(355,'2023-05-28 17:18:52','danr',NULL,NULL,1,2),(356,'2023-05-28 17:21:23','danr','2023-05-28 17:23:04','danr',1,2),(357,'2023-05-28 17:26:35','danr',NULL,NULL,1,2),(358,'2023-05-28 17:27:05','danr','2023-05-28 17:44:21','danr',1,2),(359,'2023-05-28 17:52:07','danr',NULL,NULL,1,2),(360,'2023-05-28 17:52:21','danr','2023-05-28 18:21:32','danr',1,2),(361,'2023-05-28 18:25:39','danr',NULL,NULL,1,2),(362,'2023-05-28 18:25:51','danr','2023-05-28 18:41:26','danr',1,2),(363,'2023-05-28 18:48:28','danr',NULL,NULL,1,2),(364,'2023-05-28 18:48:41','danr','2023-05-28 19:09:51','danr',1,2),(365,'2023-05-30 07:57:09','danr',NULL,NULL,1,2),(366,'2023-05-30 07:57:22','danr',NULL,NULL,1,2),(367,'2023-05-30 07:57:47','danr',NULL,NULL,1,2),(368,'2023-05-30 07:58:23','danr',NULL,NULL,1,2),(369,'2023-05-30 08:00:16','danr',NULL,NULL,1,2),(370,'2023-05-30 08:00:49','danr',NULL,NULL,1,2),(371,'2023-05-30 08:02:15','danr',NULL,NULL,1,2),(372,'2023-05-30 08:17:34','danr','2023-05-30 08:27:23','danr',1,2),(373,'2023-05-30 08:32:57','danr',NULL,NULL,1,2),(374,'2023-05-30 08:34:24','danr','2023-05-30 08:35:31','danr',1,2),(375,'2023-05-30 08:37:27','danr',NULL,NULL,1,2),(376,'2023-05-30 08:38:33','danr',NULL,NULL,1,2),(377,'2023-05-30 08:39:33','danr','2023-05-30 09:38:27','danr',1,2),(378,'2023-05-30 09:43:54','danr',NULL,NULL,1,2),(379,'2023-05-30 09:44:16','danr','2023-05-30 09:51:31','danr',1,2),(380,'2023-05-30 10:43:01','danr',NULL,NULL,1,2),(381,'2023-05-30 10:44:00','danr',NULL,NULL,1,2),(382,'2023-05-30 10:44:55','danr','2023-05-30 10:46:19','danr',1,2),(383,'2023-05-30 10:53:11','danr',NULL,NULL,1,2),(384,'2023-05-30 11:18:58','danr',NULL,NULL,1,2),(385,'2023-05-30 11:19:55','danr',NULL,NULL,1,2),(386,'2023-05-30 11:20:17','danr',NULL,NULL,1,2),(387,'2023-05-30 11:20:47','danr','2023-05-30 11:31:08','danr',1,2),(388,'2023-05-30 11:32:29','danr',NULL,NULL,1,2),(389,'2023-05-30 11:32:29','danr',NULL,NULL,1,2),(390,'2023-05-30 11:32:49','danr','2023-05-30 11:35:29','danr',1,2),(391,'2023-05-30 12:00:51','danr',NULL,NULL,1,2),(392,'2023-05-30 12:02:41','danr','2023-05-30 12:10:48','danr',1,2),(393,'2023-06-01 16:58:07','danr',NULL,NULL,1,2),(394,'2023-06-01 17:01:21','danr','2023-06-01 17:36:49','danr',1,2),(395,'2023-06-01 17:39:21','danr',NULL,NULL,1,2),(396,'2023-06-01 17:39:54','danr','2023-06-01 18:12:07','danr',1,2),(397,'2023-06-03 13:09:13','danr',NULL,NULL,1,2),(398,'2023-06-03 13:10:14','danr','2023-06-03 13:16:04','danr',1,2),(399,'2023-06-03 13:47:21','danr',NULL,NULL,1,2),(400,'2023-06-03 13:47:55','danr','2023-06-03 13:55:34','danr',1,2),(401,'2023-06-03 15:05:08','danr',NULL,NULL,1,2),(402,'2023-06-03 15:05:08','danr',NULL,NULL,1,2),(403,'2023-06-03 15:05:08','danr',NULL,NULL,1,2),(404,'2023-06-03 15:05:36','danr',NULL,NULL,1,2),(405,'2023-06-03 15:05:51','danr',NULL,NULL,1,2),(406,'2023-06-03 15:06:12','danr',NULL,NULL,1,2),(407,'2023-06-03 15:07:29','danr',NULL,NULL,1,2),(408,'2023-06-03 15:08:21','danr',NULL,NULL,1,2),(409,'2023-06-03 15:08:47','danr',NULL,NULL,1,2),(410,'2023-06-03 15:12:11','danr',NULL,NULL,1,2),(411,'2023-06-03 15:13:49','danr',NULL,NULL,1,2),(412,'2023-06-03 15:15:50','danr',NULL,NULL,1,2),(413,'2023-06-03 15:16:18','danr',NULL,NULL,1,2),(414,'2023-06-03 15:16:58','danr',NULL,NULL,1,2),(415,'2023-06-03 15:25:52','danr',NULL,NULL,1,2),(416,'2023-06-03 15:28:37','danr',NULL,NULL,1,2),(417,'2023-06-03 15:30:16','danr',NULL,NULL,1,2),(418,'2023-06-03 15:35:30','danr',NULL,NULL,1,2),(419,'2023-06-03 15:40:09','danr','2023-06-03 15:40:32','danr',1,2),(420,'2023-06-03 15:42:55','danr',NULL,NULL,1,2),(421,'2023-06-03 15:43:55','danr',NULL,NULL,1,2),(422,'2023-06-03 16:06:52','danr',NULL,NULL,1,2),(423,'2023-06-03 16:06:52','danr',NULL,NULL,1,2),(424,'2023-06-03 16:19:01','danr',NULL,NULL,1,2),(425,'2023-06-03 16:19:43','danr',NULL,NULL,1,2),(426,'2023-06-03 16:34:43','danr',NULL,NULL,1,2),(427,'2023-06-03 16:35:41','danr','2023-06-03 16:40:54','danr',1,2),(428,'2023-06-03 17:48:06','danr',NULL,NULL,1,2),(429,'2023-06-03 17:48:33','danr','2023-06-03 17:53:36','danr',1,2),(430,'2023-06-04 06:51:33','danr',NULL,NULL,1,2),(431,'2023-06-04 06:52:30','danr',NULL,NULL,1,2),(432,'2023-06-04 07:14:35','danr',NULL,NULL,1,2),(433,'2023-06-04 07:15:26','danr','2023-06-04 07:24:42','danr',1,2),(434,'2023-06-04 07:25:55','danr',NULL,NULL,1,2),(435,'2023-06-04 07:26:17','danr',NULL,NULL,1,2),(436,'2023-06-04 07:27:12','danr',NULL,NULL,1,2),(437,'2023-06-04 07:28:13','danr','2023-06-04 07:34:19','danr',1,2),(438,'2023-06-04 07:41:24','danr',NULL,NULL,1,2),(439,'2023-06-04 07:42:20','danr',NULL,NULL,1,2),(440,'2023-06-05 05:58:37','danr',NULL,NULL,1,2),(441,'2023-06-05 06:00:24','danr','2023-06-05 06:04:24','danr',1,2),(442,'2023-06-07 08:16:33','danr',NULL,NULL,1,2),(443,'2023-06-07 08:17:20','danr',NULL,NULL,1,2),(444,'2023-06-07 08:19:55','danr',NULL,NULL,1,2),(445,'2023-06-07 08:19:55','danr',NULL,NULL,1,2),(446,'2023-06-07 08:20:51','danr',NULL,NULL,1,2),(447,'2023-06-07 08:21:24','danr',NULL,NULL,1,2),(448,'2023-06-07 08:22:35','danr',NULL,NULL,1,2),(449,'2023-06-07 09:00:59','danr',NULL,NULL,1,2),(450,'2023-06-07 09:00:58','danr',NULL,NULL,1,2),(451,'2023-06-07 09:02:23','danr',NULL,NULL,1,2),(452,'2023-06-07 12:35:11','danr',NULL,NULL,1,2),(453,'2023-06-07 12:35:08','danr',NULL,NULL,1,2),(454,'2023-06-07 12:36:12','danr',NULL,NULL,1,2),(455,'2023-06-07 12:37:13','danr','2023-06-07 12:58:31','danr',1,2),(456,'2023-06-07 20:42:58','danr',NULL,NULL,1,2),(457,'2023-06-07 20:43:56','danr','2023-06-07 20:58:37','danr',1,2),(458,'2023-06-09 06:34:06','danr',NULL,NULL,1,2),(459,'2023-06-09 06:34:55','danr','2023-06-09 06:48:45','danr',1,2);
/*!40000 ALTER TABLE `usuario_sesion` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-09  6:49:18
