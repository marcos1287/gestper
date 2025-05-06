Gestper

Gestper es una aplicación web desarrollada como parte de un trabajo universitario. Su propósito es [aquí puedes agregar una descripción más detallada del objetivo de la aplicación, por ejemplo: "gestionar la información del personal de una organización de manera eficiente y segura"].

Características
Gestión de usuarios y roles.
Creación, lectura, actualización y eliminación (CRUD) de registros.
Interfaz de usuario intuitiva y responsiva.
Autenticación y autorización de usuarios.
[Agrega aquí otras funcionalidades destacadas de tu aplicación.]

Estructura del Proyecto
El proyecto está organizado en los siguientes directorios principales:
Controllers/: Contiene los controladores que manejan las solicitudes HTTP y la lógica de negocio.
Models/: Define las clases que representan los datos y las entidades del dominio.
Views/: Incluye las vistas que se renderizan en la interfaz de usuario.
Data/: Configuración del contexto de la base de datos y las migraciones.
wwwroot/: Archivos estáticos como CSS, JavaScript e imágenes.
Properties/: Archivos de configuración del proyecto.
Program.cs: Punto de entrada de la aplicación.
appsettings.json: Archivo de configuración de la aplicación.

Tecnologías Utilizadas
Lenguaje de programación: C#
Framework: ASP.NET Core MVC
Base de datos: [Especifica la base de datos utilizada, por ejemplo: SQL Server, SQLite, etc.]
Frontend: HTML, CSS, JavaScript
Control de versiones: Git

🧰 Requisitos Previos
GitHub Desktop instalado: Descargar

JetBrains Rider instalado: Descargar

Cuenta en GitHub y haber iniciado sesión en GitHub Desktop.

Tener instalado el SDK de .NET correspondiente al proyecto (por ejemplo, .NET 6 o .NET 7).

🚀 Clonar el Repositorio con GitHub Desktop
Abrir GitHub Desktop.

En la barra de menú, seleccionar Archivo > Clonar repositorio....

En la pestaña URL, ingresar la URL del repositorio:

bash
Copiar
Editar
https://github.com/CesarValenzuela157/gestper.git
Elegir la ubicación local donde se clonará el proyecto.

Hacer clic en Clonar.

Referencia: Clonar y bifurcar repositorios desde GitHub Desktop

🧑‍💻 Abrir el Proyecto en JetBrains Rider
Abrir JetBrains Rider.

Seleccionar Archivo > Abrir... y navegar hasta la carpeta donde se clonó el proyecto.

Seleccionar el archivo de solución .sln correspondiente al proyecto y hacer clic en Abrir.

Si es la primera vez que se abre el proyecto, Rider puede preguntar si se confía en el proyecto. Seleccionar Confiar en el proyecto.

Referencia: Clonar desde GitHub - Guía de JetBrains

⚙️ Ejecutar el Proyecto
En Rider, asegurarse de que el proyecto se haya restaurado correctamente. Si es necesario, Rider puede solicitar restaurar los paquetes NuGet; aceptar la restauración.

Configurar el proyecto de inicio si hay múltiples proyectos en la solución.

Presionar Shift + F10 o hacer clic en el botón de ejecución (▶️) para compilar y ejecutar la aplicación.

Una vez iniciada, la aplicación estará disponible en https://localhost:5001 o la URL que indique la consola.

🤝 Colaborar y Contribuir
Crear una Rama para Trabajar
En GitHub Desktop, ir a la pestaña Branch > New Branch....

Nombrar la nueva rama (por ejemplo, feature/nueva-funcionalidad) y hacer clic en Create Branch.

Realizar Cambios y Confirmarlos
Abrir el proyecto en Rider y realizar los cambios necesarios.

Una vez realizados los cambios, en GitHub Desktop, se mostrarán los archivos modificados.

Ingresar un mensaje descriptivo en el campo Summary (por ejemplo, "Agrega nueva funcionalidad X").

Hacer clic en Commit to feature/nueva-funcionalidad.

Referencia: Confirmar y revisar cambios en tu proyecto en GitHub Desktop

Enviar Cambios al Repositorio Remoto
Después de confirmar los cambios, hacer clic en Push origin para enviar los cambios al repositorio en GitHub.

Referencia: Enviar cambios a GitHub desde GitHub Desktop

Crear una Solicitud de Extracción (Pull Request)
En GitHub Desktop, después de enviar los cambios, hacer clic en Create Pull Request.

Se abrirá una página en el navegador para completar los detalles de la solicitud de extracción.

Ingresar un título y descripción para la solicitud, luego hacer clic en Create Pull Request.

🧪 Sincronizar Cambios del Repositorio Principal
Para mantener tu rama actualizada con los últimos cambios del repositorio principal:

En GitHub Desktop, cambiar a la rama main.

Hacer clic en Fetch origin para obtener los últimos cambios.

Cambiar de nuevo a tu rama de trabajo (por ejemplo, feature/nueva-funcionalidad).

Hacer clic en Branch > Merge into current branch... y seleccionar main para fusionar los cambios.

📺 Recursos Adicionales
Clonar un repositorio usando GitHub Desktop - Video Tutorial

Crear y enviar un repositorio usando GitHub Desktop - Video Tutorial

Si necesitas más detalles o asistencia adicional, no dudes en preguntar.

Licencia
Este proyecto está bajo la licencia MIT.
