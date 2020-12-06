using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Welcome to our registration system\n");
            string confirmation = "N";
            while (confirmation == "N")
            {
                CompletePersonalInfos();
                Console.Clear();
                ChooseLoginAndPassword();
                Console.Clear();
                LevelofClass();
                Console.Clear();
                ChooseCourses();
                Console.Clear();
                Fees();
                StudentID();
                PayFees();
                Console.Clear();
                Console.WriteLine($"You are {name} {lastname} born the {birthDate}. Your mail is {mail} and your phone is {phone}." +
                    $"You have chosen {courses.Count} courses. The courses you have chosen are {CoursesToString()}");
                Console.WriteLine("If you agree enter Y, otherwise entre N");
                confirmation = Console.ReadLine();
                while ((confirmation != "Y") && (confirmation != "N"))
                {
                    Console.WriteLine("If you agree enter Y, otherwise entre N");
                }
                if (confirmation == "N") Console.Clear();
            }
            ToStringDataAccessibilityLevel();
            ToStringCourses();
            ToStringDataStudent();
            WriteData(pathAccessibilityLevel, accessibilityData);
            WriteData(pathStudent, studentData);

        }

        /// <summary>
        /// The method uses 'CompletePersonalInfos()' in order to ask the user
        /// first name, last name, phone number and birth date and checks if the
        /// entered informations are correct. Then, it uses 'ChooseLoginAndPassword()'
        /// to ask the user login and password and checks if there are correct.
        /// After, it uses 'LevelofClass()' and 'ChooseCourses()' to ask him
        /// about his level and the courses he chooses. At least, it uses 'Fees()'
        /// and 'StudentID()' to ask him if he wants to pay the fees right now and
        /// the method creates the student number associated to the user. If the
        /// user accept that all his informations are correct when the method
        /// displays them, it uses the other methods of the class to convert the
        /// data in to string and to finally write it in the student file in the
        /// data base.
        /// </summary>
        /// <param name="admin"></param>
        public Registration(bool admin)
        {
            if (admin == true)
            {
                string confirmation = "N";
                while (confirmation == "N")
                {
                    CompletePersonalInfos();
                    Console.Clear();
                    ChooseLoginAndPassword();
                    Console.Clear();
                    LevelofClass();
                    Console.Clear();
                    ChooseCourses();
                    Console.Clear();
                    Fees();
                    StudentID();
                    Console.Clear();
                    Console.WriteLine($"firstname: {name}, lastname: {lastname}, born the {birthDate}. The mail is {mail} and the phone is {phone}." +
                        $"The courses the person have chosen are {CoursesToString()}");
                    Console.WriteLine("If you agree enter Y, otherwise entre N");
                    confirmation = Console.ReadLine();
                    while ((confirmation != "Y") && (confirmation != "N"))
                    {
                        Console.WriteLine("If you agree enter Y, otherwise entre N");
                    }
                    if (confirmation == "N") Console.Clear();
                }
                ToStringDataAccessibilityLevel();
                ToStringCourses();
                ToStringDataStudent();
                WriteData(pathAccessibilityLevel, accessibilityData);
                WriteData(pathStudent, studentData);
            }
        }

        /// <summary>
        /// The method 'convert' the enum of subject in to string.
        /// </summary>
        /// <returns>the course in string</returns>
        public string CoursesToString()
        {
            string sCourses = null;
            string[] tabEnum = Enum.GetNames(typeof(Subject));
            for (int i = 0; i < courses.Count; i++)
            {
                for (int j = 0; j < tabEnum.Length; j++)
                {
                    if (courses[i] == (Subject)j)
                    {
                        if (sCourses == null) sCourses = sCourses + tabEnum[j];
                        else sCourses = sCourses + "," + tabEnum[j];
                    }
                }

            }
            return sCourses;
        }

        /// <summary>
        /// It adds data to the student file in the data base. First, the method
        /// get all the data already present on the file student, and then close
        /// the file. Then, it adds all the data and the data of the new student
        /// while keeping all the data already present.
        /// </summary>
        /// <param name="path">the file to complete</param>
        /// <param name="data">the data to add</param>
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

        /// <summary>
        /// The method asks the user the number of courses he wants to take and
        /// what courses. There is a test when a course is enter to check if the
        /// course is not choosen twice or wrongly spelled.
        /// </summary>
        public void ChooseCourses()
        {
            string[] test = Enum.GetNames(typeof(Subject));
            Console.WriteLine($"How many courses do you want to take ? Choose a number between 1 and {test.Length}");
            int number = Convert.ToInt32(Console.ReadLine());
            while (number < 1 || number > test.Length)
            {
                Console.WriteLine($"Please choose a number between 1 and {test.Length}");
                number = Convert.ToInt32(Console.ReadLine());
            }
            Subject check = new Subject();
            int i = 0;

            string coursesAvailable = null;

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

        /// <summary>
        /// The method asks user about login and password. It checks if the login
        /// is an email adress and the password choosen has to be at least of 8
        /// characters. Then, it displays the login and choosen password and asks
        /// if the user agrees, if not the method does it all over.
        /// </summary>
        public void ChooseLoginAndPassword()
        {
            string agreement = "N";
            while (agreement == "N")
            {
                // mail
                Console.WriteLine("What is your mail ? (login)");
                mail = Console.ReadLine();
                bool check = false;
                while (check == false)
                {
                    for (int i = 0; i < mail.Length; i++)
                    {
                        if (mail[i] == '@') check = true;
                    }
                    if (check == false)
                    {
                        Console.WriteLine("email address incorrect");
                        Console.WriteLine("What is your mail ? (login)");
                        mail = Console.ReadLine();
                    }
                }

                // password
                Console.WriteLine("What is your password ? Password must have at least 8 caracters");
                password = Console.ReadLine();
                check = false;
                while (check == false)
                {
                    if (password.Length > 7) check = true;
                    if (check == false)
                    {
                        Console.WriteLine("the password you have chosen is incorrect ");
                        Console.WriteLine("What is your password ? Password must have at least 8 caracters");
                        password = Console.ReadLine();
                    }
                }
                Console.WriteLine($"Your mail is {mail} and your password is {password}. Do you agree with these informations ?\nIf you agree write Y, if you disagree write N.  ");
                agreement = Console.ReadLine();
                while (agreement != "N" && agreement != "Y")
                {
                    Console.WriteLine($"Your mail is {mail} and your password is {password}. Do you agree with these informations ?\nIf you agree write Y, if you disagree write N.  ");
                    agreement = Console.ReadLine();
                }
                if (agreement == "N") Console.Clear();
            }



        }
        /// <summary>
        /// It calculates the unpaid fees which is the number of courses * the cost
        /// of the courses.
        /// </summary>
        public void Fees() // number of courses * cost of the courses
        {
            UnpaidFees = courses.Count * priceCourses;
        }

        /// <summary>
        /// The method asks the user all his informations: first name, last name,
        /// phone number and birth date. It checks if first name and last name are
        /// string variable using a tab of characters for the alphabet. For the
        /// phone number, it checks if it is only number and +, using again a tab
        /// of characters for the number between 0 and  9. At least, it checks if
        /// the year, month and day of the  birth date are reasonable.
        /// Finally, the method displays all the informations and asks the user
        /// if it is correct. If the user is not agree the method does it all over.
        /// </summary>
        public void CompletePersonalInfos()
        {
            string agreement = "N";
            while (agreement == "N")
            {
                Console.WriteLine("What is your first name ?");
                name = Console.ReadLine();
                bool check3 = false;
                while (check3 == false)
                {
                    check3 = true;
                    if (name == "" || name == " ") check3 = false;
                    if (check3 == true)
                    {
                        char[] tabAut = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '-', 'é', 'è', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                        bool num = false;
                        for (int i = 0; i < name.Length; i++)
                        {
                            num = false;

                            for (int j = 0; j < tabAut.Length; j++)
                            {
                                if (name[i] == tabAut[j]) num = true;
                            }
                            if (num == false) check3 = false;
                        }
                    }

                    if (check3 == false)
                    {
                        Console.WriteLine("your first name has an input error. ");
                        Console.WriteLine("Please enter your first name again");
                        name = Console.ReadLine();
                    }

                }

                Console.WriteLine("What is your last Name ?");
                lastname = Console.ReadLine();
                bool check2 = false;
                while (check2 == false)
                {
                    check2 = true;
                    if (lastname == "" || lastname == " ") check2 = false;
                    if (check2 == true)
                    {
                        char[] tabAut = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '-', 'é', 'è', ' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                        bool num = false;
                        for (int i = 0; i < lastname.Length; i++)
                        {
                            num = false;
                            for (int j = 0; j < tabAut.Length; j++)
                            {
                                if (lastname[i] == tabAut[j]) num = true;
                            }
                            if (num == false) check2 = false;
                        }
                    }

                    if (check2 == false)
                    {
                        Console.WriteLine("your last name has an input error. ");
                        Console.WriteLine("Please enter your last name again");
                        lastname = Console.ReadLine();
                    }

                }

                Console.WriteLine("What is your phone number ?");
                phone = Console.ReadLine();
                bool check1 = false;
                while (check1 == false)
                {

                    check1 = true;
                    if (phone.Length < 2) check1 = false;
                    char[] tabAut = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+' };
                    bool num = false;
                    for (int i = 0; i < phone.Length; i++)
                    {
                        num = false;
                        for (int j = 0; j < tabAut.Length; j++)
                        {
                            if (phone[i] == tabAut[j]) num = true;
                        }
                        if (num == false) check1 = false;
                    }
                    if (check1 == false)
                    {
                        Console.WriteLine("your phone number has an input error. ");
                        Console.WriteLine("Please enter your phone number again");
                        phone = Console.ReadLine();
                    }

                }



                Console.WriteLine("birth date ? dd/mm/yyyy european date format");
                birthDate = Console.ReadLine();
                bool check = false;
                while (check == false)
                {
                    check = true;
                    char[] tabAut = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/' };
                    bool num = false;
                    int checkSlash = 0;
                    for (int i = 0; i < birthDate.Length; i++)
                    {
                        num = false;
                        for (int j = 0; j < tabAut.Length; j++)
                        {
                            if (birthDate[i] == tabAut[j]) num = true;
                        }
                        if (birthDate[i] == '/') checkSlash++;
                        if (num == false) check = false;

                    }
                    if (checkSlash != 2) check = false;

                    if (birthDate.Length > 5 && check == true)
                    {
                        // add a zero if you don't put a zero before your number. ex: 1/1/2000 becomes 01/01/2000
                        int decal = 0;
                        string nbirthDate = null;
                        if (birthDate[2] != '/' || birthDate[5] != '/')
                        {
                            for (int i = 0; i < birthDate.Length; i++)
                            {
                                if (birthDate[i] == '/')
                                {
                                    if (i == 1) nbirthDate = "0";
                                    if (i == 3) nbirthDate = nbirthDate + birthDate[0] + "/" + "0" + birthDate[i - 1]; decal = 1;
                                    if (i == 2) nbirthDate = nbirthDate + birthDate[0] + birthDate[1] + birthDate[2] + "0"; i = 3; decal = 1;
                                    if (i == 1 && birthDate[4] == '/') nbirthDate = "0" + birthDate[0]; decal = 1;
                                }
                                if (decal == 1) nbirthDate = nbirthDate + birthDate[i];
                            }
                            birthDate = nbirthDate;
                        }

                        if (birthDate.Length != 10) check = false;
                        if (birthDate[2] != '/') check = false;
                        if (birthDate[5] != '/') check = false;
                        if (check == true)
                        {
                            // check if the year, month and day are reasonable
                            string[] tabBirthDate = birthDate.Split('/');

                            int day = Convert.ToInt32(tabBirthDate[0]);
                            int month = Convert.ToInt32(tabBirthDate[1]);
                            int year = Convert.ToInt32(tabBirthDate[2]);

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
                        Console.WriteLine("your birth date has an input error. ");
                        Console.WriteLine("birth date ? dd/mm/yyyy");
                        birthDate = Console.ReadLine();
                    }
                }

                Console.WriteLine($"You are {name} {lastname} born the {birthDate}. Your phone number is {phone}.\nIf you agree, enter Y. If you disagree enter N. ");
                agreement = Console.ReadLine();
                while (agreement != "N" && agreement != "Y")
                {
                    Console.WriteLine($"You are {name} {lastname} born the {birthDate}. Your phone number is {phone}.\nIf you agree, enter Y. If you disagree enter N. ");
                    agreement = Console.ReadLine();
                }
                if (agreement == "N") Console.Clear();

            }


        }

        /// <summary>
        /// The method asks the user about the level of class he wants to be.
        /// </summary>
        public void LevelofClass()
        {
            Console.WriteLine("In which year do you want to be ?");
            level = Convert.ToInt32(Console.ReadLine());
            Random r = new Random();
            classe = r.Next(1, 3);
        }

        /// <summary>
        /// The method uses a random to create a student number. The student number
        /// is made of the 2 first characters of the last name and the name of the
        /// student and of a random number between 10000 and 99999. The student
        /// number is aded to the student file and finally the file is closed.
        /// </summary>
        public void StudentID()
        {
            StreamReader reader = new StreamReader(pathStudent);
            Random r = new Random();
            studentID = studentID + lastname[0] + lastname[1] + name[0] + name[1] + Convert.ToString(r.Next(10000, 99999));
            List<string> tab = new List<string>();
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                string[] tabStudent = temp.Split(';');
                while (tabStudent[3] == studentID)
                {
                    studentID = studentID + lastname[0] + lastname[1] + name[0] + name[1] + Convert.ToString(r.Next(10000, 99999));
                }

            }
            reader.Close();
        }

        /// <summary>
        /// The method displays the amount of unpaid fees and asks the user if
        /// he wants to pay now or not. If the answer is no, the method ends.
        /// Otherwise, the method asks the amount the user wants to pay right
        /// now and checks if the entered amount is correct (between 0 and the
        /// amount unpaid).
        /// </summary>
        public void PayFees()
        {
            Console.WriteLine($"Your scolarity fees are: {UnpaidFees}.\n Do you want to pay them now ?. If you want to pay now, enter Y otherwise enter N");
            string answer = Console.ReadLine();
            while (answer != "Y" && answer != "N")
            {
                Console.WriteLine($"Your scolarity fees are: {UnpaidFees}.\n Do you want to pay them now ?. If you want to pay now, enter Y otherwise enter N");
                answer = Console.ReadLine();
            }
            switch (answer)
            {
                case "Y":
                    {
                        Console.WriteLine("How much do you want to pay ?");
                        double PaidFees = Convert.ToDouble(Console.ReadLine());
                        while (PaidFees > UnpaidFees || PaidFees < 0)
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

        /// <summary>
        /// It puts all the data of the student in to a variable in string.
        /// </summary>
        public void ToStringDataStudent()
        {
            studentData = ($"{name};{lastname};{mail};{studentID};{birthDate};{absences};{phone};{tutor};{level};{classe};{UnpaidFees};{courseStorage.Length / 2}{courseStorage}");
        }

        /// <summary>
        /// It puts in string the data of accessibility (login and password).
        /// </summary>
        public void ToStringDataAccessibilityLevel()
        {
            accessibilityData = mail + ";" + password + ";" + "student";
        }

        public void DisplayPersonalInfos()
        {
            throw new NotImplementedException();
        }
    }
}
