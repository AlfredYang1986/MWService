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
    
    public partial class MW_CATEGORY_CN
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
    
        public virtual MW_CATEGORY MW_CATEGORY { get; set; }
    }
}
