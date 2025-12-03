using System;

namespace Project.Model
{
    public interface IReservable
    {
        void Zarezerwuj(DateTime od, DateTime doo);
        void Zwolnij();
        bool Dostepny { get; }
    }
}
