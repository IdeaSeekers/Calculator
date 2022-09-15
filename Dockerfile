FROM mcr.microsoft.com/dotnet/sdk

COPY . /calculator
RUN apt update && apt install -y postgresql postgresql postgresql-contrib

