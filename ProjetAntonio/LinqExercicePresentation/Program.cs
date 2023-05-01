using System;
using DataSources;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LinqExercicePresentation
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //To read the cvs file
            var csvLines = File.ReadAllLines(@$"C:\Users\acaba\Documents\IPI M1\LinQ\ProjetAntonio\DataSources\Input\Extinct-languages.csv");

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
                if(column[10] == "")
                {
                    column[10] = "0";
                }

                //Add each element of the line to the list of languages to get an object
                ListLanguagesData.ListLanguages.Add(new Languages(int.Parse(column[0]), column[1], column[2], column[3], column[4], column[5], column[6], column[7], int.Parse(column[8]), double.Parse(column[9]), double.Parse(column[10])));
            }
            */

            Functions launch = new Functions();

            //Launch the main menu from the functions class
            //launch.mainMenu("");
            launch.fileSelection("");

        }
    }
}