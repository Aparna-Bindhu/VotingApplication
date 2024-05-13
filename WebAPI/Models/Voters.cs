using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Voters
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasVoted { get; set; }
    }
}