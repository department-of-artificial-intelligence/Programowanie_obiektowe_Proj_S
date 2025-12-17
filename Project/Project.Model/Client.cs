using System;
using Project.Model;

public class Client : Person, IShowInfo
{
	public string? PhoneNumber { get; set; }

	public Client() : this(0, string.Empty, string.Empty, string.Empty) { }
	public Client(int id, string firstName, string lastName, string phoneNumber) : base(firstName, lastName)
	{
		Id = id;
		PhoneNumber = phoneNumber;
	}

	public string GetInfo()
	{
        return $@"---- Client ----
ID: {Id}
Name: {GetFullName()}
Phone: {PhoneNumber}
-------------------";
    }
}
