using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Project
{
    public class Classes
    {
        public int Level { get; set; }
        public int ClassGroup { get; set; }

        List<Student> classStudent = new List<Student>();
        public List<Student> ClassStudent
        {
            get
            {
                return this.ClassStudent = classStudent;
            }
            set
            {
                value = classStudent;
            }
        }
    }
}
