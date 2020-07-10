using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/" /*Puerto WEB API*/);

                //request
                var result = await client.GetStringAsync("products");
                _logger.LogInformation("Response: {0}", result);

                //Deserealizacion.. (JSON -> Objetos)
                Product[] models = JsonSerializer.Deserialize<Product[]>(result);
                ViewData["Models"] = models;

                // 1) Cambiar Id => id en producto
                // 2) Mostrar los productos en el HTML/Razor

                return Page();
            }
        }
    }
}
