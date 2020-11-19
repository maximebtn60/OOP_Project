using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Project
{
    public interface IPersonalInformations
    {
        string name { get; set; }
        string lastname { get; set; }
        string mail { get; set; }
        string phone { get; set; }
        string birthDate { get; set; } //date under the format xx/xx/xxxx

        void DisplayPersonalInfos();
    }
}
