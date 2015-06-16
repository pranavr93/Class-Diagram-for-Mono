//Pranav Ramarao

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.PatternMatching;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;

namespace ClassDiagram
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify the path to a .sln file on the command line");
                Console.Write("Press any key to continue . . . ");
                Console.ReadKey(true);
                return;
            }
            Solution solution = new Solution(args[0]);
            BackendLogic logic = new BackendLogic();
            UMLClass result = logic.GenerateDiagramBackend(solution);
            UMLTree tree = new UMLTree(result);
            tree.BuildGraph();
        } // End of main
    }
}