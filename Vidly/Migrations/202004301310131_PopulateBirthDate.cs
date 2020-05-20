namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBirthDate : DbMigration
    {
        public override void Up()
        {
            Sql("Update Customers set BirthDate = '19800413', MembershipTypeId = 2 where Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
