using E_Commerce.Models;
using E_Commerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var vatPerctange = 0.13m;
            var data = await productObj.ProductTable
                .Select(x => new ProductListResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Brand = x.Brand,
                    BrandCategory = $"{x.Brand}/{x.Category}",
                    Category = x.Category,
                    Image = x.Image,
                    PriceWithVat = Convert.ToDecimal(x.Price) * vatPerctange
                    Price = x.Price
                })
                .ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel requestModel)
        {
            string filename = "";
            if (requestModel.Image != null)
            {
                string Folder = Path.Combine(environment.WebRootPath, "Images");
                filename = $"NewFolder/{Guid.NewGuid()}_{requestModel.Image.FileName}";
                string Filepath = Path.Combine(Folder, filename);
                await requestModel.Image.CopyToAsync(new FileStream(Filepath, FileMode.Create));

                Product productToAdd = new Product()
                {
                    Name = requestModel.Name,
                    Brand = requestModel.Brand,
                    Category = requestModel.Category,
                    Price = requestModel.Price,
                    Image = filename
                };
                productObj.ProductTable.Add(productToAdd);
                productObj.SaveChanges();
                return RedirectToAction("Index");
            }
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
            var data = await productObj.ProductTable
                .Select(x => new ProductViewModel
                {
                    Brand = x.Brand,
                    Category = x.Category,
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).FirstOrDefaultAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel requestModel)
        {
            var data = await productObj.ProductTable.FindAsync(requestModel.Id);
            if (data == null)
            {
                throw new Exception();
            }
            if (ModelState.IsValid)
            {
                data.Name = requestModel.Name;
                data.Brand = requestModel.Brand;
                data.Category = requestModel.Category;
                data.Price = requestModel.Price;
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
