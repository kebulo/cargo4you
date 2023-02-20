--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.1

-- Started on 2023-02-20 15:03:13

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
-- TOC entry 215 (class 1259 OID 16406)
-- Name: courier; Type: TABLE; Schema: public; Owner: cargo4you
--
CREATE DATABASE cargo4you;
USE cargo4you;

CREATE TABLE public.courier (
    courier_id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.courier OWNER TO cargo4you;

--
-- TOC entry 214 (class 1259 OID 16405)
-- Name: courier_courier_id_seq; Type: SEQUENCE; Schema: public; Owner: cargo4you
--

ALTER TABLE public.courier ALTER COLUMN courier_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.courier_courier_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 217 (class 1259 OID 16412)
-- Name: rules; Type: TABLE; Schema: public; Owner: cargo4you
--

CREATE TABLE public.rules (
    price_id integer NOT NULL,
    courier_id integer,
    price double precision,
    is_dimension boolean,
    min smallint,
    extra_kg smallint,
    max character varying(20),
    extra_value double precision
);


ALTER TABLE public.rules OWNER TO cargo4you;

--
-- TOC entry 216 (class 1259 OID 16411)
-- Name: prices_price_id_seq; Type: SEQUENCE; Schema: public; Owner: cargo4you
--

ALTER TABLE public.rules ALTER COLUMN price_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.prices_price_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 219 (class 1259 OID 16423)
-- Name: quotations; Type: TABLE; Schema: public; Owner: cargo4you
--

CREATE TABLE public.quotations (
    quotation_id integer NOT NULL,
    courier_id integer,
    client_name character varying(50),
    depth integer,
    weight integer,
    price double precision,
    shipment_confirm boolean DEFAULT false NOT NULL,
    width integer,
    height integer,
    address character varying(250),
    phone_number character varying(30)
);


ALTER TABLE public.quotations OWNER TO cargo4you;

--
-- TOC entry 218 (class 1259 OID 16422)
-- Name: quotations_quotation_id_seq; Type: SEQUENCE; Schema: public; Owner: cargo4you
--

ALTER TABLE public.quotations ALTER COLUMN quotation_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.quotations_quotation_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 3335 (class 0 OID 16406)
-- Dependencies: 215
-- Data for Name: courier; Type: TABLE DATA; Schema: public; Owner: cargo4you
--

COPY public.courier (courier_id, name) FROM stdin;
1	Cargo4You
2	ShipFaster
3	MaltaShip
\.


--
-- TOC entry 3339 (class 0 OID 16423)
-- Dependencies: 219
-- Data for Name: quotations; Type: TABLE DATA; Schema: public; Owner: cargo4you
--

COPY public.quotations (quotation_id, courier_id, client_name, depth, weight, price, shipment_confirm, width, height, address, phone_number) FROM stdin;
\.


--
-- TOC entry 3337 (class 0 OID 16412)
-- Dependencies: 217
-- Data for Name: rules; Type: TABLE DATA; Schema: public; Owner: cargo4you
--

COPY public.rules (price_id, courier_id, price, is_dimension, min, extra_kg, max, extra_value) FROM stdin;
1	1	10	t	0	0	1000	0
2	1	20	t	1000	0	2000	0
3	1	15	f	0	0	2	0
4	1	18	f	2	0	15	0
5	1	35	f	15	0	20	0
6	2	11.99	t	0	0	1000	0
7	2	21.99	t	1000	0	17000	0
8	2	16.5	f	10	0	15	0
9	2	36.5	f	15	0	25	0
11	3	9.5	t	0	0	1000	0
12	3	19.5	t	1000	0	2000	0
13	3	48.5	t	2000	0	5000	0
14	3	147.5	t	5000	0	+	0
15	3	16.99	f	10	0	20	0
16	3	33.99	f	20	0	30	0
10	2	40	f	25	25	+	0.417
17	3	43.99	f	30	25	+	0.41
\.


--
-- TOC entry 3345 (class 0 OID 0)
-- Dependencies: 214
-- Name: courier_courier_id_seq; Type: SEQUENCE SET; Schema: public; Owner: cargo4you
--

SELECT pg_catalog.setval('public.courier_courier_id_seq', 3, true);


--
-- TOC entry 3346 (class 0 OID 0)
-- Dependencies: 216
-- Name: prices_price_id_seq; Type: SEQUENCE SET; Schema: public; Owner: cargo4you
--

SELECT pg_catalog.setval('public.prices_price_id_seq', 17, true);


--
-- TOC entry 3347 (class 0 OID 0)
-- Dependencies: 218
-- Name: quotations_quotation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: cargo4you
--

SELECT pg_catalog.setval('public.quotations_quotation_id_seq', 1, false);


--
-- TOC entry 3185 (class 2606 OID 16410)
-- Name: courier courier_pkey; Type: CONSTRAINT; Schema: public; Owner: cargo4you
--

ALTER TABLE ONLY public.courier
    ADD CONSTRAINT courier_pkey PRIMARY KEY (courier_id);


--
-- TOC entry 3187 (class 2606 OID 16416)
-- Name: rules prices_pkey; Type: CONSTRAINT; Schema: public; Owner: cargo4you
--

ALTER TABLE ONLY public.rules
    ADD CONSTRAINT prices_pkey PRIMARY KEY (price_id);


--
-- TOC entry 3189 (class 2606 OID 16427)
-- Name: quotations quotations_pkey; Type: CONSTRAINT; Schema: public; Owner: cargo4you
--

ALTER TABLE ONLY public.quotations
    ADD CONSTRAINT quotations_pkey PRIMARY KEY (quotation_id);


--
-- TOC entry 3190 (class 2606 OID 16417)
-- Name: rules fk_customer; Type: FK CONSTRAINT; Schema: public; Owner: cargo4you
--

ALTER TABLE ONLY public.rules
    ADD CONSTRAINT fk_customer FOREIGN KEY (courier_id) REFERENCES public.courier(courier_id);


--
-- TOC entry 3191 (class 2606 OID 16428)
-- Name: quotations fk_customer; Type: FK CONSTRAINT; Schema: public; Owner: cargo4you
--

ALTER TABLE ONLY public.quotations
    ADD CONSTRAINT fk_customer FOREIGN KEY (courier_id) REFERENCES public.courier(courier_id);


-- Completed on 2023-02-20 15:03:13

--
-- PostgreSQL database dump complete
--

