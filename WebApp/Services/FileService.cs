using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Services
{
    public class FileService : Controller
    {
        private readonly IConfiguration config;
        private readonly IFyleData fyleData;
        private readonly IPathData pathData;

        public FileService(IConfiguration config, IFyleData fyleData, IPathData pathData)
        {
            this.config = config;
            this.fyleData = fyleData;
            this.pathData = pathData;
        }

        [HttpGet]
        [Route("/DownloadFile/{id}")]
        public IActionResult DownloadFile(int id)
        {
            return Forbid();
        }
    }
}
