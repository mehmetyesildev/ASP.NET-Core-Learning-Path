using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace FormsApp.Models
{
    //Ürün modelis
    public class Product//ürün 
    {

        [Display(Name="urun Id")]
        public int ProductId { get; set; }
        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage ="Gerekli bir alan")]
        [StringLength(100)]
        public string Name { get; set; }=null!;
        [Required]
        [Range(0,100000,ErrorMessage = "0 ile 100.000 arasında")]
        [Display(Name = "Ürün Fiyatı")]
        
        public decimal? Price { get; set; }//fiyat
        [Display(Name = "Resmi")]
        public string Image { get; set; } = string.Empty;
        [Display(Name = "Satış Durumu")]
        public bool IsActive{get;set;} // ürünün aktiflik bilgisi
        [Display(Name = "Kategori")]
        [Required]
        public int? CategoryId { get; set; }

    }
}