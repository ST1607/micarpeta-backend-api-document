#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MiCarpeta.Document/MiCarpeta.Document.Presentation.csproj", "MiCarpeta.Document/"]
COPY ["MiCarpeta.Document.Application/MiCarpeta.Document.Application.csproj", "MiCarpeta.Document.Application/"]
COPY ["MiCarpeta.Document.Domain/MiCarpeta.Document.Domain.csproj", "MiCarpeta.Document.Domain/"]
COPY ["MiCarpeta.Document.Repository/MiCarpeta.Document.Repository.csproj", "MiCarpeta.Document.Repository/"]
COPY ["MiCarpeta.Document.Common/MiCarpeta.Document.Common.csproj", "MiCarpeta.Document.Common/"]
COPY ["MiCarpeta.Document.Domain.Entities/MiCarpeta.Document.Domain.Entities.csproj", "MiCarpeta.Document.Domain.Entities/"]
RUN dotnet restore "MiCarpeta.Document/MiCarpeta.Document.Presentation.csproj"
COPY . .
WORKDIR "/src/MiCarpeta.Document"
RUN dotnet build "MiCarpeta.Document.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiCarpeta.Document.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiCarpeta.Document.Presentation.dll"]
