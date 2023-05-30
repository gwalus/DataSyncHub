
# DataSyncHub

Test project to learn and explore difference popular tools with .NET


## Features

- store fake users in mongodb
- cache users in get users endpoint with redis cache service
- periodically fetch fake random user from api-ninjas (every 1 minute) with used background service
- log information to elasticsearch using serilog
- display indexed logs in kibana
- all of them works on docker container
- base modular monolith architecture
- services health checks


## Screenshots

- Swagger users response
![Swagger users response](https://raw.githubusercontent.com/gwalus/DataSyncHub/main/img/swagger_users_response.png)

- Kibana saved users logs
![Kibana saved users logs](https://raw.githubusercontent.com/gwalus/DataSyncHub/main/img/kibana_logs.png)

- Services on docker
![Services on docker](https://raw.githubusercontent.com/gwalus/DataSyncHub/main/img/docker_services.png)

- Cached users in redis
![Cached users in redis](https://raw.githubusercontent.com/gwalus/DataSyncHub/main/img/redis_cache.png)


## Tech Stack

**Client UI:** Swagger UI

**Server:** .NET 7, ASP.NET Core WebAPI

**Data:** MongoDB, Redis, ElasticSearch

**Tools:** Docker, Serilog, Kibana


## Run Locally

Clone the project

```bash
  git clone https://github.com/gwalus/DataSyncHub.git
```

Go to the https://api-ninjas.com/profile, copy API Key and pasty it to appsettings.json

```json
  "ApiNinjas": 
  {
    "Url": "https://api.api-ninjas.com/v1/",
    "Token": "PASTE_YOUR_TOKEN_HERE"
  }
```

Run services on docker

```bash
  docker compose up --build -d
```

Check health of services going to

```bash
  http://localhost:8080/_health
```

Open app in web browser and try to get fake users

```bash
  http://localhost:8080/swagger/index.html
```

Check logs in kibana dashboards, firstly create an default index

```bash
  http://localhost:5601/app/management/kibana/indexPatterns
```


## Run Locally (without dockerize .NET app)

Run services on docker

```bash
  docker compose -f docker-compose.development.yml up -d
```

