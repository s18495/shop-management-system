CREATE TABLE [dbo].[shops] (
    [shop_id]     INT           IDENTITY (1, 1) NOT NULL,
    [shop_name]   VARCHAR (255) NOT NULL,
    [address]     VARCHAR (255) NOT NULL,
    [phone]       VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([shop_id] ASC)
);


INSERT INTO [dbo].[shops] ([shop_name], [address], [phone])
VALUES ('Kandy', 'Kandy', 0711111111);




ALTER TABLE [dbo].[orders]
ADD [shop_id] INT NOT NULL;

ALTER TABLE [dbo].[orders]
ADD [shop_id] INT DEFAULT 1;

UPDATE [dbo].[orders]
SET [shop_id] = 1
WHERE [shop_id] IS NULL;

ALTER TABLE [dbo].[orders]
ALTER COLUMN [shop_id] INT NOT NULL;


ALTER TABLE [dbo].[orders]
ADD CONSTRAINT FK_Orders_Shops FOREIGN KEY (shop_id) REFERENCES [dbo].[shops] (shop_id);





ALTER TABLE [dbo].[products]
ADD [shop_id] INT DEFAULT 1;


UPDATE [dbo].[products]
SET [shop_id] = 1
WHERE [shop_id] IS NULL;


ALTER TABLE [dbo].[products]
ALTER COLUMN [shop_id] INT NOT NULL;



ALTER TABLE [dbo].[products]
ADD CONSTRAINT FK_Products_Shops FOREIGN KEY (shop_id) REFERENCES [dbo].[shops] (shop_id);


