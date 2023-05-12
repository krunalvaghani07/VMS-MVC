using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Extentions;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;
using Rotativa.AspNetCore;

namespace VisitorManagementSystems.Controllers
{
    public class GatekeeperController : Controller
    {
        private readonly ILogger<GatekeeperController> _logger;
        private readonly IVisitorProvider VisitorProvider;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public GatekeeperController(ILogger<GatekeeperController> logger,
                                IVisitorProvider visitorProvider,
                                IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            VisitorProvider = visitorProvider;
            WebHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var visitors = await VisitorProvider.GetTodaysVisitors();
            return View(visitors.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddEditVisitor(Visitor visitor)
        {
            try
            {
                var _visitor = await VisitorProvider.AddEditVisitor(visitor);
                if (_visitor == null)
                {
                    return UnprocessableEntity("Eror In Adding/Editing Visitor");
                }
                return Ok(_visitor);
            }
            catch (Exception ex)
            {
                if (visitor.Id == 0)
                {
                    return UnprocessableEntity("Eror In Adding Visitor");
                }
                return UnprocessableEntity("Eror In Editing Visitor");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveVisitor(Visitor visitor, IFormFile capturedimage)
        {
            var imagename = this.UploadFile(visitor.Photo, capturedimage);
            visitor.CreatedBy = User.Identity.GetUserId();
            visitor.ModifiedBy = User.Identity.GetUserId();
            visitor.CreatedOn = DateTime.Today;
            visitor.ModifiedOn = DateTime.Today;
            visitor.Entry_Time = DateTime.Now;
            visitor.Photo = imagename;
            var savevisitor = await this.AddEditVisitor(visitor) as OkObjectResult;
            if (savevisitor.StatusCode == 200)
            {
                if (visitor.Id == 0)
                {
                    return Ok();
                }
                else
                {
                    return Ok(new { status = "success", action = "redirect", location = "/Gatekeeper" });
                }
            }
            return UnprocessableEntity("Eror occured while saving this visitor");
        }
        [HttpGet]
        public async Task<JsonResult> GetTodaysVisitors()
        {
            var visitors = await VisitorProvider.GetTodaysVisitors();
            return Json(new { data = visitors.ToList() });
        }
        [HttpGet]
        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            return await VisitorProvider.GetAllVisitors();
        }
        [HttpGet]
        public async Task<IActionResult> AddEditVisitor(int id)
        {
            Visitor visitor = new Visitor();
            if (id != 0)
            {
                visitor = await VisitorProvider.GetVisitorById(id);
            }
            return View(visitor);
        }
        public async Task<IActionResult> ExitVisitor(int id)
        {
            var visitor = await VisitorProvider.ExitVisitor(id, DateTime.Now);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            try
            {
                var visitor = await VisitorProvider.DeleteVisitor(id);
                if (visitor != null)
                {
                    return UnprocessableEntity("Visitor is Not Deleted");
                }
                return Ok("Visitor Deleted Successfully");
            }
            catch
            {
                return UnprocessableEntity("Eror In Delting This Visitor");
            }
        }

        private string UploadFile(string oldFileName, IFormFile image)
        {
            try
            {
                string uniqueFileName = null;

                if (image != null)
                {
                    string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    if (oldFileName != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(uploadsFolder, oldFileName)))
                        {
                            System.IO.File.Delete(Path.Combine(uploadsFolder, oldFileName));
                        }

                    }

                    return uniqueFileName;
                }
                else
                {
                    return oldFileName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> VisitorById(int id)
        {
            var visitor = await VisitorProvider.GetVisitorById(id);
            return PartialView("VisitorByIdPartial", visitor);
        }
        public async Task<IActionResult> PrintVisitorDetail(int id)
        {
            var visitor = await VisitorProvider.GetVisitorById(id);
            var report = new ViewAsPdf("~/Views/PrintVisitorDetail.cshtml", visitor);
            return report;
        }
    }
}
