namespace Inspection.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Inspection")]
    public partial class Inspection
    {
        public int ID { get; set; }

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
        public string SiteID { get; set; }

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
        public string Created_By { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string UserID { get; set; }

        //[Required]
        //[StringLength(200)]
        //public string ActivityQ { get; set; }

        //[StringLength(50)]
        //[Display(Name = "Form Selected")]
        //public string SelectedForm { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Created Date")]
        public DateTime Created { get; set; }

        //[StringLength(50)]
        //[Display(Name = "Further Action")]
        //public string FurtherActionRequired { get; set; }

        //[StringLength(50)]
        //public string Comments { get; set; }

        //[StringLength(50)]
        //public string Assignee { get; set; }

        //[StringLength(50)]
        //public string Priority { get; set; }

        //[Column(TypeName = "date")]
        //[Display(Name = "Due Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTime? Due { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }
    }
}
