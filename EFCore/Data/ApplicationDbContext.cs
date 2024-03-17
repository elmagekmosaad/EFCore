using EFCore.Data.Enums;
using EFCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.MySQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Customer> Customers { get; set; }
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Customer
            #region Properties
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(c => c.MobileNumber)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(c => c.Facebook)
                .IsRequired()
                .HasDefaultValue("807826532671834");
            modelBuilder.Entity<Customer>()
                    .Property(c => c.Gender)
                    .IsRequired()
                    .HasDefaultValue(Gender.UnKnown);
            modelBuilder.Entity<Customer>()
                    .Property(c => c.Country)
                    .IsRequired()
                    .HasDefaultValue("Egypt");
            modelBuilder.Entity<Customer>()
                    .Property(c => c.Admin)
                    .IsRequired()
                    .HasDefaultValue("PC");

            //// رسائل الخطأ
            //modelBuilder.Entity<Customer>()
            //        .Property(c => c.Serial)
            //        .HasAnnotation("MaxLength", 150) // يمكن تحديد الحد الأقصى لطول السلسلة
            //        .HasAnnotation("MinLength", 1); // يمكن تحديد الحد الأدنى لطول السلسلة

            #endregion
            #region indexer
            modelBuilder.Entity<Customer>()
                   .HasIndex(c => new { c.Name });
            modelBuilder.Entity<Customer>()
                   .HasIndex(c => new { c.Email }).IsUnique();
            #endregion
            #region Relations
            // Relation Customer with Subscription
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Subscriptions) // يحدد أن العميل لديه العديد من الاشتراكات
                .WithOne(s => s.Customer) // يحدد أن الاشتراك ينتمي إلى عميل واحد
                .HasForeignKey(s => s.CustomerId) // يحدد المفتاح الخارجي في جدول الاشتراكات الذي يرتبط به معرف العميل
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade); // يحدد سلوك الحذف عند حذف العميل
            #endregion
            #endregion
           
            #region Customer.Subscription
            #region Properties
            modelBuilder.Entity<Subscription>()
                .Property(b => b.Period)
                .IsRequired()
                .HasDefaultValue(SubscriptionPeriod.Blocked);

            modelBuilder.Entity<Subscription>()
                .Property(b => b.StartOfSubscription)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToLocalTime());

            modelBuilder.Entity<Subscription>()
                .Property(b => b.EndOfSubscription)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToShortTimeString());

            modelBuilder.Entity<Subscription>()
                .Property(c => c.Points)
                .IsRequired()
                .HasDefaultValue(0);

            modelBuilder.Entity<Subscription>()
                .Property(b => b.Price)
                .HasColumnType("decimal(6,2)")
                .HasDefaultValue(0);

            modelBuilder.Entity<Subscription>()
                .Property(b => b.Discount)
                .HasColumnType("decimal(6,2)")
                .HasDefaultValue(0);


            #endregion
            #region indexer
            modelBuilder.Entity<Subscription>()
                   .HasIndex(s => new { s.Period, s.Points });
            #endregion
            #region relations
            // relation Subscription with Computer
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Computer);
            #endregion
            #endregion

            #region Subscription.Computer
            #region Properties
            modelBuilder.Entity<Computer>()
               .Property(c => c.Hwid)
               .IsRequired();
            modelBuilder.Entity<Computer>()
                .Property(c => c.Serial)
                .IsRequired();

            // رسائل الخطأ
            modelBuilder.Entity<Computer>()
                    .Property(c => c.Serial)
                    .HasAnnotation("MaxLength", 150) // يمكن تحديد الحد الأقصى لطول السلسلة
                    .HasAnnotation("MinLength", 1); // يمكن تحديد الحد الأدنى لطول السلسلة

            #endregion
            #region indexer
            modelBuilder.Entity<Computer>()
                   .HasIndex(c => new { c.Serial });
            modelBuilder.Entity<Computer>()
                   .HasIndex(c => new { c.Hwid }).IsUnique();
            #endregion
            #region relations
            // تعيين العلاقة بين العميل والاشتراكات
            modelBuilder.Entity<Computer>()
                .HasOne(s => s.Subscription); // يحدد سلوك الحذف عند حذف العميل
            #endregion
            #endregion
        }

    }
}
