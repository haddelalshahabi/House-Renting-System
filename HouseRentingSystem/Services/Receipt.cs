using DinkToPdf;
using DinkToPdf.Contracts;

namespace HouseRentingSystem.Services
{
    public class Receipt
    {
        private readonly IConverter _converter;

        public Receipt(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdfReceipt(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
                Objects = {
                new ObjectSettings() {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                }
            }
            };

            return _converter.Convert(doc);
        }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
