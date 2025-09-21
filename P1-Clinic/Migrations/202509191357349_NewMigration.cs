namespace P1_Clinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "MedicalRecordId", "dbo.MedicalRecords");
            DropForeignKey("dbo.Appointments", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            DropIndex("dbo.Appointments", new[] { "PaymentId" });
            DropIndex("dbo.Appointments", new[] { "MedicalRecordId" });
            AddColumn("dbo.MedicalRecords", "PatientId", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalRecords", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.MedicalRecords", "PatientId");
            CreateIndex("dbo.MedicalRecords", "DoctorId");
            AddForeignKey("dbo.MedicalRecords", "DoctorId", "dbo.Doctors", "DoctorId", cascadeDelete: true);
            AddForeignKey("dbo.MedicalRecords", "PatientId", "dbo.Patients", "PatientId", cascadeDelete: true);
            DropTable("dbo.Appointments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        AppointmentDate = c.DateTime(nullable: false),
                        AppointmentStatus = c.Byte(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        MedicalRecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            DropForeignKey("dbo.MedicalRecords", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.MedicalRecords", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.MedicalRecords", new[] { "DoctorId" });
            DropIndex("dbo.MedicalRecords", new[] { "PatientId" });
            DropColumn("dbo.MedicalRecords", "DoctorId");
            DropColumn("dbo.MedicalRecords", "PatientId");
            CreateIndex("dbo.Appointments", "MedicalRecordId");
            CreateIndex("dbo.Appointments", "PaymentId");
            CreateIndex("dbo.Appointments", "DoctorId");
            CreateIndex("dbo.Appointments", "PatientId");
            AddForeignKey("dbo.Appointments", "PaymentId", "dbo.Payments", "PaymentId", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "MedicalRecordId", "dbo.MedicalRecords", "MedicalRecordId", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "DoctorId", "dbo.Doctors", "DoctorId", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.Patients", "PatientId", cascadeDelete: true);
        }
    }
}
