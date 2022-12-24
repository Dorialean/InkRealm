using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InkRealmMVC.Models.DbModels;
using Npgsql;

namespace InkRealmMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InkProductsController : ControllerBase
    {
        private readonly InkRealmContext _context;

        public InkProductsController(InkRealmContext context)
        {
            _context = context;
        }

        // GET: api/InkProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InkProduct>>> GetInkProducts()
        {
          if (_context.InkProducts == null)
          {
              return NotFound();
          }
            return await _context.InkProducts.ToListAsync();
        }

        // GET: api/InkProducts/info
        [HttpGet("{info}")]
        public async Task<ActionResult<List<InkProduct>>> GetInkProduct(string info)
        {
            if (_context.InkProducts == null)
            {
                return NotFound();
            }
            List<InkProduct> incFilteredProds = new();

            using (NpgsqlConnection conn = new(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM ink_products WHERE title LIKE '%' || $1 ||'%' OR description LIKE '%' || $1 || '%'";
                NpgsqlCommand cmd = new(query, conn)
                {
                    Parameters = { new(){ Value = info } }
                };
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    string? photoLink = await reader.IsDBNullAsync(4) ? null : reader.GetString(4);
                    string? description = await reader.IsDBNullAsync(3) ? null : reader.GetString(2);

                    incFilteredProds.Add(new()
                    {
                        ProductId = reader.GetGuid(0),
                        Title = reader.GetString(1),
                        Description = description,
                        EachPrice = reader.GetDecimal(3),
                        PhotoLink = photoLink
                    });
                }
            }
                
            if (incFilteredProds == null)
            {
                return NotFound();
            }

            return incFilteredProds;
        }
    }
}
