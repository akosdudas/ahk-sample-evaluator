FROM mcr.microsoft.com/dotnet/core/aspnet:2.1.14-stretch-slim as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1.607-stretch AS build
WORKDIR /src
COPY ["evaluator/evaluator.csproj", "evaluator/"]
COPY ["ahk.common/ahk.common.csproj", "ahk.common/"]
RUN dotnet restore "evaluator/evaluator.csproj"
COPY ["evaluator", "evaluator"]
COPY ["ahk.common", "ahk.common"]
COPY ["starter-code", "starter-code"]
WORKDIR "/src/evaluator"
RUN dotnet build "evaluator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "evaluator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

CMD cp ./homework-sample/bin/Debug/netcoreapp2.1/homework-sample.dll /app || (echo "Solution application not found - Nem talalhato a megoldas alkalmazas"; exit 1) \
	&& dotnet "/app/evaluator.dll"
