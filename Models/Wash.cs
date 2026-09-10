using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Models
{
    public class Wash
    {
        [Key]
        public int WashId { get; set; }
        public string WashName { get; set; }
        public double Price { get; set; }
    }
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public double Price { get; set; }
    }
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string CustomerName { get; set; }
        public DateTime LicenseExpDate { get; set; }

        public virtual Wash Wash { get; set; }
        public int WashId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public double WashTypeCost { get; set; }
        public double VehicleTypeCost { get; set; }
        public double TotalCost { get; set; }



        public double pullWashTypeCost()
        {
            WashDbContext db = new WashDbContext();
            var price = (from w in db.Washes
                         where WashId == w.WashId
                         select w.Price).Single();
            return price;
        }
        public double pullVehicleTypeCost()
        {
            WashDbContext db = new WashDbContext();
            var Price = (from v in db.Vehicles
                         where VehicleId == v.VehicleId
                         select v.Price).Single();
            return Price;
        }
        public double calcTotalCost()
        {
            double cost = 0;
            if (LicenseExpDate.Day < DateTime.Now.Day)
            {
                cost = 200 + pullWashTypeCost() + pullVehicleTypeCost();
            }
            else
            {
                cost = pullWashTypeCost() + pullVehicleTypeCost();
            }
            return cost;
        }

    }
}
    
