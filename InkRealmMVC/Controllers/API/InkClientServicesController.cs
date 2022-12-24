using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InkRealmMVC.Models.DbModels;

namespace InkRealmMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InkClientServicesController : ControllerBase
    {
        private readonly InkRealmContext _context;

        public InkClientServicesController(InkRealmContext context)
        {
            _context = context;
        }

        [HttpPut("{clientid}/{masterid}/{serviceid}/{finished}")]
        public async Task<IActionResult> PutInkClientService(int clientId, int masterId, int serviceId, bool finished)
        {
            int maxMaster = _context.InkClientServices.Select(s => s.MasterId).Max();
            int maxClient = _context.InkClientServices.Select(s => s.ClientId).Max();
            int maxService = _context.InkClientServices.Select(s => s.ClientId).Max();

            if (clientId > maxClient || masterId > maxMaster || serviceId > maxService  )
            {
                return BadRequest();
            }

            _context.InkClientServices.Find(clientId, masterId, serviceId).ServiceFinished = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
