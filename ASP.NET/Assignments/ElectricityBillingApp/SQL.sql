CREATE DATABASE ElectricityBillDB

USE ElectricityBillDB

CREATE TABLE ElectricityBill (
    consumer_number VARCHAR(20) PRIMARY KEY,
    consumer_name   VARCHAR(50),
    units_consumed  INT,
    bill_amount     FLOAT
)

SELECT * FROM ElectricityBill