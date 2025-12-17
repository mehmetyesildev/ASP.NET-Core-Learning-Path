using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController: Controller
    {
        //localhost/
        //localhost/home
        // localhost/home/index
        public IActionResult Index()
        {

            int saat = DateTime.Now.Hour;
            // ViewBag.selamlama= saat > 12 ? "iyi günler" : "Günaydın";
            // ViewBag.UserName="Mehmet";
            ViewData["selamlama"]= saat > 12 ? "iyi günler" : "Günaydın";
             int UserCount = Repository.Users.Where(info => info.WillAttend == true).Count();
            // ViewData["UserName"]= "Vehbi";
            var meetinginfo=new Meetinginfo()
            {
                Id=1,
                Location="Konya selçuklu kongre merkezi",
                Date=new DateTime(2025,12,10,10,0,0),
                NumberofPeople= UserCount
            };
            return View(meetinginfo);
        }
    }
}