using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;
using IronXL;
using System.Data;
using VisitorManagementSystems.Extentions;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using QRCoder;
using static QRCoder.QRCodeData;
using System.Drawing;

namespace VisitorManagementSystems.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IVisitorProvider VisitorProvider;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public ManagerController(ILogger<ManagerController> logger,
                                IVisitorProvider visitorProvider,
                                IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            WebHostEnvironment = webHostEnvironment;
            VisitorProvider = visitorProvider;
        }
        public async Task<IActionResult> Index()
        {
            var visitors = await VisitorProvider.GetAllVisitors();
            visitors.Select(v => { if (v.Department == null) { v.Department = "Other"; } return v; }).ToList();
            return View(visitors.ToList());
        }
        [HttpGet]
        public async Task<JsonResult> GetTodaysVisitors()
        {
            var visitors = await VisitorProvider.GetTodaysVisitors();
            return Json(new { data = visitors.ToList() });
        }
        [HttpGet]
        public async Task<JsonResult> GetAllVisitors()
        {
            var visitors = await VisitorProvider.GetAllVisitors();
            return Json(new { data = visitors.ToList() });
        }
        [HttpGet]
        public async Task<JsonResult> GetVisitorsByDates(DateTime fromDate,DateTime toDate)
        {
            var visitors = await VisitorProvider.GetAllVisitors();
            visitors = visitors.Where(vs => vs.Entry_Time.Date >= fromDate.Date && vs.Entry_Time.Date <= toDate.Date);
            return Json(new { data = visitors.ToList() });
        }
        [HttpGet]
        public ActionResult ViewAllVisitors()
        {
            return View();
        }

        public async Task<IActionResult> RegisterVisitor(IFormFile registerVisitor)
        {
            List<Visitor> visitors = new List<Visitor>();

            WorkBook workbook = WorkBook.Load(registerVisitor.OpenReadStream());
            WorkSheet sheet = workbook.DefaultWorkSheet;
            var csvFilereader = new DataTable();
            csvFilereader = sheet.ToDataTable(true);
            DateTime? nullDateTime = null;
            int count = 0;
            for (int i = 0; i < csvFilereader.Rows.Count; i++)
            {
              if(csvFilereader.Rows[i][0].ToString() != "")
                {
                    var visitor = new Visitor
                    {
                        Name = csvFilereader.Rows[i][0].ToString(),
                        Address = csvFilereader.Rows[i][1].ToString(),
                        Phone = csvFilereader.Rows[i][2].ToString(),
                        Purpose = csvFilereader.Rows[i][3].ToString(),
                        Entry_Time = DateTime.Parse(csvFilereader.Rows[i][4].ToString()),
                        Exit_Time = csvFilereader.Rows[i][5].ToString().Equals("") ? nullDateTime : DateTime.Parse(csvFilereader.Rows[i][5].ToString()),
                        Person_to_Meet = csvFilereader.Rows[i][6].ToString(),
                        Department = csvFilereader.Rows[i][7].ToString(),
                        Carried_Assets = csvFilereader.Rows[i][8].ToString(),
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now,
                        ModifiedBy = User.Identity.GetUserId(),
                        ModifiedOn = DateTime.Now
                    };
                    visitors.Add(visitor);
                    var savevisitor = await VisitorProvider.AddEditVisitor(visitor);
                    if (savevisitor.Id != 0)
                    {
                        count++;
                    }
                }
            }
            this.GenerateBarcode(visitors);
            return Ok(new { status = "success", count});
        }
        public void GenerateBarcode(List<Visitor> visitors)
        {
            try
            {
                var data = visitors.Select(v => new { v.Name, v.Phone, v.Purpose, v.Address, v.Carried_Assets, v.Department, v.Entry_Time, v.Person_to_Meet });
                var json = JsonSerializer.Serialize(data);
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCode = qrGenerator.CreateQrCode(json.ToString(), QRCodeGenerator.ECCLevel.L);

                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images/qrcode.png");
                // qrCode.SaveRawData(filePath, Compression.Uncompressed);
                Bitmap bitMap = new QRCode(qrCode).GetGraphic(30);
                using (MemoryStream ms = new MemoryStream())

                {

                    bitMap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                }

            }
            catch (Exception ex)
            {

            }
        }
        public async Task<IActionResult> GenerateQrBarcode(int id)
        {
            try
            {
                var visitor = await VisitorProvider.GetVisitorById(id);
                List<Visitor> visitors = new List<Visitor>();
                visitors.Add(visitor);
                var data = visitors.Select(v => new { v.Name, v.Phone, v.Purpose, v.Address, v.Carried_Assets, v.Department, v.Entry_Time, v.Person_to_Meet });
                var json = JsonSerializer.Serialize(data);
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCode = qrGenerator.CreateQrCode(json.ToString(), QRCodeGenerator.ECCLevel.M);

                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images/qrcode.png");
                // qrCode.SaveRawData(filePath, Compression.Uncompressed);
                Bitmap bitMap = new QRCode(qrCode).GetGraphic(3);
                using (MemoryStream ms = new MemoryStream())

                {
                    bitMap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
                return Ok(new { status = "qrgenerated" });

            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new { status = "Failed" });
            }
        }
    }
}
