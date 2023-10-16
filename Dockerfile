# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY src src
RUN dotnet add src/DesafioNtt.Api/DesafioNtt.Api.csproj package Microsoft.AspNetCore.Http.Features 
RUN dotnet add src/DesafioNtt.Api/DesafioNtt.Api.csproj package Microsoft.Extensions.Identity.Stores


# Restaura as dependências (é necessário o arquivo de projeto)
RUN find . -name '*.csproj' -type f -exec dirname {} \; | xargs -I {} dotnet restore {}

RUN dotnet ef database update -p src/DesafioNtt.Infrastructure -s src/DesafioNtt.Api -v --context ApplicationDbContext
RUN dotnet ef database update -p src/DesafioNtt.Identity -s src/DesafioNtt.Api -v --context IdentityDataContext

# Constrói o projeto
RUN dotnet build src/DesafioNtt.Api -c Release -o out

# Publica o aplicativo
FROM build AS publish
RUN dotnet publish src/DesafioNtt.Api -c Release -o out

# Define a imagem de tempo de execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/out .

# Define o ponto de entrada para a aplicação
ENTRYPOINT ["dotnet", "DesafioNtt.Api.dll"]
