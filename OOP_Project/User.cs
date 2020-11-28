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
        public string pathFacilityMember = ".//FacilityMember.txt";
        public string pathAdmin = ".//Admin.txt";
        public string PathDispo = ".//Disponibilities.txt";
        public string pathTable = ".//TimeTable.txt";


        public abstract bool Login();

        public abstract void ExtractData();
    }
}
