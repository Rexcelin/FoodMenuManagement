using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodMenuManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // FoodItems table already exists in the database.
            // This migration records the existing schema as the baseline.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally empty.
            // Do not drop the existing FoodItems table.
        }

    }
}
