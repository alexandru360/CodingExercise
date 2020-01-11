using CodingExercise.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodingExercise.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<ProcessPayment> ProcessPayments { get; set; }
        public DbSet<PaymentState> PmtState { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProcessPayment>().ToTable("ProcessPayment");
            builder.Entity<ProcessPayment>().HasKey(p => p.Id);
            builder.Entity<ProcessPayment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ProcessPayment>().Property(p => p.CardHolder).IsRequired().HasMaxLength(250);
            builder.Entity<ProcessPayment>().Property(p => p.ExpirationDate).IsRequired();
            builder.Entity<ProcessPayment>().Property(p => p.SecurityCode).IsRequired().HasMaxLength(19);
            builder.Entity<ProcessPayment>().Property(p => p.Amount).IsRequired();
            builder.Entity<ProcessPayment>()
                .HasOne(p => p.PaymentState)
                .WithOne(o => o.ProcessPayment)
                .HasForeignKey<PaymentState>(f => f.ProcessPaymentId);

            builder.Entity<PaymentState>().ToTable("PaymentState");
            builder.Entity<PaymentState>().HasKey(p => p.Id);
            builder.Entity<PaymentState>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaymentState>().Property(p => p.PmntState).IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ProcState)Enum.Parse(typeof(ProcState), v));
        }
    }
}
