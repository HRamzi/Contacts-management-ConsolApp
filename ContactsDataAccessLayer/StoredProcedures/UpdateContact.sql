CREATE PROCEDURE sp_UpdateContact
    @ID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @DateOfBirth DATE,
    @CountryID INT,
    @ImagePath NVARCHAR(255)
AS
BEGIN
    UPDATE Contacts
    SET FirstName = @FirstName, LastName = @LastName, Email = @Email,
        Phone = @Phone, Address = @Address, DateOfBirth = @DateOfBirth,
        CountryID = @CountryID, ImagePath = @ImagePath
    WHERE ContactID = @ID;
END
