using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime;

namespace OOP_Project
{
    public class Calendar
    {
        public static void FileCreation()
        {
            string[] tab = Enum.GetNames(typeof(Subject));
            FileStream stream = new FileStream(".//AssignmentsExams.txt", FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {

                //keep all the data already present
                for (int i = 0; i < tab.Length; i++)
                {
                    writer.WriteLine(tab[i]);

                }

            }
            stream.Close();
        }

        /// <summary>
        /// adds a new exam assignment to the time table
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="Date"></param>
        /// <param name="ExamAssignment"></param>
        /// <param name="Classe"></param>
        /// <param name="Niveau"></param>
        public static void AddExamAssignment(string Subject, DateTime Date, String ExamAssignment, int Classe, int Niveau) // Modify to be able to choose the Class of student 
        {

            StreamReader reader = new StreamReader(".//AssignmentsExams.txt"); // declaration of the reader and the link of the file
            string temp = " ";
            List<string> tab = new List<string>();   // we create a list so we can temporarily keep the information of the reading

            while (temp != null) //We need to indicate an end to the reading 
            {
                temp = reader.ReadLine();
                if (temp == null) break;
                tab.Add(temp);

                if (temp == Subject)
                {
                    tab.Add(ExamAssignment + ";" + Date + ";" + Classe + ";" + Niveau);
                }
            }


            // Ajouetr une fonction qui efface un Devoir/ Exam ?

            reader.Close();


            FileStream stream = new FileStream(".//AssignmentsExams.txt", FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {

                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);
                }

            }
            stream.Close();

        }

        public static void ReadExamAssignment(int classe, int level)
        {
            StreamReader reader = new StreamReader(".//AssignmentsExams.txt"); // declaration of the reader and the link of the file
            string temp = " ";
            while (temp != null)
            {
                temp = reader.ReadLine();
                string save = temp;
                if (temp == null) break;
                string[] split = temp.Split(';');
                if (split.Length == 1)
                {
                    Console.WriteLine(save);
                }
                if (split.Length > 1 && Convert.ToInt32(split[2]) == classe && Convert.ToInt32(split[3]) == level)
                {
                    Console.WriteLine(split[1] + " " + split[0]);
                }
            }

            reader.Close();
        }
        /// <summary>
        /// Delete an exam/assignment of the assignment file
        /// </summary>
        /// <param name="level"></param> level of the class of student
        /// <param name="classe"></param> number of a class of student
        public static void DeleteExamAssignment(int level, int classe)
        {
            ReadExamAssignment(classe, level);
            Console.WriteLine("Enter the date of the exam/assignment you want to delete");
            string date = Console.ReadLine();
            string[] tabEnum = Enum.GetNames(typeof(Subject));
            string coursesAvailable = null;
            string[] test = Enum.GetNames(typeof(Subject));
            for (int index = 0; index < test.Length; index++)
            {
                coursesAvailable = (coursesAvailable + " " + test[index]);
            }

            Console.WriteLine("Enter the subject of the exam/assignment you want to delete: " + coursesAvailable);
            string subject = Console.ReadLine();

            StreamReader reader = new StreamReader(".//AssignmentsExams.txt"); // declaration of the reader and the link of the file
            string temp = " ";
            List<string> tab = new List<string>();   // we create a list so we can temporarily keep the information of the reading
            string subjectTemp = null;
            while (temp != null) //We need to indicate an end to the reading 
            {

                temp = reader.ReadLine();
                if (temp == null) break;
                if (temp.Split(';').Length == 1)
                {
                    subjectTemp = temp;
                    Console.WriteLine(subjectTemp);
                    tab.Add(temp);
                }
                if (temp.Split(';').Length > 1)
                {
                    Console.WriteLine(temp.Split(';')[1] + " " + date + "?" + subjectTemp + "/" + subject);
                    if (temp.Split(';')[1] == date && subjectTemp == subject) { Console.WriteLine("passe"); }
                    else tab.Add(temp);
                }

            }

            reader.Close();
            FileStream stream = new FileStream(".//AssignmentsExams.txt", FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {

                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);
                }

            }
            stream.Close();
        }
    }
}
