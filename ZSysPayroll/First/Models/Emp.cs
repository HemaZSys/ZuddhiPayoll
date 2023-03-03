namespace First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeDetails")]
    public partial class Emp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        public string EmployeeId { get; set; }

        public DateTime? DateofJoin { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string Qualification { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(50)]
        public string PAN { get; set; }

        [StringLength(50)]
        public string Aadhar { get; set; }

        [StringLength(50)]
        public string Passport { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        [StringLength(50)]
        public string Grade { get; set; }

        public string Availability { get; set; }

        public int? PId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
}
