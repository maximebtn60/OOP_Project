using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    public class Student : User, IPersonalInformations
    {
        public double UnpaidFees { get; set; }
        public int NbSubject { get; set; }
        public string StudentID { get; set; }
        public int Workgroup { get; set; }
        public int GradeLevel { get; set; }
        public bool Tutor { get; set; }
        public int Absences { get; set; }
        public string name { get ; set ; }
        public string lastname { get; set ; }
        public string mail { get; set; }
        public string phone { get ; set ; }
        public string birthDate { get; set; }
        public string Data { get; set; }

        List<Subject> courses = new List<Subject>();
        public List<Subject> Courses// A voir si on ne fait pas plutôt un tableau au lieu d'une liste 
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

        public Student()
        {
            ExtractData();
        }
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
        public void DisplayPersonalInfo()
        {

        }

        public void DisplayFees()
        { Console.WriteLine($"The student still have to pay {UnpaidFees}"); }

        public void DisplayAbsence()
        { Console.WriteLine($"The student did not attend {Absences} classes"); }

        public void DisplayGrades()
        {

        }
        
        public void DisplayTimeTable()
        {

        }

        public void DisplayCourses()
        {
            Console.Write("The student have the following classes : ");
            for (int i = 0; i < Courses.Count; i++)
            { Console.Write($"{Courses[i]}, "); }
            Console.Write("\n");
        }
        public void DisplayExam()
        {
            Calendar.ReadExamAssignment(Workgroup, GradeLevel);
        }

        public void AddHimselfTutor()
        {
            if (Tutor == false)
            { Tutor = true; }
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
                Console.WriteLine($"{columns[0]} {columns[1]} {columns[2]}");
                if (columns[0] == login && columns[1] == password && columns[2] == "student")// comparison between the datas of the file and the datas given by the user 
                {
                    access = true;
                }

            }
            reader.Close();// closing of the streamreader
            return access;
        }

    }
}
