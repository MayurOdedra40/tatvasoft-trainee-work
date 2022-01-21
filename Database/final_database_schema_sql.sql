/* create database with name sample*/
create database sample


use sample
go



/* create schema helperland */

create schema helperland
go




/* create table emailletter which holds email of subscribed emailers*/

create table helperland.emailLetter
(
emails nvarchar(30) primary key
);

select * from helperland.emailLetter




/* this table contain gender of user by default it have F, M and O*/

create table helperland.gender
(
gender_id char primary key,
gender_type nvarchar(20) not null
)

select * from helperland.gender;




/* table contain roles of user it has three roles C-customer, S-service Provider, A-admin */

create table helperland.role
(
role_id char primary key,
role_type nvarchar(20) not null
)

select * from helperland.role




/* it contain city information city and its postal codes*/

create table helperland.city
(
city_id int identity(1,1) primary key,
city_name nvarchar(30) not null,
postal_code nvarchar(30) not null
)
	/* contraint to check combination of city name and postal code must be unqiue */
alter table helperland.city
add constraint uq_city unique(city_name, postal_code) 

select * from helperland.city





/* this table contain address of users*/

create table helperland.addresss
(
address_id int identity(1,1) primary key,
user_id int not null,
street_name nvarchar(30) not null,
house_name nvarchar(30) not null,
city_id int foreign key references helperland.city (city_id)
)
	/*  user_id, street name, house name and city id must be unique*/
alter  table helperland.addresss
add constraint uq_addresss unique(user_id,street_name,house_name,city_id)

select * from helperland.addresss






/*  table contain languages and specific id to it*/

create table helperland.language
(
language_id int identity(1,1) primary key,
language_type  nvarchar(30) unique
)

select * from helperland.language





/* this contain image name of 5 avtars for user */
create table helperland.avtar
(
avtar_id int identity(1,1) primary key,
avtar_name nvarchar(30) not null unique
)

select * from helperland.avtar





/*  this contains the users information*/
create table helperland.users
(
users_id int not null identity(1,1) primary key,
f_name nvarchar(30) not null,
l_name nvarchar(30) ,
email nvarchar(30) unique not null,
passwrd nvarchar(16) not null,
mobile_no numeric(10,0) not null unique,
dob date default null,
avatar int default 1 references helperland.avtar (avtar_id),
gender_id char default null references helperland.gender (gender_id),
role_id char default null references helperland.role (role_id),
[language] int default 1 references helperland.language (language_id) 
)





/* this table contain 2 rows only [F, favourite] and [B, Block] */
create table helperland.pros_type
(
protype_id char primary key,
protype_name nvarchar(10) not null unique
)

select * from helperland.pros_type





/*  this contain the details of blocked and pros user*/
create table helperland.pros
(
id int identity(1,1) primary key,
cust_id int not null references helperland.users (users_id),
sp_id int not null references helperland.users (users_id),
pro_type_id char default null references helperland.pros_type (protype_id) 
)

alter table helperland.pros
add constraint uq_pros_users unique(cust_id, sp_id)

select * from helperland.pros





/* this table contain 2 choice [Y, Yes] or [N, No]  */
create table helperland.choice
(
choice_id char primary key,
choice_type nvarchar(10) not null unique
)

select * from helperland.choice





/* this table contain different status detials like completed, pending, canceled */
create table helperland.status
(
status_id int identity(1,1) primary key,
status_type nvarchar(30) not null unique 
)

select * from helperland.status





/* this table contian rating no and its value like [3, good] and [2,weak] */
create table helperland.ratings
(
NOS numeric(1,0) identity(1,1) primary key,
rating nvarchar(20) not null unique
)

select * from helperland.ratings



/* This table contains the information related to services*/
create table helperland.services
(
service_id int identity(1,1) primary key,
cust_id int not null references helperland.users (users_id),
sp_id int default null references helperland.users (users_id),
DOS date not null,
TOS time(0) not null,
HOS int not null,
address_id int not null references helperland.addresss (address_id),
inside_cabinet char default 'N' references helperland.choice (choice_id),
inside_fridge char default 'N' references helperland.choice (choice_id),
inside_oven char default 'N' references helperland.choice (choice_id),
laundry char default 'N' references helperland.choice (choice_id),
interior_windows char default 'N' references helperland.choice (choice_id),
comment nvarchar(50) default null,
have_pet char default 'N' references helperland.choice (choice_id),
net_amount numeric(10,3) not null check (net_amount>0),
status_id int default null references helperland.status (status_id),
distance numeric(10,3) not null check (distance>0),
Rating numeric(1,0) default null references helperland.ratings (NOS),
rating_comment nvarchar(50) default null
)
