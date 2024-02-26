FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CourseConstructors.CourseConstructors.API/CourseConstructors.CourseConstructors.API.csproj", "src/CourseConstructors.CourseConstructors.API/"]
RUN dotnet restore "src/CourseConstructors.CourseConstructors.API/CourseConstructors.CourseConstructors.API.csproj"
COPY . .
WORKDIR "/src/src/CourseConstructors.CourseConstructors.API"
RUN dotnet build "CourseConstructors.CourseConstructors.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CourseConstructors.CourseConstructors.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourseConstructors.CourseConstructors.API.dll"]
