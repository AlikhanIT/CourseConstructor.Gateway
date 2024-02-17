FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CourseConstructor.API/CourseConstructor.API.csproj", "CourseConstructor.API/"]
RUN dotnet restore "CourseConstructor.API/CourseConstructor.API.csproj"
COPY . .
WORKDIR "/src/CourseConstructor.API"
RUN dotnet build "CourseConstructor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CourseConstructor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourseConstructor.API.dll"]
