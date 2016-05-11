Use ITMAppDB;

DROP TABLE Employee_Absence;
DROP TABLE Employee;
DROP TABLE Absence;


CREATE TABLE Absence
(
	Id INT PRIMARY KEY IDENTITY,
	AbsenceType NVARCHAR(50) NOT NULL,
)

/*FRÅNVARO-DATA*/
INSERT INTO Absence (AbsenceType)
VALUES	('Sjukdom'), 
		('VAB'), 
		('Föräldraledighet'), 
		('Semester'), 
		('Tjänstledighet')


CREATE TABLE Employee
(
	Id INT PRIMARY KEY IDENTITY,
	UserName NVARCHAR(100) NOT NULL UNIQUE,
	Name NVARCHAR(150) NOT NULL,
	Email NVARCHAR(200) NOT NULL UNIQUE
)


CREATE TABLE Employee_Absence
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employee(Id) NOT NULL,
	AbsenceId INT FOREIGN KEY REFERENCES Absence(Id) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE,
	Removed BIT NOT NULL,
)




/*STATISTIK*/

SELECT Employee.Name AS [Employee Name], 
Absence.AbsenceType AS [Absence Name],
Employee_Absence.StartDate AS [Start Date], 
Employee_Absence.EndDate AS [End Date],
Employee_Absence.Removed AS [Removed]
FROM Employee_Absence
JOIN Employee
ON Employee_Absence.EmployeeId = Employee.Id
JOIN Absence 
ON  Employee_Absence.AbsenceId = Absence.Id

SELECT * FROM Employee