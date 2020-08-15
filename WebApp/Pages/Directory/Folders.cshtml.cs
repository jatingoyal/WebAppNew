using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WebApp.Core;
using WebApp.Data;

namespace WebApp.Pages.Directory
{
    public class FoldersModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IFolderData folderData;

        public FoldersModel(IConfiguration config, IFolderData folderData)
        {
            this.config = config;
            this.folderData = folderData;
        }

        public string Message { set; get; }
        public IEnumerable<Folder> Folders { set; get; }
        


        public void OnGet()
        {
            Message = config["Message"];
            Folders = folderData.GetAll();
        }
    }
}
