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
-- Table structure for table `lista_permisos`
--

DROP TABLE IF EXISTS `lista_permisos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lista_permisos` (
  `Id_permiso` varchar(50) NOT NULL,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) DEFAULT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Descripcion` varchar(300) NOT NULL,
  `SELECCIONADO` tinyint NOT NULL,
  `GRUPO` varchar(300) NOT NULL,
  `INICIO_GRUPO` tinyint NOT NULL,
  `Estado` int NOT NULL COMMENT '1-activo\n2-inactivo',
  PRIMARY KEY (`Id_permiso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lista_permisos`
--

LOCK TABLES `lista_permisos` WRITE;
/*!40000 ALTER TABLE `lista_permisos` DISABLE KEYS */;
INSERT INTO `lista_permisos` VALUES ('C1','2022-09-11 00:00:00','danr',NULL,NULL,' Administrar credito de clientes',0,'Clientes',1,1),('C2','2022-09-11 00:00:00','danr',NULL,NULL,' Crear , modificar o eliminar',0,'Clientes',0,1),('I1','2022-09-11 00:00:00','danr',NULL,NULL,' Agregar mercancia',0,'Inventario',1,1),('I2','2022-09-11 00:00:00','danr',NULL,NULL,' Ver existencias y minimos',0,'Inventario',0,1),('I3','2022-09-11 00:00:00','danr',NULL,NULL,' Ajustar el inventario',0,'Inventario',0,1),('I4','2022-09-11 00:00:00','danr',NULL,NULL,' Ver movimiento de invetarios',0,'Inventario',0,1),('O1','2022-09-11 00:00:00','danr',NULL,NULL,' Cambiar la configuracion',0,'Otros',1,1),('O2','2022-09-11 00:00:00','danr',NULL,NULL,' Realizar el corte del dia',0,'Otros',0,1),('O3','2022-09-11 00:00:00','danr',NULL,NULL,' Ver ganancia del dia',0,'Otros',0,1),('P1','2022-09-11 00:00:00','danr',NULL,NULL,' Crear productos',0,'Productos',1,1),('P2','2022-09-11 00:00:00','danr',NULL,NULL,' Modificar productos',0,'Productos',0,1),('P3','2022-09-11 00:00:00','danr',NULL,NULL,' Eliminar productos',0,'Productos',0,1),('P4','2022-09-11 00:00:00','danr',NULL,NULL,' Ver reporte de ventas',0,'Productos',0,1),('P5','2022-09-11 00:00:00','danr',NULL,NULL,' Crear promociones',0,'Productos',0,1),('V1','2022-09-11 00:00:00','danr',NULL,NULL,' Cobrar un ticket',0,'Ventas',1,1),('V2','2022-09-11 00:00:00','danr',NULL,NULL,' Utilizar producto comun',0,'Ventas',0,1),('V3','2022-09-11 00:00:00','danr',NULL,NULL,' Registrar entradas',0,'Ventas',0,1),('V4','2022-09-11 00:00:00','danr',NULL,NULL,' Registrar salidas',0,'Ventas',0,1),('V5','2022-09-11 00:00:00','danr',NULL,NULL,' Aplicar precio de mayoreo y descuentos',0,'Ventas',0,1),('V6','2022-09-11 00:00:00','danr',NULL,NULL,' Revisar historial de ventas',0,'Ventas',0,1),('V7','2022-09-11 00:00:00','danr',NULL,NULL,' Cobrar a credito',0,'Ventas',0,1);
/*!40000 ALTER TABLE `lista_permisos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-15 22:24:05
