namespace Projekt.Services.Lekcja;
using Projekt.Models;
using Projekt.Models.Enums;

public class LekcjaService: ILekcjaService
{
    public bool AddStudent(Lekcja lesson, Uczen student)
    {
        if(lesson != null && student != null)
        {
            lesson.Students.Add(student);
            return true;
        }
        return false;
    }

    public bool RemoveStudent(Lekcja lesson, Uczen student)
    {
        if(lesson != null && student != null)
        {
            if (lesson.Students.Contains(student))
            {
                lesson.Students.Remove(student);
                return true;
            }
        }
        return false;
    }
    
    public bool ChangeCost(Lekcja lesson, decimal cost)
    {
        if(lesson != null)
        {
            lesson.Cost = cost;
            return true;
        }
        return false;
    }

    public bool ChangeStatus(Lekcja lesson, StatusLekcji status)
    {
        if(lesson != null)
        {
            lesson.Status = status;
            return true;
        }
        return false;
    }
}