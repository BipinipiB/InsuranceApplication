

using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insurance.DataAccess.Data
{
    /*!!!!NOTE!!!!: ApplicationDbContext class serves as a bridge between your application and the database */
    //it defines how your application interacts with database using Entity Framework
    //it manages query saving and other data operations

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // by inheriting "IdentityDbContext<ApplicationUser>, you are telling EF core to include the default Identity Schemas like
        //like.. tables for users,roles,claims etc in your database
        //This ensures that user and role management work seamlessly with your application's database context


        //constructor accpets options for configuring the database context and passes them to the base class constructor
        //This allows EF core to set up the context based on the configuration provided (e.g. connection string, database provider)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //New Db Set for Policy
        public DbSet<Policy> Policies { get; set; }

        //Db Set for policyType
        public DbSet<PolicyType> PolicyType { get; set; }

        public DbSet<QuestionTypeEntity> QuestionTypeEntities { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading actions

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.UpdatedBy)
                .WithMany()
                .HasForeignKey(p => p.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading actions

            modelBuilder.Entity<Policy>()
            .HasOne(p => p.PolicyType)
            .WithMany()
            .HasForeignKey(p => p.PolicyTypeId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete



            // Seed data for PolicyTypes
            modelBuilder.Entity<PolicyType>().HasData(
                new PolicyType { Id = 1, Name = "Individual Health Insurance", IsActive = true },
                new PolicyType { Id = 2, Name = "Family Health Insurance", IsActive = true },
                new PolicyType { Id = 3, Name = "Group Health Insurance", IsActive = true }
            );

            //Seet data for QuestionTypeEntities
            modelBuilder.Entity<QuestionTypeEntity>().HasData(
           new QuestionTypeEntity { Id = 1, Name = "Multiple Choice" },
           new QuestionTypeEntity { Id = 2, Name = "True/False" },
           new QuestionTypeEntity { Id = 3, Name = "Text" }
            );

            //Seet data for Question
           modelBuilder.Entity<Question>().HasData(
           new Question { Id = 1, QuestionLabel = "What Kind o Procedure are you making a claim for?",QuestionTypeId=3, Step="MakeAClaim", IsActive=true },
           new Question { Id = 2, QuestionLabel = "Do you have other insurance policy you could claim against?", QuestionTypeId = 4, Step = "MakeAClaim", IsActive = true },
           new Question { Id = 3, QuestionLabel = "ClaimAmount", QuestionTypeId = 3, Step = "PaymentInfo",IsActive = true },
           new Question { Id = 4, QuestionLabel = "Enter Credit Card Number", QuestionTypeId = 3, Step = "PaymentInfo", IsActive = true },
           new Question { Id = 5, QuestionLabel = "ExpiryDate", QuestionTypeId = 3, Step = "Expiry Date", IsActive = true },
           new Question { Id = 6, QuestionLabel = "ExpiryDate", QuestionTypeId = 3, Step = "CCV", IsActive = true }
            );

        }


        /*
             //max lenght is 1024 chars for question column
        [StringLength(1024)]
        public string QuestionLabel { get; set; }

        //QuestionTypeId Column is a Foreign Key
        [Required, NotNull]
        public int? QuestionTypeId { get; set; } 

        public QuestionTypeEntity? QuestionType { get; set; }

        public bool IsActive { get; set; }
         
         */


    }
}
