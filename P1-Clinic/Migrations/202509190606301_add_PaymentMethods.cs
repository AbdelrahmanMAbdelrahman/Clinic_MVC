namespace P1_Clinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_PaymentMethods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        PaymentMethodId = c.Int(nullable: false, identity: true),
                        MethodName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMethodId);
            
            AddColumn("dbo.Payments", "PaymentMethodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "PaymentMethodId");
            AddForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods", "PaymentMethodId", cascadeDelete: true);
            DropColumn("dbo.Payments", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "PaymentMethod", c => c.String());
            DropForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Payments", new[] { "PaymentMethodId" });
            DropColumn("dbo.Payments", "PaymentMethodId");
            DropTable("dbo.PaymentMethods");
        }
    }
}
