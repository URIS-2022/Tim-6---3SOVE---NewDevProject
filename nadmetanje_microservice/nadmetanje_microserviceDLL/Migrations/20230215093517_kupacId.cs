﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nadmetanjemicroserviceDAL.Migrations
{
    /// <inheritdoc />
    public partial class kupacId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "KupacId",
                table: "Nadmetanja",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KupacId",
                table: "Nadmetanja");
        }
    }
}
