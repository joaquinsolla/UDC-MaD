/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *      Configure within the CREATE DATABASE sql-sentence the path where 
 *      database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 
USE [shopping]

/* ********** Drop tables if they already exist *********** */

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment_Tag]') AND type in ('U'))
DROP TABLE [Comment_Tag]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') AND type in ('U'))
DROP TABLE [Tag]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[OrderLine]') AND type in ('U'))
DROP TABLE [OrderLine]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserOrder]') AND type in ('U'))
DROP TABLE [UserOrder]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[BankCard]') AND type in ('U'))
DROP TABLE [BankCard]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[BankCardType]') AND type in ('U'))
DROP TABLE [BankCardType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Film]') AND type in ('U'))
DROP TABLE [Film]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Music]') AND type in ('U'))
DROP TABLE [Music]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Book]') AND type in ('U'))
DROP TABLE [Book]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') AND type in ('U'))
DROP TABLE [Product]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO


PRINT N'[OK] Dropped old tables.'
GO

/* ********** Create tables *********** */

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(60) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	postalAddress varchar(100) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,
	admin bit NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)
PRINT N'[OK] Table UserProfile created.'
GO

CREATE TABLE Category (
	catName varchar(30) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (catName)
)
PRINT N'[OK] Table Category created.'
GO

CREATE TABLE Product (
	proId bigint IDENTITY(1,1) NOT NULL,
	proName varchar(30) NOT NULL,
	proPrice decimal(11,2) NOT NULL,
	proReleaseDate date NOT NULL,
	proStock bigint NOT NULL,
	proCatName varchar(30) NOT NULL,

	CONSTRAINT [PK_Product] PRIMARY KEY (proId),
	CONSTRAINT [UniqueKey_ProName] UNIQUE (proName),
	CONSTRAINT [FK_Product] FOREIGN KEY (proCatName)
		REFERENCES Category (catName) ON DELETE CASCADE
)
PRINT N'[OK] Table Product created.'
GO

CREATE TABLE Book (
	proId bigint NOT NULL,
	bookISBN varchar(17) NOT NULL,
	bookEditorial varchar(30) NOT NULL,
	bookEdition varchar(30) NOT NULL,
	bookPages bigint NOT NULL,
	bookReleaseDate date NOT NULL,

	CONSTRAINT [PK_Book_Product] PRIMARY KEY (proId),
	CONSTRAINT [UniqueKey_Book_BookISBN] UNIQUE (bookISBN),
	CONSTRAINT [FK_Book_ProId] FOREIGN KEY (proId)
		REFERENCES Product (proId) ON DELETE CASCADE,
)
PRINT N'[OK] Table Book created.'
GO

CREATE TABLE Music (
	proId bigint NOT NULL,
	musicArtist varchar(30) NOT NULL,
	musicAlbum varchar(30) NOT NULL,
	musicSongs bigint NOT NULL,
	musicDurationMins bigint NOT NULL,
	musicReleaseDate date NOT NULL,

	CONSTRAINT [PK_Music_Product] PRIMARY KEY (proId),
	CONSTRAINT [FK_Music_ProId] FOREIGN KEY (proId)
		REFERENCES Product (proId) ON DELETE CASCADE,
)
PRINT N'[OK] Table Music created.'
GO

CREATE TABLE Film (
	proId bigint NOT NULL,
	filmDirector varchar(30) NOT NULL,
	filmGenre varchar(30) NOT NULL,
	filmRating bigint NOT NULL,
	filmDurationMins bigint NOT NULL,
	filmReleaseDate date NOT NULL,

	CONSTRAINT [PK_Film_Product] PRIMARY KEY (proId),
	CONSTRAINT [FK_Film_ProId] FOREIGN KEY (proId)
		REFERENCES Product (proId) ON DELETE CASCADE,
)
PRINT N'[OK] Table Film created.'
GO

CREATE TABLE BankCardType (
	typeId bigint IDENTITY(1,1) NOT NULL,
	typeName varchar(30) NOT NULL,

	CONSTRAINT [PK_BankCardType] PRIMARY KEY (typeId),
	CONSTRAINT [UniqueKey_TypeName] UNIQUE (typeName)
)
PRINT N'[OK] Table BankCardType created.'
GO

CREATE TABLE BankCard (
	cardPAN bigint NOT NULL,
	cardTypeId bigint NOT NULL,
	cardCvv bigint NOT NULL,
	cardExpirationDate date NOT NULL,
	cardDefault bit NOT NULL,
	cardOwnerId bigint NOT NULL,

	CONSTRAINT [PK_BankCard] PRIMARY KEY (cardPAN),
	CONSTRAINT [FK_BankCard_CardTypeId] FOREIGN KEY (cardTypeId)
		REFERENCES BankCardType (typeId) ON DELETE CASCADE,
	CONSTRAINT [FK_BankCard_CardOwnerId] FOREIGN KEY (cardOwnerId)
		REFERENCES UserProfile (usrId) ON DELETE CASCADE
)
PRINT N'[OK] Table BankCard created.'
GO

