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
        public List<Exam> Grades { get; set; } // classe/enum Exam à créer (note, coefficient, matière)
        public int Workgroup { get; set; }
        public int GradeLevel { get; set; }
        public List<string> Courses { get; set; } // A voir si on ne fait pas plutôt un tableau au lieu d'une liste 
        public bool Tutor { get; set; }
        public int Absences { get; set; }
        public string name { get ; set ; }
        public string lastname { get; set ; }
        public string mail { get; set; }
        public string phone { get ; set ; }
        public string birthDate { get; set; }

        public Student()
        {

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

        // extract student data from the database (student file) 
        public override void ExtractData() // à terminer lorsque le fichier student aura été créé
        {
            StreamReader reader = new StreamReader(pathStudent);
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] columns = temp.Split(';');
                if (login == columns[5])
                {
                    name = columns[0];
                    lastname = columns[1];
                    mail = columns[2];
                    StudentID = columns[3];
                    birthDate = columns[4];
                    Absences = Convert.ToInt32(columns[5]);
                    // to continue
                }
            }
            reader.Close();// closing of the streamreader
        }



    }
}
