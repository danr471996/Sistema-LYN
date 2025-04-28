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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario_sesion`
--

LOCK TABLES `usuario_sesion` WRITE;
/*!40000 ALTER TABLE `usuario_sesion` DISABLE KEYS */;
INSERT INTO `usuario_sesion` VALUES (1,'2025-01-09 14:30:31','danr','2025-01-09 14:31:18','danr',1,'808046eeeeb741fca94ae8a0c44acf63',2),(2,'2025-01-09 14:32:09','danr','2025-01-09 14:33:28','danr',1,'8271b4f7f7d5422d828e0e6913b09dce',2),(3,'2025-01-13 15:22:16','danr','2025-01-13 15:22:55','danr',1,'9d7423cbb5634c8eb28e1a1ef2938245',2),(4,'2025-01-13 15:27:17','danr','2025-01-13 15:27:39','danr',1,'7eedd58474aa47d18a5402cc5c86a085',2),(5,'2025-01-14 15:02:13','danr','2025-01-14 15:09:52','danr',1,'2e549e147aee4cb7829fcc26d24caa83',2),(6,'2025-01-14 15:58:29','danr','2025-01-14 15:59:19','danr',1,'fd643d53befd40778de38c333329a5a8',2),(7,'2025-01-14 16:00:20','danr','2025-01-14 16:04:27','danr',1,'325e87b5d4b5466ca3949f58f0e01b6d',2),(8,'2025-01-23 09:26:25','danr','2025-01-23 09:30:24','danr',1,'da37d4164a464a54ab7388b4edb23017',2),(9,'2025-01-31 11:27:59','danr','2025-01-31 11:28:33','danr',1,'2686a488b13c430ab0fde526b44fd803',2),(10,'2025-02-04 15:48:27','danr','2025-02-04 15:49:19','danr',1,'2e1ef70ffe35408abb8261159a23aa0d',2),(11,'2025-02-04 16:30:11','danr','2025-02-04 16:31:13','danr',1,'cb6f5bcf1d824842a0f3db72e6dd3d15',2);
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

-- Dump completed on 2025-04-27 19:05:31
