using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Balu_Ass_DB.Data.Model
{
    internal class WithdrawnDeregistration
    {
        public int Id { get; set; }
        public string ChildName { get; set; }
        public int ChildId { get; set; }
        [StringLength(50)]
        public string? DeregistrationReason { get; set; }
        public DateTime DeregistrationDate { get; set; }
        public DateTime DateOfDeregistrationAction { get; set; }
        public DateTime DateOfWithdrawn { get; set; }
        public bool DeregistrationPerformedFromParents { get; set; }
        public bool WithdrawnPerformedFromParents { get; set; }


        public static WithdrawnDeregistration CreateWithdrawnDeregistration(string childName, int childId, string reason, DateTime deregistrationDate, DateTime dateOfDeregistratioAction, bool fromParents, bool withdrawnFromParents)
        {
            var withdrawn = new WithdrawnDeregistration { ChildName = childName, ChildId = childId, DeregistrationReason = reason, DeregistrationDate = deregistrationDate,
                DateOfDeregistrationAction = dateOfDeregistratioAction, DateOfWithdrawn = DateTime.Now, DeregistrationPerformedFromParents = fromParents, WithdrawnPerformedFromParents = withdrawnFromParents };
            
            return withdrawn;
        }
    }
}
