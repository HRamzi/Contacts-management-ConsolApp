CREATE PROCEDURE sp_GetContactByID
    @ID INT
AS
BEGIN
    SELECT ContactID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath
    FROM Contacts
    WHERE ContactID = @ID;
END
