using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparazziLogParser
{
    class Program
    {
        
        static void Main(string[] args)
        {
            HMI.UserOutput("Welcome");
            
            var parserConfigName = HMI.UserInputFilename("ParserConfigName", ".xml", "SysidConfiguration");
            if (System.IO.File.Exists(parserConfigName))
            {
                HMI.UserOutput("Using Existing Parser Configuration File: " + parserConfigName);
            }
            else
            {
                FileInterface.GenerateXMLConfigFile(parserConfigName);
            }
            
            var flightVars = FileInterface.ReadXMLToGlobalVars(parserConfigName);
            
            if (System.IO.Directory.Exists(flightVars.Folder))
            {
                HMI.UserOutput("Using Existing Output Directory: " + flightVars.Folder);
            }
            else
            {
                System.IO.Directory.CreateDirectory(flightVars.Folder);
            }
            var mathematizer = new Mathematizer(flightVars.SampleTime);
            var dataSet = FileInterface.ReadPaparazziLogFile(flightVars.LogSourcePath);
            var outputSet = mathematizer.GenerateOutputFileTables(dataSet, flightVars);
            
            FileInterface.PrintDataToTextFileSet(outputSet, flightVars.Folder +'/');
            FileInterface.PrintMatlabMetadata(flightVars);
        
        }




    }
}
