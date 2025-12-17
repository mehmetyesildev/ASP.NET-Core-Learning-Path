using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
      public HomeController()
    {
        
    }
    public IActionResult Index(string searchString,string category)
    {
        var products = Repository.Products;// Syafaya ürün listesini index.cshtml dosyasına gönderiyoruz 
        if(!string.IsNullOrEmpty(searchString))
        { 
            ViewBag.SearchString =searchString;
            products=products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }
        if(!string.IsNullOrEmpty(category)&& category!="0")
        {
            products=products.Where(p=>p.CategoryId==int.Parse(category)).ToList();
        }
        // ViewBag.Categories= new SelectList(Repository.Categorys, "CategoryId","Name",category);

        var model =new ProductViewModel
        {
            Products=products,
            Categories= Repository.Categorys,
            selectedcCategory=category
        };
        return View(model);
    }
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categorys, "CategoryId", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
       var extension="";


        if (imageFile!=null)
        {
            extension = Path.GetExtension(imageFile.FileName);//abc.jpg de jpg alır
            var allowedExtension = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(extension))
            {
                ModelState.AddModelError("","Gecerli bir reşim şeçiniz");
            }
        }

        if(ModelState.IsValid)
        {
            if (imageFile !=null)
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                model.ProductId = Repository.Products.Count() + 1;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
            }
            
            
        }
        ViewBag.Categories = new SelectList(Repository.Categorys, "CategoryId", "Name");
        return View(model);//(model) yazarak kullanıcının girdiği bilgileri geri gönderiyoruz
        
    }
    public IActionResult Edit(int? id)
    {
        if(id==null)
        {
            return NotFound();//404
        }
        var entity=Repository.Products.FirstOrDefault(p=>p.ProductId==id);
        if(entity==null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categorys, "CategoryId", "Name");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model,IFormFile? imageFile)
    {
        if(id!=model.ProductId)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName);//abc.jpg de jpg alır
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image= randomFileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categorys, "CategoryId", "Name");
        return View(model);
    }
    public IActionResult Delete(int? id)
    {
        if(id==null)
        {
            return NotFound();
        }
        var entity=Repository.Products.FirstOrDefault(p=>p.ProductId==id);
        if(entity==null)
        {
            return NotFound();
        }
        
        return View("DeleteConfirm",entity);
    }
    [HttpPost]
    public IActionResult Delete(int? id, int ProductId)
    {
        if(id!=ProductId)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");
    }
    public IActionResult EditProducts(List<Product> Products)
    {
        foreach(var Product in Products)
        {
            Repository.EditIsActive(Product);
        }
        return RedirectToAction("Index");
    }

}
