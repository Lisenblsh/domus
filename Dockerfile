FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /workspace

# Установим ping для отладки и bash
RUN apt-get update && \
    apt-get install -y iputils-ping curl redis-tools && \
    dotnet tool install --global dotnet-ef && \
    export PATH="$PATH:/root/.dotnet/tools"
