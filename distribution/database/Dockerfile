FROM postgres:15-alpine

EXPOSE 5432
COPY init.db.sh /docker-entrypoint-initdb.d/
RUN chmod +x /docker-entrypoint-initdb.d/init.db.sh