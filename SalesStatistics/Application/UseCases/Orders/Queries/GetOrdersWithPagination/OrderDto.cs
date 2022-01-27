using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.UseCases.Orders.Queries.GetOrdersWithPagination
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string CustomerFullName { get; set; }

        public string ManagerLastName { get; set; }

        public string ProductName { get; set; }
    }
}