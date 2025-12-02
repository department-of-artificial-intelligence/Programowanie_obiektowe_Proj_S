using System;
using Project.Model;

public class Client : Person, IShowInfo
{
	public int ClientId { get; set; }
	public string? PhoneNumber { get; set; }

	public Client() : this(0, string.Empty, string.Empty, string.Empty) { }
	public Client(int clientId, string firstName, string lastName, string phoneNumber) : base(firstName, lastName)
	{
		ClientId = clientId;
		PhoneNumber = phoneNumber;
	}

	public string GetInfo()
	{
        return $@"---- Client ----
ID: {ClientId}
Name: {GetFullName()}
Phone: {PhoneNumber}
-------------------";
    }
}
