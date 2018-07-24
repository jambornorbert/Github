using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public override void ReadXml(XmlReader r)
        {
            base.ReadXml(r);
            //_position = r.ReadElementContentAsString("position", "");
            _position = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), r.ReadElementContentAsString("position", ""));
            _universityName = r.ReadElementContentAsString("universityname","");
            _yearOfStudy = int.Parse(r.ReadElementContentAsString("universityname", ""));

        }
        public override void WriteXml(XmlWriter w)
        {
            base.WriteXml(w);
            
            //w.WriteElementString("position", EmployeePosition.Intern.ToString());
            w.WriteElementString("universityName", _universityName);
            w.WriteElementString("yearOfStudy",_yearOfStudy.ToString());
            w.WriteEndElement();
        }
        public Intern(string name, float capacity)
            : base(name, EmployeePosition.Intern, capacity)
        {
        }
    }
}
