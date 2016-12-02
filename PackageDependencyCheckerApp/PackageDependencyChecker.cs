using System;
using System.Collections.Generic;
using System.Linq;

namespace PackageDependencyCheckerApp
{
    public class PackageDependencyChecker
    {
        public string Processor(string input)
        {
            try
            {
                if(input == string.Empty)
                    throw new Exception("Please enter a input");

                if(!(input.Contains("[") && input.Contains("]")))
                    throw new Exception("Square Brackets format missing");
                
                if (!(input.StartsWith("[") && input.EndsWith("]")))
                    throw new Exception("Input should start with [ and end with ]");

                if (!(input.Count(f => f == '[') == 1 && input.Count(f => f == ']') == 1))
                    throw new Exception("Input cannot have multiple [ and ] ");
                
                if (input.Count(f => f == ':') > 1 && !input.Contains(","))
                    throw new Exception("Parameters not seperated by comma");

                var arrayOfDependencyPairs = input.Split('[', ']')[1].Split(',');
                
                Dictionary<string, string> inputPackageDictionary = new Dictionary<string, string>();

                HashSet<string> allPackagesHashSet = new HashSet<string>();
                foreach (string packageParameter in arrayOfDependencyPairs)
                {
                    if (packageParameter == string.Empty)
                        throw new Exception("Empty Package Names. Please check input");

                    if (!packageParameter.Contains(":"))
                        throw new Exception("Packages must be seperated by a colon. Invalid input format for prameter "+packageParameter);

                    if (packageParameter.StartsWith(":"))
                        throw new Exception("Package Name missing on left of :");

                    if (packageParameter.EndsWith(":"))
                        throw new Exception("Package Name missing on right of : (or) replace a black space on right of : ");

                    if (packageParameter.Count(f => f == ':') > 1)
                        throw new Exception("Package name pair can have only one colon. Invalid input format for prameter " + packageParameter);
                    
                    inputPackageDictionary.Add(packageParameter.Split(':')[0].Replace('\"', ' ').Trim(),
                        packageParameter.Split(':')[1].Replace('\"', ' ').Trim());
                    allPackagesHashSet.Add(packageParameter.Split(':')[0].Replace('\"', ' ').Trim());

                    if (packageParameter.Split(':')[1].Replace('\"', ' ').Trim() != string.Empty)
                        allPackagesHashSet.Add(packageParameter.Split(':')[1].Replace('\"', ' ').Trim());
                }

                HashSet<string> sortedHashSet = new HashSet<string>();
                
                foreach (var rootDependency in inputPackageDictionary.Where(x => x.Value == string.Empty).Select(x => x.Key))
                {
                    sortedHashSet.Add(rootDependency);

                    var tempVar = rootDependency;
                    bool addedSuccessfully;
                    do
                    {
                        tempVar =
                            inputPackageDictionary.Where(x => x.Value == tempVar).Select(x => x.Key).FirstOrDefault();

                        addedSuccessfully = tempVar != null && sortedHashSet.Add(tempVar);

                    } while (addedSuccessfully);
                }

                if (sortedHashSet.Count != allPackagesHashSet.Count)
                    return "Possible Looping of Dependencies (or) Dirty Input. Please check input";

                return string.Join(", ", sortedHashSet);
            }
            catch (Exception ex)
            {
               return ex.Message;
            }
        }
    }
}