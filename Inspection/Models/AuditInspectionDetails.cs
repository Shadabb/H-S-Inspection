using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspection.Models
{
    public class AuditInspectionDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int InspectionID { get; set; }
        public string TableName { get; set; }
        public string FiledName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompliantNo { get; set; }
    }
}