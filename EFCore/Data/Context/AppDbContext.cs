using EFCore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Api.Data.Entities.Enums;

namespace Web.Api.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }

        //[NotMapped]
        // public DbSet<Customer> Customers { get; set; }

        public DbSet<Computer> Computers { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        var connectionString = configuration.GetConnectionString("localMysql");
        //        optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
        //    }
        //    //base.OnConfiguring(optionsBuilder);
        //}
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Override default AspNet Identity table names
        //    builder.Entity<AppUser>(entity => { entity.ToTable(name: "Users"); });
        //    builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
        //    builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
        //    builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
        //    builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
        //    builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
        //    builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region ApplicationUser
            #region Properties

            #endregion

            #region Schema
            builder.Entity<AppUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            #endregion

            #endregion

            #region Customer
            //#region Properties
            //builder.Entity<Customer>()
            //    .Property(c => c.Name)
            //    .IsRequired();
            //builder.Entity<Customer>()
            //    .Property(c => c.Email)
            //    .IsRequired();
            //builder.Entity<Customer>()
            //    .Property(c => c.Password)
            //    .IsRequired();
            //builder.Entity<Customer>()
            //    .Property(c => c.PhoneNumber)
            //    .IsRequired();
            //builder.Entity<Customer>()
            //    .Property(c => c.Facebook)
            //    .IsRequired()
            //    .HasDefaultValue("807826532671834");
            //builder.Entity<Customer>()
            //        .Property(c => c.Gender)
            //        .IsRequired()
            //        .HasDefaultValue(Gender.UnKnown);
            //builder.Entity<Customer>()
            //        .Property(c => c.Country)
            //        .IsRequired()
            //        .HasDefaultValue("Egypt");
            //builder.Entity<Customer>()
            //        .Property(c => c.Admin)
            //        .IsRequired()
            //        .HasDefaultValue("PC");

            ////// رسائل الخطأ
            ////builder.Entity<Customer>()
            ////        .Property(c => c.Serial)
            ////        .HasAnnotation("MaxLength", 150) // يمكن تحديد الحد الأقصى لطول السلسلة
            ////        .HasAnnotation("MinLength", 1); // يمكن تحديد الحد الأدنى لطول السلسلة

            //#endregion
            //#region indexer
            //builder.Entity<Customer>()
            //       .HasIndex(c => new { c.Name });
            //builder.Entity<Customer>()
            //       .HasIndex(c => new { c.Email }).IsUnique();
            //#endregion
            //#region Relations
            //// Relation Customer with Subscription
            //builder.Entity<Customer>()
            //    .HasMany(c => c.Subscriptions) // يحدد أن العميل لديه العديد من الاشتراكات
            //    .WithOne(s => s.Customer) // يحدد أن الاشتراك ينتمي إلى عميل واحد
            //    .HasForeignKey(s => s.CustomerId) // يحدد المفتاح الخارجي في جدول الاشتراكات الذي يرتبط به معرف العميل
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Cascade); // يحدد سلوك الحذف عند حذف العميل
            //#endregion

            #endregion

            #region Customer.Subscription
            #region Properties
            builder.Entity<Subscription>()
                .Property(b => b.Period)
                .IsRequired()
                .HasDefaultValue(SubscriptionPeriod.Blocked);

            builder.Entity<Subscription>()
                .Property(b => b.StartOfSubscription)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToLocalTime());

            builder.Entity<Subscription>()
                .Property(b => b.EndOfSubscription)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToShortTimeString());

            builder.Entity<Subscription>()
                .Property(c => c.Points)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Entity<Subscription>()
                .Property(b => b.Price)
                .HasColumnType("decimal(6,2)")
                .HasDefaultValue(0);

            builder.Entity<Subscription>()
                .Property(b => b.Discount)
                .HasColumnType("decimal(6,2)")
                .HasDefaultValue(0);


            #endregion
            #region indexer
            builder.Entity<Subscription>()
                   .HasIndex(s => new { s.Period, s.Points });
            #endregion
            #region relations
            // relation Subscription with Computer
            builder.Entity<Subscription>()
                .HasOne(s => s.Computer);
            #endregion

            #endregion

            #region Subscription.Computer
            #region Properties
            builder.Entity<Computer>()
               .Property(c => c.Hwid)
               .IsRequired();
            builder.Entity<Computer>()
                .Property(c => c.Serial)
                .IsRequired();

            // رسائل الخطأ
            builder.Entity<Computer>()
                    .Property(c => c.Serial)
                    .HasAnnotation("MaxLength", 150) // يمكن تحديد الحد الأقصى لطول السلسلة
                    .HasAnnotation("MinLength", 1); // يمكن تحديد الحد الأدنى لطول السلسلة

            #endregion
            #region indexer
            builder.Entity<Computer>()
                   .HasIndex(c => new { c.Serial });
            builder.Entity<Computer>()
                   .HasIndex(c => new { c.Hwid }).IsUnique();
            #endregion
            #region relations
            // تعيين العلاقة بين العميل والاشتراكات
            builder.Entity<Computer>()
                .HasOne(s => s.Subscription); // يحدد سلوك الحذف عند حذف العميل
            #endregion
            #endregion



            #region SeedingData
            //builder.Entity<IdentityRole>()
            //    .HasData(new IdentityRole()
            //    {
            //        Name = BL.Constants.Roles.SuperAdmin,
            //        NormalizedName = BL.Constants.Roles.SuperAdmin.ToUpper()
            //    },
            //    new IdentityRole()
            //    {
            //        Name = BL.Constants.Roles.Admin,
            //        NormalizedName = BL.Constants.Roles.Admin.ToUpper()
            //    },
            //    new IdentityRole()
            //    {
            //        Name = BL.Constants.Roles.Customer,
            //        NormalizedName = BL.Constants.Roles.Customer.ToUpper()
            //    });

            #region Customer
            //builder.Entity<Customer>()
            //    .HasData(new Customer()
            //{
            //    Id = 1,
            //    Name = "مسعد",
            //    UserName = "elmagekmosaad3535",
            //    Email = "elmagekmosaad3535@gmail.com",
            //    Password = "string.Asd@123",
            //    PhoneNumber = "01004918459",
            //    Facebook = "100006758706866",
            //    Type = CustomerType.Person,
            //    Gender = Gender.Male,
            //    Country = "Egypt",
            //    Admin = "elmagek",
            //    Comments = "الحمدلله دائما وأبدا",
            //    //Subscriptions = null,
            //    });
            #endregion

            builder.Entity<Subscription>()
                .HasData(new Subscription()
                {
                    Id = 1,
                    Period = SubscriptionPeriod.Month,
                    StartOfSubscription = DateTime.Now,
                    EndOfSubscription = DateTime.Now,
                    Points = 999,
                    Discount = 100,
                    Price = 2000,
                    //ApplicationUserId = 1,
                    //ComputerId = 1,
                    //Computer = null,
                    //Customer = null,
                });

            builder.Entity<Computer>()
                .HasData(new Computer()
                {
                    Id = 1,
                    Hwid = "OFdOZ2w5Qm",
                    Serial = "mg",
                    //ApplicationUserId = 1,
                    SubscriptionId = 1,
                });
            #endregion



        }

    }

}
