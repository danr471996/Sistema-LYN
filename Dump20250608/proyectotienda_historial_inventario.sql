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
-- Table structure for table `historial_inventario`
--

DROP TABLE IF EXISTS `historial_inventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `historial_inventario` (
  `idHistorial_inventario` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Idproducto` int(11) NOT NULL,
  `Tipo_movimiento` int(11) NOT NULL,
  `Iddepartamento` int(11) NOT NULL,
  `Cantidad_actual` int(11) NOT NULL,
  `Cantidad_anterior` int(11) NOT NULL,
  `Estado` int(11) NOT NULL COMMENT '1- Activo\n2- Inactivo',
  PRIMARY KEY (`idHistorial_inventario`),
  KEY `fk_idproducto_idx` (`Idproducto`),
  KEY `fk_tip_movi_idx` (`Tipo_movimiento`),
  KEY `fk_iddepartamento_idx` (`Iddepartamento`),
  CONSTRAINT `fk_iddepartamento` FOREIGN KEY (`Iddepartamento`) REFERENCES `departamento` (`Iddepartmento`),
  CONSTRAINT `fk_idproducto` FOREIGN KEY (`Idproducto`) REFERENCES `productos` (`Idproducto`),
  CONSTRAINT `fk_tip_movi` FOREIGN KEY (`Tipo_movimiento`) REFERENCES `tipo_movimento` (`IdTipo_movimiento`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historial_inventario`
--

LOCK TABLES `historial_inventario` WRITE;
/*!40000 ALTER TABLE `historial_inventario` DISABLE KEYS */;
INSERT INTO `historial_inventario` VALUES (1,'2024-11-11 09:16:52','danr',NULL,NULL,1,2,3,24,27,1),(2,'2024-11-11 15:21:57','danr',NULL,NULL,1,2,3,23,24,1),(3,'2024-11-11 15:21:57','danr',NULL,NULL,2,2,2,27,29,1),(4,'2024-11-11 15:43:10','danr',NULL,NULL,2,2,2,26,27,1),(5,'2024-11-18 15:20:52','danr',NULL,NULL,1,2,3,21,23,1),(6,'2024-11-18 15:20:52','danr',NULL,NULL,2,2,2,25,26,1),(7,'2024-11-18 15:35:55','danr',NULL,NULL,1,2,3,20,21,1),(8,'2024-11-18 15:44:25','danr',NULL,NULL,2,2,2,24,25,1),(9,'2025-06-02 11:35:20','danr',NULL,NULL,1,2,3,18,20,1),(10,'2025-06-02 11:35:20','danr',NULL,NULL,2,2,2,23,24,1),(11,'2025-06-02 11:53:43','danr',NULL,NULL,1,2,3,16,18,1);
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

-- Dump completed on 2025-06-08 17:57:17
