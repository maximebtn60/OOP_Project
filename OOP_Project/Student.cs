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


        List<Subject> courses = new List<Subject>(); // inutile ??
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

        } // pourquoi ?

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
        } // quand l'utilise t-on ? 

        public Student()
        {
            Console.WriteLine("Login ?");
            login = Console.ReadLine();
            Console.WriteLine("Password ?");
            password = Console.ReadLine();
            while (Login() == false)
            {
                if (Login() == true) break;
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

        }

        public void ExeFunctions()
        {
            string carryOn = "N";
            Console.Clear();
            while (carryOn == "N")
            {
                DisplayPersonalInfos();
                Console.WriteLine("1. Display Exam\n" +
                    "2. DisplayFees\n" +
                    "3. Display Absences\n" +
                    "4. Add to tutor\n" +
                    "5. Leave tutor program\n" +
                    "6. Display personal informations\n" +
                    "7. Display grades\n" + 
                    "8. Disconnect\n");

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
                        DisplayTimeTable();
                        break;
                    case 8:
                        DisplayGrades();
                        break;
                    case 9:
                        break;
                    case 10:
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
        { Console.WriteLine($"The student still have to pay {UnpaidFees}"); }

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

        public void DisplayTimeTable()// to do
        {

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
                    Console.WriteLine("passe");
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
