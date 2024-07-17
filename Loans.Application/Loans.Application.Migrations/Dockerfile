FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
LABEL maintainer="DigitalCompetitionSchool"
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Loans.Application.Migrations.dll"]