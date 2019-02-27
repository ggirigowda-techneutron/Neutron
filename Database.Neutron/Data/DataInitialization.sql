-- BEGIN TRANSACTION
BEGIN TRANSACTION;
BEGIN TRY  

	-- COUNTRIES
	DECLARE @countries UNIQUEIDENTIFIER = '6ae2f76d-2afd-4260-b4af-78f1c345d4e9';
	PRINT @countries; 
	INSERT INTO [Utility].[Reference] ([Id], [Name], [Description], [CountryCode]) VALUES (@countries, 'LstCountries', 'List of countries', 'US');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('2af6ff6c-8bb8-46f0-b27e-81def1b76b64', @countries, 'US', 'United States of America');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('c78227a5-ca89-4b9d-aa6a-e5d779b94b20', @countries, 'IN', 'India');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('70efb18e-b531-4acd-a784-70e91ee89d4c', @countries, 'GH', 'Ghana');
	
	---- GENDER
	DECLARE @genders UNIQUEIDENTIFIER = 'b7f857d8-a240-4c5c-b151-dbe8f9b2a470';
	PRINT @genders; 
	INSERT INTO [Utility].[Reference] ([Id], [Name], [Description], [CountryCode]) VALUES (@genders, 'LstGenders', 'List of genders', 'US');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('8a29a4ab-62a7-4a06-b2fa-46a40f449a84', @genders, 'M', 'Male');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('4aa1d4e0-6162-470b-b0d9-a569e482c5c0', @genders, 'F', 'Female');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('734997e9-e0c4-4e0e-86ef-ff3471ec6b05', @genders, 'U', 'Undisclosed');
	
	-- USERTYPES
	DECLARE @userTypes UNIQUEIDENTIFIER = 'ad695e54-df4a-410b-9757-6b7b512c679a';
	PRINT @usertypes; 
	INSERT INTO [Utility].[Reference] ([Id], [Name], [Description], [CountryCode]) VALUES (@userTypes, 'LstUserTypes', 'List of user types', 'US');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('e21dcb05-1f7b-4c95-9c29-0e583b120e44', @userTypes, 'EMP', 'Employee');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('5ebf5cca-df92-49c6-ae5f-f3c9670bf9d3', @userTypes, 'CUS', 'Customer');

	-- ADDRESSTYPES
	DECLARE @addressTypes UNIQUEIDENTIFIER = '7540c54e-fbc5-4d9f-b7a1-eec380a4b90a';
	PRINT @addressTypes;
	INSERT INTO [Utility].[Reference] ([Id], [Name], [Description], [CountryCode]) VALUES (@addressTypes, 'LstAddressTypes', 'List of address types', 'US');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('e0e08fcd-a1e3-4810-ab49-7f49124b52d3', @addressTypes, 'MA', 'Mailing');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('781469bf-8815-478c-b1ef-8baf06149f07', @addressTypes, 'BI', 'Billing');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('89917168-ff35-4619-a500-632410868499', @addressTypes, 'SH', 'Shipping');
	INSERT INTO [Utility].[ReferenceItem] ([Id], [ReferenceId], [Code] , [Description]) VALUES ('9f131320-420b-43cc-af22-0d60400fe8dd', @addressTypes, 'HO', 'Home');
	
	-- USER
	DECLARE @ggUserId UNIQUEIDENTIFIER = '88237092-ed98-4da9-98e6-2de1d10e0fd0';	
	PRINT @ggUserId;
	INSERT INTO [Administration].[Users] (Id, UserName, Email, EmailConfirmed, PasswordHash, SecurityStamp, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, CreatedOn, ChangedOn, PhoneNumberConfirmed, TwoFactorEnabled) 
		   VALUES(@ggUserId, 'girish66', 'girish66@hotmail.com', 1, 'sha1:64000:18:AiAumzF43i+StrW+dOlP7B/KObxXZnmb:3/93IWZSZrLRBoj0TcuABTDY', NEWID(), '1/1/1900', 1, 0, GETUTCDATE(), GETUTCDATE(), 0, 0);
	-- USER PROFILE	   
	INSERT INTO [Administration].[UserProfile] (UserId, FirstName, LastName, UserTypeId, GenderId, CountryId) VALUES(@ggUserId, 'Girish', 'Girigowda', 'e21dcb05-1f7b-4c95-9c29-0e583b120e44', '8a29a4ab-62a7-4a06-b2fa-46a40f449a84', '2af6ff6c-8bb8-46f0-b27e-81def1b76b64');
	-- USER CLAIMS
	INSERT INTO [Administration].[UserClaim]([UserId], [ClaimType], [ClaimValue]) VALUES(@ggUserId, 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'ADMIN')
	INSERT INTO [Administration].[UserClaim]([UserId], [ClaimType], [ClaimValue]) VALUES(@ggUserId, 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'USER')
		  
	-- COMMITT TRANSACTION
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	PRINT 'ERROR - ROLLING BACK';
	SELECT ERROR_MESSAGE() AS ErrorMessage;  
	-- ROLLBACK
	IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION;  
END CATCH;	