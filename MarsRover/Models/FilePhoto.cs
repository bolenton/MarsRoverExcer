using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class FilePhoto
    {
        public string DateString { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }

    public class FileDate
    {
        public string DateStringName { get; set; }
        public DateTime Date { get; set; }
    }
}
