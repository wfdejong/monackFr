using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Entities
{
    public class Package
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<Module> Modules { get; set; }

        public Package()
        {
            this.Created = DateTime.Now;
        }
    }
}