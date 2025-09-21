namespace P1_Clinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_PatientAndDoctorToPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PatientId", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "PatientId");
            CreateIndex("dbo.Payments", "DoctorId");
            AddForeignKey("dbo.Payments", "DoctorId", "dbo.Doctors", "DoctorId", cascadeDelete: true);
            AddForeignKey("dbo.Payments", "PatientId", "dbo.Patients", "PatientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Payments", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Payments", new[] { "DoctorId" });
            DropIndex("dbo.Payments", new[] { "PatientId" });
            DropColumn("dbo.Payments", "DoctorId");
            DropColumn("dbo.Payments", "PatientId");
        }
    }
}
