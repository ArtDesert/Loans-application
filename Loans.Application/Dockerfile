#Dockerfile реализовывает многоуровневую сборку.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
LABEL maintainer="DigitalCompetitionSchool"
WORKDIR /src
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /src
EXPOSE 2023
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "Loans.Application.Host.dll"]