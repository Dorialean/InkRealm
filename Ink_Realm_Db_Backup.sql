PGDMP     .    7            
    z         	   ink_realm    15.1    15.1 <    \           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ]           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ^           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            _           1262    16572 	   ink_realm    DATABASE     }   CREATE DATABASE ink_realm WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE ink_realm;
                postgres    false            p           1247    16729 
   profession    TYPE     k   CREATE TYPE public.profession AS ENUM (
    'sketch designer',
    'tatoo master',
    'pirsing master'
);
    DROP TYPE public.profession;
       public          postgres    false            �            1259    16620    clients_needs    TABLE     �   CREATE TABLE public.clients_needs (
    clients_needs_id uuid DEFAULT gen_random_uuid() NOT NULL,
    client_id integer NOT NULL,
    service_id integer,
    service_date timestamp without time zone,
    order_id uuid
);
 !   DROP TABLE public.clients_needs;
       public         heap    postgres    false            �            1259    16613    ink_clients    TABLE       CREATE TABLE public.ink_clients (
    client_id integer NOT NULL,
    first_name character varying(30) NOT NULL,
    surname character varying(30) NOT NULL,
    father_name character varying(30),
    mobile_phone character varying(20),
    email character varying(60),
    client_needs_id uuid,
    login character varying(50) NOT NULL,
    password bytea NOT NULL,
    registered timestamp without time zone DEFAULT now() NOT NULL,
    CONSTRAINT ink_clients_email_check CHECK (((email)::text ~~ '%@%'::text))
);
    DROP TABLE public.ink_clients;
       public         heap    postgres    false            �            1259    16612    ink_clients_client_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ink_clients_client_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.ink_clients_client_id_seq;
       public          postgres    false    220            `           0    0    ink_clients_client_id_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.ink_clients_client_id_seq OWNED BY public.ink_clients.client_id;
          public          postgres    false    219            �            1259    16713    ink_masters_seq    SEQUENCE     x   CREATE SEQUENCE public.ink_masters_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.ink_masters_seq;
       public          postgres    false            �            1259    16645    ink_masters    TABLE     3  CREATE TABLE public.ink_masters (
    master_id integer DEFAULT nextval('public.ink_masters_seq'::regclass) NOT NULL,
    first_name character varying(30) NOT NULL,
    second_name character varying(30) NOT NULL,
    father_name character varying(30),
    photo_link text,
    service_id integer NOT NULL,
    experience_years integer,
    other_info jsonb,
    studio_id uuid NOT NULL,
    login character varying(50) NOT NULL,
    password bytea NOT NULL,
    registered time without time zone DEFAULT now() NOT NULL,
    ink_post public.profession NOT NULL
);
    DROP TABLE public.ink_masters;
       public         heap    postgres    false    226    880            �            1259    16589    ink_products    TABLE     �   CREATE TABLE public.ink_products (
    product_id uuid DEFAULT gen_random_uuid() NOT NULL,
    title character varying(50) NOT NULL,
    description text,
    each_price money NOT NULL,
    props jsonb,
    photo_link text
);
     DROP TABLE public.ink_products;
       public         heap    postgres    false            �            1259    16581    ink_services    TABLE     �   CREATE TABLE public.ink_services (
    service_id integer NOT NULL,
    title character varying(80) NOT NULL,
    description text NOT NULL,
    min_price money NOT NULL,
    max_price money
);
     DROP TABLE public.ink_services;
       public         heap    postgres    false            �            1259    16580    ink_services_service_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ink_services_service_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.ink_services_service_id_seq;
       public          postgres    false    216            a           0    0    ink_services_service_id_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.ink_services_service_id_seq OWNED BY public.ink_services.service_id;
          public          postgres    false    215            �            1259    16680    ink_supplies    TABLE     �   CREATE TABLE public.ink_supplies (
    supl_id uuid DEFAULT gen_random_uuid() NOT NULL,
    title character varying(40) NOT NULL,
    description text NOT NULL,
    price money NOT NULL
);
     DROP TABLE public.ink_supplies;
       public         heap    postgres    false            �            1259    16717    masters_reviews_seq    SEQUENCE     |   CREATE SEQUENCE public.masters_reviews_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.masters_reviews_seq;
       public          postgres    false            �            1259    16657    master_reviews    TABLE     B  CREATE TABLE public.master_reviews (
    master_review_id integer DEFAULT nextval('public.masters_reviews_seq'::regclass) NOT NULL,
    client_id integer NOT NULL,
    master_id integer NOT NULL,
    rating integer,
    review text,
    CONSTRAINT master_reviews_rating_check CHECK (((0 <= rating) AND (rating <= 10)))
);
 "   DROP TABLE public.master_reviews;
       public         heap    postgres    false    227            �            1259    16687    masters_supplies    TABLE     o   CREATE TABLE public.masters_supplies (
    master_id integer NOT NULL,
    supl_id uuid,
    amount integer
);
 $   DROP TABLE public.masters_supplies;
       public         heap    postgres    false            �            1259    16596    orders    TABLE     �   CREATE TABLE public.orders (
    order_id uuid DEFAULT gen_random_uuid() NOT NULL,
    product_id uuid,
    create_date timestamp without time zone DEFAULT now()
);
    DROP TABLE public.orders;
       public         heap    postgres    false            �            1259    16573    studios    TABLE     �   CREATE TABLE public.studios (
    studio_id uuid DEFAULT gen_random_uuid() NOT NULL,
    address text NOT NULL,
    rental_price_per_month money
);
    DROP TABLE public.studios;
       public         heap    postgres    false            �           2604    16616    ink_clients client_id    DEFAULT     ~   ALTER TABLE ONLY public.ink_clients ALTER COLUMN client_id SET DEFAULT nextval('public.ink_clients_client_id_seq'::regclass);
 D   ALTER TABLE public.ink_clients ALTER COLUMN client_id DROP DEFAULT;
       public          postgres    false    219    220    220            �           2604    16584    ink_services service_id    DEFAULT     �   ALTER TABLE ONLY public.ink_services ALTER COLUMN service_id SET DEFAULT nextval('public.ink_services_service_id_seq'::regclass);
 F   ALTER TABLE public.ink_services ALTER COLUMN service_id DROP DEFAULT;
       public          postgres    false    215    216    216            S          0    16620    clients_needs 
   TABLE DATA                 public          postgres    false    221   $K       R          0    16613    ink_clients 
   TABLE DATA                 public          postgres    false    220   >K       T          0    16645    ink_masters 
   TABLE DATA                 public          postgres    false    222   XK       O          0    16589    ink_products 
   TABLE DATA                 public          postgres    false    217   rK       N          0    16581    ink_services 
   TABLE DATA                 public          postgres    false    216   �O       V          0    16680    ink_supplies 
   TABLE DATA                 public          postgres    false    224   �O       U          0    16657    master_reviews 
   TABLE DATA                 public          postgres    false    223   �O       W          0    16687    masters_supplies 
   TABLE DATA                 public          postgres    false    225   �O       P          0    16596    orders 
   TABLE DATA                 public          postgres    false    218   P       L          0    16573    studios 
   TABLE DATA                 public          postgres    false    214   P       b           0    0    ink_clients_client_id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.ink_clients_client_id_seq', 1, false);
          public          postgres    false    219            c           0    0    ink_masters_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.ink_masters_seq', 1, false);
          public          postgres    false    226            d           0    0    ink_services_service_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public.ink_services_service_id_seq', 1, false);
          public          postgres    false    215            e           0    0    masters_reviews_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.masters_reviews_seq', 1, false);
          public          postgres    false    227            �           2606    16624     clients_needs clients_needs_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_pkey PRIMARY KEY (clients_needs_id);
 J   ALTER TABLE ONLY public.clients_needs DROP CONSTRAINT clients_needs_pkey;
       public            postgres    false    221            �           2606    16619    ink_clients ink_clients_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.ink_clients
    ADD CONSTRAINT ink_clients_pkey PRIMARY KEY (client_id);
 F   ALTER TABLE ONLY public.ink_clients DROP CONSTRAINT ink_clients_pkey;
       public            postgres    false    220            �           2606    16651    ink_masters ink_masters_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.ink_masters
    ADD CONSTRAINT ink_masters_pkey PRIMARY KEY (master_id);
 F   ALTER TABLE ONLY public.ink_masters DROP CONSTRAINT ink_masters_pkey;
       public            postgres    false    222            �           2606    16595    ink_products ink_products_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.ink_products
    ADD CONSTRAINT ink_products_pkey PRIMARY KEY (product_id);
 H   ALTER TABLE ONLY public.ink_products DROP CONSTRAINT ink_products_pkey;
       public            postgres    false    217            �           2606    16588    ink_services ink_services_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.ink_services
    ADD CONSTRAINT ink_services_pkey PRIMARY KEY (service_id);
 H   ALTER TABLE ONLY public.ink_services DROP CONSTRAINT ink_services_pkey;
       public            postgres    false    216            �           2606    16686    ink_supplies ink_supplies_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.ink_supplies
    ADD CONSTRAINT ink_supplies_pkey PRIMARY KEY (supl_id);
 H   ALTER TABLE ONLY public.ink_supplies DROP CONSTRAINT ink_supplies_pkey;
       public            postgres    false    224            �           2606    16664 "   master_reviews master_reviews_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_pkey PRIMARY KEY (master_review_id);
 L   ALTER TABLE ONLY public.master_reviews DROP CONSTRAINT master_reviews_pkey;
       public            postgres    false    223            �           2606    16691 &   masters_supplies masters_supplies_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.masters_supplies
    ADD CONSTRAINT masters_supplies_pkey PRIMARY KEY (master_id);
 P   ALTER TABLE ONLY public.masters_supplies DROP CONSTRAINT masters_supplies_pkey;
       public            postgres    false    225            �           2606    16601    orders orders_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (order_id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    218            �           2606    16579    studios studios_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.studios
    ADD CONSTRAINT studios_pkey PRIMARY KEY (studio_id);
 >   ALTER TABLE ONLY public.studios DROP CONSTRAINT studios_pkey;
       public            postgres    false    214            �           2606    16625 *   clients_needs clients_needs_client_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_client_id_fkey FOREIGN KEY (client_id) REFERENCES public.ink_clients(client_id);
 T   ALTER TABLE ONLY public.clients_needs DROP CONSTRAINT clients_needs_client_id_fkey;
       public          postgres    false    221    3238    220            �           2606    16630 )   clients_needs clients_needs_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(order_id);
 S   ALTER TABLE ONLY public.clients_needs DROP CONSTRAINT clients_needs_order_id_fkey;
       public          postgres    false    218    221    3236            �           2606    16635 +   clients_needs clients_needs_service_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT clients_needs_service_id_fkey FOREIGN KEY (service_id) REFERENCES public.ink_services(service_id);
 U   ALTER TABLE ONLY public.clients_needs DROP CONSTRAINT clients_needs_service_id_fkey;
       public          postgres    false    221    3232    216            �           2606    16640    ink_clients fk_clients_needs_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.ink_clients
    ADD CONSTRAINT fk_clients_needs_id FOREIGN KEY (client_needs_id) REFERENCES public.clients_needs(clients_needs_id);
 I   ALTER TABLE ONLY public.ink_clients DROP CONSTRAINT fk_clients_needs_id;
       public          postgres    false    220    221    3240            �           2606    16721    clients_needs fk_orders_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.clients_needs
    ADD CONSTRAINT fk_orders_id FOREIGN KEY (order_id) REFERENCES public.orders(order_id);
 D   ALTER TABLE ONLY public.clients_needs DROP CONSTRAINT fk_orders_id;
       public          postgres    false    3236    221    218            �           2606    16652 '   ink_masters ink_masters_service_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.ink_masters
    ADD CONSTRAINT ink_masters_service_id_fkey FOREIGN KEY (service_id) REFERENCES public.ink_services(service_id);
 Q   ALTER TABLE ONLY public.ink_masters DROP CONSTRAINT ink_masters_service_id_fkey;
       public          postgres    false    216    3232    222            �           2606    16702 &   ink_masters ink_masters_studio_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.ink_masters
    ADD CONSTRAINT ink_masters_studio_id_fkey FOREIGN KEY (studio_id) REFERENCES public.studios(studio_id);
 P   ALTER TABLE ONLY public.ink_masters DROP CONSTRAINT ink_masters_studio_id_fkey;
       public          postgres    false    222    3230    214            �           2606    16670 ,   master_reviews master_reviews_client_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_client_id_fkey FOREIGN KEY (client_id) REFERENCES public.ink_clients(client_id);
 V   ALTER TABLE ONLY public.master_reviews DROP CONSTRAINT master_reviews_client_id_fkey;
       public          postgres    false    3238    220    223            �           2606    16675 ,   master_reviews master_reviews_master_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_master_id_fkey FOREIGN KEY (master_id) REFERENCES public.ink_masters(master_id);
 V   ALTER TABLE ONLY public.master_reviews DROP CONSTRAINT master_reviews_master_id_fkey;
       public          postgres    false    222    3242    223            �           2606    16665 3   master_reviews master_reviews_master_review_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.master_reviews
    ADD CONSTRAINT master_reviews_master_review_id_fkey FOREIGN KEY (master_review_id) REFERENCES public.ink_masters(master_id);
 ]   ALTER TABLE ONLY public.master_reviews DROP CONSTRAINT master_reviews_master_review_id_fkey;
       public          postgres    false    222    3242    223            �           2606    16692 0   masters_supplies masters_supplies_master_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.masters_supplies
    ADD CONSTRAINT masters_supplies_master_id_fkey FOREIGN KEY (master_id) REFERENCES public.ink_masters(master_id);
 Z   ALTER TABLE ONLY public.masters_supplies DROP CONSTRAINT masters_supplies_master_id_fkey;
       public          postgres    false    3242    222    225            �           2606    16697 .   masters_supplies masters_supplies_supl_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.masters_supplies
    ADD CONSTRAINT masters_supplies_supl_id_fkey FOREIGN KEY (supl_id) REFERENCES public.ink_supplies(supl_id);
 X   ALTER TABLE ONLY public.masters_supplies DROP CONSTRAINT masters_supplies_supl_id_fkey;
       public          postgres    false    225    224    3246            �           2606    16602    orders orders_product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_product_id_fkey FOREIGN KEY (product_id) REFERENCES public.ink_products(product_id);
 G   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_product_id_fkey;
       public          postgres    false    217    3234    218            S   
   x���          R   
   x���          T   
   x���          O     x��U�n�F��+��(D�I�E4Yp]�I�5(���H� �� �d�N;6tUmZ�hE�h�a��.f~�_�s/I�v�.���}̜s�����ۏ��֣oD���r��t��m{n�w��N��D�M�$<��v�N��K�w�=�.6������;M�+������Cq�X3����iժ^�*�S՜u��Lˬ�u�^�=�XE���Cy.r*/e$6���m�i�n��=�����$���X��L��Pȹ��˩:�z!/c>ȱ:XN�٨L�~@�����ձzI��*2�ħ�VTm(ߓ3b�0��_��t(�z�:��Հ���%gM��#|�%"�M�����l˲��Ӭ�+#�Kl`?ɲp��ڧ;��7ti�/1" D�:��q���V�2]�����/��z���>�~����b8�^o���Ћm�Z�<װ�'����uV�;F�P%���9n5`ET�{���0Ԉ�#0��[��[rDZ!3�>D_�Q�#8�JR�Bo:E���oL��&�2#(��(@�L>��1�ԠN���G����!qڈ��xK�T#������Jz#ɓ�H��kE���NA�L��(㒨��|{�Y�nS�L��t-4�0,�7�-�S\@�������;�*rT��Fy#C�����y�h�Q�-��'�!Յ��j$>��A/l��א[�WB�*編��n?Zv(�~�_��uj�͆t�b����`>c�� e%��h�$���B���(�S ��uҁD��JU*WA�YY�Nɝ���D)���I��P�˄d3��#��K�����u�:"��E��ߌ�Fs�B�7`9��Ab�S���<SÏ���[jͤ}�r.���T:K�Z�KGf�?�*K��a���*>/��w��碰MLJ���v�B2����-�(~���[��ᚶV3*.�mKs��|�av�5f�g��<�O5M�LM�`�x�r�����,Y���L�c�9>��z��y�Y��{L�EH5��ݕ����.�����N�L�����2j��1][��C��      N   
   x���          V   
   x���          U   
   x���          W   
   x���          P   
   x���          L   �  x��Q�j1��+�s##i$�]�,�0����hFjHcc;�<(�(M�*��|���$����j�6�d��#s���i~�%���m�;�:Ec0<���@�c#b���� "}w84�^�S�v���_�����dcs��C��py�$�
���%Ԕ<�\����ؚzD�\p�uD�/��1��Ο�#��/��[xBx�O�9R&0�(�����*`��;��������0�߰�W-#"U��x9ҌE�������[��N:!K-S�J�J�Ǳ�"��iV:��J~&���EP�[��_0�U&\�ϓ?��`\���+��U L+ҳ�hBF��刽�	E�%q�9-D�R�XL�P4Ͳ�t��ư��)S�݄�_%����O���0���7\bjRف���]�$�!�Ȭ����J     