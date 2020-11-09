--delete from delta_user
select * from delta_user

--update delta_user
--set EmailVerified = 0

--drop proc usp_verifyUserByEmail

--create PROCEDURE usp_GetUserByEmail (
--	@email nvarchar(300)
--	)
--AS
--BEGIN
--	select Id, email, fullname, gender
--	from delta_user
--	where email = @email
--END

--alter table delta_quiz
--add PassPercentage int default 40

--Create table delta_quiz(
--Id bigint identity(1,1) primary key not null,
--Name nvarchar(300) not null,
--PassPercentage int default 40,
--CreatedOn datetime default(getdate())
--)

--insert into delta_quiz(name, passpercentage)
--values('test quiz', 60)

--create table delta_question(
--Id bigint identity(1,1) primary key not null,
--Name nvarchar(300) not null,
--Description nvarchar(1000),
--CreatedOn datetime default(getdate()),
--QuizId bigint  foreign key references delta_quiz(Id)
--)

--insert into delta_question(name, Description, QuizId)
--values('test quiz', 'test quiz', 1)

--create table delta_answer(
--Id bigint identity(1,1) primary key not null,
--Name nvarchar(300) not null,
--Description nvarchar(1000),
--IsCorrect bit default 0,
--CreatedOn datetime default(getdate()),
--QuestionId bigint  foreign key references delta_question(Id)
--)

--insert into delta_answer(name, Description, IsCorrect, QuestionId)
--values('test quiz', 'test quiz', 0, 1)

--create table delta_user_quiz_result(
--Id bigint identity(1,1) primary key not null,
--TotalQuestion int not null,
--TotalAttempted int not null,
--TotalCorrect int not null,
--CreatedOn datetime default(getdate()),
--UserId bigint  foreign key references delta_user(Id),
--QuizId bigint  foreign key references delta_quiz(Id)
--)

--create table delta_configure_user_quiz(
--Id bigint identity(1,1) primary key not null,
--IsExpired bit default 0,
--CreatedOn datetime default(getdate()),
--UserId bigint  foreign key references delta_user(Id),
--QuizId bigint  foreign key references delta_quiz(Id)
--)

--insert into delta_configure_user_quiz(
-- IsExpired, UserId, QuizId)
--values(0, 9, 1)

create PROCEDURE usp_getQuizByUserId (
	@UserId bigint
	)
AS
BEGIN
	select * from delta_configure_user_quiz qu
	left join delta_quiz q on qu.QuizId = q.Id
	left join delta_question dq on q.Id = dq.QuizId
	left join delta_answer a on dq.Id = a.Id
	 where UserId = 9 and isexpired = 0
END

select * from delta_quiz
select * from delta_answer
select * from delta_user