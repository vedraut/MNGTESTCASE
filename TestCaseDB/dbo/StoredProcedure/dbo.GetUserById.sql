create procedure [dbo].[GetUserById] @userId int
as
begin
  --Here we are reffering Id to userId just for sake of testcase we need to maintain UserId seperately.
  select FirstName
    , LastName
    , DateOfBirth
    , Gender
    , Email
    , ContactNumber
  from t_User
  where Id = @userId
end
