﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace salesAPI.Models
{
    public class manifest
    {
        public int AssignjobId { get; set; }
        public int UserId { get; set; }
        public string Assignjobname { get; set; }
        public string Cmlno { get; set; }
        public string Branchname { get; set; }
        public string Name { get; set; }
        public string Regdate { get; set; }
        public string Truckno { get; set; }
        public string Drivername { get; set; }
        public string Drvlncno { get; set; }
        public string Conductorname { get; set; }
        public string Productname { get; set; }
        public int Stockqty { get; set; }
        public int Saleqty { get; set; }
    }
}