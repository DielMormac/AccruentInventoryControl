terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0.1"
    }
  }
}

provider "docker" {
  host = "npipe:////.//pipe//docker_engine" # For Windows Docker Engine
}

# Build and run the API container
resource "docker_image" "api_image" {
  name         = "accruent-inventory-control-api:latest"
  build {
    context    = "${path.module}/../API"
    dockerfile = "Dockerfile"
  }
}

resource "docker_container" "api_container" {
  name  = "accruent-inventory-control-api"
  image = docker_image.api_image.name

  ports {
    internal = 8080
    external = 5116
  }

  env = [
    "ASPNETCORE_ENVIRONMENT=Development"
  ]
}

# Build and run the UI container
resource "docker_image" "ui_image" {
  name         = "accruent-inventory-control-ui:latest"
  build {
    context    = "${path.module}/../UI/accruent-inventory-control"
    dockerfile = "Dockerfile"
  }
}

resource "docker_container" "ui_container" {
  name  = "accruent-inventory-control-ui"
  image = docker_image.ui_image.name

  ports {
    internal = 5173
    external = 5173
  }

  env = [
    "REACT_APP_API_BASE_URL=http://localhost:5116/api/v1"
  ]

  depends_on = [docker_container.api_container]
}