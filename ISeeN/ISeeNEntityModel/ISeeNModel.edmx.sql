
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2014 13:38:39
-- Generated from EDMX file: C:\Users\SebastianDybdal\Documents\GitHub\BNDN\ISeeN\ISeeNEntityModel\ISeeNModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RentIt02Test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RentalUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalSet] DROP CONSTRAINT [FK_RentalUser];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaRental]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RentalSet] DROP CONSTRAINT [FK_MediaRental];
GO
IF OBJECT_ID(N'[dbo].[FK_Movie_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Movie] DROP CONSTRAINT [FK_Movie_inherits_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_Music_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Music] DROP CONSTRAINT [FK_Music_inherits_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_Picture_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MediaSet_Picture] DROP CONSTRAINT [FK_Picture_inherits_Media];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[MediaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet];
GO
IF OBJECT_ID(N'[dbo].[RentalSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RentalSet];
GO
IF OBJECT_ID(N'[dbo].[StatisticSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatisticSet];
GO
IF OBJECT_ID(N'[dbo].[ReminderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReminderSet];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_Movie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_Movie];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_Music]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_Music];
GO
IF OBJECT_ID(N'[dbo].[MediaSet_Picture]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaSet_Picture];
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

-- Creating table 'RentalSet'
CREATE TABLE [dbo].[RentalSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [MediaId] int  NOT NULL
);
GO

-- Creating table 'StatisticSet'
CREATE TABLE [dbo].[StatisticSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ReminderSet'
CREATE TABLE [dbo].[ReminderSet] (
    [Id] int IDENTITY(1,1) NOT NULL
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

-- Creating primary key on [Id] in table 'MediaSet'
ALTER TABLE [dbo].[MediaSet]
ADD CONSTRAINT [PK_MediaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RentalSet'
ALTER TABLE [dbo].[RentalSet]
ADD CONSTRAINT [PK_RentalSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StatisticSet'
ALTER TABLE [dbo].[StatisticSet]
ADD CONSTRAINT [PK_StatisticSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReminderSet'
ALTER TABLE [dbo].[ReminderSet]
ADD CONSTRAINT [PK_ReminderSet]
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

-- Creating foreign key on [UserId] in table 'RentalSet'
ALTER TABLE [dbo].[RentalSet]
ADD CONSTRAINT [FK_RentalUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentalUser'
CREATE INDEX [IX_FK_RentalUser]
ON [dbo].[RentalSet]
    ([UserId]);
GO

-- Creating foreign key on [MediaId] in table 'RentalSet'
ALTER TABLE [dbo].[RentalSet]
ADD CONSTRAINT [FK_MediaRental]
    FOREIGN KEY ([MediaId])
    REFERENCES [dbo].[MediaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaRental'
CREATE INDEX [IX_FK_MediaRental]
ON [dbo].[RentalSet]
    ([MediaId]);
GO

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