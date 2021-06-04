using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TqlPoWebApi.Models
{
    public class PO
    {
        //PK
        public int ID { get; set; }

        [Required, StringLength(80)]
        public string Description { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } = "New";

  
        [Required, Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; } = 0;

        public bool Active { get; set; } = true;

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public static string StatusReview { get; internal set; }

        //default constructor
        public PO() { }
        

    
    }
}
