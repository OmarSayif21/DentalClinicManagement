using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Core.ViewModel
{
    public class DentalChartViewModel
    {
        public int Id { get; set; }
        public Dictionary<string, string> ToothStatuses { get; set; }
    }
}