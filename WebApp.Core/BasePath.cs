using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Core
{
    public class BasePath
    {
        [Required, Key]
        public string Path { set; get; }
    }
}
