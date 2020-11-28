using System;

namespace OOP_Project
{
    public class Program
    {
            static void Main(string[] args)
            {

            Console.BackgroundColor = (ConsoleColor)15;
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)1;

            string end = "N";
            while (end == "N")
            {
                Console.WriteLine("WELCOME TO OUR COLLEGE GESTION SYSTEM");
                Console.WriteLine("Registration(R) or Login(L) ?");
                string caseSwitch0 = Console.ReadLine();
                string caseSwitch = caseSwitch0.ToUpper();
                while (caseSwitch != "R" && caseSwitch != "L")
                {
                    Console.WriteLine("Registration(R) or Login(L) ?");
                    Console.WriteLine("Please enter L or R");
                    caseSwitch = Console.ReadLine();
                }
                switch (caseSwitch)
                {
                    case "R":
                        Console.Clear();
                        Registration newStudent = new Registration();
                        break;
                    case "L":
                        Console.WriteLine("Are you a student(S), a Facility member(F) or an Admin(A) ?");
                        string caseSwitchmin = Console.ReadLine();
                        string caseSwitch2 = caseSwitchmin.ToUpper();
                        while (caseSwitch2 != "S" && caseSwitch2 != "F" && caseSwitch2 != "A")
                        {
                            Console.WriteLine("Are you a student(S), a Facility member(F) or an Admin(A) ?");
                            Console.WriteLine("Please enter S, F or A");
                            caseSwitchmin = Console.ReadLine();
                            caseSwitch2 = caseSwitchmin.ToUpper();

                        }
                        switch (caseSwitch2)
                        {
                            case "S":
                                Student student = new Student();
                                student.ExeFunctions();
                                break;
                            case "F":
                                FacilityMember facilityMember = new FacilityMember();
                                facilityMember.ExeFunctions();
                                break;
                            case "A":
                                Admin admin = new Admin();
                                admin.ExeFunctions();
                                break;
                            default:
                                break;

                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
