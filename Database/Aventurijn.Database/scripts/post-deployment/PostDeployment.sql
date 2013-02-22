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


GO

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




GO