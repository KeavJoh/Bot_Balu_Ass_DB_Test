using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Data.Model
{
    internal class ImportandDate
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string? EventComment { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateTo {  get; set; }
    }
}
