FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pomekhoustoychivost/Pomekhoustoychivost.csproj", "Pomekhoustoychivost/"]
RUN dotnet restore "Pomekhoustoychivost/Pomekhoustoychivost.csproj"
COPY . .
WORKDIR "/src/Pomekhoustoychivost"
RUN dotnet build "Pomekhoustoychivost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pomekhoustoychivost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pomekhoustoychivost.dll"]
