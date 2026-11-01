namespace CinemaNetworkManagement.Model
{
    public abstract class Person
    {
        private readonly int _id;
        private string _firstName;
        private string _lastName;

        public int Id
        {
            get { return _id; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        protected Person() : this(0, string.Empty, string.Empty)
        {
        }

        protected Person(int id, string firstName, string lastName)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
        }

        public virtual string GenerateDescription()
        {
            return $"ID: {_id} Imie: {_firstName} Nazwisko: {_lastName}";
        }

        public override string ToString()
        {
            return GenerateDescription();
        }
    }
}
