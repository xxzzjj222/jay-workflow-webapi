using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jay.Workflow.WebApi.Storage.Migrations
{
    /// <inheritdoc />
    public partial class addData_SystemConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "system_config",
                columns: new[] { "id", "config_key", "config_name", "config_type", "config_value", "created_time", "creator", "deleted_time", "deleter", "ext_data", "modified_time", "modifier", "remark", "sort_no", "status" },
                values: new object[] { 1, "IsEnableSwagger", "是否启用Swagger", 2, "true", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, "", null, null, null, 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "system_config",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
