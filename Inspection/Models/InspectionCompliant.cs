using System.ComponentModel.DataAnnotations;

namespace Inspection.Models
{
    public class InspectionCompliant
    {
        public int InspectionCompliantID { get; set; }

        public int InspectionID { get; set; }

        public int CompliantNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Answer")]
        public string Compliant { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        [StringLength(50)]
        public string Severity { get; set; }

        [StringLength(50)]
        public string Assignee { get; set; }

        [StringLength(50)]
        public string DueDate { get; set; }

        [StringLength(200)]
        public string CompliantQues { get; set; }

        [StringLength(50)]
        public string InspectionForm { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

    }
}