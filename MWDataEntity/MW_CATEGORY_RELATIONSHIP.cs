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
    
    public partial class MW_CATEGORY_RELATIONSHIP
    {
        public int source_category { get; set; }
        public int target_category { get; set; }
        public Nullable<float> relationship { get; set; }
    
        public virtual MW_CATEGORY MW_CATEGORY { get; set; }
        public virtual MW_CATEGORY MW_CATEGORY1 { get; set; }
    }
}