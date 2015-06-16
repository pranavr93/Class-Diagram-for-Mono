using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram
{
    public class EnumNode : INode
    {
        public string EnumName;
        List<String> Members;
        private bool inProject;
        public EnumNode()
        {
            Members = new List<String>();
        }
        public void AddMember(string member)
        {
            Members.Add(member);
        }
        public string GetName()
        {
            return EnumName;
        }
        public bool isPartOfProject()
        {
            return inProject;
        }
    }
}
