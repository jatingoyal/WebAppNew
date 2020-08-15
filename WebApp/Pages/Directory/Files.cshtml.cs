using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WebApp.Core;
using WebApp.Data;
using WebApp.Filters;

namespace WebApp.Pages.Directory
{
    public class FilesModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IFyleData fileData;

        public FilesModel(IConfiguration config, IFyleData fileData)
        {
            this.config = config;
            this.fileData = fileData;
        }
        public IEnumerable<Fyle> Files { set; get; }
        
        [BindProperty(SupportsGet =true)]
        public string Folder { set; get; }

        public IActionResult OnGet()
        {
            Files = fileData.GetFilesFromFolder(Folder);
            if (Files != null)
                return Page();
            else
                return RedirectToPage("./NotFound");
        }


    }
}
