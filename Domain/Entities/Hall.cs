using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Place> Places { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
