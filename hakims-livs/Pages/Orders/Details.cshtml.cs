#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;
using hakims_livs.Controllers;
using Aspose.Pdf;

namespace hakims_livs.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public decimal OrderSum { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.Include(o => o.Customer).Include(o => o.OrderRows).ThenInclude(or => or.Product).FirstOrDefaultAsync(m => m.ID == id);

            foreach (var item in Order.OrderRows)
            {
                OrderSum += item.Price;
            }

            OrderSum = decimal.Round(OrderSum, 2);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        //public void CreatePdf(int id)
        //{
        //    PdfController.PickingListAsync(id);
        //}

        //public async Task<IActionResult> CreatePickingListOnPost(Order order)
        //{
        //    var pickingList = new Document();
        //    var pages = pickingList.Pages.Add();

        //    var table = new Table();
        //    table.ColumnWidths = "30, 100, 50, 30, 30, 30, 30, 40";
        //    table.Border = new BorderInfo(BorderSide.All, 1f, Color.FromRgb(System.Drawing.Color.Black));
        //    table.DefaultCellBorder = new BorderInfo(BorderSide.All, .1f, Color.FromRgb(System.Drawing.Color.Black));

        //    var headerRow = table.Rows.Add();
        //    headerRow.Cells.Add("Art.nr");
        //    headerRow.Cells.Add("Produktnamn");
        //    headerRow.Cells.Add("Märke");
        //    headerRow.Cells.Add("Antal");
        //    headerRow.Cells.Add("Volym");
        //    headerRow.Cells.Add("Enhet");
        //    headerRow.Cells.Add("Saldo");
        //    headerRow.Cells.Add("Plockad");
        //    foreach (var item in order.OrderRows)
        //    {
        //        var row = table.Rows.Add();
        //        row.Cells.Add($" {item.ID} ");
        //        row.Cells.Add($" {item.Product.Name} ");
        //        row.Cells.Add($" {item.Product.Brand} ");
        //        row.Cells.Add($" {item.Quantity} ");
        //        row.Cells.Add($" {item.Product.Volume} ");
        //        row.Cells.Add($" {item.Product.Unit} ");
        //        row.Cells.Add($" {item.Product.Stock} ");
        //        row.Cells.Add($" [  ] ");


        //        //page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"Art.nr: {item.ID} Produktnamn: {item.Product.Name}  Antal:{item.Quantity} Lagerstatus: {item.Product.Stock}"));

        //    }
        //    pages.Paragraphs.Add(table);

        //    //pages.Footer.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"Leveransadress"));
        //    //pages.Footer.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{order.Customer.FirstName} {order.Customer.LastName}"));
        //    //pages.Footer.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{order.Customer.Address.Street}"));
        //    //pages.Footer.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{order.Customer.Address.PostalCode} {order.Customer.Address.City}"));
        //    //pages.Footer.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{order.Customer.Email.ToLower()}"));
        //    //pages.Footer.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{order.Customer.FirstName} {order.Customer.LastName}"));

        //    pickingList.Save($"plocklista order {order.ID}.pdf");

        //    return Page();

        //}

        
    }
}
