create database йспянбни
use йспянбни
CREATE TABLE Restaurant
(Rest_ID int NOT NULL PRIMARY KEY identity(1,1),
[Name] varchar(30) NOT NULL,
Number_of_tables int NOT NULL,
Start_time int NOT NULL,
End_time int NOT NULL,
Restaurant_String_Image varchar(max) NOT NULL,
Type_of_cuisine varchar(30) NOT NULL);
Create table [User]
(
	[User_ID] int NOT NULL primary key identity(1,1),
	Email varchar(30) NOT NULL,
	[Password] varchar(max) NOT NULL,
	Is_admin bit NOT NULL,
	[Name] varchar(30) NOT NULL,
	[Surname] varchar(30) NOT NULL
)
create table Rating
(
	[Rate_ID] int NOT NULL primary key identity(1,1),
	Rate int NOT NULL,
	[User_ID] int NOT NULL,
	Rest_ID int NOT NULL,
	FOREIGN KEY ([User_ID]) REFERENCES [User]([User_ID]),
	FOREIGN KEY ([Rest_ID]) REFERENCES Restaurant([Rest_ID])
)
create table Booking
(
	[Booking_ID] int NOT NULL primary key identity(1,1),
	Rest_ID int NOT NULL,
	[User_ID] int NOT NULL,
	[DateTime] datetime NOT null,
	Number_of_table int not null,
	FOREIGN KEY ([User_ID]) REFERENCES [User]([User_ID]),
	FOREIGN KEY ([Rest_ID]) REFERENCES Restaurant([Rest_ID])
)
INSERT INTO [User] (Email,[Password],Is_admin,[Name],Surname) VALUES ('admin@gmail.com', '4239f1628a6cb9b722888a1bf94c9263b0ee47d2afef92b087fffed1bb284576',1,'Admin','Admin')
