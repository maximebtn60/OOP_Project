using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    class FacilityMember : User, IPersonalInformations
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string lastname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string mail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string birthDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Subject SubjectTaugth { get; set; }
        public Student[] Class { get; set; }
        public int NumClass { get; set; }

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
                    lastname = columns[1];
                    mail = columns[2];
                    phone = columns[3];
                    string subjects = columns[4];
                    if (subjects == "french") SubjectTaugth = Subject.french;
                    else if (subjects == "history") SubjectTaugth = Subject.history;
                    else if (subjects == "english") SubjectTaugth = Subject.english;
                    else if (subjects == "maths") SubjectTaugth = Subject.maths;
                    else if (subjects == "litterature") SubjectTaugth = Subject.litterature;
                    //classe = columns[5]; // problem with the data base. Impossibility to stock data in a data base with a string []

                }
            }
            reader.Close();// closing of the streamreader
        }
        public FacilityMember(Subject subjectTaugth, int numClass, Student[] Class) : base()
        {

            this.SubjectTaugth = subjectTaugth;
            this.NumClass = numClass;
            int i = 0;
            StreamReader readStudent = new StreamReader(pathStudent);
            while (readStudent != null)
            {
                string[] rstudent = readStudent.ReadLine().Split(';');
                if (Convert.ToInt32(rstudent[9]) == NumClass)
                {
                    Student student = new Student();
                    student.name = rstudent[0];
                    student.lastname = rstudent[1];
                    student.StudentID = rstudent[2];
                    student.birthDate = rstudent[3];
                    student.Absences = Convert.ToInt32(rstudent[4]);
                    student.mail = rstudent[5];
                    student.phone = rstudent[6];
                    student.Tutor = Convert.ToBoolean(rstudent[7]);
                    student.GradeLevel = Convert.ToInt32(rstudent[8]);
                    student.Workgroup = Convert.ToInt32(rstudent[9]);
                    List<string> courses = new List<string>();
                    foreach (string course in rstudent[10].Split(',')) courses.Add(course);
                    student.Courses = courses;
                    student.UnpaidFees = Convert.ToDouble(rstudent[11]);
                    //student.Grades = "...";                      //à faire une fois qu'on a l'extracteur de grades

                    Class[i] = student;
                    i++;
                }

            }
        }
        private List<Student> DisplayTutorList()
        {
            List<Student> tutorList = new List<Student>();
            foreach (Student student in Class)
            {
                if (student.Tutor == true) tutorList.Add(student);
            }
            return tutorList;
        }
        private void DisplayTutorInfo(Student student)
        {
            if (student.Tutor == true)
            {
                Console.WriteLine(student.ToString());
            }
        }
        private void DisplayAttendandePerStudent(Student student)
        {
            Console.WriteLine($"The student {student.lastname} {student.name} has been absent {student.Absences} times");
        }
        private void DisplayStudentResults(Student student)
        {
            foreach (Exam exam in student.Grades) Console.WriteLine($"subject:{exam.Sub} Mark:{exam.Mark} coef:{exam.Coef}");
        }
        private string DisplayAdminInfos(Admin admin)
        {
            return admin.ToString();
        }
        private void AddResultsExam()
        {
            Exam exam = new Exam();
            Console.Write("Enter the subject : ");
            string subjects = Console.ReadLine();
            if (subjects == "french") exam.Sub = Subject.french;
            else if (subjects == "history") exam.Sub = Subject.history;
            else if (subjects == "english") exam.Sub = Subject.english;
            else if (subjects == "maths") exam.Sub = Subject.maths;
            else if (subjects == "litterature") exam.Sub = Subject.litterature;
            Console.Write("Enter the coefficient : ");
            exam.Coef = Convert.ToDouble(Console.ReadLine());
            double mark = new double();

            foreach (Student student in Class)
            {
                Console.Write($"Enter the mark of {student.lastname} {student.name} : ");
                exam.Mark = Convert.ToDouble(Console.ReadLine());
                student.Grades.Add(exam);
            }
        }
        private void GiveAbsenceToStudents()
        {
            Console.WriteLine("who is absent ? ");
            Console.WriteLine("forename : ");
            string forenameAbs = Console.ReadLine();
            Console.WriteLine("name : ");
            string nameAbs = Console.ReadLine();
            bool test = false;
            foreach (Student student in Class)
            {
                if (student.lastname == forenameAbs && student.name == nameAbs)
                {
                    student.Absences += 1;
                    test = true;
                }
            }
            if (test == false) Console.WriteLine("the student has not been found");
        }
        private void GiveHomework()
        {
            //lorsqu'on aura décidé d'une plateforme pour mettre les devoirs
        }
        public void EditStudents()
        {
            StreamWriter writer = new StreamWriter(pathStudent);
            string line = "";
            foreach (Student student in Class)
            {
                line = ($"{student.lastname};{student.name};{student.StudentID};{student.birthDate};{student.Absences};{student.mail};{student.phone};{student.Tutor};{student.GradeLevel};{student.Workgroup};");


            }

        }
    }
}
