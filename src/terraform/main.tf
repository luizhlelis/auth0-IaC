terraform {
  required_providers {
    auth0 = {
      source  = "auth0/auth0"
      version = "0.30.2"
    }
  }
}

provider "auth0" {}

resource "auth0_branding" "auth_tenant_branding" {
  logo_url    = "https://upload.wikimedia.org/wikipedia/commons/0/04/Terraform_Logo.svg"
  favicon_url = "https://upload.wikimedia.org/wikipedia/commons/0/04/Terraform_Logo.svg"
  colors {
    primary         = "#0059d6"
    page_background = "#000000"
  }
}

resource "auth0_client" "auth0_sample_app_client" {
  logo_uri                            = "https://upload.wikimedia.org/wikipedia/commons/0/04/Terraform_Logo.svg"
  name                                = var.CLIENT_NAME
  description                         = var.CLIENT_DESCRIPTION
  app_type                            = "regular_web"
  custom_login_page_on                = true
  is_first_party                      = true
  is_token_endpoint_ip_header_trusted = true
  token_endpoint_auth_method          = "client_secret_post"
  oidc_conformant                     = true
  callbacks                           = [var.CALLBACK]
  allowed_origins                     = [var.ALLOWED_ORIGINS]
  grant_types                         = [var.GRANT_TYPES]
  allowed_logout_urls                 = [var.ALLOWED_LOGOUT_URLS]
  web_origins                         = [var.WEB_ORIGINS]
  jwt_configuration {
    lifetime_in_seconds = 300
    secret_encoded      = true
    alg                 = "RS256"
    scopes = {
      foo = "bar"
    }
  }
  client_metadata = {
    foo = "zoo"
  }
}
