/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/*	----	Level vulling (Nivo)	----	*/

IF NOT EXISTS (SELECT * FROM [Level] WHERE Name = 'Onderbouw')
BEGIN
  INSERT INTO [Level] ([Name]) VALUES ('Onderbouw') 
END



IF NOT EXISTS (SELECT * FROM [Level] WHERE Name = 'Middenbouw')
BEGIN
  INSERT INTO [Level] ([Name]) VALUES ('Middenbouw') 
END


IF NOT EXISTS (SELECT * FROM [Level] WHERE Name = 'Bovenbouw')
BEGIN
  INSERT INTO [Level] ([Name]) VALUES ('Bovenbouw')  
END




/*	----	Subject vulling (onderwerpen)	----	*/

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'nwt')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('nwt','Natuur, wetenschap en techniek' )  
END


IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'nl')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('nl','Nederlands, taal' )  
END


IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'rw')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('rw','Rekenen, wiskunde' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'ao')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('ao','Algemene ontwikkeling' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'b')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('b','Biologie, natuur' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'cr')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('cr','Creativiteit, muziek' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'bw')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('bw','Bewustzijn' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'a')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('a','Aardrijkskunde' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'g')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('g','Geschiedenis' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'en')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('en','Engels' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'fr')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('fr','Frans' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'd')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('d','Duits' )  
END

IF NOT EXISTS (SELECT * FROM [Subject] WHERE [Code] = 'gm')
BEGIN
  INSERT INTO [Subject] ([Code], [Name]) VALUES ('gm','Gym, beweging' )  
END




/*	--		TESTDATA	--	*/

DECLARE @SubjectId int
DECLARE @currDate DATETIME
DECLARE @Subject varchar(100)
DECLARE @Code varchar(10)
DECLARE @LoopNotReady bit
DECLARE @LoopCounter int

SET @currDate = getutcdate()

/*	--		Activiteiten	--	*/
SET @LoopNotReady = 1
SET @LoopCounter = 0
PRINT @currDate
PRINT @LoopCounter

WHILE @LoopNotReady = 1
BEGIN
	SET @LoopCounter = @LoopCounter + 1
	PRINT @LoopCounter

	IF @LoopCounter = 1 
	BEGIN
		SET @Subject = 'Proefjes zuur/base'
		SET @Code = 'nwt'
	END
	IF @LoopCounter = 2 
	BEGIN 
		SET @Subject = 'Economie spel'
		SET @Code = 'nwt'
	END
	IF @LoopCounter = 3
	BEGIN 
		SET @Subject = 'Technisch lego'
		SET @Code = 'nwt'
	END
	IF @LoopCounter = 4
	BEGIN 
		SET @Subject = 'Lezen, schrijven'
		SET @Code = 'nl'
	END
	IF @LoopCounter = 5
	BEGIN 
		SET @Subject = 'Rekenen'
		SET @Code = 'rw'
	END
	IF @LoopCounter = 6
	BEGIN 
		SET @Subject = 'Rode kool koken'
		SET @Code = 'nwt'
	END
	IF @LoopCounter = 7
	BEGIN 
		SET @Subject = 'Staartdeling'
		SET @Code = 'rw'
	END
	IF @LoopCounter = 8
	BEGIN 
		SET @Subject = 'Marry workshop: hout, rozenbottels'
		SET @Code = 'b'
	END
	IF @LoopCounter = 9
	BEGIN 
		SET @Subject = 'Pantoun, tibetaans gedicht maken'
		SET @Code = 'nl'
	END
	IF @LoopCounter = 10
	BEGIN 
		SET @Subject = 'Vikingen'
		SET @Code = 'g'
		SET @LoopNotReady = 0
	END
	IF @LoopCounter > 10
	BEGIN 
		SET @LoopNotReady = 0
	END


	IF NOT EXISTS (SELECT * FROM [Activity] WHERE [Name] = @Subject)
	BEGIN
	PRINT 'IN IF: ' + @Subject
	SET @SubjectId = 0
	  SELECT @SubjectId = [SubjectId] FROM [Subject] WHERE [Code] = @Code
	  IF @SubjectId > 0
	  BEGIN
	  	INSERT INTO [Activity] ([SubjectId], [Name], [CreationDate]) VALUES (@SubjectId, @Subject, @currDate) 
	  END 
	END

