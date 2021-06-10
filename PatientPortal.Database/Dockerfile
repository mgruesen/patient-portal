FROM mcr.microsoft.com/mssql/server:2019-latest
RUN mkdir -p /opt/scripts
COPY ./init.sql /opt/scripts/init.sql

ENV MSSQL_SA_PASSWORD=ThisIsATempPassword!
ENV ACCEPT_EULA=Y

RUN /opt/mssql/bin/sqlserver --accept-eula \
    && sleep 30 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${MSSQL_SA_PASSWORD} -d master -i /opt/scripts/init.sql