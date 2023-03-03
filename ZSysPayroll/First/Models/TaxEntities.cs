using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace First.Models
{
    public partial class TaxEntities : DbContext
    {
        public TaxEntities()
            : base("name=TaxEntities")
        {
        }

        public virtual DbSet<Emp> Emps { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Payslip> Payslips { get; set; }
        public virtual DbSet<TaxDeduction> TaxDeductions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emp>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Designation)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<Emp>()
                .Property(e => e.Education)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.PAN)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Aadhar)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Passport)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Grade)
                .IsUnicode(false);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.ResetPasswordCode)
                .IsUnicode(false);

            modelBuilder.Entity<Grade>()
                .Property(e => e.Grade_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Basic)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.HRA)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.DA)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Coveyance)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Bonus)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Deductions)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.PF)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Incometax)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Professiontax)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.SalaryAdv)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Others)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Total_sal)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.UAN)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.PFAccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.PaidDays)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.LOP)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Gross_Total)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Deduction_Total)
                .IsUnicode(false);
        }
    }
}
