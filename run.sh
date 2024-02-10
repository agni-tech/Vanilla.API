now=$(date +"%T")
echo "Inicializando processo às $now"

docker-compose down
docker-compose rm
docker-compose pull
docker-compose build --no-cache
docker-compose up -d --force-recreate

now=$(date +"%T")
echo "Processo finalizado às $now"