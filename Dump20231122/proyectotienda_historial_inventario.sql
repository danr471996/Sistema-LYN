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
-- Table structure for table `historial_inventario`
--

DROP TABLE IF EXISTS `historial_inventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historial_inventario` (
  `idHistorial_inventario` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Idproducto` int NOT NULL,
  `Tipo_movimiento` int NOT NULL,
  `Iddepartamento` int NOT NULL,
  `Cantidad_actual` int NOT NULL,
  `Cantidad_anterior` int NOT NULL,
  `Estado` int NOT NULL COMMENT '1- Activo\n2- Inactivo',
  PRIMARY KEY (`idHistorial_inventario`),
  KEY `fk_idproducto_idx` (`Idproducto`),
  KEY `fk_tip_movi_idx` (`Tipo_movimiento`),
  KEY `fk_iddepartamento_idx` (`Iddepartamento`),
  CONSTRAINT `fk_iddepartamento` FOREIGN KEY (`Iddepartamento`) REFERENCES `departamento` (`Iddepartmento`),
  CONSTRAINT `fk_idproducto` FOREIGN KEY (`Idproducto`) REFERENCES `productos` (`Idproducto`),
  CONSTRAINT `fk_tip_movi` FOREIGN KEY (`Tipo_movimiento`) REFERENCES `tipo_movimento` (`IdTipo_movimiento`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historial_inventario`
--

LOCK TABLES `historial_inventario` WRITE;
/*!40000 ALTER TABLE `historial_inventario` DISABLE KEYS */;
INSERT INTO `historial_inventario` VALUES (1,'2023-10-04 14:25:07','danr',NULL,NULL,1,2,1,26,28,1),(2,'2023-10-05 14:45:55','danr',NULL,NULL,1,2,1,24,26,1),(3,'2023-10-05 14:45:55','danr',NULL,NULL,1,2,1,23,24,1),(4,'2023-10-09 10:32:20','danr',NULL,NULL,1,3,1,24,23,1),(5,'2023-10-09 10:35:39','danr',NULL,NULL,1,3,1,26,24,1),(6,'2023-10-09 10:40:30','danr',NULL,NULL,1,3,1,24,26,1),(7,'2023-10-09 10:41:28','danr',NULL,NULL,1,3,1,26,24,1),(8,'2023-10-09 10:45:48','danr',NULL,NULL,1,3,1,24,26,1),(9,'2023-10-09 10:46:29','danr',NULL,NULL,1,3,1,26,24,1),(10,'2023-10-09 10:48:46','danr',NULL,NULL,1,3,1,24,26,1),(11,'2023-10-09 10:50:13','danr',NULL,NULL,1,3,1,26,24,1),(12,'2023-10-09 10:53:12','danr',NULL,NULL,1,3,1,24,26,1),(13,'2023-10-09 11:01:01','danr',NULL,NULL,1,3,1,26,24,1),(14,'2023-10-09 11:08:16','danr',NULL,NULL,1,3,1,24,26,1),(15,'2023-10-09 11:09:58','danr',NULL,NULL,1,3,1,26,24,1),(16,'2023-10-09 11:11:10','danr',NULL,NULL,1,3,1,24,26,1),(17,'2023-10-09 11:15:49','danr',NULL,NULL,1,3,1,26,24,1),(18,'2023-10-09 11:19:30','danr',NULL,NULL,1,3,1,24,26,1),(19,'2023-10-09 11:22:13','danr',NULL,NULL,1,3,1,26,24,1),(20,'2023-10-09 11:22:41','danr',NULL,NULL,1,3,1,24,26,1),(21,'2023-10-09 11:31:32','danr',NULL,NULL,1,3,1,26,24,1),(22,'2023-10-09 11:36:36','danr',NULL,NULL,1,3,1,24,26,1),(23,'2023-10-09 11:36:58','danr',NULL,NULL,1,3,1,26,24,1),(24,'2023-10-09 11:39:20','danr',NULL,NULL,1,3,1,24,26,1),(25,'2023-10-09 11:44:01','danr',NULL,NULL,1,3,1,26,24,1),(26,'2023-10-09 11:45:07','danr',NULL,NULL,1,3,1,24,26,1),(27,'2023-10-09 11:47:43','danr',NULL,NULL,1,3,1,26,24,1),(28,'2023-10-09 11:50:05','danr',NULL,NULL,1,3,1,24,26,1),(29,'2023-10-09 11:52:16','danr',NULL,NULL,1,3,1,24,24,1),(30,'2023-10-09 12:00:57','danr',NULL,NULL,1,3,1,26,24,1),(31,'2023-10-09 12:03:57','danr',NULL,NULL,1,3,1,24,26,1),(32,'2023-10-09 12:04:45','danr',NULL,NULL,1,3,1,26,24,1),(33,'2023-10-09 12:09:47','danr',NULL,NULL,1,3,1,24,26,1),(34,'2023-10-09 12:11:11','danr',NULL,NULL,1,3,1,26,24,1),(35,'2023-10-09 12:14:45','danr',NULL,NULL,1,3,1,24,26,1),(36,'2023-10-09 12:18:29','danr',NULL,NULL,1,3,1,26,24,1),(37,'2023-10-09 13:34:14','danr',NULL,NULL,1,3,1,24,26,1),(38,'2023-10-09 14:14:25','danr',NULL,NULL,1,1,1,26,24,1),(39,'2023-10-09 15:52:12','danr',NULL,NULL,1,1,1,50,26,1),(40,'2023-10-09 15:52:59','danr',NULL,NULL,1,3,1,26,50,1),(41,'2023-10-09 16:00:26','danr',NULL,NULL,1,1,1,26,26,1),(42,'2023-10-10 11:39:55','danr',NULL,NULL,1,1,1,50,26,1),(43,'2023-10-10 11:40:37','danr',NULL,NULL,1,3,1,26,50,1),(44,'2023-10-10 12:18:12','danr',NULL,NULL,1,3,1,24,26,1),(45,'2023-10-10 12:18:19','danr',NULL,NULL,1,3,1,26,24,1),(46,'2023-10-10 12:18:32','danr',NULL,NULL,1,1,1,50,26,1),(47,'2023-10-10 12:18:41','danr',NULL,NULL,1,3,1,26,50,1);
/*!40000 ALTER TABLE `historial_inventario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-15 22:24:03
