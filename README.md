# 🐎 PruebaHAPSA - Sistema de Gestión de Reservas

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-19.0-DD0031?style=flat&logo=angular)
![Architecture](https://img.shields.io/badge/Architecture-DDD-blue)
![License](https://img.shields.io/badge/License-MIT-green)

Sistema integral para la gestión de reservas de restaurante, desarrollado con una arquitectura robusta orientada al dominio (DDD) y un frontend moderno y reactivo.

## 📋 Descripción

PruebaHAPSA es una solución full-stack diseñada para administrar el ciclo de vida completo de reservas (Estándar, VIP y Cumpleaños). El sistema implementa reglas de negocio complejas, validaciones de disponibilidad en tiempo real y flujos de estados inmutables.

### Características Principales
* **Gestión Polimórfica de Reservas:** Soporte para múltiples tipos de reservas con reglas de negocio específicas (Herencia TPH).
    * *Estándar:* Validaciones horarias (19:00 - 23:30) y cupo limitado.
    * *VIP:* Códigos de acceso, mesas preferenciales y horarios extendidos.
    * *Cumpleaños:* Lógica de anticipación para pedidos de torta (48hs) y validación de edad.
* **Ciclo de Vida de Estados:** Flujo estricto (Pendiente → Confirmada → Cancelada / No Asistió).
* **Visualización de Datos:** Listados con filtrado dinámico y paginación en servidor.
* **Persistencia:** Base de datos SQLite con inicialización automática (Seeding).

---

## 🏗️ Arquitectura y Tecnologías

El proyecto sigue los principios de **Clean Architecture** y **Domain-Driven Design (DDD)** para asegurar la escalabilidad y mantenibilidad.

### Backend (.NET 10 Preview)
* **Core:** ASP.NET Core Web API.
* **ORM:** Entity Framework Core (Code First).
* **Database:** SQLite (Portable y ligera).
* **Testing:** NUnit + Moq.
* **Documentation:** Swagger / OpenAPI.
* **Patrones:** Repository Pattern, Dependency Injection, Factory Method.

### Frontend (Angular 19)
* **Estilo:** Standalone Components (Sin NgModules).
* **Routing:** Lazy Loading y parámetros dinámicos.
* **HTTP:** Cliente fuertemente tipado con RxJS.
* **UX:** Feedback visual de estados y validaciones.

---

## 🚀 Guía de Instalación y Ejecución

Sigue estos pasos para levantar el entorno de desarrollo localmente.

### Prerrequisitos
* [.NET SDK 8.0+](https://dotnet.microsoft.com/download) (o superior).
* [Node.js LTS](https://nodejs.org/) (v18 o superior).
* [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`).
* Git.

### 1. Clonar el Repositorio
```bash
git clone [https://github.com/fcpietra/PruebaHAPSA.git](https://github.com/fcpietra/PruebaHAPSA.git)
cd PruebaHAPSA
```
### 2. Configurar y Ejecutar el Backend

El backend incluye un Seeder automático que poblará la base de datos la primera vez que se ejecute.
```Bash

# Navegar a la carpeta de la API
cd PruebaHAPSA.Api

# Restaurar dependencias y ejecutar
dotnet restore
dotnet run
```
- La API estará disponible en: http://localhost:5267
- Swagger UI disponible en: http://localhost:5267/swagger

### 3. Configurar y Ejecutar el Frontend

Abre una nueva terminal en la raíz del proyecto.
```Bash

# Navegar a la carpeta del cliente
cd PruebaHAPSA.Client

# Instalar dependencias
npm install

# Ejecutar servidor de desarrollo
ng serve -o
```
La aplicación abrirá automáticamente en: http://localhost:4200
## 🧪 Testing

El proyecto cuenta con pruebas unitarias para validar la lógica de dominio y la capa de aplicación.
```Bash

# Ejecutar desde la raíz de la solución
dotnet test
```
## 📂 Estructura del Proyecto
```Plaintext

PruebaHAPSA/
├── PruebaHAPSA.Domain/          # Entidades, Enums, Excepciones (Núcleo)
├── PruebaHAPSA.Application/     # Casos de uso, DTOs, Interfaces de Servicios
├── PruebaHAPSA.Infrastructure/  # EF Core, Repositorios, Migraciones
├── PruebaHAPSA.Api/             # Controllers, DI Configuration
├── PruebaHAPSA.Client/          # SPA Angular (Standalone)
└── PruebaHAPSA.Tests/           # Unit Tests (NUnit)
```
