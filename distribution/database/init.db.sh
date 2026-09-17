#!/bin/bash

DB_USER="${DB_USER:-aia_user}"
DB_PASSWORD="${DB_PASSWORD:-1ChangeThisForProduction!}"

echo DB_USER: ${DB_USER}
echo DB_PASSWORD: ${DB_PASSWORD}

psql -U postgres <<-EOSQL
    DROP USER IF EXISTS "${DB_USER}";
    CREATE USER "${DB_USER}" WITH PASSWORD '${DB_PASSWORD}';
    DROP DATABASE IF EXISTS awsome_inventory_app;
    CREATE DATABASE awsome_inventory_app OWNER "${DB_USER}";
EOSQL