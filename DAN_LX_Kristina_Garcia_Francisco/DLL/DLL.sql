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
	SectorName CHAR (40)						NOT NULL,
);

CREATE TABLE tblLocation(
	LocationID INT IDENTITY(1,1) PRIMARY KEY 		NOT NULL,
	LocationAddress		CHAR (40)					NOT NULL,
	City				CHAR (40)					NOT NULL,
	Country				CHAR (40)					NOT NULL,
);

CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 		NOT NULL,
	FirstName				CHAR (40)			NOT NULL,
	LastName				CHAR (40)			NOT NULL,
	JMBG					CHAR (13) UNIQUE	NOT NULL,
	Gender					CHAR				NOT NULL,
	DateOfBirth				DATE     			NOT NULL,
	IDCard					CHAR (9) UNIQUE		NOT NULL,
	PhoneNumber				CHAR (20) UNIQUE	NOT NULL,
	ManagerID INT FOREIGN KEY REFERENCES tblUser(UserID),
	SectorID INT FOREIGN KEY REFERENCES tblSector(SectorID) NOT NULL,
	LocationID INT FOREIGN KEY REFERENCES tblLocation(LocationID) NOT NULL,
);


