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
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `Idcliente` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Primer_nombre` varchar(45) NOT NULL,
  `Segundo_nombre` varchar(45) DEFAULT NULL,
  `Primer_apellido` varchar(45) NOT NULL,
  `Segundo_apellido` varchar(45) DEFAULT NULL,
  `Direccion` varchar(100) NOT NULL,
  `Telefono` int DEFAULT NULL,
  `Id_tipocredito` int NOT NULL,
  `Cantidad_credito` int DEFAULT NULL,
  `Estado` int NOT NULL COMMENT '1-activo \\n2-inactivo',
  PRIMARY KEY (`Idcliente`),
  KEY `fk_tipo_credito_idx` (`Id_tipocredito`),
  CONSTRAINT `fk_tipo_credito` FOREIGN KEY (`Id_tipocredito`) REFERENCES `tipo_credito` (`Id_tipocredito`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (3,'2022-09-25 11:01:53','danr','2023-11-21 15:40:20','danr','ligia','maria','godinez','garcia','residencial valle verde',22481684,2,30,2),(4,'2022-10-29 08:10:17','danr',NULL,NULL,'David','Antonio','Navas','Romero','residencial valle verde',22481684,2,0,1),(5,'2023-06-01 17:44:02','danr',NULL,NULL,'hernaldo','maria','godinez',NULL,'residencial valle verde',3535353,1,NULL,1),(6,'2023-10-05 14:40:44','danr','2023-11-20 10:24:49','danr','sofia','carolina','dixon','rimes','km 14 carretera norte, 2 km al sur',76512748,2,1586,1),(7,'2023-11-21 14:39:42','danr',NULL,NULL,'Tania','lisseth','Navas','Romero','Clinica don bosco 4cuadras arriba 1 y 1/2 al sur ',45678925,2,800,1);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
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
