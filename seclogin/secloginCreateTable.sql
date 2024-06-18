USE [master]
GO

CREATE DATABASE [seclogin]
GO

USE [seclogin]
GO

CREATE TABLE tbl_UserCredentials (
	pk_UserCredential uniqueidentifier,
	Email varchar(60),
	Passwort varchar(256),
	Vorname varchar(60),
	Nachname varchar(60)
)