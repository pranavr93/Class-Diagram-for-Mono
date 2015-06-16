using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram
{
    interface INode
    {
        string GetName();
        bool isPartOfProject();
    }
}
