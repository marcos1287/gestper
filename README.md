# Gestper 🚀

> Aplicación web para la gestión eficiente del personal de organizaciones, desarrollada con ASP.NET Core MVC.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9-512BD4)](https://dotnet.microsoft.com/download)



## 📖 Descripción

Gestper es una aplicación web desarrollada como parte de un trabajo universitario. Su propósito es gestionar la información del personal de una organización de manera eficiente y segura, ofreciendo una interfaz intuitiva y funcionalidades completas para la administración de recursos humanos.

## ✨ Características

- **Gestión de usuarios y roles** - Control de acceso basado en permisos
- **Operaciones CRUD completas** - Creación, lectura, actualización y eliminación de registros
- **Interfaz responsiva** - Diseño adaptable a diferentes dispositivos
- **Autenticación y autorización** - Sistema seguro de acceso a la plataforma
- **Dashboard analítico** - Visualización de datos clave del personal
- **Exportación de informes** - Generación de reportes en diferentes formatos

## 🛠️ Tecnologías Utilizadas

- **Backend:** C#, ASP.NET Core MVC
- **Frontend:** HTML5, CSS3, JavaScript, Bootstrap
- **Base de datos:** SQL Server
- **ORM:** Entity Framework Core
- **Control de versiones:** Git

## 🧰 Estructura del Proyecto

```
Gestper/
├── Controllers/       # Controladores que manejan las solicitudes HTTP
├── Models/            # Clases que representan los datos y entidades
├── Views/             # Vistas para la interfaz de usuario
├── Data/              # Contexto de la base de datos y migraciones
├── wwwroot/           # Archivos estáticos (CSS, JS, imágenes)
├── Properties/        # Archivos de configuración del proyecto
├── Program.cs         # Punto de entrada de la aplicación
└── appsettings.json   # Configuración de la aplicación
```

## 📋 Requisitos Previos

Para trabajar con este proyecto, necesitarás tener instalado:

- [.NET SDK](https://dotnet.microsoft.com/download) (.NET 6.0 o superior)
- [GitHub Desktop](https://desktop.github.com/)
- [JetBrains Rider](https://www.jetbrains.com/rider/) o [Visual Studio](https://visualstudio.microsoft.com/)
- Una cuenta en GitHub

## 🚀 Primeros Pasos

### Clonar el Repositorio

**Usando GitHub Desktop:**

1. Abrir GitHub Desktop
2. Seleccionar `Archivo > Clonar repositorio...`
3. En la pestaña URL, ingresar: `https://github.com/CesarValenzuela157/gestper.git`
4. Elegir la ubicación local para el proyecto
5. Hacer clic en `Clonar`

**Usando Git CLI:**

```bash
git clone https://github.com/CesarValenzuela157/gestper.git
cd gestper
```

### Configurar y Ejecutar el Proyecto

**Con JetBrains Rider:**

1. Abrir JetBrains Rider
2. Seleccionar `Archivo > Abrir...` y navegar hasta la carpeta del proyecto
3. Abrir el archivo de solución `.sln`
4. Restaurar los paquetes NuGet (Rider suele hacerlo automáticamente)
5. Presionar `Shift + F10` o hacer clic en el botón de ejecución para iniciar la aplicación

**Con Visual Studio:**

1. Abrir Visual Studio
2. Seleccionar `Archivo > Abrir > Proyecto/Solución` y navegar hasta la carpeta del proyecto
3. Abrir el archivo de solución `.sln`
4. Hacer clic derecho en la solución en el Explorador de soluciones y seleccionar `Restaurar paquetes NuGet`
5. Presionar `F5` para compilar y ejecutar la aplicación

**Con .NET CLI:**

```bash
dotnet restore
dotnet build
dotnet run
```

La aplicación estará disponible en `https://localhost:5001` o la URL que indique la consola.

## 🤝 Cómo Contribuir

### 1. Crear una Rama

**Con GitHub Desktop:**

1. Ir a `Branch > New Branch...`
2. Nombrar la rama (ej. `feature/nueva-funcionalidad`)
3. Hacer clic en `Create Branch`

**Con Git CLI:**

```bash
git checkout -b feature/nueva-funcionalidad
```

### 2. Realizar Cambios

1. Implementa los cambios necesarios en el código
2. Prueba tus cambios localmente

### 3. Confirmar Cambios

**Con GitHub Desktop:**

1. Revisa los archivos modificados
2. Escribe un mensaje descriptivo en el campo `Summary`
3. Haz clic en `Commit to feature/nueva-funcionalidad`

**Con Git CLI:**

```bash
git add .
git commit -m "Descripción detallada de los cambios realizados"
```

### 4. Enviar Cambios al Repositorio Remoto

**Con GitHub Desktop:**

1. Hacer clic en `Push origin`

**Con Git CLI:**

```bash
git push origin feature/nueva-funcionalidad
```

### 5. Crear un Pull Request

1. En GitHub, navega al repositorio
2. Haz clic en `Compare & pull request`
3. Completa la información requerida
4. Haz clic en `Create pull request`

## 🧪 Mantener tu Rama Actualizada

Para sincronizar tu rama con los últimos cambios de la rama principal:

**Con GitHub Desktop:**

1. Cambiar a la rama `main`
2. Hacer clic en `Fetch origin`
3. Cambiar de nuevo a tu rama de trabajo
4. Hacer clic en `Branch > Merge into current branch...` y seleccionar `main`

**Con Git CLI:**

```bash
git checkout main
git pull
git checkout feature/nueva-funcionalidad
git merge main
```

## 📚 Recursos Adicionales

- [Documentación de ASP.NET Core](https://docs.microsoft.com/es-es/aspnet/core/)
- [Tutoriales de GitHub Desktop](https://docs.github.com/es/desktop)
- [Guía de Entity Framework Core](https://docs.microsoft.com/es-es/ef/core/)
- [Clonar un repositorio usando GitHub Desktop - Video Tutorial](https://www.youtube.com/watch?v=PoZJGJvOlVc)

## 🔒 Licencia

Este proyecto está bajo la [Licencia MIT](LICENSE).


<p align="center">
  Desarrollado con ❤️ como parte de un proyecto universitario
</p>
