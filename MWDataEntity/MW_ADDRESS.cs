//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MWDataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class MW_ADDRESS
    {
        public MW_ADDRESS()
        {
            this.MW_APPAREL_SOURCE = new HashSet<MW_APPAREL_SOURCE>();
        }
    
        public int address_id { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string street_address { get; set; }
        public string unit { get; set; }
    
        public virtual ICollection<MW_APPAREL_SOURCE> MW_APPAREL_SOURCE { get; set; }
    }
}
