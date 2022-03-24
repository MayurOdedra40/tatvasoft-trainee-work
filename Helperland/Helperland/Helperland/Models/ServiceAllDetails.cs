using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;

namespace Helperland.Models
{
    public class ServiceAllDetails
    {
        public ServiceRequest Service { get; set; }

        public ServiceRequestAddress ServiceAddress { get; set; }

        public ServiceRequestExtra ServiceExtra { get; set; }

    }
}
