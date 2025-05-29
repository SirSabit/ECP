# ECP (E-Commmerce Platform API)
This is an assesment project for BPN Ödeme.

The structure built as DDD like architecture. The project is DB First. Please be sure to add this table to your local postgre db and set your Db connection from appsettings accordingly. 

CREATE TABLE OrderLogs (
    Id SERIAL PRIMARY KEY,
    Success BOOLEAN NOT NULL,
    Message TEXT NOT NULL,
    ResponseData TEXT NOT NULL,
    LogDate TIMESTAMP
);

## ENDPOINT CURLS
Complete pre order endpoint

curl --location 'https://localhost:44373/api/v1/orders/complete' \
--header 'Content-Type: application/json' \
--data '{
    "orderId": "03bf609d-c5d0-4f30-a58a-ad2656f49e05"
}'

Create a pre order endpoint
curl --location 'https://localhost:44373/api/v1/orders/create' \
--header 'Content-Type: application/json' \
--data '{
    "amount" : 120,
    "orderId": ""
}'

Get products endpoint
curl --location 'https://localhost:44373/api/v1/products'
