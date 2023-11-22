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
-- Table structure for table `factura`
--

DROP TABLE IF EXISTS `factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `factura` (
  `Idfactura` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Idcliente` int DEFAULT NULL,
  `Num_factura` int NOT NULL,
  `Monto_total` decimal(15,2) NOT NULL,
  `Estado` int NOT NULL DEFAULT '1' COMMENT '1-Pagado\n2-Pendiente de pago',
  PRIMARY KEY (`Idfactura`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `factura`
--

LOCK TABLES `factura` WRITE;
/*!40000 ALTER TABLE `factura` DISABLE KEYS */;
INSERT INTO `factura` VALUES (1,'2022-10-28 19:33:56','danr',NULL,NULL,NULL,1,760.00,1),(2,'2022-10-28 21:03:43','danr',NULL,NULL,NULL,2,1140.00,1),(3,'2022-10-28 21:34:50','danr',NULL,NULL,NULL,3,1140.00,1),(4,'2022-10-28 21:38:16','danr',NULL,NULL,NULL,4,380.00,1),(5,'2022-10-28 21:44:07','danr',NULL,NULL,NULL,5,380.00,1),(6,'2022-11-26 16:18:09','danr',NULL,NULL,NULL,6,760.00,1),(7,'2023-01-16 20:12:58','danr',NULL,NULL,NULL,7,760.00,1),(8,'2023-01-29 08:47:01','danr',NULL,NULL,NULL,8,760.00,1),(9,'2023-06-03 16:39:21','danr',NULL,NULL,NULL,9,760.00,1),(10,'2023-10-04 14:24:56','danr',NULL,NULL,4,10,760.00,2),(11,'2023-10-05 14:45:55','danr',NULL,NULL,6,11,760.00,1),(12,'2023-10-05 15:55:03','danr',NULL,NULL,6,12,380.00,2);
/*!40000 ALTER TABLE `factura` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-22 14:17:23