## Registry system backend


## EF Core migrations

Install tooling

~~~bash
dotnet tool update -g dotnet-ef
dotnet tool update -g dotnet-aspnet-codegenerator
~~~

Run from solution folder

~~~bash
dotnet ef migrations --project App.DAL.EF --startup-project WebApp add initialmigration --context AppDbContext
dotnet ef database   --project App.DAL.EF --startup-project WebApp update --context AppDbContext
~~~
~~~bash
dotnet ef database   --project App.DAL.EF --startup-project WebApp drop --context AppDbContext
~~~

## Generate admin controllers
~~~bash
cd WebApp

dotnet aspnet-codegenerator controller -name EventController -actions -m App.Domain.Event -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FirmController -actions -m App.Domain.Firm -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ParticipantEventController -actions -m App.Domain.ParticipantEvent -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PaymentMethodController -actions -m App.Domain.PaymentMethod -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PersonController -actions -m App.Domain.Person -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

cd ..
~~~

## Generate api controllers
 ~~~bash
cd WebApp

dotnet aspnet-codegenerator controller -name EventController -async -api -m App.Domain.Event -dc AppDbContext --relativeFolderPath Controllers
dotnet aspnet-codegenerator controller -name FirmController -async -api -m App.Domain.Firm -dc AppDbContext --relativeFolderPath Controllers
dotnet aspnet-codegenerator controller -name ParticipantEventController -async -api -m App.Domain.ParticipantEvent -dc AppDbContext --relativeFolderPath Controllers
dotnet aspnet-codegenerator controller -name PaymentMethodController -async -api -m App.Domain.PaymentMethod -dc AppDbContext --relativeFolderPath Controllers
dotnet aspnet-codegenerator controller -name PersonController -async -api -m App.Domain.Person -dc AppDbContext --relativeFolderPath Controllers

cd ..
~~~