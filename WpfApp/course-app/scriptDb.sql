--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2
-- Dumped by pg_dump version 13.2

-- Started on 2021-05-13 12:11:00

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 200 (class 1259 OID 25112)
-- Name: customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customers (
    customer_id integer NOT NULL,
    customer_name character varying(40) NOT NULL,
    description character varying(250),
    CONSTRAINT customers_customer_name_check CHECK (((customer_name)::text <> ''::text))
);


ALTER TABLE public.customers OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 25116)
-- Name: customers_customer_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.customers_customer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.customers_customer_id_seq OWNER TO postgres;

--
-- TOC entry 3081 (class 0 OID 0)
-- Dependencies: 201
-- Name: customers_customer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.customers_customer_id_seq OWNED BY public.customers.customer_id;


--
-- TOC entry 202 (class 1259 OID 25118)
-- Name: expenditure_invoices; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.expenditure_invoices (
    expenditure_invoice_id integer NOT NULL,
    expenditure_invoice_date date,
    customer_id integer,
    stock_id integer
);


ALTER TABLE public.expenditure_invoices OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 25122)
-- Name: expenditure_invoices_expenditure_invoice_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.expenditure_invoices_expenditure_invoice_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.expenditure_invoices_expenditure_invoice_id_seq OWNER TO postgres;

--
-- TOC entry 3082 (class 0 OID 0)
-- Dependencies: 203
-- Name: expenditure_invoices_expenditure_invoice_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.expenditure_invoices_expenditure_invoice_id_seq OWNED BY public.expenditure_invoices.expenditure_invoice_id;


--
-- TOC entry 215 (class 1259 OID 25253)
-- Name: expenditure_positions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.expenditure_positions (
    expenditure_position_id integer NOT NULL,
    product_id integer,
    count_product real,
    expenditure_invoice_id integer,
    product_price real
);


ALTER TABLE public.expenditure_positions OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 25258)
-- Name: expenditure_positions_expenditure_position_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.expenditure_positions ALTER COLUMN expenditure_position_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.expenditure_positions_expenditure_position_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 204 (class 1259 OID 25124)
-- Name: products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.products (
    product_id integer NOT NULL,
    product_name character varying(40) NOT NULL,
    product_price real,
    CONSTRAINT products_product_name_check CHECK (((product_name)::text <> ''::text))
);


ALTER TABLE public.products OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 25294)
-- Name: products_in_stock; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.products_in_stock (
    id integer NOT NULL,
    product_id integer,
    stock_id integer,
    count_product real
);


ALTER TABLE public.products_in_stock OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 25311)
-- Name: products_in_stock_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.products_in_stock ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.products_in_stock_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 205 (class 1259 OID 25128)
-- Name: products_product_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.products_product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.products_product_id_seq OWNER TO postgres;

--
-- TOC entry 3083 (class 0 OID 0)
-- Dependencies: 205
-- Name: products_product_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.products_product_id_seq OWNED BY public.products.product_id;


--
-- TOC entry 206 (class 1259 OID 25130)
-- Name: receipt_invoices; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.receipt_invoices (
    receipt_invoice_id integer NOT NULL,
    receipt_invoice_date date,
    customer_id integer,
    stock_id integer
);


ALTER TABLE public.receipt_invoices OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 25133)
-- Name: receipt_invoices_receipt_invoice_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.receipt_invoices_receipt_invoice_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.receipt_invoices_receipt_invoice_id_seq OWNER TO postgres;

--
-- TOC entry 3084 (class 0 OID 0)
-- Dependencies: 207
-- Name: receipt_invoices_receipt_invoice_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.receipt_invoices_receipt_invoice_id_seq OWNED BY public.receipt_invoices.receipt_invoice_id;


--
-- TOC entry 213 (class 1259 OID 25207)
-- Name: receipt_positions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.receipt_positions (
    position_id integer NOT NULL,
    count_product real,
    product_id integer,
    receipt_invoice_id integer
);


ALTER TABLE public.receipt_positions OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 25251)
-- Name: receipt_positions_position_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.receipt_positions ALTER COLUMN position_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.receipt_positions_position_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 208 (class 1259 OID 25135)
-- Name: roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.roles (
    role_name character varying(40) NOT NULL,
    role_key character varying(15) NOT NULL,
    CONSTRAINT roles_role_name_check CHECK (((role_name)::text <> ''::text))
);


