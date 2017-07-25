CREATE DATABASE UserRegistration
GO

USE UserRegistration
GO

CREATE TABLE Users
(
	UserID			int PRIMARY KEY IDENTITY(10000,1)
	,Username		varchar(50)		NOT NULL
	,Password		varchar(50)		NOT NULL
	,Email			varchar(150)	NOT NULL
	,DateOfBirth	date			NOT NULL
)
GO