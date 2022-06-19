FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.16 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.16 AS build
WORKDIR /src
COPY ["src/Werter.Api.LojaOnline/Werter.Api.LojaOnline.csproj", "Werter.Api.LojaOnline/"]
COPY ["Werter.LojaOnline.Dominio/Werter.LojaOnline.Dominio.csproj", "Werter.LojaOnline.Dominio/"]
COPY ["Werter.LojaOnline.Compartilhado/Werter.LojaOnline.Compartilhado.csproj", "Werter.LojaOnline.Compartilhado/"]
RUN dotnet restore "src/Werter.Api.LojaOnline/Werter.Api.LojaOnline.csproj"
COPY . .
WORKDIR "/src/Werter.Api.LojaOnline"
RUN dotnet build "Werter.Api.LojaOnline.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Werter.Api.LojaOnline.csproj" --self-contained  -c Release -r linux-musl-x64 -p:PublishTrimmed=true -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Werter.Api.LojaOnline.dll"]
