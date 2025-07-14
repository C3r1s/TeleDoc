FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["TeleDoc.API/TeleDoc.API.csproj", "TeleDoc.API/"]
COPY ["TeleDoc.Application/TeleDoc.Application.csproj", "TeleDoc.Application/"]
COPY ["TeleDoc.Data/TeleDoc.Data.csproj", "TeleDoc.Data/"]
COPY ["TeleDoc.Domain/TeleDoc.Domain.csproj", "TeleDoc.Domain/"]
COPY ["TeleDoc.Infrastructure/TeleDoc.Infrastructure.csproj", "TeleDoc.Infrastructure/"]

RUN dotnet restore "TeleDoc.API/TeleDoc.API.csproj"

COPY . .

WORKDIR "/src/TeleDoc.API"
RUN dotnet build "TeleDoc.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet tool install --global dotnet-ef --version 9.0.*
RUN dotnet publish "TeleDoc.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

COPY --from=publish /root/.dotnet/tools /root/.dotnet/tools

ENV PATH="$PATH:/root/.dotnet/tools"

RUN echo '#!/bin/sh \n\
dotnet ef database update --project ../TeleDoc.Data \n\
exec dotnet TeleDoc.API.dll' > entrypoint.sh && \
    chmod +x entrypoint.sh

ENV ConnectionStrings__DefaultConnection="Host=db;Database=postgres;Username=postgres;Password=teledoc;Port=5438"

ENTRYPOINT ["./entrypoint.sh"]