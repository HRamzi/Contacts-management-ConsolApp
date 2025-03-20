CREATE PROCEDURE sp_AddNewContact
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @DateOfBirth DATE,
    @CountryID INT,
    @ImagePath NVARCHAR(255),
    @NewID INT OUTPUT
AS
BEGIN
    INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
    VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);

    SET @NewID = SCOPE_IDENTITY();
END
