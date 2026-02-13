using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Data.Context
{
    public class IjarifyDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public IjarifyDbContext(DbContextOptions<IjarifyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Omar Ali",
                    Email = "omar.ali@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEH8zQK",
                    PhoneNumber = "01234567890",
                    Address = "cairo",
                    ImageUrl = "https://i.pravatar.cc/150?img=33",
                    CreatedAt = new DateTime(2024, 1, 20, 9, 15, 0)
                },
                new User
                {
                    Id = 2,
                    Name = "Nour Ibrahim",
                    Email = "nour.ibrahim@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEH8zQK",
                    PhoneNumber = "01098765432",
                    Address = "cairo",
                    ImageUrl = "https://i.pravatar.cc/150?img=27",
                    CreatedAt = new DateTime(2024, 3, 5, 11, 45, 0)
                },
                new User
                {
                    Id = 3,
                    Name = "Khaled Mahmoud",
                    Email = "khaled.mahmoud@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEH8zQK",
                    PhoneNumber = "01187654321",
                    Address = "cairo",
                    ImageUrl = "https://i.pravatar.cc/150?img=51",
                    CreatedAt = new DateTime(2024, 2, 28, 16, 20, 0)
                },
                new User
                {
                    Id = 4,
                    Name = "Ahmed Hassan",
                    Email = "ahmed.hassan@example.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEH8zQK",
                    PhoneNumber = "01012345678",
                    Address = "cairo",                 // ✅ Add this line
                    ImageUrl = "https://i.pravatar.cc/150?img=12",
                    CreatedAt = new DateTime(2024, 1, 15, 8, 0, 0, 0, DateTimeKind.Unspecified)
                },
            new User
            {
                Id = 5,
                Name = "Fatima Mohamed",
                Email = "fatima.mohamed@example.com",
                PasswordHash = "AQAAAAEAACcQAAAAEH8zQK",
                PhoneNumber = "01123456789",
                Address = "cairo",                // ✅ Already there, just needs proper syntax
                ImageUrl = "https://i.pravatar.cc/150?img=45",
                CreatedAt = new DateTime(2024, 1, 18, 10, 30, 0, 0, DateTimeKind.Unspecified)
            }
                        );

            modelBuilder.Entity<Location>().HasData(
     // 1. New Cairo (General/Fifth Settlement)
     new Location { Id = 1, City = "New Cairo", Regoin = "Fifth Settlement", Street = "North 90th St", Latitude = 30.0055m, Longitude = 31.4782m, ImageUrl = "https://prod-images.nawy.com/processed/area/image/2/high.webp", CreatedAt = new DateTime(2024, 1, 1) },

     // 2. New Administrative Capital
     new Location { Id = 2, City = "New Capital City", Regoin = "R7 District", Street = "Bin Zayed Axis", Latitude = 30.0131m, Longitude = 31.7258m, ImageUrl = "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=800", CreatedAt = new DateTime(2024, 1, 1) },

     // 3. 6th of October City
     new Location { Id = 3, City = "6th October City", Regoin = "West Somid", Street = "26th of July Corridor", Latitude = 29.9737m, Longitude = 30.9510m, ImageUrl = "https://prod-images.nawy.com/processed/area/image/1/high.webp", CreatedAt = new DateTime(2024, 1, 1) },

     // 4. Mostakbal City
     new Location { Id = 4, City = "Mostakbal City", Regoin = "Phase 1", Street = "Suez Road", Latitude = 30.1290m, Longitude = 31.6030m, ImageUrl = "https://prod-images.nawy.com/processed/area/image/10/high.webp", CreatedAt = new DateTime(2024, 1, 1) },

    // 5. ELgouna 
    new Location
    { Id = 5, City = "El Gouna", Regoin = "Abu Tig Marina", Street = "Marina Way", Latitude = 27.3942m, Longitude = 33.6782m, ImageUrl = "https://prod-images.nawy.com/processed/area/image/5/high.webp", CreatedAt = new DateTime(2024, 1, 1) },

     // 6. El Shorouk
     new Location { Id = 6, City = "El Shorouk", Regoin = "District 3", Street = "El Horreya Axis", Latitude = 30.1197m, Longitude = 31.6046m, ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800", CreatedAt = new DateTime(2024, 1, 1) },

     // 7. Maadi
     new Location { Id = 7, City = "Maadi", Regoin = "Degla", Street = "Road 233", Latitude = 29.9599m, Longitude = 31.2676m, ImageUrl = "https://cairogossip.com/app/uploads/2020/02/caf268d7b5e3978ce944d44b6a144653.jpg", CreatedAt = new DateTime(2024, 1, 1) },

     // 8. Ain Sokhna
     new Location { Id = 8, City = "Ain Sokhna", Regoin = "Galala", Street = "Zaafarana Road", Latitude = 29.5768m, Longitude = 32.3385m, ImageUrl = "https://www.etbtoursegypt.com/storage/1421/Ain-El-Sokhna-Travel-Guide.jpg", CreatedAt = new DateTime(2024, 1, 1) }
 );

            // Seed Amenities (using only Interior and Exterior categories)
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "WiFi",
                    Icon = "wifi",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 2,
                    Name = "Air Conditioning",
                    Icon = "snowflake",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 3,
                    Name = "Parking",
                    Icon = "car",
                    Category = AminityCategory.Exterior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 4,
                    Name = "Swimming Pool",
                    Icon = "water",
                    Category = AminityCategory.Exterior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 5,
                    Name = "Gym",
                    Icon = "dumbbell",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 6,
                    Name = "Security",
                    Icon = "shield",
                    Category = AminityCategory.Exterior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 7,
                    Name = "Elevator",
                    Icon = "elevator",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 8,
                    Name = "Garden",
                    Icon = "tree",
                    Category = AminityCategory.Exterior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 9,
                    Name = "Balcony",
                    Icon = "balcony",
                    Category = AminityCategory.Exterior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 10,
                    Name = "Central Heating",
                    Icon = "fire",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 11,
                    Name = "Kitchen Appliances",
                    Icon = "kitchen",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                },
                new Amenity
                {
                    Id = 12,
                    Name = "Furnished",
                    Icon = "couch",
                    Category = AminityCategory.Interior,
                    CreatedAt = new DateTime(2024, 1, 5, 8, 0, 0)
                }
            );

            // Seed Properties
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Title = "Luxury Apartment in Nasr City",
                    Description = "Spacious 3-bedroom apartment with modern amenities and stunning city views. Perfect for families looking for comfort and convenience in the heart of Nasr City.",
                    Price = 15000m,
                    BedRooms = 3,
                    BathRooms = 2,
                    Area = 150m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Apartment,
                    UserId = 1,
                    LocationId = 1,
                    CreatedAt = new DateTime(2024, 1, 20, 10, 0, 0)
                },
                new Property
                {
                    Id = 2,
                    Title = "Beachfront Villa in Alexandria",
                    Description = "Beautiful villa with private beach access, pool, and garden. Ideal for vacation rentals or permanent residence. Enjoy the Mediterranean lifestyle.",
                    Price = 50000m,
                    BedRooms = 5,
                    BathRooms = 4,
                    Area = 350m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Villa,
                    UserId = 3,
                    LocationId = 2,
                    CreatedAt = new DateTime(2024, 1, 25, 11, 30, 0)
                },
                new Property
                {
                    Id = 3,
                    Title = "Modern Studio in Zamalek",
                    Description = "Cozy studio apartment in the heart of Zamalek, close to cafes, restaurants, and shops. Perfect for young professionals.",
                    Price = 8000m,
                    BedRooms = 1,
                    BathRooms = 1,
                    Area = 60m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Studio,
                    UserId = 1,
                    LocationId = 3,
                    CreatedAt = new DateTime(2024, 2, 1, 9, 0, 0)
                },
                new Property
                {
                    Id = 4,
                    Title = "Family Townhouse in 6th October",
                    Description = "Spacious family townhouse with garden, 4 bedrooms, and modern kitchen. Located in a quiet compound with 24/7 security.",
                    Price = 25000m,
                    BedRooms = 4,
                    BathRooms = 3,
                    Area = 280m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Townhouse,
                    UserId = 5,
                    LocationId = 4,
                    CreatedAt = new DateTime(2024, 2, 10, 14, 15, 0)
                },
                new Property
                {
                    Id = 5,
                    Title = "Penthouse Apartment in New Cairo",
                    Description = "Luxurious penthouse with panoramic views, private terrace, and premium finishes. Features include smart home system and private parking.",
                    Price = 35000m,
                    BedRooms = 4,
                    BathRooms = 3,
                    Area = 250m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Apartment,
                    UserId = 3,
                    LocationId = 5,
                    CreatedAt = new DateTime(2024, 2, 15, 12, 45, 0)
                },
                new Property
                {
                    Id = 6,
                    Title = "Affordable Apartment in Nasr City",
                    Description = "Well-maintained 2-bedroom apartment, perfect for small families or couples. Close to schools and shopping centers.",
                    Price = 10000m,
                    BedRooms = 2,
                    BathRooms = 1,
                    Area = 100m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Apartment,
                    UserId = 5,
                    LocationId = 1,
                    CreatedAt = new DateTime(2024, 3, 1, 10, 30, 0)
                },
                new Property
                {
                    Id = 7,
                    Title = "Commercial Office Space in Downtown Cairo",
                    Description = "Prime office location in downtown Cairo with modern facilities and easy access to public transportation.",
                    Price = 40000m,
                    BedRooms = 0,
                    BathRooms = 2,
                    Area = 200m,
                    ListingType = PropertyListingType.Rent,
                    Type = PropertyType.Office,
                    UserId = 1,
                    LocationId = 8,
                    CreatedAt = new DateTime(2024, 2, 20, 11, 0, 0)
                },
                new Property
                {
                    Id = 8,
                    Title = "Medical Clinic for Sale in Nasr City",
                    Description = "Fully equipped medical clinic with modern equipment and established patient base. Great investment opportunity.",
                    Price = 2500000m,
                    BedRooms = 0,
                    BathRooms = 3,
                    Area = 180m,
                    ListingType = PropertyListingType.Sale,
                    Type = PropertyType.Clinic,
                    UserId = 3,
                    LocationId = 7,
                    CreatedAt = new DateTime(2024, 2, 25, 13, 30, 0)
                }
            );

            // Seed Property Images
            modelBuilder.Entity<PropertyImages>().HasData(
                // Property 1 Images
                new PropertyImages { Id = 1, ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=800", PropertyId = 1, CreatedAt = new DateTime(2024, 1, 20, 10, 0, 0) },
                new PropertyImages { Id = 2, ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800", PropertyId = 1, CreatedAt = new DateTime(2024, 1, 20, 10, 0, 0) },
                new PropertyImages { Id = 3, ImageUrl = "https://images.unsplash.com/photo-1493809842364-78817add7ffb?w=800", PropertyId = 1, CreatedAt = new DateTime(2024, 1, 20, 10, 0, 0) },

                // Property 2 Images
                new PropertyImages { Id = 4, ImageUrl = "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800", PropertyId = 2, CreatedAt = new DateTime(2024, 1, 25, 11, 30, 0) },
                new PropertyImages { Id = 5, ImageUrl = "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800", PropertyId = 2, CreatedAt = new DateTime(2024, 1, 25, 11, 30, 0) },
                new PropertyImages { Id = 6, ImageUrl = "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800", PropertyId = 2, CreatedAt = new DateTime(2024, 1, 25, 11, 30, 0) },
                new PropertyImages { Id = 7, ImageUrl = "https://images.unsplash.com/photo-1600047509807-ba8f99d2cdde?w=800", PropertyId = 2, CreatedAt = new DateTime(2024, 1, 25, 11, 30, 0) },

                // Property 3 Images
                new PropertyImages { Id = 8, ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=800", PropertyId = 3, CreatedAt = new DateTime(2024, 2, 1, 9, 0, 0) },
                new PropertyImages { Id = 9, ImageUrl = "https://images.unsplash.com/photo-1560185127-6ed189bf02f4?w=800", PropertyId = 3, CreatedAt = new DateTime(2024, 2, 1, 9, 0, 0) },

                // Property 4 Images
                new PropertyImages { Id = 10, ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800", PropertyId = 4, CreatedAt = new DateTime(2024, 2, 10, 14, 15, 0) },
                new PropertyImages { Id = 11, ImageUrl = "https://images.unsplash.com/photo-1600566753190-17f0baa2a6c3?w=800", PropertyId = 4, CreatedAt = new DateTime(2024, 2, 10, 14, 15, 0) },
                new PropertyImages { Id = 12, ImageUrl = "https://images.unsplash.com/photo-1600573472591-ee6b68d14c68?w=800", PropertyId = 4, CreatedAt = new DateTime(2024, 2, 10, 14, 15, 0) },

                // Property 5 Images
                new PropertyImages { Id = 13, ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800", PropertyId = 5, CreatedAt = new DateTime(2024, 2, 15, 12, 45, 0) },
                new PropertyImages { Id = 14, ImageUrl = "https://images.unsplash.com/photo-1600210492486-724fe5c67fb0?w=800", PropertyId = 5, CreatedAt = new DateTime(2024, 2, 15, 12, 45, 0) },
                new PropertyImages { Id = 15, ImageUrl = "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800", PropertyId = 5, CreatedAt = new DateTime(2024, 2, 15, 12, 45, 0) },

                // Property 6 Images
                new PropertyImages { Id = 16, ImageUrl = "https://images.unsplash.com/photo-1502672023488-70e25813eb80?w=800", PropertyId = 6, CreatedAt = new DateTime(2024, 3, 1, 10, 30, 0) },
                new PropertyImages { Id = 17, ImageUrl = "https://images.unsplash.com/photo-1560185009-5bf9f2849488?w=800", PropertyId = 6, CreatedAt = new DateTime(2024, 3, 1, 10, 30, 0) },

                // Property 7 Images
                new PropertyImages { Id = 18, ImageUrl = "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800", PropertyId = 7, CreatedAt = new DateTime(2024, 2, 20, 11, 0, 0) },
                new PropertyImages { Id = 19, ImageUrl = "https://images.unsplash.com/photo-1497366811353-6870744d04b2?w=800", PropertyId = 7, CreatedAt = new DateTime(2024, 2, 20, 11, 0, 0) },

                // Property 8 Images
                new PropertyImages { Id = 20, ImageUrl = "https://images.unsplash.com/photo-1519494026892-80bbd2d6fd0d?w=800", PropertyId = 8, CreatedAt = new DateTime(2024, 2, 25, 13, 30, 0) },
                new PropertyImages { Id = 21, ImageUrl = "https://images.unsplash.com/photo-1551076805-e1869033e561?w=800", PropertyId = 8, CreatedAt = new DateTime(2024, 2, 25, 13, 30, 0) }
            );

            // Seed Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    Rating = 9,
                    Comment = "Amazing apartment! Very spacious and clean. The location is perfect and the landlord is very responsive.",
                    PropertyId = 1,
                    UserId = 2,
                    CreatedAt = new DateTime(2024, 2, 5, 15, 30, 0)
                },
                new Review
                {
                    Id = 2,
                    Rating = 10,
                    Comment = "Dream villa! The beach access is incredible and the kids love the pool. Highly recommended!",
                    PropertyId = 2,
                    UserId = 4,
                    CreatedAt = new DateTime(2024, 2, 20, 11, 0, 0)
                },
                new Review
                {
                    Id = 3,
                    Rating = 8,
                    Comment = "Nice studio in a great location. Perfect for my needs as a single professional.",
                    PropertyId = 3,
                    UserId = 2,
                    CreatedAt = new DateTime(2024, 2, 25, 14, 20, 0)
                },
                new Review
                {
                    Id = 4,
                    Rating = 9,
                    Comment = "Excellent family townhouse. The compound is safe and the neighbors are friendly. Kids love playing in the garden.",
                    PropertyId = 4,
                    UserId = 2,
                    CreatedAt = new DateTime(2024, 3, 5, 16, 45, 0)
                },
                new Review
                {
                    Id = 5,
                    Rating = 7,
                    Comment = "Good apartment for the price. Could use some updates but overall a solid choice.",
                    PropertyId = 6,
                    UserId = 4,
                    CreatedAt = new DateTime(2024, 3, 15, 13, 10, 0)
                }
            );

            // Seed Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    Check_In = new DateTime(2024, 3, 1),
                    Check_Out = new DateTime(2024, 3, 31),
                    Status = BookingStatus.Approved,
                    TotalPrice = 15000m,
                    PropertyID = 1,
                    UserID = 2,
                    CreatedAt = new DateTime(2024, 2, 15, 10, 0, 0)
                },
                new Booking
                {
                    Id = 2,
                    Check_In = new DateTime(2024, 4, 1),
                    Check_Out = new DateTime(2024, 4, 15),
                    Status = BookingStatus.Approved,
                    TotalPrice = 25000m,
                    PropertyID = 2,
                    UserID = 4,
                    CreatedAt = new DateTime(2024, 3, 10, 14, 30, 0)
                },
                new Booking
                {
                    Id = 3,
                    Check_In = new DateTime(2024, 3, 15),
                    Check_Out = new DateTime(2024, 4, 15),
                    Status = BookingStatus.Pending,
                    TotalPrice = 8000m,
                    PropertyID = 3,
                    UserID = 2,
                    CreatedAt = new DateTime(2024, 3, 5, 9, 15, 0)
                },
                new Booking
                {
                    Id = 4,
                    Check_In = new DateTime(2024, 2, 20),
                    Check_Out = new DateTime(2024, 2, 25),
                    Status = BookingStatus.Rejected,
                    TotalPrice = 8000m,
                    PropertyID = 5,
                    UserID = 4,
                    CreatedAt = new DateTime(2024, 2, 10, 11, 30, 0)
                }
            );

            // Seed Favourites
            modelBuilder.Entity<Favourite>().HasData(
                new Favourite
                {
                    Id = 1,
                    PropertyId = 1,
                    UserId = 2,
                    CreatedAt = new DateTime(2024, 1, 25, 12, 0, 0)
                },
                new Favourite
                {
                    Id = 2,
                    PropertyId = 2,
                    UserId = 2,
                    CreatedAt = new DateTime(2024, 1, 28, 15, 30, 0)
                },
                new Favourite
                {
                    Id = 3,
                    PropertyId = 5,
                    UserId = 4,
                    CreatedAt = new DateTime(2024, 2, 20, 10, 45, 0)
                },
                new Favourite
                {
                    Id = 4,
                    PropertyId = 3,
                    UserId = 4,
                    CreatedAt = new DateTime(2024, 2, 25, 14, 20, 0)
                },
                new Favourite
                {
                    Id = 5,
                    PropertyId = 7,
                    UserId = 2,
                    CreatedAt = new DateTime(2024, 2, 28, 9, 15, 0)
                }
            );

            // Seed Inquiries
            modelBuilder.Entity<Inquiry>().HasData(
                new Inquiry
                {
                    Id = 1,
                    Message = "Is the apartment still available for rent starting next month? Also, are pets allowed?",
                    UserId = 2,
                    PropertyId = 1,
                    CreatedAt = new DateTime(2024, 2, 1, 10, 30, 0)
                },
                new Inquiry
                {
                    Id = 2,
                    Message = "I'm interested in renting the villa for the summer. Can we schedule a viewing?",
                    UserId = 4,
                    PropertyId = 2,
                    CreatedAt = new DateTime(2024, 2, 10, 14, 15, 0)
                },
                new Inquiry
                {
                    Id = 3,
                    Message = "Does the rent include utilities? Also, is there parking available?",
                    UserId = 2,
                    PropertyId = 3,
                    CreatedAt = new DateTime(2024, 2, 15, 11, 45, 0)
                },
                new Inquiry
                {
                    Id = 4,
                    Message = "What is the minimum lease period for the office space?",
                    UserId = 4,
                    PropertyId = 7,
                    CreatedAt = new DateTime(2024, 2, 22, 10, 0, 0)
                }
            );

            modelBuilder.Entity<Offer>().HasData(
                 new Offer
                 {
                     Id = 1,
                     Title = "Spring Special - 10% Off",
                     CreatedAt = new DateTime(2024, 3, 1),
                     EndDate = new DateTime(2024, 3, 31),
                     DiscountPercentage = 10m,
                     PropertyId = 1,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 2,
                     Title = "Ramadan Mubarak Deal",
                     CreatedAt = new DateTime(2024, 3, 1),
                     EndDate = new DateTime(2024, 3, 31),
                     DiscountPercentage = 10m,
                     PropertyId = 2,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 3,
                     Title = "Spring Special - 10% Off",
                     CreatedAt = new DateTime(2024, 3, 1),
                     EndDate = new DateTime(2024, 3, 31),
                     DiscountPercentage = 10m,
                     PropertyId = 3,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 4,
                     Title = "Summer Early Bird",
                     CreatedAt = new DateTime(2024, 3, 1),
                     EndDate = new DateTime(2024, 3, 31),
                     DiscountPercentage = 10m,
                     PropertyId = 4,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 5,
                     Title = "Ramadan Offer",
                     CreatedAt = new DateTime(2024, 3, 10),
                     EndDate = new DateTime(2024, 4, 10),
                     DiscountPercentage = 15m,
                     PropertyId = 5,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 6,
                     Title = "Summer Early Bird",
                     CreatedAt = new DateTime(2024, 5, 1),
                     EndDate = new DateTime(2024, 6, 1),
                     DiscountPercentage = 5m,
                     PropertyId = 7,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 7,
                     Title = "Eid Sale - Villas",
                     CreatedAt = new DateTime(2024, 4, 8),
                     EndDate = new DateTime(2024, 4, 15),
                     DiscountPercentage = 20m,
                     PropertyId = 6,
                     IsActive = true
                 },
                 new Offer
                 {
                     Id = 8,
                     Title = "Office Lease Discount",
                     CreatedAt = new DateTime(2024, 2, 1),
                     EndDate = new DateTime(2024, 4, 1),
                     DiscountPercentage = 12m,
                     PropertyId = 5,
                     IsActive = false // Expired/Inactive
                 },
                 new Offer
                 {
                     Id = 9,
                     Title = "Family Home Promo",
                     CreatedAt = new DateTime(2024, 2, 1),
                     EndDate = new DateTime(2024, 4, 1),
                     DiscountPercentage = 12m,
                     PropertyId = 8,
                     IsActive = false // Expired/Inactive
                 }
             );
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<PropertyImages> PropertyImages { get; set; }
        public DbSet<Amenity> amenities { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Favourite> favourites { get; set; }
        public DbSet<Review> reviews { get; set; }
    }
}