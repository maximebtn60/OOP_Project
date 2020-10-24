using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    class FacilityMember : User, IPersonalInformations
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string forename { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string mail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string birthDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string subject { get; set; }
        public string[] classe { get; set; }

        // return true if the login and password are the good ones and also if the category(facilityMember) is true
        public override bool Login() //complete 
        {
            bool access = false;
            StreamReader reader = new StreamReader(pathAccessibilityLevel); // declaration of the reader and the link of the file
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] columns = temp.Split(';');
                Console.WriteLine($"{columns[0]} {columns[1]} {columns[2]}");
                if (columns[0] == login && columns[1] == password && columns[2] == "FacilityMember")// comparison between the datas of the file and the data given by the user 
                {
                    access = true;
                }

            }
            reader.Close();// closing of the streamreader
            return access;
        }

        // extract student data from the database (student file) 
        public override void ExtractData() // à terminer lorsque le fichier student aura été créé
        {
            StreamReader reader = new StreamReader(pathFacilityMember);
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] columns = temp.Split(';');
                if (login == columns[2])
                {
                    name = columns[0];
                    forename = columns[1];
                    mail = columns[2];
                    phone = columns[3];
                    subject = columns[4];
                    //classe = columns[5]; // problem with the data base. Impossibility to stock data in a data base with a string []

                }
            }
            reader.Close();// closing of the streamreader
        }

    }
}
