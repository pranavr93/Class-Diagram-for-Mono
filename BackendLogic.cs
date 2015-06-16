using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.PatternMatching;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;

namespace ClassDiagram
{
    public class BackendLogic
    {
        private IEnumerable<Field> GetFieldObject(EntityDeclaration declaration)
        {
            FieldDeclaration fieldDeclaration = declaration as FieldDeclaration;
            string modifier = fieldDeclaration.Modifiers.ToString();
            string name = "";
            string returnType = fieldDeclaration.ReturnType.ToString();

            foreach (var variable in fieldDeclaration.Variables)
            {
                name = variable.Name;
                Field field = new Field(name, modifier, returnType);
                //Console.WriteLine("Modifier : " + modifier);
                //Console.WriteLine("Name  : " + name);
                //Console.WriteLine("Return type : " + returnType);
                //Console.WriteLine("\n\n");
                yield return field;
            }
        }
        private Method GetMethodObject(EntityDeclaration declaration)
        {
            MethodDeclaration methodDeclaration = declaration as MethodDeclaration;
            string modifier = methodDeclaration.Modifiers.ToString();
            string returnType = methodDeclaration.ReturnType.ToString();
            string methodName = methodDeclaration.Name;

            Method method = new Method(methodName, modifier, returnType);
            foreach(var param in methodDeclaration.Parameters)
            {
                //param.Name;
                Parameter p = new Parameter();
                p.name = param.Name;
                p.type = param.Type.ToString();
                method.Parameters.Add(p);
            }
            //Console.WriteLine("Modifier : " + modifier);
            //Console.WriteLine("Name  : " + methodName);
            //Console.WriteLine("\n\n");
            return method;
        }
        public UMLClass GenerateDiagramBackend(Solution solution)
        {
            UMLClass diagram = new UMLClass();            

            //Enums
            foreach(var file in solution.AllFiles)
            {
                var AllEnums = file.SyntaxTree.Descendants.OfType<TypeDeclaration>().Where(x => x.ClassType == ClassType.Enum);
                if (AllEnums == null) continue;
                foreach(var EachEnum in AllEnums)
                {
                    EnumNode node = new EnumNode();
                    node.EnumName = EachEnum.Name;
                    foreach(var member in EachEnum.Members)
                    {
                        node.AddMember(member.Name);
                    }
                    diagram.EnumNodes.Add(node);
                }
            }
            //Structs
            foreach (var file in solution.AllFiles)
            {
                var AllStructs = file.SyntaxTree.Descendants.OfType<TypeDeclaration>().Where(x => x.ClassType == ClassType.Struct);
                if (AllStructs == null) continue;
                //Iterating through each class in the file
                foreach (var EachStruct in AllStructs)
                {
                    StructNode node = new StructNode();
                    node.StructName = EachStruct.Name;
                    foreach (var link in EachStruct.BaseTypes)
                    {
                        node.Links.Add(link.ToString());
                    }
                    foreach (var member in EachStruct.Members)
                    {
                        var type = member.EntityType;
                        switch (type)
                        {
                            case EntityType.Field:
                                {
                                    foreach (Field field in GetFieldObject(member))
                                    {
                                        node.Fields.Add(field);
                                    }
                                    break;
                                }
                            case EntityType.Method:
                                {
                                    Method method = GetMethodObject(member);
                                    node.Methods.Add(method);
                                    break;
                                }
                        }
                    }
                    diagram.StructNodes.Add(node);
                }
            } 
            // Interfaces
            foreach(var file in solution.AllFiles)
            {
                var AllInterfaces = file.SyntaxTree.Descendants.OfType<TypeDeclaration>().Where(x => x.ClassType == ClassType.Interface);
                if (AllInterfaces == null) continue;
                foreach(var EachInterface in AllInterfaces)
                {
                    InterfaceNode node = new InterfaceNode();
                    node.InterfaceName = EachInterface.Name;
                    foreach (var link in EachInterface.BaseTypes)
                    {
                        node.Links.Add(link.ToString());
                    }
                    foreach(var member in EachInterface.Members)
                    {
                        Method method = GetMethodObject(member);
                        node.Methods.Add(method);
                    }
                    diagram.InterfaceNodes.Add(node);
                }
            }

            // Classes
            foreach (var file in solution.AllFiles)
            {
                var AllClasses = file.SyntaxTree.Descendants.OfType<TypeDeclaration>().Where(x => x.ClassType == ClassType.Class);
                if (AllClasses == null) continue;
                //Iterating through each class in the file
                foreach (var EachClass in AllClasses)
                {
                    ClassNode node = new ClassNode();
                    node.ClassName = EachClass.Name;
                    foreach (var link in EachClass.BaseTypes)
                    {
                        node.Links.Add(link.ToString());
                    }
                    foreach (var member in EachClass.Members)
                    {
                        var type = member.EntityType;
                        switch (type)
                        {
                            case EntityType.Field:
                                {
                                    foreach (Field field in GetFieldObject(member))
                                    {
                                        node.Fields.Add(field);
                                    }
                                    break;
                                }
                            case EntityType.Method:
                                {
                                    Method method = GetMethodObject(member);
                                    node.Methods.Add(method);
                                    break;
                                }
                        }
                    }
                    diagram.ClassNodes.Add(node);
                }               
            } 
            return diagram;
        }
        // End of class
    }   
}//End of namespace



#region previousAttempt
////Iterating through each field in the class
                    //foreach (var fieldDeclaration in file.SyntaxTree.Descendants.OfType<FieldDeclaration>())
                    //{

                    //    string modifier = fieldDeclaration.Modifiers.ToString();
                    //    string name = "";
                    //    string returnType = fieldDeclaration.ReturnType.ToString();

                    //    foreach (var a in fieldDeclaration.Variables)
                    //    {
                    //        name = a.Name;
                    //        Field field = new Field(name, modifier, returnType);
                    //        node.Fields.Add(field);
                    //        Console.WriteLine("\nClassName :" + node.ClassName);
                    //        Console.WriteLine("Modifier : " + modifier);
                    //        Console.WriteLine("Name  : " + name);
                    //        Console.WriteLine("Return type : " + returnType);
                    //        Console.WriteLine("\n\n");
                    //    }
                    //    file.IndexOfInvocations.Add(fieldDeclaration);
                    //}
                    //Console.WriteLine("Getting all methods");
                    ////Iterating through each method in the class
                    //foreach (var invocation in file.SyntaxTree.Descendants.OfType<MethodDeclaration>())
                    //{

//                    //    string modifier = invocation.Modifiers.ToString();
//                    //    string returnType = invocation.ReturnType.ToString();
//                    //    Method method = new Method(EachClass.Name, modifier, returnType);
//                    //    node.Methods.Add(method);
//                    //    Console.WriteLine("\nClassName :" + EachClass.Name);
//                    //    Console.WriteLine("Modifier : " + modifier);
//                    //    Console.WriteLine("Name  : " + node.ClassName);
//                    //    Console.WriteLine("\n\n");
//                    //    //	Console.WriteLine(invocation.GetRegion() + ": " + invocation.GetText());
//                    //    file.IndexOfInvocations.Add(invocation);
//                    //}//End of method invocations
//            //        diagram.nodes.Add(node);
//            //    }// End of each class
//            //}// End of each file
//            return diagram;
//        } // End of actual method
//    }
//}
#endregion 