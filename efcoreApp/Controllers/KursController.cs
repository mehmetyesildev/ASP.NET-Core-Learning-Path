using System.Threading.Tasks;
using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController:Controller
    {
       private readonly DataContext _context;
       public KursController (DataContext context)
       {
         _context=context;
       }
        public async Task<IActionResult> index()
        {
            return View(await _context.Kurslar.Include(k=>k.Ogretmen).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler=new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if(ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);

        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Ogr = await _context
                            .Kurslar
                            .Include(k=>k.KursKayitlari)
                            .ThenInclude(k=>k.Ogrenci)
                            .Select(k=>new KursViewModel
                            {
                                KursId=k.KursId,
                                Baslik=k.Baslik,
                                OgretmenId=k.OgretmenId,
                                KursKayitlari=k.KursKayitlari
                            })
                            .FirstOrDefaultAsync(k=>k.KursId==id);
            // var Ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o=>o.OgrenciId==id);
            if (Ogr == null)
            {
                return NotFound();
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(Ogr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursViewModel model)

        {
            if (id != model.KursId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs(){KursId=model.KursId,Baslik=model.Baslik,OgretmenId=model.OgretmenId});
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Ogrenci = await _context.Kurslar.FindAsync(id);
            if (Ogrenci == null)
            {
                return NotFound();
            }
            return View(Ogrenci);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}