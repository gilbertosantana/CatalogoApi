using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoApi.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO CATEGORIAS(Nome, ImagemUrl) VALUES('Bebidas', 'bebidas.jpg')");
            mb.Sql("INSERT INTO CATEGORIAS(Nome, ImagemUrl) VALUES('Lanches', 'lanches.jpg')");
            mb.Sql("INSERT INTO CATEGORIAS(Nome, ImagemUrl) VALUES('Sobremesas', 'sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM CATEGORIAS");
        }
    }
}
