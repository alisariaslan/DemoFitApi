drop table memberships;
drop table products;
drop table logs;
drop table users;

create table users (
"id" serial primary key,
"username" varchar,
"password" varchar,
"registerdate" date
);
create table memberships  (
"user_id" serial primary key,
"product_id" integer,
"start" date,
"end" date
);
create table logs  (
"user_id" serial primary key,
"lastloggedin" date,
"loggedcount" integer default 0
);
create table products  (
"id" serial primary key,
"name" varchar,
"price" money,
"sellcount" integer
);

insert into products ("name","price","sellcount") values ('1 GÜN STANDART PAKET',15,1);
insert into products ("name","price","sellcount") values ('7 GÜN STANDART PAKET',50,15);
insert into products ("name","price","sellcount") values ('15 GÜN STANDART PAKET',100,100);
insert into products ("name","price","sellcount") values ('21 GÜN STANDART PAKET',150,50);
insert into products ("name","price","sellcount") values ('1 AY STANDART PAKET',200,997);
insert into products ("name","price","sellcount") values ('3 AY STANDART PAKET',350,1456);
insert into products ("name","price","sellcount") values ('6 AY STANDART PAKET',700,225);
insert into products ("name","price","sellcount") values ('12 AY STANDART PAKET',1000,58);
insert into products ("name","price","sellcount") values ('24 AY STANDART PAKET',1500,12);
insert into users ("username","password","registerdate") values ('test','1234',pg_catalog.now());
insert into memberships("product_id","start","end") values (1,'2023-02-01','2023-05-01');
insert into logs("loggedcount") values (0);

insert into users ("username","password","registerdate") values ('ali','1234',pg_catalog.now());
insert into memberships("product_id","start","end") values (1,'2023-02-01','2023-05-01');
insert into logs("loggedcount") values (0);

insert into users ("username","password","registerdate") values ('zurna','1234',pg_catalog.now());
insert into memberships("product_id","start","end") values (1,'2023-02-01','2023-05-01');
insert into logs("loggedcount") values (0);

ALTER TABLE memberships ADD FOREIGN KEY (user_id) REFERENCES users(id);
ALTER TABLE memberships ADD FOREIGN KEY (product_id) REFERENCES products(id);
ALTER TABLE logs ADD FOREIGN KEY (user_id) REFERENCES users(id);

SELECT * FROM public.users; 
SELECT * FROM public.memberships;
SELECT * FROM public.logs;
SELECT * FROM public.products;









