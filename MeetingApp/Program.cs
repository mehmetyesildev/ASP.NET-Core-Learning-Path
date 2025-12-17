var builder = WebApplication.CreateBuilder(args);
// servis ekliyoruz 
builder.Services.AddControllersWithViews();//projeye Mvc sablonunu tanıtıyoruz yani servisini ekliyoruz

var app = builder.Build();
app.UseStaticFiles();//yeni eklenen wwwroot dosyasını erissime acık hale yani statik dosyalı erisime acık halae getirmek için yaptık
app.UseRouting();//Routing yönlendirmesi için middleawere aktif ediyoruz

//{controller=home}/{action=Index}/id?
//app.MapDefaultControllerRoute(); // endpointleri sildik controller için sema ekledik 
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}"

);

app.Run();
