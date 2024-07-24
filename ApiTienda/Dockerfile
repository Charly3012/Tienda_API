FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR app
EXPOSE 8080
EXPOSE 8081
ENV DB_CONNECTION_STRING="Server=sqlServerDocker; Database=ApiTienda;User ID=sa; Password=MiContrasena*1234; Trusted_Connection=false;TrustServerCertificate=true;MultipleActiveResultSets=true"

#Copiar los archivos del proyecto
COPY ./*.csproj ./
RUN dotnet restore

#Copiar el resto de las cosas
COPY . .
RUN dotnet publish -c Release -o out

#Build 
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ApiTienda.dll"]