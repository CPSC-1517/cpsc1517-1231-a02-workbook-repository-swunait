using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlClassLibrary
{
    public class NhlPlayer
    {
        // A class the following components:
        // 1) Fields - for storing data
        // 2) Properties - for encapulating access (read or write) to the data fields
        // 3) Constructors
        //      - for initialize fields/properties to meaningful values
        //      - It is a method with no return type and same name as the Class name
        // 4) Instance-Level Methods
        //      - methods that needs to access fields/properties of the object
        //      - To access a instance method you need to create an instance of the class 
        // 5) Class-level (static) Methods
        //      - methods that does not neeed to access fields/properties of the object
        //      - To access a class-level (static) method you specify the ClassName.MethodName()

        // Define data fields for a NhlPlayer
        private string _firstName;
        private string _lastName;
        private int _jerseyNumber;
        private PlayerPosition _position;
        private int _goals;
        private int _assists;

        // Define properties to encapsulate access to the data fields
        // In C# there are three different ways to define a property
        //  1) Fully-implemented properties
        //      - use this when you need validate the new values being assigned
        //  2) Auto-implemented properties
        //      - use this when there is no need to validate the new value being assigned
        //  3) Expression-body modifier
        //      - use this syntax when there is only ONE statement 

        // Define fully-implemented properties for _firstName, _lastName, _jerseyNumber, _goals, _assists
        public string FirstName
        {
            //get 
            //{ 
            //    return _firstName;
            //}
            get => _firstName;  // expression-body modifier syntax

            set
            {
                // throw an ArgumentNullException if the new value is null or an empty string
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First Name cannot be blank.");
                }
                // throw an ArgumentException if the new value does not contain at least 2 characters
                if (value.Trim().Length < 2)
                {
                    throw new ArgumentException("First Name must contain 2 or more characters.");
                }
                _firstName = value;
            }
        }
        // Define auto-implemented properties for _position and add new property to Points



    }
}
