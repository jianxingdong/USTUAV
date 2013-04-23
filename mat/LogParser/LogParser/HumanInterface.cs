using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace PaparazziLogParser
{
    public static class HMI
    {
        private static DataTable supportedMessages;
        public static string UserInput(string prompt, string defaultValue = "")
        {
            if (dictionary.ContainsKey(prompt))
            {
                Console.WriteLine(dictionary[prompt]);
            }
            else
            {
                if (prompt.Split(' ').Count() == 1)
                {
                    Console.WriteLine("UNCONFIGURED TEXT: " + prompt);
                }
                else
                {
                    Console.WriteLine(prompt);
                }
            }
            var value = Console.ReadLine();
            if (defaultValue != "" && value == "")
            {
                Console.WriteLine(String.Format(dictionary["UsingDefaultValue"], defaultValue));
                return defaultValue;
            }
            return value;
        }
        public static void UserOutput(string prompt)
        {
            if (dictionary.ContainsKey(prompt))
            {
                Console.WriteLine(dictionary[prompt]);
            }
            else
            {
                if (prompt.Split(' ').Count() == 1)
                {
                    Console.WriteLine("UNCONFIGURED TEXT: " + prompt);
                }
                else
                {
                    Console.WriteLine(prompt);
                }
            }
        }
        //TODO: PUT IN UserInputFilename VALIDATION
        public static string UserInputFilename(string prompt, string extension = "", string defaultValue = "", bool validate = false)
        {
            var filename = UserInput(prompt);
            if (defaultValue != "" && filename == "")
            {
                Console.WriteLine(String.Format(dictionary["UsingDefaultValue"], defaultValue));
                filename = defaultValue;
            }
            if (filename.EndsWith(extension))
            {
                filename = filename.Remove(filename.IndexOf(extension));
            }
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            filename = System.Text.RegularExpressions.Regex.Replace(filename, invalidReStr, "_");
            if (!filename.EndsWith(extension))
            {
                filename = filename + extension;
            }
            return filename;
        }
        public static string UserInputDouble(string prompt, string defaultValue = "")
        {
            var uInput = UserInput(prompt);
            if (defaultValue != "" && uInput == "")
            {
                Console.WriteLine(String.Format(dictionary["UsingDefaultValue"], defaultValue));
                return defaultValue;
            }
            try
            {
                return Convert.ToDouble(uInput).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input...");
                return UserInputDouble(prompt);
            }
        }

        public static string UserInputInt16(string prompt, string defaultValue = "")
        {
            var uInput = UserInput(prompt);
            if (defaultValue != "" && uInput == "")
            {
                Console.WriteLine(String.Format(dictionary["UsingDefaultValue"],defaultValue));
                return defaultValue;
            }
            try
            {
                return Convert.ToInt16(uInput).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input...");
                return UserInputInt16(prompt);
            }
        }


        //THIS IS THE PROMPT LOOKUP TABLE, It SHould be configured to an XML table later
        private static Dictionary<string, string> dictionary = new Dictionary<string, string>() 
        { 
             {"Welcome", "---***---\nWelcome to the Paparazzi Log Parser and Matlab Interface Configuration Wizard\n---***---\n\n"},
             {"NewFile", "Configure New Output File? (Y/N)"},
             {"NewColumn", "Configure New Column? (Y/N)"},
             {"OutputFolder", "Enter Output File Folder"},
             {"UsingDefaultValue", "\tUsing Default Value: {0}\n"},
             {"OutputName", "Enter Output File Name"},
             {"SampleTime", "Enter SampleTime (double)"},
             {"AircraftNumber", "Enter  Aircraft Number (int) \n\t(Will be 1 for a single aircraft)"},
             {"FlightCaptureMode", "Enter Output File Flight Capture Mode \n\t0: Toggle on Auto1 Falling Edge\n\t1: Enter start and stop times"},
             {"LogSource", "Enter Log File Path"},
             {"ParserConfigName", "Please enter a Parser Configuration,\n or enter a new Parser Configuration Name"}
        };

    }
}
