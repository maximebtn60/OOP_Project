using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Project
{
    public class LevelAndClasses
    {
        public int Level { get; set; }
        List<int> classes = new List<int>();
        public List<int> Classes
        {
            get
            {
                return this.Classes = classes;
            }
            set
            {
                value = classes;
            }
        }



        public LevelAndClasses()
        {

        }
    }
}
