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
    
    public partial class MW_APPAREL_SOURCE
    {
        public MW_APPAREL_SOURCE()
        {
            this.MW_ITEM_PICTURE = new HashSet<MW_ITEM_PICTURE>();
            this.MW_SEARCHABLE_ITEM = new HashSet<MW_SEARCHABLE_ITEM>();
        }
    
        public int source_id { get; set; }
        public string source_name { get; set; }
        public string source_url { get; set; }
        public Nullable<int> address_id { get; set; }
        public Nullable<System.DateTime> last_checked { get; set; }
    
        public virtual MW_ADDRESS MW_ADDRESS { get; set; }
        public virtual ICollection<MW_ITEM_PICTURE> MW_ITEM_PICTURE { get; set; }
        public virtual ICollection<MW_SEARCHABLE_ITEM> MW_SEARCHABLE_ITEM { get; set; }
    }
}