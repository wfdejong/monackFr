using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Entities
{
    public class Module
    {
        public int Id { get; set; }

        public Tile Tile { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public Dictionary<string, string> MetaData { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}