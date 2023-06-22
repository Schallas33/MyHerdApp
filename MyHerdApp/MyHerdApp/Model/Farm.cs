using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHerdApp.Model
{
    public class Farm
    {
        [PrimaryKey, AutoIncrement]
        public int FarmID { get;set; }
        public string FarmName { get; set; }
        
        [OneToMany]
        public List<Camp> Camps { get; set; }
    }
}
