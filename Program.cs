using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Clearwave.IO;
using System.Diagnostics;

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
                    while (addresses.Count < 1000)
                    {
                        addresses.Add(new Tuple<string, string, string, string, string>(row[0] ?? string.Empty, row[1] ?? string.Empty, row[2] ?? string.Empty, row[3] ?? string.Empty, row[4] ?? string.Empty));
                        row = csv.NextRow();
                    }
                }
            }

            var timer = new Stopwatch();
            timer.Start();
            var ran = new Random();

            File.WriteAllText(@"data_patients_1000.csv", "MRN, LastName,FirstName,Gender,DOB,AddressStreet1,AddressStreet2,AddressCity,AddressState,AddressZIPCode\n");
            for (int i = 0; i < 1000; i++)
            {
                File.AppendAllText("data_patients_1000.csv", "P" + (i + 1).ToString().PadLeft(7, '0') + ",");
                File.AppendAllText("data_patients_1000.csv", lastNames[ran.Next(lastNames.Count)].Trim() + ",");
                File.AppendAllText("data_patients_1000.csv", firstNames[ran.Next(lastNames.Count)].Trim() + ",");
                File.AppendAllText("data_patients_1000.csv", ran.Next(2) + ",");
                File.AppendAllText("data_patients_1000.csv", RandomDOB(ran).ToString("yyyy/MM/dd") + ",");
                var address = addresses[ran.Next(addresses.Count)];
                File.AppendAllText("data_patients_1000.csv", address.Item1.Trim() + ",");
                File.AppendAllText("data_patients_1000.csv", address.Item2.Trim() + ",");
                File.AppendAllText("data_patients_1000.csv", address.Item3.Trim() + ",");
                File.AppendAllText("data_patients_1000.csv", address.Item4.Trim() + ",");
                File.AppendAllText("data_patients_1000.csv", address.Item5.Trim());
                if (i % 100 == 0)
                {
                    Console.WriteLine("Generated : " + i + "/" + 1000 + " patients");
                }
            }

            File.WriteAllText(@"data_patients_10000.csv", "MRN, LastName,FirstName,Gender,DOB,AddressStreet1,AddressStreet2,AddressCity,AddressState,AddressZIPCode\n");
            for (int i = 0; i < 10000; i++)
            {
                File.AppendAllText("data_patients_10000.csv", "P" + (i + 1).ToString().PadLeft(7, '0') + ",");
                File.AppendAllText("data_patients_10000.csv", lastNames[ran.Next(lastNames.Count)].Trim() + ",");
                File.AppendAllText("data_patients_10000.csv", firstNames[ran.Next(lastNames.Count)].Trim() + ",");
                File.AppendAllText("data_patients_10000.csv", ran.Next(2) + ",");
                File.AppendAllText("data_patients_10000.csv", RandomDOB(ran).ToString("yyyy/MM/dd") + ",");
                var address = addresses[ran.Next(addresses.Count)];
                File.AppendAllText("data_patients_10000.csv", address.Item1.Trim() + ",");
                File.AppendAllText("data_patients_10000.csv", address.Item2.Trim() + ",");
                File.AppendAllText("data_patients_10000.csv", address.Item3.Trim() + ",");
                File.AppendAllText("data_patients_10000.csv", address.Item4.Trim() + ",");
                File.AppendAllText("data_patients_10000.csv", address.Item5.Trim() + "\n");
                if (i % 100 == 0)
                {
                    Console.WriteLine("Generated : " + i + "/10,000 patients");
                }
            }

            File.WriteAllText(@"data_patients_100000.csv", "MRN, LastName,FirstName,Gender,DOB,AddressStreet1,AddressStreet2,AddressCity,AddressState,AddressZIPCode\n");
            for (int i = 0; i < 100000; i++)
            {
                File.AppendAllText("data_patients_100000.csv", "P" + (i + 1).ToString().PadLeft(7, '0') + ",");
                File.AppendAllText("data_patients_100000.csv", lastNames[ran.Next(lastNames.Count)].Trim() + ",");
                File.AppendAllText("data_patients_100000.csv", firstNames[ran.Next(lastNames.Count)].Trim() + ",");
                File.AppendAllText("data_patients_100000.csv", ran.Next(2) + ",");
                File.AppendAllText("data_patients_100000.csv", RandomDOB(ran).ToString("yyyy/MM/dd") + ",");
                var address = addresses[ran.Next(addresses.Count)];
                File.AppendAllText("data_patients_100000.csv", address.Item1.Trim() + ",");
                File.AppendAllText("data_patients_100000.csv", address.Item2.Trim() + ",");
                File.AppendAllText("data_patients_100000.csv", address.Item3.Trim() + ",");
                File.AppendAllText("data_patients_100000.csv", address.Item4.Trim() + ",");
                File.AppendAllText("data_patients_100000.csv", address.Item5.Trim());
                if (i % 100 == 0)
                {
                    Console.WriteLine("Generated : " + i + "/100,000 patients");
                }
            }
            timer.Stop();
            Console.WriteLine("Generated 111,000 patients in " + timer.Elapsed);
            Console.ReadLine();
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
