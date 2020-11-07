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
        public Exam(double Mark, double Coef, int Sub, string Date)
        {
            this.Mark = Mark;
            this.Coef = Coef;
            this.Sub = Sub;
            this.Date = Date;
        }

        public override string ToString()
        {
            string exam = ($"{Mark},{Coef},{Date}");
            return exam;
        }
    }
}
