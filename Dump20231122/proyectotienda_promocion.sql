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
-- Table structure for table `promocion`
--

DROP TABLE IF EXISTS `promocion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promocion` (
  `Idpromocion` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Nombre_promocion` varchar(45) NOT NULL,
  `Id_producto` int NOT NULL,
  `Cant_desde` int NOT NULL,
  `Cant_hasta` int NOT NULL,
  `Precio_unitario` decimal(15,2) NOT NULL,
  `Estado` int NOT NULL,
  PRIMARY KEY (`Idpromocion`),
  KEY `fk_productos_idx` (`Id_producto`),
  CONSTRAINT `fk_productos` FOREIGN KEY (`Id_producto`) REFERENCES `productos` (`Idproducto`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promocion`
--

LOCK TABLES `promocion` WRITE;
/*!40000 ALTER TABLE `promocion` DISABLE KEYS */;
INSERT INTO `promocion` VALUES (1,'2022-09-13 14:54:21',NULL,NULL,'carajo',1,23,30,30.00,1),(2,'2022-09-13 14:55:24','2023-11-20 11:20:30','danr','carajo',1,23,30,30.00,2),(3,'2023-04-30 17:05:59',NULL,NULL,'carajo',1,4242,45455,465464.00,1),(4,'2023-11-15 16:40:43',NULL,NULL,'pruebita',1,20,50,0.00,1),(5,'2023-11-15 16:42:48',NULL,NULL,'pruebita',1,50,60,20.00,1),(6,'2023-11-15 16:46:51',NULL,NULL,'pruebita',1,36,68,380.00,1),(7,'2023-11-15 16:49:00',NULL,NULL,'jerusalen',1,30,50,380.00,1),(8,'2023-11-15 16:52:07',NULL,NULL,'jerusalen',1,2,10,380.00,1),(9,'2023-11-15 16:52:48',NULL,NULL,'jerusalenrer',1,4,365,380.00,1),(10,'2023-11-15 16:56:11',NULL,NULL,'jerusalenkjhkhj',1,1,20,380.00,1);
/*!40000 ALTER TABLE `promocion` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-15 22:24:02
