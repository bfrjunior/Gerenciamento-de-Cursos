# Est�gio de Build: Usa a imagem SDK para compilar a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos da solu��o e restaura todas as depend�ncias
# Isso assume que o seu Dockerfile est� em Gerenciamento-cursos/Dockerfile
# e a solu��o inteira est� no diret�rio acima (/)
COPY ../. .

# Restaura o projeto principal
# O diret�rio de trabalho � /src, ent�o Gerenciamento-cursos.csproj deve ser copiado para /src/Gerenciamento-cursos/
RUN dotnet restore "Gerenciamento-cursos/Gerenciamento-cursos.csproj"

# Constr�i o projeto
WORKDIR /src/Gerenciamento-cursos
RUN dotnet build "Gerenciamento-cursos.csproj" -c Release -o /app/build

# Est�gio de Publica��o: Cria o pacote final de produ��o
FROM build AS publish
WORKDIR /src/Gerenciamento-cursos
RUN dotnet publish "Gerenciamento-cursos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Est�gio Final: Usa a imagem runtime para executar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=publish /app/publish .

# Define a porta do container
EXPOSE 8080

# Define a entrada do container para rodar a DLL
ENTRYPOINT ["dotnet", "Gerenciamento-cursos.dll"]
