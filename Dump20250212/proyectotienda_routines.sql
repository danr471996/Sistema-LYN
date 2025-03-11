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
-- Dumping routines for database 'proyectotienda'
--
/*!50003 DROP PROCEDURE IF EXISTS `SP_PAGOSEFECTIVOCREDITO` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGOSEFECTIVOCREDITO`(IN usuario VARCHAR(50))
BEGIN
SELECT SUM(CASE 
			WHEN mov.Tipo_pago = 1
				THEN (
						CASE 
							WHEN mov.Tipo_movimiento = 1
								THEN mov.Monto
							WHEN mov.Tipo_movimiento = 2
								THEN (mov.Monto * - 1)
							ELSE 0
							END
						)
			ELSE 0
			END) AS CREDITO
	,SUM(CASE 
			WHEN mov.Tipo_pago = 2
				THEN (
						CASE 
							WHEN mov.Tipo_movimiento = 1
								THEN mov.Monto
							WHEN mov.Tipo_movimiento = 2
								THEN (mov.Monto * - 1)
							ELSE 0
							END
						)
			ELSE 0
			END) AS EFECTIVO,    COUNT(
        CASE 
            WHEN mov.Tipo_pago = 1 THEN 1
            ELSE NULL
        END
    ) AS CANTIDAD_CREDITO,
    COUNT(
        CASE 
            WHEN mov.Tipo_pago = 2 THEN 1
            ELSE NULL
        END
    ) AS CANTIDAD_EFECTIVO
FROM movimientos AS mov
where DATE(mov.Fecha_alta) = DATE(CURRENT_DATE())
AND mov.Usuario_alta = usuario;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_PRODUCTOSXDEPARTAMENTO` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PRODUCTOSXDEPARTAMENTO`(IN usuario VARCHAR(50))
BEGIN
select 
producto.Codigo_producto as CODIGO_PRODUCTO,
sum(detalle.Cantidad)  as CANTIDAD_PRODUCTO,
departamento.Descripcion as DEPARTAMENTO
from proyectotienda.detalle_factura as detalle
inner join proyectotienda.productos  as producto on producto.Idproducto = detalle.Idproducto
inner join proyectotienda.departamento as departamento on producto.Iddepartamento = departamento.Iddepartmento
where DATE(detalle.Fecha_alta) = DATE(CURRENT_DATE())
AND detalle.Usuario_alta = usuario
group by producto.Codigo_producto;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SP_RESUMEN_CORTE` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_RESUMEN_CORTE`(IN usuario VARCHAR(50))
BEGIN
     SELECT 
    COUNT(DISTINCT factura.Idfactura) AS CANTIDAD_FACTURAS,
    COALESCE(SUM(pagos.total_pagos), 0) AS TOTAL_PAGOS,
    SUM(factura.monto_total) AS MONTO_FACTURAS,
    SUM(detalle.total_cantidad) AS CANTIDAD_DE_PRODUCTOS_FACTURADOS
FROM 
    proyectotienda.factura AS factura
LEFT JOIN 
    (SELECT Id_factura, SUM(Monto_pagado) AS total_pagos 
     FROM proyectotienda.pagos 
     GROUP BY Id_factura) AS pagos ON factura.Idfactura = pagos.Id_factura
LEFT JOIN 
    (SELECT Id_factura, COUNT(*) AS num_detalles, SUM(cantidad) AS total_cantidad 
     FROM proyectotienda.detalle_factura 
     GROUP BY Id_factura) AS detalle ON factura.Idfactura = detalle.Id_factura
WHERE 
    DATE(factura.Fecha_alta) = DATE(CURRENT_DATE())
    AND factura.Usuario_alta = usuario;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-02-12 10:37:27
