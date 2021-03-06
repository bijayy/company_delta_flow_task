USE [master]
GO
/****** Object:  Database [company_delta_flow]    Script Date: 11/10/2020 6:04:30 PM ******/
CREATE DATABASE [company_delta_flow]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'company_delta_flow', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\company_delta_flow.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'company_delta_flow_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\company_delta_flow_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [company_delta_flow].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [company_delta_flow] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [company_delta_flow] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [company_delta_flow] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [company_delta_flow] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [company_delta_flow] SET ARITHABORT OFF 
GO
ALTER DATABASE [company_delta_flow] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [company_delta_flow] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [company_delta_flow] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [company_delta_flow] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [company_delta_flow] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [company_delta_flow] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [company_delta_flow] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [company_delta_flow] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [company_delta_flow] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [company_delta_flow] SET  ENABLE_BROKER 
GO
ALTER DATABASE [company_delta_flow] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [company_delta_flow] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [company_delta_flow] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [company_delta_flow] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [company_delta_flow] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [company_delta_flow] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [company_delta_flow] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [company_delta_flow] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [company_delta_flow] SET  MULTI_USER 
GO
ALTER DATABASE [company_delta_flow] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [company_delta_flow] SET DB_CHAINING OFF 
GO
ALTER DATABASE [company_delta_flow] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [company_delta_flow] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [company_delta_flow] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [company_delta_flow] SET QUERY_STORE = OFF
GO
USE [company_delta_flow]
GO
ALTER DATABASE SCOPED CONFIGURATION SET ACCELERATED_PLAN_FORCING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ADAPTIVE_JOINS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET DEFERRED_COMPILATION_TV = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET GLOBAL_TEMPORARY_TABLE_AUTO_DROP = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET INTERLEAVED_EXECUTION_TVF = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ISOLATE_SECURITY_POLICY_CARDINALITY = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LAST_QUERY_PLAN_STATS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LIGHTWEIGHT_QUERY_PROFILING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ROW_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET TSQL_SCALAR_UDF_INLINING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET VERBOSE_TRUNCATION_WARNINGS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [company_delta_flow]
GO
/****** Object:  Table [dbo].[delta_answer]    Script Date: 11/10/2020 6:04:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delta_answer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsCorrect] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[QuestionId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delta_configure_user_quiz]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delta_configure_user_quiz](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsExpired] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[UserId] [bigint] NULL,
	[QuizId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delta_question]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delta_question](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[CreatedOn] [datetime] NULL,
	[QuizId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delta_quiz]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delta_quiz](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[PassPercentage] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delta_user]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delta_user](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](300) NOT NULL,
	[EmailVerified] [bit] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delta_user_quiz_result]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delta_user_quiz_result](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TotalQuestion] [int] NOT NULL,
	[TotalAttempted] [int] NOT NULL,
	[TotalCorrect] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UserId] [bigint] NULL,
	[QuizId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[delta_answer] ADD  DEFAULT ((0)) FOR [IsCorrect]
GO
ALTER TABLE [dbo].[delta_answer] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[delta_configure_user_quiz] ADD  DEFAULT ((0)) FOR [IsExpired]
GO
ALTER TABLE [dbo].[delta_configure_user_quiz] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[delta_question] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[delta_quiz] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[delta_quiz] ADD  DEFAULT ((40)) FOR [PassPercentage]
GO
ALTER TABLE [dbo].[delta_user] ADD  DEFAULT ((0)) FOR [EmailVerified]
GO
ALTER TABLE [dbo].[delta_user] ADD  DEFAULT ('Male') FOR [Gender]
GO
ALTER TABLE [dbo].[delta_user] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[delta_user_quiz_result] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[delta_answer]  WITH CHECK ADD FOREIGN KEY([QuestionId])
REFERENCES [dbo].[delta_question] ([Id])
GO
ALTER TABLE [dbo].[delta_configure_user_quiz]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[delta_quiz] ([Id])
GO
ALTER TABLE [dbo].[delta_configure_user_quiz]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[delta_user] ([Id])
GO
ALTER TABLE [dbo].[delta_question]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[delta_quiz] ([Id])
GO
ALTER TABLE [dbo].[delta_user_quiz_result]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[delta_quiz] ([Id])
GO
ALTER TABLE [dbo].[delta_user_quiz_result]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[delta_user] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[usp_getQuizByUserId]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_getQuizByUserId] (
	@UserId bigint
	)
AS
BEGIN
	select da.Id As 'AnswerId', da.name as 'Answer', da.IsCorrect,
	dq.Id as 'QuestionId', dq.Name as 'Question', dq.Description as 'QuestionDetails',
	d.Id as 'QuizId', d.Name as 'QuizName', d.PassPercentage,
	uq.UserId as 'UserId' from delta_answer da
left join delta_question dq on da.QuestionId = dq.Id
left join delta_quiz d on dq.QuizId = d.Id
left join delta_configure_user_quiz uq on uq.QuizId = d.Id
where uq.UserId = @UserId and uq.isexpired = 0
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserByEmail]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[usp_GetUserByEmail] (
	@email nvarchar(300)
	)
AS
BEGIN
	select Id, email, fullname, gender
	from delta_user
	where email = @email
END
GO
/****** Object:  StoredProcedure [dbo].[usp_insertUserQuizResult]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_insertUserQuizResult](
	@TotalQuestion int,
	@TotalAttempted int,
	@TotalCorrect int,
	@UserId bigint,
	@QuizId bigint
)
as
begin
	insert into delta_user_quiz_result(TotalQuestion, TotalAttempted
	, TotalCorrect, UserId, QuizId)
	values(@TotalQuestion,
	@TotalAttempted,
	@TotalCorrect,
	@UserId,
	@QuizId)
end
GO
/****** Object:  StoredProcedure [dbo].[usp_signInUserByUserNameAndPassword]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_signInUserByUserNameAndPassword] (
	@Email NVARCHAR(300)
	,@Password NVARCHAR(MAX)
	)
AS
BEGIN
	SELECT fullname, email, EmailVerified, gender
	FROM delta_user
	WHERE email = @email
		AND password = @password
END
GO
/****** Object:  StoredProcedure [dbo].[usp_signUpUser]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--CREATE TABLE delta_user (
--	Id BIGINT IDENTITY(1, 1) PRIMARY KEY
--	,FullName NVARCHAR(100) NOT NULL
--	,Email NVARCHAR(300) NOT NULL
--	,Gender NVARCHAR(10) NOT NULL default('Male')
--	,Password NVARCHAR(MAX) NOT NULL
--	,CreatedOn DATETIME DEFAULT(getdate())
--	)

--INSERT INTO delta_user (
--	FullName
--	,Email
--	,Gender
--	,Password
--	)
--VALUES (
--	'Bijay Kumar Yadav'
--	,'b@b.com'
--	,'Male'
--	,1234
--	)

--CREATE PROCEDURE usp_signInUserByUserNameAndPassword (
--	@Email NVARCHAR(300)
--	,@Password NVARCHAR(MAX)
--	)
--AS
--BEGIN
--	SELECT fullname
--	FROM delta_user
--	WHERE email = @email
--		AND password = @password
--END

CREATE PROCEDURE [dbo].[usp_signUpUser] (
	@FullName NVARCHAR(100)
	,@Email NVARCHAR(300)
	,@Gender NVARCHAR(10)
	,@Password NVARCHAR(MAX)
	)
AS
BEGIN
	INSERT INTO delta_user (
	FullName
	,Email
	,Gender
	,Password
	)
VALUES (
	@FullName
	,@Email
	,@Gender
	,@Password
	)
END

SELECT *
FROM delta_user;

--drop table delta_user;
GO
/****** Object:  StoredProcedure [dbo].[usp_userExistsByEmail]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_userExistsByEmail] (
	@Email NVARCHAR(300)
	)
AS
BEGIN
	SELECT fullname
	FROM delta_user
	WHERE email = @email
END
GO
/****** Object:  StoredProcedure [dbo].[usp_verifyUserById]    Script Date: 11/10/2020 6:04:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[usp_verifyUserById] (
	@Id bigint
	)
AS
BEGIN
	update delta_user
set EmailVerified = 1
where Id = @Id
END

GO
USE [master]
GO
ALTER DATABASE [company_delta_flow] SET  READ_WRITE 
GO
