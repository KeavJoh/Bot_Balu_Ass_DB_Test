using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Data.Model
{
    internal class Deregistration
    {
        public int Id { get; set; }
        public string ChildName { get; set; }
        public int ChildId { get; set; }
        public DateTime? DeregistrationAt { get; set; }
        public DateTime? DeregistrationFrom { get; set; }
        public DateTime? DeregistrationTo { get; set;}
        public bool DeregistrationForOneDay { get; set; }
        public bool DeregistrationPerformedFromParents { get; set; }
    }
}
