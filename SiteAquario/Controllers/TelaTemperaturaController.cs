using Microsoft.AspNetCore.Mvc;
using SiteAquario.Data;
using Microsoft.EntityFrameworkCore;
using SiteAquario.Hubs; 
using Microsoft.AspNetCore.SignalR;

namespace SiteAquario.Controllers
{

    public class TelaTemperaturaController : Controller
    {
        private readonly SiteAquarioContext _context;

        public TelaTemperaturaController(SiteAquarioContext context, IHubContext<SensorHub> hubContext)
        {
            _context = context;
        }

        [HttpGet("ValoresSensor/TemperaturaHistorico")]
        public async Task<IActionResult> TemperaturaHistorico()
        {
            var historico = await _context.ValoresSensor
               .OrderBy(v => v.DataCadastro)
               .Select(v => new { v.Temperatura, v.DataCadastro })
               .ToListAsync();
            return Ok(historico);
        }

        [HttpGet("ValoresSensor/PhHistorico")]
        public async Task<IActionResult> PhHistorico()
        {
            var historico = await _context.ValoresSensor
               .OrderBy(v => v.DataCadastro)
               .Select(v => new { v.Ph, v.DataCadastro })
               .ToListAsync();
            return Ok(historico);
        }

        [HttpGet("ValoresSensor/Temperatura")]
        public IActionResult Temperatura()
        {
            return View();
        }

        [HttpGet("ValoresSensor/Ph")]
        public IActionResult Ph()
        {
            return View();
        }
    }
}
