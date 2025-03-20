CREATE PROCEDURE sp_GetAllContacts
AS
BEGIN
    SELECT ContactID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath
    FROM Contacts;
END
