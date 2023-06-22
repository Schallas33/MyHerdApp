using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHerdApp.Model
{
    public class Camp
    {
        [PrimaryKey, AutoIncrement]
        public int CampID { get; set; }
        public string CampName { get; set; }
        public int Females { get; set; }
        public int Infants { get; set; }
        public int Males { get; set; }
        public DateTime LastCount { get; set; }
        public string Notes { get; set; }

        [ForeignKey(typeof(Farm))]
        public int FarmID { get; set; }

        [ManyToOne]
        public Camp camp { get; set; }
    }
}
