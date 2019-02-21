namespace Inspection.Models
{
    using System.Data.Entity;

    public partial class InspectionModelContext : DbContext
    {
        public InspectionModelContext()
            : base("name=InspectionModelContext")
        {
            Database.SetInitializer<InspectionModelContext>(new CreateDatabaseIfNotExists<InspectionModelContext>());
        }

        public virtual DbSet<CompliantQuestion> CompliantQuestions { get; set; }
        public virtual DbSet<Inspection> Inspections { get; set; }
        public virtual DbSet<InspectionExt> InspectionExts { get; set; }
        public virtual DbSet<AuditInspectionDetail> AuditInspectionDetails { get; set; }

        public DbSet<UserTokenCache> UserTokenCacheList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompliantQuestion>()
                .Property(e => e.CompliantQues)
                .IsUnicode(false);

            modelBuilder.Entity<CompliantQuestion>()
                .Property(e => e.InspectionForm)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.InspectionForm)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.SiteName)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.SiteID)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.AreaOfBusiness)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.AreaInspected)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.InspectedPerson)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.ActivityName)
                .IsUnicode(false);

            modelBuilder.Entity<Inspection>()
                .Property(e => e.Created_By)
                .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.UserID)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.ActivityQ)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.SelectedForm)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.FurtherActionRequired)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.Comments)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.Assignee)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Inspection>()
            //    .Property(e => e.Priority)
            //    .IsUnicode(false);

            modelBuilder.Entity<InspectionExt>()
                .Property(e => e.Compliant)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionExt>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionExt>()
                .Property(e => e.Severity)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionExt>()
                .Property(e => e.Assignee)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionExt>()
                .Property(e => e.DueDate)
                .IsUnicode(false);
        }
    }
}
