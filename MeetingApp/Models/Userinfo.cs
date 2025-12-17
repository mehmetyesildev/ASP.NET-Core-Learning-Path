using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Models
{
    public class Userinfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ad alanı zorunlu")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Telefon alanı zorunlu")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Eposta alanı zorunlu")]
        [EmailAddress(ErrorMessage = "Hatalı E-mail")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "katıl durumunuzu Belirtiniz")]
        public bool? WillAttend { get; set; }
    }
}