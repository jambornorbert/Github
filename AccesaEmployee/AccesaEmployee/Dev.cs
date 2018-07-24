using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public class Dev:Employee
	{
		private readonly List<string> _technologyStack = new List<string>();
		public List<string> TechnologyStack => _technologyStack;
        public const string XmlTehnology = "technology";
        public override void ReadXml(XmlReader r)
        {
            base.ReadXml(r);
            //_position = r.ReadElementContentAs("position", "");
            _position = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), r.ReadElementContentAsString("position", ""));
            r.ReadStartElement();

            if (r.Name == "TechnologyStack")
                while (r.NodeType == XmlNodeType.Element)
                {
                    if (r.Name == "TechnologyStack")
                        TechnologyStack.Add(r.ReadElementContentAsString("technology", ""));
                }
            r.ReadEndElement();
        }
        public override void WriteXml(XmlWriter w)
        {
            base.WriteXml(w);
            //w.WriteElementString($"{EmployeePosition.DEV}", _position.ToString());
            foreach (string tech in _technologyStack)
            {
                w.WriteStartElement(Dev.XmlTehnology);
                w.WriteElementString("technology", tech);
                w.WriteEndElement();
            }
        }
        public Dev(string name, float capacity)
            : base(name, EmployeePosition.DEV, capacity)
        {
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            var sb = new StringBuilder();
            _technologyStack.ForEach(x => sb.Append(x + ", "));
            Console.WriteLine("Technology stack: \r\n {0}", sb);
        }
    }
}
