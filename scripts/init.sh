service postgresql start

su - postgres -c 'psql -f /calculator/DatabaseQueries/CreateTables.sql'
su - postgres -c 'psql -f /calculator/DatabaseQueries/SetPassword.sql';

export DATABASE_USER=postgres
export DATABASE_PASSWORD=abc123
export DATABASE_NAME=calculator

dotnet dev-certs https
cd /calculator && dotnet build && cd ServerAPI && dotnet run
