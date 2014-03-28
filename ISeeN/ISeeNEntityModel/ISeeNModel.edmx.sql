
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2014 02:36:45
-- Generated from EDMX file: C:\Users\SebastianDybdal\Documents\GitHub\BNDN\ISeeN\ISeeNEntityModel\ISeeNModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RentIt02];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[PotatoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PotatoSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [Bio] nvarchar(max)  NOT NULL,
    [IsAdmin] bit  NOT NULL
);
GO

-- Creating table 'PotatoSet'
CREATE TABLE [dbo].[PotatoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EncPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MediaSet'
CREATE TABLE [dbo].[MediaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [ReleaseDate] datetime  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MediaSet_Movie'
CREATE TABLE [dbo].[MediaSet_Movie] (
    [Director] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'MediaSet_Music'
CREATE TABLE [dbo].[MediaSet_Music] (
    [Artist] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'MediaSet_Picture'
CREATE TABLE [dbo].[MediaSet_Picture] (
    [Painter] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PotatoSet'
ALTER TABLE [dbo].[PotatoSet]
ADD CONSTRAINT [PK_PotatoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet'
ALTER TABLE [dbo].[MediaSet]
ADD CONSTRAINT [PK_MediaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_Movie'
ALTER TABLE [dbo].[MediaSet_Movie]
ADD CONSTRAINT [PK_MediaSet_Movie]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_Music'
ALTER TABLE [dbo].[MediaSet_Music]
ADD CONSTRAINT [PK_MediaSet_Music]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaSet_Picture'
ALTER TABLE [dbo].[MediaSet_Picture]
ADD CONSTRAINT [PK_MediaSet_Picture]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'MediaSet_Movie'
ALTER TABLE [dbo].[MediaSet_Movie]
ADD CONSTRAINT [FK_Movie_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'MediaSet_Music'
ALTER TABLE [dbo].[MediaSet_Music]
ADD CONSTRAINT [FK_Music_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'MediaSet_Picture'
ALTER TABLE [dbo].[MediaSet_Picture]
ADD CONSTRAINT [FK_Picture_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------