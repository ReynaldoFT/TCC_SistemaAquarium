using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SiteAquario.Data;
using SiteAquario.Hubs;
using SiteAquario.Models;

namespace SiteAquario.Controllers
{
    public class ValoresSensorController : Controller
    {

        private readonly SiteAquarioContext _context;
        private readonly IHubContext<SensorHub> _hubContext;

        public ValoresSensorController(SiteAquarioContext context, IHubContext<SensorHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }


        [HttpPost("ReceiveData")]
        [HttpGet("ValoresSensor/ReceiveData")]
        public async Task<IActionResult> ReceiveData([FromQuery] double temperature, [FromQuery] double ph)
        {

            var valoresSensor = new ValoresSensor
            {
                Temperatura = temperature,
                Ph = ph,
                DataCadastro = DateTime.Now
            };
            _context.ValoresSensor.Add(valoresSensor);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveSensorData", valoresSensor);

            return Ok(valoresSensor);
        }

        public async Task<IActionResult> Index()
        {
              return _context.ValoresSensor != null ? 
                          View(await _context.ValoresSensor.ToListAsync()) :
                          Problem("Entity set 'Contexto.ValoresSensor'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ValoresSensor == null)
            {
                return NotFound();
            }

            var valoresSensor = await _context.ValoresSensor
                .FirstOrDefaultAsync(m => m.ValoresSensorID == id);
            if (valoresSensor == null)
            {
                return NotFound();
            }

            return View(valoresSensor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ValoresSensorID,Ph,Temperatura,DataCadastro")] ValoresSensor valoresSensor)
        {
            if (ModelState.IsValid)
            {
                valoresSensor.DataCadastro = DateTime.Now;
                _context.Add(valoresSensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(valoresSensor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ValoresSensor == null)
            {
                return NotFound();
            }

            var valoresSensor = await _context.ValoresSensor.FindAsync(id);
            if (valoresSensor == null)
            {
                return NotFound();
            }
            return View(valoresSensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ValoresSensorID,Ph,Temperatura,DataCadastro")] ValoresSensor valoresSensor)
        {
            if (id != valoresSensor.ValoresSensorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valoresSensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValoresSensorExists(valoresSensor.ValoresSensorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(valoresSensor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ValoresSensor == null)
            {
                return NotFound();
            }

            var valoresSensor = await _context.ValoresSensor
                .FirstOrDefaultAsync(m => m.ValoresSensorID == id);
            if (valoresSensor == null)
            {
                return NotFound();
            }

            return View(valoresSensor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ValoresSensor == null)
            {
                return Problem("Entity set 'Contexto.ValoresSensor'  is null.");
            }
            var valoresSensor = await _context.ValoresSensor.FindAsync(id);
            if (valoresSensor != null)
            {
                _context.ValoresSensor.Remove(valoresSensor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValoresSensorExists(int id)
        {
          return (_context.ValoresSensor?.Any(e => e.ValoresSensorID == id)).GetValueOrDefault();
        }
    }
}
