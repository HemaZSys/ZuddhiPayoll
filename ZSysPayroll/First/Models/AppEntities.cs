using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace First.Models
{
    public partial class AppEntities : DbContext
    {
        public AppEntities()
            : base("name=AppEntities")
        {
        }

        public virtual DbSet<Emp> Emps { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Payslip> Payslips { get; set; }
        // public virtual DbSet<TaxDeduction> TaxDeductions { get; set; }  

        public virtual DbSet<InvestmentDeclarationMaster> InvestmentDeclarationMasters { get; set; }

        public virtual DbSet<InvestmentDeclaration> InvestmentDeclarations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emp>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Designation)
                .IsUnicode(false);
                        
            modelBuilder.Entity<Emp>()
                .Property(e => e.Qualification)
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
               .Property(e => e.Month);
               //.IsUnicode(false);
            
            
            modelBuilder.Entity<Payslip>()
              .Property(e => e.Gross_Total)
              .IsUnicode(false);

              modelBuilder.Entity<Payslip>()
              .Property(e => e.Deduction_Total)
              .IsUnicode(false);

        }
    }
}
