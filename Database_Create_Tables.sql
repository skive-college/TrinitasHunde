CREATE TABLE PinTypes (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Type] varchar(50) NOT NULL);
CREATE TABLE LocationTypes (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Type] varchar(50) NOT NULL);
CREATE TABLE Locations (
	[ID] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(100) NOT NULL,
	[GPSLatitude] float NOT NULL,
	[GPSLongitude] float NOT NULL,
	[LocationType] int NOT NULL FOREIGN KEY REFERENCES LocationTypes([ID]),
	[PinType] int NOT NULL FOREIGN KEY REFERENCES PinTypes([ID]));