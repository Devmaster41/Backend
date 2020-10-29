
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/25/2020 09:12:37
-- Generated from EDMX file: D:\งานอาจารย์นัด\Member\ASP_MEMBER\ASP_MEMBER\Entitiy\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [memberDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccessTokens_Member]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccessTokens] DROP CONSTRAINT [FK_AccessTokens_Member];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AccessTokens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccessTokens];
GO
IF OBJECT_ID(N'[dbo].[Member]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Member];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Members'
CREATE TABLE [dbo].[Members] (
    [id] int IDENTITY(1,1) NOT NULL,
    [firstname] nvarchar(50)  NOT NULL,
    [lastname] nvarchar(50)  NOT NULL,
    [email] nvarchar(80)  NOT NULL,
    [password] nvarchar(64)  NOT NULL,
    [position] nvarchar(50)  NULL,
    [image] varbinary(max)  NULL,
    [role] smallint  NOT NULL,
    [created] datetime  NOT NULL,
    [updated] datetime  NOT NULL,
    [image_type] nvarchar(50)  NULL
);
GO

-- Creating table 'AccessTokens'
CREATE TABLE [dbo].[AccessTokens] (
    [id] int IDENTITY(1,1) NOT NULL,
    [token] nvarchar(50)  NOT NULL,
    [exprise] datetime  NOT NULL,
    [memberID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [PK_Members]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'AccessTokens'
ALTER TABLE [dbo].[AccessTokens]
ADD CONSTRAINT [PK_AccessTokens]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [memberID] in table 'AccessTokens'
ALTER TABLE [dbo].[AccessTokens]
ADD CONSTRAINT [FK_AccessTokens_Member]
    FOREIGN KEY ([memberID])
    REFERENCES [dbo].[Members]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessTokens_Member'
CREATE INDEX [IX_FK_AccessTokens_Member]
ON [dbo].[AccessTokens]
    ([memberID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------