ALTER TABLE public.roles OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 25139)
-- Name: stocks; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.stocks (
    stock_id integer NOT NULL,
    stock_name character varying(40) NOT NULL,
    description character varying(250),
    markup real,
    user_id integer,
    CONSTRAINT stocks_stock_name_check CHECK (((stock_name)::text <> ''::text))
);


ALTER TABLE public.stocks OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 25143)
-- Name: stocks_stock_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.stocks_stock_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.stocks_stock_id_seq OWNER TO postgres;

--
-- TOC entry 3085 (class 0 OID 0)
-- Dependencies: 210
-- Name: stocks_stock_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.stocks_stock_id_seq OWNED BY public.stocks.stock_id;


--
-- TOC entry 211 (class 1259 OID 25145)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    role_key character varying(15),
    user_name character varying(40) NOT NULL,
    user_pass character varying(40) NOT NULL,
    full_name character varying(100),
    CONSTRAINT users_user_name_check CHECK (((user_name)::text <> ''::text)),
    CONSTRAINT users_user_pass_check CHECK (((user_pass)::text <> ''::text))
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 25150)
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_user_id_seq OWNER TO postgres;

--
-- TOC entry 3086 (class 0 OID 0)
-- Dependencies: 212
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- TOC entry 2902 (class 2604 OID 25152)
-- Name: customers customer_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers ALTER COLUMN customer_id SET DEFAULT nextval('public.customers_customer_id_seq'::regclass);


--
-- TOC entry 2904 (class 2604 OID 25153)
-- Name: expenditure_invoices expenditure_invoice_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_invoices ALTER COLUMN expenditure_invoice_id SET DEFAULT nextval('public.expenditure_invoices_expenditure_invoice_id_seq'::regclass);


--
-- TOC entry 2905 (class 2604 OID 25154)
-- Name: products product_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products ALTER COLUMN product_id SET DEFAULT nextval('public.products_product_id_seq'::regclass);


--
-- TOC entry 2907 (class 2604 OID 25155)
-- Name: receipt_invoices receipt_invoice_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_invoices ALTER COLUMN receipt_invoice_id SET DEFAULT nextval('public.receipt_invoices_receipt_invoice_id_seq'::regclass);


--
-- TOC entry 2909 (class 2604 OID 25156)
-- Name: stocks stock_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stocks ALTER COLUMN stock_id SET DEFAULT nextval('public.stocks_stock_id_seq'::regclass);


--
-- TOC entry 2911 (class 2604 OID 25157)
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- TOC entry 2915 (class 2606 OID 25159)
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (customer_id);


--
-- TOC entry 2917 (class 2606 OID 25161)
-- Name: expenditure_invoices expenditure_invoices_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_invoices
    ADD CONSTRAINT expenditure_invoices_pkey PRIMARY KEY (expenditure_invoice_id);


--
-- TOC entry 2931 (class 2606 OID 25257)
-- Name: expenditure_positions expenditure_positions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_positions
    ADD CONSTRAINT expenditure_positions_pkey PRIMARY KEY (expenditure_position_id);


--
-- TOC entry 2933 (class 2606 OID 25298)
-- Name: products_in_stock products_in_stock_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products_in_stock
    ADD CONSTRAINT products_in_stock_pkey PRIMARY KEY (id);


--
-- TOC entry 2919 (class 2606 OID 25163)
-- Name: products products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (product_id);


--
-- TOC entry 2921 (class 2606 OID 25165)
-- Name: receipt_invoices receipt_invoices_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_invoices
    ADD CONSTRAINT receipt_invoices_pkey PRIMARY KEY (receipt_invoice_id);


--
-- TOC entry 2929 (class 2606 OID 25211)
-- Name: receipt_positions receipt_positions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_positions
    ADD CONSTRAINT receipt_positions_pkey PRIMARY KEY (position_id);


--
-- TOC entry 2923 (class 2606 OID 25167)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role_key);


--
-- TOC entry 2925 (class 2606 OID 25169)
-- Name: stocks stocks_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stocks
    ADD CONSTRAINT stocks_pkey PRIMARY KEY (stock_id);


