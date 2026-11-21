namespace Projekt.Services.Lekcja;
using Projekt.Models;
using Projekt.Models.Enums;

public interface ILekcjaService
{
    bool AddStudent(Lekcja lesson, Uczen student);
    bool RemoveStudent(Lekcja lesson, Uczen student);
    bool ChangeCost(Lekcja lesson, decimal cost);
    bool ChangeStatus(Lekcja lesson, StatusLekcji status);

}