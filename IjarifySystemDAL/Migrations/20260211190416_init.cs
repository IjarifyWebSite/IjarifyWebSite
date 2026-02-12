using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IjarifySystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Regoin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CK_IjarifyUserValidEmail", "Email like '_%@_%._%'");
                    table.CheckConstraint("CK_IjarifyUserValidPhone", "phone like '01%' and Phone not like '%[^0-9]%'");
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BedRooms = table.Column<int>(type: "int", nullable: false),
                    BathRooms = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ListingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmenityProperty",
                columns: table => new
                {
                    amenitiesId = table.Column<int>(type: "int", nullable: false),
                    propertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityProperty", x => new { x.amenitiesId, x.propertiesId });
                    table.ForeignKey(
                        name: "FK_AmenityProperty_Properties_propertiesId",
                        column: x => x.propertiesId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityProperty_amenities_amenitiesId",
                        column: x => x.amenitiesId,
                        principalTable: "amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Check_In = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Check_Out = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookings_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_bookings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "favourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_favourites_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_favourites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiries_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inquiries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                    table.CheckConstraint("CK_Review_Rating_Range", "Rating Between 1 and 10");
                    table.ForeignKey(
                        name: "FK_reviews_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "CreatedAt", "ImageUrl", "Latitude", "Longitude", "Regoin", "Street", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Cairo", new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1549144511-f099e773c147?w=800", 30.0444m, 31.2357m, "Nasr City", "Makram Ebeid", null },
                    { 2, "Alexandria", new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1572252009286-268acec5ca0a?w=800", 31.2001m, 29.9187m, "Smouha", "Mostafa Kamel", null },
                    { 3, "Cairo", new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1553913861-c0fddf2619ee?w=800", 30.0626m, 31.2197m, "Zamalek", "26th July", null },
                    { 4, "Giza", new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1568084680786-a84f91d1153c?w=800", 29.9668m, 30.9329m, "6th October", "Central Axis", null },
                    { 5, "Cairo", new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800", 30.0330m, 31.4913m, "New Cairo", "90th Street", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "ImageUrl", "Name", "Password", "Phone", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "15 El Nile Street, Maadi, Cairo", new DateTime(2024, 1, 20, 9, 15, 0, 0, DateTimeKind.Unspecified), "omar.ali@example.com", "https://i.pravatar.cc/150?img=33", "Omar Ali", "AQAAAAEAACcQAAAAEH8zQK", "01234567890", "Owner", null },
                    { 2, "28 Tahrir Street, Downtown, Cairo", new DateTime(2024, 3, 5, 11, 45, 0, 0, DateTimeKind.Unspecified), "nour.ibrahim@example.com", "https://i.pravatar.cc/150?img=27", "Nour Ibrahim", "AQAAAAEAACcQAAAAEH8zQK", "01098765432", "Owner", null },
                    { 3, "42 Nasr Road, Nasr City, Cairo", new DateTime(2024, 2, 28, 16, 20, 0, 0, DateTimeKind.Unspecified), "khaled.mahmoud@example.com", "https://i.pravatar.cc/150?img=51", "Khaled Mahmoud", "AQAAAAEAACcQAAAAEH8zQK", "01187654321", "User", null },
                    { 4, "10 Tahrir Square, Cairo", new DateTime(2024, 1, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.hassan@example.com", "https://i.pravatar.cc/150?img=12", "Ahmed Hassan", "AQAAAAEAACcQAAAAEH8zQK", "01012345678", "Owner", null },
                    { 5, "25 Alexandria Road, Cairo", new DateTime(2024, 1, 18, 10, 30, 0, 0, DateTimeKind.Unspecified), "fatima.mohamed@example.com", "https://i.pravatar.cc/150?img=45", "Fatima Mohamed", "AQAAAAEAACcQAAAAEH8zQK", "01123456789", "User", null }
                });

            migrationBuilder.InsertData(
                table: "amenities",
                columns: new[] { "Id", "Category", "CreatedAt", "Icon", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "wifi", "WiFi", null },
                    { 2, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "snowflake", "Air Conditioning", null },
                    { 3, "Exterior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "car", "Parking", null },
                    { 4, "Exterior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "water", "Swimming Pool", null },
                    { 5, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "dumbbell", "Gym", null },
                    { 6, "Exterior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "shield", "Security", null },
                    { 7, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "elevator", "Elevator", null },
                    { 8, "Exterior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "tree", "Garden", null },
                    { 9, "Exterior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "balcony", "Balcony", null },
                    { 10, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "fire", "Central Heating", null },
                    { 11, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "kitchen", "Kitchen Appliances", null },
                    { 12, "Interior", new DateTime(2024, 1, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "couch", "Furnished", null }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Area", "BathRooms", "BedRooms", "CreatedAt", "Description", "ListingType", "LocationId", "Price", "Title", "Type", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 150m, 2, 3, new DateTime(2024, 1, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Spacious 3-bedroom apartment with modern amenities and stunning city views. Perfect for families looking for comfort and convenience in the heart of Nasr City.", "Rent", 1, 15000m, "Luxury Apartment in Nasr City", "Apartment", null, 1 },
                    { 2, 350m, 4, 5, new DateTime(2024, 1, 25, 11, 30, 0, 0, DateTimeKind.Unspecified), "Beautiful villa with private beach access, pool, and garden. Ideal for vacation rentals or permanent residence. Enjoy the Mediterranean lifestyle.", "Rent", 2, 50000m, "Beachfront Villa in Alexandria", "Villa", null, 3 },
                    { 3, 60m, 1, 1, new DateTime(2024, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Cozy studio apartment in the heart of Zamalek, close to cafes, restaurants, and shops. Perfect for young professionals.", "Rent", 3, 8000m, "Modern Studio in Zamalek", "Studio", null, 1 },
                    { 4, 280m, 3, 4, new DateTime(2024, 2, 10, 14, 15, 0, 0, DateTimeKind.Unspecified), "Spacious family townhouse with garden, 4 bedrooms, and modern kitchen. Located in a quiet compound with 24/7 security.", "Rent", 4, 25000m, "Family Townhouse in 6th October", "Townhouse", null, 5 },
                    { 5, 250m, 3, 4, new DateTime(2024, 2, 15, 12, 45, 0, 0, DateTimeKind.Unspecified), "Luxurious penthouse with panoramic views, private terrace, and premium finishes. Features include smart home system and private parking.", "Rent", 5, 35000m, "Penthouse Apartment in New Cairo", "Apartment", null, 3 },
                    { 6, 100m, 1, 2, new DateTime(2024, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Well-maintained 2-bedroom apartment, perfect for small families or couples. Close to schools and shopping centers.", "Rent", 1, 10000m, "Affordable Apartment in Nasr City", "Apartment", null, 5 },
                    { 7, 200m, 2, 0, new DateTime(2024, 2, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), "Prime office location in downtown Cairo with modern facilities and easy access to public transportation.", "Rent", 3, 40000m, "Commercial Office Space in Downtown Cairo", "Office", null, 1 },
                    { 8, 180m, 3, 0, new DateTime(2024, 2, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), "Fully equipped medical clinic with modern equipment and established patient base. Great investment opportunity.", "Sale", 1, 2500000m, "Medical Clinic for Sale in Nasr City", "Clinic", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Inquiries",
                columns: new[] { "Id", "CreatedAt", "Message", "PropertyId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Is the apartment still available for rent starting next month? Also, are pets allowed?", 1, null, 2 },
                    { 2, new DateTime(2024, 2, 10, 14, 15, 0, 0, DateTimeKind.Unspecified), "I'm interested in renting the villa for the summer. Can we schedule a viewing?", 2, null, 4 },
                    { 3, new DateTime(2024, 2, 15, 11, 45, 0, 0, DateTimeKind.Unspecified), "Does the rent include utilities? Also, is there parking available?", 3, null, 2 },
                    { 4, new DateTime(2024, 2, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), "What is the minimum lease period for the office space?", 7, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "DiscountPercentage", "EndDate", "IsActive", "PropertyId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 10m, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "Spring Special - 10% Off First Month", null },
                    { 2, 15m, new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, "Summer Vacation Deal - 15% Off", null },
                    { 3, 5m, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6, "New Tenant Bonus - 5% Off", null },
                    { 4, 20m, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 8, "Limited Time - 20% Off Sale Price", null }
                });

            migrationBuilder.InsertData(
                table: "PropertyImages",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "PropertyId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=800", 1, null },
                    { 2, new DateTime(2024, 1, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800", 1, null },
                    { 3, new DateTime(2024, 1, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1493809842364-78817add7ffb?w=800", 1, null },
                    { 4, new DateTime(2024, 1, 25, 11, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800", 2, null },
                    { 5, new DateTime(2024, 1, 25, 11, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800", 2, null },
                    { 6, new DateTime(2024, 1, 25, 11, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800", 2, null },
                    { 7, new DateTime(2024, 1, 25, 11, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600047509807-ba8f99d2cdde?w=800", 2, null },
                    { 8, new DateTime(2024, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=800", 3, null },
                    { 9, new DateTime(2024, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1560185127-6ed189bf02f4?w=800", 3, null },
                    { 10, new DateTime(2024, 2, 10, 14, 15, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800", 4, null },
                    { 11, new DateTime(2024, 2, 10, 14, 15, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600566753190-17f0baa2a6c3?w=800", 4, null },
                    { 12, new DateTime(2024, 2, 10, 14, 15, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600573472591-ee6b68d14c68?w=800", 4, null },
                    { 13, new DateTime(2024, 2, 15, 12, 45, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800", 5, null },
                    { 14, new DateTime(2024, 2, 15, 12, 45, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600210492486-724fe5c67fb0?w=800", 5, null },
                    { 15, new DateTime(2024, 2, 15, 12, 45, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800", 5, null },
                    { 16, new DateTime(2024, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1502672023488-70e25813eb80?w=800", 6, null },
                    { 17, new DateTime(2024, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1560185009-5bf9f2849488?w=800", 6, null },
                    { 18, new DateTime(2024, 2, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800", 7, null },
                    { 19, new DateTime(2024, 2, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1497366811353-6870744d04b2?w=800", 7, null },
                    { 20, new DateTime(2024, 2, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1519494026892-80bbd2d6fd0d?w=800", 8, null },
                    { 21, new DateTime(2024, 2, 25, 13, 30, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1551076805-e1869033e561?w=800", 8, null }
                });

            migrationBuilder.InsertData(
                table: "bookings",
                columns: new[] { "Id", "Check_In", "Check_Out", "CreatedAt", "PropertyID", "Status", "TotalPrice", "UpdatedAt", "UserID" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, "Approved", 15000m, null, 2 },
                    { 2, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), 2, "Approved", 25000m, null, 4 },
                    { 3, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 5, 9, 15, 0, 0, DateTimeKind.Unspecified), 3, "Pending", 8000m, null, 2 },
                    { 4, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), 5, "Rejected", 8000m, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "favourites",
                columns: new[] { "Id", "CreatedAt", "PropertyId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 2 },
                    { 2, new DateTime(2024, 1, 28, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, null, 2 },
                    { 3, new DateTime(2024, 2, 20, 10, 45, 0, 0, DateTimeKind.Unspecified), 5, null, 4 },
                    { 4, new DateTime(2024, 2, 25, 14, 20, 0, 0, DateTimeKind.Unspecified), 3, null, 4 },
                    { 5, new DateTime(2024, 2, 28, 9, 15, 0, 0, DateTimeKind.Unspecified), 7, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "PropertyId", "Rating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Amazing apartment! Very spacious and clean. The location is perfect and the landlord is very responsive.", new DateTime(2024, 2, 5, 15, 30, 0, 0, DateTimeKind.Unspecified), 1, 9, null, 2 },
                    { 2, "Dream villa! The beach access is incredible and the kids love the pool. Highly recommended!", new DateTime(2024, 2, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, null, 4 },
                    { 3, "Nice studio in a great location. Perfect for my needs as a single professional.", new DateTime(2024, 2, 25, 14, 20, 0, 0, DateTimeKind.Unspecified), 3, 8, null, 2 },
                    { 4, "Excellent family townhouse. The compound is safe and the neighbors are friendly. Kids love playing in the garden.", new DateTime(2024, 3, 5, 16, 45, 0, 0, DateTimeKind.Unspecified), 4, 9, null, 2 },
                    { 5, "Good apartment for the price. Could use some updates but overall a solid choice.", new DateTime(2024, 3, 15, 13, 10, 0, 0, DateTimeKind.Unspecified), 6, 7, null, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityProperty_propertiesId",
                table: "AmenityProperty",
                column: "propertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_PropertyID",
                table: "bookings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserID",
                table: "bookings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_favourites_PropertyId",
                table: "favourites",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_favourites_UserId",
                table: "favourites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_PropertyId",
                table: "Inquiries",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_UserId",
                table: "Inquiries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PropertyId",
                table: "Offers",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_LocationId",
                table: "Properties",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_PropertyId",
                table: "reviews",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserId",
                table: "reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityProperty");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "favourites");

            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "amenities");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
