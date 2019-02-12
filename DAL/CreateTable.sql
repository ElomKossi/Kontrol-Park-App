
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/24/2017 19:12:27
-- Generated from EDMX file: C:\Users\Black Hole\documents\visual studio 2017\Projects\Kontrol Park App\DAL\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Kontrol Park];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Action_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Action] DROP CONSTRAINT [FK_Action_Action];
GO
IF OBJECT_ID(N'[dbo].[FK_Affectation_Conducteur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Affectation] DROP CONSTRAINT [FK_Affectation_Conducteur];
GO
IF OBJECT_ID(N'[dbo].[FK_Affectation_Mission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Affectation] DROP CONSTRAINT [FK_Affectation_Mission];
GO
IF OBJECT_ID(N'[dbo].[FK_Affectation_Vehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Affectation] DROP CONSTRAINT [FK_Affectation_Vehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_AvoirDroit_Droit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AvoirDroit] DROP CONSTRAINT [FK_AvoirDroit_Droit];
GO
IF OBJECT_ID(N'[dbo].[FK_AvoirDroit_Profil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AvoirDroit] DROP CONSTRAINT [FK_AvoirDroit_Profil];
GO
IF OBJECT_ID(N'[dbo].[FK_ConducteurPermis_CategoriePermis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConducteurPermis] DROP CONSTRAINT [FK_ConducteurPermis_CategoriePermis];
GO
IF OBJECT_ID(N'[dbo].[FK_ConducteurPermis_Conducteur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConducteurPermis] DROP CONSTRAINT [FK_ConducteurPermis_Conducteur];
GO
IF OBJECT_ID(N'[dbo].[FK_Connexion_Utilisateur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Connexion] DROP CONSTRAINT [FK_Connexion_Utilisateur];
GO
IF OBJECT_ID(N'[dbo].[FK_Consommer_Carburant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SortieCarburant] DROP CONSTRAINT [FK_Consommer_Carburant];
GO
IF OBJECT_ID(N'[dbo].[FK_Consommer_Vehiculee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SortieCarburant] DROP CONSTRAINT [FK_Consommer_Vehiculee];
GO
IF OBJECT_ID(N'[dbo].[FK_ConsommerHuile_Huile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SortieHuile] DROP CONSTRAINT [FK_ConsommerHuile_Huile];
GO
IF OBJECT_ID(N'[dbo].[FK_ConsommerHuile_Vehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SortieHuile] DROP CONSTRAINT [FK_ConsommerHuile_Vehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_DetailAssurance_Assurance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailAssurance] DROP CONSTRAINT [FK_DetailAssurance_Assurance];
GO
IF OBJECT_ID(N'[dbo].[FK_DetailAssurance_Vehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailAssurance] DROP CONSTRAINT [FK_DetailAssurance_Vehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_DetatilIntervention_Intervention]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetatilIntervention] DROP CONSTRAINT [FK_DetatilIntervention_Intervention];
GO
IF OBJECT_ID(N'[dbo].[FK_DetatilIntervention_Piece]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetatilIntervention] DROP CONSTRAINT [FK_DetatilIntervention_Piece];
GO
IF OBJECT_ID(N'[dbo].[FK_HistoriqueAspect_AspectVehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HistoriqueAspect] DROP CONSTRAINT [FK_HistoriqueAspect_AspectVehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_HistoriqueAspect_Vehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HistoriqueAspect] DROP CONSTRAINT [FK_HistoriqueAspect_Vehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_TypeIntervention]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Intervention] DROP CONSTRAINT [FK_Intervention_TypeIntervention];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_Vehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Intervention] DROP CONSTRAINT [FK_Intervention_Vehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_Mission_CategorieMission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mission] DROP CONSTRAINT [FK_Mission_CategorieMission];
GO
IF OBJECT_ID(N'[dbo].[FK_Mission_TypeMission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mission] DROP CONSTRAINT [FK_Mission_TypeMission];
GO
IF OBJECT_ID(N'[dbo].[FK_SortieCarburant_Technicien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SortieCarburant] DROP CONSTRAINT [FK_SortieCarburant_Technicien];
GO
IF OBJECT_ID(N'[dbo].[FK_SortieHuile_Technicien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SortieHuile] DROP CONSTRAINT [FK_SortieHuile_Technicien];
GO
IF OBJECT_ID(N'[dbo].[FK_TechCarburant_Carburant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntreeCarburant] DROP CONSTRAINT [FK_TechCarburant_Carburant];
GO
IF OBJECT_ID(N'[dbo].[FK_TechCarburant_Technicien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntreeCarburant] DROP CONSTRAINT [FK_TechCarburant_Technicien];
GO
IF OBJECT_ID(N'[dbo].[FK_TechHuile_Huile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntreeHuile] DROP CONSTRAINT [FK_TechHuile_Huile];
GO
IF OBJECT_ID(N'[dbo].[FK_TechHuile_Technicien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntreeHuile] DROP CONSTRAINT [FK_TechHuile_Technicien];
GO
IF OBJECT_ID(N'[dbo].[FK_TechIntervention_Intervention]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MecaIntervention] DROP CONSTRAINT [FK_TechIntervention_Intervention];
GO
IF OBJECT_ID(N'[dbo].[FK_TechIntervention_Technicien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MecaIntervention] DROP CONSTRAINT [FK_TechIntervention_Technicien];
GO
IF OBJECT_ID(N'[dbo].[FK_Utilisateur_Profil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateur] DROP CONSTRAINT [FK_Utilisateur_Profil];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehicule_Fournisseur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicule] DROP CONSTRAINT [FK_Vehicule_Fournisseur];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehicule_Marque]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicule] DROP CONSTRAINT [FK_Vehicule_Marque];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehicule_Type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicule] DROP CONSTRAINT [FK_Vehicule_Type];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehicule_TypeCarburant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicule] DROP CONSTRAINT [FK_Vehicule_TypeCarburant];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__RefactorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__RefactorLog];
GO
IF OBJECT_ID(N'[dbo].[Action]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Action];
GO
IF OBJECT_ID(N'[dbo].[Affectation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Affectation];
GO
IF OBJECT_ID(N'[dbo].[AspectVehicule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspectVehicule];
GO
IF OBJECT_ID(N'[dbo].[Assurance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assurance];
GO
IF OBJECT_ID(N'[dbo].[AvoirDroit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AvoirDroit];
GO
IF OBJECT_ID(N'[dbo].[Carburant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Carburant];
GO
IF OBJECT_ID(N'[dbo].[CategorieMission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategorieMission];
GO
IF OBJECT_ID(N'[dbo].[CategoriePermis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoriePermis];
GO
IF OBJECT_ID(N'[dbo].[Conducteur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conducteur];
GO
IF OBJECT_ID(N'[dbo].[ConducteurPermis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConducteurPermis];
GO
IF OBJECT_ID(N'[dbo].[Connexion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Connexion];
GO
IF OBJECT_ID(N'[dbo].[DetailAssurance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetailAssurance];
GO
IF OBJECT_ID(N'[dbo].[DetatilIntervention]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetatilIntervention];
GO
IF OBJECT_ID(N'[dbo].[Droit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Droit];
GO
IF OBJECT_ID(N'[dbo].[EntreeCarburant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntreeCarburant];
GO
IF OBJECT_ID(N'[dbo].[EntreeHuile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntreeHuile];
GO
IF OBJECT_ID(N'[dbo].[Fournisseur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fournisseur];
GO
IF OBJECT_ID(N'[dbo].[HistoriqueAspect]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HistoriqueAspect];
GO
IF OBJECT_ID(N'[dbo].[Huile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Huile];
GO
IF OBJECT_ID(N'[dbo].[Intervention]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Intervention];
GO
IF OBJECT_ID(N'[dbo].[MarqueVehicule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MarqueVehicule];
GO
IF OBJECT_ID(N'[dbo].[MecaIntervention]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MecaIntervention];
GO
IF OBJECT_ID(N'[dbo].[Mecanicien]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mecanicien];
GO
IF OBJECT_ID(N'[dbo].[Mission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mission];
GO
IF OBJECT_ID(N'[dbo].[Piece]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Piece];
GO
IF OBJECT_ID(N'[dbo].[Profil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profil];
GO
IF OBJECT_ID(N'[dbo].[SortieCarburant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SortieCarburant];
GO
IF OBJECT_ID(N'[dbo].[SortieHuile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SortieHuile];
GO
IF OBJECT_ID(N'[dbo].[TypeCarburant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeCarburant];
GO
IF OBJECT_ID(N'[dbo].[TypeIntervention]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeIntervention];
GO
IF OBJECT_ID(N'[dbo].[TypeMission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeMission];
GO
IF OBJECT_ID(N'[dbo].[TypeVehicule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeVehicule];
GO
IF OBJECT_ID(N'[dbo].[Utilisateur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateur];
GO
IF OBJECT_ID(N'[dbo].[Vehicule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehicule];
GO
IF OBJECT_ID(N'[KontrolParkModelStoreContainer].[ConducteurDispoView]', 'U') IS NOT NULL
    DROP TABLE [KontrolParkModelStoreContainer].[ConducteurDispoView];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__RefactorLog'
CREATE TABLE [dbo].[C__RefactorLog] (
    [OperationKey] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Action'
CREATE TABLE [dbo].[Action] (
    [IdAction] int IDENTITY(1,1) NOT NULL,
    [LibelleAction] varchar(200)  NOT NULL,
    [HeureAction] datetime  NOT NULL,
    [IdConnexion] int  NOT NULL
);
GO

-- Creating table 'Affectation'
CREATE TABLE [dbo].[Affectation] (
    [IdAffectation] int IDENTITY(1,1) NOT NULL,
    [IdMission] int  NOT NULL,
    [IdVehicule] int  NOT NULL,
    [IdConducteur] int  NOT NULL,
    [DateAffectation] datetime  NOT NULL
);
GO

-- Creating table 'AspectVehicule'
CREATE TABLE [dbo].[AspectVehicule] (
    [IdAspect] int IDENTITY(1,1) NOT NULL,
    [LibelleAspect] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Assurance'
CREATE TABLE [dbo].[Assurance] (
    [IdAssurance] int IDENTITY(1,1) NOT NULL,
    [LibelleAssurance] varchar(50)  NOT NULL,
    [AdresseAssurance] varchar(50)  NOT NULL,
    [EmailAssurance] varchar(50)  NOT NULL,
    [TelAssurance] varchar(15)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'AvoirDroit'
CREATE TABLE [dbo].[AvoirDroit] (
    [IdDroitProfil] int IDENTITY(1,1) NOT NULL,
    [IdDroit] int  NOT NULL,
    [IdProfil] int  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Carburant'
CREATE TABLE [dbo].[Carburant] (
    [IdCarburant] int IDENTITY(1,1) NOT NULL,
    [LibelleCarburant] varchar(50)  NOT NULL,
    [VolumeCarburant] float  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'CategorieMission'
CREATE TABLE [dbo].[CategorieMission] (
    [IdCategorieMission] int IDENTITY(1,1) NOT NULL,
    [LibelleCategorieMission] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'CategoriePermis'
CREATE TABLE [dbo].[CategoriePermis] (
    [IdCategoriePermis] int IDENTITY(1,1) NOT NULL,
    [LibelleCategoriePermis] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Conducteur'
CREATE TABLE [dbo].[Conducteur] (
    [IdConducteur] int IDENTITY(1,1) NOT NULL,
    [NumCNIConducteur] varchar(50)  NOT NULL,
    [NomConducteur] varchar(50)  NOT NULL,
    [PrenomConducteur] varchar(50)  NOT NULL,
    [DateNaissanceConducteur] datetime  NOT NULL,
    [LieuxNaissanceConducteur] varchar(50)  NOT NULL,
    [SexeConducteur] varchar(50)  NOT NULL,
    [AdresseConducteur] varchar(50)  NOT NULL,
    [TelConducteur] varchar(15)  NOT NULL,
    [EmailConducteur] varchar(50)  NOT NULL,
    [DateEmbaucheConducteur] datetime  NOT NULL,
    [EnActivite] bit  NOT NULL,
    [EnMission] bit  NOT NULL,
    [DateDesactivation] datetime  NOT NULL
);
GO

-- Creating table 'ConducteurPermis'
CREATE TABLE [dbo].[ConducteurPermis] (
    [IdConducteurPermis] int IDENTITY(1,1) NOT NULL,
    [IdCategoriePermis] int  NOT NULL,
    [IdConducteur] int  NOT NULL,
    [NumPermis] varchar(50)  NOT NULL,
    [DateExpire] datetime  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Connexion'
CREATE TABLE [dbo].[Connexion] (
    [IdConnexion] int IDENTITY(1,1) NOT NULL,
    [DebutConnexion] datetime  NOT NULL,
    [FinConnexion] datetime  NOT NULL,
    [SystemeOS] varchar(50)  NOT NULL,
    [Navigateur] varchar(50)  NOT NULL,
    [AdresseIP] varchar(50)  NOT NULL,
    [AdresseMac] varchar(50)  NOT NULL,
    [Machine] varchar(50)  NOT NULL,
    [IdUser] int  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'DetailAssurance'
CREATE TABLE [dbo].[DetailAssurance] (
    [IdDetailAssurance] int  NOT NULL,
    [IdAssurance] int  NOT NULL,
    [IdVehicule] int  NOT NULL,
    [DataDebutCouverture] datetime  NOT NULL,
    [DateFinCouverture] datetime  NOT NULL
);
GO

-- Creating table 'DetatilIntervention'
CREATE TABLE [dbo].[DetatilIntervention] (
    [IdInterventionPiece] int IDENTITY(1,1) NOT NULL,
    [IdIntervention] int  NOT NULL,
    [IdPiece] int  NOT NULL
);
GO

-- Creating table 'Droit'
CREATE TABLE [dbo].[Droit] (
    [IdDroit] int IDENTITY(1,1) NOT NULL,
    [LibelleDroit] varchar(50)  NOT NULL,
    [ActifDroit] bit  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'EntreeCarburant'
CREATE TABLE [dbo].[EntreeCarburant] (
    [IdEntreeCarburant] int IDENTITY(1,1) NOT NULL,
    [DateEntreeCarburant] datetime  NOT NULL,
    [QuantiteEntreeCarb] float  NOT NULL,
    [IdMecanicien] int  NOT NULL,
    [IdCarburant] int  NOT NULL
);
GO

-- Creating table 'EntreeHuile'
CREATE TABLE [dbo].[EntreeHuile] (
    [IdEntreeHuile] int IDENTITY(1,1) NOT NULL,
    [DateEntreeHuile] datetime  NOT NULL,
    [QuantiteEntreeHuile] float  NOT NULL,
    [IdMecanicien] int  NOT NULL,
    [IdHuile] int  NOT NULL
);
GO

-- Creating table 'Fournisseur'
CREATE TABLE [dbo].[Fournisseur] (
    [IdFournisseur] int IDENTITY(1,1) NOT NULL,
    [LibelleFournisseur] varchar(50)  NOT NULL,
    [AdresseFournisseur] varchar(50)  NOT NULL,
    [EmailFournisseur] varchar(50)  NOT NULL,
    [TelFournisseur] varchar(15)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'HistoriqueAspect'
CREATE TABLE [dbo].[HistoriqueAspect] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdVehicule] int  NOT NULL,
    [IdAspect] int  NOT NULL,
    [DateMAJ] datetime  NOT NULL
);
GO

-- Creating table 'Huile'
CREATE TABLE [dbo].[Huile] (
    [IdHuile] int IDENTITY(1,1) NOT NULL,
    [LibelleHuile] varchar(50)  NOT NULL,
    [VolumeHuile] float  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Intervention'
CREATE TABLE [dbo].[Intervention] (
    [IdIntervention] int IDENTITY(1,1) NOT NULL,
    [LibelleIdIntervention] varchar(50)  NOT NULL,
    [DescriptionIdIntervention] varchar(max)  NOT NULL,
    [DateDebut] datetime  NOT NULL,
    [DateFin] datetime  NOT NULL,
    [IdTypeIntervention] int  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL,
    [IdVehicule] int  NOT NULL
);
GO

-- Creating table 'MarqueVehicule'
CREATE TABLE [dbo].[MarqueVehicule] (
    [IdMarque] int IDENTITY(1,1) NOT NULL,
    [LibelleMarque] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'MecaIntervention'
CREATE TABLE [dbo].[MecaIntervention] (
    [IdTechIntervention] int IDENTITY(1,1) NOT NULL,
    [DateIntervention] datetime  NOT NULL,
    [IdMecanicien] int  NOT NULL,
    [IdIntervention] int  NOT NULL
);
GO

-- Creating table 'Mecanicien'
CREATE TABLE [dbo].[Mecanicien] (
    [IdMecanicien] int IDENTITY(1,1) NOT NULL,
    [NumCNIMecanicien] varchar(50)  NOT NULL,
    [NomMecanicien] varchar(50)  NOT NULL,
    [PrenomMecanicien] varchar(50)  NOT NULL,
    [DateNaissanceMecanicien] datetime  NOT NULL,
    [LieuxNaissanceMecanicien] varchar(50)  NOT NULL,
    [SexeMecanicien] varchar(50)  NOT NULL,
    [AdresseMecanicien] varchar(50)  NOT NULL,
    [TelMecanicien] varchar(15)  NOT NULL,
    [EmailMecanicien] varchar(50)  NOT NULL,
    [DateEmbaucheMecanicien] datetime  NOT NULL,
    [EnActivite] bit  NOT NULL,
    [DateDesactivation] datetime  NOT NULL
);
GO

-- Creating table 'Mission'
CREATE TABLE [dbo].[Mission] (
    [IdMission] int IDENTITY(1,1) NOT NULL,
    [LibelleMission] varchar(50)  NOT NULL,
    [DateDebutMission] datetime  NOT NULL,
    [DateFinMission] datetime  NOT NULL,
    [IdCategorieMission] int  NOT NULL,
    [IdTypeMission] int  NOT NULL,
    [Cloturer] bit  NOT NULL,
    [DateCloturer] datetime  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Piece'
CREATE TABLE [dbo].[Piece] (
    [IdPiece] int IDENTITY(1,1) NOT NULL,
    [LibellePiece] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Profil'
CREATE TABLE [dbo].[Profil] (
    [IdProfil] int IDENTITY(1,1) NOT NULL,
    [LibelleProfil] varchar(50)  NOT NULL,
    [ActifProfil] bit  NOT NULL,
    [Actif] bit  NOT NULL,
    [DateDesactivation] datetime  NOT NULL
);
GO

-- Creating table 'SortieCarburant'
CREATE TABLE [dbo].[SortieCarburant] (
    [IdSortieCarburant] int IDENTITY(1,1) NOT NULL,
    [VolumeSortieCarburant] float  NOT NULL,
    [DateSortieCarburant] datetime  NOT NULL,
    [IdVehicule] int  NOT NULL,
    [IdCarburant] int  NOT NULL,
    [IdMecanicien] int  NOT NULL
);
GO

-- Creating table 'SortieHuile'
CREATE TABLE [dbo].[SortieHuile] (
    [IdSortieHuile] int IDENTITY(1,1) NOT NULL,
    [VolumeSortieHuile] float  NOT NULL,
    [DateSortieHuile] datetime  NOT NULL,
    [IdVehicule] int  NOT NULL,
    [IdHuile] int  NOT NULL,
    [IdMecanicien] int  NOT NULL
);
GO

-- Creating table 'TypeCarburant'
CREATE TABLE [dbo].[TypeCarburant] (
    [IdTypeCarb] int IDENTITY(1,1) NOT NULL,
    [LibelleTypeCarb] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'TypeIntervention'
CREATE TABLE [dbo].[TypeIntervention] (
    [IdTypeIntervention] int IDENTITY(1,1) NOT NULL,
    [LibelleTypeIntervention] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL
);
GO

-- Creating table 'TypeMission'
CREATE TABLE [dbo].[TypeMission] (
    [IdTypeMission] int IDENTITY(1,1) NOT NULL,
    [LibelleTypeMission] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'TypeVehicule'
CREATE TABLE [dbo].[TypeVehicule] (
    [IdType] int IDENTITY(1,1) NOT NULL,
    [LibelleType] varchar(50)  NOT NULL,
    [Supprimer] bit  NOT NULL,
    [DateSupprimer] datetime  NOT NULL
);
GO

-- Creating table 'Utilisateur'
CREATE TABLE [dbo].[Utilisateur] (
    [IdUser] int IDENTITY(1,1) NOT NULL,
    [NumCNIUser] varchar(50)  NOT NULL,
    [NomUser] varchar(50)  NOT NULL,
    [PrenomUser] varchar(50)  NOT NULL,
    [DateNaissanceUser] datetime  NOT NULL,
    [LieuxNaissanceUser] varchar(50)  NOT NULL,
    [SexeUser] varchar(50)  NOT NULL,
    [AdresseUser] varchar(50)  NOT NULL,
    [TelUser] varchar(15)  NOT NULL,
    [EmailUser] varchar(50)  NOT NULL,
    [DateEmbaucheUser] datetime  NOT NULL,
    [DateCreationCompteUser] datetime  NOT NULL,
    [Identifiant] varchar(50)  NOT NULL,
    [MotDePasse] varchar(200)  NOT NULL,
    [DateModificationPass] datetime  NOT NULL,
    [Tentative] int  NOT NULL,
    [EstConnecte] bit  NOT NULL,
    [EnActivite] bit  NOT NULL,
    [DateDesactivation] datetime  NOT NULL,
    [IdProfil] int  NOT NULL
);
GO

-- Creating table 'Vehicule'
CREATE TABLE [dbo].[Vehicule] (
    [IdVehicule] int IDENTITY(1,1) NOT NULL,
    [NomVehicule] varchar(50)  NOT NULL,
    [NumChassis] varchar(50)  NOT NULL,
    [Immatriculation] varchar(50)  NOT NULL,
    [DateAcquisition] datetime  NOT NULL,
    [DateExpireGarantie] datetime  NOT NULL,
    [IdFournisseur] int  NOT NULL,
    [IdTypeCarb] int  NOT NULL,
    [IdMarque] int  NOT NULL,
    [IdType] int  NOT NULL,
    [EnActivite] bit  NOT NULL,
    [EnMission] bit  NOT NULL,
    [DateDesactivation] datetime  NOT NULL
);
GO

-- Creating table 'ConducteurDispoView'
CREATE TABLE [dbo].[ConducteurDispoView] (
    [IdAffectation] int IDENTITY(1,1) NOT NULL,
    [IdMission] int  NOT NULL,
    [IdVehicule] int  NOT NULL,
    [IdConducteur] int  NOT NULL,
    [DateAffectation] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [OperationKey] in table 'C__RefactorLog'
ALTER TABLE [dbo].[C__RefactorLog]
ADD CONSTRAINT [PK_C__RefactorLog]
    PRIMARY KEY CLUSTERED ([OperationKey] ASC);
GO

-- Creating primary key on [IdAction] in table 'Action'
ALTER TABLE [dbo].[Action]
ADD CONSTRAINT [PK_Action]
    PRIMARY KEY CLUSTERED ([IdAction] ASC);
GO

-- Creating primary key on [IdAffectation] in table 'Affectation'
ALTER TABLE [dbo].[Affectation]
ADD CONSTRAINT [PK_Affectation]
    PRIMARY KEY CLUSTERED ([IdAffectation] ASC);
GO

-- Creating primary key on [IdAspect] in table 'AspectVehicule'
ALTER TABLE [dbo].[AspectVehicule]
ADD CONSTRAINT [PK_AspectVehicule]
    PRIMARY KEY CLUSTERED ([IdAspect] ASC);
GO

-- Creating primary key on [IdAssurance] in table 'Assurance'
ALTER TABLE [dbo].[Assurance]
ADD CONSTRAINT [PK_Assurance]
    PRIMARY KEY CLUSTERED ([IdAssurance] ASC);
GO

-- Creating primary key on [IdDroitProfil] in table 'AvoirDroit'
ALTER TABLE [dbo].[AvoirDroit]
ADD CONSTRAINT [PK_AvoirDroit]
    PRIMARY KEY CLUSTERED ([IdDroitProfil] ASC);
GO

-- Creating primary key on [IdCarburant] in table 'Carburant'
ALTER TABLE [dbo].[Carburant]
ADD CONSTRAINT [PK_Carburant]
    PRIMARY KEY CLUSTERED ([IdCarburant] ASC);
GO

-- Creating primary key on [IdCategorieMission] in table 'CategorieMission'
ALTER TABLE [dbo].[CategorieMission]
ADD CONSTRAINT [PK_CategorieMission]
    PRIMARY KEY CLUSTERED ([IdCategorieMission] ASC);
GO

-- Creating primary key on [IdCategoriePermis] in table 'CategoriePermis'
ALTER TABLE [dbo].[CategoriePermis]
ADD CONSTRAINT [PK_CategoriePermis]
    PRIMARY KEY CLUSTERED ([IdCategoriePermis] ASC);
GO

-- Creating primary key on [IdConducteur] in table 'Conducteur'
ALTER TABLE [dbo].[Conducteur]
ADD CONSTRAINT [PK_Conducteur]
    PRIMARY KEY CLUSTERED ([IdConducteur] ASC);
GO

-- Creating primary key on [IdConducteurPermis] in table 'ConducteurPermis'
ALTER TABLE [dbo].[ConducteurPermis]
ADD CONSTRAINT [PK_ConducteurPermis]
    PRIMARY KEY CLUSTERED ([IdConducteurPermis] ASC);
GO

-- Creating primary key on [IdConnexion] in table 'Connexion'
ALTER TABLE [dbo].[Connexion]
ADD CONSTRAINT [PK_Connexion]
    PRIMARY KEY CLUSTERED ([IdConnexion] ASC);
GO

-- Creating primary key on [IdDetailAssurance] in table 'DetailAssurance'
ALTER TABLE [dbo].[DetailAssurance]
ADD CONSTRAINT [PK_DetailAssurance]
    PRIMARY KEY CLUSTERED ([IdDetailAssurance] ASC);
GO

-- Creating primary key on [IdInterventionPiece] in table 'DetatilIntervention'
ALTER TABLE [dbo].[DetatilIntervention]
ADD CONSTRAINT [PK_DetatilIntervention]
    PRIMARY KEY CLUSTERED ([IdInterventionPiece] ASC);
GO

-- Creating primary key on [IdDroit] in table 'Droit'
ALTER TABLE [dbo].[Droit]
ADD CONSTRAINT [PK_Droit]
    PRIMARY KEY CLUSTERED ([IdDroit] ASC);
GO

-- Creating primary key on [IdEntreeCarburant] in table 'EntreeCarburant'
ALTER TABLE [dbo].[EntreeCarburant]
ADD CONSTRAINT [PK_EntreeCarburant]
    PRIMARY KEY CLUSTERED ([IdEntreeCarburant] ASC);
GO

-- Creating primary key on [IdEntreeHuile] in table 'EntreeHuile'
ALTER TABLE [dbo].[EntreeHuile]
ADD CONSTRAINT [PK_EntreeHuile]
    PRIMARY KEY CLUSTERED ([IdEntreeHuile] ASC);
GO

-- Creating primary key on [IdFournisseur] in table 'Fournisseur'
ALTER TABLE [dbo].[Fournisseur]
ADD CONSTRAINT [PK_Fournisseur]
    PRIMARY KEY CLUSTERED ([IdFournisseur] ASC);
GO

-- Creating primary key on [Id] in table 'HistoriqueAspect'
ALTER TABLE [dbo].[HistoriqueAspect]
ADD CONSTRAINT [PK_HistoriqueAspect]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdHuile] in table 'Huile'
ALTER TABLE [dbo].[Huile]
ADD CONSTRAINT [PK_Huile]
    PRIMARY KEY CLUSTERED ([IdHuile] ASC);
GO

-- Creating primary key on [IdIntervention] in table 'Intervention'
ALTER TABLE [dbo].[Intervention]
ADD CONSTRAINT [PK_Intervention]
    PRIMARY KEY CLUSTERED ([IdIntervention] ASC);
GO

-- Creating primary key on [IdMarque] in table 'MarqueVehicule'
ALTER TABLE [dbo].[MarqueVehicule]
ADD CONSTRAINT [PK_MarqueVehicule]
    PRIMARY KEY CLUSTERED ([IdMarque] ASC);
GO

-- Creating primary key on [IdTechIntervention] in table 'MecaIntervention'
ALTER TABLE [dbo].[MecaIntervention]
ADD CONSTRAINT [PK_MecaIntervention]
    PRIMARY KEY CLUSTERED ([IdTechIntervention] ASC);
GO

-- Creating primary key on [IdMecanicien] in table 'Mecanicien'
ALTER TABLE [dbo].[Mecanicien]
ADD CONSTRAINT [PK_Mecanicien]
    PRIMARY KEY CLUSTERED ([IdMecanicien] ASC);
GO

-- Creating primary key on [IdMission] in table 'Mission'
ALTER TABLE [dbo].[Mission]
ADD CONSTRAINT [PK_Mission]
    PRIMARY KEY CLUSTERED ([IdMission] ASC);
GO

-- Creating primary key on [IdPiece] in table 'Piece'
ALTER TABLE [dbo].[Piece]
ADD CONSTRAINT [PK_Piece]
    PRIMARY KEY CLUSTERED ([IdPiece] ASC);
GO

-- Creating primary key on [IdProfil] in table 'Profil'
ALTER TABLE [dbo].[Profil]
ADD CONSTRAINT [PK_Profil]
    PRIMARY KEY CLUSTERED ([IdProfil] ASC);
GO

-- Creating primary key on [IdSortieCarburant] in table 'SortieCarburant'
ALTER TABLE [dbo].[SortieCarburant]
ADD CONSTRAINT [PK_SortieCarburant]
    PRIMARY KEY CLUSTERED ([IdSortieCarburant] ASC);
GO

-- Creating primary key on [IdSortieHuile] in table 'SortieHuile'
ALTER TABLE [dbo].[SortieHuile]
ADD CONSTRAINT [PK_SortieHuile]
    PRIMARY KEY CLUSTERED ([IdSortieHuile] ASC);
GO

-- Creating primary key on [IdTypeCarb] in table 'TypeCarburant'
ALTER TABLE [dbo].[TypeCarburant]
ADD CONSTRAINT [PK_TypeCarburant]
    PRIMARY KEY CLUSTERED ([IdTypeCarb] ASC);
GO

-- Creating primary key on [IdTypeIntervention] in table 'TypeIntervention'
ALTER TABLE [dbo].[TypeIntervention]
ADD CONSTRAINT [PK_TypeIntervention]
    PRIMARY KEY CLUSTERED ([IdTypeIntervention] ASC);
GO

-- Creating primary key on [IdTypeMission] in table 'TypeMission'
ALTER TABLE [dbo].[TypeMission]
ADD CONSTRAINT [PK_TypeMission]
    PRIMARY KEY CLUSTERED ([IdTypeMission] ASC);
GO

-- Creating primary key on [IdType] in table 'TypeVehicule'
ALTER TABLE [dbo].[TypeVehicule]
ADD CONSTRAINT [PK_TypeVehicule]
    PRIMARY KEY CLUSTERED ([IdType] ASC);
GO

-- Creating primary key on [IdUser] in table 'Utilisateur'
ALTER TABLE [dbo].[Utilisateur]
ADD CONSTRAINT [PK_Utilisateur]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- Creating primary key on [IdVehicule] in table 'Vehicule'
ALTER TABLE [dbo].[Vehicule]
ADD CONSTRAINT [PK_Vehicule]
    PRIMARY KEY CLUSTERED ([IdVehicule] ASC);
GO

-- Creating primary key on [IdAffectation], [IdMission], [IdVehicule], [IdConducteur], [DateAffectation] in table 'ConducteurDispoView'
ALTER TABLE [dbo].[ConducteurDispoView]
ADD CONSTRAINT [PK_ConducteurDispoView]
    PRIMARY KEY CLUSTERED ([IdAffectation], [IdMission], [IdVehicule], [IdConducteur], [DateAffectation] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdConnexion] in table 'Action'
ALTER TABLE [dbo].[Action]
ADD CONSTRAINT [FK_Action_Action]
    FOREIGN KEY ([IdConnexion])
    REFERENCES [dbo].[Connexion]
        ([IdConnexion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Action_Action'
CREATE INDEX [IX_FK_Action_Action]
ON [dbo].[Action]
    ([IdConnexion]);
GO

-- Creating foreign key on [IdConducteur] in table 'Affectation'
ALTER TABLE [dbo].[Affectation]
ADD CONSTRAINT [FK_Affectation_Conducteur]
    FOREIGN KEY ([IdConducteur])
    REFERENCES [dbo].[Conducteur]
        ([IdConducteur])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Affectation_Conducteur'
CREATE INDEX [IX_FK_Affectation_Conducteur]
ON [dbo].[Affectation]
    ([IdConducteur]);
GO

-- Creating foreign key on [IdMission] in table 'Affectation'
ALTER TABLE [dbo].[Affectation]
ADD CONSTRAINT [FK_Affectation_Mission]
    FOREIGN KEY ([IdMission])
    REFERENCES [dbo].[Mission]
        ([IdMission])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Affectation_Mission'
CREATE INDEX [IX_FK_Affectation_Mission]
ON [dbo].[Affectation]
    ([IdMission]);
GO

-- Creating foreign key on [IdVehicule] in table 'Affectation'
ALTER TABLE [dbo].[Affectation]
ADD CONSTRAINT [FK_Affectation_Vehicule]
    FOREIGN KEY ([IdVehicule])
    REFERENCES [dbo].[Vehicule]
        ([IdVehicule])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Affectation_Vehicule'
CREATE INDEX [IX_FK_Affectation_Vehicule]
ON [dbo].[Affectation]
    ([IdVehicule]);
GO

-- Creating foreign key on [IdAspect] in table 'HistoriqueAspect'
ALTER TABLE [dbo].[HistoriqueAspect]
ADD CONSTRAINT [FK_HistoriqueAspect_AspectVehicule]
    FOREIGN KEY ([IdAspect])
    REFERENCES [dbo].[AspectVehicule]
        ([IdAspect])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HistoriqueAspect_AspectVehicule'
CREATE INDEX [IX_FK_HistoriqueAspect_AspectVehicule]
ON [dbo].[HistoriqueAspect]
    ([IdAspect]);
GO

-- Creating foreign key on [IdAssurance] in table 'DetailAssurance'
ALTER TABLE [dbo].[DetailAssurance]
ADD CONSTRAINT [FK_DetailAssurance_Assurance]
    FOREIGN KEY ([IdAssurance])
    REFERENCES [dbo].[Assurance]
        ([IdAssurance])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailAssurance_Assurance'
CREATE INDEX [IX_FK_DetailAssurance_Assurance]
ON [dbo].[DetailAssurance]
    ([IdAssurance]);
GO

-- Creating foreign key on [IdDroit] in table 'AvoirDroit'
ALTER TABLE [dbo].[AvoirDroit]
ADD CONSTRAINT [FK_AvoirDroit_Droit]
    FOREIGN KEY ([IdDroit])
    REFERENCES [dbo].[Droit]
        ([IdDroit])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AvoirDroit_Droit'
CREATE INDEX [IX_FK_AvoirDroit_Droit]
ON [dbo].[AvoirDroit]
    ([IdDroit]);
GO

-- Creating foreign key on [IdProfil] in table 'AvoirDroit'
ALTER TABLE [dbo].[AvoirDroit]
ADD CONSTRAINT [FK_AvoirDroit_Profil]
    FOREIGN KEY ([IdProfil])
    REFERENCES [dbo].[Profil]
        ([IdProfil])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AvoirDroit_Profil'
CREATE INDEX [IX_FK_AvoirDroit_Profil]
ON [dbo].[AvoirDroit]
    ([IdProfil]);
GO

-- Creating foreign key on [IdCarburant] in table 'SortieCarburant'
ALTER TABLE [dbo].[SortieCarburant]
ADD CONSTRAINT [FK_Consommer_Carburant]
    FOREIGN KEY ([IdCarburant])
    REFERENCES [dbo].[Carburant]
        ([IdCarburant])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Consommer_Carburant'
CREATE INDEX [IX_FK_Consommer_Carburant]
ON [dbo].[SortieCarburant]
    ([IdCarburant]);
GO

-- Creating foreign key on [IdCarburant] in table 'EntreeCarburant'
ALTER TABLE [dbo].[EntreeCarburant]
ADD CONSTRAINT [FK_TechCarburant_Carburant]
    FOREIGN KEY ([IdCarburant])
    REFERENCES [dbo].[Carburant]
        ([IdCarburant])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TechCarburant_Carburant'
CREATE INDEX [IX_FK_TechCarburant_Carburant]
ON [dbo].[EntreeCarburant]
    ([IdCarburant]);
GO

-- Creating foreign key on [IdCategorieMission] in table 'Mission'
ALTER TABLE [dbo].[Mission]
ADD CONSTRAINT [FK_Mission_CategorieMission]
    FOREIGN KEY ([IdCategorieMission])
    REFERENCES [dbo].[CategorieMission]
        ([IdCategorieMission])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Mission_CategorieMission'
CREATE INDEX [IX_FK_Mission_CategorieMission]
ON [dbo].[Mission]
    ([IdCategorieMission]);
GO

-- Creating foreign key on [IdCategoriePermis] in table 'ConducteurPermis'
ALTER TABLE [dbo].[ConducteurPermis]
ADD CONSTRAINT [FK_ConducteurPermis_CategoriePermis]
    FOREIGN KEY ([IdCategoriePermis])
    REFERENCES [dbo].[CategoriePermis]
        ([IdCategoriePermis])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConducteurPermis_CategoriePermis'
CREATE INDEX [IX_FK_ConducteurPermis_CategoriePermis]
ON [dbo].[ConducteurPermis]
    ([IdCategoriePermis]);
GO

-- Creating foreign key on [IdConducteur] in table 'ConducteurPermis'
ALTER TABLE [dbo].[ConducteurPermis]
ADD CONSTRAINT [FK_ConducteurPermis_Conducteur]
    FOREIGN KEY ([IdConducteur])
    REFERENCES [dbo].[Conducteur]
        ([IdConducteur])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConducteurPermis_Conducteur'
CREATE INDEX [IX_FK_ConducteurPermis_Conducteur]
ON [dbo].[ConducteurPermis]
    ([IdConducteur]);
GO

-- Creating foreign key on [IdUser] in table 'Connexion'
ALTER TABLE [dbo].[Connexion]
ADD CONSTRAINT [FK_Connexion_Utilisateur]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Utilisateur]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Connexion_Utilisateur'
CREATE INDEX [IX_FK_Connexion_Utilisateur]
ON [dbo].[Connexion]
    ([IdUser]);
GO

-- Creating foreign key on [IdVehicule] in table 'DetailAssurance'
ALTER TABLE [dbo].[DetailAssurance]
ADD CONSTRAINT [FK_DetailAssurance_Vehicule]
    FOREIGN KEY ([IdVehicule])
    REFERENCES [dbo].[Vehicule]
        ([IdVehicule])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailAssurance_Vehicule'
CREATE INDEX [IX_FK_DetailAssurance_Vehicule]
ON [dbo].[DetailAssurance]
    ([IdVehicule]);
GO

-- Creating foreign key on [IdIntervention] in table 'DetatilIntervention'
ALTER TABLE [dbo].[DetatilIntervention]
ADD CONSTRAINT [FK_DetatilIntervention_Intervention]
    FOREIGN KEY ([IdIntervention])
    REFERENCES [dbo].[Intervention]
        ([IdIntervention])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetatilIntervention_Intervention'
CREATE INDEX [IX_FK_DetatilIntervention_Intervention]
ON [dbo].[DetatilIntervention]
    ([IdIntervention]);
GO

-- Creating foreign key on [IdPiece] in table 'DetatilIntervention'
ALTER TABLE [dbo].[DetatilIntervention]
ADD CONSTRAINT [FK_DetatilIntervention_Piece]
    FOREIGN KEY ([IdPiece])
    REFERENCES [dbo].[Piece]
        ([IdPiece])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetatilIntervention_Piece'
CREATE INDEX [IX_FK_DetatilIntervention_Piece]
ON [dbo].[DetatilIntervention]
    ([IdPiece]);
GO

-- Creating foreign key on [IdMecanicien] in table 'EntreeCarburant'
ALTER TABLE [dbo].[EntreeCarburant]
ADD CONSTRAINT [FK_TechCarburant_Technicien]
    FOREIGN KEY ([IdMecanicien])
    REFERENCES [dbo].[Mecanicien]
        ([IdMecanicien])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TechCarburant_Technicien'
CREATE INDEX [IX_FK_TechCarburant_Technicien]
ON [dbo].[EntreeCarburant]
    ([IdMecanicien]);
GO

-- Creating foreign key on [IdHuile] in table 'EntreeHuile'
ALTER TABLE [dbo].[EntreeHuile]
ADD CONSTRAINT [FK_TechHuile_Huile]
    FOREIGN KEY ([IdHuile])
    REFERENCES [dbo].[Huile]
        ([IdHuile])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TechHuile_Huile'
CREATE INDEX [IX_FK_TechHuile_Huile]
ON [dbo].[EntreeHuile]
    ([IdHuile]);
GO

-- Creating foreign key on [IdMecanicien] in table 'EntreeHuile'
ALTER TABLE [dbo].[EntreeHuile]
ADD CONSTRAINT [FK_TechHuile_Technicien]
    FOREIGN KEY ([IdMecanicien])
    REFERENCES [dbo].[Mecanicien]
        ([IdMecanicien])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TechHuile_Technicien'
CREATE INDEX [IX_FK_TechHuile_Technicien]
ON [dbo].[EntreeHuile]
    ([IdMecanicien]);
GO

-- Creating foreign key on [IdFournisseur] in table 'Vehicule'
ALTER TABLE [dbo].[Vehicule]
ADD CONSTRAINT [FK_Vehicule_Fournisseur]
    FOREIGN KEY ([IdFournisseur])
    REFERENCES [dbo].[Fournisseur]
        ([IdFournisseur])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehicule_Fournisseur'
CREATE INDEX [IX_FK_Vehicule_Fournisseur]
ON [dbo].[Vehicule]
    ([IdFournisseur]);
GO

-- Creating foreign key on [IdVehicule] in table 'HistoriqueAspect'
ALTER TABLE [dbo].[HistoriqueAspect]
ADD CONSTRAINT [FK_HistoriqueAspect_Vehicule]
    FOREIGN KEY ([IdVehicule])
    REFERENCES [dbo].[Vehicule]
        ([IdVehicule])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HistoriqueAspect_Vehicule'
CREATE INDEX [IX_FK_HistoriqueAspect_Vehicule]
ON [dbo].[HistoriqueAspect]
    ([IdVehicule]);
GO

-- Creating foreign key on [IdHuile] in table 'SortieHuile'
ALTER TABLE [dbo].[SortieHuile]
ADD CONSTRAINT [FK_ConsommerHuile_Huile]
    FOREIGN KEY ([IdHuile])
    REFERENCES [dbo].[Huile]
        ([IdHuile])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsommerHuile_Huile'
CREATE INDEX [IX_FK_ConsommerHuile_Huile]
ON [dbo].[SortieHuile]
    ([IdHuile]);
GO

-- Creating foreign key on [IdTypeIntervention] in table 'Intervention'
ALTER TABLE [dbo].[Intervention]
ADD CONSTRAINT [FK_Intervention_TypeIntervention]
    FOREIGN KEY ([IdTypeIntervention])
    REFERENCES [dbo].[TypeIntervention]
        ([IdTypeIntervention])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_TypeIntervention'
CREATE INDEX [IX_FK_Intervention_TypeIntervention]
ON [dbo].[Intervention]
    ([IdTypeIntervention]);
GO

-- Creating foreign key on [IdVehicule] in table 'Intervention'
ALTER TABLE [dbo].[Intervention]
ADD CONSTRAINT [FK_Intervention_Vehicule]
    FOREIGN KEY ([IdVehicule])
    REFERENCES [dbo].[Vehicule]
        ([IdVehicule])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_Vehicule'
CREATE INDEX [IX_FK_Intervention_Vehicule]
ON [dbo].[Intervention]
    ([IdVehicule]);
GO

-- Creating foreign key on [IdIntervention] in table 'MecaIntervention'
ALTER TABLE [dbo].[MecaIntervention]
ADD CONSTRAINT [FK_TechIntervention_Intervention]
    FOREIGN KEY ([IdIntervention])
    REFERENCES [dbo].[Intervention]
        ([IdIntervention])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TechIntervention_Intervention'
CREATE INDEX [IX_FK_TechIntervention_Intervention]
ON [dbo].[MecaIntervention]
    ([IdIntervention]);
GO

-- Creating foreign key on [IdMarque] in table 'Vehicule'
ALTER TABLE [dbo].[Vehicule]
ADD CONSTRAINT [FK_Vehicule_Marque]
    FOREIGN KEY ([IdMarque])
    REFERENCES [dbo].[MarqueVehicule]
        ([IdMarque])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehicule_Marque'
CREATE INDEX [IX_FK_Vehicule_Marque]
ON [dbo].[Vehicule]
    ([IdMarque]);
GO

-- Creating foreign key on [IdMecanicien] in table 'MecaIntervention'
ALTER TABLE [dbo].[MecaIntervention]
ADD CONSTRAINT [FK_TechIntervention_Technicien]
    FOREIGN KEY ([IdMecanicien])
    REFERENCES [dbo].[Mecanicien]
        ([IdMecanicien])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TechIntervention_Technicien'
CREATE INDEX [IX_FK_TechIntervention_Technicien]
ON [dbo].[MecaIntervention]
    ([IdMecanicien]);
GO

-- Creating foreign key on [IdMecanicien] in table 'SortieCarburant'
ALTER TABLE [dbo].[SortieCarburant]
ADD CONSTRAINT [FK_SortieCarburant_Technicien]
    FOREIGN KEY ([IdMecanicien])
    REFERENCES [dbo].[Mecanicien]
        ([IdMecanicien])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SortieCarburant_Technicien'
CREATE INDEX [IX_FK_SortieCarburant_Technicien]
ON [dbo].[SortieCarburant]
    ([IdMecanicien]);
GO

-- Creating foreign key on [IdMecanicien] in table 'SortieHuile'
ALTER TABLE [dbo].[SortieHuile]
ADD CONSTRAINT [FK_SortieHuile_Technicien]
    FOREIGN KEY ([IdMecanicien])
    REFERENCES [dbo].[Mecanicien]
        ([IdMecanicien])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SortieHuile_Technicien'
CREATE INDEX [IX_FK_SortieHuile_Technicien]
ON [dbo].[SortieHuile]
    ([IdMecanicien]);
GO

-- Creating foreign key on [IdTypeMission] in table 'Mission'
ALTER TABLE [dbo].[Mission]
ADD CONSTRAINT [FK_Mission_TypeMission]
    FOREIGN KEY ([IdTypeMission])
    REFERENCES [dbo].[TypeMission]
        ([IdTypeMission])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Mission_TypeMission'
CREATE INDEX [IX_FK_Mission_TypeMission]
ON [dbo].[Mission]
    ([IdTypeMission]);
GO

-- Creating foreign key on [IdProfil] in table 'Utilisateur'
ALTER TABLE [dbo].[Utilisateur]
ADD CONSTRAINT [FK_Utilisateur_Profil]
    FOREIGN KEY ([IdProfil])
    REFERENCES [dbo].[Profil]
        ([IdProfil])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Utilisateur_Profil'
CREATE INDEX [IX_FK_Utilisateur_Profil]
ON [dbo].[Utilisateur]
    ([IdProfil]);
GO

-- Creating foreign key on [IdVehicule] in table 'SortieCarburant'
ALTER TABLE [dbo].[SortieCarburant]
ADD CONSTRAINT [FK_Consommer_Vehiculee]
    FOREIGN KEY ([IdVehicule])
    REFERENCES [dbo].[Vehicule]
        ([IdVehicule])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Consommer_Vehiculee'
CREATE INDEX [IX_FK_Consommer_Vehiculee]
ON [dbo].[SortieCarburant]
    ([IdVehicule]);
GO

-- Creating foreign key on [IdVehicule] in table 'SortieHuile'
ALTER TABLE [dbo].[SortieHuile]
ADD CONSTRAINT [FK_ConsommerHuile_Vehicule]
    FOREIGN KEY ([IdVehicule])
    REFERENCES [dbo].[Vehicule]
        ([IdVehicule])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsommerHuile_Vehicule'
CREATE INDEX [IX_FK_ConsommerHuile_Vehicule]
ON [dbo].[SortieHuile]
    ([IdVehicule]);
GO

-- Creating foreign key on [IdTypeCarb] in table 'Vehicule'
ALTER TABLE [dbo].[Vehicule]
ADD CONSTRAINT [FK_Vehicule_TypeCarburant]
    FOREIGN KEY ([IdTypeCarb])
    REFERENCES [dbo].[TypeCarburant]
        ([IdTypeCarb])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehicule_TypeCarburant'
CREATE INDEX [IX_FK_Vehicule_TypeCarburant]
ON [dbo].[Vehicule]
    ([IdTypeCarb]);
GO

-- Creating foreign key on [IdType] in table 'Vehicule'
ALTER TABLE [dbo].[Vehicule]
ADD CONSTRAINT [FK_Vehicule_Type]
    FOREIGN KEY ([IdType])
    REFERENCES [dbo].[TypeVehicule]
        ([IdType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehicule_Type'
CREATE INDEX [IX_FK_Vehicule_Type]
ON [dbo].[Vehicule]
    ([IdType]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------