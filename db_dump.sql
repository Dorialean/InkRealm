--
-- PostgreSQL database dump
--

-- Dumped from database version 14.6
-- Dumped by pg_dump version 14.6

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

--
-- Name: add_prof(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.add_prof(p text) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
  EXECUTE format('ALTER TYPE profession ADD VALUE %L', p);
  RETURN;
END;
$$;


ALTER FUNCTION public.add_prof(p text) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: clients_needs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clients_needs (
    service_id integer,
    service_date timestamp without time zone,
    order_id uuid,
    client_id integer NOT NULL,
    master_id integer
);


ALTER TABLE public.clients_needs OWNER TO postgres;

--
-- Name: ink_clients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ink_clients (
    client_id integer NOT NULL,
    first_name character varying(30) NOT NULL,
    surname character varying(30) NOT NULL,
    father_name character varying(30),
    mobile_phone character varying(20),
    email character varying(60) NOT NULL,
    login character varying(50) NOT NULL,
    password bytea NOT NULL,
    registered timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT ink_clients_email_check CHECK (((email)::text ~~ '%@%'::text))
);


ALTER TABLE public.ink_clients OWNER TO postgres;

--
-- Name: ink_clients_client_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ink_clients_client_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.ink_clients_client_id_seq OWNER TO postgres;

--
-- Name: ink_clients_client_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ink_clients_client_id_seq OWNED BY public.ink_clients.client_id;


--
-- Name: ink_masters_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ink_masters_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.ink_masters_seq OWNER TO postgres;

--
-- Name: ink_masters; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ink_masters (
    master_id integer DEFAULT nextval('public.ink_masters_seq'::regclass) NOT NULL,
    first_name character varying(30) NOT NULL,
    second_name character varying(30) NOT NULL,
    father_name character varying(30),
    photo_link text,
    experience_years integer,
    other_info jsonb,
    studio_id uuid NOT NULL,
    login character varying(50) NOT NULL,
    password bytea NOT NULL,
    ink_post text NOT NULL,
    registered timestamp without time zone DEFAULT now()
);


ALTER TABLE public.ink_masters OWNER TO postgres;

--
-- Name: ink_products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ink_products (
    product_id uuid DEFAULT gen_random_uuid() NOT NULL,
    title character varying(50) NOT NULL,
    description text,
    each_price money NOT NULL,
    props jsonb,
    photo_link text
);


ALTER TABLE public.ink_products OWNER TO postgres;

--
-- Name: ink_services; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ink_services (
    service_id integer NOT NULL,
    title character varying(80) NOT NULL,
    description text NOT NULL,
    min_price money NOT NULL,
    max_price money
);


ALTER TABLE public.ink_services OWNER TO postgres;

--
-- Name: ink_services_service_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ink_services_service_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.ink_services_service_id_seq OWNER TO postgres;

--
-- Name: ink_services_service_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ink_services_service_id_seq OWNED BY public.ink_services.service_id;


--
-- Name: ink_supplies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ink_supplies (
    supl_id uuid DEFAULT gen_random_uuid() NOT NULL,
    title character varying(40) NOT NULL,
    description text NOT NULL,
    price money NOT NULL
);


ALTER TABLE public.ink_supplies OWNER TO postgres;

--
-- Name: masters_reviews_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.masters_reviews_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.masters_reviews_seq OWNER TO postgres;

--
-- Name: master_reviews; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.master_reviews (
    master_review_id integer DEFAULT nextval('public.masters_reviews_seq'::regclass) NOT NULL,
    client_id integer NOT NULL,
    master_id integer NOT NULL,
    rating integer,
    review text,
    CONSTRAINT master_reviews_rating_check CHECK (((0 <= rating) AND (rating <= 10)))
);


ALTER TABLE public.master_reviews OWNER TO postgres;

--
-- Name: masters_services; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masters_services (
    master_id integer NOT NULL,
    service_id integer
);


ALTER TABLE public.masters_services OWNER TO postgres;

--
-- Name: masters_supplies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masters_supplies (
    master_id integer NOT NULL,
    supl_id uuid,
    amount integer
);


ALTER TABLE public.masters_supplies OWNER TO postgres;

--
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    order_id uuid DEFAULT gen_random_uuid() NOT NULL,
    product_id uuid,
    create_date timestamp without time zone DEFAULT now(),
    client_id integer NOT NULL
);


ALTER TABLE public.orders OWNER TO postgres;

--
-- Name: studios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.studios (
    studio_id uuid DEFAULT gen_random_uuid() NOT NULL,
    address text NOT NULL,
    rental_price_per_month money
);


ALTER TABLE public.studios OWNER TO postgres;

--
-- Name: ink_clients client_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_clients ALTER COLUMN client_id SET DEFAULT nextval('public.ink_clients_client_id_seq'::regclass);


--
-- Name: ink_services service_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_services ALTER COLUMN service_id SET DEFAULT nextval('public.ink_services_service_id_seq'::regclass);


