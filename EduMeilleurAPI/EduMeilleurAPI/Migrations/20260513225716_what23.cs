using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class what23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseNotes");

            migrationBuilder.DeleteData(
                table: "Chat",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chat",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "NoteExercise",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteExercise", x => new { x.NoteId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_NoteExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteExercise_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d349de1-12af-4aca-8cdf-1c8ec8e86bfe", "AQAAAAIAAYagAAAAENSfRcAOCYIqiGhxSp3ILLzHAWITzv9cLL5OW6E+qYxWn3dnNawQXGyVMOmg4Zk4rg==", "22a23e64-da60-4dba-a91d-65adb1de0e6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cd4aace-2963-4bc2-9494-f55ebd84ca34", "AQAAAAIAAYagAAAAECc3qn0pXgADOnPOkKQl9Y6eIrZ725a9bjAZ6aVyj+PQJN1PaCRd8OW8AN5xCaTF7w==", "227b5c87-a62c-4c2b-85fd-5331199cea9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d93d2e16-ea95-4893-83ac-fe64f711e6e7", "AQAAAAIAAYagAAAAENVMRNIs6H6t9+KrXwGug/m30kfiWHCi6yy6brScTx+rukAeqIThb3AYJGyTdau2uA==", "bfd99d44-3a6d-4614-bcf1-31088d867033" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a52e967f-6644-47ea-b56d-9bbb29120369", "AQAAAAIAAYagAAAAEDw7HWQDIPhBv3R8UkrugRut5ifShLTH1xQlNWeCMil2zI0HxSGKz+zj53k6sXu0zA==", "e9376ed6-de57-409f-a8aa-1af77ae67450" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d4853c-97a3-4ec0-bb5e-569b18b5199c", "AQAAAAIAAYagAAAAECiUHDe27NYOdJi5xIjuej5win3EnIu3RTmDIIhEBQkNW4WmRQ8iJYWtd7A93Ximyw==", "b4d7b570-9b09-4a64-a58a-cffdfa00b4f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "531f2596-78bc-48d9-acfe-ca08ff7e81d5", "AQAAAAIAAYagAAAAEIQ6i3gxGQOp3bwI9JK+8mMrUQMKaazF4lOhtcQfRJ5S0gDeMbZux7AcSn7U2UQu6g==", "5713c888-d9cf-4685-94ba-b3e4d53cdd3b" });

            migrationBuilder.InsertData(
                table: "NoteExercise",
                columns: new[] { "ExerciseId", "NoteId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteExercise_ExerciseId",
                table: "NoteExercise",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteExercise");

            migrationBuilder.CreateTable(
                name: "ExerciseNotes",
                columns: table => new
                {
                    ExercicesId = table.Column<int>(type: "integer", nullable: false),
                    NotesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseNotes", x => new { x.ExercicesId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_ExerciseNotes_Exercise_ExercicesId",
                        column: x => x.ExercicesId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseNotes_Notes_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f93727f3-de18-4d42-b54c-4eb7c661f2a5", "AQAAAAIAAYagAAAAEFcnklnin13TCzcKdkGOC9m8rDg70mnxPp0432Cfv0x4ebJiF3XbBaTrEatBOef0aQ==", "bfafebe5-b01c-48a6-a104-081b8a98f46b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1bdb480-119d-4fc2-8be6-b54addcc6676", "AQAAAAIAAYagAAAAEHC0SIzPhOyv7qMWqodeoPdJip4nDizo/HAuAepXXxwnbWBqivG8cCR35D+vsITK6g==", "2e65c3c4-8147-4a84-b16e-f720990fc517" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9adbe2b3-e4b8-4bea-8f73-65bc282d5b56", "AQAAAAIAAYagAAAAEJzW2G4GgeR123fJAhAZr7jAA2yjlj6nKBsIUlUKaMaPc6xl7mdMj5mvKNtb1Wkk8A==", "249e8d4b-28f7-4264-ac00-b4e126759bb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd99cab5-0318-4e62-81a5-a576b4a00c28", "AQAAAAIAAYagAAAAEBsSjGkJnBA1xrtj2Tm+BD8jYH4/i0c24TFS6rmbpPnfFNtFiCjDMZ9DR1hM1/Dl/g==", "c759b87e-7355-4a0f-99b1-a553df4a2d92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d23b82d3-6151-484e-afd3-82a11adc253c", "AQAAAAIAAYagAAAAEJa+M79WiAxPt9sHnE2Y16kUUy9XJhgZoxkhMpreyRfTJQg5taVp4HcEf0hw7BvEEg==", "d5dc79c5-dd0e-48f7-9d10-71ab3db63dae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38d5a178-9fc7-453f-8499-04bbf82f2b55", "AQAAAAIAAYagAAAAEAeqiGyYe5eEAAqtWpe2qqmV6sFXF/oSxRSuCwwV0Gj1y2IwKpSJUW+6lop1Kkw5bQ==", "e7f25152-6d48-422f-9bdf-5e1faf10cdbf" });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "Id", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "My first Chat", "11111111-1111-1111-1111-111111111111" },
                    { 2, "My second Chat", "11111111-1111-1111-1111-111111111111" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseNotes_NotesId",
                table: "ExerciseNotes",
                column: "NotesId");
        }
    }
}
