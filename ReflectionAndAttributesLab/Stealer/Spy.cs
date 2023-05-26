namespace Stealer
{
    using System.Reflection;
    using System.Text;
    public class Spy
    {
        public string StealFieldInfo(string typeToInvestigate, params string[] fieldsToInvestigate)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType(typeToInvestigate);
            sb.AppendLine($"Class under investigation: {type.FullName}");

            FieldInfo[] fields = type.GetFields(BindingFlags.Public 
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Static);

            object instance = Activator.CreateInstance(type, new object[] { });

            foreach (FieldInfo field in fields)
            {
                if (fieldsToInvestigate.Contains(field.Name))
                {
                    sb.AppendLine($"{field.Name} = {field.GetValue(instance)}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            Type type = Type.GetType(className);

            FieldInfo[] fields = type.GetFields(BindingFlags.Public 
                | BindingFlags.Instance
                | BindingFlags.Static);

            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Instance
                | BindingFlags.Public);

            MethodInfo[] privateMethods = type.GetMethods(BindingFlags.Instance
                | BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();
            
            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (var method in privateMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            foreach (var method in publicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType(className);
            sb.AppendLine($"All Private Methods of Class: {type.FullName}");
            sb.AppendLine($"Base Class: {type.BaseType.Name}");

            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var method in methods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().TrimEnd();
        }

        public string CollectGettersAndSetters(string className)
        {
            Type type = Type.GetType(className);

            MethodInfo[] methods = type.GetMethods(BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Static);

            StringBuilder sb = new StringBuilder();

            foreach (var method in methods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }

            foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
