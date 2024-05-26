using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Diagnostics;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductDbContext productObj;
        private readonly IWebHostEnvironment environment;

        public HomeController(ProductDbContext ProductObj, IWebHostEnvironment environment)
        {
            this.productObj = ProductObj;
            this.environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var data = await productObj.ProductTable.ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ForImage obj)
        {
            string filename = "";
            if (obj.Image != null)
            {
                string Folder = Path.Combine(environment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + obj.Image.FileName;
                string Filepath = Path.Combine(Folder, filename);
                await obj.Image.CopyToAsync(new FileStream(Filepath, FileMode.Create));


                Product p = new Product()
                {
                    Name = obj.Name,
                    Brand = obj.Brand,
                    Category = obj.Category,
                    Price = obj.Price,
                    Image = filename
                };
                productObj.ProductTable.Add(p);
                productObj.SaveChanges();
                return RedirectToAction("Index"); }
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await productObj.ProductTable.FirstOrDefaultAsync(x => x.Id == id);
            productObj.ProductTable.Remove(data);
            productObj.SaveChanges();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await productObj.ProductTable.FindAsync(id);
            return View(data);
        }

        [HttpPost]
      public async Task<IActionResult> Edit( Product obj)
        {
            var data = await productObj.ProductTable.FindAsync(obj.Id);
            if (data==null)
            {
                throw new Exception();
            }
            if (ModelState.IsValid)
            {
                data.Name = obj.Name;
                data.Brand = obj.Brand;
                data.Category = obj.Category;
                data.Price = obj.Price;
              productObj.ProductTable.Update(data);
               await productObj.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View();
        }
            
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
