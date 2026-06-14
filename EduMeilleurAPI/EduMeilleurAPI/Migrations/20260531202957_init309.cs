using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class init309 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "Attachments",
                newName: "FileName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80e324a9-d322-44b0-a7b8-7892db320292", "AQAAAAIAAYagAAAAEBPYszBKzKFdpuSjn3ZbiP5fXYRiUGP6fbYQ+NVas+pET7Xagp9I3cl9XKfJIUjzUQ==", "e4f75087-e88e-4c9d-8b2f-2ec9e026cf12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de0d52af-64fc-4322-be9a-9e2073959f99", "AQAAAAIAAYagAAAAECmH47nZt2Hasw1WoThXUybubIesj/sIAz7vwsqqm1eLxZw4uixxZhQvAdtf5yJ17Q==", "50edf7fa-3327-4b36-8881-7c84f708d5ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a9a5a5a-366b-420d-bfc2-6de59da86207", "AQAAAAIAAYagAAAAEK5JxVI0fvk0mTa5xbqzlE4UlJpu0IXF3J6uACiOvYdYgv0LcO9bVIARxVym6kGOHQ==", "65b7f503-c45e-40c1-ae69-a22c30fc4647" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3f13382-0356-4ce2-9a05-1bc63fdb40cf", "AQAAAAIAAYagAAAAEBvx4ueNFBm3KrdkFAY/78EFOS8i727DwlV6MDidYUrgz1AYxknENG47Innjp2HuOw==", "2f7cd94b-9957-496c-9f25-3f271dc68a23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbe59bc2-c0c2-4816-bbae-a2f487e4f1c8", "AQAAAAIAAYagAAAAEAr8efj7I+zfWHhRyrfCT0yMosTE+KVkkEGmiQVmrIRk0NOf0fkuz35iG+790bVaUQ==", "2cdbd9b7-6310-4abf-95e0-24f9d7c08fa1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b476634-69c6-4abf-a3a9-933a9570f246", "AQAAAAIAAYagAAAAEGfpEqB8uYxoprPbt4u+tW9b17gSVauf/qdGFmxhBuqbXbjYWJDu4zTwEPGkv6kSLQ==", "16e917c4-a997-48d6-91e2-4836eb302c6b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Attachments",
                newName: "Filename");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b6ac133-b312-4f70-9c3f-b998fdb435ba", "AQAAAAIAAYagAAAAENN5MvB0CZhomQoxJPFUOpjFgrsJ4/+lJUub7hcat4zFfKDZGquAkigXuHV8OGOrlw==", "974d509c-a2d5-443a-b3eb-a39f56fe909b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71f71bdb-b648-4149-b046-22fd86d6edea", "AQAAAAIAAYagAAAAEJHBaz91+mTSOP9O0+w7R2YmlAFOYt0eatqQV5RBqnsgaUWxQtLmd7IYbouMb7RYMw==", "0b1e5caa-1a57-4035-ac88-3a21970aa17a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b32e2a51-4897-46f4-96ed-d78817cf7498", "AQAAAAIAAYagAAAAEKvvCBVdqOJaUPQtaPPAsQOIrtSpu8hpL1TSPzKsJiqpcU68nX6u73hHyqGmPGv/xg==", "e58da28c-c35d-4463-9be0-dea7caab697b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f07b7f7-fd11-419a-9038-e76010fb9777", "AQAAAAIAAYagAAAAELe+G/JaX1hSc27NskZBmzb7sXh9i1F/NWeLkV2Dh+eA//QjWiG133wvtvmsJOJtCA==", "24fd9d20-d654-4319-b0a3-d49f145bea64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85cd124b-6970-4e9a-bd07-d9fab0024732", "AQAAAAIAAYagAAAAEFlOgpg9wHgmjbNmMevi4WANV6GlXnkM7HvcmQB4Tap5YYgrLU6qQbuZgfxX77n2WA==", "b8bca621-9a9d-4ef1-9e1e-2306accb7a02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e867c66-28f8-462b-a198-777c5b9fdf68", "AQAAAAIAAYagAAAAEBoO5Eg9LCNi4Qh1R+pBgYk8fkbMMbFcmay861S7WyCbsTGVOH4OqODyrNokmdnkPg==", "654b6f04-28e8-43d5-9989-03361675c273" });
        }
    }
}
