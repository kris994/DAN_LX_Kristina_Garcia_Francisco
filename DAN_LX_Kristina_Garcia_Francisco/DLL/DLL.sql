IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;
IF OBJECT_ID('tblSector', 'U') IS NOT NULL DROP TABLE tblSector;
IF OBJECT_ID('tblLocation', 'U') IS NOT NULL DROP TABLE tblLocation;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EmployeeDB')
CREATE DATABASE EmployeeDB;
GO

USE EmployeeDB
CREATE TABLE tblSector(
	SectorID INT IDENTITY(1,1) PRIMARY KEY 		NOT NULL,
	SectorName VARCHAR (40)	UNIQUE				NOT NULL,
);

CREATE TABLE tblLocation(
	LocationID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	LocationAddress		VARCHAR (40)			NOT NULL,
	City				VARCHAR (40)			NOT NULL,
	Country				VARCHAR (40)			NOT NULL,
);

CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 		NOT NULL,
	FirstName				VARCHAR (40)		NOT NULL,
	LastName				VARCHAR (40)		NOT NULL,
	JMBG					VARCHAR (13) UNIQUE	NOT NULL,
	Gender					VARCHAR				NOT NULL,
	DateOfBirth				DATE     			NOT NULL,
	IDCard					VARCHAR (9) UNIQUE	NOT NULL,
	PhoneNumber				VARCHAR (20) UNIQUE	NOT NULL,
	ManagerID INT FOREIGN KEY REFERENCES tblUser(UserID),
	SectorName VARCHAR (40) FOREIGN KEY REFERENCES tblSector(SectorName) NOT NULL,
	LocationID INT FOREIGN KEY REFERENCES tblLocation(LocationID) NOT NULL,
);


