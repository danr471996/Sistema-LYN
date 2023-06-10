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
-- Table structure for table `usuarios_tienda`
--

DROP TABLE IF EXISTS `usuarios_tienda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios_tienda` (
  `Idusuario` int NOT NULL AUTO_INCREMENT,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) NOT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Login` varchar(45) NOT NULL,
  `Contraseña` varchar(45) NOT NULL,
  `Id_perfil` int NOT NULL,
  `Estado_usuario` int NOT NULL COMMENT '1-activo\n2- inactivo',
  PRIMARY KEY (`Idusuario`),
  KEY `fk_usuarios_perfiles_idx` (`Id_perfil`),
  CONSTRAINT `fk_usuarios_perfiles` FOREIGN KEY (`Id_perfil`) REFERENCES `usuarios_perfiles` (`Id_perfil`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios_tienda`
--

LOCK TABLES `usuarios_tienda` WRITE;
/*!40000 ALTER TABLE `usuarios_tienda` DISABLE KEYS */;
INSERT INTO `usuarios_tienda` VALUES (1,'2022-09-11 00:00:00','danr',NULL,NULL,'danr','8522',1,1),(2,'2022-10-09 12:23:34','danr',NULL,NULL,'LG','8522',1,1),(3,'2023-04-30 15:34:10','danr','2023-04-30 17:04:10','danr','TLR','8522',1,2),(4,'2023-04-30 15:39:56','danr',NULL,NULL,'lmgg','8522',1,1),(5,'2023-04-30 15:57:15','danr','2023-04-30 16:00:09','danr','lmgg','8522',1,2);
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

-- Dump completed on 2023-06-09  6:49:20
