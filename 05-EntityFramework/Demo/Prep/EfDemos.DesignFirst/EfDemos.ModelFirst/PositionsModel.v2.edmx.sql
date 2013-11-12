
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/12/2013 06:21:39
-- Generated from EDMX file: C:\Users\Bret\Documents\GitHub\MVAWebServicesAndAzure\05-EntityFramework\Demo\Prep\EfDemos.DesignFirst\EfDemos.ModelFirst\PositionsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PositionsModelFirst];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CruisePosition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Positions] DROP CONSTRAINT [FK_CruisePosition];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Cruises]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cruises];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [PositionId] int IDENTITY(1,1) NOT NULL,
    [ReportedAt] datetime  NOT NULL,
    [Latitude] real  NOT NULL,
    [Longitude] real  NOT NULL,
    [CruiseId] int  NOT NULL
);
GO

-- Creating table 'Cruises'
CREATE TABLE [dbo].[Cruises] (
    [CruiseId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PositionId] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([PositionId] ASC);
GO

-- Creating primary key on [CruiseId] in table 'Cruises'
ALTER TABLE [dbo].[Cruises]
ADD CONSTRAINT [PK_Cruises]
    PRIMARY KEY CLUSTERED ([CruiseId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CruiseId] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [FK_CruisePosition]
    FOREIGN KEY ([CruiseId])
    REFERENCES [dbo].[Cruises]
        ([CruiseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CruisePosition'
CREATE INDEX [IX_FK_CruisePosition]
ON [dbo].[Positions]
    ([CruiseId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------