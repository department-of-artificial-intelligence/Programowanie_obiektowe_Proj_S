IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Address] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [Street] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);

CREATE TABLE [Restaurant] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [AddressId] int NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [OpeningHours] time NOT NULL,
    [ClosingHours] time NOT NULL,
    CONSTRAINT [PK_Restaurant] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Restaurant_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [MenuItem] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] real NOT NULL,
    [RestaurantId] int NULL,
    CONSTRAINT [PK_MenuItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MenuItem_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id])
);

CREATE TABLE [Person] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [AddressId] int NOT NULL,
    [Discriminator] nvarchar(8) NOT NULL,
    [RestaurantId1] int NULL,
    [RestaurantId] int NULL,
    [EmployeeType] int NULL,
    [Salary] int NULL,
    [HiredOn] datetime2 NULL,
    [FiredOn] datetime2 NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Person_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Person_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Person_Restaurant_RestaurantId1] FOREIGN KEY ([RestaurantId1]) REFERENCES [Restaurant] ([Id])
);

CREATE TABLE [Reservation] (
    [Id] int NOT NULL IDENTITY,
    [RestaurantId] int NOT NULL,
    [CustomerName] nvarchar(max) NOT NULL,
    [NumberOfPeople] int NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Date] datetime2 NOT NULL,
    [Time] time NOT NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reservation_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_MenuItem_RestaurantId] ON [MenuItem] ([RestaurantId]);

CREATE INDEX [IX_Person_AddressId] ON [Person] ([AddressId]);

CREATE INDEX [IX_Person_RestaurantId] ON [Person] ([RestaurantId]);

CREATE INDEX [IX_Person_RestaurantId1] ON [Person] ([RestaurantId1]);

CREATE INDEX [IX_Reservation_RestaurantId] ON [Reservation] ([RestaurantId]);

CREATE INDEX [IX_Restaurant_AddressId] ON [Restaurant] ([AddressId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251209201620_Initial', N'9.0.11');

COMMIT;
GO

