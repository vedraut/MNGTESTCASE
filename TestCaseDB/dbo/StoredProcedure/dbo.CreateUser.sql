create procedure [dbo].[CreateUser] @firstName nvarchar(50)
  , @lastName nvarchar(50)
  , @dateOfBirth datetime
  , @gender nvarchar(10)
  , @email nvarchar(50)
  , @contactNumber nvarchar(20)
as
begin
  insert into t_User (
    FirstName
    , LastName
    , DateOfBirth
    , Gender
    , Email
    , ContactNumber
    )
  values (
    @firstName
    , @lastName
    , @dateOfBirth
    , @gender
    , @email
    , @contactNumber
    );
end
