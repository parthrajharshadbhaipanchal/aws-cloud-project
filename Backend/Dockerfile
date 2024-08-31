#Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /Backend
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /Backend
COPY --from=build-env /Backend/out .

EXPOSE 5124

ENTRYPOINT ["dotnet", "JobApplicationTracker.dll"]
