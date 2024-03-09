using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }
}
