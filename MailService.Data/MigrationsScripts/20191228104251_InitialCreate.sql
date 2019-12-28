IF OBJECT_ID(N'[ms].[__EFMigrationsHistory]') IS NULL
BEGIN
    IF SCHEMA_ID(N'ms') IS NULL EXEC(N'CREATE SCHEMA [ms];');
    CREATE TABLE [ms].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'ms') IS NULL EXEC(N'CREATE SCHEMA [ms];');

GO

CREATE TABLE [ms].[Mails] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [UpdatedDate] datetimeoffset NULL,
    [From] nvarchar(max) NULL,
    [To] nvarchar(max) NULL,
    [Subject] nvarchar(max) NULL,
    [Body] nvarchar(max) NULL,
    [IsHtml] bit NOT NULL,
    [Priority] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [SentDate] datetimeoffset NULL,
    CONSTRAINT [PK_Mails] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ms].[MailAttachments] (
    [Id] uniqueidentifier NOT NULL,
    [MailId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Content] nvarchar(max) NULL,
    [mediaType] nvarchar(max) NULL,
    CONSTRAINT [PK_MailAttachments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MailAttachments_Mails_MailId] FOREIGN KEY ([MailId]) REFERENCES [ms].[Mails] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_MailAttachments_MailId] ON [ms].[MailAttachments] ([MailId]);

GO

INSERT INTO [ms].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191228104251_InitialCreate', N'2.2.6-servicing-10079');

GO

