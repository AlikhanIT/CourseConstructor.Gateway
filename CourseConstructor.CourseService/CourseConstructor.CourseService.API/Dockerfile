FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CourseConstructor.CourseService.API/CourseConstructor.CourseService.API.csproj", "CourseConstructor.CourseService.API/"]
RUN dotnet restore "CourseConstructor.CourseService.API/CourseConstructor.CourseService.API.csproj"
COPY . .
WORKDIR "/src/CourseConstructor.CourseService.API"
RUN dotnet build "CourseConstructor.CourseService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CourseConstructor.CourseService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourseConstructor.CourseService.API.dll"]
