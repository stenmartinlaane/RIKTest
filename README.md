## Registry System

## deployment

have docker version 24.0.6 or later installed https://docs.docker.com/get-docker/
have git installed https://git-scm.com/book/en/v2/Getting-Started-Installing-Git

```bash
https://github.com/stenmartinlaane/RIKTest.git
cd registry-system
```

## Building frontend docker image

have npm 9.8.0 or later installed https://docs.npmjs.com/cli/v8/commands/npm-install

configure dockerfie: frontend/Dockerfile

```bash
cd frontend
npm run build
docker build .
cd ..
```

## Building backend and postgres database together

```bash
cd backend
docker-compose -f registry-backend-postgres.yml build
cd ..
```

make account to dockerhub. Note that free
upload images to dockerhub
download images to hostmachine

run images on host machine:
docker run -e NEXT_PUBLIC_BACKEND_SERVER="http://new_backend_server_address:port" -p 8080:80 frontend_image
docker run backend_db_image
