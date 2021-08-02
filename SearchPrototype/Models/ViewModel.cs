using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchPrototype.Models
{
    // Class used for passing multiple collections to View. 
    public class ViewModel
    {
        public ViewModel(IEnumerable<tblCategory> categories, IEnumerable<tblSpeciality> specialities, IEnumerable<tblLocation> locations)
        {
            Categories = categories;
            Specialities = specialities;
            Locations = locations;
        }

        public IEnumerable<tblCategory> Categories { get; set; }
        public IEnumerable<tblSpeciality> Specialities { get; set; }
        public IEnumerable<tblLocation> Locations { get; set; }
    }
}