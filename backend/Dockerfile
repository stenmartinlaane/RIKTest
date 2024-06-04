FROM mcr.microsoft.com/dotnet/sdk:8.0.203 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
# copy ALL the projects
COPY WebApp/*.csproj ./WebApp/

COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.Contracts.BLL/*.csproj ./App.Contracts.BLL/
COPY App.Contracts.DAL/*.csproj ./App.Contracts.DAL/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.DTO/*.csproj ./App.DTO/
COPY App.Test/*.csproj ./App.Test/

COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.Contracts.BLL/*.csproj ./Base.Contracts.BLL/
COPY Base.Contracts.DAL/*.csproj ./Base.Contracts.DAL/
COPY Base.Contracts.Domain/*.csproj ./Base.Contracts.Domain/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.DTO/*.csproj ./Base.DTO/

COPY Helpers/*.csproj ./Helpers/

RUN dotnet restore

# copy everything else and build app
# copy all the projects
COPY WebApp/. ./WebApp/

COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.Contracts.BLL/. ./App.Contracts.BLL/
COPY App.Contracts.DAL/. ./App.Contracts.DAL/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.Domain/. ./App.Domain/
COPY App.DTO/. ./App.DTO/
COPY App.Test/. ./App.Test/

COPY Base.BLL/. ./Base.BLL/
COPY Base.Contracts.BLL/. ./Base.Contracts.BLL/
COPY Base.Contracts.DAL/. ./Base.Contracts.DAL/
COPY Base.Contracts.Domain/. ./Base.Contracts.Domain/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.DTO/. ./Base.DTO/

COPY Helpers/. ./Helpers/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0.3 AS runtime
WORKDIR /app
#ENV ConnectionStrings:DefaultConnection="Host=host.docker.internal;Port=7899;Database=backend;Username=postgres;Password=postgres"
COPY --from=build /app/WebApp/out ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApp.dll"]