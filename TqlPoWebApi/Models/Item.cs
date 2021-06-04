using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TqlPoWebApi.Models
{
    public class Item
    {
        //PK
       public int ID { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [ Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; } = 0;

        public Boolean Active { get; set; } = true;

        //default constructor
        public Item() { }
    }
}
