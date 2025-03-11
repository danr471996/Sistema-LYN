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
-- Table structure for table `movimientos`
--

DROP TABLE IF EXISTS `movimientos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movimientos` (
  `idMovimientos` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Monto` decimal(15,2) NOT NULL,
  `Tipo_movimiento` int NOT NULL,
  `Tipo_pago` int NOT NULL,
  `Idpago` int DEFAULT NULL,
  `Estado` int NOT NULL COMMENT '1-Activo\n2-Inactivo',
  PRIMARY KEY (`idMovimientos`),
  KEY `fk_tipo_movimiento_idx` (`Tipo_movimiento`),
  KEY `fk_tipo_pago_idx` (`Tipo_pago`),
  KEY `fk_id_pago_idx` (`Idpago`),
  CONSTRAINT `fk_id_pago` FOREIGN KEY (`Idpago`) REFERENCES `pagos` (`Idpagos`),
  CONSTRAINT `fk_tipo_movimiento` FOREIGN KEY (`Tipo_movimiento`) REFERENCES `tipo_movimento` (`IdTipo_movimiento`),
  CONSTRAINT `fk_tipo_pago` FOREIGN KEY (`Tipo_pago`) REFERENCES `tipo_pago` (`idtipo_pago`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movimientos`
--

LOCK TABLES `movimientos` WRITE;
/*!40000 ALTER TABLE `movimientos` DISABLE KEYS */;
INSERT INTO `movimientos` VALUES (1,'2024-11-11 09:16:52','danr',NULL,NULL,1140.00,1,1,NULL,1),(2,'2024-11-11 09:20:17','danr',NULL,NULL,200.00,1,2,1,1),(3,'2024-11-11 10:29:35','danr',NULL,NULL,200.00,1,2,2,1),(4,'2024-11-11 11:11:25','danr',NULL,NULL,200.00,1,2,3,1),(5,'2024-11-11 15:21:57','danr',NULL,NULL,1080.00,1,2,4,1),(6,'2024-11-11 15:43:10','danr',NULL,NULL,350.00,1,2,5,1),(7,'2024-11-18 15:20:52','danr',NULL,NULL,1110.00,1,1,NULL,1),(8,'2024-11-18 15:31:13','danr',NULL,NULL,200.00,1,2,6,1),(9,'2024-11-18 15:35:55','danr',NULL,NULL,380.00,1,1,NULL,1),(10,'2024-11-18 15:37:48','danr',NULL,NULL,200.00,1,2,7,1),(11,'2024-11-18 15:44:25','danr',NULL,NULL,350.00,1,1,NULL,1),(12,'2024-11-18 15:44:53','danr',NULL,NULL,200.00,1,2,8,1);
/*!40000 ALTER TABLE `movimientos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-11 16:13:28
