using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    public class Registration : IPersonalInformations
    {

        // assume each subject cost 300 euros per year (to confirm) 
        // assume you can choose between 1 and 5 subjects


        //file link
        public string pathAccessibilityLevel = ".//AccessibilityLevel.txt";
        public string pathStudent = ".//Student.txt";

        // specific to the class
        public List<Subject> courses = new List<Subject>();
        public double UnpaidFees { get; set; }
        public string studentID;
        int level = 0;
        int classe = 0;
        public int absences = 0;
        int tutor = 0;
        public double priceCourses = 300;

        //personal info
        public string name { get; set; }
        public string lastname { get; set; }
        public string mail { get; set; } //also used as login
        public string phone { get; set; }
        public string birthDate { get; set; }

        //login and password
        public string login { get; set; }
        public string password { get; set; }

        // storage of data before writing in a file
        string studentData = null;
        string accessibilityData = null;
        string courseStorage = null;

        public Registration() // constructor
        {
            CompletePersonalInfos();
            ChooseLoginAndPassword();
            LevelofClass();
            ChooseCourses();
            Fees();
            StudentID();
            PayFees();
            ToStringDataAccessibilityLevel();
            ToStringCourses();
            ToStringDataStudent();
            WriteData(pathAccessibilityLevel, accessibilityData);
            WriteData(pathStudent, studentData);

        }

        //Add data to the student file in the data base
        public static void WriteData(string path, string data) //path: choose the file you want to complete, data: add the data you want to add
        {
            //get all the datas already present on the file student
            StreamReader reader = new StreamReader(path);
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                tab.Add(temp);
            }
            reader.Close();

            // add all the data and the data of the new student
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);
                }
                writer.WriteLine(data);
            }
        }

        public void ChooseCourses()
        {

            Console.WriteLine("How many courses do you want to take ? number between 1 and 5");
            int number = Convert.ToInt32(Console.ReadLine());
            Subject check = new Subject();
            int i = 0;

            string coursesAvailable = null;
            string[] test = Enum.GetNames(typeof(Subject));
            for (int index = 0; index < test.Length; index++)
            {
                coursesAvailable = (coursesAvailable + " " + test[index]);
            }

            while (i < number) // choice of the courses
            {
                Console.WriteLine($"what course de you want? {coursesAvailable} ");
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

        }

        public void ToStringCourses()
        {
            string[] test = Enum.GetNames(typeof(Subject));
            for (int i = 0; i < courses.Count; i++)
            {
                for (int k = 0; k < test.Length; k++)
                {
                    if (courses[i] == (Subject)k)
                    {
                        courseStorage = courseStorage + ";" + Convert.ToString(k);
                    }
                }
            }

        }

        public void ChooseLoginAndPassword()
        {
            Console.WriteLine("What is your mail ? (login)");
            mail = Console.ReadLine();
            Console.WriteLine("What is your password ?");
            password = Console.ReadLine();
        }

        public void Fees() // number of courses * cost of the courses
        {
            UnpaidFees = courses.Count * priceCourses;
        }

        public void CompletePersonalInfos()
        {
            Console.WriteLine("Name ?");
            name = Console.ReadLine();

            Console.WriteLine("Last Name ?");
            lastname = Console.ReadLine();

            Console.WriteLine("Phone number ?");
            phone = Console.ReadLine();

            Console.WriteLine("birth date ? xx/ww/zzzz");
            birthDate = Console.ReadLine();
            while (birthDate.Length != 10)
            {
                Console.WriteLine("format incorrect, please enter birth date again");
                birthDate = Console.ReadLine();
            }

        }

        public void LevelofClass()
        {
            Console.WriteLine("In which level do you want to be ?");
            level = Convert.ToInt32(Console.ReadLine());
            Random r = new Random();
            classe = r.Next(1, 3);
        }

        public void StudentID()
        {
            Random r = new Random();
            studentID = studentID + lastname[0] + lastname[1] + name[0] + name[1] + Convert.ToString(r.Next(10000, 99999));
        }

        public void PayFees()
        {
            Console.WriteLine($"Your scolarity fees are: {UnpaidFees}./n Do you want to pay them now ?");
            string answer = "";
            switch (answer)
            {
                case "Y":
                    {
                        Console.WriteLine("How much do you want to pay ?");
                        double PaidFees = Convert.ToDouble(Console.ReadLine());
                        while (PaidFees > UnpaidFees || PaidFees < 1)
                        {
                            Console.WriteLine("Amount Impossible. How much do you want to pay ?");
                            PaidFees = Convert.ToDouble(Console.ReadLine());
                        }
                        // possibility to ask bank account to give a realistic feeling
                        UnpaidFees = UnpaidFees - PaidFees;
                        break;
                    }
                case "N":
                    {
                        break;
                    }
            }
        }

        public void ToStringDataStudent()
        {
            studentData = ($"{name};{lastname};{mail};{studentID};{birthDate};{absences};{phone};{tutor};{level};{classe};{UnpaidFees};{courseStorage.Length / 2}{courseStorage}");
        }

        public void ToStringDataAccessibilityLevel()
        {
            accessibilityData = mail + ";" + password + ";" + "student";
        }
    }
}
