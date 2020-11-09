alter procedure usp_insertUserQuizResult(
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
select * from delta_user_quiz_result