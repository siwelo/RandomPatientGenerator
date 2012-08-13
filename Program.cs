using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Clearwave.IO;

namespace RandomPatientGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // seed values
            var lastNames = new HashSet<string>(File.ReadAllLines(@"data_lastnames.txt")).Select(x => x.ToUpper().Trim()).ToList();
            var firstNames = new HashSet<string>(File.ReadAllLines(@"data_firstnames.txt")).Select(x => x.ToUpper().Trim()).ToList();
            var addresses = new List<Tuple<string, string, string, string, string>>();
            using (var fileReader = File.OpenRead(@"data_addresses.csv"))
            {
                using (var csv = new CSVReader(fileReader))
                {
                    var row = csv.NextRow();
                    while (row != null)
                    {
                        addresses.Add(new Tuple<string, string, string, string, string>(row[0] ?? string.Empty, row[1] ?? string.Empty, row[2] ?? string.Empty, row[3] ?? string.Empty, row[4] ?? string.Empty));
                        row = csv.NextRow();
                    }
                }
            }

            var ran = new Random(42);

            // generate patients
            File.WriteAllText(@"data_patients_1000.csv", "MRN, LastName,FirstName,Gender,DOB,AddressStreet1,AddressStreet2,AddressCity,AddressState,AddressZIPCode" + Environment.NewLine);
            using (var file = new FileStream(@"data_patients_1000.csv", FileMode.Append))
            using (var writer = new StreamWriter(file))
            {
                for (int i = 0; i < 1000; i++)
                {
                    writer.Write("P" + (i + 1).ToString().PadLeft(7, '0') + ",");
                    writer.Write(lastNames[ran.Next(lastNames.Count)].Trim() + ",");
                    writer.Write(firstNames[ran.Next(lastNames.Count)].Trim() + ",");
                    writer.Write(ran.Next(2) + ",");
                    writer.Write(RandomDOB(ran).ToString("yyyy/MM/dd") + ",");
                    var address = addresses[ran.Next(addresses.Count)];
                    writer.Write(address.Item1.Trim() + ",");
                    writer.Write(address.Item2.Trim() + ",");
                    writer.Write(address.Item3.Trim() + ",");
                    writer.Write(address.Item4.Trim() + ",");
                    writer.Write(address.Item5.Trim());
                    writer.WriteLine();
                }
            }

            File.Copy(@"data_patients_1000.csv", @"data_patients_10000.csv", true);
            using (var file = new FileStream(@"data_patients_10000.csv", FileMode.Append))
            using (var writer = new StreamWriter(file))
            {
                for (int i = 1000; i < 10000; i++)
                {
                    writer.Write("P" + (i + 1).ToString().PadLeft(7, '0') + ",");
                    writer.Write(lastNames[ran.Next(lastNames.Count)].Trim() + ",");
                    writer.Write(firstNames[ran.Next(lastNames.Count)].Trim() + ",");
                    writer.Write(ran.Next(2) + ",");
                    writer.Write(RandomDOB(ran).ToString("yyyy/MM/dd") + ",");
                    var address = addresses[ran.Next(addresses.Count)];
                    writer.Write(address.Item1.Trim() + ",");
                    writer.Write(address.Item2.Trim() + ",");
                    writer.Write(address.Item3.Trim() + ",");
                    writer.Write(address.Item4.Trim() + ",");
                    writer.Write(address.Item5.Trim());
                    writer.WriteLine();
                }
            }

            File.Copy(@"data_patients_10000.csv", @"data_patients_100000.csv", true);
            using (var file = new FileStream(@"data_patients_100000.csv", FileMode.Append))
            using (var writer = new StreamWriter(file))
            {
                for (int i = 10000; i < 100000; i++)
                {
                    writer.Write("P" + (i + 1).ToString().PadLeft(7, '0') + ",");
                    writer.Write(lastNames[ran.Next(lastNames.Count)].Trim() + ",");
                    writer.Write(firstNames[ran.Next(lastNames.Count)].Trim() + ",");
                    writer.Write(ran.Next(2) + ",");
                    writer.Write(RandomDOB(ran).ToString("yyyy/MM/dd") + ",");
                    var address = addresses[ran.Next(addresses.Count)];
                    writer.Write(address.Item1.Trim() + ",");
                    writer.Write(address.Item2.Trim() + ",");
                    writer.Write(address.Item3.Trim() + ",");
                    writer.Write(address.Item4.Trim() + ",");
                    writer.Write(address.Item5.Trim());
                    writer.WriteLine();
                }
            }
        }

        public static DateTime RandomDOB(Random randomTest)
        {
            TimeSpan timeSpan = DateTime.Now.Date.AddDays(-1) - new DateTime(1930, 01, 01);
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = new DateTime(1930, 01, 01) + newSpan;
            return newDate;
        }
    }
}
