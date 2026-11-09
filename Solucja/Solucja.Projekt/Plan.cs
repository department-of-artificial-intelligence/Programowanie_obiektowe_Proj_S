using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Solucja.Projekt
{
	public class Plan
	{
		public List<Lesson> Lessons;

		public Plan()
		{
			Lessons = new List<Lesson>();
		}
		public Plan(List<Lesson> lessons)
		{
			Lessons = lessons;
		}
	}
}