# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de solução e projetos
COPY ["CursoDez/CursoDez.sln", "./CursoDez/"]
COPY ["CursoDez.Application/CursoDez.Application.csproj", "CursoDez.Application/"]
COPY ["CursoDez.Domain/CursoDez.Domain.csproj", "CursoDez.Domain/"]
COPY ["CursoDez.Infrastructure/CursoDez.Infrastructure.csproj", "CursoDez.Infrastructure/"]
COPY ["CursoDez/CursoDez.API.csproj", "./CursoDez/"]

# Restaurar as dependências
RUN dotnet restore "CursoDez/CursoDez.sln"

# Copiar todos os arquivos do repositório para o diretório de build
COPY . .

# Compilar o projeto
RUN dotnet publish "CursoDez/CursoDez.API.csproj" -c Release -o /app/publish --no-self-contained --framework net8.0

# Etapa 2: Executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Definir diretório de trabalho para a aplicação
WORKDIR /app

# Copiar os artefatos de build da etapa anterior
COPY --from=build /app/publish .

# Expor a porta que a aplicação vai rodar
EXPOSE 80

# Definir o ponto de entrada para a aplicação
ENTRYPOINT ["dotnet", "CursoDez.API.dll"]
