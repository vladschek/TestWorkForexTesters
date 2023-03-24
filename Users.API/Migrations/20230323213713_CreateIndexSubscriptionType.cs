using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndexSubscriptionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "idx_subscription_type",
                table: "Subscriptions",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
