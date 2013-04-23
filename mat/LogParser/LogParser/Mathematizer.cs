using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparazziLogParser
{
    public class Mathematizer
    {
        private double timestep;
        public Mathematizer(double timestep)
        {
            this.timestep = timestep;
        }

        public DataSet PeriodizeTimestep(DataSet set, ParserGlobalVariables vars)
        {
            var result = new DataSet();
            var periodizeTheseTables = new List<string>();
            foreach (var o in vars.OutputFileConfigs)
            {
                foreach (var c in o.Columns)
                {
                    if (periodizeTheseTables.Contains(c.MessageName))
                    {
                        continue;
                    }
                    periodizeTheseTables.Add(c.MessageName);
                }
            }
            foreach (DataTable t in set.Tables)
            {
                if(periodizeTheseTables.Contains(t.TableName))
                    result.Tables.Add(PeriodizeTimestep(t, vars.StartTime, vars.EndTime, t.TableName));
            }
            return result;
        }
        public DataTable PeriodizeTimestep(DataTable table, double start, double end, string tableName = "testTable")
        {
            Console.WriteLine("Periodizing Table: " + table.TableName);
            var prevRow = table.Rows[0];
            var newTable = new DataTable(tableName);
            foreach (DataColumn c in table.Columns)
            {
                newTable.Columns.Add(new DataColumn(c.ColumnName));
            }
            var colNum = table.Columns.Count;
            var timecount = start;
            foreach (DataRow r in table.Rows)
            {
                if (Convert.ToDouble(r[0]) < start)
                {
                    prevRow = r;
                    continue;
                }
                if (Convert.ToDouble(r[0]) > end)
                {
                    break;
                }
                
                while (timecount <= Convert.ToDouble(r[0]))
                {
                    timecount = Math.Round(timecount * 1000) / 1000;
                    var newRow = newTable.NewRow();
                    newRow[0] = timecount;
                    for (int colcount = 1; colcount < colNum; colcount++)
                    {
                        try
                        {
                            newRow[colcount] = FindTimedDatapoint(
                                    Convert.ToDouble(prevRow[0]),
                                    Convert.ToDouble(prevRow[colcount]),
                                    Convert.ToDouble(r[0]),
                                    Convert.ToDouble(r[colcount]),
                                    timecount);
                        }
                        catch (Exception e)
                        {
                            timecount = 100000000;
                            break;

                        }
                    }
                    newTable.Rows.Add(newRow);
                    timecount = timecount + timestep;
                }
                prevRow = r;
            }
            return newTable;
        }

        public DataSet GenerateOutputFileTables(DataSet set, ParserGlobalVariables vars)
        {
            var periodizedSet = PeriodizeTimestep(set, vars);
            var result = new DataSet();
            foreach (var opf in vars.OutputFileConfigs)
            {
                result.Tables.Add(opf.Name);
                var t = result.Tables[opf.Name];
                foreach (var c in opf.Columns)
                {
                    t.Columns.Add(c.MessageName + c.MessageColumn);
                }
                for (int i = 0; i < ((vars.EndTime - vars.StartTime) / vars.SampleTime); i++)
                {
                    var r = t.NewRow();
                    for (int c = 0; c < t.Columns.Count; c++)
                    {
                        try
                        {
                            r[c] = periodizedSet.Tables[opf.Columns[c].MessageName].Rows[i][opf.Columns[c].MessageColumn+1];
                        }
                        catch (Exception e)
                        {

                            r[c] = 0;
                        }
                    }
                    t.Rows.Add(r);
                }
            }
            return result;
        }

        private double FindTimedDatapoint(double prevtime, double prevdata, double nexttime, double nextdata, double time)
        {
            return ((nextdata - prevdata) * (time - prevtime) / (nexttime - prevtime)) + prevdata;
        }
    }

}
