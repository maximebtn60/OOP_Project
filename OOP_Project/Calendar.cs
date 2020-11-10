using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime;

namespace OOP_Project
{
    public class Calendar
    {

        String Subject;  // We ask for what subject the Assignment or exam is for 
        DateTime Date;  // We ask for the date
        String ExamAssignment;   //  we ask what is it going to be about (Like what chapter or something)
        List<Student> Classe;


        public Calendar(string Subject, DateTime Date, String ExamAssignment, List<Student> Classe)
        {
            this.Subject = Subject;
            this.Date = Date;
            this.Classe = Classe;  // ADD A STUDENT
        }

        public static void AddExamAssignment(string Subject, DateTime Date, String ExamAssignment, int Classe, int Niveau) // Modify to be able to choose the Class of student 
        {

            StreamReader reader = new StreamReader("C:/Users/HP/Downloads/AssignmentsExams.txt"); // declaration of the reader and the link of the file
            string temp = " ";
            List<string> tab = new List<string>();   // we create a list so we can temporarily keep the information of the reading

            while (temp != "End of List") //We need to indicate an end to the reading 
            {
                temp = reader.ReadLine();
                tab.Add(temp);

                if (temp == Subject)
                {
                    tab.Add(ExamAssignment + ";" + Date + ";" + Classe + ";" + Niveau);
                }
            }


            // Ajouetr une fonction qui efface un Devoir/ Exam ?

            reader.Close();


            FileStream stream = new FileStream("C:/Users/HP/Downloads/AssignmentsExams.txt", FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {

                //keep all the data already present
                for (int i = 0; i < tab.Count; i++)
                {
                    writer.WriteLine(tab[i]);



                }

            }

        }



        public override string ToString()
        {
            string x = ExamAssignment + "at the following date : " + Date;
            return x;
        }

    }
}
