namespace Application.UseCases.Customers.Queries.GetCustomersWithPagination
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
    }
}