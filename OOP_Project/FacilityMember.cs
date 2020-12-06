using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
namespace OOP_Project
{
    public class FacilityMember : User, IPersonalInformations
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }
        public int numLevel { get; set; }
        public int level2 { get; set; }
        public int class2 { get; set; }
        public List<string>[] disponibility = new List<string>[7];
        public int NumClass { get; set; }
        List<Subject> subjectTaught = new List<Subject>();
        public List<Subject> SubjectTaught
        {
            get
            {
                return this.SubjectTaught = subjectTaught;
            }
            set
            {
                value = subjectTaught;
            }
        }
        List<LevelAndClasses> levelandclasses = new List<LevelAndClasses>();
        public List<LevelAndClasses> Levelandclasses
        {
            get
            {
                return this.Levelandclasses = levelandclasses;
            }
            set
            {
                value = levelandclasses;
            }
        }
        public List<Classes> classes = new List<Classes>();
        public List<Classes> ClassesStudent
        {
            get { return this.ClassesStudent = classes; }

            set { value = classes; }

        }
        public List<Student> classe = new List<Student>();
        public List<Student> Class
        {
            get { return this.Class = classe; }

            set { value = classe; }

        }
        public string Data { get; set; }
        public string lAndC { get; set; }
        public string sT { get; set; }


        /// <summary>
        /// The method declare the reader and the link to the file. Then, it
        /// makes a comparison between the data of the file and the data given by the user.
        /// At the end of the method, the streamreader is closed.
        /// </summary>
        /// <returns>true if the login and password are the good ones and also if the category(facilityMember) is true</returns>
        public override bool Login() 
        {
            bool access = false;
            StreamReader reader = new StreamReader(pathAccessibilityLevel); // declaration of the reader and the link of the file
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] columns = temp.Split(';');
                if (columns[0] == login && columns[1] == password && columns[2] == "facilityMember")// comparison between the datas of the file and the data given by the user 
                {
                    access = true;
                }

            }
            reader.Close();// closing of the streamreader
            return access;
        }

        /// <summary>
        /// It extracts student data from the database (student file) and when it
        /// is done, the  streamreader is closed.
        /// </summary>
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
                }
            }
            reader.Close();// closing of the streamreader
        }

        public FacilityMember() : base()
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

            for (int i = 0; i < 7; i++) disponibility[i] = new List<string>();
            ReadDispo();
            StreamReader readTeacher = new StreamReader(pathFacilityMember);
            string temp2 = "";
            while (temp2 != null)
            {
                temp2 = readTeacher.ReadLine();
                if (temp2 == null) break;
                string[] rTeacher = temp2.Split(';');
                if (rTeacher[2] == login)
                {
                    this.name = rTeacher[0];
                    this.lastname = rTeacher[1];
                    this.login = rTeacher[2];
                    this.phone = rTeacher[3];
                    string[] rTeacher2 = rTeacher[4].Split(',');
                    // subjects taught
                    for (int i = 0; i < rTeacher2.Length; i++)
                    {
                        SubjectTaught.Add((Subject)Convert.ToInt32(rTeacher2[i]));
                    }

                    // level and classes
                    for (int j = 5; j < rTeacher.Length; j++)
                    {
                        LevelAndClasses level = new LevelAndClasses();
                        string[] splitTab = rTeacher[j].Split(',');
                        level.Level = (Convert.ToInt32(splitTab[0]));
                        for (int k = 1; k < splitTab.Length; k++)
                        {
                            level.Classes.Add(Convert.ToInt32(splitTab[k]));
                        }

                        Levelandclasses.Add(level);
                    }
                    break;
                }



            }
            readTeacher.Close();

            Classes classe = new Classes();
            for (int i = 0; i < Levelandclasses.Count; i++)
            {
                for (int j = 0; j < Levelandclasses[i].Classes.Count; j++)
                {

                    classe = new Classes();
                    classe.Level = Levelandclasses[i].Level;
                    classe.ClassGroup = Levelandclasses[i].Classes[j];
                    StreamReader readStudent = new StreamReader(pathStudent);
                    string temp = "";
                    while (temp != null)
                    {
                        temp = readStudent.ReadLine();
                        if (temp == null) break;
                        string[] rstudent = temp.Split(';');
                        if (Convert.ToInt32(rstudent[8]) == Levelandclasses[i].Level && Convert.ToInt32(rstudent[9]) == Levelandclasses[i].Classes[j])
                        {
                            bool ctor = false;
                            Student student = new Student(ctor);
                            student.name = rstudent[0];
                            student.lastname = rstudent[1];
                            student.mail = rstudent[2];
                            student.StudentID = rstudent[3];
                            student.birthDate = rstudent[4];
                            student.Absences = Convert.ToInt32(rstudent[5]);
                            student.phone = rstudent[6];
                            if (rstudent[7] == "0") student.Tutor = false;
                            else student.Tutor = true;
                            student.GradeLevel = Convert.ToInt32(rstudent[8]);
                            student.Workgroup = Convert.ToInt32(rstudent[9]);
                            student.UnpaidFees = Convert.ToDouble(rstudent[10]);
                            student.NbSubject = Convert.ToInt32(rstudent[11]);
                            for (int l = 12; l < 12 + student.NbSubject; l++)
                            {
                                student.GradesTemp = student.GradesTemp + ";" + rstudent[l];
                            }
                            for (int l = 12; l < 12 + student.NbSubject; l++)
                            {
                                string[] splitTab = rstudent[l].Split(',');
                                if (rstudent[l].Length > 1)
                                {
                                    student.Courses.Add((Subject)Convert.ToInt32(splitTab[0]));
                                    int k = 0;
                                    while (k < splitTab.Length)
                                    {
                                        Exam exam = new Exam();
                                        exam.Sub = Convert.ToInt32(splitTab[k]);
                                        exam.Mark = Convert.ToDouble(splitTab[k + 1]);
                                        exam.Coef = Convert.ToDouble(splitTab[k + 2]);
                                        exam.Date = splitTab[k + 3];
                                        student.Grades.Add(exam);
                                        k = k + 4;
                                    }
                                }
                                else
                                {
                                    Subject adding = new Subject();
                                    adding = (Subject)Convert.ToInt32(Convert.ToInt32(rstudent[l]));
                                    student.Courses.Add(adding);
                                }
                            }
                            string tut = null;
                            if (student.Tutor == false) tut = "0";
                            else tut = "1";
                            student.Data = ($"{student.name};{student.lastname};{student.mail};{student.StudentID};{student.birthDate};{student.Absences};{phone};{tut};{student.GradeLevel};{student.Workgroup};{student.UnpaidFees};{student.NbSubject}{student.GradesTemp}");
                            classe.ClassStudent.Add(student);

                        }
                    }
                    readStudent.Close();
                    ClassesStudent.Add(classe);
                }
            }
            DesignationOfLevelAndClass();
            Console.Clear();

        }
        public FacilityMember(string login3) : base()
        {
            StreamReader reader = new StreamReader(pathAccessibilityLevel); // declaration of the reader and the link of the file
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] columns = temp.Split(';');
                if (columns[0] == login3)// comparison between the datas of the file and the data given by the user 
                {
                    password = columns[1];
                }

            }
            reader.Close();// closing of the streamreader


            for (int i = 0; i < 7; i++) disponibility[i] = new List<string>();
            ReadDispo();
            StreamReader readTeacher = new StreamReader(pathFacilityMember);
            string temp2 = "";
            while (temp2 != null)
            {
                temp2 = readTeacher.ReadLine();
                if (temp2 == null) break;
                string[] rTeacher = temp2.Split(';');
                if (rTeacher[2] == login3)
                {
                    this.name = rTeacher[0];
                    this.lastname = rTeacher[1];
                    this.login = rTeacher[2];
                    this.phone = rTeacher[3];
                    string[] rTeacher2 = rTeacher[4].Split(',');
                    // subjects taught
                    for (int i = 0; i < rTeacher2.Length; i++)
                    {
                        SubjectTaught.Add((Subject)Convert.ToInt32(rTeacher2[i]));
                        if (i == rTeacher2.Length - 1)
                        {
                            sT = sT + rTeacher2[i];
                        }
                        else sT = sT + rTeacher2[i] + ",";

                    }
                    Console.WriteLine(sT);

                    // level and classes
                    for (int j = 5; j < rTeacher.Length; j++)
                    {
                        LevelAndClasses level = new LevelAndClasses();
                        string[] splitTab = rTeacher[j].Split(',');
                        level.Level = (Convert.ToInt32(splitTab[0]));
                        for (int k = 1; k < splitTab.Length; k++)
                        {
                            level.Classes.Add(Convert.ToInt32(splitTab[k]));
                        }

                        Levelandclasses.Add(level);
                    }
                    break;
                }



            }
            readTeacher.Close();
            lAndC = null;
            for (int i = 0; i < levelandclasses.Count; i++)
            {
                lAndC = lAndC + levelandclasses[i].Level;
                for (int j = 0; j < levelandclasses[i].Classes.Count; j++)
                {
                    lAndC = lAndC + "," + levelandclasses[i].Classes[j];
                }
                if (i != levelandclasses.Count - 1) lAndC = lAndC + ";";


            }
            Console.WriteLine(lAndC);
        }

        /// <summary>
        /// The method that show the menu displayed on the temrinal.
        /// </summary>
        public void ExeFunctions()
        {
            string carryOn = "N";
            int compt = 0;
            while (carryOn == "N")
            {
                DisplayPersonalInfos();
                if (compt > 0) DesignationOfLevelAndClass();
                compt++;
                Console.WriteLine("1. Add new notes to a class\n" +
                    "2. Display tutor list\n" +
                    "3. Display Attendance of a student\n" +
                    "4. Display student results\n" +
                    "5. Display Exams of the year\n" +
                    "6. Add exam assignments\n" +
                    "7. Give absences to student\n" +
                    "8. Take off absences of a student\n" +
                    "9. Edit Disponibilities\n" +
                    "10. Display Disponibilities\n" +
                    "11. Display tutor information\n" +
                    "12. Mean of an exam\n" +
                    "13. Display the name of the students in the class\n" +
                    "14. Remove Grades\n" +
                    "15. Delate Exam\n"+
                    "16. Disconnect\n");

                int switchCase = Convert.ToInt32(Console.ReadLine());
                switch (switchCase)
                {
                    case 1:
                        AddResultsExam();
                        break;
                    case 2:
                        DisplayTutorList();
                        break;
                    case 3:
                        DisplayStudentClass();
                        Console.WriteLine("First name of the student");
                        string fN = Console.ReadLine();
                        Console.WriteLine("Last name of the student");
                        string lN = Console.ReadLine();
                        DisplayAttendancePerStudent(fN, lN);
                        break;
                    case 4:
                        DisplayStudentResults();
                        break;
                    case 5:
                        DisplayExams();
                        break;
                    case 6:
                        AddExamAssignment();
                        break;
                    case 7:
                        DisplayStudentClass();
                        GiveAbsenceToStudents();
                        break;
                    case 8:
                        TakeOffAbsences();
                        break;
                    case 9:
                        EditDispo();
                        break;
                    case 10:
                        ReadDispo();
                        DisplayDispo();
                        break;
                    case 11:
                        DisplayTutorList();
                        Console.WriteLine("First name of the tutor");
                        fN = Console.ReadLine();
                        Console.WriteLine("Last name of the tutor");
                        lN = Console.ReadLine();
                        DisplayTutorInfo(fN, lN);
                        break;
                    case 12:
                        DisplayStudentResults();
                        MeanExam();
                        break;
                    case 13:
                        DisplayStudentClass();
                        break;
                    case 14:
                        RemoveResultsExam();
                        break;
                    case 15:
                        Calendar.DeleteExamAssignment(level2,class2);
                        break;
                    case 16:
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
            }
        }

        /// <summary>
        /// It displays the level and the class of the student if there are students
        /// in the class.
        /// </summary>
        private void DisplayStudentClass()
        {
            if (Class.Count != 0)
            {
                foreach (Student stud in Class)
                {
                    Console.WriteLine(stud.name + " " + stud.lastname);
                }
            }
            else
            {
                Console.WriteLine("There are no students in this class");
            }

        }

        /// <summary>
        /// It displays all the informations about one person.
        /// </summary>
        public void DisplayPersonalInfos()
        {

            Console.WriteLine($"First Name: {name}\n" +
                $"Last Name: {lastname}\n" +
                $"phone number: {phone}\n" +
                $"email address: {login}\n" +
                $"subject(s) taught: {SubjectsTaught()}\n ");
        }

        /// <summary>
        /// The method scrolls through the list and then converts each element in
        /// string in order to have all the subjects in string on the variable 'sub'
        /// </summary>
        /// <returns>the names in string of all the subjects taught</returns>
        public string SubjectsTaught()
        {
            string sub = null;
            for (int i = 0; i < SubjectTaught.Count; i++)
            {
                sub = sub + " " + Convert.ToString(SubjectTaught[i]);
            }
            return sub;
        }

        /// <summary>
        /// The method scrolls through the list of subjectTaught and displays each
        /// one in the terminal in order to let choose the subject by the user. The
        /// user is asked to enter the date also. Then, it scrolls through the list
        /// Class in order to calculate the mean.
        /// </summary>
        public void MeanExam()
        {

            for (int i = 0; i < subjectTaught.Count; i++)
            {
                Console.Write(subjectTaught[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Choose the subject");
            string subject = Console.ReadLine();
            Console.WriteLine("Date ?");
            string date = Console.ReadLine();

            double sum = 0;
            int num = 0;
            for (int i = 0; i < Class.Count; i++)
            {
                for (int j = 0; j < Class[i].Grades.Count; j++)
                {

                    if (Class[i].Grades[j].Date == date && Convert.ToString((Subject)Class[i].Grades[j].Sub) == subject)
                    {

                        sum = sum + Class[i].Grades[j].Mark;
                        num++;
                    }
                }

            }
            double mean = sum / num;
            Console.WriteLine($"The mean of this exam is {mean}");

        }

        /// <summary>
        /// First, the method displays all the levels and classes for each level in
        /// order to ask the user the choice of the class and the level to modify.
        /// Then, the student is placed in the class and level choosen.
        /// </summary>
        private void DesignationOfLevelAndClass()
        {
            Class = new List<Student>();
            while (Class.Count == 0)
            {

                for (int i = 0; i < levelandclasses.Count; i++)
                {
                    Console.WriteLine("Level: " + levelandclasses[i].Level);
                    Console.Write("Classes: ");
                    for (int j = 0; j < levelandclasses[i].Classes.Count; j++)
                    {
                        Console.Write(levelandclasses[i].Classes[j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Choice of the class and level to modify");
                Console.WriteLine("Level ?");
                level2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("class ?");
                class2 = Convert.ToInt32(Console.ReadLine());
                for (int m = 0; m < ClassesStudent.Count; m++)
                {
                    if (ClassesStudent[m].Level == level2 && ClassesStudent[m].ClassGroup == class2)
                    {
                        for (int i = 0; i < ClassesStudent[m].ClassStudent.Count; i++)
                        {
                            Class.Add(ClassesStudent[m].ClassStudent[i]);
                        }
                    }
                }
                if (Class.Count == 0)
                {
                    Console.WriteLine("Enter another level and/or class, there is no student assigned to this class");
                }
            }
        }

        /// <summary>
        /// It displays if there is a tutor in the class or not.
        /// </summary>
        private void DisplayTutorList()
        {
            bool test = true;
            foreach (Student student in Class)
            {
                if (student.Tutor == true) Console.WriteLine(student.name + " " + student.lastname); test = false;
            }
            if (test == true) Console.WriteLine("No student in this class is a tutor");
            Console.WriteLine();
        }

        /// <summary>
        /// It displays if the choosen student is a tutor or not.
        /// </summary>
        /// <param name="firstName">first name of the student</param>
        /// <param name="lastName">last name of the student</param>
        private void DisplayTutorInfo(string firstName, string lastName)
        {
            foreach (Student student in Class)
            {
                if (student.Tutor == true && student.name == firstName && student.lastname == lastName) student.DisplayPersonalInfos();
                if (student.name == firstName && student.lastname == lastName) Console.WriteLine("This student is not a tutor.");
            }
        }

        /// <summary>
        /// It displays if a student has been absent and how many times he has been
        /// absent.
        /// </summary>
        /// <param name="name">name of the student</param>
        /// <param name="lastName">last name of the student</param>
        private void DisplayAttendancePerStudent(string name, string lastName)
        {
            foreach (Student stud in Class)
            {
                if (stud.name == name && stud.lastname == lastName)
                {
                    Console.WriteLine($"The student {stud.lastname} {stud.name} has been absent {stud.Absences} times");
                }

            }

        }

        /// <summary>
        /// It displays all the grades and informations about them of each student
        /// in the class.
        /// </summary>
        private void DisplayStudentResults()
        {
            foreach (Student stud in Class)
            {
                Console.WriteLine(stud.name + " " + stud.lastname + ":");
                if (stud.Grades.Count == 0) Console.WriteLine("There are no results for this student");
                foreach (Exam exam in stud.Grades)
                {
                    Console.WriteLine($"subject:{(Subject)exam.Sub} Mark:{exam.Mark} coef:{exam.Coef} date:{exam.Date}");
                }
                Console.WriteLine();
            }


        }

        /// <summary>
        /// It displays all the exams for each class for each level.
        /// </summary>
        public void DisplayExams()
        {
            for (int i = 0; i < Levelandclasses.Count; i++)
            {
                for (int j = 0; j < Levelandclasses[i].Classes.Count; j++)
                {
                    Console.Write("Level " + Levelandclasses[i].Level + " ");
                    Console.WriteLine("Class " + Levelandclasses[i].Classes[j]);
                    Calendar.ReadExamAssignment(Levelandclasses[i].Classes[j], Levelandclasses[i].Level);
                    Console.WriteLine();

                }
            }
        }

        /// <summary>
        /// The method asks the user the choice of the subject, the exact date of
        /// when the exam will be and its description to add it in to the calendar.
        /// </summary>
        public void AddExamAssignment()
        {
            string[] EnumTab = Enum.GetNames(typeof(Subject));
            Console.Write("Choose the subject of the assignment ");
            for (int i = 0; i < EnumTab.Length; i++)
            {
                Console.Write(EnumTab[i] + " ");
            }
            Console.WriteLine();
            string subject = Console.ReadLine();
            Console.WriteLine("Year ?");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Month ?");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Day ?");
            int day = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hour ?");
            int hour = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Minute ?");
            int minute = Convert.ToInt32(Console.ReadLine());
            DateTime date = new DateTime(year, month, day, hour, minute, 00);
            Console.WriteLine("Description of the exam");
            string description = Console.ReadLine();

            Calendar.AddExamAssignment(subject, date, description, class2, level2);
        }

        private string DisplayAdminInfos(Admin admin)
        {
            return admin.ToString();
        }

        /// <summary>
        /// First, the user has to choose the subject of the exam (he is asked
        /// while he enters a wrong one), he has to enter the coefficient of the
        /// exam (he is asked while the coefficient is not correct), he has to
        /// enter the date which is tested if it is in the correct format and
        /// reasonable. Then, the methods scrolls through the list class to ask
        /// the user to enter the grade for each student of the class (the grade
        /// is tested if it is in the correct format).
        /// At least, grades are aded to the student file.
        /// </summary>
        public void AddResultsExam()
        {

            string[] tabEnum = Enum.GetNames(typeof(Subject));
            Exam exam = new Exam();
            //string coursesAvailable = null;
            string[] test = Enum.GetNames(typeof(Subject));
            /*
            for (int index = 0; index < test.Length; index++)
            {
                coursesAvailable = (coursesAvailable + " " + test[index]);
            }*/

            Console.WriteLine("Enter the subject : " + SubjectsTaught());
            string subjects = Console.ReadLine();
            if (SubjectsTaught().Contains(subjects) == false)
            {
                while (SubjectsTaught().Contains(subjects) == false)
                {
                    Console.WriteLine("Please enter a correct subject : " + SubjectsTaught());
                    subjects = Console.ReadLine();
                }
            }
            int k = 0;
            for (; k < tabEnum.Length; k++)
            {
                if (subjects == tabEnum[k])
                {
                    exam.Sub = k;
                    break;
                }
            }

            Console.Write("Enter the coefficient : ");
            bool cTest = false;
            double Coef = -1;
            while (cTest == false)
            {
                try
                {
                    Coef = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception)
                {
                    ;
                    cTest = false;
                }
                if (Coef >= 0) break;
                else Console.WriteLine("Please enter a correct coeficient");
            }

            Console.WriteLine("Enter the date of the exam : dd/mm/yyyy european date format");
            string Date = Console.ReadLine();
            bool check = false;
            while (check == false)
            {
                check = true;
                char[] tabAut = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/' };
                bool num = false;
                int checkSlash = 0;
                for (int i = 0; i < Date.Length; i++)
                {
                    num = false;
                    for (int j = 0; j < tabAut.Length; j++)
                    {
                        if (Date[i] == tabAut[j]) num = true;
                    }
                    if (Date[i] == '/') checkSlash++;
                    if (num == false) check = false;

                }
                if (checkSlash != 2) check = false;

                if (Date.Length > 5 && check == true)
                {
                    // add a zero if you don't put a zero before your number. ex: 1/1/2000 becomes 01/01/2000
                    int decal = 0;
                    string nDate = null;
                    if (Date[2] != '/' || Date[5] != '/')
                    {
                        for (int i = 0; i < birthDate.Length; i++)
                        {
                            if (birthDate[i] == '/')
                            {
                                if (i == 1) nDate = "0";
                                if (i == 3) nDate = nDate + birthDate[0] + "/" + "0" + birthDate[i - 1]; decal = 1;
                                if (i == 2) nDate = nDate + birthDate[0] + birthDate[1] + birthDate[2] + "0"; i = 3; decal = 1;
                                if (i == 1 && birthDate[4] == '/') nDate = "0" + birthDate[0]; decal = 1;
                            }
                            if (decal == 1) nDate = nDate + birthDate[i];
                        }
                        birthDate = nDate;
                    }

                    if (Date.Length != 10) check = false;
                    if (Date[2] != '/') check = false;
                    if (Date[5] != '/') check = false;
                    if (check == true)
                    {
                        // check if the year, month and day are reasonable
                        string[] tabDate = Date.Split('/');
                        int day = Convert.ToInt32(tabDate[0]);
                        int month = Convert.ToInt32(tabDate[1]);
                        int year = Convert.ToInt32(tabDate[2]);

                        if (year < 1901) check = false;
                        if (year > DateTime.Today.Year) check = false;

                        if (month < 1 || month > 12) check = false;

                        if (check == true)
                        {
                            int[] daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                            int[] daysInMonthBissextile = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                            bool biss = DateTime.IsLeapYear(year);
                            if (biss == true) if (day < 1 || day > daysInMonthBissextile[month - 1]) check = false;
                                else if (day < 1 || day > daysInMonth[month - 1]) check = false;
                        }
                    }
                }
                else check = false;
                if (check == false)
                {
                    Console.WriteLine("The exam date has an input error. ");
                    Console.WriteLine("Exam date ? dd/mm/yyyy");
                    Date = Console.ReadLine();
                }
            }


            for (int j = 0; j < Class.Count; j++)
            {
                if (CheckCourses((Subject)k, Class[j]) == true)
                {
                    exam = new Exam();
                    exam.Coef = Coef;
                    exam.Date = Date;
                    exam.Sub = k;
                    Console.Write($"Enter the mark of {Class[j].lastname} {Class[j].name} : ");
                    bool mTest = false;
                    double mark = -1;
                    while (mTest == false)
                    {
                        try
                        {
                            mark = Convert.ToDouble(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            cTest = false;
                        }
                        if (mark >= 0) break;
                        else Console.WriteLine("Please enter a correct mark");
                    }
                    exam.Mark = mark;
                    Class[j].Grades.Add(exam);
                }
            }

            AddGradesToStudentFile((Subject)exam.Sub);
        }

        public void RemoveResultsExam()
        {
            string[] tabEnum = Enum.GetNames(typeof(Subject));
            int Sub = 0;
            //string coursesAvailable = null;
            string[] test = Enum.GetNames(typeof(Subject));
            /*
            for (int index = 0; index < test.Length; index++)
            {
                coursesAvailable = (coursesAvailable + " " + test[index]);
            }*/

            Console.WriteLine("Enter the subject : " + SubjectsTaught());
            string subjects = Console.ReadLine();
            if (SubjectsTaught().Contains(subjects) == false)
            {
                while (SubjectsTaught().Contains(subjects) == false)
                {
                    Console.WriteLine("Please enter a correct subject : " + SubjectsTaught());
                    subjects = Console.ReadLine();
                }
            }
            int k = 0;
            for (; k < tabEnum.Length; k++)
            {
                if (subjects == tabEnum[k])
                {
                    Sub = k;

                    break;
                }
            }


            Console.WriteLine("Enter the date of the exam : dd/mm/yyyy european date format");
            string Date = Console.ReadLine();
            bool check = false;
            while (check == false)
            {
                check = true;
                char[] tabAut = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/' };
                bool num = false;
                int checkSlash = 0;
                for (int i = 0; i < Date.Length; i++)
                {
                    num = false;
                    for (int j = 0; j < tabAut.Length; j++)
                    {
                        if (Date[i] == tabAut[j]) num = true;
                    }
                    if (Date[i] == '/') checkSlash++;
                    if (num == false) check = false;

                }
                if (checkSlash != 2) check = false;

                if (Date.Length > 5 && check == true)
                {
                    // add a zero if you don't put a zero before your number. ex: 1/1/2000 becomes 01/01/2000
                    int decal = 0;
                    string nDate = null;
                    if (Date[2] != '/' || Date[5] != '/')
                    {
                        for (int i = 0; i < birthDate.Length; i++)
                        {
                            if (birthDate[i] == '/')
                            {
                                if (i == 1) nDate = "0";
                                if (i == 3) nDate = nDate + birthDate[0] + "/" + "0" + birthDate[i - 1]; decal = 1;
                                if (i == 2) nDate = nDate + birthDate[0] + birthDate[1] + birthDate[2] + "0"; i = 3; decal = 1;
                                if (i == 1 && birthDate[4] == '/') nDate = "0" + birthDate[0]; decal = 1;
                            }
                            if (decal == 1) nDate = nDate + birthDate[i];
                        }
                        birthDate = nDate;
                    }

                    if (Date.Length != 10) check = false;
                    if (Date[2] != '/') check = false;
                    if (Date[5] != '/') check = false;
                    if (check == true)
                    {
                        // check if the year, month and day are reasonable
                        string[] tabDate = Date.Split('/');
                        int day = Convert.ToInt32(tabDate[0]);
                        int month = Convert.ToInt32(tabDate[1]);
                        int year = Convert.ToInt32(tabDate[2]);

                        if (year < 1901) check = false;
                        if (year > DateTime.Today.Year) check = false;

                        if (month < 1 || month > 12) check = false;

                        if (check == true)
                        {
                            int[] daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                            int[] daysInMonthBissextile = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                            bool biss = DateTime.IsLeapYear(year);
                            if (biss == true) if (day < 1 || day > daysInMonthBissextile[month - 1]) check = false;
                                else if (day < 1 || day > daysInMonth[month - 1]) check = false;
                        }
                    }
                }
                else check = false;
                if (check == false)
                {
                    Console.WriteLine("The exam date has an input error. ");
                    Console.WriteLine("Exam date ? dd/mm/yyyy");
                    Date = Console.ReadLine();
                }
            }


            for (int j = 0; j < Class.Count; j++)
            {
                if (CheckCourses((Subject)k, Class[j]) == true)
                {
                    for (int l = 0; l < Class[j].Grades.Count; l++)
                    {
                        if (Class[j].Grades[l].Date == Date && Class[j].Grades[l].Sub == Sub)
                        {
                            Class[j].Grades.RemoveAt(l);
                        }
                    }
                }
            }

            AddGradesToStudentFile((Subject)Sub);
        }

        /// <summary>
        /// The method modifies the student list and is used in 'AddResultsExam()'.
        /// First, the list of the students who have grades added is initialized.
        /// The method scrolls through the list Class in order to add in the tab
        /// tabexam all the exam for each student and then, sort the tab. Here are
        /// 2 cases: the student has one exam in his tab or he has more than one.
        /// First case: the method adds the number of the course that hasn't been
        /// modified and tabEnum contains all the data that can be found in the
        /// enum class but under the form of a string.
        /// Second case: there are 2 possibilities: the exam is in the same subject
        /// than the next exam in the tabExam or not.
        /// Then, there is the modification of the personal data. The method reads
        /// all the student file and adds the data of the student who are not in
        /// the workgroup chosen in the constructor. After the streamreader is
        /// closed, the method adds all the students who are in the same workgroup
        /// but who have not the subject.
        /// Then, the file student is deleted.
        /// </summary>
        /// <param name="sub"></param>
        public void AddGradesToStudentFile(Subject sub)//modify the student list, need to be used after AddResultsExam()
        {
            List<Student> listStudent = new List<Student>();// list of the students who have grades added
            for (int i = 0; i < Class.Count; i++)
            {
                if (CheckCourses(sub, Class[i]) == true)
                {
                    Exam[] tabExam = new Exam[Class[i].Grades.Count];
                    for (int j = 0; j < Class[i].Grades.Count; j++)//add in tabexam all the exam of the student
                    {
                        tabExam[j] = Class[i].Grades[j];
                    }
                    Sort(tabExam);//sort the tabExam 

                    string grades = null;
                    // 2 cases: the student has 1 exam in his tab or he has more than one
                    if (tabExam.Length == 0)
                    {
                        for (int j = 0; j < Class[i].Courses.Count; j++)
                        {
                            if (j != Class[i].Courses.Count - 1)
                            {
                                grades = grades + Class[i].Courses[j].GetHashCode() + ";";
                            }
                            else grades = grades + Class[i].Courses[j].GetHashCode();
                        }
                    }
                    else if (tabExam.Length == 1)
                    {
                        grades = grades + tabExam[0].ToString();
                        if (Class[i].Grades.Count > 1)
                        {
                            for (int j = 0; j < Class[i].Courses.Count; j++)
                            {
                                if (Class[i].Courses[j] != (Subject)tabExam[0].Sub)
                                {
                                    if (j != Class[i].Courses.Count - 1)
                                    {
                                        grades = grades + Class[i].Courses[j].GetHashCode() + ";";
                                    }
                                    else grades = grades + Class[i].Courses[j].GetHashCode();
                                }
                            }
                        }
                    }
                    else
                    {
                        grades = tabExam[0].ToString();
                        if (tabExam[0].Sub == tabExam[1].Sub) grades = grades + ",";
                        else grades = grades + ";";
                        for (int k = 1; k < tabExam.Length - 1; k++)
                        {
                            // 2 possibilities: the exam is in the same subject than the next exam in the tabExam or it is not.
                            if (tabExam[k].Sub == tabExam[k + 1].Sub) grades = grades + tabExam[k].ToString() + ",";
                            else grades = grades + tabExam[k].ToString() + ";";

                        }
                        grades = grades + tabExam[tabExam.Length - 1].ToString();

                        // add courses that hace not been used yet
                        List<int> save = new List<int>();
                        for (int k = 0; k < tabExam.Length - 1; k++)
                        {
                            if (k == 0)
                            {
                                save.Add(tabExam[0].Sub);
                            }
                            if (tabExam[k].Sub != tabExam[k + 1].Sub)
                            {
                                save.Add(tabExam[k + 1].Sub);
                            }
                        }
                        for (int k = 0; k < Class[i].Courses.Count; k++)
                        {
                            int compteur = 0;
                            for (int l = 0; l < save.Count; l++)
                            {
                                if (Class[i].Courses[k] == (Subject)save[l]) compteur++;
                            }
                            if (compteur == 0) grades = grades + ";" + Class[i].Courses[k].GetHashCode();
                        }
                    }
                    string tutor = null;
                    if (Class[i].Tutor == false) tutor = "0";
                    else tutor = "1";
                    Class[i].Data = ($"{Class[i].name};{Class[i].lastname};{Class[i].mail};{Class[i].StudentID};{Class[i].birthDate};{Class[i].Absences};{Class[i].phone};{tutor};{Class[i].GradeLevel};{Class[i].Workgroup};{Class[i].UnpaidFees};{Class[i].NbSubject}");
                    Class[i].Data = Class[i].Data + ";" + grades;
                    listStudent.Add(Class[i]);
                }
            }

            //modification of the personal data
            // read all the student file and add the data of the student who are not in the workgroup chosen in the constructor
            StreamReader reader = new StreamReader(pathStudent);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] comparison = temp.Split(';');

                bool test = false;
                for (int i = 0; i < listStudent.Count; i++)
                {
                    if (listStudent[i].name == comparison[0] && listStudent[i].lastname == comparison[1])
                    {
                        test = true;
                    }
                }
                if (test == false)
                {
                    tab.Add(temp);
                }

            }
            reader.Close();


            for (int j = 0; j < listStudent.Count; j++)// add all the students who are in the same workgroup but who have not the subject
            {
                tab.Add(listStudent[j].Data);
            }

            File.Delete(pathStudent);// delete the file student

            FileStream stream = new FileStream(pathStudent, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);
                    //Console.WriteLine("passe2");
                }
            }
            stream.Dispose();

        }

        /// <summary>
        /// It sorts a choosen array from the minimum to the maximum.
        /// </summary>
        /// <param name="tab">the array to sort</param>
        public static void Sort(Exam[] tab)// sort from the min to the the max
        {
            for (int i = 0; i < tab.Length - 1; i++)
            {
                int minIndex = i; int minValue = tab[i].Sub;
                for (int j = i + 1; j < tab.Length; j++)
                {
                    if (tab[j].Sub.CompareTo(minValue) < 0)
                    {
                        minIndex = j; minValue = tab[j].Sub;
                    }
                }
                Swap(tab, i, minIndex);
            }


        }

        /// <summary>
        /// It swaps to element in an array.
        /// </summary>
        /// <param name="tab">the array where elements have to be swapped</param>
        /// <param name="first">first element to swap</param>
        /// <param name="second">second element to swap</param>
        private static void Swap(Exam[] tab, int first, int second)
        {
            Exam temp = tab[first];
            tab[first] = tab[second];
            tab[second] = temp;
        }

        /// <summary>
        /// It checks if the student has a choosen course in his course list or not.
        /// </summary>
        /// <param name="subject">the subject to test</param>
        /// <param name="student">the student with his course list</param>
        /// <returns></returns>
        public bool CheckCourses(Subject subject, Student student)// check if a student has this course in his course list
        {
            bool check = false;
            for (int i = 0; i < student.Courses.Count; i++)
            {
                if (subject == student.Courses[i])
                {
                    check = true;
                }
            }
            return check;
        }

        /// <summary>
        /// The method asks the user the name of the absent student in order to
        /// add an absence to this student. There is a test of if the student is
        /// in the class.
        /// </summary>
        private void GiveAbsenceToStudents()
        {
            Console.WriteLine("who is absent ? ");
            Console.WriteLine("lastname : ");
            string forenameAbs = Console.ReadLine();
            Console.WriteLine("name : ");
            string nameAbs = Console.ReadLine();
            bool test = false;
            foreach (Student student in Class)
            {
                if (student.lastname == forenameAbs && student.name == nameAbs)
                {
                    student.Absences += 1;
                    student.ModifyDataStudent();

                    test = true;
                }
            }
            if (test == false) Console.WriteLine("the student has not been found");
        }

        /// <summary>
        /// The method asks user name of the student and the number of absences
        /// he wants to take off absences. There is a test of if the student is
        /// in the class.
        /// </summary>
        private void TakeOffAbsences()
        {

            Console.WriteLine("last name : ");
            string lastNameAbs = Console.ReadLine();
            Console.WriteLine("first name : ");
            string nameAbs = Console.ReadLine();
            Console.WriteLine("Number of absences you want to take off");
            int absences = Convert.ToInt32(Console.ReadLine());
            bool test = false;
            foreach (Student student in Class)
            {
                if (student.lastname == lastNameAbs && student.name == nameAbs)
                {
                    if (student.Absences - absences > -1)
                    {
                        student.Absences = student.Absences - absences;
                        student.ModifyDataStudent();
                    }
                    else Console.WriteLine("The number of absences you want to take off is impossible.");
                    test = true;
                }
            }
            if (test == false) Console.WriteLine("the student has not been found");

        }

        /// <summary>
        /// It edits every informations about each student of the class.
        /// </summary>
        public void EditStudents()
        {
            string line = "";
            foreach (Student student in Class)
            {
                line = ($"{student.lastname};{student.name};{student.StudentID};{student.birthDate};{student.Absences};{student.mail};{student.phone};{student.Tutor};{student.GradeLevel};{student.Workgroup};");


            }

        }

        public List<string>[] ReadDispo()
        {
            string disponibilities = "";
            StreamReader dispoReader = new StreamReader(PathDispo);
            string line = "";
            while (line != null)
            {
                line = dispoReader.ReadLine();
                if (line == null | line == "") break;
                if (line.Split(';')[0] == name & line.Split(';')[1] == lastname)
                {
                    disponibilities = line;
                    break;
                }
            }
            dispoReader.Close();
            for (int i = 0; i < disponibilities.Split(';').Length; i++)         //transtiping to List<string>[]
            {
                List<string> tempList = new List<string>();
                foreach (string st in disponibilities.Split(';')[i].Split(',')) tempList.Add(st);
                tempList.RemoveAll(item => item == "");         //removes the empty strings that appears if there is a day witout disponibilities
                disponibility[i] = tempList;
            }
            return disponibility;
        }

        /// <summary>
        /// It displays a menu in order to ask the user his disponibility of day
        /// and time. The time slot entered by the user is tested in order to have
        /// a correct format. Then, the method adds the time slot at the correct
        /// position. There are 3 cases, when the time slot is already entered (so
        /// the time slot is not filled), when the time entered is before another
        /// (the time slot is filled) and when everything is alright (the time
        /// slot is filled). Finally, it asks the user his choice about adding
        /// another disponibility on the same day or on another day.
        /// </summary>
        public void AddDispo()
        {
            string test = "yes";
            while (test == "yes")
            {
                Console.Clear();
                DisplayDispo();
                string dtest = "yes";
                Console.WriteLine("Which day are you disponible ?");
                Console.WriteLine("Monday : type 1");
                Console.WriteLine("Tuesday : type 2");
                Console.WriteLine("Wednesday : type 3");
                Console.WriteLine("Thursday : type 4");
                Console.WriteLine("Friday : type 5");
                int day = Convert.ToInt32(Console.ReadLine());
                while (dtest == "yes")
                {
                    Console.Write("Enter a time slot (ex 09:30-11:00) : ");
                    string slot = Console.ReadLine();
                    bool stest = false;
                    string numbers = "0123456789";
                    int[] positions = { 0, 1, 3, 4, 6, 7, 9, 10 };
                    while (stest == false)                         //check if the slot format is correct
                    {
                        bool test1 = false;
                        bool test2 = false;
                        bool test3 = false;
                        if (slot.Length == 11)
                        {
                            if (slot[5] == '-') test1 = true;
                            if (slot[2] == ':' & slot[8] == ':') test2 = true;
                            int compt = 0;
                            foreach (int p in positions)
                            {
                                if (numbers.Contains(slot[p])) compt++;
                            }
                            if (compt == 8) test3 = true;
                            if (test1 == true & test2 == true & test3 == true) stest = true;
                            else
                            {
                                Console.Write("please enter a correct time slot (ex 09:30-11:00) : ");
                                slot = Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Write("please enter a correct time slot (ex 09:30-11:00) : ");
                            slot = Console.ReadLine();
                        }
                    }
                    if (disponibility[day + 1].Count == 0) { disponibility[day + 1].Add(slot); }
                    else
                    {
                        for (int compt = 0; compt < disponibility[day + 1].Count; compt++)          //Adds the time slot at the correct position
                        {
                            if (slot.CompareTo(disponibility[day + 1][compt]) == 0)              //case if the time slot is already entered
                            {
                                Console.WriteLine("the time slot is already filled");
                                break;
                            }
                            if ((slot.CompareTo(disponibility[day + 1][compt]) < 0))               //case if the time entered is before another
                            {
                                disponibility[day + 1].Insert(compt, slot);
                                break;
                            }
                            if ((slot.CompareTo(disponibility[day + 1][compt]) > 0 & compt == disponibility[day + 1].Count - 1))
                            {
                                disponibility[day + 1].Insert(compt + 1, slot);
                                break;
                            }
                        }
                        Console.Clear();
                        DisplayDispo();
                    }
                    Console.WriteLine("Do you want to add another disponibility this day ?");    //while loop exit
                    Console.Write("type yes or no : ");
                    dtest = Console.ReadLine();
                }
                Console.WriteLine("Do you want to add disponibilities on another day ?");       //while loop exit
                Console.Write("type yes or no : ");
                test = Console.ReadLine();
            }
        }

        /// <summary>
        /// It displays a menu in order to ask the user his disponibility he wants
        /// to remove. The user is asked about the day and the time slot he wants
        /// to remove. The time slot is tested, if it exists it is removed, else
        /// it displays that this disponibility does not exist. Finally, it asks
        /// the user his choice about removing another disponibility on the same
        /// day or on another day.
        /// </summary>
        public void RemoveDispo()
        {
            string test = "";
            while (test != "no")
            {
                Console.Clear();
                DisplayDispo();
                string dtest = "";
                Console.WriteLine("Which day are you no longer disponible ?");
                Console.WriteLine("Monday : type 1");
                Console.WriteLine("Tuesday : type 2");
                Console.WriteLine("Wednesday : type 3");
                Console.WriteLine("Thursday : type 4");
                Console.WriteLine("Friday : type 5");
                int day = Convert.ToInt32(Console.ReadLine());
                while (dtest != "no")
                {
                    Console.Write("Enter the time slot you wish to remove (ex 09:30-11:00) : ");
                    string slot = Console.ReadLine();
                    if (disponibility[day + 1].Contains(slot)) disponibility[day + 1].Remove(slot); //remove an instance of the slot entered if it exists
                    else Console.WriteLine("No matching time slot found this day.");
                    Console.Clear();
                    DisplayDispo();
                    Console.WriteLine("Do you want to remove another disponibility this day ?");    //while loop exit
                    while (dtest != "yes" & dtest != "no")
                    {
                        Console.Write("type yes or no : ");
                        dtest = Console.ReadLine();
                    }
                    if (dtest == "no") break;
                }
                Console.WriteLine("Do you want to remove disponibilities on another day ?");       //while loop exit
                while (test != "yes" & test != "no")
                {
                    Console.Write("type yes or no : ");
                    test = Console.ReadLine();
                }
                if (test == "no") break;
            }
        }

        /// <summary>
        /// It displays the disponibility.
        /// </summary>
        public void DisplayDispo()
        {
            string days = "Monday          Tuesday         Wednesday       Thursday        Friday";
            string space1 = "           ";
            string space2 = "     ";
            string line = "";
            //Console.Write(disponibility[0][0]);
            //Console.WriteLine(disponibility[1][0]);
            Console.WriteLine(days);
            for (int i = 0; i < 8; i++)
            {
                line = "";
                for (int j = 2; j < 7; j++)
                {
                    if (disponibility[j].Count >= i + 1) line += disponibility[j][i] + space2;
                    else line += space1 + space2;
                }
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// The method write the disponibility in the file.
        /// </summary>
        public void WriteDispo()
        {
            string disponibilities = name + ";" + lastname;
            for (int i = 1; i < 6; i++)         //transtype to string
            {

                disponibilities += ";";
                if (disponibility[i + 1].Count != 0)
                {
                    disponibilities += disponibility[i + 1][0];
                    foreach (string slots in disponibility[i + 1].Skip(1))
                    {
                        disponibilities += ",";
                        disponibilities += slots;
                    }
                }
            }
            StreamReader streamReader = new StreamReader(PathDispo);
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
                if (lines[i].Contains($"{name};{lastname}") == true)
                {
                    lines[i] = disponibilities;
                    check = false;
                }
            }
            if (check == true)
            {
                Console.WriteLine("passe2");
                Console.WriteLine(disponibilities);
                lines.Add(disponibilities);
            }
            File.Delete(PathDispo);
            FileStream stream = new FileStream(PathDispo, FileMode.OpenOrCreate);
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                foreach (string l in lines)
                {
                    streamWriter.WriteLine(l);
                }
            }
            stream.Dispose();
        }

        /// <summary>
        /// It displays a menu in order to ask the user if he would likes to add
        /// or remove disponibilities.
        /// </summary>
        public void EditDispo()
        {
            disponibility = ReadDispo();


            string test1 = "";
            while (test1 != "3")
            {
                Console.Clear();
                DisplayDispo();
                Console.WriteLine("What do you want to do ?");
                Console.WriteLine("1 : Add a disponibility");
                Console.WriteLine("2 : Remove a disponibility");
                Console.WriteLine("3 : Nothing");
                test1 = Console.ReadLine();
                if (test1 == "3") break;
                if (test1 == "1")
                {
                    AddDispo();
                }
                if (test1 == "2")
                {
                    RemoveDispo();
                }
            }
            WriteDispo();
        }
    }
}
