CREATE TABLE [dbo].[FieldMetadata] (
    [Id]         NVARCHAR (25)  NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [DataType]   NVARCHAR (50)  NOT NULL,
    [Length]     INT            CONSTRAINT [DF_FieldMetadata_Length] DEFAULT ((0)) NOT NULL,
    [LookupName] NVARCHAR (50)  NULL,
    [Items]      NVARCHAR (MAX) NULL,
    [ObjectId]   NVARCHAR (25)  NOT NULL,
    CONSTRAINT [PK_FieldMetadata] PRIMARY KEY CLUSTERED ([Id] ASC)
);

