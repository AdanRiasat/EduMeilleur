using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class what2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Introduction to Vectors");

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "ChapterId", "Content", "Title" },
                values: new object[] { 2, 1, "SN5_Components_EN.md", "Vectors and Components" });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseNotes_NotesId",
                table: "ExerciseNotes",
                column: "NotesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseNotes");

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "247d878d-5277-4dfd-90ca-2e24d5ec0e9f", "AQAAAAIAAYagAAAAEBTjoUYGsC4DI8gpyv8U1X+syUU47kntXFYdlPezM21KuI9Zoehstwtwt4/th3opGg==", "e0e3b5c4-5cd2-48a1-bb10-45867e6af2c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28c1cf86-bc20-4ba4-8eb4-df1a5a6e4cbe", "AQAAAAIAAYagAAAAENoSPJ1fwKZ/uoo35DJg3SnzkCN+8+q6wPCzOck5dCLM/3sOCcRN37HzNCfkLDQMCg==", "e23eb11c-9bf2-4fec-b341-f3dc4b9e02d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4273c2c9-52a5-4280-a610-251f263d7ada", "AQAAAAIAAYagAAAAEI975pP16x76nz32BoGVODb+33gP8bJi3ZaO5s6hJhyUoh0T3L0raM7MQlTQ9esoGw==", "1ab77b14-3e8c-4812-bf21-c5336043c92a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93ea49b0-2a42-4081-889c-a6bd83a86120", "AQAAAAIAAYagAAAAEOoiC8lzHOXuAl5XtkvJnFPMlSdZb7w4QKc5lHdhZFtN1mdYL90GpahfZ4BxNX/okA==", "0e39b5e5-122d-4920-9935-609a74629137" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5c666da-f1d2-4b38-a732-2c6c2680757e", "AQAAAAIAAYagAAAAEAwtmlEdMcWt1uydMmxhfRvg+6eWDIm+CEB2yrfKypKlP5hBnnXOayKPlMNYrkUbBQ==", "b6d140bb-2575-4b0b-a794-c03931062732" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d987aac3-646f-4362-adc5-559a89fcfbe5", "AQAAAAIAAYagAAAAEBiWzBFeuXYu7Slv4bVP9xXyFAcNJXcUCYMkTjazBUD1q0H1vZOfMQHfutv/6qsJow==", "45ba4526-ec3a-4f37-8e7b-c16ff0440267" });

            migrationBuilder.UpdateData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "1.1 Introduction to Vectors");
        }
    }
}
