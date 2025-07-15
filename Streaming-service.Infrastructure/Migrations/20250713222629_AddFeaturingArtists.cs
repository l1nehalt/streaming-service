using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streaming_service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFeaturingArtists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturingArtists",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "SongFeaturingArtists",
                columns: table => new
                {
                    SongId = table.Column<long>(type: "bigint", nullable: false),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongFeaturingArtists", x => new { x.SongId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_SongFeaturingArtists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongFeaturingArtists_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongFeaturingArtists_ArtistId",
                table: "SongFeaturingArtists",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongFeaturingArtists");

            migrationBuilder.AddColumn<string>(
                name: "FeaturingArtists",
                table: "Songs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
