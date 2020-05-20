namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MEMBERSHIPTYPES SET NOMBRE ='Nombre1' WHERE Id=1");
            Sql("UPDATE MEMBERSHIPTYPES SET NOMBRE ='Nombre2' WHERE Id=2");
            Sql("UPDATE MEMBERSHIPTYPES SET NOMBRE ='Nombre3' WHERE Id=3");
            Sql("UPDATE MEMBERSHIPTYPES SET NOMBRE ='Nombre4' WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
