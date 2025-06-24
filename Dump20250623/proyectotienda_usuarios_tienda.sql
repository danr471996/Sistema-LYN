-- MySQL dump 10.13  Distrib 8.0.16, for Win64 (x86_64)
--
-- Host: localhost    Database: proyectotienda
-- ------------------------------------------------------
-- Server version	8.0.16

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `usuarios_tienda`
--

DROP TABLE IF EXISTS `usuarios_tienda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `usuarios_tienda` (
  `Idusuario` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Login` varchar(45) NOT NULL,
  `Contraseña` varchar(65) NOT NULL,
  `Id_perfil` int(11) NOT NULL,
  `Estado_usuario` int(11) NOT NULL COMMENT '1-activo\n2- inactivo',
  PRIMARY KEY (`Idusuario`),
  KEY `fk_usuarios_perfiles_idx` (`Id_perfil`),
  CONSTRAINT `fk_usuarios_perfiles` FOREIGN KEY (`Id_perfil`) REFERENCES `usuarios_perfiles` (`Id_perfil`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios_tienda`
--

LOCK TABLES `usuarios_tienda` WRITE;
/*!40000 ALTER TABLE `usuarios_tienda` DISABLE KEYS */;
INSERT INTO `usuarios_tienda` VALUES (1,'2022-09-11 00:00:00','danr','2023-11-22 11:20:49','danr','danr','$2a$12$72kWmZV7w3MZ2wRSRjOpI.YJavzjg5Q1b7erXrlEzWIY2ohKDVXhm',1,1),(2,'2022-10-09 12:23:34','danr','2023-11-22 11:20:57','danr','LG','$2a$12$72kWmZV7w3MZ2wRSRjOpI.YJavzjg5Q1b7erXrlEzWIY2ohKDVXhm',1,1),(3,'2024-10-28 14:49:04','danr',NULL,NULL,'JMaria','$2a$12$72kWmZV7w3MZ2wRSRjOpI.YJavzjg5Q1b7erXrlEzWIY2ohKDVXhm',1,1),(4,'2024-10-28 16:31:42','danr',NULL,NULL,'Mjesus','$2a$12$72kWmZV7w3MZ2wRSRjOpI.YJavzjg5Q1b7erXrlEzWIY2ohKDVXhm',1,1);
/*!40000 ALTER TABLE `usuarios_tienda` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-23 19:35:20
