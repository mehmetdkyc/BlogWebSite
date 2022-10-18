using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Writer
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? WriterAbout { get; set; }
        public string? WriterImage { get; set; }
        public bool Status { get; set; }
        public string? MailAdress { get; set; }
        public string? Password { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
