create procedure [dbo].[UpdateUser] @userId int
  , @firstName nvarchar(50) = ''
  , @lastName nvarchar(50) = ''
  , @dateOfBirth datetime = null
  , @gender nvarchar(10) = ''
  , @email nvarchar(50) = ''
  , @contactNumber nvarchar(20) = ''
as
begin
  if exists (
      select id
      from t_User
      where Id = @userId
      )
  begin
    update t_User
    set FirstName = ISNULL(@firstName, FirstName)
      , LastName = ISNULL(@lastName, LastName)
      , DateOfBirth = ISNULL(@dateOfBirth, DateOfBirth)
      , Gender = ISNULL(@gender, Gender)
      , Email = ISNULL(@email, Email)
      , ContactNumber = ISNULL(@contactNumber, ContactNumber)
    where Id = @userId;
  end
end
