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
--values('test delta_question', 'test delta_question', 1)

--create table delta_answer(
--Id bigint identity(1,1) primary key not null,
--Name nvarchar(300) not null,
--Description nvarchar(1000),
--IsCorrect bit default 0,
--CreatedOn datetime default(getdate()),
--QuestionId bigint  foreign key references delta_question(Id)
--)

--insert into delta_answer(name, Description, IsCorrect, QuestionId)
--values('test quiz', 'test quiz', 0, 3)

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

--alter PROCEDURE usp_getQuizByUserId (
--	@UserId bigint
--	)
--AS
--BEGIN
--	select da.Id As 'AnswerId', da.name as 'Answer', da.IsCorrect,
--	dq.Id as 'QuestionId', dq.Name as 'Question', dq.Description as 'QuestionDetails',
--	d.Id as 'QuizId', d.Name as 'Quiz Name', d.PassPercentage,
--	uq.UserId as 'UserId' from delta_answer da
--left join delta_question dq on da.QuestionId = dq.Id
--left join delta_quiz d on dq.QuizId = d.Id
--left join delta_configure_user_quiz uq on uq.QuizId = d.Id
--where uq.UserId = @UserId and uq.isexpired = 0
--END

select * from delta_quiz
select * from delta_answer
select * from delta_user
exec usp_getQuizByUserId 9

select da.Id As 'AnswerId', da.name as 'Answer', da.IsCorrect,
	dq.Id as 'QuestionId', dq.Name as 'Question', dq.Description as 'QuestionDetails',
	d.Id as 'QuizId', d.Name as 'QuizName', d.PassPercentage,
	uq.UserId as 'UserId' from delta_answer da
left join delta_question dq on da.QuestionId = dq.Id
left join delta_quiz d on dq.QuizId = d.Id
left join delta_configure_user_quiz uq on uq.QuizId = d.Id
where uq.UserId = 9 and uq.isexpired = 0