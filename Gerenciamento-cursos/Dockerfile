# Estágio de Build: Usa a imagem SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos da solução e restaura todas as dependências
# Isso assume que o seu Dockerfile está em Gerenciamento-cursos/Dockerfile
# e a solução inteira está no diretório acima (/)
COPY ../. .

# Restaura o projeto principal
# O diretório de trabalho é /src, então Gerenciamento-cursos.csproj deve ser copiado para /src/Gerenciamento-cursos/
RUN dotnet restore "Gerenciamento-cursos/Gerenciamento-cursos.csproj"

# Constrói o projeto
WORKDIR /src/Gerenciamento-cursos
RUN dotnet build "Gerenciamento-cursos.csproj" -c Release -o /app/build

# Estágio de Publicação: Cria o pacote final de produção
FROM build AS publish
WORKDIR /src/Gerenciamento-cursos
RUN dotnet publish "Gerenciamento-cursos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final: Usa a imagem runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=publish /app/publish .

# Define a porta do container
EXPOSE 8080

# Define a entrada do container para rodar a DLL
ENTRYPOINT ["dotnet", "Gerenciamento-cursos.dll"]