//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchPrototype.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSpecialitiesToCompany
    {
        public int intID { get; set; }
        public int intSpecialityID { get; set; }
        public int intCompanyID { get; set; }
    
        public virtual tblCompany tblCompany { get; set; }
        public virtual tblSpeciality tblSpeciality { get; set; }
    }
}
