namespace Inspection.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompliantQuestion")]
    public partial class CompliantQuestion
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string CompliantQues { get; set; }

        [StringLength(50)]
        public string InspectionForm { get; set; }

        public int? CompliantNo { get; set; }
    }
}
