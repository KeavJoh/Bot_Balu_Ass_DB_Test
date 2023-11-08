using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(50)]
        public string? Reason { get; set; }
        public DateTime DeregistrationDate{ get; set; }
        public DateTime DateOfAction { get; set; }
        public bool DeregistrationPerformedFromParents { get; set; }

        public static Deregistration CreateDeregistration(string childName, int childId, string reason, DateTime deregistrationDate, DateTime dateOfAction, bool performedFromParents)
        {
            var Deregistration = new Deregistration { ChildName = childName, ChildId = childId, Reason = reason, DeregistrationDate = deregistrationDate, DateOfAction = dateOfAction, DeregistrationPerformedFromParents = performedFromParents };

            return Deregistration;
        }
    }
}
