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
-- Table structure for table `detalle_factura`
--

DROP TABLE IF EXISTS `detalle_factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `detalle_factura` (
  `Iddetalle_factura` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Id_factura` int(11) NOT NULL,
  `Idproducto` int(11) NOT NULL,
  `Cantidad` int(11) NOT NULL,
  `Monto` decimal(15,2) NOT NULL,
  `Estado` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Iddetalle_factura`),
  KEY `fk_factura_idx` (`Id_factura`),
  KEY `fk_producto_idx` (`Idproducto`),
  CONSTRAINT `fk_factura` FOREIGN KEY (`Id_factura`) REFERENCES `factura` (`Idfactura`),
  CONSTRAINT `fk_producto` FOREIGN KEY (`Idproducto`) REFERENCES `productos` (`Idproducto`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalle_factura`
--

LOCK TABLES `detalle_factura` WRITE;
/*!40000 ALTER TABLE `detalle_factura` DISABLE KEYS */;
INSERT INTO `detalle_factura` VALUES (1,'2024-11-11 09:16:52','danr',NULL,NULL,1,1,3,1140.00,1),(2,'2024-11-11 15:21:57','danr',NULL,NULL,2,1,1,380.00,1),(3,'2024-11-11 15:21:57','danr',NULL,NULL,2,2,2,700.00,1),(4,'2024-11-11 15:43:10','danr',NULL,NULL,3,2,1,350.00,1),(5,'2024-11-18 15:20:52','danr',NULL,NULL,4,1,2,760.00,1),(6,'2024-11-18 15:20:52','danr',NULL,NULL,4,2,1,350.00,1),(7,'2024-11-18 15:35:55','danr',NULL,NULL,5,1,1,380.00,1),(8,'2024-11-18 15:44:25','danr',NULL,NULL,6,2,1,350.00,1),(9,'2025-06-02 11:35:20','danr',NULL,NULL,7,1,2,760.00,1),(10,'2025-06-02 11:35:20','danr',NULL,NULL,7,2,1,350.00,1),(11,'2025-06-02 11:53:43','danr',NULL,NULL,8,1,2,760.00,1);
/*!40000 ALTER TABLE `detalle_factura` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-08 17:57:16
