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
-- Table structure for table `lista_permisos`
--

DROP TABLE IF EXISTS `lista_permisos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `lista_permisos` (
  `Id_permiso` varchar(50) NOT NULL,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_alta` varchar(45) DEFAULT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Descripcion` varchar(300) NOT NULL,
  `SELECCIONADO` tinyint(4) NOT NULL,
  `GRUPO` varchar(300) NOT NULL,
  `INICIO_GRUPO` tinyint(4) NOT NULL,
  `Estado` int(11) NOT NULL COMMENT '1-activo\n2-inactivo',
  PRIMARY KEY (`Id_permiso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lista_permisos`
--

LOCK TABLES `lista_permisos` WRITE;
/*!40000 ALTER TABLE `lista_permisos` DISABLE KEYS */;
INSERT INTO `lista_permisos` VALUES ('A1','2025-04-27 13:59:17','danr',NULL,NULL,'Usuarios',0,'Administración',1,1),('A10','2025-04-27 13:59:17','danr',NULL,NULL,'Proveedores',0,'Administración',0,1),('A11','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Administración',0,1),('A12','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Administración',0,1),('A13','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Administración',0,1),('A14','2025-04-27 13:59:17','danr',NULL,NULL,' ● Detalle',0,'Administración',0,1),('A15','2025-04-27 13:59:17','danr',NULL,NULL,'Departamentos',0,'Administración',0,1),('A16','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Administración',0,1),('A17','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Administración',0,1),('A18','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Administración',0,1),('A19','2025-04-27 13:59:17','danr',NULL,NULL,'Sesiones de usuarios',0,'Administración',0,1),('A2','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Administración',0,1),('A3','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Administración',0,1),('A4','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Administración',0,1),('A5','2025-04-27 13:59:17','danr',NULL,NULL,' ● Detalle',0,'Administración',0,1),('A6','2025-04-27 13:59:17','danr',NULL,NULL,'Perfiles',0,'Administración',0,1),('A7','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Administración',0,1),('A8','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Administración',0,1),('A9','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Administración',0,1),('C1','2025-04-27 13:59:17','danr',NULL,NULL,'Clientes',0,'Clientes',1,1),('C2','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Clientes',0,1),('C3','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Clientes',0,1),('C4','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Clientes',0,1),('C5','2025-04-27 13:59:17','danr',NULL,NULL,' ● Estado de cuenta',0,'Clientes',0,1),('I1','2025-04-27 13:59:17','danr',NULL,NULL,'Agregar Inventario',0,'Inventario',1,1),('I2','2025-04-27 13:59:17','danr',NULL,NULL,'Ajustar Inventario',0,'Inventario',0,1),('O1','2025-04-27 13:59:17','danr',NULL,NULL,'Configuracion',0,'Configuración',1,1),('O2','2025-04-27 13:59:17','danr',NULL,NULL,'Base de datos',0,'Configuración',0,1),('O3','2025-04-27 13:59:17','danr',NULL,NULL,'Opciones habilitadas',0,'Configuración',0,1),('O4','2025-04-27 13:59:17','danr',NULL,NULL,'Folio de tickets',0,'Configuración',0,1),('O5','2025-04-27 13:59:17','danr',NULL,NULL,'Impuestos',0,'Configuración',0,1),('O6','2025-04-27 13:59:17','danr',NULL,NULL,'Formas de pago',0,'Configuración',0,1),('O7','2025-04-27 13:59:17','danr',NULL,NULL,'Unidades de medida',0,'Configuración',0,1),('O8','2025-04-27 13:59:17','danr',NULL,NULL,'Símbolo de moneda',0,'Configuración',0,1),('O9','2025-04-27 13:59:17','danr',NULL,NULL,'Tickets',0,'Configuración',0,1),('P1','2025-04-27 13:59:17','danr',NULL,NULL,'Gestion de Productos',0,'Productos',1,1),('P10','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Productos',0,1),('P11','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Productos',0,1),('P12','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Productos',0,1),('P2','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Productos',0,1),('P3','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Productos',0,1),('P4','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Productos',0,1),('P5','2025-04-27 13:59:17','danr',NULL,NULL,'Promociones',0,'Productos',0,1),('P6','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Productos',0,1),('P7','2025-04-27 13:59:17','danr',NULL,NULL,' ● Editar',0,'Productos',0,1),('P8','2025-04-27 13:59:17','danr',NULL,NULL,' ● Borrar',0,'Productos',0,1),('P9','2025-04-27 13:59:17','danr',NULL,NULL,'Presentación',0,'Productos',0,1),('R1','2025-04-27 13:59:17','danr',NULL,NULL,'Reporte de Inventario',0,'Reportes',1,1),('R2','2025-04-27 13:59:17','danr',NULL,NULL,'Productos bajos en inventario',0,'Reportes',0,1),('R3','2025-04-27 13:59:17','danr',NULL,NULL,'Reporte de saldos',0,'Reportes',0,1),('R4','2025-04-27 13:59:17','danr',NULL,NULL,'Ventas por periodo',0,'Reportes',0,1),('R5','2025-04-27 13:59:17','danr',NULL,NULL,'Reporte de Movimientos',0,'Reportes',0,1),('V1','2025-04-27 13:59:17','danr',NULL,NULL,'Facturación',0,'Ventas',1,1),('V2','2025-04-27 13:59:17','danr',NULL,NULL,' ● Agregar',0,'Ventas',0,1),('V3','2025-04-27 13:59:17','danr',NULL,NULL,' ● Facturar',0,'Ventas',0,1),('V4','2025-04-27 13:59:17','danr',NULL,NULL,'Impresión',0,'Ventas',0,1),('V5','2025-04-27 13:59:17','danr',NULL,NULL,'Corte',0,'Ventas',0,1);
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

-- Dump completed on 2025-04-27 19:05:34
