using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wkmvc.Data.Migrations
{
    public partial class _1917 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PASSWORD",
                table: "SYS_USER",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "USERNAME",
                table: "SYS_USER",
                maxLength: 50,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PASSWORD",
                table: "SYS_USER");

            migrationBuilder.AlterColumn<string>(
                name: "USERNAME",
                table: "SYS_USER",
                nullable: true);
        }
    }
}
