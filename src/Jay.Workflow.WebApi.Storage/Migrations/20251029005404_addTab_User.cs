using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jay.Workflow.WebApi.Storage.Migrations
{
    /// <inheritdoc />
    public partial class addTab_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "system_config",
                comment: "系统配置表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "department",
                comment: "部门表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "system_config",
                type: "int",
                nullable: false,
                comment: "启用状态，0：禁用，1：启用",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "sort_no",
                table: "system_config",
                type: "int",
                nullable: false,
                comment: "排序编号",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "system_config",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ext_data",
                table: "system_config",
                type: "longtext",
                nullable: true,
                comment: "扩展数据",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "config_value",
                table: "system_config",
                type: "longtext",
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "config_type",
                table: "system_config",
                type: "int",
                nullable: false,
                comment: "配置类型",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "config_name",
                table: "system_config",
                type: "longtext",
                nullable: false,
                comment: "配置名称",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "config_key",
                table: "system_config",
                type: "longtext",
                nullable: false,
                comment: "配置key",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "department",
                type: "int",
                nullable: false,
                comment: "启用状态，0：禁用，1：启用",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "sort_no",
                table: "department",
                type: "int",
                nullable: false,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "department",
                type: "longtext",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_dept_id",
                table: "department",
                type: "char(36)",
                nullable: true,
                comment: "上级部门id",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dept_type_name",
                table: "department",
                type: "longtext",
                nullable: true,
                comment: "部门类型名称",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "dept_type_id",
                table: "department",
                type: "int",
                nullable: true,
                comment: "部门类型id",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dept_name",
                table: "department",
                type: "longtext",
                nullable: false,
                comment: "部门名称",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_id",
                table: "department",
                type: "char(36)",
                nullable: false,
                comment: "部门id",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dept_icon",
                table: "department",
                type: "longtext",
                nullable: true,
                comment: "部门图标",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "resource",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    resource_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "资源id", collation: "ascii_general_ci"),
                    resource_name = table.Column<string>(type: "longtext", nullable: false, comment: "资源名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    app_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "应用id", collation: "ascii_general_ci"),
                    resource_url = table.Column<string>(type: "longtext", nullable: true, comment: "资源Url")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "longtext", nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    is_show = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否显示"),
                    resource_type = table.Column<int>(type: "int", nullable: false, comment: "资源类型"),
                    button_type = table.Column<int>(type: "int", nullable: true, comment: "按钮类型"),
                    parent_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "上级id", collation: "ascii_general_ci"),
                    creator = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modifier = table.Column<int>(type: "int", nullable: true),
                    modified_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter = table.Column<int>(type: "int", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource", x => x.id);
                },
                comment: "资源表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色id", collation: "ascii_general_ci"),
                    role_code = table.Column<string>(type: "longtext", nullable: false, comment: "角色编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_name = table.Column<string>(type: "longtext", nullable: false, comment: "角色名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_description = table.Column<string>(type: "longtext", nullable: true, comment: "角色描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creator = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modifier = table.Column<int>(type: "int", nullable: true),
                    modified_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter = table.Column<int>(type: "int", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                },
                comment: "角色表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role_resource",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色ID", collation: "ascii_general_ci"),
                    resource_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "资源id", collation: "ascii_general_ci"),
                    creator = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modifier = table.Column<int>(type: "int", nullable: true),
                    modified_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter = table.Column<int>(type: "int", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_resource", x => x.id);
                },
                comment: "角色资源表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "用户id", collation: "ascii_general_ci"),
                    user_name = table.Column<string>(type: "longtext", nullable: false, comment: "用户名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_avatar = table.Column<string>(type: "longtext", nullable: true, comment: "用户头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_phone = table.Column<string>(type: "longtext", nullable: false, comment: "用户手机号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_email = table.Column<string>(type: "longtext", nullable: true, comment: "用户邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creator = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modifier = table.Column<int>(type: "int", nullable: true),
                    modified_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter = table.Column<int>(type: "int", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                },
                comment: "用户表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_department",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "用户id", collation: "ascii_general_ci"),
                    dept_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "部门id", collation: "ascii_general_ci"),
                    creator = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modifier = table.Column<int>(type: "int", nullable: true),
                    modified_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter = table.Column<int>(type: "int", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_department", x => x.id);
                },
                comment: "用户部门表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "用户id", collation: "ascii_general_ci"),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, comment: "角色ID", collation: "ascii_general_ci"),
                    creator = table.Column<int>(type: "int", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modifier = table.Column<int>(type: "int", nullable: true),
                    modified_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter = table.Column<int>(type: "int", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.id);
                },
                comment: "用户角色表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resource");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "role_resource");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_department");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.AlterTable(
                name: "system_config",
                oldComment: "系统配置表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "department",
                oldComment: "部门表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "system_config",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "启用状态，0：禁用，1：启用");

            migrationBuilder.AlterColumn<int>(
                name: "sort_no",
                table: "system_config",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序编号");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "system_config",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ext_data",
                table: "system_config",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "扩展数据")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "config_value",
                table: "system_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "配置值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "config_type",
                table: "system_config",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "配置类型");

            migrationBuilder.AlterColumn<string>(
                name: "config_name",
                table: "system_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "配置名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "config_key",
                table: "system_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "配置key")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "department",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "启用状态，0：禁用，1：启用");

            migrationBuilder.AlterColumn<int>(
                name: "sort_no",
                table: "department",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "department",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "备注")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_dept_id",
                table: "department",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true,
                oldComment: "上级部门id")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dept_type_name",
                table: "department",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "部门类型名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "dept_type_id",
                table: "department",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "部门类型id");

            migrationBuilder.AlterColumn<string>(
                name: "dept_name",
                table: "department",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "部门名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_id",
                table: "department",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "部门id")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dept_icon",
                table: "department",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "部门图标")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
