using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SearchInCSVUsingDotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * To be entered from Console App
                */
            //Console.WriteLine("Enter Path?");
            //var path = Convert.ToString(Console.ReadLine());
            //Console.WriteLine("\nEntered Path is: " + path + "\n");

            //Console.WriteLine("Enter Column number to execute search in?");
            //var searchColumnNumber = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("\nEntered Column number is: " + searchColumnNumber + "\n");

            //Console.WriteLine("Enter Value to be Searched?");
            //var valueToBeSearched = Convert.ToString(Console.ReadLine());
            //Console.WriteLine("\nEntered Value to be Searched is: " + valueToBeSearched + "\n");

            if (args != null && args.Length > 0)
            {
                //Command line Parameters
                var path = string.Concat(args[0], " ", args[1]);
                var searchColumnNumber = Convert.ToInt64(args[2]);
                var valueToBeSearched = args[3];

                var results = new List<string>();
                var keyValuePairs = new List<KeyValuePair<string, string>>();

                if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(valueToBeSearched) || searchColumnNumber <= 0)
                {
                    Console.WriteLine("\nPlease enter valid input values!");
                    return;
                }

                //File reading
                using (var reader = new StreamReader(path))
                {
                    while (!(reader.EndOfStream))
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (values != null && values.Length > 0 && searchColumnNumber <= values.Length)
                        {
                            keyValuePairs.Add(new KeyValuePair<string, string>(values[searchColumnNumber], line));
                        }
                        else
                        {
                            Console.WriteLine("\nPlease enter a valid column number for the value to be searched in.");
                            return;
                        }
                    }

                    //Search execution
                    if (keyValuePairs.Count > 0)
                    {
                        if (keyValuePairs.Exists(x => x.Key.Trim(';').ToLower().Equals(valueToBeSearched.ToLower())))
                        {
                            var pairs = keyValuePairs.Where(x => x.Key.Trim(';').ToLower().Equals(valueToBeSearched.ToLower())).ToList();
                            if (pairs != null && pairs.Count > 0)
                            {
                                pairs.ForEach(x => results.Add(x.Value));
                            }

                            results.ForEach(x => Console.WriteLine(x));
                        }
                        else
                        {
                            Console.WriteLine("\nNo match found.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nPlease add comma seperated rows in the csv file.");
                        return;
                    }
                }
            }
        }
    }
}
