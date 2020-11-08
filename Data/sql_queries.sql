--create database company_delta_flow
--go
USE [company_delta_flow]
GO

CREATE TABLE delta_user (
	Id BIGINT IDENTITY(1, 1) PRIMARY KEY
	,FullName NVARCHAR(100) NOT NULL
	,Email NVARCHAR(300) NOT NULL
	,EmailVerified boolean NOT NULL default(0)
	,Gender NVARCHAR(10) NOT NULL default('Male')
	,Password NVARCHAR(MAX) NOT NULL
	,CreatedOn DATETIME DEFAULT(getdate())
	)

INSERT INTO delta_user (
	FullName
	,Email
	,Gender
	,Password
	)
VALUES (
	'Bijay Kumar Yadav'
	,'b@b.com'
	,'Male'
	,1234
	)

--alter PROCEDURE usp_signInUserByUserNameAndPassword (
--	@Email NVARCHAR(300)
--	,@Password NVARCHAR(MAX)
--	)
--AS
--BEGIN
--	SELECT fullname, email, EmailVerified, gender
--	FROM delta_user
--	WHERE email = @email and EmailVerified = 1
--		AND password = @password
--END

--CREATE PROCEDURE usp_signUpUser (
--	@FullName NVARCHAR(100)
--	,@Email NVARCHAR(300)
--	,@Gender NVARCHAR(10)
--	,@Password NVARCHAR(MAX)
--	)
--AS
--BEGIN
--	INSERT INTO delta_user (
--	FullName
--	,Email
--, EmailVerified
--	,Gender
--	,Password
--	)
--VALUES (
--	@FullName
--	,@Email
--	,@Gender
--  , 1
--	,@Password
--	)
--END

--CREATE PROCEDURE usp_userExistsByEmail (
--	@Email NVARCHAR(300)
--	)
--AS
--BEGIN
--	SELECT fullname
--	FROM delta_user
--	WHERE email = @email
--END

SELECT *
FROM delta_user;

exec usp_signInUserByUserNameAndPassword 'b@b.com', '1234'

--drop table delta_user;