CREATE TABLE UserOrder (
	orderId bigint IDENTITY(1,1) NOT NULL,
	orderDate date NOT NULL,
	orderBankCardPAN bigint NOT NULL,
	orderPostalAddress varchar(100) NOT NULL,
	orderValue decimal(11,2) NOT NULL,
	orderUserId bigint NOT NULL,
	orderDescription varchar(100) NOT NULL,

	CONSTRAINT [PK_UserOrder] PRIMARY KEY (orderId),
	CONSTRAINT [FK_UserOrder_OrderBankCardPAN] FOREIGN KEY (orderBankCardPAN)
		REFERENCES BankCard (cardPAN) ON DELETE CASCADE,
	CONSTRAINT [FK_UserOrder_OrderUserId] FOREIGN KEY (orderUserId)
		REFERENCES UserProfile (usrId) ON DELETE NO ACTION
)
PRINT N'[OK] Table UserOrder created.'
GO

CREATE TABLE OrderLine (
	lineId bigint IDENTITY(1,1) NOT NULL,
	lineOrderId bigint NOT NULL,
	lineProductId bigint NOT NULL,
	lineUnitaryPrice decimal (11,2) NOT NULL,
	lineQuantity bigint NOT NULL,

	CONSTRAINT [PK_OrderLine] PRIMARY KEY (lineId),
	CONSTRAINT [FK_OrderLine_LineOrderId] FOREIGN KEY (lineOrderId)
		REFERENCES UserOrder (orderId) ON DELETE CASCADE,
	CONSTRAINT [FK_OrderLine_LineProductId] FOREIGN KEY (lineProductId)
		REFERENCES Product (proId) ON DELETE CASCADE
)
PRINT N'[OK] Table OrderLine created.'
GO

CREATE TABLE Tag(
	tagName varchar(30) NOT NULL,
	tagShows bigint NOT NULL,

	CONSTRAINT [PK_Tag] PRIMARY KEY (tagName)
)
PRINT N'[OK] Table Tag created.'
GO

CREATE TABLE Comment(
	commentId bigint IDENTITY(1,1) NOT NULL,
	proId bigint NOT NULL,
	usrId bigint NOT NULL,
	commentText varchar(200) NOT NULL,
	commentDate date NOT NULL,

	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	CONSTRAINT [FK_Comment_ProductId] FOREIGN KEY (proId)
		REFERENCES Product (proId) ON DELETE CASCADE,
	CONSTRAINT [FK_Comment_UserId] FOREIGN KEY (usrId)
		REFERENCES UserProfile (usrId) ON DELETE CASCADE
)
PRINT N'[OK] Table Comment created.'
GO

CREATE TABLE Comment_Tag(
	commentId bigint NOT NULL,
	tagName varchar(30) NOT NULL,

	CONSTRAINT [PK_Comment_Tag] PRIMARY KEY (commentId, tagName),
		CONSTRAINT [FK_Comment_Tag_CommentId] FOREIGN KEY (commentId)
		REFERENCES Comment (commentId) ON DELETE CASCADE,
	CONSTRAINT [FK_Comment_Tag_TagName] FOREIGN KEY (tagName)
		REFERENCES Tag (tagName) ON DELETE CASCADE
)
PRINT N'[OK] Table Comment_Tag created.'
GO

/* ********** Generate indexes *********** */

CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_ProductIndexByProName]
ON [Product] ([proName] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_ProductIndexByProCatName]
ON [Product] ([proCatName] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_UserOrderIndexByOrderDate]
ON [UserOrder] ([orderDate] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_OrderLineIndexByLineId]
ON [OrderLine] ([lineId] DESC)
GO

CREATE NONCLUSTERED INDEX [IX_CommentByOrderDate]
ON [Comment] ([commentDate] DESC)
GO

PRINT N'[OK] Indexes created.
[INFO] Begin to insert data'
GO

/* ********** Insert data into tables *********** */

INSERT INTO UserProfile(loginName, enPassword, firstName, lastName, email, postalAddress, language, country, admin) VALUES ('admin', 'n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=', 'User', 'Admin', 'admin@admin.com', 'Admin postal address', 'en', 'US', 1)
INSERT INTO UserProfile(loginName, enPassword, firstName, lastName, email, postalAddress, language, country, admin) VALUES ('test', 'n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=', 'User', 'Tester', 'test@test.com', 'Testing postal address', 'es', 'ES', 0)

INSERT INTO Category(catName) VALUES ('Books')
INSERT INTO Category(catName) VALUES ('Music')
INSERT INTO Category(catName) VALUES ('Films')

INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Book 1', 9.95, '2022-10-08', 10, 'Books')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Book 2', 4.95, '2020-01-01', 1, 'Books')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Book 3', 15, '2022-10-08', 0, 'Books')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Music 1', 11.95, '2022-10-08', 100, 'Music')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Music 2', 2.95, '2020-01-01', 10000, 'Music')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Music 3', 5, '2022-10-08', 3, 'Music')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Film 1', 1.99, '2022-10-08', 50, 'Films')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Film 2', 1.99, '2020-01-01', 50, 'Films')
INSERT INTO Product(proName, proPrice, proReleaseDate, proStock, proCatName) VALUES ('Film 3', 2.99, '2022-10-08', 49, 'Films')

