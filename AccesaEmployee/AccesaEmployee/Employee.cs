using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{   [DataContract]
	public class Employee
	{
        [DataMember]
        private  string _name;
        [DataMember]
        public  EmployeePosition _position;
        [DataMember]
        private  float _capacity;//max number of hours per day
        [DataMember]
        private  List<string> _hobbies = new List<string>();

        public Employee() { }
        public Employee(XmlReader r) { ReadXml(r); }

        public const string XmlName = "employee";
        public const string XmlHobbies = "hobby";

        public virtual void ReadXml(XmlReader r)
        { 
            r.ReadStartElement();
            _name = r.ReadElementContentAsString("name", "");
            //_position = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), r.ReadElementContentAsString("position", ""));
            _capacity = r.ReadElementContentAsFloat("capacity", "");
            r.ReadStartElement();

            if (r.Name == "Hobbies")
                while (r.NodeType == XmlNodeType.Element)
                {
                    if (r.Name == "Hobbies")
                        Hobbies.Add(r.ReadElementContentAsString("hobby", ""));
                }
            r.ReadEndElement();
            r.ReadEndElement();
        }
        public virtual void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("employee");
            w.WriteElementString("name", _name);
            w.WriteElementString("capacity", _capacity.ToString());
            w.WriteStartElement("hobbies"); 
            foreach (string hobby in _hobbies)
            {
                w.WriteElementString("hobby", hobby);
            }
            w.WriteEndElement();
            w.WriteElementString("position", _position.ToString());
           // w.WriteEndElement();
        }
        public string Name { get { return _name; } set { _name = value; } }
		public EmployeePosition Position { get { return _position; } set { _position = value; } }
        public float Capacity { get { return _capacity; } set { _capacity = value; } }
        public List<string> Hobbies => _hobbies;

        protected Employee(string name, EmployeePosition position, float capacity)
        {
            _name = name;
            _position = position;
            _capacity = capacity;
        }

        public virtual void DisplayInfo()
        {
            var sb = new StringBuilder();
            _hobbies.ForEach(x => sb.Append(x + " "));
            Console.WriteLine($"{_name} ocupa pozitia {_position} si e angajat cu {_capacity} ore pe zi. Lui ii place {sb.ToString()}");
        }
    }
}
