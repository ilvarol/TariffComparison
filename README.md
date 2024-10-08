
# TariffComparison Web API

This is a .NET 8 web application, fully containerized using Docker. It can be run on any operating system that supports Docker.

## Installation & Running Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/ilvarol/TariffComparison.git
```

### 2. Install Docker
Ensure Docker is installed on the target OS. For Ubuntu, you can follow [this guide](https://docs.docker.com/engine/install/ubuntu/).

### 3. Navigate to the Project Directory
In your terminal, navigate to the directory where the `docker-compose.yml` file is located:
```bash
cd ~/Downloads/TariffComparison
```

### 4. Run the Application
Start the application by running the following command:
```bash
docker compose up
```

### 5. Access the APIs
Once everything is set up, you can test the APIs via the following links:
- [http://localhost:5000/Swagger/index.html](http://localhost:5000/Swagger/index.html)
- [http://localhost:4000/Swagger/index.html](http://localhost:4000/Swagger/index.html)

The application should be ready within seconds.