--
-- Data for Name: clients_needs; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.clients_needs (service_id, service_date, order_id, client_id, master_id) FROM stdin;
\.


--
-- Data for Name: ink_clients; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ink_clients (client_id, first_name, surname, father_name, mobile_phone, email, login, password, registered) FROM stdin;
2	Имя	Фамилия	Отчество	+7(971)315-44-20	abra@codabra.ru	Aboba	\\xaf1e6425dc555dd15c632579f3f952bf1b51007bacc5a63d84be472e06b9c6ab	2022-12-07 19:05:32.344739
\.


--
-- Data for Name: ink_masters; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ink_masters (master_id, first_name, second_name, father_name, photo_link, experience_years, other_info, studio_id, login, password, ink_post, registered) FROM stdin;
1	Imya	Familiya	Otchestvo		\N	\N	4693ad94-d765-474d-b30c-a562f68b2961	Login	\\xb272f32f1f0e49cb997d91522ee7429969fc1c981a77e58f5f46c67ca0942480	sketch designer	2022-12-06 13:03:22.927361
\.


--
-- Data for Name: ink_products; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ink_products (product_id, title, description, each_price, props, photo_link) FROM stdin;
4b2c8160-208d-45b2-a906-0ac76e3aa4af	Футболка Ink Realm	Футбо́лка — предмет нательной одежды для обоих полов, обычно не имеющий пуговиц, воротника и карманов, с короткими рукавами, закрывающий туловище, часть рук и верх бёдер, надевается через голову.	500,00 ₽	\N	\N
4604ef1e-544b-49ab-a38f-bca327a8f440	Роторная беспроводная тату-машинка InkRealm 	Машинка в данной комплектации оснащена батареей увеличенной ёмкости!Полностью беспроводная Тату-машинка InkRealmзаставит Вас навсегда забыть о проводах и блоках питания.\nВ данной модели используется система Direct Drive и специально разработанный 8 ватный мотор Ambition Custom Motors.  Он оснащен немецкими подшипниками, и имеет возможность установки двух видов эксцентриков, на 3,5 и 4мм. Максимальная скорость вращения двигателя составляет 9000 оборотов в минуту. Благодаря этому двигателю машинка справляется со всеми видами работ.	30 199,00 ₽	{"type": "Rotor", "weight": "200gr"}	\N
94082e00-5afc-47f7-b109-c2a0eb350223	Серьга-Кликер	Серьга-Кликер Ink Realm стильная	299,99 ₽	{"params": {"width": "2mm", "length": "2mm"}, "weight": "18g", "material": "steel 316L"}	\N
\.


--
-- Data for Name: ink_services; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ink_services (service_id, title, description, min_price, max_price) FROM stdin;
4	Набить тату по готовому эскизу	Если у Вас уже есть готовая идея для татуировки, то предоставьте её нам любым удобным Вам способом и мы обязательно сотворим на Вашем теле это произведение исскуства.	4 000,00 ₽	1 000 000,00 ₽
5	Разработка эскизов татуировок	Если вы делаете постоянное тату, не стоит экономить на эскизе. Оно должно отображать ваше душевное состояние и отличаться от других. Подойдите к подбору эскиза серьёзно, ведь вывести татуировку будет не так просто, а расценки на небольшой уникальный рисунок по карману любому желающему.	2 000,00 ₽	50 000,00 ₽
6	Консультация по поводу будущей татуировки	☕️На консультации, за чашечкой вкуснейшего чая, мы обсуждаем вашу будущую татуировку. ✏️ Эскиз (если требуется изменить или нарисовать новый), 📏📐размер эскиза, место нанесения, 💰цену за вашу татуировку и/или эскиз. Так же на бесплатной консультации вы запишитесь на сеанс и внесете 💸предоплату, которая входит в стоимость работы.	0,00 ₽	\N
\.


--
-- Data for Name: ink_supplies; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ink_supplies (supl_id, title, description, price) FROM stdin;
deeff894-485c-485d-817d-fa7f6d9a7191	Тату-машинка	Скорость, 10В-1Вт об / мин, мощная, может использоваться для затенения и накладки. Подключается к линии постоянного тока, пусковое напряжение: 5 В, рабочее напряжение: 8-10 В. Частота переключения: 55-165 Гц, ход: 3,5 мм, регулируемая глубина иглы 0-4 мм.\n\nПреимущества ручки для татуировки, Эргономичная, легкая машина в виде ручки, имитирующая захват и ощущение настоящей ручки, эта ручка для татуировки позволяет сделать процедуру татуировки более удобной и точной.	4 153,99 ₽
8919d2b1-9460-4687-b317-627dde109911	Резиновые перчатки	Чёрные резиновые перчатки	49,99 ₽
33f75fe5-3132-4596-82d6-c43135e0dc4c	Футболка InkRealm	Чёрная футболка InkRealm	1 399,99 ₽
\.


--
-- Data for Name: master_reviews; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.master_reviews (master_review_id, client_id, master_id, rating, review) FROM stdin;
1	2	1	3	Татуировщик уснул на сеансе, но сделал красиво
\.


