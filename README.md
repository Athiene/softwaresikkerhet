# softwaresikkerhet

# Docker Running

1. Først naviger deg til prosjekt mappen.
```bash
cd
```
2. Dermed må du bygge en docker image: docker build -t aspnetcoreapp .
```bash
docker build -t aspnetcoreapp .
```
3. Etter du har laget docker iamge, kan du nå runne imaget i en container.
```bash
docker run -it --rm -p 8080:80 --name myapp aspnetcoreapp
```

Du vil finne nettsiden i ved [Link](https://www.google.com](http://localhost:8080:80)http://localhost:8080:80)
