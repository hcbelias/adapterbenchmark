using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterTestTrigger
{
    class ReportParser
    {
        private class ReportSummary
        {
            public ReportSummary(string name, int lines)
            {
                this.ReportName = name;
                this.NumberOfLines = lines;
            }
            public string ReportName { get; private set; }
            public int NumberOfLines { get; private set; }
            public decimal AvgOfMinValues { get; set; }
            public decimal AvgOfMaxValues { get; set; }
            public decimal AvgOfMeanValues { get; set; }
            public decimal AvgOfMedianValues { get; set; }
        }

        private const string FOLDER_PATH = @"C:\Temp\iisexpress\";
        private const string BENCHMARK_FILE_NAME = "__benchmark.csv";

        static void Main(string[] args)
        {
            var reports = System.IO.Directory.EnumerateFiles(FOLDER_PATH).Where(w => w != FOLDER_PATH + BENCHMARK_FILE_NAME);
            WriteBenchmark(GetAllReportsSummary(reports));
        }

        private static decimal GetAvgOfColumn(IEnumerable<string> lines, int lineCount, int colNumber)
        {
            return lines.Sum(s => Convert.ToDecimal(s.Split(',')[colNumber]) / lineCount);
        }

        private static List<ReportSummary> GetAllReportsSummary(IEnumerable<string> reports)
        {
            var summaryCollection = new List<ReportSummary>(reports.Count());

            foreach (var r in reports)
            {
                var lines = System.IO.File.ReadAllLines(r).Skip(1);//   skip the header line;
                var reportSummary = new ReportSummary(r, lines.Count());

                reportSummary.AvgOfMinValues = GetAvgOfColumn(lines, reportSummary.NumberOfLines, 10);
                reportSummary.AvgOfMaxValues = GetAvgOfColumn(lines, reportSummary.NumberOfLines, 12);
                reportSummary.AvgOfMeanValues = GetAvgOfColumn(lines, reportSummary.NumberOfLines, 14);
                reportSummary.AvgOfMedianValues = GetAvgOfColumn(lines, reportSummary.NumberOfLines, 16);

                summaryCollection.Add(reportSummary);
            }
            return summaryCollection;
        }

        private static void WriteBenchmark(List<ReportSummary> summaryCollection)
        {
            IEnumerable<string> benchmark = new List<string>(){
                /*header*/"file name, lines, min , max, mean, median"
            };

            benchmark = benchmark.Concat(summaryCollection.Select(s =>
                 string.Join(",", new object[] { s.ReportName, s.NumberOfLines, s.AvgOfMinValues, s.AvgOfMaxValues, s.AvgOfMeanValues, s.AvgOfMedianValues })));

            System.IO.File.WriteAllLines(FOLDER_PATH + BENCHMARK_FILE_NAME, benchmark);
        }
    }
}
