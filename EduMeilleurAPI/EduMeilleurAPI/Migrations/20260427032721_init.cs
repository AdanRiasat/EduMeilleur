using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduMeilleurAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    IQPoints = table.Column<int>(type: "integer", nullable: false),
                    SchoolYear = table.Column<int>(type: "integer", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    MimeType = table.Column<string>(type: "text", nullable: true),
                    SchoolId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapters_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionTeacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTeacher_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ChapterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ChapterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ChapterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IsUser = table.Column<bool>(type: "boolean", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: false),
                    QuestionTeacherId = table.Column<int>(type: "integer", nullable: true),
                    FeedbackId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachments_QuestionTeacher_QuestionTeacherId",
                        column: x => x.QuestionTeacherId,
                        principalTable: "QuestionTeacher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: false),
                    QuestionTeacherId = table.Column<int>(type: "integer", nullable: true),
                    FeedbackId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Picture_QuestionTeacher_QuestionTeacherId",
                        column: x => x.QuestionTeacherId,
                        principalTable: "QuestionTeacher",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "admin", "ADMIN" },
                    { "2", null, "teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "FileName", "IQPoints", "LockoutEnabled", "LockoutEnd", "MimeType", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SchoolId", "SchoolYear", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 0, null, "339d1e38-c9af-48e5-86de-8e5b43b9c327", "2ariasat@gmail.com", false, null, 0, false, null, null, "2ARIASAT@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKnYiXvYgeRQT2HOIpD/RsZTkZEuxuL02rLcg04XCmfVZ86mfMUb7Z5D5nmfXQwhCQ==", null, false, null, null, null, null, "9ed6d73e-dc90-4deb-8007-6126979b595c", false, "admin" },
                    { "11111111-1111-1111-1111-111111111112", 0, null, "2e6158de-f06c-4072-bb1c-139c8e66e212", "bobibo@mail.com", false, null, 0, false, null, null, "BOBIBO@MAIL.COM", "ROBERT LEBOIS", "AQAAAAIAAYagAAAAENVvTqmYC7JBlCJsKVdoESJsGPagBXep22gHCiXdC7mSLjBVdgnnXJFvLPipZ94SJg==", null, false, null, null, null, null, "b1d1bd6a-048d-4494-b8a4-24607c87a9d3", false, "Robert Lebois" },
                    { "11111111-1111-1111-1111-111111111113", 0, null, "11c8d6f0-bdfd-4df0-bb85-dc3eb9a5a930", "hmmm@mail.com", false, null, 0, false, null, null, "HMMM@MAIL.COM", "JEROME LAPLANTE", "AQAAAAIAAYagAAAAEAUFCbypvdeGXZ4Ez7Dnk6bdw+kompLcyTjneUO7BVfdGowP3EoNOVRBqFIOlcZWzQ==", null, false, null, null, null, null, "f0f0978c-a314-4397-81de-03c73b0d46b4", false, "Jerome Laplante" },
                    { "11111111-1111-1111-1111-111111111114", 0, null, "45dd391d-e92d-4184-ae1d-591742cb8db2", "teacher3@mail.com", false, null, 0, false, null, null, "TEACHER3@MAIL.COM", "EMILY TREMBLAY", "AQAAAAIAAYagAAAAEB0xWFkQzVtL084wenLwqkH4n3145hDScP38tKiV8guu9xFZjNWSgW03+YGwv6fo/A==", null, false, null, null, null, null, "72426117-cd4e-4b0d-a1b6-52097831f1c0", false, "Emily Tremblay" },
                    { "11111111-1111-1111-1111-111111111115", 0, null, "4f6b27ed-2f16-435b-94ff-df05a2bb1365", "teacher4@mail.com", false, null, 0, false, null, null, "teacher4@MAIL.COM", "TYRONE ROCHELIEU", "AQAAAAIAAYagAAAAEGGsuV9VDb+F2oePcWyA1vlSfQka4if9X3GZlPc7SgzJos8s2FGqjIskvR5xJQKOJQ==", null, false, null, null, null, null, "fa4bcf46-5662-4e27-974f-1c7ff063db22", false, "Tyrone Rochelieu" },
                    { "11111111-1111-1111-1111-111111111116", 0, null, "23b6a824-c8a4-4253-8d4d-09b627fa6fd4", "teacher5@mail.com", false, null, 0, false, null, null, "teacher5@MAIL.COM", "SARAH LAIDE", "AQAAAAIAAYagAAAAEKD9hKi32WJdbmTNX6Bt0tGNX2JJ53iNl5i1CwFoNab5YbA1nSI7tGOpcgi2x5lLew==", null, false, null, null, null, null, "e06cf7c6-7300-4d12-b506-d3fb44e8c859", false, "Sarah Laide" }
                });

            migrationBuilder.InsertData(
                table: "Picture",
                columns: new[] { "Id", "FeedbackId", "FileName", "MimeType", "QuestionTeacherId" },
                values: new object[,]
                {
                    { 21, null, "graph_plainVector.png", "image/png", null },
                    { 22, null, "graph_vectorComponentsTriangle.png", "image/png", null }
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Antoine-Brossard" },
                    { 2, "Lucille-Teasdale" }
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Introduction to advanced math concepts, focusing on functions, trigonometry, and problem-solving for science pathways.", "SN4", "Math" },
                    { 2, "Prepares students for post-secondary science programs with calculus basics, advanced algebra, and physics applications.", "SN5", "Math" },
                    { 3, "Practical mathematics for daily life: statistics, financial math, geometry, and measurement.", "CST4", "Math" },
                    { 4, "Continues CST4 with emphasis on budgeting, probability, and applied problem-solving.", "CST5", "Math" },
                    { 5, "Math for technical and science careers: algebra, systems of equations, and basic trigonometry.", "TS4", "Math" },
                    { 6, "Expands on TS4 with more complex functions, analytic geometry, and preparation for college science programs.", "TS5", "Math" },
                    { 7, "General science covering biology, chemistry, and physics fundamentals through hands-on experiments.", "ST", "Science" },
                    { 8, "Science and technology with enriched content for students aiming at advanced science or technical studies.", "STE", "Science" },
                    { 9, "Study of matter, chemical reactions, stoichiometry, and laboratory techniques.", "Chimie", "Science" },
                    { 10, "Exploration of motion, forces, energy, and waves, with emphasis on practical experiments.", "Physique", "Science" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "11111111-1111-1111-1111-111111111111" },
                    { "2", "11111111-1111-1111-1111-111111111112" },
                    { "2", "11111111-1111-1111-1111-111111111113" },
                    { "2", "11111111-1111-1111-1111-111111111114" },
                    { "2", "11111111-1111-1111-1111-111111111115" },
                    { "2", "11111111-1111-1111-1111-111111111116" }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "Id", "SubjectId", "Title" },
                values: new object[] { 1, 2, "Vectors" });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "Id", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "My first Chat", "11111111-1111-1111-1111-111111111111" },
                    { 2, "My second Chat", "11111111-1111-1111-1111-111111111111" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "ChapterId", "Content", "Title" },
                values: new object[,]
                {
                    { 1, 1, "SN5_IntroVectors_EN.md", "1.1 Introduction to Vectors" },
                    { 2, 1, "SN5_ComponentsandOrientation_EN.md", "1.2 Components and Orientation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SchoolId",
                table: "AspNetUsers",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_FeedbackId",
                table: "Attachments",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_QuestionTeacherId",
                table: "Attachments",
                column: "QuestionTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_SubjectId",
                table: "Chapters",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserId",
                table: "Chat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ChapterId",
                table: "Exercise",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_userId",
                table: "Feedbacks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ChapterId",
                table: "Notes",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_FeedbackId",
                table: "Picture",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_QuestionTeacherId",
                table: "Picture",
                column: "QuestionTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTeacher_userId",
                table: "QuestionTeacher",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_ChapterId",
                table: "Video",
                column: "ChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "QuestionTeacher");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
