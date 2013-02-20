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