create procedure [dbo].[DeleteUser] @userId int
as
begin
  delete from t_User where id = @userId;
end
