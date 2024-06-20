USE [master]
GO

CREATE DATABASE [seclogin]
GO

USE [seclogin]
GO

CREATE TABLE tbl_UserCredentials (
	pk_UserCredential uniqueidentifier CONSTRAINT pk_UC PRIMARY KEY (pk_UserCredential),
	Email varchar(60) CONSTRAINT ak_Email UNIQUE,
	Passwort varchar(256),
	Vorname varchar(60),
	Nachname varchar(60)
)