using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PaparazziLogParser
{
    public class FileInterface
    {
        public string ConfigFilePath { get; set; }

        static public void PrintDataToTextFileSet(DataSet data, string folderPath)
        {
            foreach (DataTable table in data.Tables)
            {
                var filename = folderPath + table.TableName;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
                {
                    foreach (DataRow r in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            file.Write(r[i] + " ");
                        }
                        file.Write("\n");
                    }
                }
            }
        }


        static public void GenerateXMLConfigFile(string parserConfigName)
        {
           
            using (XmlWriter xml = XmlWriter.Create(parserConfigName, new XmlWriterSettings() { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("ParserConfiguration");
                xml.WriteStartElement("FlightLog");
                xml.WriteElementString("LogSourcePath", HMI.UserInput("LogSource", "C:\\USTUAV/mat/12_11_02__16_41_08.data"));
                xml.WriteElementString("SampleTime", HMI.UserInputDouble("SampleTime", ".01"));
                xml.WriteElementString("Folder", HMI.UserInput("OutputFolder",
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ToString())));
                xml.WriteElementString("AircraftNumber", HMI.UserInputInt16("AircraftNumber", "1"));
                var flightCaptureMode = HMI.UserInputInt16("FlightCaptureMode", "1");                
                xml.WriteElementString("FlightCaptureMode", flightCaptureMode);
                if (Convert.ToInt16(flightCaptureMode) == 1)
                {
                    xml.WriteElementString("StartTime", HMI.UserInputDouble("StartTime"));
                    xml.WriteElementString("EndTime", HMI.UserInputDouble("EndTime"));
                }

                xml.WriteEndElement();
                xml.WriteStartElement("OutputFiles");
                while (HMI.UserInput("NewFile").ToLower() == "y")
                {
                    Console.Clear();
                    xml.WriteStartElement("OutputFile");
                    xml.WriteElementString("Name", HMI.UserInputFilename("OutputName", ".data", "SysidConfiguration"));
                    xml.WriteStartElement("Columns");
                    while (HMI.UserInput("NewColumn").ToLower() == "y")
                    {
                        Console.Clear();
                        xml.WriteStartElement("Column");
                        xml.WriteElementString("MessageName", HMI.UserInput("MessageName"));
                        xml.WriteElementString("MessageColumn", HMI.UserInputInt16("MessageColumn"));
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.WriteEndElement();
                xml.WriteEndDocument();
            }
        }
        public static string FormatFileName(string filename, string extension)
        {
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

        static public ParserGlobalVariables ReadXMLToGlobalVars(string XMLPath)
        {
            var flightVars = new ParserGlobalVariables();
            flightVars.FlightName = XMLPath.Remove(XMLPath.IndexOf(".xml"));
            var outputFiles = new List<OutputFileConfig>();
            OutputFileConfig outputFile = new OutputFileConfig();
            OutputColumn outputColumn = new OutputColumn(); 
            using (XmlReader xml = XmlReader.Create(XMLPath, new XmlReaderSettings() { }))
            {
                while (xml.Read())
                {
                    if (xml.IsStartElement())
                    {
                        switch (xml.Name)
                        {
                            case "OutputFiles":
                                flightVars.OutputFileConfigs = new List<OutputFileConfig>();
                                break;

                            case "OutputFile":
                                outputFile = new OutputFileConfig();
                                flightVars.OutputFileConfigs.Add(outputFile);
                                break;

                            case "Columns":
                                outputFile.Columns = new List<OutputColumn>();
                                break;

                            case "Column":
                                outputColumn = new OutputColumn();
                                outputFile.Columns.Add(outputColumn);
                                break;

                            case "Name":
                                if (xml.Read())
                                    outputFile.Name = xml.Value.Trim();
                                break;

                            case "AircraftNumber":
                                if (xml.Read())
                                    flightVars.AircraftNumber = Convert.ToInt16(xml.Value.Trim());
                                break;

                            case "StartTime":
                                if (xml.Read())
                                    flightVars.StartTime = Convert.ToDouble(xml.Value.Trim());
                                break;

                            case "EndTime":
                                if (xml.Read())
                                    flightVars.EndTime = Convert.ToDouble(xml.Value.Trim());
                                break;
                            case "SampleTime":
                                if (xml.Read())
                                    flightVars.SampleTime = Convert.ToDouble(xml.Value.Trim());
                                break;

                            case "MessageName":
                                if (xml.Read())
                                    outputColumn.MessageName = xml.Value.TrimEnd();
                                break;

                            case "MessageColumn":
                                if (xml.Read())
                                    outputColumn.MessageColumn = Convert.ToInt16(xml.Value.Trim());
                                break;

                            case "LogSourcePath":
                                if (xml.Read())
                                    flightVars.LogSourcePath = xml.Value.Trim();
                                break;
                            case "Folder":
                                if (xml.Read())
                                    flightVars.Folder = xml.Value.Trim();
                                break;

                        }
                    }
                }
            }
            return flightVars;
        }
        static public string GetLogSourcePath(string XMLPath)
        {
            using (XmlReader xml = XmlReader.Create(XMLPath, new XmlReaderSettings() { }))
            {
                while (xml.Read())
                {
                    if (xml.IsStartElement())
                    {
                        switch (xml.Name)
                        {
                            case "LogSourcePath":
                                if (xml.Read())
                                    return xml.Value.Trim();
                                break;
                        }
                    }
                }
            }
            return null;
        }

        static public DataSet ReadPaparazziLogFile(string path)
        {
            TextReader tr = new StreamReader(path);
            var tableSet = new DataSet();
            var lineNum = 0;
            while (tr.Peek() >= 0)
            {
                lineNum++;
                var line = tr.ReadLine();
                var data = line.Split(new char[] { ' ', ',' }).ToArray();
                DataTable table;

                if (!tableSet.Tables.Contains(data[2]))
                {
                    tableSet.Tables.Add(new DataTable(data[2]));
                    table = tableSet.Tables[tableSet.Tables.IndexOf(data[2])];
                    table.Columns.Add(new DataColumn("Time(s)"));
                    foreach (var dp in data.Skip(3))
                    {
                        table.Columns.Add(new DataColumn());
                    }
                }
                table = tableSet.Tables[tableSet.Tables.IndexOf(data[2])];
                var row = table.NewRow();
                var i = 1;
                row[0] = data[0];
                try
                {
                    foreach (var datapoint in data.Skip(3))
                    {
                        if (datapoint.Split(',').Count() > 1)
                        {
                            var arrData = datapoint.Split(',');
                            row[i] = arrData;
                            continue;
                        }
                        row[i] = datapoint;
                        i++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in Paparazzi Log Read: Line - " + lineNum);
                }
                table.Rows.Add(row);
            }
            return tableSet;
        }

        static public void PrintMatlabMetadata(ParserGlobalVariables vars)
        {
            var mFilePath = vars.Folder + '/' + vars.FlightName + ".m";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(mFilePath))
            {
                foreach (var opf in vars.OutputFileConfigs)
                {
                    file.WriteLine(opf.Name.Remove(opf.Name.IndexOf('.')) + "= dlmread('/" + vars.Folder + '/' + opf.Name+"');");
                }
                file.WriteLine("sampleRate = " + vars.SampleTime + ';');
            }
        }

    }
}
