using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First.Models
{
    [Table("Settings")]
    public class Setting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [StringLength(10)]
        public string SettingId { get; set; }

        [StringLength(50)]
        public string SettingName { get; set; }

        [StringLength(50)]
        public string SettingValue { get; set; }

    }
}