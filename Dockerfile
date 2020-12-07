FROM node:12-slim as build-env-front
WORKDIR /usr/src/app

COPY wwwapp/package*.json ./
RUN npm install

COPY wwwapp/. .
RUN npm run build

########################################################
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
RUN dotnet test
RUN dotnet ef database update

########################################################
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 5000:80
COPY --from=build-env /app/out .
COPY --from=build-env-front /usr/src/app/build ./wwwroot

VOLUME data
COPY --from=build-env /app/database.db /data/

ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "CustomerManagement.dll"]