--
-- Data for Name: masters_services; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masters_services (master_id, service_id) FROM stdin;
\.


--
-- Data for Name: masters_supplies; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masters_supplies (master_id, supl_id, amount) FROM stdin;
1	d558af9e-8de2-48d6-966d-beaa221e7f1e	1
1	1ca3034b-229a-4fe1-af63-ade1960e2217	1
1	866f076b-1e29-40ab-a323-9919687a7189	1
\.


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.orders (order_id, product_id, create_date, client_id) FROM stdin;
6d4a0239-34eb-423b-bf40-d178de7b8625	4604ef1e-544b-49ab-a38f-bca327a8f440	2022-12-09 12:16:51.014818	2
6fad5797-34de-4df5-bcea-0a71ba326575	94082e00-5afc-47f7-b109-c2a0eb350223	2022-12-09 12:17:11.755905	2
\.


--
-- Data for Name: studios; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.studios (studio_id, address, rental_price_per_month) FROM stdin;
4693ad94-d765-474d-b30c-a562f68b2961	121416, Кировская область, город Дорохово, въезд Будапештсткая, 45	573 600,00 ₽
57dc6be2-e904-4054-8c61-4308e0c54f32	194152, Владимирская область, город Подольск, спуск Домодедовская, 49	600 000,00 ₽
7dda41ae-5e1a-4fb3-8eb9-52ac212807f4	428051, Томская область, город Мытищи, пр. Гоголя, 56	1 406 400,00 ₽
\.


--
-- Name: ink_clients_client_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ink_clients_client_id_seq', 2, true);


--
-- Name: ink_masters_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ink_masters_seq', 1, true);


--
-- Name: ink_services_service_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ink_services_service_id_seq', 6, true);


--
-- Name: masters_reviews_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masters_reviews_seq', 1, true);


--
-- Name: clients_needs clients_needs_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_pk PRIMARY KEY (client_id);


--
-- Name: ink_clients ink_clients_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_clients
    ADD CONSTRAINT ink_clients_pkey PRIMARY KEY (client_id);


--
-- Name: ink_masters ink_masters_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_masters
    ADD CONSTRAINT ink_masters_pkey PRIMARY KEY (master_id);


--
-- Name: ink_products ink_products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_products
    ADD CONSTRAINT ink_products_pkey PRIMARY KEY (product_id);


--
-- Name: ink_services ink_services_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_services
    ADD CONSTRAINT ink_services_pkey PRIMARY KEY (service_id);


--
-- Name: ink_supplies ink_supplies_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ink_supplies
    ADD CONSTRAINT ink_supplies_pkey PRIMARY KEY (supl_id);


--
-- Name: master_reviews master_reviews_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_pkey PRIMARY KEY (master_review_id);


--
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (order_id);


--
-- Name: studios studios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.studios
    ADD CONSTRAINT studios_pkey PRIMARY KEY (studio_id);


--
-- Name: ink_product_name_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX ink_product_name_idx ON public.ink_products USING btree (lower((title)::text), lower(description));


--
-- Name: clients_needs clients_needs_client_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_client_id_fkey FOREIGN KEY (client_id) REFERENCES public.ink_clients(client_id);


--
-- Name: clients_needs clients_needs_order_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id);


--
-- Name: clients_needs clients_needs_service_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_service_id_fkey FOREIGN KEY (service_id) REFERENCES public.ink_services(service_id);


--
-- Name: clients_needs fk_orders_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT fk_orders_id FOREIGN KEY (order_id) REFERENCES public.orders(order_id);


--
-- Name: clients_needs master_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT master_id_fk FOREIGN KEY (master_id) REFERENCES public.ink_masters(master_id);


--
-- Name: master_reviews master_reviews_client_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_client_id_fkey FOREIGN KEY (client_id) REFERENCES public.ink_clients(client_id);


--
-- Name: master_reviews master_reviews_master_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_master_id_fkey FOREIGN KEY (master_id) REFERENCES public.ink_masters(master_id);


--
-- Name: masters_services masters_services_master_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masters_services
    ADD CONSTRAINT masters_services_master_id_fkey FOREIGN KEY (master_id) REFERENCES public.ink_masters(master_id);


--
-- Name: masters_services masters_services_service_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masters_services
    ADD CONSTRAINT masters_services_service_id_fkey FOREIGN KEY (service_id) REFERENCES public.ink_services(service_id);


--
-- Name: masters_supplies masters_supplies_master_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masters_supplies
    ADD CONSTRAINT masters_supplies_master_id_fkey FOREIGN KEY (master_id) REFERENCES public.ink_masters(master_id);


--
-- Name: orders orders_client_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_client_id_fkey FOREIGN KEY (client_id) REFERENCES public.ink_clients(client_id);


--
-- Name: orders orders_product_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_product_id_fkey FOREIGN KEY (product_id) REFERENCES public.ink_products(product_id);


--
-- PostgreSQL database dump complete
--

