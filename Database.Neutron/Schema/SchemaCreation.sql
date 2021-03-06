﻿USE [MASTER]
GO
-- DROP DB
IF EXISTS(SELECT * FROM sys.databases WHERE NAME='PRACTISEV1')
	DROP DATABASE [PRACTISEV1]
GO

-- CREATE DB
CREATE DATABASE [PRACTISEV1]
GO

-- ALTER DB
ALTER DATABASE [PRACTISEV1] SET MULTI_USER
GO

-- USE DB
USE [PRACTISEV1]
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO


-- BEGIN TRANSACTION
BEGIN TRANSACTION SCHEMACREATION
GO

CREATE SCHEMA [Utility];
GO

-- Reference master 
CREATE TABLE [Utility].[Reference](
	[Ci] [int] IDENTITY(1,1) NOT NULL,
	[Id] uniqueidentifier NOT NULL,
	[Name] nvarchar(256) NOT NULL,
	[Description] nvarchar(512) NULL,
	[CountryCode] nvarchar(2) NOT NULL,
	[Archived] DATETIME  NULL,
	[CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ChangedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
	CONSTRAINT [PK_Utility_Reference] PRIMARY KEY NONCLUSTERED([Id] ASC),
	CONSTRAINT [CI_Utility_Reference] UNIQUE CLUSTERED ([Ci] ASC),
	CONSTRAINT [UQ_Utility_Reference] UNIQUE NONCLUSTERED([Name], [CountryCode])
);
GO

-- Reference child
CREATE TABLE [Utility].[ReferenceItem](
	[Ci] [int] IDENTITY(1,1) NOT NULL,
	[Id] uniqueidentifier NOT NULL,
	[ReferenceId] uniqueidentifier NOT NULL,
	[Code] nvarchar(256) NOT NULL,
	[Description] nvarchar(512) NOT NULL,
	[Archived] DATETIME  NULL,
	[CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ChangedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
	[Udf1] nvarchar(MAX),
	[Udf2] nvarchar(MAX),
	[Udf3] nvarchar(MAX),
	CONSTRAINT [PK_Utility_ReferenceItem] PRIMARY KEY NONCLUSTERED([Id] ASC),
	CONSTRAINT [CI_Utility_ReferenceItem] UNIQUE CLUSTERED ([Ci] ASC),
	CONSTRAINT [UQ_Utility_ReferenceItem] UNIQUE ([ReferenceId], [Code]),
	CONSTRAINT [FK_Utility_ReferenceItem_Reference] FOREIGN KEY ([ReferenceId]) REFERENCES [Utility].[Reference] ([Id]) ON DELETE CASCADE
);
GO

-- Address
CREATE TABLE [Utility].[Address](
	[Ci] [int] IDENTITY(1,1) NOT NULL,
	[Id] uniqueidentifier NOT NULL,
	[Address1] [nvarchar](512) NOT NULL,
	[Address2] [nvarchar](256),
	[City] [nvarchar](256) NOT NULL,
	[County] [nvarchar](256) NULL,
	[State] [nvarchar](256) NOT NULL,
	[Zip] [nvarchar](256) NOT NULL,
	[CountryId] uniqueidentifier NOT NULL,
	[AddressTypeId] uniqueidentifier NOT NULL,
	[Latitude] DECIMAL(9,6) NULL,
	[Longitude] DECIMAL(9,6) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ChangedOn] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[Udf1]  [nvarchar](512) NULL,
	[Udf2]  [nvarchar](512) NULL,
	[Udf3]  [nvarchar](512) NULL,
	CONSTRAINT [PK_Utility_Address] PRIMARY KEY NONCLUSTERED([Id] ASC),
	CONSTRAINT [CI_Utility_Address] UNIQUE CLUSTERED ([Ci] ASC),
	CONSTRAINT [FK_UtilityAddress_ReferenceItem_Country] FOREIGN KEY ([CountryId]) REFERENCES [Utility].[ReferenceItem] ([Id]),
	CONSTRAINT [FK_Utility_Address_ReferenceItem_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Utility].[ReferenceItem] ([Id])
);
GO

-- Administration
CREATE SCHEMA [Administration];
GO
	
-- Users
CREATE TABLE [Administration].[Users]
(
	[Ci] INT IDENTITY(1,1) NOT NULL, 
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[UserName] NVARCHAR(256) NOT NULL,
	[Email] NVARCHAR(256) NOT NULL,
	[EmailConfirmed] BIT NOT NULL,
	[PasswordHash] NVARCHAR(512) NOT NULL,
	[SecurityStamp] NVARCHAR(256) NOT NULL,
	[PhoneNumber] NVARCHAR(256) NULL,
	[PhoneNumberConfirmed] BIT NOT NULL,
	[MobileNumber] NVARCHAR(256) NULL,
	[MobileNumberConfirmed] BIT NOT NULL,
	[NationalId] NVARCHAR(512) NULL,
	[NationalIdVerificationDateUtc] DATETIME NULL,
	[TwoFactorEnabled] BIT NOT NULL,
	[LockoutEndDateUtc] DATETIME NULL,
	[LockoutEnabled] BIT NOT NULL,
	[AccessFailedCount] INT NOT NULL,
	[CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
	[ChangedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
	[DeletedOn] DATETIME NULL, 
	[DeactivatedDate] DATETIME NULL, 
	[Udf1]  [nvarchar](512) NULL,
	[Udf2]  [nvarchar](512) NULL,
	[Udf3]  [nvarchar](512) NULL,
	CONSTRAINT [PK_Administration_Users] PRIMARY KEY NONCLUSTERED([Id] ASC),
	CONSTRAINT [CI_Administration_Users] UNIQUE CLUSTERED (Ci ASC),
	CONSTRAINT [UQ_Administration_Users_UserName] UNIQUE (UserName)
);
CREATE INDEX [IX_Administration_Users_Id] ON [Administration].[Users]([Id]);
CREATE INDEX [IX_Administration_Users_UserName] ON [Administration].[Users]([UserName]);
GO

-- Check Function
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[Administration].[FuncCheckValidClaimValue]') AND xtype IN (N'FN', N'IF', N'TF'))
    DROP FUNCTION [Administration].[FuncCheckValidClaimValue]
GO
CREATE FUNCTION [Administration].[FuncCheckValidClaimValue](@claimValue varchar(256))
RETURNS INT
AS BEGIN
    IF(@claimValue = 'ADMIN' OR 
		@claimValue = 'POWERUSER' OR
		@claimValue = 'USER' OR
		@claimValue = 'GUEST')
		RETURN 0;
	RETURN 1;
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[Administration].[FuncCheckValidClaimType]') AND xtype IN (N'FN', N'IF', N'TF'))
    DROP FUNCTION [Administration].[FuncCheckValidClaimType]
GO
CREATE FUNCTION [Administration].[FuncCheckValidClaimType](@claimType varchar(512))
RETURNS INT
AS BEGIN
    IF(@claimType = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role')
		RETURN 0;
	RETURN 1;
END
GO

-- UserClaim
CREATE TABLE [Administration].[UserClaim](
	[Ci] INT IDENTITY(1,1) NOT NULL,
	[UserId] uniqueidentifier NOT NULL,
	[ClaimType] NVARCHAR(256) NOT NULL,
	[ClaimValue] NVARCHAR(256) NOT NULL,
	CONSTRAINT [PK_Administration_UserClaim] PRIMARY KEY NONCLUSTERED ([UserId], [ClaimType], [ClaimValue]),
	CONSTRAINT [CI_Administration_UserClaim] UNIQUE CLUSTERED (Ci ASC),
	CONSTRAINT [FK_Administration_UserClaim_Users] FOREIGN KEY ([UserId]) REFERENCES [Administration].[Users]([Id]) ON DELETE CASCADE,
	CONSTRAINT [CH_Administration_ClaimType] CHECK ([Administration].[FuncCheckValidClaimType]([ClaimType]) = 0),  
	CONSTRAINT [CH_Administration_ClaimValue] CHECK ([Administration].[FuncCheckValidClaimValue]([ClaimValue]) = 0)
);
GO
CREATE INDEX [IX_Administration_UserClaim_UserId] ON [Administration].[UserClaim]([UserId]);
GO	
CREATE INDEX [IX_Administration_UserClaim_UserId_ClaimValue] ON [Administration].[UserClaim]([UserId], [ClaimValue]);
GO	

-- UserProfile
CREATE TABLE [Administration].[UserProfile](
	[Ci] INT IDENTITY(1,1) NOT NULL,
	[UserId] uniqueidentifier NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[UserTypeId] UNIQUEIDENTIFIER NOT NULL, -- Customer, Employee etc...
	[Title] [nvarchar](256) NULL,
	[Suffix] [nvarchar](256) NULL,
	[Prefix] [nvarchar](256) NULL,
	[PrefferedName] NVARCHAR (256) NULL,
	[Dob] [date] NULL,
	[GenderId] [uniqueidentifier] NOT NULL,
	[CountryId] uniqueidentifier NOT NULL, 
	[Organization] [nvarchar](256) NULL,
	[Department] [nvarchar](256) NULL,
	[PictureUrl] Nvarchar(1024) NULL,
	[Udf1]  [nvarchar](512) NULL,
	[Udf2]  [nvarchar](512) NULL,
	[Udf3]  [nvarchar](512) NULL,
	CONSTRAINT [PK_Administration_UserProfile] PRIMARY KEY NONCLUSTERED ([UserId]),
	CONSTRAINT [CI_Administration_UserProfile] UNIQUE CLUSTERED (Ci ASC),
	CONSTRAINT [FK_Administration_UserProfile_ReferenceItem_UserType] FOREIGN KEY ([UserTypeId]) REFERENCES [Utility].[ReferenceItem] ([Id]),
	CONSTRAINT [FK_Administration_UserProfile_ReferenceItem_Gender] FOREIGN KEY ([GenderId]) REFERENCES [Utility].[ReferenceItem] ([Id]),
	CONSTRAINT [FK_Administration_UserProfile_ReferenceItem_Country] FOREIGN KEY ([CountryId]) REFERENCES [Utility].[ReferenceItem] ([Id]),
	CONSTRAINT [FK_Administration_UserProfile_Users] FOREIGN KEY ([UserId]) REFERENCES [Administration].[Users]([Id]) ON DELETE CASCADE
); 
GO
CREATE INDEX [IX_Administration_UserProfile_UserId] ON [Administration].[UserProfile]([UserId]);
GO

-- UserAddress
CREATE TABLE [Administration].[UserAddress](
	[Ci] [int] IDENTITY(1,1) NOT NULL,
	[Id] uniqueidentifier NOT NULL,
	[UserId] uniqueidentifier NOT NULL,
	[AddressId] uniqueidentifier NOT NULL,
	[Preffered] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Administration_UserAddress] PRIMARY KEY NONCLUSTERED([Id] ASC),
	CONSTRAINT [CI_Administration_UserAddress] UNIQUE CLUSTERED ([Ci] ASC),
	CONSTRAINT [FK_Administration_UserAddress_User] FOREIGN KEY ([UserId]) REFERENCES [Administration].[Users] ([Id]),
	CONSTRAINT [FK_Administration_UserAddress_Utility_Address] FOREIGN KEY ([AddressId]) REFERENCES [Utility].[Address] ([Id]),
	CONSTRAINT [UQ_Administration_UserAddress_User_Address] UNIQUE ([UserId], [AddressId]),
);
GO

-- COMMITT TRANSACTION
COMMIT TRANSACTION SCHEMACREATION;
GO