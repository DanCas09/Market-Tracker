create extension if not exists pg_trgm SCHEMA pg_catalog;
drop table if exists post_comment;
drop table if exists post;
drop table if exists product_favourite;
drop table if exists list_entry;
drop table if exists list;
drop table if exists token;
drop table if exists product_review;
drop table if exists price_alert;
drop table if exists product_stats_counts;
drop table if exists fcm_registration;
drop table if exists moderator;
drop table if exists operator;
drop table if exists client;
drop table if exists "user";
drop table if exists promotion;
drop index if exists price_entry_index;
drop table if exists price_entry;
drop table if exists product_availability;
drop table if exists store;
drop table if exists city;
drop table if exists product;
drop table if exists brand;
drop table if exists category;
drop table if exists company;

create table if not exists brand
(
    id   int generated always as identity primary key,
    name varchar(50) unique not null
);

create table if not exists category
(
    id   int generated always as identity primary key,
    name varchar(50) unique not null
);

create table if not exists product
(
    id          varchar(18) primary key,
    name        varchar(100) not null,
    image_url   TEXT         not null,
    quantity    int                   default 1,
    unit        varchar(20)  not null default 'unidades' check (unit in ('unidades', 'kilogramas', 'gramas', 'litros', 'mililitros')),
    views       int          not null default 0,
    rating      float        not null default 0,
    brand_id    int references brand (id) on delete cascade,
    category_id int references category (id) on delete cascade
);

create table if not exists company
(
    id         int generated always as identity primary key,
    name       varchar(30) unique not null,
    created_at timestamp          not null default now()
);

create table if not exists city
(
    id   int generated always as identity primary key,
    name varchar(30) unique not null
);

create table if not exists store
(
    id         int generated always as identity primary key,
    name       varchar(30)         not null,
    address    varchar(200) unique not null,
    city_id    int references city (id) on delete cascade,
    company_id int references company (id) on delete cascade
);

create table if not exists price_entry
(
    id         varchar(25) primary key default substr(md5(random()::text), 1, 10),
    price      integer   not null,
    created_at timestamp not null      default now(),
    store_id   int references store (id) on delete cascade,
    product_id varchar(18) references product (id) on delete cascade
);

create index if not exists price_entry_index on price_entry (product_id, store_id);

create table if not exists promotion
(
    percentage     int       not null check (percentage between 0 and 100),
    created_at     timestamp not null default now(),
    price_entry_id varchar(25) primary key references price_entry (id) on delete cascade
);

create table if not exists product_availability
(
    is_available boolean   not null default true,
    last_checked timestamp not null default now(),
    product_id   varchar(18) references product (id) on delete cascade,
    store_id     int references store (id) on delete cascade,
    primary key (product_id, store_id)
);

create table if not exists "user"
(
    id         uuid primary key             default gen_random_uuid(),
    username   varchar(20) unique  not null,
    name       varchar(30),
    email      varchar(200) unique not null,
    password   varchar(30)         not null,
    created_at timestamp           not null default now()
);

create table if not exists client
(
    id         uuid primary key references "user" (id) on delete cascade,
    avatar_url TEXT
);

create table if not exists operator
(
    user_id      uuid primary key references "user" (id) on delete cascade,
    store_id     int references store (id) on delete cascade,
    phone_number int not null check (phone_number between 210000000 and 999999999)
);

create table if not exists moderator
(
    user_id uuid primary key references "user" (id) on delete cascade
);

create table if not exists token
(
    token_value VARCHAR(256) primary key,
    created_at  timestamp not null default now(),
    expires_at  timestamp not null,
    user_id     uuid references "user" (id) on delete cascade
);

create table if not exists fcm_registration
(
    client_id uuid references client (id) on delete cascade,
    token     varchar(255) not null,
    device_id varchar(255) not null,
    primary key (client_id, token, device_id)
);

create table if not exists product_review
(
    id         int generated always as identity,
    client_id  uuid references client (id) on delete cascade,
    product_id varchar(18) references product (id) on delete cascade,
    rating     int       not null check (rating between 1 and 5),
    text       varchar(255),
    created_at timestamp not null default now(),
    primary key (client_id, product_id)
);

create table if not exists product_favourite
(
    client_id  uuid references client (id) on delete cascade,
    product_id varchar(18) references product (id) on delete cascade,
    primary key (client_id, product_id)
);

create table if not exists price_alert
(
    client_id       uuid references client (id) on delete cascade,
    product_id      varchar(18) references product (id) on delete cascade,
    price_threshold int       not null,
    created_at      timestamp not null default now(),
    primary key (client_id, product_id)
);

create table if not exists product_stats_counts
(
    product_id varchar(18) primary key references product (id) on delete cascade,
    favourites int not null default 0,
    ratings    int not null default 0,
    lists      int not null default 0
);

create table if not exists list
(
    id          int generated always as identity primary key,
    client_id   uuid references client (id) on delete cascade,
    name        varchar(30) NOT NULL,
    archived_at timestamp,
    created_at  timestamp not null default now()
);

create table if not exists list_entry
(
    list_id    int references list (id) on delete cascade,
    product_id varchar(18) references product (id) on delete cascade,
    store_id   int references store (id) on delete cascade,
    quantity   int not null,
    primary key (list_id, product_id)
);

create table if not exists post
(
    id         int generated always as identity primary key,
    title      varchar(30)  not null,
    text       varchar(255) not null,
    created_at timestamp    not null default now(),
    client_id  uuid references client (id) on delete cascade,
    list_id    int references list (id) on delete cascade
);

create table if not exists post_comment
(
    text       varchar(255) not null,
    created_at timestamp    not null default now(),
    client_id  uuid references client (id) on delete cascade,
    post_id    int references post (id) on delete cascade,
    primary key (client_id, post_id)
);