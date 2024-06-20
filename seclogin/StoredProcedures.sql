use [master]
GO

USE [seclogin]
GO


CREATE OR ALTER procedure usp_PostUserCredential
	@pk_UserCredential UNIQUEIDENTIFIER,
	@Email varchar(60),
	@Passwort varchar(256),
	@Vorname varchar(60),
	@Nachname varchar(60)
AS
BEGIN
	SET @Email = LTRIM(RTRIM(@Email));
	SET @Passwort = LTRIM(RTRIM(@Passwort));
	SET @Vorname = LTRIM(RTRIM(@Vorname));
	SET @Nachname = LTRIM(RTRIM(@Nachname));
	INSERT INTO tbl_UserCredentials(
		pk_UserCredential,
		Email,
		Passwort,
		Vorname,
		Nachname
	) VALUES (
		@pk_UserCredential,
		@Email,
		@Passwort,
		@Vorname,
		@Nachname
	);
	SELECT * FROM tbl_UserCredentials WHERE @Passwort = Passwort;
END
GO


CREATE OR ALTER PROCEDURE usp_PatchUserCredential
	@pk_UserCredential uniqueidentifier,
	@Email varchar(60) = NULL,
	@Passwort varchar(256) = NULL,
	@Vorname varchar(60) = NULL,
	@Nachname varchar(60) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE tbl_UserCredentials
	SET
		Email = ISNULL(@Email, Email),
		Passwort = ISNULL(@Passwort, Passwort),
		Vorname = ISNULL(@Vorname, Vorname),
		Nachname = ISNULL(@Nachname, Nachname)
	WHERE pk_UserCredential = @pk_UserCredential
	select * from tbl_UserCredentials where pk_UserCredential = @pk_UserCredential;
END
GO

CREATE OR ALTER PROCEDURE usp_DeleteUserCredential
	(@pk_UserCredential uniqueidentifier)
AS
BEGIN
	DELETE FROM tbl_UserCredentials WHERE pk_UserCredential = @pk_UserCredential
END
GO