
🏠 Million Luxury – Prueba Técnica Backend .NET  
Autor: Jairo Alonso Bernal Bolívar  
Rol: Senior Backend Developer (.NET)  
Fecha: Agosto 2025  

---

🧠 Enfoque técnico y arquitectura  
Este proyecto fue desarrollado en .NET 8.0, siguiendo los principios de arquitectura limpia y diseño hexagonal, con el objetivo de garantizar:  
- Separación clara de responsabilidades  
- Escalabilidad y mantenibilidad  
- Facilidad para pruebas unitarias e integración  
- Adaptabilidad ante cambios funcionales  

Estructura del proyecto:  

MillionLuxury.RealEstate.API/  
├── Application/  
│   ├── Interfaces/       → Contratos de servicios y casos de uso  
│   ├── Services/         → Lógica de aplicación y orquestación  
│   ├── DTOs/             → Objetos de transferencia de datos  
│   ├── Mappings/         → Configuración de AutoMapper  
│   └── Validators/       → Validaciones con FluentValidation  
├── Domain/  
│   ├── Entities/         → Entidades del negocio  
│   └── Interfaces/       → Repositorios y reglas del dominio  
├── Infrastructure/  
│   ├── Persistence/      → Contexto de base de datos y configuración  
│   └── Repositories/     → Implementaciones de acceso a datos  
├── API/  
│   ├── Controllers/      → Endpoints públicos y manejo de rutas  
│   └── Middlewares/      → Validaciones, logging y manejo de errores  
├── Tests/  
│   └── UnitTests/  
│       ├── Controllers/  → Pruebas de endpoints  
│       ├── Services/     → Pruebas de lógica de negocio  
│       └── TestUtil/     → Utilidades para pruebas  
├── appsettings.json      → Configuración de entorno  
├── Program.cs            → Punto de entrada de la aplicación  
├── RealEstateApi.csproj  → Configuración del proyecto (.NET 8)  

---

⚙️ Tecnologías utilizadas  
- .NET 8  
- C#  
- SQL Server  
- Entity Framework Core  
- AutoMapper  
- FluentValidation  
- nUnit + Moq  
- Swagger  
- ILogger (con integración de logs en consola)  

---

📈 Gestión de rendimiento La solución incorpora prácticas clave para garantizar un rendimiento óptimo:
- Uso de consultas optimizadas con Entity Framework y proyecciones controladas
- Paginación en filtros para evitar sobrecarga de datos
- Validaciones previas para evitar operaciones innecesarias en base de datos
- Logs estructurados con ILogger para identificar cuellos de botella
- Separación de responsabilidades que facilita el monitoreo y la escalabilidad
- Preparación para integración con herramientas como Application Insights o Azure Monitor
---

📌 Funcionalidades implementadas  
- Crear propiedad  
- Agregar imágenes a propiedad  
- Actualizar propiedad (con mapeo condicional y DTOs opcionales)  
- Cambiar precio de propiedad  
- Listar propiedades con filtros dinámicos y paginación  
- Validaciones de entrada y manejo de errores estructurado  
- Logs técnicos para auditoría y debugging  

---

🔐 Seguridad aplicada  
La solución incorpora prácticas esenciales para proteger la integridad de los datos y el comportamiento de la API:

- Validación estricta de entrada mediante DTOs y FluentValidation  
- Manejo controlado de excepciones con middlewares personalizados  
- Separación de capas que evita exposición directa de entidades del dominio  
- Uso de GUIDs como identificadores únicos para evitar enumeración de recursos  
- Logs estructurados para trazabilidad sin exponer datos sensibles  
- Preparación para integración con autenticación y autorización si el entorno lo requiere

---

🚀 Ejecución local  
1. Clonar el repositorio  
2. Restaurar paquetes con dotnet restore  
3. Ejecutar migraciones con dotnet ef database update  
4. Iniciar el proyecto con dotnet run desde la carpeta /API  
5. Acceder a Swagger en https://localhost:{puerto}/swagger  

---

🧪 Pruebas unitarias  
Ejecutar pruebas con:  
dotnet test tests/UnitTests  

Cobertura:  
- Casos de uso  
- Validaciones  
- Mapeo condicional  
- Comportamiento ante entradas inválidas  
- Simulación de repositorios con Moq  

---

📨 Ejemplos de uso (Swagger o Postman)  

Crear propiedad:  
POST /api/properties  
Content-Type: application/json  

{  
  "name": "Apartamento en Medellín",  
  "price": 350000000,  
  "address": "Cra 45 #12-34",  
  "ownerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"  
}  

Actualizar propiedad (solo campos enviados):  
PUT /api/properties/d4a6b7c8-2e3f-4d1a-8c9b-5d6e7f8a1bbb  
Content-Type: application/json  

 360000000  
 

Filtrar propiedades:  
POST /api/properties/filter  
Content-Type: application/json  

{  
  "idOwner": "3fa85f64-5717-4562-b3fc-2c963f66afa6",  
  "pageNumber": 1,  
  "pageSize": 3  
}  

---

📚 Documentación y soporte  
- Guía de despliegue incluida  
- Respaldo de base de datos SQL Server  
- Logs integrados con ILogger para trazabilidad  
- Código comentado y estructurado para facilitar onboarding  
- Checklist técnico para revisión y entrega  
- Pruebas unitarias organizadas por capa funcional  

