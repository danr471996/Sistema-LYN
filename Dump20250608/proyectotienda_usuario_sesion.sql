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
-- Table structure for table `usuario_sesion`
--

DROP TABLE IF EXISTS `usuario_sesion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `usuario_sesion` (
  `Idusuario_sesion` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Idusuario` int(11) NOT NULL,
  `Token` varchar(32) NOT NULL,
  `Estado` int(11) NOT NULL COMMENT '1-activo, 2-inactivo',
  PRIMARY KEY (`Idusuario_sesion`),
  KEY `fr_usuarios_tiendas_idx` (`Idusuario`),
  CONSTRAINT `fr_usuarios_tiendas` FOREIGN KEY (`Idusuario`) REFERENCES `usuarios_tienda` (`Idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario_sesion`
--

LOCK TABLES `usuario_sesion` WRITE;
/*!40000 ALTER TABLE `usuario_sesion` DISABLE KEYS */;
INSERT INTO `usuario_sesion` VALUES (1,'2025-06-08 16:06:20','danr',NULL,NULL,1,'b12d45920cae4e8393d90d530ca78f70',1);
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

-- Dump completed on 2025-06-08 17:57:11
