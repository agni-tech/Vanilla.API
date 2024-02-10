#wait for the SQL Server to come up
sleep 30s

echo "rodando o script de criacao..."
#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Vanilla@123 -d master -i db-init.sql