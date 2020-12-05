using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    public class Student : User, IPersonalInformations
    {
        public double UnpaidFees { get; set; }
        public string StudentID { get; set; }

        public int Workgroup { get; set; }
        public int GradeLevel { get; set; }

        public bool Tutor { get; set; }
        public int Absences { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }
        public int NbSubject { get; set; }
        public int Class { get; set; }
        public int Level { get; set; }
        public string GradesTemp = null;
        public string Data { get; set; }
        public string[,] TimeTable { get; set; }



        List<Subject> courses = new List<Subject>();
        public List<Subject> Courses
        {
            get
            {
                return this.Courses = courses;
            }
            set
            {
                value = courses;
            }
        }

        List<Exam> grades = new List<Exam>();
        public List<Exam> Grades
        {
            get
            {
                return this.Grades = grades;
            }
            set
            {
                value = grades;
            }
        }

        public Student(bool ctro)
        {

        }

        public Student(string name2, string lastname2)
        {
            StreamReader readStudent = new StreamReader(pathStudent);
            string temp = "";
            while (temp != null)
            {
                temp = readStudent.ReadLine();
                if (temp == null) break;
                string[] rstudent = temp.Split(';');
                if (rstudent[0] == name2 && rstudent[1] == lastname2)
                {

                    name = rstudent[0];
                    lastname = rstudent[1];
                    mail = rstudent[2];
                    StudentID = rstudent[3];
                    birthDate = rstudent[4];
                    Absences = Convert.ToInt32(rstudent[5]);
                    phone = rstudent[6];
                    if (rstudent[7] == "0") Tutor = false;
                    else Tutor = true;
                    GradeLevel = Convert.ToInt32(rstudent[8]);
                    Workgroup = Convert.ToInt32(rstudent[9]);
                    UnpaidFees = Convert.ToDouble(rstudent[10]);
                    NbSubject = Convert.ToInt32(rstudent[11]);
                    for (int l = 12; l < 12 + NbSubject; l++)
                    {
                        GradesTemp = GradesTemp + ";" + rstudent[l];
                    }
                    for (int l = 12; l < 12 + NbSubject; l++)
                    {

                        string[] splitTab = rstudent[l].Split(',');
                        if (rstudent[l].Length > 1)
                        {
                            Courses.Add((Subject)Convert.ToInt32(splitTab[0]));
                            int k = 0;
                            while (k < splitTab.Length)
                            {


                                Exam exam = new Exam();
                                exam.Sub = Convert.ToInt32(splitTab[k]);
                                exam.Mark = Convert.ToDouble(splitTab[k + 1]);
                                exam.Coef = Convert.ToDouble(splitTab[k + 2]);
                                exam.Date = splitTab[k + 3];
                                Grades.Add(exam);
                                k = k + 4;
                            }
                        }
                        else
                        {
                            Subject adding = new Subject();
                            adding = (Subject)Convert.ToInt32(Convert.ToInt32(rstudent[l]));
                            Courses.Add(adding);

                        }
                    }

                }
            }
            readStudent.Close();
            string tut = null;
            if (Tutor == false) tut = "0";
            else tut = "1";
            Data = ($"{name};{lastname};{mail};{StudentID};{birthDate};{Absences};{phone};{tut};{GradeLevel};{Workgroup};{UnpaidFees};{NbSubject}{GradesTemp}");
        }

        public Student()
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
            StreamReader readStudent = new StreamReader(pathStudent);
            string temp = "";
            while (temp != null)
            {
                temp = readStudent.ReadLine();
                if (temp == null) break;
                string[] rstudent = temp.Split(';');
                if (rstudent[2] == login)
                {

                    name = rstudent[0];
                    lastname = rstudent[1];
                    mail = rstudent[2];
                    StudentID = rstudent[3];
                    birthDate = rstudent[4];
                    Absences = Convert.ToInt32(rstudent[5]);
                    phone = rstudent[6];
                    if (rstudent[7] == "0") Tutor = false;
                    else Tutor = true;
                    GradeLevel = Convert.ToInt32(rstudent[8]);
                    Workgroup = Convert.ToInt32(rstudent[9]);
                    UnpaidFees = Convert.ToDouble(rstudent[10]);
                    NbSubject = Convert.ToInt32(rstudent[11]);
                    for (int l = 12; l < 12 + NbSubject; l++)
                    {
                        GradesTemp = GradesTemp + ";" + rstudent[l];
                    }
                    for (int l = 12; l < 12 + NbSubject; l++)
                    {

                        string[] splitTab = rstudent[l].Split(',');
                        if (rstudent[l].Length > 1)
                        {
                            Courses.Add((Subject)Convert.ToInt32(splitTab[0]));
                            int k = 0;
                            while (k < splitTab.Length)
                            {


                                Exam exam = new Exam();
                                exam.Sub = Convert.ToInt32(splitTab[k]);
                                exam.Mark = Convert.ToDouble(splitTab[k + 1]);
                                exam.Coef = Convert.ToDouble(splitTab[k + 2]);
                                exam.Date = splitTab[k + 3];
                                Grades.Add(exam);
                                k = k + 4;
                            }
                        }
                        else
                        {
                            Subject adding = new Subject();
                            adding = (Subject)Convert.ToInt32(Convert.ToInt32(rstudent[l]));
                            Courses.Add(adding);

                        }
                    }

                }

            }
            readStudent.Close();
            string tut = null;
            if (Tutor == false) tut = "0";
            else tut = "1";
            Data = ($"{name};{lastname};{mail};{StudentID};{birthDate};{Absences};{phone};{tut};{GradeLevel};{Workgroup};{UnpaidFees};{NbSubject}{GradesTemp}");
            TimeTable = new string[5, 4];
            StreamReader readTable = new StreamReader(pathTable);       //extract timeTable
            string temp2 = "";
            while (temp2 != null)
            {
                temp2 = readTable.ReadLine();
                if (temp2 == null) break;
                if (Convert.ToInt32(temp2.Split(';')[0].Split(',')[0]) == GradeLevel & Convert.ToInt32(temp2.Split(';')[0].Split(',')[1]) == Workgroup)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (temp2.Split(';')[i + 1].Split(',')[j] == "") TimeTable[i, j] = null;
                            else TimeTable[i, j] = temp2.Split(';')[i + 1].Split(',')[j];
                        }
                    }
                    break;
                }
            }
            readTable.Close();
        }
        public Student(string login3)
        {
            StreamReader reader = new StreamReader(pathAccessibilityLevel); // declaration of the reader and the link of the file
            string temp2 = " ";
            while (temp2 != null)
            {
                temp2 = reader.ReadLine();
                if (temp2 == null) break;
                string[] columns = temp2.Split(';');
                if (columns[0] == login3)
                {
                    password = columns[1];
                }

            }
            reader.Close();// closing of the streamreader



            StreamReader readStudent = new StreamReader(pathStudent);
            string temp = "";
            while (temp != null)
            {
                temp = readStudent.ReadLine();
                if (temp == null) break;
                string[] rstudent = temp.Split(';');
                if (rstudent[2] == login3)
                {

                    name = rstudent[0];
                    lastname = rstudent[1];
                    mail = rstudent[2];
                    StudentID = rstudent[3];
                    birthDate = rstudent[4];
                    Absences = Convert.ToInt32(rstudent[5]);
                    phone = rstudent[6];
                    if (rstudent[7] == "0") Tutor = false;
                    else Tutor = true;
                    GradeLevel = Convert.ToInt32(rstudent[8]);
                    Workgroup = Convert.ToInt32(rstudent[9]);
                    UnpaidFees = Convert.ToDouble(rstudent[10]);
                    NbSubject = Convert.ToInt32(rstudent[11]);
                    for (int l = 12; l < 12 + NbSubject; l++)
                    {
                        GradesTemp = GradesTemp + ";" + rstudent[l];
                    }
                    for (int l = 12; l < 12 + NbSubject; l++)
                    {

                        string[] splitTab = rstudent[l].Split(',');
                        if (rstudent[l].Length > 1)
                        {
                            Courses.Add((Subject)Convert.ToInt32(splitTab[0]));
                            int k = 0;
                            while (k < splitTab.Length)
                            {


                                Exam exam = new Exam();
                                exam.Sub = Convert.ToInt32(splitTab[k]);
                                exam.Mark = Convert.ToDouble(splitTab[k + 1]);
                                exam.Coef = Convert.ToDouble(splitTab[k + 2]);
                                exam.Date = splitTab[k + 3];
                                Grades.Add(exam);
                                k = k + 4;
                            }
                        }
                        else
                        {
                            Subject adding = new Subject();
                            adding = (Subject)Convert.ToInt32(Convert.ToInt32(rstudent[l]));
                            Courses.Add(adding);

                        }
                    }

                }
            }
            readStudent.Close();
            string tut = null;
            if (Tutor == false) tut = "0";
            else tut = "1";
            Data = ($"{name};{lastname};{mail};{StudentID};{birthDate};{Absences};{phone};{tut};{GradeLevel};{Workgroup};{UnpaidFees};{NbSubject}{GradesTemp}");

        }

        public void ExeFunctions()
        {
            string carryOn = "N";
            Console.Clear();
            do
            {
                DisplayPersonalInfos();
                Console.WriteLine("1. Display Exam\n" +
                    "2. DisplayFees\n" +
                    "3. Display Absences\n" +
                    "4. Add to tutor\n" +
                    "5. Leave tutor program\n" +
                    "6. Display personal informations\n" +
                    "7. Display grades\n" +
                    "8. Display TimeTable\n" +
                    "9. Display Courses\n" +
                    "10. Disconnect\n");

                int switchCase = Convert.ToInt32(Console.ReadLine());
                switch (switchCase)
                {
                    case 1:
                        DisplayExam();
                        break;
                    case 2:
                        DisplayFees();
                        break;
                    case 3:
                        DisplayAbsence();

                        break;
                    case 4:
                        AddHimselfTutor();
                        ModifyDataStudent();
                        break;
                    case 5:
                        LeaveTutor();
                        ModifyDataStudent();
                        break;
                    case 6:
                        DisplayPersonalInfos();
                        break;
                    case 7:
                        DisplayGrades();
                        break;
                    case 8:
                        DisplayTimeTable();
                        break;
                    case 9:
                        DisplayCourses();
                        break;
                    case 10:
                        Console.Clear();
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Do you want to disconnect? if yes, enter Y else enter N");
                carryOn = Console.ReadLine();
                while (carryOn != "Y" && carryOn != "N")
                {
                    Console.WriteLine("Do you want to disconnect? if yes, enter Y else enter N");
                    carryOn = Console.ReadLine();
                }
                Console.Clear();


            } while (carryOn != "10");






        }
        public void DisplayExam()
        {
            Calendar.ReadExamAssignment(Class, Level);
        }

        public void DisplayPersonalInfos()
        {
            Console.WriteLine($"First Name: {name}\n " +
                $"Last Name: {lastname}\n" +
                $"Birth Date: {birthDate}\n" +
                $"phone number: {phone}\n" +
                $"email address: {mail}\n");
        }

        public void DisplayFees()
        { Console.WriteLine($"The student still have to pay {UnpaidFees} €"); }

        public void DisplayAbsence()
        { Console.WriteLine($"The student did not attend {Absences} classes"); }

        public void DisplayGrades()
        {
            Console.WriteLine($"Grades of {name} {lastname}");
            for (int i = 0; i < Grades.Count; i++)
            {
                Console.WriteLine(Grades[i].ToString2());
            }
        }

        public void DisplayTimeTable()
        {
            string filler = "|               ";
            string h0 = "  08:30-10:00  ";
            string h1 = "  10:00-11:30  ";
            string h2 = "  13:30-15:00  ";
            string h3 = "  15:00-16:30  ";
            string[] h = { h0, h1, h2, h3 };
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("               |  Monday       |  Tuesday      |  Wednesday    |  Thursday     |  Friday       ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < TimeTable.GetLength(1); i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("________________________________________________________________________________________________");
                Console.Write(h[i]);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int j = 0; j < TimeTable.GetLength(0); j++)
                {
                    if (TimeTable[j, i] != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(TimeTable[j, i]);
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
        }
        public void DisplayCourses()
        {
            Console.Write("The student have the following classes : ");
            for (int i = 0; i < Courses.Count; i++)
            { Console.Write($"{Courses[i]}, "); }
            Console.Write("\n");
        }

        public void AddHimselfTutor()
        {
            if (Tutor == false)
            { Tutor = true; }
            string tut = null;
            if (Tutor == false) tut = "0";
            else tut = "1";
            Data = ($"{name};{lastname};{mail};{StudentID};{birthDate};{Absences};{phone};{tut};{GradeLevel};{Workgroup};{UnpaidFees}{GradesTemp}");
        }

        public void LeaveTutor()
        {
            if (Tutor == true)
            { Tutor = false; }
            string tut = null;
            if (Tutor == false) tut = "0";
            else tut = "1";
            Data = ($"{name};{lastname};{mail};{StudentID};{birthDate};{Absences};{phone};{tut};{GradeLevel};{Workgroup};{UnpaidFees}{GradesTemp}");

        }

        // return true if the login and password are the good ones and also if the category(student) is true
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
                if (columns[0] == login && columns[1] == password && columns[2] == "student")// comparison between the datas of the file and the datas given by the user 
                {
                    access = true;
                }

            }
            reader.Close();// closing of the streamreader
            return access;
        }

        // extract student data from the database (student file) 
        public override void ExtractData()
        {
            StreamReader reader = new StreamReader(pathStudent);
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] rstudent = temp.Split(';');
                if (login == rstudent[2])
                {

                    name = rstudent[0];
                    lastname = rstudent[1];
                    mail = rstudent[2];
                    StudentID = rstudent[3];
                    birthDate = rstudent[4];
                    Absences = Convert.ToInt32(rstudent[5]);
                    phone = rstudent[6];
                    if (rstudent[7] == "0") Tutor = false;
                    else Tutor = true;
                    GradeLevel = Convert.ToInt32(rstudent[8]);
                    Workgroup = Convert.ToInt32(rstudent[9]);
                    UnpaidFees = Convert.ToDouble(rstudent[10]);
                    NbSubject = Convert.ToInt32(rstudent[11]);

                    for (int j = 12; j < 12 + NbSubject; j++)
                    {

                        string[] splitTab = rstudent[j].Split(',');
                        if (splitTab.Length > 1)
                        {
                            Courses.Add((Subject)Convert.ToInt32(splitTab[0]));
                            int k = 0;
                            Exam exam = new Exam();
                            exam.Sub = Convert.ToInt32(splitTab[k]);
                            exam.Mark = Convert.ToDouble(splitTab[k + 1]);
                            exam.Coef = Convert.ToDouble(splitTab[k + 2]);
                            exam.Date = splitTab[k + 3];
                            Grades.Add(exam);

                        }
                        else
                        {
                            Subject adding = new Subject();
                            adding = (Subject)Convert.ToInt32(splitTab[0]);
                            Console.WriteLine(adding);
                            Courses.Add(adding);

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect login or the student doesn't exist");
                    break;
                }


            }
            reader.Close();// closing of the streamreader
        }

        public void ModifyDataStudent()
        {

            string tut = null;
            if (Tutor == false) tut = "0";
            else tut = "1";
            Data = ($"{name};{lastname};{mail};{StudentID};{birthDate};{Absences};{phone};{tut};{GradeLevel};{Workgroup};{UnpaidFees};{NbSubject}{GradesTemp}");
            StreamReader reader = new StreamReader(pathStudent);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] comparison = temp.Split(';');
                if (comparison[2] == mail)
                {
                    tab.Add(Data);
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
    }
}