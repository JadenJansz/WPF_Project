
create schema Hotel

create table Hotel.RoleT(
	RoleId varchar(5) primary key,
	Role_Name varchar(15));

create table Hotel.UserT(
	RoleID varchar(5) references Hotel.RoleT(RoleId),
	UserID varchar(6) primary key,
	UserName varchar(30),
	Email varchar(20) not null,
	Password varchar(15) not null,
	URec_ID int);

create table Hotel.Customer(
	Rec_ID int,
	Cus_ID varchar(5) primary key,
	Cus_Name varchar(20) not null,
	Cus_Address varchar(30) not null,
	Cus_Tel int,
	Cus_Email varchar(30));

create table Hotel.Room(
	RoomID varchar(5) primary key,
	Room_type varchar(10) not null,
	Room_Price decimal(7,2) not null,
	Room_Floor varchar(10),
	Room_Availability varchar(3),
	Rec_ID int);

create table Hotel.Cus_Room(
	RoomId varchar(5) references Hotel.Room(RoomID),
	CusID varchar(5) references Hotel.Customer(Cus_ID),
	Check_In varchar(10) not null,
	Check_Out varchar(10) not null,
	Price decimal(7,2) not null,
	RCount int,
	primary key(RoomId,CusID));



create table Hotel.Customer_Audits(
	Rec_ID int identity primary key,
	Cus_ID varchar(5),
	Cus_Name varchar(20) not null,
	Cus_Address varchar(30) not null,
	Cus_Tel int,
	Cus_Email varchar(30),
	Updated_at datetime,
	Operation varchar(8) check(Operation in ('INS','DEL','UPD')));


create trigger Trig_Customer
on Hotel.Customer
after insert,update,delete
as begin
insert into Hotel.Customer_Audits(Cus_ID,Cus_Name,Cus_Address,Cus_Tel,Cus_Email,Updated_at,Operation)
select Cus_ID,Cus_Name,Cus_Address,Cus_Tel,Cus_Email,getdate(),'INS'
from inserted
union all
select Cus_ID,Cus_Name,Cus_Address,Cus_Tel,Cus_Email,getdate(),'DEL'
from deleted
end

create table Hotel.User_Audits(
	URec_ID int identity primary key,
	RoleID varchar(5) references Hotel.RoleT(RoleId),
	UserID varchar(6),
	UserName varchar(30),
	Email varchar(20) not null,
	Password varchar(15) not null,
	Updated_at datetime,
	Operation varchar(8) check(Operation in ('INS','DEL','UPD')));

create trigger Trig_User
on Hotel.UserT
after insert,update,delete
as begin
insert into Hotel.User_Audits(RoleID,UserID,UserName,Email,Password,Updated_at,Operation)
select RoleID,UserID,UserName,Email,Password,getdate(),'INS'
from inserted
union all
select RoleID,UserID,UserName,Email,Password,getdate(),'DEL'
from deleted
end


create table Hotel.Room_Audits(
	RRecID int identity primary key,
	RoomID varchar(5),
	Room_type varchar(10) not null,
	Room_Price decimal(7,2) not null,
	Room_Floor varchar(10),
	Room_Availability varchar(10),
	Updated_at datetime,
	Operation varchar(8) check(Operation in ('INS','DEL','UPD')));

create trigger Trig_Room
on Hotel.Room
after insert,update,delete
as begin
insert into Hotel.Room_Audits(RoomID,Room_type,Room_Price,Room_Floor,Room_Availability,Updated_at,Operation)
select RoomID,Room_type,Room_Price,Room_Floor,Room_Availability,getdate(),'INS'
from inserted
union all
select RoomID,Room_type,Room_Price,Room_Floor,Room_Availability,getdate(),'DEL'
from deleted
end

insert into Hotel.RoleT
values ('R','Reception'),
		('M','Manager'),
		('A','Admin');


-------------------------------------

