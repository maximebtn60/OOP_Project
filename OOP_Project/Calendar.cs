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
    }
}
