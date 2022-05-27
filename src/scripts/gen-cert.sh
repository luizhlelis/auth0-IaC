#!/bin/bash
FILE_CERT_NAME=servercert
FILE_KEY_NAME=serverkey
if [ -f "../certificates/$FILE_CERT_NAME.crt" ]; then
    echo "Cert and Key already exist"
else
echo "Cert and Key does not exist, trying to create new ones..."
  mkdir ../certificates
  openssl req -new -subj "/C=US/ST=California/CN=localhost" \
      -newkey rsa:2048 -nodes -keyout "$FILE_KEY_NAME.pem" -out "$FILE_CERT_NAME.csr"
  openssl x509 -req -days 365 -in "$FILE_CERT_NAME.csr" -signkey "$FILE_KEY_NAME.pem" -out "../certificates/$FILE_CERT_NAME.crt" -extfile "self-signed-cert.ext"
  openssl pkcs12 -inkey "$FILE_KEY_NAME.pem" -in "../certificates/$FILE_CERT_NAME.crt" -export -out "../certificates/$FILE_CERT_NAME.pfx" -passout pass:
fi
