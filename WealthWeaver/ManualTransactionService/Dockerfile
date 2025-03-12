# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY ManualTransactionService/*.csproj .
RUN dotnet restore

# Copy everything else and build
COPY ManualTransactionService/. .
RUN dotnet publish -c Release -o /app

# Use the official .NET 8 runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Expose port and run the application
EXPOSE 80
ENTRYPOINT ["dotnet", "ManualTransactionService.dll"]