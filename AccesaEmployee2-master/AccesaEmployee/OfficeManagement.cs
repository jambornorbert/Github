using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace AccesaEmployee
{
    public class OfficeManagement
    {
        private readonly List<Employee> _employees = new List<Employee>();
        private readonly List<Project> _projects = new List<Project>();

        public IReadOnlyCollection<Employee> Employees => _employees;
        public IReadOnlyCollection<Project> Projects => _projects;

        //public void WriteJSON()
        //{
        //    MemoryStream stream1 = new MemoryStream();
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
        //    ser.WriteObject(stream1, p);
        //}

        //public void SerializeJSON()
        //{

        //    ////Create User object.  
        //    Employee emp = new Employee();

        //    string content = JsonConvert.SerializeObject(emp);
        //    File.WriteAllText("office.json", content);

        ////Create a stream to serialize the object to.  
        //MemoryStream ms = new MemoryStream();

        //// Serializer the User object to the stream.  
        //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
        //ser.WriteObject(ms, emp);
        //byte[] json = ms.ToArray();
        //ms.Close();
        //return Encoding.UTF8.GetString(json, 0, json.Length);
        //}

        // Deserialize a JSON stream to a User object.  
        //public static Employee ReadToObject(string json)
        //{
        //    Employee deserializedUser = new Employee();
        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
        //    deserializedUser = ser.ReadObject(ms) as Employee;
        //    ms.Close();
        //    return deserializedUser;
        //}

        public void ReadXml(XmlReader r)
        {
            bool isEmpty = r.IsEmptyElement;
            r.ReadStartElement();
            if (isEmpty) return;
            while (r.NodeType == XmlNodeType.Element)
            {
                if (r.Name == Employee.XmlName) _employees.Add(new Employee(r));
                else if (r.Name == Project.XmlName) _projects.Add(new Project(r));
                else
                    throw new XmlException("Unexpected node: " + r.Name);
            }
            r.ReadEndElement();
        }
        public void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("employees");
            foreach (Employee e in _employees)
            {
                //w.WriteStartElement(Employee.XmlName);
                e.WriteXml(w);
                //w.WriteEndElement();
            }
            w.WriteEndElement();
            foreach (Project s in _projects)
            {
                w.WriteStartElement(Project.XmlName);
                s.WriteXml(w);
                w.WriteEndElement();
            }
        }



        public Employee AddEmployee(string name, EmployeePosition position, float capacity, List<string> hobbies)
        {
            Employee employee;
            switch (position)
            {
                case EmployeePosition.DEV:
                    employee = new Dev(name, capacity);
                    employee.Hobbies.AddRange(hobbies);
                    _employees.Add(employee);

                    break;
                case EmployeePosition.Intern:
                    employee = new Intern(name, capacity);
                    employee.Hobbies.AddRange(hobbies);
                    _employees.Add(employee);

                    break;
                case EmployeePosition.QA:
                default:
                    employee = new QA(name, capacity);
                    employee.Hobbies.AddRange(hobbies);
                    _employees.Add(employee);
                    break;
            }
            return employee;
        }

        public Project AddProject(string name, string description, DateTime deadLine)
        {
            var project = new Project(name, description, deadLine);
            _projects.Add(project);
            return project;
        }

        public bool AddEmployeeToProject(Employee employee, float noOfHours, Project project)
        {
            if (project.DeadLine < DateTime.Now || project.Team.Keys.Contains(employee))
                return false;

            //var currentAllocationHours = _projects.Where(x => x.Team.Keys.Contains(employee))
            //    .Select(x => x.Team[employee])
            //    .Sum();
            var currentAllocationHours = (from x in _projects
                                          where x.Team.Keys.Contains(employee)
                                          select x.Team[employee]).Sum();

            if (employee.Capacity < currentAllocationHours + noOfHours)
                return false;

            project.AddTeamMember(employee, noOfHours);

            return true;
        }

        public void DeleteEmployee(Employee employee)
        {
            _projects.ForEach(x => x.DeleteTeamMember(employee));
            _employees.Remove(employee);
        }

        public void DisplayAllEmployees()
        {
            _employees.ForEach(x => x.DisplayInfo());
        }

        public void DisplayAllProjects()
        {
            _projects.ForEach(x => x.DisplayInfo());
        }
    }
}

