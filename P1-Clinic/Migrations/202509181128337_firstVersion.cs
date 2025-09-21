namespace P1_Clinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstVersion : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: false)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: false)

                .ForeignKey("dbo.MedicalRecords", t => t.MedicalRecordId, cascadeDelete: false)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: false)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.PaymentId)
                .Index(t => t.MedicalRecordId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: false)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Specialization = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: false)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.MedicalRecords",
                c => new
                    {
                        MedicalRecordId = c.Int(nullable: false, identity: true),
                        VisitDescription = c.String(),
                        Diagnosis = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.MedicalRecordId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        PaymentMethod = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionId = c.Int(nullable: false, identity: true),
                        MedicationName = c.String(),
                        MedicalRecordId = c.Int(nullable: false),
                        Dosage = c.String(),
                        Frequency = c.String(),
                        Notes = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PrescriptionId)
                .ForeignKey("dbo.MedicalRecords", t => t.MedicalRecordId, cascadeDelete: false)
                .Index(t => t.MedicalRecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescriptions", "MedicalRecordId", "dbo.MedicalRecords");
            DropForeignKey("dbo.Appointments", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Appointments", "MedicalRecordId", "dbo.MedicalRecords");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "PersonId", "dbo.People");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "PersonId", "dbo.People");
            DropIndex("dbo.Prescriptions", new[] { "MedicalRecordId" });
            DropIndex("dbo.Doctors", new[] { "PersonId" });
            DropIndex("dbo.Patients", new[] { "PersonId" });
            DropIndex("dbo.Appointments", new[] { "MedicalRecordId" });
            DropIndex("dbo.Appointments", new[] { "PaymentId" });
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Payments");
            DropTable("dbo.MedicalRecords");
            DropTable("dbo.Doctors");
            DropTable("dbo.People");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
        }
    }
}
