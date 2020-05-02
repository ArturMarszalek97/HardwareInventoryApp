using HardwareInventoryService.Models.Models;
using HardwareInventoryService.Models.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInventoryApp.Helpers
{
    public static class Data
    {
        public static Session Session { get; set; }

        public static List<Item> Items { get; set; }
    }
}
