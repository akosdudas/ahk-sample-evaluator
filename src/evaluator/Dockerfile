FROM mcr.microsoft.com/dotnet/core/runtime:2.1.14-alpine3.10 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1.802-alpine3.10 AS build
WORKDIR /src
COPY ["evaluator/evaluator.csproj", "evaluator/"]
RUN dotnet restore "evaluator/evaluator.csproj"
COPY ["evaluator", "evaluator"]
WORKDIR "/src/evaluator"
RUN dotnet build "evaluator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "evaluator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

CMD dotnet "/app/evaluator.dll"
