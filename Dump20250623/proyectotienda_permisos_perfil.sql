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
-- Table structure for table `permisos_perfil`
--

DROP TABLE IF EXISTS `permisos_perfil`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `permisos_perfil` (
  `Id_permiso` varchar(50) NOT NULL,
  `Id_perfil` int(11) NOT NULL,
  PRIMARY KEY (`Id_permiso`,`Id_perfil`),
  KEY `fk__idx` (`Id_permiso`),
  KEY `fk_perfil_idx` (`Id_perfil`),
  CONSTRAINT `fk_perfil` FOREIGN KEY (`Id_perfil`) REFERENCES `usuarios_perfiles` (`Id_perfil`),
  CONSTRAINT `fk_permiso` FOREIGN KEY (`Id_permiso`) REFERENCES `lista_permisos` (`Id_permiso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisos_perfil`
--

LOCK TABLES `permisos_perfil` WRITE;
/*!40000 ALTER TABLE `permisos_perfil` DISABLE KEYS */;
INSERT INTO `permisos_perfil` VALUES ('A1',1),('A10',1),('A11',1),('A12',1),('A13',1),('A14',1),('A15',1),('A16',1),('A17',1),('A18',1),('A19',1),('A2',1),('A3',1),('A4',1),('A5',1),('A6',1),('A7',1),('A8',1),('A9',1),('C1',1),('C2',1),('C3',1),('C4',1),('C5',1),('I1',1),('I2',1),('O1',1),('O10',1),('O2',1),('O3',1),('O6',1),('O8',1),('O9',1),('P1',1),('P10',1),('P11',1),('P12',1),('P2',1),('P3',1),('P4',1),('P5',1),('P6',1),('P7',1),('P8',1),('P9',1),('R1',1),('R2',1),('R3',1),('R4',1),('R5',1),('V1',1),('V2',1),('V3',1),('V4',1),('V5',1),('A1',2),('A10',2),('A11',2),('A12',2),('A13',2),('A14',2),('A15',2),('A16',2),('A17',2),('A18',2),('A19',2),('A2',2),('A3',2),('A4',2),('A5',2),('A6',2),('A7',2),('A8',2),('A9',2),('C1',2),('C2',2),('C3',2),('C4',2),('C5',2),('I1',2),('I2',2),('O1',2),('O2',2),('O3',2),('O8',2),('O9',2),('P1',2),('P10',2),('P11',2),('P12',2),('P2',2),('P3',2),('P4',2),('P5',2),('P6',2),('P7',2),('P8',2),('P9',2),('R1',2),('R2',2),('R3',2),('R4',2),('R5',2),('V1',2),('V2',2),('V3',2),('V4',2),('V5',2);
/*!40000 ALTER TABLE `permisos_perfil` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-23 19:35:23
