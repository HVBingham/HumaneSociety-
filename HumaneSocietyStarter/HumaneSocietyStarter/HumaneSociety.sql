CREATE TABLE Employees (EmployeeId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), EmployeeNumber INTEGER, Email VARCHAR(50));
CREATE TABLE Categories (CategoryId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY, RoomNumber INTEGER, AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE AnimalShots (AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ShotId INTEGER FOREIGN KEY REFERENCES Shots(ShotId), DateReceived DATE, CONSTRAINT AnimalShotId PRIMARY KEY (AnimalId, ShotId));
CREATE TABLE USStates (USStateId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Abbreviation VARCHAR(2));
CREATE TABLE Addresses (AddressId INTEGER IDENTITY (1,1) PRIMARY KEY, AddressLine1 VARCHAR(50), City VARCHAR(50), USStateId INTEGER FOREIGN KEY REFERENCES USStates(USStateId),  Zipcode INTEGER); 
CREATE TABLE Clients (ClientId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), AddressId INTEGER FOREIGN KEY REFERENCES Addresses(AddressId), Email VARCHAR(50));
CREATE TABLE Adoptions(ClientId INTEGER FOREIGN KEY REFERENCES Clients(ClientId), AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ApprovalStatus VARCHAR(50), AdoptionFee INTEGER, PaymentCollected BIT, CONSTRAINT AdoptionId PRIMARY KEY (ClientId, AnimalId));

INSERT INTO Categories VALUES('Cat');
INSERT INTO Categories VALUES('Dog');
INSERT INTO Categories VALUES('Birds');
INSERT INTO Categories VALUES('Lizards');
INSERT INTO Categories VALUES('Rodents');
INSERT INTO Employees VALUES('Liz','Curro','CurLiz','StanleyCup10',3214,'lizCur@gmail.com');
INSERT INTO Employees VALUES('David','Beachem', 'BeaDav','Packers412',3215,'DavBea@gmail.com');
INSERT INTO Employees VALUES('Amber','Dupras','DupAmb','Subie93',3216,'AmbDup@gmail.com');
INSERT INTO Employees VALUES('Melissa','Fenninger','FenMel','AlleyTro247',3217,'MelMarFen@gmail.com');
INSERT INTO Employees Values('Tyre','Ludeke','LudTyr','MonoLabe889',3218,'LudekeTyre@gmail.com');
SELECT * FROM Categories
SELECT * FROM Employees
INSERT INTO DietPlans VALUES('Raw Cat','Varried Raw Meats',1.5);
INSERT INTO DietPlans VALUES('Seeds','ZuPreem Fruit Blend',0.5);
INSERT INTO DietPlans VALUES('Dog Puppy','Wellness Just For Puppies',1);
INSERT INTO DietPlans VALUES('Cat Adult','Fromm Beef Livattini',.5);
INSERT INTO DietPlans VALUES('Dog Adult','Wellness TruFood',2);
SELECT * FROM DietPlans
INSERT INTO Shots VALUES('Rabies');
INSERT INTO Shots VALUES('Distemper');
INSERT INTO Shots VALUES('Parvo');
INSERT INTO Shots VALUES('CPV-2');
INSERT INTO Shots VALUES('CAV-2');
SELECT * FROM Shots
SELECT * FROM DietPlans
SELECT * FROM Employees
SELECT * FROM Categories
SELECT * FROM Animals
SELECT * FROM Rooms
INSERT INTO Animals VALUES('Nitro',95,4,'Hyper',1,1,'Male','adopted',2,5,null);
INSERT INTO Animals VALUES('Alley',45,3,'Shy',0,0,'Female','not adopted',2,5,null);
INSERT INTO Animals VALUES('Liam',10,6,'Loving',1,1,'Male','adopted',1,4,null);
INSERT INTO Animals VALUES('Cyan',8,2,'Skiddish',0,0,'Male','not adopted',1,4,null);
INSERT INTO Animals VALUES('Georgia',50,2,'Chill',1,1,'Female','adopted',2,5,null);

INSERT INTO Rooms VALUES(1,2);
INSERT INTO Rooms VALUES(2,1);
INSERT INTO Rooms VALUES(3,3);
INSERT INTO Rooms VALUES(4,4);
INSERT INTO Rooms VALUES(5,5);
INSERT INTO Rooms VALUES(6,null);
INSERT INTO Rooms VALUES(7,null);
INSERT INTO Rooms VALUES(8,null);
INSERT INTO Rooms VALUES(9, null);
INSERT INTO Rooms VALUES(10, null);

INSERT INTO Clients VALUES('Madeline', 'Sanchez', 'SanMad', 'sanMAD123', null, 'madsan@gmail.com');
INSERT INTO Clients VALUES('Jose', 'Fuentez', 'JosFue', 'fueJOS123', null, 'josfue@gmail.com');
INSERT INTO Clients VALUES('Maria', 'Santos', 'MarSan', 'sanMAR123', null, 'marsan@gmail.com');
INSERT INTO Clients VALUES('George', 'Mendoza', 'GeoMen', 'menGEO123', null, 'geomen@gmail.com');
INSERT INTO Clients VALUES('Andrea', 'Gonzalez', 'AndGon', 'gonAND123', null, 'andgon@gmail.com');

SELECT * FROM Clients
