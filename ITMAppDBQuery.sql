Use ITMAppDB;

DROP TABLE Employee_Absence;
DROP TABLE Lunch_Employee
DROP TABLE Employee;
DROP TABLE Absence;
DROP TABLE Lunch;
DROP TABLE Restaurant


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

INSERT INTO Employee (UserName, Name, Email)
VALUES	('petpan', 'Peter Pan', 'Peter.Pan@itmastaren.se'),
		('snövit', 'Snö Vit', 'Snö.Vit@itmastaren.se'),
		('tinlin', 'Tinge Ling', 'Tinge.Ling@itmastaren.se'),
		('kapkro', 'Kapten Krok', 'Kapten.Krok@itmastaren.se'),
		('johbra', 'Johnny Bravo', 'Johnny.Bravo@itmastaren.se')

CREATE TABLE Employee_Absence
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employee(Id) NOT NULL,
	AbsenceId INT FOREIGN KEY REFERENCES Absence(Id) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE,
	Removed BIT NOT NULL,
)

CREATE TABLE Restaurant
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	ImagePath NVARCHAR(100) NOT NULL
)

INSERT INTO Restaurant (Name, ImagePath)
VALUES	('Burger Love', 'burgerlove.jpg'),
		('McDonald´s', 'mcdonalds.jpg'),
		('Burger King', 'burgerking.jpg'),
		('Sallad Inn', 'salladinn.jpg'),
		('Dannes', 'dannes.jpg'),
		('Grekiska Kolgrillsbaren', 'greken.jpg'),
		('Sweet Chili', 'sweetchili.jpg'),
		('Coco Thai', 'cocothai.jpg'),
		('Subway', 'subway.jpg'),
		('Texas Star', 'texasstar.jpg'),
		('Saluhallen', 'saluhallen.jpg'),
		('Rosalis Deli', 'rosalis.jpg'),
		('Wongs', 'wongs.jpg'),
		('Pitcher´s', 'pitchers.jpg'),
		('Mike´s Foodtruck', 'mikesfoodtruck.jpg'),
		('Sushi Yama', 'sushiyama.jpg'),
		('La Pampa', 'pampa.jpg'),
		('Kinamuren', 'kinamuren.jpg'),
		('Texas Longhorn', 'texaslonghorn.jpg'),
		('Strike & Co', 'strikeoco.jpg'),
		('NorrTull', 'norrtull.jpg'),
		('Wobbler', 'wobbler.jpg'),
		('Jensens Bofhus', 'jensen.jpg'),
		('Svalan', 'svalan.jpg')


CREATE TABLE Lunch 
(
	Id INT PRIMARY KEY IDENTITY,
	RestaurantId INT FOREIGN KEY REFERENCES Restaurant(Id) NOT NULL,
	LunchTime DATETIME NOT NULL,
	Removed BIT NOT NULL
)

INSERT INTO Lunch (RestaurantId, LunchTime, Removed)
VALUES	('1', '2016-05-24 18:00', 0),
		('2', '2016-05-24 18:00', 0),
		('3', '2016-05-24 18:00', 0),
		('4', '2016-05-24 18:00', 0),
		('5', '2016-05-24 18:00', 0)

CREATE TABLE Lunch_Employee
(
	EmployeeId INT FOREIGN KEY REFERENCES Employee(Id) NOT NULL,
	LunchId INT FOREIGN KEY REFERENCES Lunch(Id) NOT NULL,
)

--ALTER TABLE Lunch_Employee
--ADD CONSTRAINT PK_LunEmp
--PRIMARY KEY (EmployeeId, LunchId)

INSERT INTO Lunch_Employee (EmployeeId, LunchId)
VALUES	(1 , 1), 
		(2 , 2),
		(3 , 3), 
		(4 , 4),
		(5 , 5)




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

SELECT * FROM Restaurant

SELECT * FROM Lunch

SELECT Lunch.Id AS [Lunch ID], 
Employee.Name AS [Employee Name]
FROM Lunch_Employee
JOIN Employee
ON Lunch_Employee.EmployeeId = Employee.Id
JOIN Lunch 
ON  Lunch_Employee.LunchId = Lunch.Id

SELECT Employee.Name
FROM Lunch_Employee
JOIN Employee
ON Lunch_Employee.EmployeeId = Employee.Id