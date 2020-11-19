using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Project
{
    public class Exam
    {
        public double Mark { get; set; }
        public double Coef { get; set; }
        public int Sub { get; set; }
        public string Date { get; set; }


        public Exam(double Mark, double Ceof, int Sub, string Date)
        {
            this.Mark = Mark;
            this.Coef = Coef;
            this.Sub = Sub;
            this.Date = Date;
        }

        public Exam() { }

        public override string ToString()
        {
            string exam = ($"{Sub},{Mark},{Coef},{Date}");
            return exam;
        }

        public string ToString2()
        {
            string exam = ($"Subject: {(Subject)Sub}, Mark: {Mark}, Coefficient: {Coef}, Date: {Date}");
            return exam;
        }
    }
}
