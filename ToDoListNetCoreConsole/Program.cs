using System;
using System.Collections.Generic;

namespace ToDoListNetCoreConsole
{
    class Program
    {
        static string line, command, parameters;
        static List<string> todoList = new List<string>();
        static int indexOfFisrtWhitespace = 0;

        static void Main(string[] args)
        {
            do
            {
                PrintMenu();
                line = Console.ReadLine();
                try
                {
                    indexOfFisrtWhitespace = line.IndexOf(' ');
                    command = line.Substring(0, indexOfFisrtWhitespace);
                    
                }
                catch (IndexOutOfRangeException ex)
                {
                    command = line;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    command = line;
                }

                switch (command.ToLower())
                {
                    case "add":
                        parameters = line.Substring(indexOfFisrtWhitespace + 1);
                        Add(parameters);
                        break;
                    case "print":
                        Print();
                        break;
                    case "printall":
                        PrintAll();
                        break;
                    case "done":
                        parameters = line.Substring(indexOfFisrtWhitespace + 1);
                        Done(parameters);
                        break;
                    case "reorder":
                        int actualPosition = GetFirstArgument(line);
                        int newPosition = GetSecondArgument(line);
                        Reorder(actualPosition - 1, newPosition - 1);
                        break;
                    case "quit":
                        break;
                    default:
                        Console.WriteLine("Error: Command not reognized");
                        PrintMenu();
                        break;
                } 
            } while (command.ToLower() != "quit");

        }

        private static void Reorder(int actualPosition, int newPosition)
        {
            //Console.WriteLine(actualPosition.ToString());
            //Console.WriteLine(newPosition.ToString());
            var item = todoList[actualPosition];
            todoList.RemoveAt(actualPosition);
            todoList.Insert(newPosition, item);
            PrintAll();
        }

        private static int GetSecondArgument(string line)
        {
            int firstWhiteIndex = line.IndexOf(' ');
            int secondWhiteIndex = line.IndexOf(' ', firstWhiteIndex + 1);
            var secondArgument = line.Substring(secondWhiteIndex);
            if (int.TryParse(secondArgument, out int position))
            {
                return position;
            }
            else
            {
                return 0;
            }
        }

        private static int GetFirstArgument(string line)
        {
            int firstWhiteIndex = line.IndexOf(' ');
            int secondWhiteIndex = line.IndexOf(' ', firstWhiteIndex + 1);
            var firstArgument = line.Substring(firstWhiteIndex + 1, secondWhiteIndex - (firstWhiteIndex + 1));
            if ( int.TryParse(firstArgument, out int position) )
            {
                return position;
            }
            else
            {
                return 0;
            }
        }

        private static void Done(string parameters)
        {
            int i;
            Console.WriteLine();
            if (int.TryParse(parameters, out i))
            {
                if (i < todoList.Count)
                {
                    todoList.RemoveAt(i - 1);
                    Console.WriteLine("Item " + i + " has been removed successfully. New list is:");
                    PrintAll();
                }
                else
                {
                    Console.WriteLine("Error: item number " + i + "does not exist in ToDo list");
                }

            }
            else
            {
                Console.WriteLine("Error: invalid item number");
            }
            Console.WriteLine();
        }

        private static void PrintAll()
        {
            Console.WriteLine();
            for (int i = 0; i < todoList.Count; i++)
            {
                Console.WriteLine($"\t{i + 1}: " + todoList[i]);
            }
            Console.WriteLine();
        }

        private static void Print()
        {
            Console.WriteLine();
            for (int i = 0; i < todoList.Count; i++)
            {
                Console.WriteLine($"\t{i+1}: " + todoList[i]);
                if (i == 2) break;
            }
            Console.WriteLine();
        }

        private static void Add(string p)
        {
            todoList.Add(p);
            Console.WriteLine();
            Console.WriteLine("\t" + p + "has been added to ToDo list at position " + todoList.Count);
            Console.WriteLine();
        }




        static void PrintMenu()
        {
            Console.WriteLine("Add <todo>");
            Console.WriteLine("Print");  //prints the first 3 items
            Console.WriteLine("PrintAll");
            Console.WriteLine("Done <todo number>");
            Console.WriteLine("ReOrder <item nr> <desired position>");
            Console.WriteLine("Quit");
            Console.WriteLine();
            
        }
    }
}
