FROM mcr.microsoft.com/mssql/server:2022-latest

ARG MSSQL_SA_PASSWORD

HEALTHCHECK --interval=10s --timeout=3s --start-period=10s --retries=10 \
  CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -Q "SELECT 1" -b