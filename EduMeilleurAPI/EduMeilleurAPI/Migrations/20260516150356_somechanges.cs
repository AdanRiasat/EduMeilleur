using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class somechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
