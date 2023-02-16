using BusinesLayer.Concrete;
using DataLayer.Concrete;
using DataLayer.EntityFramawork;
using DomainLayer.Concrate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafliogliWeb.Controllers
{
    public class DefaultController : Controller
    {

        ImageCategoryManager imageCategoryManager = new ImageCategoryManager(new EfImageCategoryDal());
        AboutManager aboutManager = new AboutManager(new EfAboutDal());
        ContractManager contractManager = new ContractManager(new EfContactDal());
        [HttpGet]
        public IActionResult Index()
        {
            var imageCategoryList = imageCategoryManager.TGetlist();
            return View(imageCategoryList);
        }

        public IActionResult Images(int id)
        {
            using Context context = new Context();
            var imageList = context.Images.Include(x => x.ImagesCategory).Where(x => x.ImageCategoryID == x.ImagesCategory.ImageCategoryID).ToList();
            ViewBag.ImageCategory = context.ImageCategorys.Where(x => x.ImageCategoryID == id).Select(x => x.Title).FirstOrDefault();
            return View(imageList);
        }

        public IActionResult About()
        {
            var about = aboutManager.TGetlist();
            return View(about);
        }
        [HttpGet]
        public IActionResult Contract()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Contract contract)
        {
            contractManager.TAdd(contract);
            return RedirectToAction("Contract");

        }
    }
}
