using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge_pantry.Models
{
    public class Summary
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string LinkToCreator { get; set; }
        public string Annotation { get; set; }
        public int NumberOfSpecialty { get; set; }
        public string Text { get; set; }
        public int Like { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
