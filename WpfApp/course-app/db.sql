-- Create DB
CREATE DATABASE "Stonks"
	WITH
	OWNER = postgres
	ENCODING = 'UTF8'
	LC_COLLATE = 'Russian_Russia.1251'
	LC_CTYPE = 'Russian_Russia.1251';

-- And connect to the database and do the rest
CREATE TABLE roles (
	role_name varchar(40) NOT NULL CHECK (role_name <> ''),
	role_key varchar(15) PRIMARY KEY
);

CREATE TABLE users (
	user_id SERIAL PRIMARY KEY,
	role_key varchar(15) REFERENCES roles (role_key) ON DELETE CASCADE,
	user_name varchar(40) NOT NULL CHECK (user_name <> ''),
	user_pass varchar(40) NOT NULL CHECK (user_pass <> ''),
	full_name varchar(100)
);

CREATE TABLE customers (
	customer_id SERIAL PRIMARY KEY,
	customer_name varchar(40) NOT NULL CHECK (customer_name <> ''),
	description varchar(250)
);

CREATE TABLE stocks (
	stock_id SERIAL PRIMARY KEY,
	stock_name varchar(40) NOT NULL CHECK (stock_name <> ''),
	description varchar(250),
	markup real,
	user_id INTEGER REFERENCES users (user_id) ON DELETE CASCADE
);

CREATE TABLE products (
	product_id SERIAL PRIMARY KEY,
	product_name varchar(40) NOT NULL CHECK (product_name <> ''),
	product_price real
);

CREATE TABLE receipt_invoices (
	receipt_invoice_id SERIAL PRIMARY KEY,
	receipt_invoice_date date,
	customer_id INTEGER REFERENCES customers (customer_id) ON DELETE CASCADE,	
	product_id INTEGER REFERENCES products (product_id) ON DELETE CASCADE,
	count_product real,
	price_product real,
	stock_name varchar(40) NOT NULL CHECK (stock_name <> ''),
	class_name varchar(40) NOT NULL CHECK (stock_name <> ''),
	markup real
);

CREATE TABLE expenditure_invoices (
	expenditure_invoice_id SERIAL PRIMARY KEY,
	expenditure_invoice_date date,
	customer_id INTEGER REFERENCES customers (customer_id) ON DELETE CASCADE,
	product_id INTEGER REFERENCES products (product_id) ON DELETE CASCADE,
	count_product real,
	price_product real,
	stock_name varchar(40) NOT NULL CHECK (stock_name <> '')
);

-- Default data
-- Roles
INSERT INTO public.roles(role_name, role_key) VALUES ('Администратор', 'admin');
INSERT INTO public.roles(role_name, role_key) VALUES ('Кладовщик', 'stoker');
INSERT INTO public.roles(role_name, role_key) VALUES ('Менеджер', 'manager');
-- Users эти юзеры используются для входа в систему
INSERT INTO public.users(role_key, user_name, user_pass, full_name)
	VALUES ('admin', 'admin', 'admin', '');
INSERT INTO public.users(role_key, user_name, user_pass, full_name)
	VALUES ('manager', 'manager', 'manager', 'Иванов Иван');
INSERT INTO public.users(role_key, user_name, user_pass, full_name)
	VALUES ('stoker', 'stoker', 'stoker', 'Петров Петр');