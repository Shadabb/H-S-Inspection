using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspection.Models
{
    public class MasterDetails
    {
        public int InspectionId { get; set; }

        public int InspectionExtId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Inspection")]
        public string InspectionForm { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Site Name")]
        public string SiteName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Site ID")]
        public string SiteId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Business Area")]
        public string AreaOfBusiness { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Inspected Area")]
        public string AreaInspected { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Inspected Person")]
        public string InspectedPerson { get; set; }


        [StringLength(300)]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Inspection Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InspectionDate { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(100)]
        public string UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "Form Selected")]
        public string SelectedForm { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Created Date")]
        public DateTime Created { get; set; }

        public int CompliantNo { get; set; }

        [StringLength(200)]
        public string CompliantQues { get; set; }

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

        //public IEnumerable<Inspection> Inspections { get; set; }

    }
}