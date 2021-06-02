using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TqlPoWebApi.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required, StringLength(30)]
        public string Login { get; set; }
        [Required, StringLength(30)]
        public string Password { get; set; }
        [Required, StringLength(30)]
        public string Firstname { get; set; }
        [Required, StringLength(30)]
        public string Lastname { get; set; }
        [Required, StringLength(30)]
        public bool IsManager { get; set; }
        

        //default cotr constructor
        public Employee()
        {

        }
    }   
}
