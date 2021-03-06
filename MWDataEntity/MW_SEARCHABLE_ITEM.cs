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
    
    public partial class MW_SEARCHABLE_ITEM
    {
        public MW_SEARCHABLE_ITEM()
        {
            this.MW_ITEM_MATCHINGNESS = new HashSet<MW_ITEM_MATCHINGNESS>();
            this.MW_ITEM_MATCHINGNESS1 = new HashSet<MW_ITEM_MATCHINGNESS>();
            this.MW_ITEM_PICTURE = new HashSet<MW_ITEM_PICTURE>();
            this.MW_ITEM_SIMILARITY = new HashSet<MW_ITEM_SIMILARITY>();
            this.MW_ITEM_SIMILARITY1 = new HashSet<MW_ITEM_SIMILARITY>();
            this.MW_OUTFIT_ITEM = new HashSet<MW_OUTFIT_ITEM>();
            this.MW_REVIEW = new HashSet<MW_REVIEW>();
            this.MW_UNIQ_ITEM = new HashSet<MW_UNIQ_ITEM>();
            this.MW_USER_TAG_ITEM = new HashSet<MW_USER_TAG_ITEM>();
            this.MW_TAG = new HashSet<MW_TAG>();
            this.MW_USER = new HashSet<MW_USER>();
        }
    
        public int abstract_item_id { get; set; }
        public int colour_id { get; set; }
        public float price { get; set; }
        public Nullable<float> discount { get; set; }
        public string from_url { get; set; }
        public string title { get; set; }
        public int searchable_item_id { get; set; }
        public int source_id { get; set; }
        public Nullable<int> default_pic { get; set; }
        public string product_code { get; set; }
    
        public virtual MW_ABSTRACT_ITEM MW_ABSTRACT_ITEM { get; set; }
        public virtual MW_APPAREL_SOURCE MW_APPAREL_SOURCE { get; set; }
        public virtual MW_COLOUR MW_COLOUR { get; set; }
        public virtual ICollection<MW_ITEM_MATCHINGNESS> MW_ITEM_MATCHINGNESS { get; set; }
        public virtual ICollection<MW_ITEM_MATCHINGNESS> MW_ITEM_MATCHINGNESS1 { get; set; }
        public virtual ICollection<MW_ITEM_PICTURE> MW_ITEM_PICTURE { get; set; }
        public virtual ICollection<MW_ITEM_SIMILARITY> MW_ITEM_SIMILARITY { get; set; }
        public virtual ICollection<MW_ITEM_SIMILARITY> MW_ITEM_SIMILARITY1 { get; set; }
        public virtual ICollection<MW_OUTFIT_ITEM> MW_OUTFIT_ITEM { get; set; }
        public virtual MW_PICTURE MW_PICTURE { get; set; }
        public virtual ICollection<MW_REVIEW> MW_REVIEW { get; set; }
        public virtual ICollection<MW_UNIQ_ITEM> MW_UNIQ_ITEM { get; set; }
        public virtual ICollection<MW_USER_TAG_ITEM> MW_USER_TAG_ITEM { get; set; }
        public virtual ICollection<MW_TAG> MW_TAG { get; set; }
        public virtual ICollection<MW_USER> MW_USER { get; set; }
    }
}
