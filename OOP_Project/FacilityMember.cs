using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    class FacilityMember : User, IPersonalInformations
    {
        public string name { get ; set ; }
        public string lastname { get ; set ; }
        public string mail { get ; set ; }
        public string phone { get ; set ; }
        public string birthDate { get ; set ; }
        public int numLevel { get; set; }

        public List<string>[] disponibility { get; set; }

        public Subject SubjectTaugth { get; set; }
        public List<Student> Class { get; set; }
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
                    SubjectTaugth = (Subject)Convert.ToInt32(columns[4]);
                    //classe = columns[5]; // problem with the data base. Impossibility to stock data in a data base with a string []

                }
            }
            reader.Close();// closing of the streamreader
        }
        public FacilityMember(Subject subjectTaugth, int numClass, int numLevel)
        {
            this.SubjectTaugth = subjectTaugth;
            this.NumClass = numClass;
            this.numLevel = numLevel;
            StreamReader readStudent = new StreamReader(pathStudent);
            string temp = "";
            while (temp != null)
            {
                temp = readStudent.ReadLine();
                if (temp == null) break;
                string[] rstudent = temp.Split(';');
                if (Convert.ToInt32(rstudent[9]) == NumClass && Convert.ToInt32(rstudent[8]) == numLevel)
                {
                    Student student = new Student();
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

                    for (int j = 12; j < 12 + student.NbSubject; j++)
                    {

                        string[] splitTab = rstudent[j].Split(',');
                        if (splitTab.Length > 1)
                        {
                            student.Courses.Add((Subject)Convert.ToInt32(splitTab[0]));
                            int k = 0;
                            Exam exam = new Exam();
                            exam.Sub = Convert.ToInt32(splitTab[k]);
                            exam.Mark = Convert.ToDouble(splitTab[k + 1]);
                            exam.Coef = Convert.ToDouble(splitTab[k + 2]);
                            exam.Date = splitTab[k + 3];
                            student.Grades.Add(exam);

                        }
                        else
                        {
                            Subject adding = new Subject();
                            adding = (Subject)Convert.ToInt32(splitTab[0]);
                            Console.WriteLine(adding);
                            student.Courses.Add(adding);

                        }
                    }
                    Class.Add(student);
                }
            }
            readStudent.Close();
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
        private void DisplayAttendancePerStudent(Student student)
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
        public void AddResultsExam()
        {
            string[] tabEnum = Enum.GetNames(typeof(Subject));
            Exam exam = new Exam();
            string coursesAvailable = null;
            string[] test = Enum.GetNames(typeof(Subject));
            for (int index = 0; index < test.Length; index++)
            {
                coursesAvailable = (coursesAvailable + " " + test[index]);
            }
            Console.WriteLine("Enter the subject : " + coursesAvailable);
            string subjects = Console.ReadLine();
            for (int i = 0; i < tabEnum.Length; i++)
            {
                if (subjects == tabEnum[i])
                {
                    exam.Sub = i;
                }
            }

            Console.Write("Enter the coefficient : ");
            exam.Coef = Convert.ToDouble(Console.ReadLine());
            double mark = new double();
            Console.Write("Enter the date of the exam : ");
            exam.Date = Console.ReadLine();

            foreach (Student student in Class)
            {
                if (CheckCourses((Subject)exam.Sub, student) == true)
                {
                    Console.Write($"Enter the mark of {student.lastname} {student.name} : ");
                    exam.Mark = Convert.ToDouble(Console.ReadLine());
                    student.Grades.Add(exam);
                }
            }

            AddGradesToStudentFile((Subject)exam.Sub);
        }
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
                    string grades = Convert.ToString(tabExam[0].Sub);
                    // 2 cases: the student has 1 exam in his tab or he has more than one
                    if (tabExam.Length == 1)
                    {
                        grades = grades + "," + tabExam[0].ToString();

                        for (int j = 0; j < Class[i].Courses.Count; j++)// add the number of the course that hasn't been modified
                        {
                            if (Class[i].Courses[j] != (Subject)tabExam[0].Sub)
                            {

                                string[] tabEnum = Enum.GetNames(typeof(Subject));// tabEnum contains all the data that can be found in the enum class but under the form of a string
                                for (int k = 0; k < tabEnum.Length; k++)
                                {
                                    if (Class[i].Courses[j] == (Subject)k)
                                    {
                                        grades = grades + ";" + k;
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k < tabExam.Length - 1; k++)
                        {
                            // 2 possibilities: the exam is in the same subject than the next exam in the tabExam or it is not.
                            if (tabExam[k].Sub == tabExam[k + 1].Sub) grades = grades + "," + tabExam[k].ToString();
                            else grades = grades + "," + tabExam[k].ToString() + ";" + Convert.ToString(tabExam[k + 1].Sub);

                        }
                        grades = grades + "," + tabExam[tabExam.Length - 1].ToString();


                        List<int> save = new List<int>();
                        for (int j = 0; j < Class[i].Courses.Count; j++)
                        {
                            bool check2 = true;
                            for (int l = 0; l < tabExam.Length; l++)
                            {
                                if (Class[i].Courses[j] == (Subject)tabExam[l].Sub) check2 = false;
                            }
                            if (check2 == true)
                            {
                                string[] tabEnum = Enum.GetNames(typeof(Subject));
                                for (int k = 0; k < tabEnum.Length; k++)
                                {
                                    if (Class[i].Courses[j] == (Subject)k)
                                    {
                                        if (save.Count == 0)
                                        {
                                            save.Add(k);
                                            grades = grades + ";" + k;
                                            break;
                                        }
                                        else
                                        {
                                            bool check = true;
                                            for (int m = 0; m < save.Count; m++)
                                            {
                                                if (k == save[m]) check = false;
                                            }
                                            if (check == true) grades = grades + ";" + k;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string tutor = null;
                    if (Class[i].Tutor == false) tutor = "0";
                    else tutor = "1";
                    Class[i].Data = ($"{Class[i].name};{Class[i].lastname};{Class[i].mail};{Class[i].StudentID};{Class[i].birthDate};{Class[i].Absences};{Class[i].phone};{tutor};{Class[i].GradeLevel};{Class[i].Workgroup};{Class[i].UnpaidFees};{Class[i].NbSubject}");
                    Class[i].Data = Class[i].Data + ";" + grades;
                    Console.WriteLine(Class[i].Data);
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

        private static void Swap(Exam[] tab, int first, int second)
        {
            Exam temp = tab[first];
            tab[first] = tab[second];
            tab[second] = temp;
        }

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
        public void ReadDispo()
        {
            string disponibilities = "";
            StreamReader dispoReader = new StreamReader(PathDispo);
            string line = "";
            while (line != null)
            {
                line = dispoReader.ReadLine();
                if (line == null) break;
                if (line.Split(';')[0] == name & line.Split(';')[1] == lastname)
                {
                    disponibilities = line;
                    break;
                }
            }
            for (int i = 0; i < disponibilities.Split(';').Length; i++)         //transtiping to List<string>[]
            {
                List<string> tempList = new List<string>();
                foreach (string st in disponibilities.Split(';')[i].Split(',')) tempList.Add(st);
                tempList.RemoveAll(item => item == "");         //removes the empty strings that appears if there is a day witout disponibilities
                disponibility[i] = tempList;
            }
        }
        public void AddDispo()
        {
            string test = "yes";
            while (test == "yes")
            {
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
                    if (disponibility[day + 1].Count == 0) disponibility[day + 1].Add(slot);
                    else
                    {
                        for (int compt = 0; compt < disponibility[day + 1].Count; compt++)
                        {
                            if (slot.CompareTo(disponibility[day + 1][compt]) == 0)              //case if the time slot is already entered
                            {
                                Console.WriteLine("the time slot is already filled");
                                break;
                            }
                            if ((slot.CompareTo(disponibility[day + 1][compt]) < 0) | (slot.CompareTo(disponibility[day + 1][compt]) > 0 & compt == disponibility[day + 1].Count))               //case if the time entered is before another
                            {
                                disponibility[day + 1].Insert(compt, slot);
                                break;
                            }
                        }
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
        public void RemoveDispo()
        {
            string test = "yes";
            while (test == "yes")
            {
                string dtest = "yes";
                Console.WriteLine("Which day are you no longer disponible ?");
                Console.WriteLine("Monday : type 1");
                Console.WriteLine("Tuesday : type 2");
                Console.WriteLine("Wednesday : type 3");
                Console.WriteLine("Thursday : type 4");
                Console.WriteLine("Friday : type 5");
                int day = Convert.ToInt32(Console.ReadLine());
                while (dtest == "yes")
                {
                    Console.Write("Enter the time slot you wish to remove (ex 09:30-11:00) : ");
                    string slot = Console.ReadLine();
                    if (disponibility[day + 1].Contains(slot)) disponibility[day + 1].Remove(slot); //remove an instance of the slot entered if it exists
                    else Console.WriteLine("No matching time slot found this day.");
                    Console.WriteLine("Do you want to remove another disponibility this day ?");    //while loop exit
                    Console.Write("type yes or no : ");
                    dtest = Console.ReadLine();
                }
                Console.WriteLine("Do you want to remove disponibilities on another day ?");       //while loop exit
                Console.Write("type yes or no : ");
                test = Console.ReadLine();
            }
        }
        public void DisplayDispo()
        {
            string days = "Monday          Tuesday         Wednesday       Thursday        Friday";
            string space1 = "           ";
            string space2 = "     ";
            string line = "";
            Console.Write(disponibility[0][0]);
            Console.WriteLine(disponibility[1][0]);
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
    }
}
