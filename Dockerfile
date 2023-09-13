FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build

WORKDIR /app
COPY "./ExampleProject.sln" "./ExampleProject.sln"
COPY "./src/Application/Application.csproj" "./src/Application/Application.csproj"
COPY "./src/Domain/Domain.csproj" "./src/Domain/Domain.csproj"
COPY "./src/Infrastructure/Infrastructure.csproj" "./src/Infrastructure/Infrastructure.csproj"
COPY "./src/API/API.csproj" "./src/API/API.csproj"

RUN dotnet restore  "./src/API/API.csproj"
COPY . .
WORKDIR /app/src/API
RUN dotnet publish -c Release -o /app/published-app 

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

RUN apk update \
    && apk add icu-libs

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT=prod

ENTRYPOINT [ "dotnet", "/app/API.dll" ]
