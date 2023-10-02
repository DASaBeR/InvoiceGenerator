namespace InvoiceGenerator.Models
{
	public class Invoice
	{
		public string CustomerName { get; set; }
		public decimal Amount { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
