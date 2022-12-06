cd financial_management_service/
dotnet publish -c Release -o published
cd ..
docker image rm -f monoleak_api
docker build -t monoleak_api .
docker rm -f monoleak_api
docker run -it -d -p 5259:5259 --name monoleak_api monoleak_api
docker rmi $(docker images -f "dangling=true" -q)