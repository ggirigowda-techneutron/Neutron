# NEUTRON 
---

## Neutron Project Structure
### **Neutron** _Solution_
```

├── ClassLibrary
    ├── Classlibrary.Infrastructure - Infrastructure
    ├── Classlibrary.Crosscutting - Cross cutting utilities
    ├── Classlibrary.Dao - Data access objects with Dapper
    ├── ClassLibrary.Dao.Test - Xunit test for data access objects
    ├── ClassLibrary.Domain - Domain objects
    ├── ClassLibrary.Domain.Test - Xunit test for domain objects
├── Database
   ├── Database.Neutron - Database project  
       ├── Schema - DDL for schema creation
       ├── Data - DML for data loading
├── Middleware
   ├── Middleware.Core.WebApi - Sample core web API with API versioning and swagger 
   ├── Middleware.Core.WebApi.Auth - Sample core web API auth with JWT 
   ├── Middleware.Core.WebApi.Gateway - Sample core web API gateway using Ocelot 
   ├── Middleware.WebApi - Sample .NET standard web API  
└── Solution Items
   ├── Readme.md

```

