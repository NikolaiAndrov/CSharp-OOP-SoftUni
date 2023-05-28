using CommandPattern.Core.Contracts;
using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {     
            string[] argsInfo = args.Split();
            string commandName = argsInfo[0];
            string[] commandArgs = argsInfo.Skip(1).ToArray();

            Assembly assembly = Assembly.GetEntryAssembly();
            Type[] types = assembly.GetTypes();
            Type commandToExecute = types
                .FirstOrDefault(t => t.Name == $"{commandName}Command");

            if (commandToExecute == null)
            {
                throw new InvalidOperationException("Invalid Command!");
            }

            MethodInfo methodToExecute =
                commandToExecute.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "Execute");

            if (methodToExecute == null)
            {
                throw new InvalidOperationException("Method does not exist!");
            }   

            object command = Activator.CreateInstance(commandToExecute);

            string commandOutput = (string)methodToExecute.Invoke(command, new object[] { commandArgs });
            return commandOutput;
        }
    }
}
