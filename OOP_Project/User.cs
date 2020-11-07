using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Project
{
    public abstract class User
    {
        public string login;
        public string password;
        public string pathAccessibilityLevel = ".//AccessibilityLevel.txt";
        public string pathStudent = ".//Student.txt";
        public string pathFacilityMember = null; // to complete
        public string pathAdmin = null; // to complete
        public string PathDispo { get; set; }


        public abstract bool Login();

        public abstract void ExtractData();
    }
}
