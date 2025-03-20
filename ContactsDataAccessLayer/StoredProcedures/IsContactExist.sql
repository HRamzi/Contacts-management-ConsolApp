CREATE PROCEDURE sp_IsContactExist
    @ID INT,
    @Exists BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Contacts WHERE ContactID = @ID)
        SET @Exists = 1;
    ELSE
        SET @Exists = 0;
END
