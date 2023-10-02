using DinkToPdf;
using DinkToPdf.Contracts;
using InvoiceGenerator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Net;

namespace InvoiceGenerator.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class InvoicesController : ControllerBase
	{
		private readonly ILogger<InvoicesController> _logger;
		private readonly IConverter _converter;
		public InvoicesController(ILogger<InvoicesController> logger, IConverter converter)
		{
			_logger = logger;
			_converter = converter;
		}


		[HttpGet]
		public async Task<IActionResult> GenerateInvoice()
		{
			var invoice = new Invoice
			{
				CustomerName = "Mohsen Saberi",
				Amount = 150,
				CreatedAt = DateTime.UtcNow,
			};

			var htmlContent = $"<h1>Invoice</h1><p>Customer Name: {invoice.CustomerName}</p><p>Amount: ${invoice.Amount}</p>";

			var globalSettings = new GlobalSettings
			{
				PaperSize = PaperKind.A4,
				Orientation = Orientation.Portrait,
				Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
			};

			var objectSettings = new ObjectSettings
			{
				PagesCount = true,
				HtmlContent = htmlContent,
				WebSettings = { DefaultEncoding = "utf-8"/*, UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "styles.css")*/ },
			};

			var document = new HtmlToPdfDocument()
			{
				GlobalSettings = globalSettings,
				Objects = { objectSettings }
			};

			return File(_converter.Convert(document), "application/pdf", "invoice.pdf");
		}


	}
}
