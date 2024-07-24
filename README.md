# Tienda_API - BackEnd 
Aplicación BackEnd para un sistema de venta en una tienda u otro comercio. 

## Contenido
- [Sobre el proyecto](#sobre-el-proyecto)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Características](#características)
- [Uso con Docker Compose](#uso-con-docker-compose)
- [Despliegue](#despliegue-de-la-API)


## Sobre el proyecto

El objetivo de este proyecto es desarrollar una aplicación de gestión para una tienda de productos, que permita la administración eficiente de ventas y productos. La aplicación proporcionará funcionalidades para:

1. **Gestión de Productos:** Permitir el registro, actualización y eliminación de productos, así como la consulta de su información. Incluye la gestión de categorías para clasificar los productos.

2. **Registro de Ventas:** Facilitar la creación de ventas, donde los usuarios puedan registrar las compras realizadas, asociando productos con sus detalles (precio unitario, cantidad, subtotal) y calcular el total de la venta incluyendo el IVA.

3. **Gestión de Detalles de Venta:** Administrar los detalles de cada venta, garantizando que se registren correctamente los productos involucrados, así como sus precios y cantidades.

4. **Persistencia de Datos:** Utilizar una base de datos SQL Server para almacenar toda la información relacionada con productos, categorías y ventas, asegurando la integridad y disponibilidad de los datos.

5. **Interfaz y Documentación:** Proporcionar una API RESTful que sea fácil de utilizar y documentar con Swagger, permitiendo a los desarrolladores integrarse rápidamente con el sistema y comprender sus funcionalidades.

El sistema busca optimizar el proceso de venta y gestión de productos, ofreciendo una solución robusta y eficiente para la tienda.




## Tecnologías Utilizadas

- ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
- ![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=.net&logoColor=white) 
- ![ASP.NET](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=aspdotnet&logoColor=white) 
- ![Entity Framework](https://img.shields.io/badge/Entity_Framework-9C1D1D?style=for-the-badge&logo=entity-framework&logoColor=white) 
- ![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white) 
- ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white) 


## Características

- Gestión de productos y categorías.
- Registro de ventas con detalles de productos.
- Cálculo de subtotales, totales e IVA en las ventas.

## Uso con Docker Compose ![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
Este proyecto utiliza Docker Compose para facilitar la configuración y el despliegue de la aplicación y la base de datos SQL Server. A continuación se detallan los pasos para levantar la aplicación utilizando Docker Compose.

### Requisitos Previos
- [Docker instalado](https://www.docker.com) en tu máquina.
- [Docker Compose](https://docs.docker.com/compose/) instalado.

### Instrucciones
1. Crear el archivo `docker-compose.yml` con la siguiente estructura

```yml
version: '3.8'

services:
  sqlServerDocker:
    container_name: sql-server-docker
    image: mcr.microsoft.com/mssql/server:2022-latest
    ports:
      - "8006:1433"
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=MiContrasena*1234
    networks:
      - mynetworkapi

  apitienda:
    container_name: apiTienda
    image: charly3012/apitienda:1.0
    environment:
      - DB_CONNECTION_STRING=Server=sqlServerDocker;Database=ApiTienda;User ID=sa;Password=MiContrasena*1234;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=True
    networks:
      - mynetworkapi
    depends_on:
      - sqlServerDocker
    ports:
      - "5001:8080"
      - "5002:8081"

networks:
  mynetworkapi:
```
2. Construir y desplegar los contenedores
   ```bash
   docker-compose up -d
   ```

3. Accede a la aplicación:

Una vez que los contenedores estén corriendo, puedes acceder a la API en el siguiente URL:

API: http://localhost:5001 <br>
Swagger: http://localhost:5001/swagger

### Notas
- Asegúrate de que el puerto 5001 no este siendo utilizado por otras aplicaciones en tu máquina.
- Puedes ajustar las configuraciones de puerto y otras variables de entorno en el archivo docker-compose.yml según tus necesidades.


## Despliegue de la API ![Linux](https://img.shields.io/badge/Linux-FCC624?style=for-the-badge&logo=linux&logoColor=black) ![Ubuntu](https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white)

La API ha sido desplegada en un servidor Linux, el cual es un proyecto personal, utilizando Docker Compose. Puedes acceder a los endpoints de la API y a la interfaz de Swagger para explorar y probar los servicios disponibles.<br>

### Acceso a la API
La API está disponible en los siguientes puertos:

HTTP: http://apolo.myftp.org:5001/api/<Método HTTP><br>
HTTPS: Proximamente...<br>

### Interfaz de Swagger 
Puedes acceder a la documentación interactiva de la API proporcionada por Swagger <br>
[Documentación de la API](http://apolo.myftp.org:5001/index.html)





