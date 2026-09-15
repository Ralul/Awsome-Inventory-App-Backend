#!/bin/bash

DB_USER="${DB_USER:-inventoryAppUser}"
DB_PASSWORD="${DB_PASSWORD:-1ChangeThisForProduction!}"

echo DB_USER: ${DB_USER}
echo DB_PASSWORD: ${DB_PASSWORD}

psql -U postgres <<-EOSQL
    DROP USER IF EXISTS "${DB_USER}";
    CREATE USER "${DB_USER}" WITH PASSWORD '${DB_PASSWORD}';
    DROP DATABASE IF EXISTS inventory_app;
    CREATE DATABASE inventory_app OWNER "${DB_USER}";
EOSQL