
https://github.com/mmercan/Turquoise.HealthChecks/tree/master/HealthChecks/Turquoise.HealthChecks.Mongo

Usage

----

```csharp
using Turquoise.HealthChecks.Mongo

   services.AddHealthChecks()
   ....
    .AddMongoHealthCheck(Configuration["Mongodb:ConnectionString"])
   ....

```

Output
----
servers

databases