using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Zadania
{
    class Person
    {
        protected string _firstName;
        protected string _lastName;
        protected DateTime _dateOfBirth;

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }

        public Person() : this(string.Empty, string.Empty, DateTime.MinValue) { }
        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._dateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"Osoba: {_firstName} {_lastName}, data urodzenia: {_dateOfBirth}\n";
        }
    }


    class Grade
    {
        private string _subjectName;
        private double _value;
        private DateTime _date;

        public string SubjectName { get => _subjectName; set => _subjectName = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public double Value
        {
            get => _value;
            set
            {
                if (value < 2.0 || value > 5.0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Ocena musi być w przedziale od 2 do 5."
                        );
                _value = value;
            }
        }


        public Grade() : this(string.Empty, 2.0, DateTime.MinValue) { }
        public Grade(string subjectName, double value, DateTime date)
        {
            this._subjectName = subjectName;
            this.Value = value;
            this._date = date;
        }

        public override string ToString()
        {
            return $"Ocena: {_subjectName}/{_value}/{_date.ToShortDateString()}\n";
        }
    }

    class Student : Person
    {
        private int _year;
        private int _group;
        private int _indexId;
        private List<Grade> _grades;

        public int Year { get => _year; set => _year = value; }
        public int Group { get => _group; set => _group = value; }
        public int IndexId { get => _indexId; set => _indexId = value; }
        public List<Grade> Grades { get => _grades; set => _grades = value ?? new List<Grade>(); }

        public Student() : base()
        {
            this._year = 0;
            this._group = 0;
            this._indexId = 0;
            this._grades = new List<Grade>();
        }

        public Student(string firstName, string lastName, DateTime dateOfBirth, int year, int group, int indexId) : base(firstName, lastName, dateOfBirth)
        {
            this._year = year;
            this._group = group;
            this._indexId = indexId;
            this._grades = new List<Grade>();
        }


        public override string ToString()
        {
            string s = base.ToString() + $"Student: {_year}/{_group}/{_indexId}/oceny:\n";
            foreach (var o in Grades) s += "-" + o;
            return s;
        }

        public void AddGrade(string subjectName, double value, DateTime date)
        {
            Grade grade = new Grade(subjectName, value, date);
            this._grades.Add(grade);
        }

        public void AddGrade(Grade grade)
        {
            if (grade != null) this._grades.Add(grade);
        }

        public void DeleteGrade(string subjectName, double value, DateTime date)
        {
            this._grades.RemoveAll(g =>
                g.SubjectName == subjectName &&
                g.Value == value &&
                g.Date == date
            );
        }

        public void DeleteGrades() { this._grades.Clear(); }

        public void DeleteGrades(string subjectName) { this._grades.RemoveAll(grade => grade.SubjectName == subjectName); }

        public string GetGrades()
        {
            var grade = new List<string>();
            foreach (var o in this._grades)
            {
                grade.Add(o.Value.ToString());
            }
            return string.Join(", ", grade);
        }
    }


    class Player : Person
    {
        private string _position;
        private string _club;
        private int _scoredGoals;

        public string Position { get => _position; set { _position = value; } }
        public string Club { get => _club; set { _club = value; } }
        public int ScoredGoals { get => _scoredGoals; set => _scoredGoals = value; }

        public Player() : base()
        {
            this._position = string.Empty;
            this._club = string.Empty;
            this._scoredGoals = 0;
        }

        public Player(string firstName, string lastName, DateTime dateOfBirth, string position, string club, int scoredGoals) : base(firstName, lastName, dateOfBirth)
        {
            this._position = position;
            this._club = club;
            this._scoredGoals = scoredGoals;
        }


        public override string ToString()
        {
            return base.ToString() + $" - {_position} klubu {_club}, gole: {_scoredGoals}";
        }

        public virtual void ScoreGoal() { ScoredGoals++; }
    }

    sealed class HandballPlayer : Player
    {
        public HandballPlayer() : base() { }

        public HandballPlayer(string firstName, string lastName, DateTime dateOfBirth, string position, string club)
            : base(firstName, lastName, dateOfBirth, position, club, 0) { }

        public override void ScoreGoal()
        {
            ScoredGoals += 2;
        }
    }

    sealed class FootballPlayer : Player
    {
        public FootballPlayer() : base() { }
        public FootballPlayer(string firstName, string lastName, DateTime dateOfBirth, string position, string club, int scoredGoals)
            : base(firstName, lastName, dateOfBirth, position, club, scoredGoals) { }
        public override void ScoreGoal()
        {
            ScoredGoals += 4;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Kod testowy 1");
            Console.WriteLine("-------------------------------------------");

            Person person1 = new("Alex", "Nowak", new(2035, 3, 8, 12, 30, 10));
            Person person2 = new Student("Michalina", "Wójcik", new(2032, 6, 1), 3, 5, 12345);
            Person person3 = new Player("Marian", "Kozłowski", new(2033, 12, 24), "Striker", "ARS Warsaw", 41);
            Console.WriteLine(person1);
            Console.WriteLine(person2);
            Console.WriteLine(person3);

            Student student = new("Katarzyna", "Piotrowska", new(2030, 12, 31), 2, 5, 54321);
            Console.WriteLine(student);
            ((Player)person3).ScoreGoal();
            Console.WriteLine(person3);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Kod testowy 2");
            Console.WriteLine("-------------------------------------------");
            ((Student)person2).AddGrade("PO", 5.0D, new(2051, 2, 14));
            ((Student)person2).AddGrade("Bazy Danych", 5.0D, new(2052, 11, 11));
            Console.WriteLine(person2);

            Grade grade = new("Bazy Danych", 5.0D, new(2053, 5, 1));
            student.AddGrade(grade);
            student.AddGrade("AWWW", 5.0D, new(2054, 12, 6));
            student.AddGrade("AWWW", 4.5D, new(2055, 10, 31));
            var grades = student.GetGrades();
            Console.WriteLine(student);

            student.DeleteGrade("AWWW", 4.5D, new(2055, 10, 31));
            Console.WriteLine(student);
            student.DeleteGrades("AWWW");
            Console.WriteLine(student);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Kod testowy 3");
            Console.WriteLine("-------------------------------------------");

            Person footballPlayer = new FootballPlayer("Mateusz", "Żuraw", new(2031, 7, 4), "striker", "BTR Paris", 10);
            Person handballPlayer = new HandballPlayer("Patrycja", "Szymańska", new(2030, 5, 1), "stiker", "EQY Tokyo");
            Console.WriteLine(footballPlayer);
            Console.WriteLine(handballPlayer);

            ((Player)handballPlayer).ScoreGoal();
            (footballPlayer as Player)?.ScoreGoal();
            Console.WriteLine(footballPlayer);
            Console.WriteLine(handballPlayer);
        }
    }
}
