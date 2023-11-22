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
-- Table structure for table `formato_factura`
--

DROP TABLE IF EXISTS `formato_factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `formato_factura` (
  `idformato_factura` int NOT NULL AUTO_INCREMENT,
  `Usuaro_alta` varchar(45) NOT NULL,
  `Fecha_alta` datetime NOT NULL,
  `Usuario_baja` varchar(45) DEFAULT NULL,
  `Fecha_baja` datetime DEFAULT NULL,
  `Cabeza_factura` varchar(2000) NOT NULL,
  `Cuerpo_factura` varchar(2000) NOT NULL,
  `Estado` int NOT NULL COMMENT '1-Activo\\n2-Inactivo',
  PRIMARY KEY (`idformato_factura`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formato_factura`
--

LOCK TABLES `formato_factura` WRITE;
/*!40000 ALTER TABLE `formato_factura` DISABLE KEYS */;
INSERT INTO `formato_factura` VALUES (1,'danr','2022-11-26 00:00:00',NULL,NULL,'<!DOCTYPE html>\n<html>\n<head>\n    <meta charset=\"utf-8\"> <!-- utf-8 works for most cases -->\n    <meta name=\"viewport\" content=\"width=device-width\"> <!-- Forcing initial-scale shouldn\'t be necessary -->\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"> <!-- Use the latest (edge) version of IE rendering engine -->\n    <meta name=\"x-apple-disable-message-reformatting\">  <!-- Disable auto-scale in iOS 10 Mail entirely -->\n\n    <title>Plantilla Factura</title>\n <style>\n        div.polaroid\n            {{\n\n            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);\n            text-align: center;\n        }}\n\n        div.container {{\n            padding: 40px;\n        }}\n\n        /* iPhone 4, 4S, 5, 5S, 5C, and 5SE */\n        @media only screen and (min-device-width: 320px) and (max-device-width: 374px) {{\n            u~div .container {{\n                min-width: 320px !important;\n            }}\n        }}\n\n        /* iPhone 6, 6S, 7, 8, and X */\n        @media only screen and (min-device-width: 375px) and (max-device-width: 413px) {{\n            u~div .container {{\n                min-width: 375px !important;\n            }}\n        }}\n\n        /* iPhone 6+, 7+, and 8+ */\n        @media only screen and (min-device-width: 414px) {{\n            u~div .container {{\n                min-width: 414px !important;\n            }}\n        }}\n    </style>\n</head>\n\n<body>\n\n    <div style=\"max-width: 400px; margin: 0 auto;\" class=\"polaroid\">\n        <div style=\"max-width: 400px; margin: 0 auto;\" class=\"container\">\n            <font size=\"4\" color=\"black\">\n\n                <p align=\"Center\">\n                 {0}\n                </p>\n                <br>\n                <p align=\"left\">\n\n                    Factura # {1}<br>\n                    Tipo de Pago: {2}<br>\n                    Fecha: {3} hora: {4}<br>\n                    Cliente: {5}<br>\n                    Cajero: {6}<br>\n\n                </p>\n                <hr>\n                <hr>','  <br>\n                <br>\n                <br>\n                <p align=\"left\">\n                    Totales en C$\n                </p>\n                <table style=\"width: 100%;\">\n                    <thead>\n                        <tr>\n                            <th></th>\n                            <th></th>\n                        </tr>\n                    </thead>\n                    <tbody>\n                        <tr align=\"right\">\n\n                            <td>Subtotal:</td>\n                            <td>{0}</td>\n\n                        </tr>\n                        <tr align=\"right\">\n\n                            <td>Total:</td>\n                            <td>{1}</td>\n\n                        </tr>\n                    </tbody>\n\n                </table>\n\n                <p align=\"left\">\n                    Totales en $\n                </p>\n                <table style=\"width: 100%;\">\n                    <thead>\n                        <tr>\n                            <th></th>\n                            <th></th>\n                        </tr>\n                    </thead>\n                    <tbody>\n                        <tr align=\"right\">\n\n                            <td>T/C:</td>\n                            <td>{2}</td>\n\n                        </tr>\n                        <tr align=\"right\">\n\n                            <td>Subtotal:</td>\n                            <td>{3}</td>\n\n                        </tr>\n                        <tr align=\"right\">\n\n                            <td>Total:</td>\n                            <td>{4}</td>\n\n                        </tr>\n                    </tbody>\n\n                </table>\n\n                <br>\n                <br>\n                <br>\n                <p align=\"Center\">\n\n                   {5}\n\n                </p>\n            </font>\n\n\n\n        </div>\n\n    </div>\n\n\n</body>\n\n</html>',1);
/*!40000 ALTER TABLE `formato_factura` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-22 14:17:25
