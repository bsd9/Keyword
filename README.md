# Keyword

Este proyecto tiene como objetivo crear una API en C# que retorne datos de keywords ordenados por volumen de búsqueda, y un dashboard simple utilizando React y Tailwind CSS que visualiza estos datos.

## Estructura del Proyecto

1. **Backend (API en C#)**: Una API que expone un endpoint llamado GetKeywords para obtener datos de keywords. Los datos están en memoria y se retornan ordenados por volumen de búsqueda.
2. **Frontend (React + Tailwind CSS)**: Un dashboard que muestra un resumen de las métricas y una tabla ordenada por volumen de búsqueda de las keywords.

## Requisitos

### Backend (API en C#)

- La API esta construida con .NET Core.
- Un endpoint llamado GetKeywords que retorna un listado de keywords con los siguientes campos:
  - `keyword`: La palabra clave.
  - `search_volume`: El volumen de búsqueda de la keyword.
- Los datos están en memoria, no se utiliza una base de datos.

### Frontend (React + Tailwind CSS)

- Un dashboard que incluye:
  - **Card de resumen** con métricas totales como:
    - Número total de keywords.
    - Volumen total de búsqueda.
  - **Tabla de keywords**:
    - La tabla muestra las keywords junto con sus volúmenes de búsqueda.
    - La tabla esta ordenada por volumen de búsqueda de mayor a menor.
    - Utiliza Tailwind CSS para el diseño y la responsividad.

# Instalación

## Backend

1. Clona este repositorio en tu máquina local.
2. Navega al directorio del backend.
3. Abre una terminal y ejecuta el siguiente comando para restaurar los paquetes NuGet:
   ```bash
   dotnet restore
4. Luego, ejecuta la aplicación:
   ```bash
   dotnet run
5. La API estará disponible en http://localhost:7147

## Frontend

1. Navega al directorio del frontend.
2. Asegúrate de tener Node.js instalado.
3. Instala las dependencias del proyecto:
   ```bash
   npm install
4. Inicia el servidor de desarrollo:
   ```bash
   npm start
5. El dashboard estará disponible en http://localhost:3000.

# Uso

1. Accede al frontend en http://localhost:3000.
2. La API está disponible en https://localhost:7147/api/Keywords/GetKeywords o en Swagger https://localhost:7147/swagger/index.html.
3. El dashboard mostrará los datos de las keywords con el volumen de búsqueda ordenado de mayor a menor.

# Estructura de la API

El endpoint que la API expone es:

## **GET api/Keywords/GetKeywords**

- **Descripción:** Retorna un listado de keywords con sus datos correspondientes ordenados por volumen de búsqueda.
- **Ejemplo de respuesta:**
  ```json
  [
    {
      "keyword": "React JS",
      "search_volume": 50000,
      "cpc": 1.20
    },
    {
      "keyword": "C# API",
      "search_volume": 30000,
      "cpc": 0.80
    }
  ]

# Estructura del Dashboard

- Card de resumen:** Muestra las métricas totales como el número de keywords y el volumen total de búsqueda.
- Tabla de keywords:** Muestra las keywords con su volumen de búsqueda y CPC, ordenadas de mayor a menor según el volumen de búsqueda.

# Tecnologías utilizadas

## Backend

- C# (ASP.NET Core)
- JSON para la comunicación de datos.

## Frontend

- React
- Tailwind CSS para el diseño responsivo.
