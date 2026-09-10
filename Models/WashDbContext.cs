using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CarWash.Models
{
    public class WashDbContext : DbContext
    {
        public WashDbContext() : base("WashDb")
        {
        }
        public virtual DbSet<Wash>Washes { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
    }
}