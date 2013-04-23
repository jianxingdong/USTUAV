using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparazziLogParser
{
    public class OutputFileConfig
    {
        public string Name { get; set; }
        public List<OutputColumn> Columns { get; set; }
    }
    public class OutputColumn
    {
        public string MessageName { get; set; }
        public int MessageColumn { get; set; }
    }
    public class ParserGlobalVariables
    {
        public string FlightName { get; set; }
        public string LogSourcePath { get; set; }
        public string Folder { get; set; }
        public double SampleTime { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }        
        public int AircraftNumber { get; set; }
        public List<OutputFileConfig> OutputFileConfigs { get; set; }
    }
}