INSERT INTO Book(proId, bookISBN, bookEditorial, bookEdition, bookPages, bookReleaseDate) VALUES (1, '978-3-16-148410-1', 'Editorial x', 'Tapa dura', 254, '2001-08-05')
INSERT INTO Book(proId, bookISBN, bookEditorial, bookEdition, bookPages, bookReleaseDate) VALUES (2, '978-3-16-148410-2', 'Editorial x', 'Tapa blanda', 333, '2004-07-10')
INSERT INTO Book(proId, bookISBN, bookEditorial, bookEdition, bookPages, bookReleaseDate) VALUES (3, '978-3-16-148410-3', 'Editorial y', 'De bolsillo', 182, '2017-12-12')

INSERT INTO Music(proId, musicArtist, musicAlbum, musicSongs, musicDurationMins, musicReleaseDate) VALUES (4, 'Artist x', 'Album x', 11, 45, '2001-08-05')
INSERT INTO Music(proId, musicArtist, musicAlbum, musicSongs, musicDurationMins, musicReleaseDate) VALUES (5, 'Artist x', 'Album y', 8, 30, '2004-07-10')
INSERT INTO Music(proId, musicArtist, musicAlbum, musicSongs, musicDurationMins, musicReleaseDate) VALUES (6, 'Artist y', 'Album z', 15, 60, '2017-12-12')

INSERT INTO Film(proId, filmDirector, filmGenre, filmRating, filmDurationMins, filmReleaseDate) VALUES (7, 'Director x', 'Thriller', 5, 120, '2001-08-05')
INSERT INTO Film(proId, filmDirector, filmGenre, filmRating, filmDurationMins, filmReleaseDate) VALUES (8, 'Director y', 'Horror', 9, 115, '2004-07-10')
INSERT INTO Film(proId, filmDirector, filmGenre, filmRating, filmDurationMins, filmReleaseDate) VALUES (9, 'Director z', 'Comedy', 10, 105, '2017-12-12')

INSERT INTO BankCardType(typeName) VALUES ('Credit')
INSERT INTO BankCardType(typeName) VALUES ('Debit')
INSERT INTO BankCardType(typeName) VALUES ('PayPal')

INSERT INTO BankCard(cardPAN, cardTypeId, cardCVV, cardExpirationDate, cardDefault, cardOwnerId) VALUES (1234567890000001, 1, 123, '2024-01-01', 1, 1)
INSERT INTO BankCard(cardPAN, cardTypeId, cardCVV, cardExpirationDate, cardDefault, cardOwnerId) VALUES (1234567890000002, 2, 555, '2023-04-10', 0, 1)

INSERT INTO UserOrder(orderDate, orderBankCardPAN, orderPostalAddress, orderValue, orderUserId, orderDescription) VALUES ('2022-10-7', 1234567890000001, 'Order address x', 35, 1, 'My first order')
INSERT INTO UserOrder(orderDate, orderBankCardPAN, orderPostalAddress, orderValue, orderUserId, orderDescription) VALUES ('2022-10-8', 1234567890000001, 'Order address x', 5, 1, 'My second order')

INSERT INTO OrderLine(lineOrderId, lineProductId, lineUnitaryPrice, lineQuantity) VALUES (1, 3, 15, 2)
INSERT INTO OrderLine(lineOrderId, lineProductId, lineUnitaryPrice, lineQuantity) VALUES (1, 6, 5, 1)
INSERT INTO OrderLine(lineOrderId, lineProductId, lineUnitaryPrice, lineQuantity) VALUES (2, 6, 5, 1)

INSERT INTO Tag(tagName, tagShows) VALUES ('Ganga', 2)
INSERT INTO Tag(tagName, tagShows) VALUES ('Pésimo', 1)
INSERT INTO Tag(tagName, tagShows) VALUES ('Oferta', 1)

INSERT INTO Comment(proId, usrId, commentText, commentDate) VALUES (1, 1, 'Una ganga de libro', '2022-10-15')
INSERT INTO Comment(proId, usrId, commentText, commentDate) VALUES (1, 1, 'Una ganga de libro otra vez', '2022-10-16')
INSERT INTO Comment(proId, usrId, commentText, commentDate) VALUES (1, 1, 'Una ganga de libro ultima vez', '2022-10-17')
INSERT INTO Comment(proId, usrId, commentText, commentDate) VALUES (3, 1, 'Aprovechen la oferta por este libro', '2022-10-12')
INSERT INTO Comment(proId, usrId, commentText, commentDate) VALUES (7, 1, 'No vale la pena esta película', '2022-10-10')

INSERT INTO Comment_Tag(commentId, tagName) VALUES (1, 'Ganga')
INSERT INTO Comment_Tag(commentId, tagName) VALUES (1, 'Pésimo')
INSERT INTO Comment_Tag(commentId, tagName) VALUES (1, 'Oferta')
INSERT INTO Comment_Tag(commentId, tagName) VALUES (2, 'Ganga')

GO

PRINT N'[OK] Data inserted.'
GO