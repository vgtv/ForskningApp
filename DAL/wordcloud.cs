//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class wordcloud
    {
        public int Id { get; set; }
        public string cristinID { get; set; }
        public Nullable<int> key { get; set; }
        public Nullable<short> count { get; set; }
    
        public virtual person person { get; set; }
        public virtual words words { get; set; }
    }
}