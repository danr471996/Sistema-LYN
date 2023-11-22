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
-- Table structure for table `opciones`
--

DROP TABLE IF EXISTS `opciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `opciones` (
  `ID_OP` varchar(45) NOT NULL,
  `NOMBRE_OP` varchar(100) DEFAULT NULL,
  `DESCRIPCION_OP` varchar(500) DEFAULT NULL,
  `SELECCIONADO_OP` tinyint(1) DEFAULT NULL,
  `DETALLE_EXT1` varchar(500) DEFAULT NULL,
  `DETALLE_EXT2` varchar(500) DEFAULT NULL,
  `DETALLE_EXT3` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID_OP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `opciones`
--

LOCK TABLES `opciones` WRITE;
/*!40000 ALTER TABLE `opciones` DISABLE KEYS */;
INSERT INTO `opciones` VALUES ('OP1_ATP','Deseo activar esta base de datos de mas de 7500 productos','al activar esta opcion, cada vez que des de alta un producto, se buscara en esta base de datos , al ser encontrado te incluira la descripcion donde tu solo ingresas el precio al cual lo tienes en venta, ahorrandote el tiempo de captura de esta informacion',0,'',NULL,NULL),('OP1_FDP','No permitir cobrar si el efecto ingresado es menor que el total de la venta','al habilitardo cuando el efectio es menor que el total de venta no permitira el cobro',1,'Efectivo',NULL,NULL),('OP1_FDT','Nombre:','Puedes Configurar aqui el manejo de los folios de las ventanas del programa',0,'Folio:Cajero',NULL,NULL),('OP1_IMP','Mis productos manejan impuestos','(IVA,etc)',0,'',NULL,NULL),('OP1_OPH','Utilizar Invetarios para mis productos','si usas invetario, tus productos tendran cantidades limitadas en venta y podras llevar un control de cuanto tienes, cuando y cuanto se vende, si actualmente no usas inventario, puedes no usarlo y posteriormente activarlo',1,'',NULL,NULL),('OP1_SDM','Simbolo de moneda ',NULL,0,'$',NULL,NULL),('OP1_TKS','CAMPOLINEAS1',NULL,0,'Ruc:J304323933456\r\nRazón social :L&N\r\nTeléfono:22481684',NULL,NULL),('OP1_UDM','Unidad para productos \"A GRANEL\" mayores a uno:',NULL,0,'Kg','Puedes personalizar las unidades de medida que se usan al imprimir el ticket \"A GRANEL\" mayores a uno',NULL),('OP2_FDP','Deseo habilitar el cobro en dolares americanos','al habilitarlo podras realizara el cobro en dolares, el tip ode cambio te es preguntado cada vez que inicia el programa',0,'Dolares americanos',NULL,NULL),('OP2_OPH','Deseo ofrecer credito a mis clientes','Activar esta opcion para dar de alta clientes y poder ofrecer ventas a credito, recibir abonos y liquidar adeudos',1,'',NULL,NULL),('OP2_SDM','Separador de miles ',NULL,0,',',NULL,NULL),('OP2_TKS','CAMPOLINEAS2',NULL,0,'¡No hay peor compra, que la que no se hace!',NULL,NULL),('OP2_UDM','Unidad para productos \"A GRANEL\" menores a uno:',NULL,0,'Gr','Puedes personalizar las unidades de medida que se usan al imprimir el ticket \"A GRANEL\" menores a uno',NULL),('OP3_FDP','Deseo habilitar cobro con tarjeta de credito/debito','al habilitarlo podras registrar el pago de la venta con tarjeta y obtener el reporte de ingresos que tuvistes con esta forma de pago en el corte del dia',1,'Tarjeta de credito',NULL,NULL),('OP3_OPH','Habilitar ventana de producto comun','deseas activar la opcion de ventada de \"Producto Comun\" con la cual puedes vender articulos que no estan en la base de datos al momento de hacer una venta',1,'',NULL,NULL),('OP3_SDM','Separador Decimal',NULL,0,'.',NULL,NULL),('OP3_TKS','Incluir precio unitario en la impresion del ticket','Sin informacion',0,'',NULL,NULL),('OP4_FDP','Deseo habilitar cobro con vales de despensa','al habilitarlo podras registrar el pago de la venta con vales de despensa y obtener el reporte de ingresos que tuviste con esta forma de pago en el corte del dia',1,'Vales de despensa',NULL,NULL),('OP4_OPH','Calcular automaticamente precio de venta con el margen de ganancia del','% al crear,modificar o ingresar inventario a los productos',0,'44',NULL,NULL),('OP4_SDM','Numero de Decimales',NULL,0,'2','number',NULL),('OP4_TKS','Imprimir descripcion completa (Varios reglones para la descripcion)','Sin informacion',0,'',NULL,NULL),('OP5_OPH','Habilitar redondeo de centavos a cantidad cerrada','Seleccione una opcion de la lista',0,'A decimales (Ej:45.52 -> 45.60)','A decimales (Ej:45.52 -> 45.60)','A a mitad/entero (Ej:45.44 -> 46.60)');
/*!40000 ALTER TABLE `opciones` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-22 14:17:24
