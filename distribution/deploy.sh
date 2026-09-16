echo executing deploy.sh

echo building database image...
docker build -t aia-db ./database/

echo building liquibase image
docker build -t aia-liquibase ./liquibase/

echo create docker network
docker network create aia-network

echo run database
docker run -d \
  --network aia-network \
  --name aia-db \
  -e POSTGRES_PASSWORD="your-strong-password-here" \
  -p 5432:5432 \
  aia-db
  
sleep 10
  
echo run liquibase
docker run --rm \
  --network aia-network \
  --name aia-liquibase \
  -w /liquibase \
  aia-liquibase \
  liquibase update \
  --url='jdbc:postgresql://aia-db:5432/awsome_inventory_app?encrypt=false' \
  --username=aia_user \
  --password='1ChangeThisForProduction!'