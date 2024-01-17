--Using master Database
USE master
GO

--Deleting Database If  Already Exist
--DROP DATABASE IF EXISTS StudentDb
--GO

--Creating Database -> StudentDb
CREATE DATABASE StudentDb
GO

--Using Database -> StudentDb
USE StudentDb
GO

--Creating Students Table
CREATE TABLE Students (
    ID INT PRIMARY KEY,
    [Name] NVARCHAR(30)
)
GO

--Insert Data into Students Table
INSERT INTO Students VALUES
(1, 'Brandon Wilson'),
(2, 'Jack Martin'),
(3, 'Kyle Rodriguez'),
(4, 'Charles Clark'),
(5, 'Kevin Young'),
(6, 'Edward Anderson'),
(7, 'Eric King'),
(8, 'James Allen'),
(9, 'Christopher Lee'),
(10, 'Anthony Brown'),
(11, 'David Green')
GO
--Output
SELECT ID AS [Student Id], [Name] AS [Student Name] FROM Students
GO


--Creating PassedStudent Table
--Suppose pass mark is between 33 to 100
CREATE TABLE PassedStudent (
    ID INT PRIMARY KEY,
    Marks INT CHECK(Marks>32 AND Marks<=100),
    FOREIGN KEY (ID) REFERENCES Students(ID)
)
GO

--Insert Data into PassedStudent Table
INSERT INTO PassedStudent VALUES
(1, 82),
(2, 60),
(3, 42),
(5, 55),
(6, 90),
(7, 99),
(9, 68),
(11, 85)
GO
--Output
SELECT ID AS [Student Id], Marks FROM PassedStudent
GO

--Creating View -> vwFailedStudents
CREATE VIEW vwFailedStudents AS
SELECT ID, [Name]
FROM Students
WHERE ID NOT IN (SELECT ID FROM PassedStudent);
GO
--Output
SELECT * FROM vwFailedStudents
GO

--Creating Store Procedure -> dbo.spGetStudentWithPosition
CREATE PROCEDURE dbo.spGetStudentWithPosition
    @Position INT
AS
BEGIN
    SELECT  ID AS [Student Id], [Name] AS [Student Name],Marks,Position
    FROM (
        SELECT Students.ID, Name, Marks, ROW_NUMBER() OVER (ORDER BY Marks DESC) AS Position
        FROM Students
        JOIN PassedStudent ON Students.ID = PassedStudent.ID
    ) AS StudentsWithPosition
    WHERE Position = @Position;
END;
GO

--Example:
EXEC dbo.spGetStudentWithPosition @Position = 1
GO
EXEC dbo.spGetStudentWithPosition @Position = 2
GO
EXEC dbo.spGetStudentWithPosition @Position = 3
GO
EXEC dbo.spGetStudentWithPosition @Position = 4
GO