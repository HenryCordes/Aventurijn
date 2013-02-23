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


GO