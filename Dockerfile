# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ./*.sln ./
COPY DNATestingSystem.Repository.NhanVT/*.csproj DNATestingSystem.Repository.NhanVT/
COPY DNATestingSystem.Services.NhanVT/*.csproj DNATestingSystem.Services.NhanVT/
COPY DNATestingSystem.APIServices.BE.NhanVT/*.csproj DNATestingSystem.APIServices.BE.NhanVT/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/DNATestingSystem.APIServices.BE.NhanVT
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:7199
EXPOSE 7199
ENTRYPOINT ["dotnet", "DNATestingSystem.APIServices.BE.NhanVT.dll"]