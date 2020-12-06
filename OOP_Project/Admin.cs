﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace OOP_Project
{
    public class Admin : User, IPersonalInformations
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }//not implemented in admin class
        public string Data { get; set; }

        /// <summary>
        /// verifies if the login informations are right
        /// </summary>
        public Admin()
        {
            Console.WriteLine("Login ?");
            login = Console.ReadLine();
            Console.WriteLine("Password ?");
            password = Console.ReadLine();
            while (Login() == false)
            {
                if (Login() == true) break;
                Console.Clear();
                Console.WriteLine("Wrong Login or Password, try again.");
                Console.WriteLine("Login ?");
                login = Console.ReadLine();
                Console.WriteLine("Password ?");
                password = Console.ReadLine();
            }
            StreamReader readAdmin = new StreamReader(pathAdmin);
            string temp = "";
            while (temp != null)
            {
                temp = readAdmin.ReadLine();
                if (temp == null) break;
                string[] rAdmin = temp.Split(';');
                if (rAdmin[2] == login)
                {
                    name = rAdmin[0];
                    lastname = rAdmin[1];
                    mail = rAdmin[2];
                    phone = rAdmin[3];

                }
            }
            readAdmin.Close();
        }

        /// <summary>
        /// extracting data
        /// </summary>
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

        /// <summary>
        /// return  true if the identificaton is positive, return false if the identification is negative
        /// </summary>
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
                if (columns[0] == login && columns[1] == password && columns[2] == "admin")// comparison between the datas of the file and the data given by the user 
                {
                    access = true;
                }

            }
            reader.Close();// closing of the streamreader
            return access;
        }

        public Admin(string login2)
        {
            StreamReader reader = new StreamReader(pathAccessibilityLevel); // declaration of the reader and the link of the file
            string temp2 = " ";
            while (temp2 != null)
            {
                temp2 = reader.ReadLine();// give us a way to read a file
                if (temp2 == null) break;
                string[] columns = temp2.Split(';');
                if (columns[0] == login2)// comparison between the datas of the file and the data given by the user 
                {
                    password = columns[1];
                }

            }
            reader.Close();// closing of the streamreader

            StreamReader readAdmin = new StreamReader(pathAdmin);
            string temp = "";
            while (temp != null)
            {
                temp = readAdmin.ReadLine();
                if (temp == null) break;
                string[] rAdmin = temp.Split(';');
                if (rAdmin[2] == login2)
                {
                    name = rAdmin[0];
                    lastname = rAdmin[1];
                    mail = rAdmin[2];
                    phone = rAdmin[3];

                }
            }
            readAdmin.Close();
            Data = ($"{name};{lastname};{mail};{phone}");
        }
        public void ExeFunctions()
        {
            string carryOn = "N";

            while (carryOn == "N")
            {
                Console.Clear();
                DisplayPersonalInfos();
                Console.WriteLine("1. Add a new admin\n" +
                    "2. Add a new facility member\n" +
                    "3. Add a new student\n" +
                    "4. Delete with login\n" +
                    "5. Change fees of a student\n" +
                    "6. Modify data facility member\n" +
                    "7. Modify student data\n" +
                    "8. Modify admin data\n" +
                    "9. Edit TimeTable\n" + 
                    "10. Disconect\n");

                int switchCase = Convert.ToInt32(Console.ReadLine());
                switch (switchCase)
                {
                    case 1:
                        AddAdmin();
                        break;
                    case 2:
                        AddFacilityMember();
                        break;
                    case 3:
                        AddStudent();
                        break;
                    case 4:
                        Console.WriteLine("login of the person you want to delete");
                        string logTemp = Console.ReadLine();
                        Delete(logTemp);
                        break;
                    case 5:
                        PayFees();
                        break;
                    case 6:
                        ModifPersonalDataFacilityMember();
                        break;
                    case 7:
                        ModifStudentData();
                        break;
                    case 8:
                        ModifAdminData();
                        break;
                    case 9:
                        WriteTimeTable();
                        break;
                    case 10:
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Do you want to disconnect? if yes, enter Y else enter N");
                carryOn = Console.ReadLine();
                while (carryOn != "Y" && carryOn != "N")
                {
                    Console.WriteLine("Do you want to disconnect? if yes, enter Y else enter N");
                    carryOn = Console.ReadLine();
                }
                Console.Clear();
            }
        }

        public void DisplayPersonalInfos()
        {
            Console.WriteLine($"First Name: {name}\n" +
                $"Last Name: {lastname}\n" +
                $"phone number: {phone}\n" +
                $"email address: {mail}\n");
        }
        public void ModifStudentData()
        {
            StreamReader reader = new StreamReader(pathStudent);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                Console.WriteLine(temp.Split(';')[2]);
            }
            reader.Close();

            Console.WriteLine("Enter the login of the student you want to modify");
            string login = Console.ReadLine();
            Student student = new Student(login);
            Console.WriteLine("What do you want to change:\n" +
                "1. phone\n" +
                "2. first name\n" +
                "3. last name\n" +
                "4. login (mail)\n" +
                "5. password\n" +
                "6. Level\n" +
                "7. Workgroup\n"
                );
            int answer = Convert.ToInt32(Console.ReadLine());
            while (answer != 1 && answer != 2 && answer != 3 && answer != 4 && answer != 5 && answer != 6 && answer != 7)
            {
                Console.WriteLine("Incorrect answer");
                Console.WriteLine("What do you want to change:\n" +
                "1. phone\n" +
                "2. first name\n" +
                "3. last name\n" +
                "4. login (mail)\n" +
                "5. password\n" +
                "6. Level\n" +
                "7. Workgroup\n");
                answer = Convert.ToInt32(Console.ReadLine());
            }
            string tut = null;
            switch (answer)
            {
                case 1:
                    Console.WriteLine("New phone number ?");
                    student.phone = Console.ReadLine();
                    if (student.Tutor == false) tut = "0";
                    else tut = "1";
                    student.Data = ($"{student.name};{student.lastname};{student.mail};{student.StudentID};{student.birthDate};{student.Absences};{student.phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                    Console.WriteLine(student.Data);
                    ModifyDataStudentInFile(student, login);
                    break;
                case 2:
                    Console.WriteLine("first name ?");
                    student.name = Console.ReadLine();
                    if (student.Tutor == false) tut = "0";
                    else tut = "1";
                    student.Data = ($"{student.name};{student.lastname};{student.mail};{student.StudentID};{student.birthDate};{student.Absences};{student.phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                    ModifyDataStudentInFile(student, login);
                    break;
                case 3:
                    Console.WriteLine("last name ?");
                    student.lastname = Console.ReadLine();
                    if (student.Tutor == false) tut = "0";
                    else tut = "1";
                    student.Data = ($"{student.name};{student.lastname};{student.mail};{student.StudentID};{student.birthDate};{student.Absences};{student.phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                    ModifyDataStudentInFile(student, login);
                    break;
                case 4:
                    Console.WriteLine("mail ?");
                    student.login = Console.ReadLine();
                    if (student.Tutor == false) tut = "0";
                    else tut = "1";
                    student.Data = ($"{student.name};{student.lastname};{student.login};{student.StudentID};{student.birthDate};{student.Absences};{student.phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                    ModifyDataStudentInFile(student, login);
                    student.Data = ($"{student.login};{student.password};student");
                    ModifyDataStudentInFileAccessibilityLevel(student, login);
                    break;
                case 5:
                    Console.WriteLine("New password ?");
                    student.password = Console.ReadLine();
                    student.Data = ($"{student.mail};{student.password};student");
                    ModifyDataStudentInFileAccessibilityLevel(student, login);
                    break;
                case 6:
                    Console.WriteLine("New level?");
                    student.GradeLevel = Convert.ToInt32(Console.ReadLine());
                    if (student.Tutor == false) tut = "0";
                    else tut = "1";
                    student.Data = ($"{student.name};{student.lastname};{student.mail};{student.StudentID};{student.birthDate};{student.Absences};{student.phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                    ModifyDataStudentInFile(student, login);
                    break;
                case 7:
                    Console.WriteLine("New class?");
                    if (student.Tutor == false) tut = "0";
                    else tut = "1";
                    student.Workgroup = Convert.ToInt32(Console.ReadLine());
                    student.Data = ($"{student.name};{student.lastname};{student.mail};{student.StudentID};{student.birthDate};{student.Absences};{student.phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                    ModifyDataStudentInFile(student, login);
                    break;


            }
        }
        public void ModifyDataStudentInFile(Student stud, string login)
        {
            StreamReader reader = new StreamReader(pathStudent);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[2] == login)
                {
                    tab.Add(stud.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathStudent);
            FileStream stream = new FileStream(pathStudent, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();

        }
        public void ModifyDataStudentInFileAccessibilityLevel(Student stud, string login)
        {
            StreamReader reader = new StreamReader(pathAccessibilityLevel);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[0] == login)
                {
                    tab.Add(stud.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathAccessibilityLevel);
            FileStream stream = new FileStream(pathAccessibilityLevel, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();
        }


        public void ModifAdminData()
        {
            StreamReader reader = new StreamReader(pathAdmin);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                Console.WriteLine(temp.Split(';')[2]);
            }
            reader.Close();

            Console.WriteLine("Enter the login of the admin you want to modify");
            string login = Console.ReadLine();
            Admin admin = new Admin(login);
            Console.WriteLine("What do you want to change:\n" +
                "1. phone\n" +
                "2. first name\n" +
                "3. last name\n" +
                "4. login (mail)\n" +
                "5. password\n");
            int answer = Convert.ToInt32(Console.ReadLine());
            while (answer != 1 && answer != 2 && answer != 3 && answer != 4 && answer != 5)
            {
                Console.WriteLine("Incorrect answer");
                Console.WriteLine("What do you want to change:\n" +
                "1. phone\n" +
                "2. first name\n" +
                "3. last name\n" +
                "4. login (mail)\n" +
                "5. password\n");
                answer = Convert.ToInt32(Console.ReadLine());
            }
            switch (answer)
            {
                case 1:
                    Console.WriteLine("New phone number ?");
                    admin.phone = Console.ReadLine();
                    admin.Data = ($"{admin.name};{admin.lastname};{admin.mail};{admin.phone}");
                    Console.WriteLine(admin.Data);
                    ModifyDataAdminInFile(admin, login);
                    break;
                case 2:
                    Console.WriteLine("first name ?");
                    admin.name = Console.ReadLine();
                    admin.Data = ($"{admin.name};{admin.lastname};{admin.mail};{admin.phone}");
                    Console.WriteLine(admin.Data);
                    ModifyDataAdminInFile(admin, login);
                    break;
                case 3:
                    Console.WriteLine("last name ?");
                    admin.lastname = Console.ReadLine();
                    admin.Data = ($"{admin.name};{admin.lastname};{admin.mail};{admin.phone}");
                    Console.WriteLine(admin.Data);
                    ModifyDataAdminInFile(admin, login);
                    break;
                case 4:
                    Console.WriteLine("mail ?");
                    admin.login = Console.ReadLine();
                    admin.Data = ($"{admin.name};{admin.lastname};{admin.login};{admin.phone}");
                    ModifyDataAdminInFile(admin, login);
                    admin.Data = ($"{admin.login};{admin.password};admin");
                    ModifyDataAdminInFileAccessibilityLevel(admin, login);
                    break;
                case 5:
                    Console.WriteLine("New password ?");
                    admin.password = Console.ReadLine();
                    admin.Data = ($"{admin.mail};{admin.password};admin");
                    ModifyDataAdminInFileAccessibilityLevel(admin, login);
                    break;

            }
        }
        public void ModifyDataAdminInFile(Admin admin, string login)
        {
            StreamReader reader = new StreamReader(pathAdmin);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[2] == login)
                {
                    tab.Add(admin.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathAdmin);
            FileStream stream = new FileStream(pathAdmin, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    Console.WriteLine(tab[i]);
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();
        }
        public void ModifyDataAdminInFileAccessibilityLevel(Admin admin, string login)
        {
            StreamReader reader = new StreamReader(pathAccessibilityLevel);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[0] == login)
                {
                    tab.Add(admin.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathAccessibilityLevel);
            FileStream stream = new FileStream(pathAccessibilityLevel, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();
        }


        public void ModifPersonalDataFacilityMember()
        {
            StreamReader reader = new StreamReader(pathFacilityMember);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                Console.WriteLine(temp.Split(';')[2]);
            }
            reader.Close();

            Console.WriteLine("Enter the login of the facility member you want to modify");
            string login = Console.ReadLine();
            FacilityMember teacher = new FacilityMember(login);
            Console.WriteLine("What do you want to change:\n" +
                "1. phone\n" +
                "2. first name\n" +
                "3. last name\n" +
                "4. login (mail)\n" +
                "5. password\n" +
                "6. Classes assigned\n");
            int answer = Convert.ToInt32(Console.ReadLine());
            while (answer != 1 && answer != 2 && answer != 3 && answer != 4 && answer != 5 && answer != 6)
            {
                Console.WriteLine("Incorrect answer");
                Console.WriteLine("What do you want to change:\n" +
                "1. phone\n" +
                "2. first name\n" +
                "3. last name\n" +
                "4. login (mail)\n" +
                "5. password\n" +
                "6. Classes assigned\n");
                answer = Convert.ToInt32(Console.ReadLine());
            }
            switch (answer)
            {
                case 1:
                    Console.WriteLine("New phone number ?");
                    teacher.phone = Console.ReadLine();
                    teacher.Data = ($"{teacher.name};{teacher.lastname};{teacher.login};{teacher.phone};{teacher.sT};{teacher.lAndC}");
                    Console.WriteLine(teacher.Data);
                    ModifDataFacilityMemberInFile(teacher, login);
                    break;
                case 2:
                    Console.WriteLine("first name ?");
                    teacher.name = Console.ReadLine();
                    teacher.Data = ($"{teacher.name};{teacher.lastname};{teacher.login};{teacher.phone};{teacher.sT};{teacher.lAndC}");
                    Console.WriteLine(teacher.Data);
                    ModifDataFacilityMemberInFile(teacher, login);
                    break;
                case 3:
                    Console.WriteLine("last name ?");
                    teacher.lastname = Console.ReadLine();
                    teacher.Data = ($"{teacher.name};{teacher.lastname};{teacher.login};{teacher.phone};{teacher.sT};{teacher.lAndC}");
                    Console.WriteLine(teacher.Data);
                    ModifDataFacilityMemberInFile(teacher, login);
                    break;
                case 4:
                    Console.WriteLine("mail ?");
                    teacher.login = Console.ReadLine();
                    teacher.Data = ($"{teacher.name};{teacher.lastname};{teacher.login};{teacher.phone};{teacher.sT};{teacher.lAndC}");
                    ModifDataFacilityMemberInFile(teacher, login);
                    teacher.Data = ($"{teacher.login};{teacher.password};facilityMember");
                    ModifDataFacilityMemberInFileAccessibilityLevel(teacher, login);
                    break;
                case 5:
                    Console.WriteLine("New password ?");
                    teacher.password = Console.ReadLine();
                    teacher.Data = ($"{teacher.login};{teacher.password};facilityMember");
                    ModifDataFacilityMemberInFileAccessibilityLevel(teacher, login);
                    break;
                case 6:
                    string levelAndClass = null;
                    string confirmation = "Y";
                    while (confirmation == "Y")
                    {
                        Console.WriteLine("Choose the level you want to give to a facility member");
                        int level = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Choose the number of classes you want to give to facility member");
                        int number2 = Convert.ToInt32(Console.ReadLine());
                        int j = 0;
                        levelAndClass = levelAndClass + level;
                        while (j < number2)
                        {
                            Console.WriteLine("Choose the numero of the class");
                            int numero = Convert.ToInt32(Console.ReadLine());
                            levelAndClass = levelAndClass + "," + numero;
                            j++;
                        }
                        Console.WriteLine("Do you want to enter other ? If yes, enter Y, if no, enter N");
                        confirmation = Console.ReadLine();
                        if (confirmation == "Y") levelAndClass = levelAndClass + ";";

                    }
                    teacher.Data = ($"{teacher.name};{teacher.lastname};{teacher.login};{teacher.phone};{teacher.sT};{levelAndClass}");
                    ModifDataFacilityMemberInFile(teacher, login);
                    break;
            }

        }
        public void ModifDataFacilityMemberInFile(FacilityMember facilityMember, string login)
        {
            StreamReader reader = new StreamReader(pathFacilityMember);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[2] == login)
                {
                    tab.Add(facilityMember.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathFacilityMember);
            FileStream stream = new FileStream(pathFacilityMember, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();

        }
        public void ModifDataFacilityMemberInFileAccessibilityLevel(FacilityMember teacher, string login)
        {
            StreamReader reader = new StreamReader(pathAccessibilityLevel);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[0] == login)
                {
                    tab.Add(teacher.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathAccessibilityLevel);
            FileStream stream = new FileStream(pathAccessibilityLevel, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();
        }

        /// <summary>
        /// To add a new admin into the system
        /// </summary>
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

        /// <summary>
        /// add someone and all the informations about him
        /// </summary>
        public void AddFacilityMember()
        {
            Console.WriteLine("First Name ?");
            string name2 = Console.ReadLine();
            Console.WriteLine("Last name ?");
            string forename2 = Console.ReadLine();
            Console.WriteLine("email ?");
            string email2 = Console.ReadLine();
            Console.WriteLine("phone ?");
            string phone2 = Console.ReadLine();
            Console.WriteLine("Password ?");
            string password2 = Console.ReadLine();

            string[] test = Enum.GetNames(typeof(Subject));
            string coursesAvailable = null;
            for (int index = 0; index < test.Length; index++)
            {
                coursesAvailable = (coursesAvailable + " " + test[index]);
            }
            Console.WriteLine($"Number of subject(s) taught ? {coursesAvailable}");
            int number = Convert.ToInt32(Console.ReadLine());
            while (number < 1 || number > test.Length)
            {
                Console.WriteLine($"Please choose a number between 1 and {test.Length}");
                number = Convert.ToInt32(Console.ReadLine());
            }
            int i = 0;
            Subject check = new Subject();
            List<Subject> courses = new List<Subject>();
            while (i < number) // choice of the courses
            {
                Console.WriteLine($"what course de you want to teach? {coursesAvailable} ");
                string temp = Console.ReadLine();
                int blockage = 1;
                for (int index2 = 0; index2 < test.Length; index2++)
                {
                    if (temp == test[index2])
                    {
                        check = (Subject)index2;
                        blockage = 0;
                    }
                }

                // check that you didn't choose twice the same course
                if (i != 0)
                {
                    for (int j = 0; j < courses.Count; j++)
                    {
                        if (courses[j] == check) blockage++;
                    }
                    if (blockage == 0) courses.Add(check);
                    else Console.WriteLine("Error! incorrect spelling or subject already chosen");
                }
                else
                {
                    if (blockage == 0) courses.Add(check);
                    else Console.WriteLine("Error! incorrect spelling or subject already chosen");
                }
                if (blockage == 0) i++;
            }

            int passe = 0;
            string coursesChosen = null;
            for (int k = 0; k < courses.Count; k++)
            {
                for (int j = 0; j < test.Length; j++)
                {

                    if (courses[k] == (Subject)j)
                    {
                        passe++;
                        if (passe == number) coursesChosen = coursesChosen + j;
                        else coursesChosen = coursesChosen + j + ",";
                    }
                }
            }


            string levelAndClass = null;
            string confirmation = "Y";
            while (confirmation == "Y")
            {
                Console.WriteLine("Choose the level you want to give to a facility member");
                int level = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Choose the number of classes you want to give to facility member");
                int number2 = Convert.ToInt32(Console.ReadLine());
                int j = 0;
                levelAndClass = levelAndClass + level;
                while (j < number2)
                {
                    Console.WriteLine("Choose the numero of the class");
                    int numero = Convert.ToInt32(Console.ReadLine());
                    levelAndClass = levelAndClass + "," + numero;
                    j++;
                }
                Console.WriteLine("Do you want to enter other ? If yes, enter Y, if no, enter N");
                confirmation = Console.ReadLine();
                if (confirmation == "Y") levelAndClass = levelAndClass + ";";
            }
            string sumData = name2 + ";" + forename2 + ";" + email2 + ";" + phone2 + ";" + coursesChosen + ";" + levelAndClass;
            string sumAccessData = email2 + ";" + password2 + ";" + "facilityMember";

            Registration.WriteData(pathFacilityMember, sumData);//add the admin data in the admin file
            Registration.WriteData(pathAccessibilityLevel, sumAccessData);//add the admin in the accessibility file
            Registration.WriteData(PathDispo, name2 + ";" + forename2 + ";;;;;");

        }

        public void AddStudent()
        {
            bool admin = true;
            Registration student = new Registration(admin);
        }

        /// <summary>
        /// delete admin, student or facility Member
        /// </summary>
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
                }
            }
            stream2.Dispose();
        }

        public void TimeTable()
        {
            List<FacilityMember> teachers = new List<FacilityMember>();
            StreamReader reader = new StreamReader(pathFacilityMember); // declaration of the reader and the link of the file
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();// give us a way to read a file
                if (temp == null) break;
                string[] split = temp.Split();
                //FacilityMember teacher = new FacilityMember(split[2]);
                //teachers.Add(teacher);
            }
            reader.Close();


        }

        public static void ModifyDataStudent(Student stud)
        {
            string pathStudent = ".//Student.txt";
            StreamReader reader = new StreamReader(pathStudent);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[2] == stud.mail)
                {
                    tab.Add(stud.Data);
                }
                else tab.Add(temp);


            }
            reader.Close();

            File.Delete(pathStudent);
            FileStream stream = new FileStream(pathStudent, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);

                }
            }
            stream.Dispose();

        }


        /// <summary>
        /// Show the amount of fees you have to pay
        /// </summary>
        public void PayFees()
        {
            Console.WriteLine("PAY FEES");
            Console.WriteLine("First Name of the student?");
            string name = Console.ReadLine();
            Console.WriteLine("Last name of the student");
            string lastname2 = Console.ReadLine();
            Student stud = new Student(name, lastname2);
            Console.WriteLine($"The fees still unpaid are {stud.UnpaidFees}");
            Console.WriteLine("How much do you want to deduce from the fees");
            double number = Convert.ToDouble(Console.ReadLine());
            while (number < 0)
            {
                Console.WriteLine("The number you want to pay is incorrect, please insert an amount superiot to zero");
                number = Convert.ToDouble(Console.ReadLine());
            }
            while (stud.UnpaidFees - number < 0)
            {
                Console.WriteLine("The number you want to pay is too much, please insert an amount inferior to the fees");
                number = Convert.ToDouble(Console.ReadLine());
            }
            stud.UnpaidFees = stud.UnpaidFees - number;
            stud.ModifyDataStudent();
        }
        public void CheckDispo(int level, int classe)
        {
            List<string[]> facilityMembers = new List<string[]>();
            StreamReader facilityReader = new StreamReader(pathFacilityMember);
            string temp = "";
            while (temp != null)                            //reader facilitymember.txt
            {
                temp = facilityReader.ReadLine();
                if (temp == null) break;
                string[] line = temp.Split(';');
                bool test = false;
                for (int i = 5; i < line.Length; i++)
                {
                    if (Convert.ToInt32(line[i].Split(',')[0]) == level & line[i].Substring(1).Contains(Convert.ToString(classe)))    //checks if the specified line corresponds to a facility member of the specified level and class
                    {
                        test = true;
                    }
                }
                if (test == true) facilityMembers.Add(line);
            }
            facilityReader.Close();
            string[][][] tabDispo = new string[facilityMembers.Count][][];
            StreamReader dispoReader = new StreamReader(PathDispo);
            temp = "";
            while (temp != null)                        //reader disponibilitiy.txt
            {
                temp = dispoReader.ReadLine();
                if (temp == null) break;
                string[][] line = new string[7][];
                for (int i = 0; i < 7; i++)
                {
                    line[i] = temp.Split(';')[i].Split(',');
                    int j = 0;
                    foreach (string s in line[i])
                    {
                        if (line[i][j] == "") line[i][j] = null;              //delete empty strings
                        j++;
                    }
                }
                for (int i = 0; i < facilityMembers.Count; i++)
                {
                    if (line[0][0] == facilityMembers[i][0] & line[1][0] == facilityMembers[i][1])          //looks for the facility member with the same name and surname as the disponibility
                    {
                        tabDispo[i] = line;
                        break;
                    }
                }
            }
            string days = "Monday          Tuesday         Wednesday       Thursday        Friday";         //display dispo
            string space1 = "           ";
            string space2 = "     ";
            string line1 = "";
            for (int i = 0; i < tabDispo.Length; i++)                      //display disponibilities of each teacher of the class
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(tabDispo[i][0][0] + " " + tabDispo[i][1][0]);
                foreach (string s in facilityMembers[i][4].Split(','))              //display subjects taught
                {
                    Console.Write((Subject)Convert.ToInt32(s) + ", ");
                }
                Console.WriteLine();
                Console.WriteLine(days);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int j = 0; j < 8; j++)                 //display disponibilities
                {
                    line1 = "";
                    for (int k = 2; k < 7; k++)
                    {
                        if (tabDispo[i][k].Length >= j + 1)
                        {
                            if (tabDispo[i][k][j] != null) line1 += tabDispo[i][k][j] + space2;
                            else line1 += space1 + space2;
                        }
                        else line1 += space1 + space2;
                    }
                    Console.WriteLine(line1);
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("================================================================================================");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
        }
        public void WriteTimeTable()
        {
            int level = -1;
            int classe = -1;
            Console.Write("Enter the level of the class you want to create the timeTable : ");
            level = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the class number of the class you want to create the timeTable : ");
            classe = Convert.ToInt32(Console.ReadLine());
            string[,] timeTable = new string[5, 4];
            StreamReader readTable = new StreamReader(pathTable);       //extract timeTable
            string temp2 = "";
            while (temp2 != null)
            {
                temp2 = readTable.ReadLine();
                if (temp2 == null) break;
                if (Convert.ToInt32(temp2.Split(';')[0].Split(',')[0]) == level & Convert.ToInt32(temp2.Split(';')[0].Split(',')[1]) == classe)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (temp2.Split(';')[i + 1].Split(',')[j] == "") timeTable[i, j] = null;
                            else timeTable[i, j] = temp2.Split(';')[i + 1].Split(',')[j];
                        }
                    }
                    break;
                }
            }
            readTable.Close();
            string test = "yes";
            int day = -1;
            int hour = -1;
            while (test == "yes")
            {
                if (test != "yes") break;
                Console.Clear();
                CheckDispo(level, classe);                          //Display disponibilities
                string filler = "|               ";                 //Display timeTable
                string h0 = "  08:30-10:00  ";
                string h1 = "  10:00-11:30  ";
                string h2 = "  13:30-15:00  ";
                string h3 = "  15:00-16:30  ";
                string[] h = { h0, h1, h2, h3 };
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("               |  Monday       |  Tuesday      |  Wednesday    |  Thursday     |  Friday       ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int i = 0; i < timeTable.GetLength(1); i++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("________________________________________________________________________________________________");
                    Console.Write(h[i]);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    for (int j = 0; j < timeTable.GetLength(0); j++)
                    {
                        if (timeTable[j, i] != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(timeTable[j, i]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(filler);
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                    }
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Which day do you want to change the time table ?");
                Console.WriteLine("Monday : type 1");
                Console.WriteLine("Tuesday : type 2");
                Console.WriteLine("Wednesday : type 3");
                Console.WriteLine("Thursday : type 4");
                Console.WriteLine("Friday : type 5");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Day : ");
                day = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("What time slot do you want to change ?");
                Console.WriteLine("08:30-10:00 : type 1");
                Console.WriteLine("10:00-11:30 : type 2");
                Console.WriteLine("13:30-15:00 : type 3");
                Console.WriteLine("15:00-16:30 : type 4");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Time slot : ");
                hour = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("What is the subject taught on this time slot ?");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{(Subject)i} : type {i + 1}");
                }
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Subject : ");
                int subject = Convert.ToInt32(Console.ReadLine());
                switch (subject)
                {
                    case 1:
                        timeTable[day, hour] = "  french       ";
                        break;
                    case 2:
                        timeTable[day, hour] = "  history      ";
                        break;
                    case 3:
                        timeTable[day, hour] = "  english      ";
                        break;
                    case 4:
                        timeTable[day, hour] = "  maths        ";
                        break;
                    case 5:
                        timeTable[day, hour] = "  litterature  ";
                        break;
                }
                Console.WriteLine("Do you want to make another change ?");
                while (test != "yes" | test != "no")
                {
                    Console.WriteLine("Type yes or no : ");
                    test = Console.ReadLine();
                    if (test == "yes" | test == "no") break;
                }
            }
            string sTimeTable = Convert.ToString(level) + "," + Convert.ToString(classe);
            for (int i = 0; i < 5; i++)         //transtype to string
            {
                sTimeTable += ";";
                sTimeTable += timeTable[i, 0];
                for (int j = 1; j < 4; j++)
                {
                    sTimeTable += ",";
                    sTimeTable += timeTable[i, j];
                }
            }
            StreamReader streamReader = new StreamReader(pathTable);
            List<string> lines = new List<string>();
            string line = "";
            while (line != null)
            {
                line = streamReader.ReadLine();
                if (line == null) break;
                lines.Add(line);
            }
            streamReader.Close();
            bool check = true;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains($"{level},{classe}") == true)
                {
                    lines[i] = sTimeTable;
                    check = false;
                }
            }
            if (check == true)
            {
                lines.Add(sTimeTable);
            }
            File.Delete(pathTable);
            FileStream stream = new FileStream(pathTable, FileMode.OpenOrCreate);
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                foreach (string l in lines)
                {
                    streamWriter.WriteLine(l);
                }
            }
            stream.Dispose();
        }


    }
}
