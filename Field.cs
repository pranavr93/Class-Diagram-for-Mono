using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram
{
    public class Field
    {
        public string Name;
        public string Modifier;
        public string ReturnType;
        public Field(string Name, string Modifier, string ReturnType){
            this.Name = Name;
            this.Modifier = Modifier;
            this.ReturnType = ReturnType;
        }
    }
}
