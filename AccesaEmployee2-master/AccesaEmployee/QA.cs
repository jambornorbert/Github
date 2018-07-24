using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
	public class QA:Employee
	{

		private List<string> _testingTools = new List<string>();
		public List<string> TestingTools => _testingTools;
        public const string XmlTesting = "tool";
        public override void ReadXml(XmlReader r)
        {
            base.ReadXml(r);
            //_position = r.ReadElementContentAsString("position", "");
           // _position = (EmployeePosition)Enum.Parse(typeof(EmployeePosition), r.ReadElementContentAsString("position", ""));
            r.ReadStartElement();

            if (r.Name == "TestingTools")
                while (r.NodeType == XmlNodeType.Element)
                {
                    if (r.Name == "TestingTools")
                        TestingTools.Add(r.ReadElementContentAsString("tool", ""));
                }
            r.ReadEndElement();
            //_testingTools = r.ReadElementContentAsString("testingtools", "");
        }
        public override void WriteXml(XmlWriter w)
        {
            base.WriteXml(w);
            //w.WriteElementString($"{EmployeePosition.QA}", _position.ToString());
            foreach(string tool in _testingTools)
            {
                w.WriteStartElement(QA.XmlTesting);
                w.WriteElementString("tool", tool);
                w.WriteEndElement();
            }
        }
        public QA(string name, float capacity) : base(name, EmployeePosition.QA, capacity)
        {
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            var sb = new StringBuilder();
            _testingTools.ForEach(x => sb.Append(x + ", "));
            Console.WriteLine("Testing tools experience: \r\n {0}", sb);
        }
    }
}
