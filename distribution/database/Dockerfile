FROM postgres:15.19
ENV POSTGRES_PASSWORD=ahmedpostgres
EXPOSE 5432
COPY init.db.sh /docker-entrypoint-initdb.d/
RUN chmod +x /docker-entrypoint-initdb.d/init.db.sh