## Registry System

#### Overview:
This project consists of a backend API built in C# .NET for creating and managing events, with a frontend developed using Next.js. The system utilizes a PostgreSQL database for data storage. The backend is containerized using Docker for easy deployment, while the frontend is deployed on Vercel.

#### Note:
- The frontend was developed before I took courses in Next.js, so the code may look a little unconventional.
- This project was my first introduction to Domain-Driven Design (DDD), so the code might not fully follow best practices.

## deployment
1. have docker version 24.0.6 or later installed https://docs.docker.com/get-docker/
2. have git installed https://git-scm.com/book/en/v2/Getting-Started-Installing-Git

3. start docker
4. run the following commands:
```bash
https://github.com/stenmartinlaane/RIKTest.git
cd RIKTest
docker-compose up --build
```
access frontend at http://localhost:3000/login

access backend api documentation at http://localhost:7898/swagger/index.html
