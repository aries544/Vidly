namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Movies (Name,GenreId,ReleaseDate,DateAdded,NumberInStock) values ('Pelicula1',1,'19650321','20000429',1)");
            Sql("Insert into Movies (Name,GenreId,ReleaseDate,DateAdded,NumberInStock) values ('Pelicula2',2,'19650321','20000429',2)");
            Sql("Insert into Movies (Name,GenreId,ReleaseDate,DateAdded,NumberInStock) values ('Pelicula3',3,'19650321','20000429',3)");
            Sql("Insert into Movies (Name,GenreId,ReleaseDate,DateAdded,NumberInStock) values ('Pelicula4',4,'19650321','20000429',4)");
            Sql("Insert into Movies (Name,GenreId,ReleaseDate,DateAdded,NumberInStock) values ('Pelicula5',5,'19650321','20000429',5)");
        }
        
        public override void Down()
        {
        }
    }
}
