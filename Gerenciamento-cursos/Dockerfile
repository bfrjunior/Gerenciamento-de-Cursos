FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gerenciamento-cursos/Gerenciamento-cursos.csproj", "Gerenciamento-cursos/"]
RUN dotnet restore "Gerenciamento-cursos/Gerenciamento-cursos.csproj"
COPY . .
WORKDIR "/src/Gerenciamento-cursos"
RUN dotnet build "Gerenciamento-cursos.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY ["Gerenciamento-cursos/Gerenciamento-cursos.csproj", "Gerenciamento-cursos/"]
RUN dotnet restore "Gerenciamento-cursos/Gerenciamento-cursos.csproj"
COPY . .
WORKDIR "/src/Gerenciamento-cursos"
RUN dotnet publish "Gerenciamento-cursos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Gerenciamento-cursos.dll"]