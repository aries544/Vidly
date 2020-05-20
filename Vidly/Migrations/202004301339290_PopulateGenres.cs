namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (name) values ('Genero1')");
            Sql("Insert into Genres (name) values ('Genero2')");
            Sql("Insert into Genres (name) values ('Genero3')");
            Sql("Insert into Genres (name) values ('Genero4')");
            Sql("Insert into Genres (name) values ('Genero5')");
        }
        
        public override void Down()
        {
        }
    }
}
