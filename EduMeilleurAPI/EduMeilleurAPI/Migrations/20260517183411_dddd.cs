using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class dddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c515e5b-b5aa-4301-83fd-d590d84750da", "AQAAAAIAAYagAAAAEP8jU32TEF8wu0pSVWwPDfAbwwzoYthhMEG0C4Pb5pvijlFwLysOFvKB9UzraYxfXQ==", "b335ced0-7110-42ec-8e12-84c124086769" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6ec03bc-d16f-4167-9cd0-3dc5f88dd5c7", "AQAAAAIAAYagAAAAEDSPM0pF2kXO9TUnJaYPK/IyCXyUee+RaBqDO1RPqfQ1IFnYoCuPwE0VfULwnKclyw==", "75b80159-2787-441e-b2d3-30a218b4d3e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76467b3f-5d27-4a1c-8ca0-bb9bb1d68be1", "AQAAAAIAAYagAAAAEHvy02iL6s9ZJ0y7UdfcUqTvnik9LP4jtFNo2PmGJ/v9ELq5VEv+pU/p87rFpkHOKg==", "448d47ce-171d-4b7f-b006-49fb2c3bc825" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bd0c55e-1cea-48d6-b2c5-f47b69d671f1", "AQAAAAIAAYagAAAAEKqyO/O0nRne4e6s5wcixaic/II/iuMUG5urljJcOjRo35OejOpmviNOls9p8UWJ3w==", "819ad57e-1f61-43e0-9bc4-3be73e1b3ef1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "059ed5b2-c7b6-4064-aae1-e309e0a95eb1", "AQAAAAIAAYagAAAAEGcU/H9ddNAN5vay5irGWiIwdIZ/qlSSCDHcZ8f5u0nv5+ufFisTDDmNkmjG/dj4pA==", "eeaaf790-255f-492d-9d46-a06ece03b96a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9e20139-732f-4dba-aa59-2da0194d7b14", "AQAAAAIAAYagAAAAEFszx7jgH2yhkcZB/12acG5msirJGan2gw9KE6zCEvAFv73JXzPWOjcZLc/VYpLSiQ==", "1fced978-50b4-47a8-b1a4-857ab43e2882" });

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Introduction to Vectors");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Components and Orientation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f1c459d-3bbc-4bf2-8b0e-b1de733458a4", "AQAAAAIAAYagAAAAEBgKJFn5jJyVUsS6mvFrIbzgpMKMjbp/fGIxenKGjQYAgB0pbRyWHU2ZZ2AMm8+y3A==", "b9af4c4d-6ec4-409a-abdf-1db315c6df5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51758db9-a57b-410d-a1fe-a516194227b9", "AQAAAAIAAYagAAAAEKXxfKgt0qcSkSjMjd7HGSVSeNEm30XAQCZI8fYCvqB8kA5E+yWOt3fjlT9RNHwt6A==", "8b60ad0a-7ff2-437f-b54b-7eaf5b24e818" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111113",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0aa47848-0615-4595-97d1-709048c2366e", "AQAAAAIAAYagAAAAEB9VdirYGf6AQqNV7/AA0aQAmSJtdQ2tMkFtsBIpkqevPeq24op/DC4hWm5CLoT3lw==", "5ece9236-c083-4435-9c48-5d1bbc19cf51" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111114",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28b5a64a-9bea-4f60-a182-a4a7f8c3bf67", "AQAAAAIAAYagAAAAEHRlS3I843iGuE6847+TqJZLsxXdbr7R4gpnwKtt4v4PCg16sG+ypo9UHoi3bDOzOw==", "ee1c62c0-a784-44fa-92ca-41b7a2c02488" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111115",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dd18ce8-2465-41c2-9d15-4e5c3339d025", "AQAAAAIAAYagAAAAEDsi9PVkEn7H37sbIQ+YTPVwRLvIb9cNITG1BEhWKjxqkaZ38RS/O9rl98H5Pf9SlA==", "be775f82-f9b8-4ec0-b787-9753df32b405" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111116",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3582a1f-5c41-48f4-93dc-a71274973e84", "AQAAAAIAAYagAAAAEDZprD1aOwfdLsa+wCjpunEzLhBkaMbTBDLxzFy4+p768Ef22NHP3dPEaDHx9LWeNA==", "008bff6c-a07c-418c-a7d6-08fe6aa3c5e6" });

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "1.1 Introduction to Vectors");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "1.2 Components and Orientation");
        }
    }
}
