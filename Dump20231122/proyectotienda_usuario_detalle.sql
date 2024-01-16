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
-- Table structure for table `usuario_detalle`
--

DROP TABLE IF EXISTS `usuario_detalle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario_detalle` (
  `Idusuariodetalle` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Idusuario` int NOT NULL,
  `Primer_nombre` varchar(45) NOT NULL,
  `Segundo_nombre` varchar(45) DEFAULT NULL,
  `Primer_apellido` varchar(45) NOT NULL,
  `Segundo_apellido` varchar(45) DEFAULT NULL,
  `Telefono` int DEFAULT NULL,
  `Direccion` varchar(100) NOT NULL,
  PRIMARY KEY (`Idusuariodetalle`),
  KEY `fk_usuarios_tienda_idx` (`Idusuario`),
  CONSTRAINT `fk_usuarios_tienda` FOREIGN KEY (`Idusuario`) REFERENCES `usuarios_tienda` (`Idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario_detalle`
--

LOCK TABLES `usuario_detalle` WRITE;
/*!40000 ALTER TABLE `usuario_detalle` DISABLE KEYS */;
INSERT INTO `usuario_detalle` VALUES (1,'2022-09-11 00:00:00','danr','2023-11-22 11:20:49','danr',1,'David','Antonio','Navas','Navas',22481684,'clinica don bosco 4 cuadras arriba 110 varas al sur'),(2,'2022-10-09 12:23:55','danr','2023-11-22 11:20:57','danr',2,'ligia',NULL,'godinez','maria',22481684,'residencial valle verde'),(3,'2023-04-30 15:34:10','danr',NULL,NULL,3,'fdsafdsf','fdsafsdf','fdsafsdf','fdsafsdf',123123,'fdsafsdf'),(4,'2023-04-30 15:39:56','danr','2023-11-22 11:15:35','danr',4,'ligia','fdsafsdf','godinez','maria',NULL,'valle verde'),(5,'2023-04-30 15:57:15','danr',NULL,NULL,5,'fdsafdsf','fdsafsdf','godinez','maria',NULL,'valle verde'),(6,'2023-11-21 16:42:08','danr','2023-11-22 11:14:54','danr',6,'David','Antonio','Navas','Romero',36524789,'dafdfdsf'),(7,'2023-11-21 16:42:38','danr','2023-11-22 11:14:41','danr',7,'David','Antonio','Navas','Romero',12345678,'dafdfdsf'),(8,'2023-11-22 11:21:33','danr','2023-11-22 11:21:42','danr',8,'David','Antonio','Navas','Romero',12365478,'dafdfdsf');
/*!40000 ALTER TABLE `usuario_detalle` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-15 22:24:04
