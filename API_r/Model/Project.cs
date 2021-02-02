using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_r.Model
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Img { get; set; }

        public string Technologies { get; set; }

        public string WebSite { get; set; }
    }
}
