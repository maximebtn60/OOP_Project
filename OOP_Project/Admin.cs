using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    class Admin : User, IPersonalInformations
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string forename { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string mail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string birthDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }//not implemented in admin class

        public override void ExtractData()
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
                }
            }
            reader.Close();// closing of the streamreader

        }
        public override bool Login()//return  true if the identificaton is positive, return false if the identification is negative
        {
            bool access = false;
            StreamReader reader = new StreamReader(pathAccessibilityLevel); // declaration of the reader and the link of the file
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();// give us a way to read a file
                if (temp == null) break;
                string[] columns = temp.Split(';');
                Console.WriteLine($"{columns[0]} {columns[1]} {columns[2]}");
                if (columns[0] == login && columns[1] == password && columns[2] == "Admin")// comparison between the datas of the file and the data given by the user 
                {
                    access = true;
                }

            }
            reader.Close();// closing of the streamreader
            return access;
        }

    }
