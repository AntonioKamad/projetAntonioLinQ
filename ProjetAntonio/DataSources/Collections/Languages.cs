using System;
using System.Collections.Generic;
using System.Text;

namespace DataSources
{
    public class Languages
    {
        public Languages(int lanId, string enName , string frName , string esName , string countries, string countryCdeA3, string isoCdes, string dangerLevel, int nbSpeakers, double latitude, double longitude)
        {
            LanId = lanId;
            EnName = enName;
            FrName = frName;
            EsName = esName;
            Countries = countries;
            CountryCdeA3 = countryCdeA3;
            IsoCdes = isoCdes;
            DangerLevel = dangerLevel;
            NbSpeakers = nbSpeakers;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int LanId { get; set; }
        public string EnName { get; set; }
        public string FrName { get; set; }
        public string EsName { get; set; }
        public string Countries { get; set; }
        public string CountryCdeA3 { get; set; }
        public string IsoCdes { get; set; }
        public string DangerLevel { get; set; }
        public int NbSpeakers { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
