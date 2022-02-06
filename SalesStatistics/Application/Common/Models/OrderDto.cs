using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models
{
    public class OrderDto
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string CustomerFullName { get; set; }

        public string ManagerLastName { get; set; }

        public string ProductName { get; set; }
    }
}