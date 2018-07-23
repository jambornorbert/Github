using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public  class Employee
	{
        public const string XmlName = "employees";
		private  string _name;
		private  EmployeePosition _position;
		private  float _capacity;
		private readonly List<string> _hobbies=new List<string>();

		public string Name => _name;
		public EmployeePosition Position => _position;
		public float Capacity => _capacity;
		public List<string> Hobbies => _hobbies;

		protected Employee(string name, EmployeePosition position, float capacity)
		{
			_name = name;
			_position = position;
			_capacity = capacity;
		}

        public virtual void DisplayInfo()
		{
			var sb= new StringBuilder();
			_hobbies.ForEach(x=>sb.Append(x+" "));
			Console.WriteLine($"{_name} ocupa pozitia {_position} si e angajat cu {_capacity} ore pe zi. Lui ii place {sb.ToString()}");
		}

	}
}
