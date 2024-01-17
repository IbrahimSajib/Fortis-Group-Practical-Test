USE master
Go

CREATE DATABASE ItemDb
GO

USE ItemDb
GO

CREATE TABLE Category
(
CategoryId INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(30) NOT NULL
)

INSERT INTO Category VALUES('Smartphones'),('Laptops'),('Headphones'),('Smartwatches'),('Tablets'),('Cameras')CREATE TABLE Item
(
ItemId INT PRIMARY KEY IDENTITY,
ItemName NVARCHAR(30) UNIQUE NOT NULL,CategoryId INT FOREIGN KEY REFERENCES Category(CategoryId) NOT NULL,[Status] BIT NOT NULL)INSERT INTO Item VALUES('Apple iPhone 13 Pro Max',1,1),('MacBook Pro 16-inch',2,1),('Canon EOS R6',6,0),('Samsung Galaxy Watch 4',4,1)