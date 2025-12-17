using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController:Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> index()
        {
            return View(await _context.Ogrenciler.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var Ogr = await _context
                            .Ogrenciler
                            .Include(o=>o.KursKayitlari)
                            .ThenInclude(o=>o.Kurs)
                            .FirstOrDefaultAsync(o=>o.OgrenciId==id);
            // var Ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o=>o.OgrenciId==id);
            if (Ogr == null)
            {
                return NotFound();
            }
            return View(Ogr);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogrenci model)

        {
            if(id!=model.OgrenciId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Ogrenciler.Any(o=>o.OgrenciId==model.OgrenciId))
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
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var Ogrenci=await _context.Ogrenciler.FindAsync(id);
            if(Ogrenci==null)
            {
                return NotFound();
            }
            return View(Ogrenci);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var Ogrenci=await _context.Ogrenciler.FindAsync(id);
            if(Ogrenci==null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(Ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
    
    
}