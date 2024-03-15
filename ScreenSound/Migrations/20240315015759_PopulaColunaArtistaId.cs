using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations;

/// <inheritdoc />
public partial class PopulaColunaArtistaId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1 WHERE Id IN(1,2,3,4)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = null WHERE Id IN(1,2,3,4)");
    }
}
