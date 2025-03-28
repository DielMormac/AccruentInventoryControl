terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
  }
}

provider "azurerm" {
  features {}
}

# Resource Group
resource "azurerm_resource_group" "rg" {
  name     = "inventory-control_group"
  location = "Canada Central" # Change to a supported Azure region
}

# App Service Plan
resource "azurerm_service_plan" "app_service_plan" {
  name                = "accruent-app-service-plan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Windows" # Specify the OS type
  sku_name            = "F1"      # Free tier SKU
}

# API Windows Web App
resource "azurerm_windows_web_app" "api_web_app" {
  name                = "accruent-api-service"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.app_service_plan.id

  site_config {
    always_on  = true
    ftps_state = "Disabled"
  }

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "1"
    "ASPNETCORE_ENVIRONMENT"   = "Development"
  }
}

# UI Windows Web App
resource "azurerm_windows_web_app" "ui_web_app" {
  name                = "accruent-ui-service"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.app_service_plan.id

  site_config {
    always_on  = true
    ftps_state = "Disabled"
  }

  app_settings = {
    "REACT_APP_API_BASE_URL" = "https://${azurerm_windows_web_app.api_web_app.default_hostname}/api/v1"
  }
}

# Output the Web App URLs
output "api_web_app_url" {
  value = azurerm_windows_web_app.api_web_app.default_hostname
}

output "ui_web_app_url" {
  value = azurerm_windows_web_app.ui_web_app.default_hostname
}