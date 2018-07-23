using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesaEmployee
{
	public class Intern:Employee
	{
		private string _universityName;
		private int _yearOfStudy;
		private EmployeePosition _targetPosition;

		public string UniversityName => _universityName;
		public int YearOfStudy => _yearOfStudy;
		public EmployeePosition TargetPosition => _targetPosition;
		public Intern(string name, float capacity) 
			: base(name, EmployeePosition.Intern, capacity)
		{
		}
	}
}
