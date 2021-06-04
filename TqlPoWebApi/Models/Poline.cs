using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TqlPoWebApi.Models
{
    public class Poline
    {
        //PK
        public int ID { get; set; }

        //items or line attached to a PO
        public int Quantity { get; set; } = 1;

        public int MyProperty { get; set; }


        //two FKs --- one pointing to PO Id, virtual to po is virtual, it is not a column cannot be null
        public virtual  PO Po { get; set; }
        //FK column points to class PO and PK ID...caps do not matter here
        public int POId { get; set; }

        //2nd FK has a price, item id attached to item instance, requested on line, has to point to a po and an item
        //Item is class, 2nd item is just a variable name that is called to
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
        


}
}
