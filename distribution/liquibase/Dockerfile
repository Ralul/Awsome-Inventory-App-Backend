FROM liquibase/liquibase:latest

RUN lpm add postgresql --global

COPY liquibase.properties /liquibase/changelog/
COPY db/db.changelog-main.xml /liquibase/changelog/
COPY db/changelog /liquibase/changelog/changelog