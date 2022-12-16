# auth0-IaC

Just playing a bit with Auth0 🔐, Terraform 🏗️ , `auth0-deploy-cli` 🧑‍💻 and Pulumi 🟪

## Prerequisites

- [Auth0 Account](https://auth0.com/)

- [A Machine to Machine application](https://auth0.com/docs/get-started/auth0-overview/create-applications/machine-to-machine-apps) on your `auth0` tenant with all permissions.

## Running commands

First, you need to update the [.env](src/.env) file with your application credentials as described in `Prerequisites`:

Go to the [src](src/) folder and then type one of the following commands (depending on the infrastructure you want to deploy):

1) The command below will publish all infrastructure in your tenant with `terraform`:

```bash
docker-compose up --build terraform
```

After that, get the `Sample App` credentials in [terraform.tfstate](src/terraform/terraform.tfstate) file and update `# Sample app` section of [.env](src/.env) file.

2) The command below will publish all infrastructure in your tenant with `auth0-deploy-cli`:

```bash
docker-compose up --build auth0-cli
```

3) The command below will publish all infrastructure in your tenant with `pulumi`:

```bash
docker-compose up --build pulumi
```

After that, get the `Sample App` credentials in Auth0 portal (go to `Applications` > `Applications` > `Sample app client` > `Settings` > `Basic Information`) . Then, update `# Sample app` section of [.env](src/.env) file.

As `sample-app` uses `https` endpoints, you need to create a localhost certificate, got to the [scripts](src/scripts) folder and type the following command:

```bash
./gen-cert.sh
```

After that, you need to trust the certificate `servercert.crt` that was created in [certificates](src/certificates) folder. To do that, you can follow one of the following ways (depending on your OS):

[Mac Os](https://tosbourn.com/getting-os-x-to-trust-self-signed-ssl-certificates/)

[Windows](https://superuser.com/questions/370217/trust-ssl-certificate-to-local-system-account)

[Linux](https://unix.stackexchange.com/questions/90450/adding-a-self-signed-certificate-to-the-trusted-list)

After your infra is up, you're able to run the `Regular Web` application:

```bash
docker-compose up --build sample-app
```

Open the client in your favorite browser:

```bash
http://localhost/
```