END


/*	--		Students	--	*/
SET @LoopNotReady = 1
SET @LoopCounter = 0

DECLARE @LevelId int
DECLARE @LevelName varchar(100)
DECLARE @Name varchar(100)
DECLARE @Insertion varchar(10) 
DECLARE @LastName varchar(100)
DECLARE @BirthDate datetime

WHILE @LoopNotReady = 1
BEGIN
	SET @LoopCounter = @LoopCounter + 1
	PRINT @LoopCounter

	IF @LoopCounter = 1 
	BEGIN
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Sylvie'
		SET @Insertion  = '' 
		SET @LastName  = 'Dikkers'
		SET @BirthDate  = convert(datetime,'28-09-2001',105)
	END
	IF @LoopCounter = 2 
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Jarno'
		SET @Insertion  = '' 
		SET @LastName  = 'Dikkers'
		SET @BirthDate  = convert(datetime,'26-09-1997',105)
	END
	IF @LoopCounter = 3
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Thomas'
		SET @Insertion  = '' 
		SET @LastName  = 'Esser'
		SET @BirthDate  = convert(datetime,'14-02-1997',105)
	END
	IF @LoopCounter = 4
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Stern'
		SET @Insertion  = 'van der' 
		SET @LastName  = 'Heide'
		SET @BirthDate  = convert(datetime,'04-07-1999',105)
	END
	IF @LoopCounter = 5
	BEGIN 
		SET @LevelName = 'Onderbouw'
		SET @Name  = 'Igor'
		SET @Insertion  = ''
		SET @LastName  = 'Perik'
		SET @BirthDate  = convert(datetime,'28-01-2006',105)
	END
	IF @LoopCounter = 6
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Tomas'
		SET @Insertion  = ''
		SET @LastName  = 'Ritsma van Eck'
		SET @BirthDate  = convert(datetime,'07-06-1996',105)
	END
	IF @LoopCounter = 7
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Sofie'
		SET @Insertion  = ''
		SET @LastName  = 'Perik'
		SET @BirthDate  = convert(datetime,'29-12-2004',105)
	END
	IF @LoopCounter = 8
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Storm '
		SET @Insertion  =  ''
		SET @LastName  = 'Ritsma van Eck'
		SET @BirthDate  = convert(datetime,'12-04-1998',105)
		
	END
	IF @LoopCounter = 9
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Duco'
		SET @Insertion  = ''
		SET @LastName  = 'Ritsma van Eck'
		SET @BirthDate  = convert(datetime,'31-08-2002',105)
	END
	IF @LoopCounter = 10
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Taco'
		SET @Insertion  = ''
		SET @LastName  = 'Ritsma van Eck'
		SET @BirthDate  = convert(datetime,'31-08-2002',105)
		
	END
	IF @LoopCounter = 11
	BEGIN 
		SET @LevelName = 'Onderbouw'
		SET @Name  = 'Aëla'
		SET @Insertion  = ''
		SET @LastName  = 'Smeehuyzen'
		SET @BirthDate  = convert(datetime,'30-10-2006',105)
	END
	IF @LoopCounter = 12
	BEGIN 
		SET @LevelName = 'Onderbouw'
		SET @Name  = 'Marie-louise'
		SET @Insertion  = ''
		SET @LastName  = 'Strijker'
		SET @BirthDate  = convert(datetime,'12-11-2005',105)	
	END
	IF @LoopCounter = 13
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Dylan'
		SET @Insertion  = ''
		SET @LastName  = 'Strijker'
		SET @BirthDate  = convert(datetime,'19-08-2001',105)	
	END
	IF @LoopCounter = 14
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Lester'
		SET @Insertion  = ''
		SET @LastName  = 'Strijker'
		SET @BirthDate  = convert(datetime,'15-04-2004',105)
	END
	IF @LoopCounter = 15
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Aimy'
		SET @Insertion  = ''
		SET @LastName  = 'Tomeï'
		SET @BirthDate  = convert(datetime,'14-05-1996',105)
	END
	IF @LoopCounter = 16
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Floor'
		SET @Insertion  = 'de'
		SET @LastName  = 'Voogt'
		SET @BirthDate  = convert(datetime,'08-11-2003',105)
	END
	IF @LoopCounter = 17
	BEGIN 
		SET @LevelName = 'Onderbouw'
		SET @Name  = 'Thessa'
		SET @Insertion  = 'de'
		SET @LastName  = 'Voogt'
		SET @BirthDate  = convert(datetime,'26-04-2006',105)
	END
	IF @LoopCounter = 18
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Aldo'
		SET @Insertion  = 'de'
		SET @LastName  = 'Vos'
		SET @BirthDate  = convert(datetime,'14-10-1996',105)
		
	END
	IF @LoopCounter = 19
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Yannick'
		SET @Insertion  = 'de'
		SET @LastName  = 'Vroed'
		SET @BirthDate  = convert(datetime,'02-01-1998',105)
	END
	IF @LoopCounter = 20
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Zoë'
		SET @Insertion  = 'de'
		SET @LastName  = 'Vroed'
		SET @BirthDate  = convert(datetime,'05-06-1996',105)
	END
	IF @LoopCounter = 21
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Fleur'
		SET @Insertion  = 'van'
		SET @LastName  = 'Willige'
		SET @BirthDate  = convert(datetime,'13-05-2003',105)
	END
	IF @LoopCounter = 22
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Jolie'
		SET @Insertion  = 'van'
		SET @LastName  = 'Willige'
		SET @BirthDate  = convert(datetime,'23-03-2005',105)
	END
	IF @LoopCounter = 23
	BEGIN 
		SET @LevelName = 'Bovenbouw'
		SET @Name  = 'Sofie'
		SET @Insertion  = ''
		SET @LastName  = 'Veerbeek'
		SET @BirthDate  = convert(datetime,'13-07-1998',105)
	END
	IF @LoopCounter = 24
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Tom'
		SET @Insertion  = ''
		SET @LastName  = 'Veerbeek'
		SET @BirthDate  = convert(datetime,'05-02-2003',105)
	END
	IF @LoopCounter = 25
	BEGIN 
		SET @LevelName = 'Onderbouw'
		SET @Name  = 'Maya'
		SET @Insertion  = ''
		SET @LastName  = 'Veerbeek'
		SET @BirthDate  = convert(datetime,'16-01-2008',105)	
	END
	IF @LoopCounter = 26
	BEGIN 
		SET @LevelName = 'Middenbouw'
		SET @Name  = 'Nemo'
		SET @Insertion  = ''
		SET @LastName  = 'Boertjens'
		SET @BirthDate  = convert(datetime,'04-04-2002',105)
	END
	IF @LoopCounter = 27
	BEGIN 
		SET @LevelName = 'Onderbouw'
		SET @Name  = 'Puk'
		SET @Insertion  = ''
		SET @LastName  = 'Cordes'
		SET @BirthDate  = convert(datetime,'23-07-2005'	,105)
		SET @LoopNotReady = 0
	END
	IF @LoopCounter > 27
	BEGIN 
		SET @LoopNotReady = 0
	END


	IF NOT EXISTS (SELECT * FROM [Student] WHERE [Name] = @Name AND [LastName] = @LastName)
	BEGIN
	PRINT 'IN IF: ' + @Name
	  SET @LevelId = 0
	  SELECT @LevelId = [LevelId] FROM [Level] WHERE [Name] = @LevelName
	  IF @LevelId > 0
	  BEGIN
	  	INSERT INTO [Student] ([Name], [Insertion], [LastName], [BirthDate], [LevelId]) VALUES (@Name, @Insertion, @LastName, @BirthDate, @LevelId) 
	  END 
	END
END

GO