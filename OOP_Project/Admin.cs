using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    class Admin : User, IPersonalInformations
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }//not implemented in admin class

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
                    lastname = columns[1];
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

        public void AddAdmin()
        {
            Console.WriteLine("Name ?");
            string name2 = Console.ReadLine();
            Console.WriteLine("Forename ?");
            string forename2 = Console.ReadLine();
            Console.WriteLine("email ?");
            string email2 = Console.ReadLine();
            Console.WriteLine("phone ?");
            string phone2 = Console.ReadLine();
            Console.WriteLine("Password ?");
            string password2 = Console.ReadLine();

            string sumData = name2 + ";" + forename2 + ";" + email2 + ";" + phone2;
            string sumAccessData = email2 + ";" + password2 + ";" + "admin";

            Registration.WriteData(pathAdmin, sumData);//add the admin data in the admin file
            Registration.WriteData(pathAccessibilityLevel, sumAccessData);//add the admin in the accessibility file
        }
        public void Delete(string login2)//delete admin, student or facility Member
        {
            string accessLevel = null;
            string path = null;

            //modification of the accessibilityLevel data
            StreamReader reader2 = new StreamReader(pathAccessibilityLevel);
            List<string> tab2 = new List<string>();
            string temp2 = " ";
            while (temp2 != null)
            {
                temp2 = reader2.ReadLine();
                if (temp2 == null) break;
                string[] comparison2 = temp2.Split(';');
                if (comparison2[0] == login2) { accessLevel = comparison2[2]; }//if equal to login don't add it to the list but give us where the personal data is stored
                else tab2.Add(temp2);

            }
            reader2.Close();


            if (accessLevel == "admin") path = pathAdmin;
            else if (accessLevel == "facilityMember") path = pathFacilityMember;
            else path = pathStudent;

            //modification of the personal data
            StreamReader reader = new StreamReader(path);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[2] == login2) { }
                else tab.Add(temp);


            }
            reader.Close();


            //rewrite of the files with the new modified information
            File.Delete(path);
            File.Delete(pathAccessibilityLevel);
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);
                    Console.WriteLine("passe1");
                }
            }
            stream.Dispose();

            FileStream stream2 = new FileStream(pathAccessibilityLevel, FileMode.OpenOrCreate);
            using (StreamWriter writer2 = new StreamWriter(stream2))
            {
                //keep all the data already present
                for (int i = 0; i < tab2.Count; i++)
                {
                    writer2.WriteLine(tab2[i]);
                    Console.WriteLine("passe2");
                }
            }
            stream2.Dispose();
        }

    }
}
