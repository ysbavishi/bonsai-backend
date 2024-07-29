use IVP_3310;
select * from [User];
select * from client;
select * from task;
select * from tasktime;

select * from payment;

drop procedure GETWeeklyCount;

-- Stored procedure to count number of weeks a task took
CREATE PROCEDURE GetWeeklyCount (
	@ClientId INT,
	@TaskId INT,
	@DIFF INT OUTPUT,
)
AS
BEGIN
	select @DIFF = DATEDIFF(ww, StartDate, CurrentDate)
	from TaskTime
	where ClientId = @ClientId and TaskId = @TaskId;
	return @DIFF
end

DECLARE @ClientId int = 2;
DECLARE @TaskID int = 2;
DECLARE @DIFF int;
exec @DIFF=GetWeeklyCount @ClientId, @TaskId, @DIFF=null; 
select @DIFF * 500

/*
DECLARE @p_OutputInt int = 4
exec GetWeeklyCount @p_InputInt = 1, @p_OutputInt = @p_OutputInt  OUTPUT
select @p_OutputInt
*/

insert into Task(ClientId, ClientsId, UserId, [Description], [Status])
values (2, 2, 2, 'FOR TESTING PROC', 1)

insert into TaskTime(UserId, ClientId, TaskId, CurrentDate, StartDate)
values (2, 2, 2, GETDATE(), DATEADD(ww, -2, GETDATE()))

select * from TaskTime

drop procedure CalcOutstandingAmt

create procedure CalcOutstandingAmt (
	@ClientId INT,
	@TaskId INT,
	@DIFF INT,
	@STATUS INT
)
AS
BEGIN
	 
	 exec @DIFF=GetWeeklyCount @ClientId, @TaskId, @DIFF=null
	 IF (@STATUS = 1) 
		select @DIFF * 500
	 ELSE 
		select (@DIFF * 500) + 100
	 
END

DECLARE @DIFF int;
DECLARE @STATUS int
SELECT @STATUS = [STATUS]
from Task where ClientId = 2 and TaskId = 2
exec @DIFF=CalcOutstandingAmt @ClientId=2, @TaskId=2, @DIFF = 0, @STATUS = @STATUS;

update Task set Status = 0 where TaskId =2;