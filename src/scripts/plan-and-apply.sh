#!/bin/bash
terraform init
terraform plan -out plan-result
terraform apply plan-result
