CREATE PROCEDURE sp_DeleteContact
    @ID INT
AS
BEGIN
    DELETE FROM Contacts WHERE ContactID = @ID;
END
