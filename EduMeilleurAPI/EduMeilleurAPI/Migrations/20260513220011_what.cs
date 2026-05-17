using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class what : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "ChapterId", "Content", "Title" },
                values: new object[] { 1, 1, "SN5_IntroVectors_EN.md", "1.1 Introduction to Vectors" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "339d1e38-c9af-48e5-86de-8e5b43b9c327", "AQAAAAIAAYagAAAAEKnYiXvYgeRQT2HOIpD/RsZTkZEuxuL02rLcg04XCmfVZ86mfMUb7Z5D5nmfXQwhCQ==", "9ed6d73e-dc90-4deb-8007-6126979b595c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e6158de-f06c-4072-bb1c-139c8e66e212", "AQAAAAIAAYagAAAAENVvTqmYC7JBlCJsKVdoESJsGPagBXep22gHCiXdC7mSLjBVdgnnXJFvLPipZ94SJg==", "b1d1bd6a-048d-4494-b8a4-24607c87a9d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11c8d6f0-bdfd-4df0-bb85-dc3eb9a5a930", "AQAAAAIAAYagAAAAEAUFCbypvdeGXZ4Ez7Dnk6bdw+kompLcyTjneUO7BVfdGowP3EoNOVRBqFIOlcZWzQ==", "f0f0978c-a314-4397-81de-03c73b0d46b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45dd391d-e92d-4184-ae1d-591742cb8db2", "AQAAAAIAAYagAAAAEB0xWFkQzVtL084wenLwqkH4n3145hDScP38tKiV8guu9xFZjNWSgW03+YGwv6fo/A==", "72426117-cd4e-4b0d-a1b6-52097831f1c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f6b27ed-2f16-435b-94ff-df05a2bb1365", "AQAAAAIAAYagAAAAEGGsuV9VDb+F2oePcWyA1vlSfQka4if9X3GZlPc7SgzJos8s2FGqjIskvR5xJQKOJQ==", "fa4bcf46-5662-4e27-974f-1c7ff063db22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23b6a824-c8a4-4253-8d4d-09b627fa6fd4", "AQAAAAIAAYagAAAAEKD9hKi32WJdbmTNX6Bt0tGNX2JJ53iNl5i1CwFoNab5YbA1nSI7tGOpcgi2x5lLew==", "e06cf7c6-7300-4d12-b506-d3fb44e8c859" });
        }
    }
}
