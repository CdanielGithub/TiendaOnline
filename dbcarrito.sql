-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 15-03-2024 a las 02:25:56
-- Versión del servidor: 10.4.25-MariaDB
-- Versión de PHP: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `dbcarrito`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alumnos`
--

CREATE TABLE `alumnos` (
  `id_alumno` int(11) NOT NULL,
  `nombre_alumno` varchar(40) NOT NULL,
  `edad` int(11) DEFAULT NULL,
  `materia` varchar(40) DEFAULT NULL,
  `grado` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `alumnos`
--

INSERT INTO `alumnos` (`id_alumno`, `nombre_alumno`, `edad`, `materia`, `grado`) VALUES
(1, 'Carlos Daniel', 22, 'Historia', 4),
(2, 'Juan Sanchez', 23, 'Lengua Española', 2),
(3, 'Maria Segura', 24, 'Mercadotecnia', 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `carrito`
--

CREATE TABLE `carrito` (
  `idCarrito` int(11) NOT NULL,
  `idCliente` int(11) DEFAULT NULL,
  `idProducto` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categoria`
--

CREATE TABLE `categoria` (
  `idCategoria` int(11) NOT NULL,
  `descripcion` varchar(100) DEFAULT NULL,
  `activo` bit(1) DEFAULT b'1',
  `fechaRegistro` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `categoria`
--

INSERT INTO `categoria` (`idCategoria`, `descripcion`, `activo`, `fechaRegistro`) VALUES
(1, 'Electrónica', b'1', '2023-06-30 10:13:59'),
(2, 'Hogar y Jardín', b'1', '2023-06-30 10:13:59'),
(3, 'Salud y Belleza', b'1', '2023-06-30 10:13:59'),
(4, 'Alimentos y Bebidas', b'1', '2023-06-30 10:13:59'),
(5, 'Deportes', b'1', '2023-06-30 10:13:59'),
(6, 'Libros y Medios', b'1', '2023-06-30 10:13:59'),
(7, 'Electrónica', b'1', '2023-06-30 12:42:31'),
(8, 'Hogar y Jardín', b'1', '2023-06-30 12:42:31'),
(9, 'Salud y Belleza', b'1', '2023-06-30 12:42:31'),
(10, 'Alimentos y Bebidas', b'1', '2023-06-30 12:42:31'),
(11, 'Deportes', b'1', '2023-06-30 12:42:31'),
(12, 'Libros y Medios', b'1', '2023-06-30 12:42:31');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente`
--

CREATE TABLE `cliente` (
  `idCliente` int(11) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  `correo` varchar(50) DEFAULT NULL,
  `clave` varchar(150) DEFAULT NULL,
  `reestablecer` bit(1) DEFAULT b'0',
  `fechaRegistro` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalle_venta`
--

CREATE TABLE `detalle_venta` (
  `idDetalleVenta` int(11) NOT NULL,
  `idVenta` int(11) DEFAULT NULL,
  `idProducto` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `distrito`
--

CREATE TABLE `distrito` (
  `idDistrito` varchar(6) NOT NULL,
  `descripcion` varchar(45) NOT NULL,
  `idProvincia` varchar(4) NOT NULL,
  `idDepartamento` varchar(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `marca`
--

CREATE TABLE `marca` (
  `idMarca` int(11) NOT NULL,
  `descripcion` varchar(100) DEFAULT NULL,
  `activo` bit(1) DEFAULT b'1',
  `fechaRegistro` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `marca`
--

INSERT INTO `marca` (`idMarca`, `descripcion`, `activo`, `fechaRegistro`) VALUES
(1, 'Sony', b'1', '2023-06-30 10:33:03'),
(2, 'Hp', b'1', '2023-06-30 10:33:03'),
(3, 'LG', b'1', '2023-06-30 10:33:03'),
(4, 'Hyundai', b'1', '2023-06-30 10:33:03'),
(5, 'Canon', b'1', '2023-06-30 10:33:03'),
(16, 'Sony', b'1', '2023-06-30 12:42:31'),
(17, 'Hp', b'1', '2023-06-30 12:42:31'),
(18, 'LG', b'1', '2023-06-30 12:42:31'),
(19, 'Hyundai', b'1', '2023-06-30 12:42:31'),
(20, 'Canon', b'1', '2023-06-30 12:42:31');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `producto`
--

CREATE TABLE `producto` (
  `idProducto` int(11) NOT NULL,
  `nombre` varchar(500) DEFAULT NULL,
  `descripcion` varchar(500) DEFAULT NULL,
  `idMArca` int(11) DEFAULT NULL,
  `idCategoria` int(11) DEFAULT NULL,
  `precio` decimal(10,2) DEFAULT 0.00,
  `stock` int(11) DEFAULT NULL,
  `rutaImagen` varchar(100) DEFAULT NULL,
  `nombreImagen` varchar(100) DEFAULT NULL,
  `activo` bit(1) DEFAULT b'1',
  `fechaRegistro` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `provincia`
--

CREATE TABLE `provincia` (
  `idProvincia` varchar(4) NOT NULL,
  `descripcion` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `provincia`
--

INSERT INTO `provincia` (`idProvincia`, `descripcion`) VALUES
('0101', 'Santo Domingo'),
('0102', 'Santiago'),
('0103', 'San Juan');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  `correo` varchar(50) DEFAULT NULL,
  `clave` varchar(150) DEFAULT NULL,
  `reestablecer` bit(1) DEFAULT b'1',
  `activo` bit(1) DEFAULT b'1',
  `fechaRegistro` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `nombre`, `apellidos`, `correo`, `clave`, `reestablecer`, `activo`, `fechaRegistro`) VALUES
(1, 'test.nombre', 'test.apellido', 'test@example.com', '289160DB0D9F39F9AE1754C4EC9C16F90B50E32E09C5FB5481AE642B3D3D1A36', b'1', b'1', '2023-06-30 09:28:49'),
(2, 'test.nombre', 'test.apellido', 'test@example.com', '289160DB0D9F39F9AE1754C4EC9C16F90B50E32E09C5FB5481AE642B3D3D1A36', b'1', b'1', '2023-06-30 09:30:15'),
(3, 'test.nombre', 'test.apellido', 'test@example.com', '289160DB0D9F39F9AE1754C4EC9C16F90B50E32E09C5FB5481AE642B3D3D1A36', b'1', b'1', '2023-06-30 12:42:31');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `venta`
--

CREATE TABLE `venta` (
  `idVenta` int(11) NOT NULL,
  `idCliente` int(11) DEFAULT NULL,
  `totalProducto` int(11) DEFAULT NULL,
  `montoTotal` decimal(10,2) DEFAULT NULL,
  `contacto` varchar(50) DEFAULT NULL,
  `idDistrito` varchar(10) DEFAULT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `direccion` varchar(50) DEFAULT NULL,
  `idTransaccion` varchar(50) DEFAULT NULL,
  `fechaVenta` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `alumnos`
--
ALTER TABLE `alumnos`
  ADD PRIMARY KEY (`id_alumno`);

--
-- Indices de la tabla `carrito`
--
ALTER TABLE `carrito`
  ADD PRIMARY KEY (`idCarrito`);

--
-- Indices de la tabla `categoria`
--
ALTER TABLE `categoria`
  ADD PRIMARY KEY (`idCategoria`);

--
-- Indices de la tabla `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`idCliente`);

--
-- Indices de la tabla `detalle_venta`
--
ALTER TABLE `detalle_venta`
  ADD PRIMARY KEY (`idDetalleVenta`);

--
-- Indices de la tabla `marca`
--
ALTER TABLE `marca`
  ADD PRIMARY KEY (`idMarca`);

--
-- Indices de la tabla `producto`
--
ALTER TABLE `producto`
  ADD PRIMARY KEY (`idProducto`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- Indices de la tabla `venta`
--
ALTER TABLE `venta`
  ADD PRIMARY KEY (`idVenta`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `carrito`
--
ALTER TABLE `carrito`
  MODIFY `idCarrito` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `categoria`
--
ALTER TABLE `categoria`
  MODIFY `idCategoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `cliente`
--
ALTER TABLE `cliente`
  MODIFY `idCliente` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `detalle_venta`
--
ALTER TABLE `detalle_venta`
  MODIFY `idDetalleVenta` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `marca`
--
ALTER TABLE `marca`
  MODIFY `idMarca` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `producto`
--
ALTER TABLE `producto`
  MODIFY `idProducto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `venta`
--
ALTER TABLE `venta`
  MODIFY `idVenta` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
