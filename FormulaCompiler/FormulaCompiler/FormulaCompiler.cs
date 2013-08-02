using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;

namespace RuntimeCompiler.FormulaCompiler
{
    public class FormulaCompiler
    {
        private static CompilerMethods methods;
        private static CompilerMethods booleanMethods;

        public static CompilerMethods CompileFormula(string expr)
        {
            methods = null;
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            System.CodeDom.Compiler.CompilerParameters cpar = new CompilerParameters();
            cpar.GenerateInMemory = true;
            cpar.GenerateExecutable = false;
            cpar.ReferencedAssemblies.Add("system.dll");
            cpar.ReferencedAssemblies.Add(typeof(RuntimeCompiler.FormulaCompiler.FormulaCompiler).Assembly.Location);

            string src = "using System; using System.Collections.Generic;" +
             "class CompilerClass:RuntimeCompiler.FormulaCompiler.CompilerMethods" +
             "{" +
             "public override double ExecuteFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)" +
             "{" +
             "return " + expr + ";" +
             "}" +
             "}";

            CompilerResults cr = codeProvider.CompileAssemblyFromSource(cpar, src);

            if (cr.Errors.Count == 0 && cr.CompiledAssembly != null)
            {
                Type ObjType = cr.CompiledAssembly.GetType("CompilerClass");
                try
                {
                    if (ObjType != null)
                    {
                         methods = (CompilerMethods)Activator.CreateInstance(ObjType);
                         return methods;
                    }
                }
                catch { }
                return null;
            }
            else
            {
                throw new NotSupportedException("Formula is not correct: " + cr.Errors[cr.Errors.Count - 1].ErrorText);
            }
        }

        public static CompilerMethods CompileBoolFormula(string expr)
        {
            booleanMethods = null;
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            System.CodeDom.Compiler.CompilerParameters cpar = new CompilerParameters();
            cpar.GenerateInMemory = true;
            cpar.GenerateExecutable = false;
            cpar.ReferencedAssemblies.Add("system.dll");
            cpar.ReferencedAssemblies.Add(typeof(RuntimeCompiler.FormulaCompiler.FormulaCompiler).Assembly.Location);

            string src = "using System; using System.Collections.Generic;" +
             "class CompilerClass:RuntimeCompiler.FormulaCompiler.CompilerMethods" +
             "{" +
             "public override bool ExecuteBooleanFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)" +
             "{" +
             "return " + expr + ";" +
             "}" +
             "}";

            CompilerResults cr = codeProvider.CompileAssemblyFromSource(cpar, src);
            //foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
            //    Console.WriteLine(ce.ErrorText);
            if (cr.Errors.Count == 0 && cr.CompiledAssembly != null)
            {
                Type ObjType = cr.CompiledAssembly.GetType("CompilerClass");
                try
                {
                    if (ObjType != null)
                    {
                        booleanMethods = (CompilerMethods)Activator.CreateInstance(ObjType);
                        return booleanMethods;
                    }
                }
                catch { }
                return null;
            }
            else
                throw new NotSupportedException("Formula is not correct: " + cr.Errors[cr.Errors.Count - 1].ErrorText);
        }

        public static double ExecuteFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)
        {
            double val = 0.0;
            if (methods != null)
            {
                val = methods.ExecuteFormula(doubleValues, boolValues);
            }
            return val;
        }
        public static bool ExecuteBooleanFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)
        {
            bool val = false;
            if (booleanMethods != null)
            {
                val = booleanMethods.ExecuteBooleanFormula(doubleValues, boolValues);
            }
            return val;
        }

       
    }
}
