CREATE DATABASE calculator;
\c calculator

CREATE TABLE users(id SERIAL PRIMARY KEY, login VARCHAR UNIQUE, password VARCHAR);

CREATE TABLE calculations(id INT REFERENCES users(id), query VARCHAR, valid BOOL, result DECIMAL);
