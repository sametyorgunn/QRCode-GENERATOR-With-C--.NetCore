using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCode.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QRCoder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string text)
        {
            //qrtext txt = new qrtext();
            //text = txt.qrtxt.ToString();
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10, Color.Orange, Color.White, true);
            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                var bitmapBytes = stream.ToArray();
                return File(bitmapBytes, "image/jpeg");
            }
        }
        public IActionResult QrCodeTextGonder()
        {
            return View();
        }
        [HttpPost]
        public IActionResult QrCodeTextGonder(QRModel QR)
        {
            var text = QR.qrkodtexti.ToString();
            return Index(text);
            
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
