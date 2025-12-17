using Microsoft.AspNetCore.Http.HttpResults;

namespace FormsApp.Models
{
    //Depo Bilgilerin tutulduğu yer
    public class Repository
    {
        private static readonly List<Product>_products=new(); //ürünler listesi olusturduk 
        private static readonly List<Category> _categoris=new();//kategori listesi oluşturduk
        static Repository()
        {
            //kategori listesine eleman ekleme
            _categoris.Add(new Category{ CategoryId=1 , Name="Telefon" });
            _categoris.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });
            //Ürün listesine eleman ekleme
            _products.Add(new Product{ ProductId = 1,Name="Iphone 14", Price=40000,IsActive=true, Image = "1.jpg", CategoryId=1});
            _products.Add(new Product { ProductId = 2, Name = "Iphone 15", Price = 50000, IsActive = false, Image = "2.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "Iphone 16", Price = 60000, IsActive = true, Image = "3.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "Iphone 17", Price = 70000, IsActive = false, Image = "4.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 5, Name = "Macbook Air", Price = 80000, IsActive = false, Image = "5.jpg", CategoryId = 2 });
            _products.Add(new Product { ProductId = 6, Name = "Macbook Pro", Price = 90000, IsActive = true, Image = "6.jpg", CategoryId = 2 });
        }
        public static List<Product> Products//ürün listesinin sadece baska sınıftan görüntülenmesi
        {
            get
            {
                return _products;
            }
        }
        public static void CreateProduct(Product Entity)
        {
            _products.Add(Entity);
        }

        public static void EditProduct(Product uptdateProduct)
        {
            var Entity = _products.FirstOrDefault(p=>p.ProductId== uptdateProduct.ProductId);
            if(Entity!=null)
            {
                if(!string.IsNullOrEmpty(uptdateProduct.Name))
                {
                    Entity.Name = uptdateProduct.Name;
                }
                Entity.Name=uptdateProduct.Name;
                Entity.Price = uptdateProduct.Price;
                Entity.Image = uptdateProduct.Image;
                Entity.CategoryId = uptdateProduct.CategoryId;
                Entity.IsActive=uptdateProduct.IsActive;
            }
        }
        public static void EditIsActive(Product uptdateProduct)
        {
            var Entity = _products.FirstOrDefault(p => p.ProductId == uptdateProduct.ProductId);
            if (Entity != null)
            {
                Entity.IsActive = uptdateProduct.IsActive;
            }
        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity=_products.FirstOrDefault(p=>p.ProductId== deletedProduct.ProductId);
            if(entity!=null)
            {
                _products.Remove(entity);
            }
        }
        public static List<Category> Categorys
        {
            get
            {
                return _categoris;
            }
        }

    }
}