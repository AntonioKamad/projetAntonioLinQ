using System;
using DataSources;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace LinqExercicePresentation
{
    class Functions
    {
        public void fileSelection(string error)
        {
            // clean the console and write the title
            Console.Clear();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("           Languages That Are In Danger Of Extinction              ");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine(error);

            Console.WriteLine("----------------------------File Selection-----------------------------");
            Console.WriteLine("");
            Console.WriteLine("Insert the file name with the data.File must be .csv or .txt");
            Console.WriteLine(@$"*The file must be saved in the project in ProjetAntonio\DataSources\Input");
            Console.WriteLine("*File must be .csv or .txt");
            Console.WriteLine("**Example : 'Extinct-languages.csv' or 'test.txt' ");
            Console.WriteLine("");

            string input = Console.ReadLine();

            string filePathType = input.Split('.').Last();
            Console.WriteLine(filePathType);

            string path = Directory.GetCurrentDirectory();
            path = path.Split($@"ProjetAntonio\").First();
            path = path + @$"ProjetAntonio\DataSources\Input\"+ input;

            Console.WriteLine($"Path :  {path}");

            //Input verification, it has to be txt or csv
            if (filePathType == "txt" || filePathType == "csv")
            {
                    //To read the cvs file
                    var csvLines = File.ReadAllLines(path);

                    foreach (var ln in csvLines)
                    {
                        //Separete the line by ";" to get every element of the line
                        var column = ln.Split(';');

                        // Assign a 0 value to all the fields that should contain an int or double
                        // but have no value, so that we can parse them to create the object
                        if (column[8] == "")
                        {
                            column[8] = "0";
                        }
                        if (column[9] == "")
                        {
                            column[9] = "0";
                        }
                        if (column[10] == "")
                        {
                            column[10] = "0";
                        }

                        //Add each element of the line to the list of languages to get an object
                        ListLanguagesData.ListLanguages.Add(new Languages(int.Parse(column[0]), column[1], column[2], column[3], column[4], column[5], column[6], column[7], int.Parse(column[8]), double.Parse(column[9]), double.Parse(column[10])));
                    }

                mainMenu("", path);
            }
            else
            {
                Console.WriteLine("file format not supported");
                Console.WriteLine("Restarting programme...");
                fileSelection("File format not supported, select a valid file format please");
            }
        }
        public void mainMenu(string error, string path)
        {
            //Main menu display
            Console.Clear();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("           Languages That Are In Danger Of Extinction              ");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine(error);

            Console.WriteLine("Input file Path : " + path);
            Console.WriteLine("");
            Console.WriteLine("Select the action you wish to perform by number :");
            Console.WriteLine(" 1) Transform a file");
            Console.WriteLine(" 2) Make a research");
            Console.WriteLine(" 3) Change InputFile");
            Console.WriteLine("");

            string input = Console.ReadLine();
            int input_nb = int.Parse(input);

            bool isNumeric = int.TryParse(input, out input_nb);

            //Input verification, it has to be a number
            if (!isNumeric)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                mainMenu("Parsing error, select a valid number please", path);
            }
            //if it is a number but the number is not in the menu
            if (input_nb > 3 || input_nb <= 0)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                mainMenu("Parsing error, select a valid number please", path);
            }

            switch (input_nb)
            {
                case 1:
                    convertMenu("", path);
                    break;
                case 2:
                    querySubMenu("", "", path);
                    break;
                case 3:
                    fileSelection("");
                    break;
            }
        }

        public void convertMenu(string error,string path)
        {
            // clean the console and write the title
            Console.Clear();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("           Languages That Are In Danger Of Extinction              ");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine(error);

            Console.WriteLine("----------------------------Convert Menu-----------------------------");
            Console.WriteLine("");
            Console.WriteLine("Select one of the fields by number,  or press 0 to go back to the previous menu");
            Console.WriteLine("Available types of files to transform to are :");
            Console.WriteLine("");
            Console.WriteLine("1) Text");
            Console.WriteLine("2) Json");
            Console.WriteLine("3) XML");
            Console.WriteLine("");

            string input = Console.ReadLine();
            int input_nb = int.Parse(input);

            string fileType = "";

            bool isNumeric = int.TryParse(input, out input_nb);

            //Input verification, it has to be a number
            if (!isNumeric)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                convertMenu("Parsing error, select a valid number please", path);
            }
            //If the input is a number but it is not in the menu
            if (input_nb > 3 || input_nb < 0)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                convertMenu("Parsing error, select a valid number please", path);
            }

            switch (input_nb)
            {
                case 0:
                    mainMenu("", path);
                    break;
                case 1:
                    fileType = "Text";
                    convertSubMenu("", fileType, path);
                    break;
                case 2:
                    fileType = "Json";
                    convertSubMenu("", fileType, path);
                    break;
                case 3:
                    fileType = "Xml";
                    convertSubMenu("", fileType, path);
                    break;
            }
        }

        public void convertSubMenu(string error, string fileType, string path)
        {
            // clean the console and write the title
            Console.Clear();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("           Languages That Are In Danger Of Extinction              ");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine(error);
            Console.WriteLine($"--------------------------{fileType} File---------------------------");

            Console.WriteLine("");

            string csvLinesFile = path;

            if (fileType == "Xml")
            {
                convertToXml(csvLinesFile);
            }
            if (fileType == "Json")
            {
                convertToJson(csvLinesFile);
            }
            if(fileType == "Text")
            {
                convertToText(csvLinesFile);
            }

        }

        public void convertToText(string fileLocation)
        {
            Console.Clear();

            // Read the file into a string array
            if (fileLocation.Split('.').Last() == "txt")
            {
                Console.WriteLine("File is already in txt format");
                Console.WriteLine(" ");
            }

            string[] source = File.ReadAllLines(fileLocation);
            string txt;

            string pathNoExtension = fileLocation.Split('.').First();
            string fileNameP = pathNoExtension.Split(@"\").Last();
            string pathPreConversion = pathNoExtension.Split(@"\" + fileNameP).First();
            string pathConversion = pathPreConversion + @"\conversions\";
            string pathTxt = pathConversion + fileNameP + ".txt";
            string[] txtF = { };

            foreach (string str in source)
            {
                txt = str;

                if(fileLocation.Split('.').Last() != "txt")
                {
                    string txtFi = txt + "\n";
                    File.AppendAllText(pathTxt, txtFi);
                }
                Console.WriteLine(txt);
            }

            Console.WriteLine("");
            Console.WriteLine("Press 0 to return to Main Menu, or any other key (exept ESC) to quit (2 for example)");
            Console.WriteLine("");

            string input = Console.ReadLine();

            int input_nb = int.Parse(input);

            bool isNumeric = int.TryParse(input, out input_nb);

            //If the input is a number but it is not in the menu
            if (input_nb == 0)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                mainMenu("", fileLocation);
            }
            //Input verification, it has to be a number
            if (!isNumeric || input_nb != 1)
            {
                Console.WriteLine("Exiting Programme");
                return;
            }
        }

        public void convertToJson(string fileLocation)
        {
            Console.Clear();

            var newJsonFile =
             new JObject(
                 new JProperty("LanguagesInDanger",
                 from language in ListLanguagesData.ListLanguages
                 select new JObject(
                    new JProperty("lanID", language.LanId),
                    new JProperty("enName", language.EnName),
                    new JProperty("frName", language.FrName),
                    new JProperty("esName", language.EsName),
                    new JProperty("countries", language.Countries),
                    new JProperty("countryCdeA3", language.CountryCdeA3),
                    new JProperty("isoCdes", language.IsoCdes),
                    new JProperty("dangerLevel", language.DangerLevel),
                    new JProperty("nbSpeakers", language.NbSpeakers),
                    new JProperty("latitude", language.Latitude),
                    new JProperty("longitude", language.Longitude)))
            );

            Console.WriteLine(newJsonFile);
            string pathNoExtension = fileLocation.Split('.').First();

            string fileNameP = pathNoExtension.Split(@"\").Last();
            string pathPreConversion = pathNoExtension.Split(@"\" + fileNameP).First();
            string pathConversion = pathPreConversion + @"\conversions\";
            string pathJson = pathConversion + fileNameP + ".json";

            File.WriteAllText(pathJson, newJsonFile.ToString());

            Console.WriteLine("");
            Console.WriteLine("Press 0 to return to Main Menu, or any other key (exept ESC) to quit (2 for example)");
            Console.WriteLine("");

            string input = Console.ReadLine();

            int input_nb = int.Parse(input);

            bool isNumeric = int.TryParse(input, out input_nb);

            //If the input is a number but it is not in the menu
            if (input_nb == 0)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                mainMenu("", fileLocation);
            }
            //Input verification, it has to be a number
            if (!isNumeric || input_nb != 1)
            {
                Console.WriteLine("Exiting Programme");
                return;
            }
        }

        public void convertToXml(string fileLocation)
        {
            Console.Clear();

            // Read the file into a string array
            string[] source = File.ReadAllLines(fileLocation);
            XElement lan = new XElement("Root",
                from str in source
                let fields = str.Split(';')
                select new XElement("LanguageinDanger",
                    new XElement("lanID", fields[0]),
                    new XElement("enName", fields[1]),
                    new XElement("frName", fields[2]),
                    new XElement("esName", fields[3]),
                    new XElement("countries", fields[4]),
                    new XElement("countryCdeA3", fields[5]),
                    new XElement("isoCdes", fields[6]),
                    new XElement("dangerLevel", fields[7]),
                    new XElement("nbSpeakers", fields[8]),
                    new XElement("latitude", fields[9]),
                    new XElement("longitude", fields[10])
               )
            );
            Console.WriteLine(lan);
            string pathNoExtension = fileLocation.Split('.').First();

            string fileNameP = pathNoExtension.Split(@"\").Last();
            string pathPreConversion = pathNoExtension.Split(@"\" + fileNameP).First();
            string pathConversion = pathPreConversion + @"\conversions\";
            string pathXml = pathConversion + fileNameP + ".xml";

            //string pathXml = pathNoExtension + ".xml";
            File.WriteAllText(pathXml, lan.ToString());

            Console.WriteLine("");
            Console.WriteLine("Press 0 to return to Main Menu, or any other key (exept ESC) to quit (2 for example)");
            Console.WriteLine("");

            string input = Console.ReadLine();

            int input_nb = int.Parse(input);

            bool isNumeric = int.TryParse(input, out input_nb);

            //If the input is a number but it is not in the menu
            if (input_nb == 0)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                mainMenu("", fileLocation);
            }
            //Input verification, it has to be a number
            if (!isNumeric || input_nb != 1)
            {
                Console.WriteLine("Exiting Programme");
                return;
            }
        }

        public void querySubMenu(string error, string select, string path)
        {
            // clean the console and write the title
            Console.Clear();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("           Languages That Are In Danger Of Extinction              ");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine(error);
            Console.WriteLine("--------------------------Query Sub Menu---------------------------");
            Console.WriteLine("");
            Console.WriteLine($"Now select one of following, or press 0 to go back to the previous menu");
            Console.WriteLine("fields available from the dataset are :");
            Console.WriteLine("");
            Console.WriteLine($"1) All elements");
            Console.WriteLine($"2) Elements containing a specific value (All fields)");
            Console.WriteLine($"3) Elements containing a specific value (Only Name, Country, Danger Level and number of Speakers fields)");
            Console.WriteLine("");

            string input = Console.ReadLine();
            int input_nb = int.Parse(input);

            bool isNumeric = int.TryParse(input, out input_nb);
            string allOrContain = "";

            //Input verification, it has to be a number
            if (!isNumeric)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
            }
            //If the input is a number but it is not in the menu
            if (input_nb > 3 || input_nb < 0)
            {
                Console.WriteLine("Parsing error");
                Console.WriteLine("Restarting programme...");
                querySubMenu("Parsing error, select a valid number please", select, path);
            }

            switch (input_nb)
            {
                case 0:
                    mainMenu("", path);
                    break;
                case 1:
                    allOrContain = "all";
                    queryMaker("", select, allOrContain, path);
                    break;
                case 2:
                    allOrContain = "contain";
                    queryMaker("", select, allOrContain, path);
                    break;
                case 3:
                    allOrContain = "filter";
                    queryMaker("", select, allOrContain, path);
                    break;

            }
            
        }
            

        /* Function to process the selected query*/
        public void queryMaker(string error, string select, string allOrContain, string path)
        {
            // clean the console and write the title
            Console.Clear();
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("           Languages That Are In Danger Of Extinction              ");
            Console.WriteLine("*******************************************************************");
            Console.WriteLine(error);
            Console.WriteLine("--------------------------Your Query---------------------------");
            Console.WriteLine("");

            //Creation of a variable containeng the element of the object
            var listLanguagesTest = ListLanguagesData.ListLanguages;

            if(allOrContain == "all")
            {
                //Creation of display query
                var display = from languages in listLanguagesTest
                              select $"\x1b[94mId:\x1b[39m {languages.LanId} \x1b[94menName:\x1b[39m {languages.EnName} \x1b[94mfrName:\x1b[39m {languages.FrName} \x1b[94mesName:\x1b[39m {languages.EsName} " +
                                $"\x1b[94mCountries:\x1b[39m {languages.Countries} \x1b[94mCountryCde:\x1b[39m {languages.CountryCdeA3} \x1b[94mIsoCde:\x1b[39m {languages.IsoCdes} " +
                                $"\x1b[94mDanger Level:\x1b[39m {languages.DangerLevel} \x1b[94mnbSpeakers:\x1b[39m {languages.NbSpeakers} " +
                                $"\x1b[94mLatitude:\x1b[39m {languages.Latitude} \x1b[94mLongitude:\x1b[39m {languages.Longitude}";

                foreach (var lang in display)
                {
                    Console.WriteLine($"{lang}");
                }

                Console.WriteLine("");
                Console.WriteLine("Press 0 to return to Main Menu, or any other key (exept ESC) to quit (2 for example)");
                Console.WriteLine("");

                string input = Console.ReadLine();

                int input_nb = int.Parse(input);

                bool isNumeric = int.TryParse(input, out input_nb);

                //If the input is a number but it is not in the menu
                if (input_nb == 0)
                {
                    Console.WriteLine("Parsing error");
                    Console.WriteLine("Restarting programme...");
                    mainMenu("", path);
                }
                //Input verification, it has to be a number
                if (!isNumeric || input_nb != 1)
                {
                    Console.WriteLine("Exiting Programme");
                    return;
                }

            }
            if(allOrContain == "contain" || allOrContain == "filter")
            {
                Console.WriteLine("Enter the search criteria :");
                Console.WriteLine("");

                string search = Console.ReadLine();
                Console.WriteLine("");

                //Creation of display query
                if (allOrContain == "contain")
                {
                    var display = from languages in listLanguagesTest
                                  let inLan = languages.LanId.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.EnName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.FrName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.EsName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.Countries.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.CountryCdeA3.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.IsoCdes.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.DangerLevel.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.NbSpeakers.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.Latitude.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.Longitude.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase)

                                  where inLan

                                  select $"\x1b[94mId:\x1b[39m {languages.LanId} \x1b[94menName:\x1b[39m {languages.EnName} \x1b[94mfrName:\x1b[39m {languages.FrName} \x1b[94mesName:\x1b[39m {languages.EsName} " +
                                      $"\x1b[94mCountries:\x1b[39m {languages.Countries} \x1b[94mCountryCde:\x1b[39m {languages.CountryCdeA3} \x1b[94mIsoCde:\x1b[39m {languages.IsoCdes} " +
                                      $"\x1b[94mDanger Level:\x1b[39m {languages.DangerLevel} \x1b[94mnbSpeakers:\x1b[39m {languages.NbSpeakers} " +
                                      $"\x1b[94mLatitude:\x1b[39m {languages.Latitude} \x1b[94mLongitude:\x1b[39m {languages.Longitude}";

                    if (display.Any() != true)
                    {
                        Console.WriteLine($"The word '{search}' was not found");
                    }
                    else
                    {
                        foreach (var lang in display)
                        {
                            Console.WriteLine($"{lang}");
                        }
                    }
                }
                else
                {
                    var display = from languages in listLanguagesTest
                                  let inLan = languages.LanId.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.EnName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.FrName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.EsName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.Countries.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.CountryCdeA3.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.IsoCdes.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.DangerLevel.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.NbSpeakers.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.Latitude.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                                  languages.Longitude.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase)

                                  where inLan
                                  select $"\x1b[94mName:\x1b[39m {languages.EnName} " +
                              $"\x1b[94mCountries:\x1b[39m {languages.Countries} " +
                              $"\x1b[94mDanger Level:\x1b[39m {languages.DangerLevel} \x1b[94mnbSpeakers:\x1b[39m {languages.NbSpeakers}";

                    if (display.Any() != true)
                    {
                        Console.WriteLine($"The word '{search}' was not found");
                    }
                    else
                    {
                        foreach (var lang in display)
                        {
                            Console.WriteLine($"{lang}");  
                        }
                    }    
                }

                Console.WriteLine("");
                Console.WriteLine("Press 0 to return to Main Menu, or any other key (exept ESC) to quit (2 for example)");
                Console.WriteLine("");

                string input = Console.ReadLine();

                int input_nb = int.Parse(input);

                bool isNumeric = int.TryParse(input, out input_nb);

                //If the input is a number but it is not in the menu
                if (input_nb == 0)
                {
                    Console.WriteLine("Parsing error");
                    Console.WriteLine("Restarting programme...");
                    mainMenu("", path);
                }
                //Input verification, it has to be a number
                if (!isNumeric || input_nb != 1)
                {
                    Console.WriteLine("Exiting Programme");
                    return;
                }
            }

        }
    }
}