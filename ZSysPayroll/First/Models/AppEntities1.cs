using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace First.Models
{
    public partial class AppEntities1 : DbContext
    {
        public AppEntities1() : base("name=AppEntities1")
        {
        }

        public virtual DbSet<Emp> Emps { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Payslip> Payslips { get; set; }
        public virtual DbSet<PayslipGradeEntry> PayslipGradeEntries { get; set; }

        public virtual DbSet<PayslipGradeHeader> PayslipGradeHeaders { get; set; }
        public virtual DbSet<Payslip1> Payslips1 { get; set; }
       // public virtual DbSet<TaxDeduction> TaxDeductions { get; set; }
        public virtual DbSet<TblTaxDeclForm> TblTaxDeclForm { get; set; }
        public virtual DbSet<InvestmentDeclaration> InvestmentDeclarations { get; set; }
        public virtual DbSet<InvestmentDeclarationMaster> InvestmentDeclarationMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emp>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.EmployeeId);
                //.IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Designation)
                .IsUnicode(false);

            modelBuilder.Entity<Emp>()
                .Property(e => e.Gender)
                .IsFixedLength();

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

            modelBuilder.Entity<Emp>()
                .Property(e => e.Grade)
                .IsUnicode(false);

            /*** SETTINGS ENTITY ***/

            modelBuilder.Entity<Setting>()
                .Property(e => e.SettingId)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.SettingValue)
                .IsUnicode(false);

            /*************************************/

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
                .Property(e => e.Gross_Total)
                .IsUnicode(false);

            modelBuilder.Entity<Payslip>()
                .Property(e => e.Deduction_Total)
                .IsUnicode(false);

            modelBuilder.Entity<InvestmentDeclarationMaster>()
            .Property(e => e.Declaration_master_id);
            //   .IsUnicode(false);

           modelBuilder.Entity<InvestmentDeclarationMaster>()
          .Property(e => e.Declaration_category);

            modelBuilder.Entity<InvestmentDeclarationMaster>()
         .Property(e => e.Declaration_type);

            modelBuilder.Entity<InvestmentDeclarationMaster>()
        .Property(e => e.Is_active);

            modelBuilder.Entity<InvestmentDeclarationMaster>()
       .Property(e => e.Sort_order);

            modelBuilder.Entity<InvestmentDeclaration>()
          .Property(e => e.Declaration_Id);

            modelBuilder.Entity<InvestmentDeclaration>()
         .Property(e => e.Declaration_master_id);

            modelBuilder.Entity<InvestmentDeclaration>()
         .Property(e => e.Declared_amt);

            modelBuilder.Entity<InvestmentDeclaration>()
         .Property(e => e.Proof_amount);

            modelBuilder.Entity<InvestmentDeclaration>()
         .Property(e => e.Proof_doc_path);

            modelBuilder.Entity<InvestmentDeclaration>()
         .Property(e => e.Proof_doc_name);




        }
    }
}
