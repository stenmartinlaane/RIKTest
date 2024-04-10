## Registry system backend


## EF Core migrations

Install tooling

~~~bash
dotnet tool update -g dotnet-ef
~~~

Run from solution folder

~~~bash
dotnet ef migrations --project App.DAL.EF --startup-project WebApp add initialmigration --context AppDbContext
dotnet ef database   --project App.DAL.EF --startup-project WebApp update --context AppDbContext
~~~
~~~bash
dotnet ef database   --project App.DAL.EF --startup-project WebApp drop --context AppDbContext
~~~