--
-- TOC entry 2927 (class 2606 OID 25171)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- TOC entry 2935 (class 2606 OID 25172)
-- Name: expenditure_invoices expenditure_invoices_customer_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_invoices
    ADD CONSTRAINT expenditure_invoices_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(customer_id) ON DELETE CASCADE;


--
-- TOC entry 2942 (class 2606 OID 25265)
-- Name: expenditure_positions fc_expenditure_positions_invoices; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_positions
    ADD CONSTRAINT fc_expenditure_positions_invoices FOREIGN KEY (expenditure_invoice_id) REFERENCES public.expenditure_invoices(expenditure_invoice_id) NOT VALID;


--
-- TOC entry 2943 (class 2606 OID 25289)
-- Name: expenditure_positions fc_expenditure_positions_products; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_positions
    ADD CONSTRAINT fc_expenditure_positions_products FOREIGN KEY (product_id) REFERENCES public.products(product_id) NOT VALID;


--
-- TOC entry 2934 (class 2606 OID 25275)
-- Name: expenditure_invoices fk_expenditure_invoices_stocks; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenditure_invoices
    ADD CONSTRAINT fk_expenditure_invoices_stocks FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id) NOT VALID;


--
-- TOC entry 2944 (class 2606 OID 25301)
-- Name: products_in_stock fk_products_in_stock_products; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products_in_stock
    ADD CONSTRAINT fk_products_in_stock_products FOREIGN KEY (product_id) REFERENCES public.products(product_id) NOT VALID;


--
-- TOC entry 2945 (class 2606 OID 25306)
-- Name: products_in_stock fk_products_in_stock_stocks; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products_in_stock
    ADD CONSTRAINT fk_products_in_stock_stocks FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id) NOT VALID;


--
-- TOC entry 2936 (class 2606 OID 25241)
-- Name: receipt_invoices fk_receipt_invoices_stocks; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_invoices
    ADD CONSTRAINT fk_receipt_invoices_stocks FOREIGN KEY (stock_id) REFERENCES public.stocks(stock_id) NOT VALID;


--
-- TOC entry 2940 (class 2606 OID 25212)
-- Name: receipt_positions fk_receipt_positions_products; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_positions
    ADD CONSTRAINT fk_receipt_positions_products FOREIGN KEY (product_id) REFERENCES public.products(product_id);


--
-- TOC entry 2941 (class 2606 OID 25236)
-- Name: receipt_positions fk_receipt_positions_receipt; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_positions
    ADD CONSTRAINT fk_receipt_positions_receipt FOREIGN KEY (receipt_invoice_id) REFERENCES public.receipt_invoices(receipt_invoice_id) NOT VALID;


--
-- TOC entry 2937 (class 2606 OID 25182)
-- Name: receipt_invoices receipt_invoices_customer_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receipt_invoices
    ADD CONSTRAINT receipt_invoices_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(customer_id) ON DELETE CASCADE;


--
-- TOC entry 2938 (class 2606 OID 25197)
-- Name: stocks stocks_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.stocks
    ADD CONSTRAINT stocks_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


--
-- TOC entry 2939 (class 2606 OID 25202)
-- Name: users users_role_key_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_role_key_fkey FOREIGN KEY (role_key) REFERENCES public.roles(role_key) ON DELETE CASCADE;


-- Completed on 2021-05-13 12:11:00

--
-- PostgreSQL database dump complete
- Default data
-- Roles
INSERT INTO public.roles(role_name, role_key) VALUES ('Администратор', 'admin');
INSERT INTO public.roles(role_name, role_key) VALUES ('Кладовщик', 'stoker');
INSERT INTO public.roles(role_name, role_key) VALUES ('Менеджер', 'manager');
-- Users эти юзеры используются для входа в систему
INSERT INTO public.users(role_key, user_name, user_pass, full_name)
	VALUES ('admin', 'admin', 'admin', '');
INSERT INTO public.users(role_key, user_name, user_pass, full_name)
	VALUES ('manager', 'manager', 'manager', 'Расшивалов Никита');
INSERT INTO public.users(role_key, user_name, user_pass, full_name)
	VALUES ('stoker', 'stoker', 'stoker', 'Матарас Артем');
--

