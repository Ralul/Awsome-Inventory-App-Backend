FROM liquibase/liquibase:latest

RUN lpm add postgresql --global

COPY liquibase.docker.properties /liquibase/
COPY db/db.changelog-main.xml /liquibase/changelog/
COPY db/changelog /liquibase/changelog/changelog

WORKDIR /liquibase
CMD ["liquibase"]