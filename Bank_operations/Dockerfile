#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BankOperations.Presentation/BankOperations.Presentation.csproj", "BankOperations.Presentation/"]
COPY ["BankOperations.Application/BankOperations.Application.csproj", "BankOperations.Application/"]
COPY ["BankOperations.Domain/BankOperations.Domain.csproj", "BankOperations.Domain/"]
COPY ["BankOperations.Persistence/BankOperations.Persistence.csproj", "BankOperations.Persistence/"]
RUN dotnet restore "BankOperations.Presentation/BankOperations.Presentation.csproj"
COPY . .
WORKDIR "/src/BankOperations.Presentation"
RUN dotnet build "BankOperations.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankOperations.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankOperations.Presentation.dll"]