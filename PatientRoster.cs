using RandomPatientGenerator.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace RandomPatientGenerator
{
    /// <summary>
    /// Container for currently served patients
    /// </summary>
    public class PatientRoster
    {
        private const int MAX_PATIENT_COUNT = 10000;
        private int currentLoad;

        private HashSet<Patient> roster = new HashSet<Patient>();

        public PatientRoster(int patientLoad)
        {
            if (patientLoad > MAX_PATIENT_COUNT)
                throw new ArgumentOutOfRangeException(String.Format("Our maximum patient load is: {0:N}", MAX_PATIENT_COUNT.ToString()));
            else
                this.currentLoad = patientLoad;

            //initData();
        }

        public void AdmitPatient(Patient newPatient)
        {
            //preliminary processing if required
            roster.Add(newPatient);
        }

        public void ReleasePatient(Patient toRelease)
        {
            //preliminary processing if required
            roster.Remove(toRelease);
        }

        /// <summary>
        /// Exports current roster as comma separated list of values
        /// </summary>
        /// <param name="outputFileName"></param>
        public void exportToCSV(String outputFileName)
        {
            using (StreamWriter fileOut =
           new StreamWriter(outputFileName, false))
            {
                fileOut.Write("MRN,LastName,FirstName,Gender,DOB,AddressStreet1,AddressStreet2,AddressCity,AddressState,AddressZIPCode\n");
                fileOut.Write(String.Join(Environment.NewLine, roster.Select(p => p.viewPatientRecord()).ToArray()));
                //fileOut.WriteLine(roster.Select(p => p.outputRecord()).ToArray());
            }
        }

        /// <summary>
        /// initializes data set using lists created elswhere
        /// </summary>
        /// <param name="lastNames"></param>
        /// <param name="firstNames"></param>
        /// <param name="addresses"></param>
        public void initData(List<string> lastNames, List<string> firstNames, List<Tuple<string, string, string, string, string>> addresses)
        {
            var timer = new Stopwatch();
            timer.Start();
            var ran = new Random();
            timer.Start();
            for (int i = 0; i < currentLoad; i++)
            {
                var address = addresses[ran.Next(addresses.Count)];
                Address addr = new Address(address.Item1.Trim(), address.Item2.Trim(), address.Item3.Trim(), address.Item4.Trim(), address.Item5.Trim());
                Patient p = new Patient("P" + (i + 1).ToString().PadLeft(7, '0'), firstNames[ran.Next(lastNames.Count)].Trim(),
                    lastNames[ran.Next(lastNames.Count)].Trim(), Program.RandomDOB(ran).ToString("yyyy/MM/dd"), addr);
                p.PatientGender = (ran.Next(2) == 0) ? Gender.Male : Gender.Female;

                this.AdmitPatient(p);

                if (i % 100 == 0)
                {
                    Console.WriteLine("Generated : " + i + "/" + currentLoad + " patients");
                }
            }
            timer.Stop();
            Console.WriteLine("Generated 1,000 patients in " + timer.Elapsed);
        }
    }
}
