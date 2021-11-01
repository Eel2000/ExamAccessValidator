using System;
using System.Collections.Generic;
using System.Text;

namespace ExamAccessValidator.Interfaces
{
    public interface IToastMessage
    {
       void ShowLong(string message);
       void ShowShort(string message);
    }
}
