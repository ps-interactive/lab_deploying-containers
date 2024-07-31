# syntax=docker/dockerfile:1

# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder

COPY . /source/
WORKDIR /source

# This is the architecture you’re building for, which is passed in by the builder.
# Placing it here allows the previous steps to be cached across architectures.
ARG TARGETARCH

# build and publish the app to /app
# Leverage a cache mount to /root/.nuget/packages so that subsequent builds don't have to re-download packages.
# If TARGETARCH is "amd64", replace it with "x64" - "x64" is .NET's canonical name for this and "amd64" doesn't
#   work in .NET 6.0.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runner

WORKDIR /app

# copy the published app
COPY --from=builder /app .

# 8.x+ port 8080=>80 https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/8.0/aspnet-port
ENV ASPNETCORE_HTTP_PORTS=80

# what the container runs:
ENTRYPOINT ["dotnet", "api.dll"]
