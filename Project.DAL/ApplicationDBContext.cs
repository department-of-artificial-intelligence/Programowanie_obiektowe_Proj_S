using Microsoft.EntityFrameworkCore;
using Project.Model;
using System;

namespace Project.DAL;

public class ApplicationDbcontext : DbContext
{
	public DbSet<Osoba> Osoby {  get; set; }

	public ApplicationDbContext(DbContextOptionsc<ApplicationDbcontext> options) : base(options)
	{

	}
}
