cd financial_management_service/
dotnet publish -c Release -o published
cd ..
docker image rm -f monoleak_api
docker build -t monoleak_api .
docker rm -f monoleak_api
# docker run -it -d -p 5259:5259 --name monoleak_api monoleak_api

docker run -it -d --name monoleak_api -e VIRTUAL_HOST="api.monoleak.net" -e VIRTUAL_PORT=5259 -e LETSENCRYPT_HOST="api.monoleak.net" -e LETSENCRYPT_EMAIL="anhocva@gmail.com" monoleak_api:latest

docker rmi $(docker images -f "dangling=true" -q)