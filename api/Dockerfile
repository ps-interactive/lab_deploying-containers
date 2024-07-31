# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder

COPY . /source/

WORKDIR /source

# build and publish the app to /app
RUN dotnet publish --use-current-runtime --self-contained false -o /app





FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runner

WORKDIR /app

# copy the published app into this prod (runtime/runner) image
COPY --from=builder /app .

# 8.x+ port 8080=>80 https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/8.0/aspnet-port
ENV ASPNETCORE_HTTP_PORTS=80

# what the container runs when using this runner image:
ENTRYPOINT ["dotnet", "api.dll"]
