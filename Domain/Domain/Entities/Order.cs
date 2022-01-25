using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual Product Product { get; set; }
    }
}