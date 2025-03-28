﻿using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsConsolApp
{
    internal class Program
    {
        /**
         * Finds and displays contact details by ID.
         *
         * @param ID The ID of the contact to find.
         * @return void
         */
        static void testFindContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            if (Contact1 != null)
            {
                Console.WriteLine(Contact1.FirstName + " " + Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.CountryID);
                Console.WriteLine(Contact1.ImagePath);
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] Not found!");
            }
        }

        /**
         * Adds a new contact with predefined details.
         *
         * @return void
         */
        static void testAddNewContact()
        {
            clsContact Contact1 = new clsContact();

            Contact1.FirstName = "Fadi";
            Contact1.LastName = "Maher";
            Contact1.Email = "A@a.com";
            Contact1.Phone = "010010";
            Contact1.Address = "address1";
            Contact1.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            Contact1.ImagePath = "";

            if (Contact1.Save())
            {
                Console.WriteLine("Contact Added Successfully with id=" + Contact1.ID);
            }
        }

        /**
         * Updates an existing contact's details.
         *
         * @param ID The ID of the contact to update.
         * @return void
         */
        static void testUpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            if (Contact1 != null)
            {
                Contact1.FirstName = "Lina";
                Contact1.LastName = "Maher";
                Contact1.Email = "A2@a.com";
                Contact1.Phone = "2222";
                Contact1.Address = "222";
                Contact1.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
                Contact1.CountryID = 1;
                Contact1.ImagePath = "";

                if (Contact1.Save())
                {
                    Console.WriteLine("Contact updated Successfully.");
                }
            }
            else
            {
                Console.WriteLine("Not found!");
            }
        }

        /**
         * Deletes a contact by ID if it exists.
         *
         * @param ID The ID of the contact to delete.
         * @return void
         */
        static void testDeleteContact(int ID)
        {
            if (clsContact.isContactExist(ID))
            {
                if (clsContact.DeleteContact(ID))
                    Console.WriteLine("Contact Deleted Successfully.");
                else
                    Console.WriteLine("Failed to delete contact.");
            }
            else
            {
                Console.WriteLine("The contact with id = " + ID + " is not found.");
            }
        }

        /**
         * Lists all contacts in the database.
         *
         * @return void
         */
        static void ListContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("Contacts Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]} {row["LastName"]}");
            }
        }

        /**
         * Checks if a contact exists by ID.
         *
         * @param ID The ID of the contact to check.
         * @return void
         */
        static void testIsContactExist(int ID)
        {
            if (clsContact.isContactExist(ID))
                Console.WriteLine("Yes, Contact is there.");
            else
                Console.WriteLine("No, Contact is not there.");
        }

        /**
         * Main entry point of the application, testing various contact operations.
         *
         * @param args Command-line arguments.
         * @return void
         */
        static void Main(string[] args)
        {
            testFindContact(6);
            testAddNewContact();
            testUpdateContact(15);
            testDeleteContact(17);
            ListContacts();
            testIsContactExist(1);
            testIsContactExist(100);

            Console.ReadKey();
        }
    }
}
