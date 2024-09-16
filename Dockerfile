# Usa la imagen de .NET 8.0 SDK para la fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo .csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia todo el contenido del proyecto y compílalo
COPY . ./
RUN dotnet publish -c Release -o /out

# Usa la imagen de .NET 8.0 Runtime para la fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Expone el puerto 80
EXPOSE 80

# Configura el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "StoreApi.dll"]