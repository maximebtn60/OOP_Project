using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OOP_Project
{
    class Registration : IPersonalInformations
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
        public string forename { get; set; }
        public string mail { get; set; } //also used as login
        public string phone { get; set; }
        public string birthDate { get; set; }

        //login and password
        public string login { get; set; }
        public string password { get; set; }

        // storage of data before writing in a file
        string studentData = null;
        string accessibilityData = null;

        public Registration() // constructor
        {
            CompletePersonalInfos();
            ChooseLoginAndPassword();
            LevelofClass();
            ChooseCourses();
            Fees();
            StudentID();
            ToStringDataAccessibilityLevel();
            ToStringDataStudent();
            WriteData(pathAccessibilityLevel, accessibilityData);
            WriteData(pathStudent, studentData);

        }

        //Add data to the student file in the data base
        public void WriteData(string path, string data) //path: choose the file you want to complete, data: add the data you want to add
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
            Subject check = Subject.french;
            int i = 0;
            while (i < number) // choice of the courses
            {
                Console.WriteLine("what course de you want? french, litterature, history, maths, english ?");
                string temp = Console.ReadLine();
                switch (temp)
                {
                    case "french":
                        {
                            check = Subject.french;
                            break;
                        }

                    case "english":
                        {
                            check = Subject.english;
                            break;
                        }

                    case "history":
                        {
                            check = Subject.history;
                            break;
                        }
                    case "maths":
                        {
                            check = Subject.maths;
                            break;
                        }
                    case "litterature":
                        {
                            check = Subject.litterature;
                            break;
                        }

                }
                // check that you didn't choose twice the same course

                int blockage = 0;
                if (i != 0)
                {
                    for (int j = 0; j < courses.Count; j++)
                    {
                        if (courses[j] == check)
                        {
                            blockage++;
                        }
                    }
                    if (blockage == 0)
                    {
                        courses.Add(check);
                    }
                }
                else
                {
                    courses.Add(check);
                }
                if (blockage == 0)
                {
                    i++;
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

            Console.WriteLine("ForeName ?");
            forename = Console.ReadLine();

            Console.WriteLine("Phone number ? Please insert the indicatif of the country ");
            phone = Console.ReadLine();


            Console.WriteLine("birth date ? xx/ww/zzzz");
            birthDate = Console.ReadLine();
            while (birthDate.Length != 10)
            {
                birthDate = Console.ReadLine();
            }

        }

        public void LevelofClass()
        {
            Console.WriteLine("In which level do you want to be ?");
            level = Convert.ToInt32(Console.ReadLine());
            Random r = new Random();
            classe = r.Next(1, 2);
        }

        public void StudentID()
        {
            Random r = new Random();
            studentID = studentID + forename[0] + forename[1] + name[0] + name[1] + Convert.ToString(r.Next(10000, 99999));
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

        public void ToStringDataStudent() // to complete
        {
            studentData = name + ";" + forename + ";" + studentID + ";" + birthDate + ";" + absences + ";" + mail + ";" + phone + ";" + tutor + ";" + level + ";" + classe + ";" + UnpaidFees;
        }

        public void ToStringDataAccessibilityLevel()
        {
            accessibilityData = mail + ";" + password + ";" + "student";
        }


    }
}
