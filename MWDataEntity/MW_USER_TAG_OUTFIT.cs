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
    
    public partial class MW_USER_TAG_OUTFIT
    {
        public string user_id { get; set; }
        public int tag_id { get; set; }
        public int outfit_id { get; set; }
        public Nullable<System.DateTime> tag_time { get; set; }
    
        public virtual MW_OUTFIT MW_OUTFIT { get; set; }
        public virtual MW_USER_TAG MW_USER_TAG { get; set; }
    }
}