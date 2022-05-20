#!/bin/bash
terraform init
terraform plan -out plan-result -refresh-only
terraform apply plan-result
