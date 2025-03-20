using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    /**
     * Business logic layer for handling contacts
     */
    public class clsContact
    {
        /**
         * Enumeration to determine whether the object is in Add or Update mode
         */
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        // Properties representing contact details
        /** @property int ID - Contact ID */
        public int ID { set; get; }
        /** @property string FirstName - First Name of the contact */
        public string FirstName { set; get; }
        /** @property string LastName - Last Name of the contact */
        public string LastName { set; get; }
        /** @property string Email - Email address */
        public string Email { set; get; }
        /** @property string Phone - Phone number */
        public string Phone { set; get; }
        /** @property string Address - Physical address */
        public string Address { set; get; }
        /** @property DateTime DateOfBirth - Date of birth */
        public DateTime DateOfBirth { set; get; }
        /** @property string ImagePath - Path to the contact's image */
        public string ImagePath { set; get; }
        /** @property int CountryID - Country ID reference */
        public int CountryID { set; get; }

        /**
         * Default constructor initializing default values
         */
        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }

        /**
         * Private constructor for retrieving contact information
         * 
         * @param ID Contact ID
         * @param FirstName First Name
         * @param LastName Last Name
         * @param Email Email address
         * @param Phone Phone number
         * @param Address Physical address
         * @param DateOfBirth Date of birth
         * @param CountryID Country ID reference
         * @param ImagePath Path to the contact's image
         */
        private clsContact(int ID, string FirstName, string LastName, string Email,
            string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }

        /**
         * Adds a new contact by calling the Data Access Layer
         * 
         * @return bool True if the contact is added successfully, otherwise false
         */
        private bool _AddNewContact()
        {
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
            return (this.ID != -1);
        }

        /**
         * Updates an existing contact by calling the Data Access Layer
         * 
         * @return bool True if the contact is updated successfully, otherwise false
         */
        private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        }

        /**
         * Finds a contact by ID and returns a clsContact object
         * 
         * @param ID Contact ID
         * @return clsContact Contact object if found, otherwise null
         */
        public static clsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.GetContactInfoByID(ID, ref FirstName, ref LastName,
                    ref Email, ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
            {
                return new clsContact(ID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath);
            }
            else
                return null;
        }

        /**
         * Saves the contact - either inserts a new record or updates an existing one
         * 
         * @return bool True if save operation is successful, otherwise false
         */
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateContact();
            }
            return false;
        }

        /**
         * Retrieves all contacts as a DataTable
         * 
         * @return DataTable A table containing all contacts
         */
        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }

        /**
         * Deletes a contact by ID
         * 
         * @param ID Contact ID
         * @return bool True if the contact is deleted successfully, otherwise false
         */
        public static bool DeleteContact(int ID)
        {
            return clsContactDataAccess.DeleteContact(ID);
        }

        /**
         * Checks if a contact exists in the database
         * 
         * @param ID Contact ID
         * @return bool True if the contact exists, otherwise false
         */
        public static bool isContactExist(int ID)
        {
            return clsContactDataAccess.IsContactExist(ID);
        }
    }
}
