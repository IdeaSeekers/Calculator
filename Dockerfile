FROM mcr.microsoft.com/dotnet/sdk

COPY . /calculator
ENV CALCULATOR_BACKEND_PATH=/calculator
RUN apt update && apt install -y postgresql postgresql postgresql-contrib

