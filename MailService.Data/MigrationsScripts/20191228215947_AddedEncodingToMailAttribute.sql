EXEC sp_rename N'[ms].[MailAttachments].[ContentType]', N'MediaType', N'COLUMN';

GO

ALTER TABLE [ms].[MailAttachments] ADD [Encoding] nvarchar(max) NULL;

GO

INSERT INTO [ms].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191228215947_AddedEncodingToMailAttribute', N'2.2.6-servicing-10079');

GO

