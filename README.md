# Softwaresikkerhet

## Docker Running

1. Først naviger deg til prosjekt mappen.
```bash
cd
```
2. Dermed må du bygge en docker image.
```bash
docker build -t aspnetcoreapp .
```
3. Etter du har laget docker imaget, kan du nå runne imaget i en container.
```bash
winpty docker run -it --rm -p 8080:80 --name myapp aspnetcoreapp
```

Du vil finne nettsiden ved [localhost:8080]
