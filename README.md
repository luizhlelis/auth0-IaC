# auth0-IaC

Just playing a bit with Auth0 🔐, Terraform 🏗️ , and `auth0-cli` 🧑‍💻

## Prerequisites

- [Auth0 Account](https://auth0.com/)

- [A Machine to Machine application](https://auth0.com/docs/get-started/auth0-overview/create-applications/machine-to-machine-apps) on `auth0` with all permissions.

## Terraform

Before `init`,`plan` and `apply` the terraform files, first you need to store the credentials in environment variables:

```bash
export AUTH0_DOMAIN="<domain>"
export AUTH0_CLIENT_ID="<client-id>"
export AUTH0_CLIENT_SECRET="<client-secret>"
```

> **NOTE:** The `client-id` and `client-secret` above are different from the app credentials, those credentials are gonna be created in the next section.

To override [variables.tf](./src/terraform/variables.tf) (in your pipeline for example), just add `TF_VAR_` prefix to the variables you want to override:

```bash
TF_VAR_CLIENT_DESCRIPTION overrides CLIENT_DESCRIPTION
```
