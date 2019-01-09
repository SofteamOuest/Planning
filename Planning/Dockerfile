FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

WORKDIR /app
EXPOSE 52966
EXPOSE 44353

FROM microsoft/dotnet:2.1-sdk AS build

RUN curl -sL https://deb.nodesource.com/setup_8.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm

WORKDIR /src

COPY "Planning.csproj" /src/

RUN dotnet restore "Planning.csproj"

COPY . .

RUN dotnet build "Planning.csproj" -c Release -o /app

FROM build AS publish

RUN dotnet publish "Planning.csproj" -c Release -o /app

FROM base AS final

WORKDIR /app

COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Planning.dll"]