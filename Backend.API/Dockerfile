#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Backend.API/Backend.API.csproj", "Backend.API/"]
COPY ["Backend.Common/Backend.Common.csproj", "Backend.Common/"]
COPY ["Mediator/Mediator.csproj", "Mediator/"]
COPY ["Backend.Models/Backend.Models.csproj", "Backend.Models/"]
COPY ["Backend.Services/Backend.Services.csproj", "Backend.Services/"]
COPY ["Backend.Data/Backend.Data.csproj", "Backend.Data/"]
RUN dotnet restore "Backend.API/Backend.API.csproj"
COPY . .
WORKDIR "/src/Backend.API"
RUN dotnet build "Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Backend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.API.dll"]