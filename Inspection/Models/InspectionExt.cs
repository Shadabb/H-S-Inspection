namespace Inspection.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InspectionExt")]
    public partial class InspectionExt
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public int CompliantNo { get; set; }

        [StringLength(50)]
        public string Compliant { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        [StringLength(50)]
        public string Severity { get; set; }

        [StringLength(50)]
        public string Assignee { get; set; }

        [StringLength(50)]
        public string DueDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string ClosureDate { get; set; }

        [StringLength(100)]
        public string UpdatedComments { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }
    }
}
