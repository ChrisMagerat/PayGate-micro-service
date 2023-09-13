# Codehesion C# template

to install this template use the following: 
pull the repo, but don't restore or build as this will add bloat the the usage of the template on your system

```bash
dotnet new install /Users/chrismagerat/Desktop/micro-serivce/PayGate
```
the usage will then be:
```bash
dotnet new codehesion-ddd
```

--name has the same result as --output

spin up dbms in a docker container
```bash
 docker run --rm --name pay-gate-microservice -e POSTGRES_PASSWORD=Password1 -e POSTGRES_USER=postgres -p 5433:5432 -d postgres
```

spin up prometheus in a docker container 
```bash
docker run --name prometheus -d -p 9090:9090 -v $(pwd)/prometheus.yml:/etc/prometheus/prometheus.yml prom/prometheus
```
spin up grafana in a docker container
```bash
docker run -d -p 4000:3000 --name=grafana \

--volume grafana-storage:/var/lib/grafana \
grafana/grafana-enterprise
```