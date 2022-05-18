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
  logo_url    = "https://raw.githubusercontent.com/luizhlelis/luizlelis.com/main/src/assets/logo-black.png"
  favicon_url = "https://raw.githubusercontent.com/luizhlelis/luizlelis.com/main/src/assets/logo-black.png"
  colors {
    primary         = "#0059d6"
    page_background = "#000000"
  }
  universal_login {
    body = "<!DOCTYPE html><html><head>{%- auth0:head -%}</head><body>{%- auth0:widget -%}</body></html>"
  }
}

resource "auth0_client" "auth0_sample_app_client" {
  name                                = var.CLIENT_NAME
  description                         = var.CLIENT_DESCRIPTION
  app_type                            = "regular_web"
  custom_login_page_on                = true
  is_first_party                      = true
  is_token_endpoint_ip_header_trusted = true
  token_endpoint_auth_method          = "client_secret_post"
  oidc_conformant                     = true
  callbacks                           = ["https://localhost:5001/v1/auth/response-oidc"]
  allowed_origins                     = ["https://localhost"]
  grant_types                         = ["authorization_code"]
  allowed_logout_urls                 = ["https://localhost:5001/"]
  web_origins                         = ["https://localhost"]
  jwt_configuration {
    lifetime_in_seconds = 300
    secret_encoded      = true
    alg                 = "RS256"
    scopes = {
      foo = "bar"
    }
  }
  refresh_token {
    rotation_type                = "rotating"
    expiration_type              = "expiring"
    leeway                       = 15
    token_lifetime               = 1296000
    infinite_idle_token_lifetime = true
    infinite_token_lifetime      = false
    idle_token_lifetime          = 84600
  }
  client_metadata = {
    foo = "zoo"
  }
}
