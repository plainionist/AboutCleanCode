using System.Collections.Generic;

namespace Nameing
{
    public class Patient
    {
        public Patient(string patient) { }

        public Patient(string patient, string study) { }

        public Patient(string patient, string study, string series) { }

        public string PatientName { get; private set; }

        public IList<string> Series { get; private set; }

        public IList<string> Studies { get; private set; }
    }
}