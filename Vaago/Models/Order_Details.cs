//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vaago.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Details
    {
        public int detailID { get; set; }
        public Nullable<int> orderID { get; set; }
        public Nullable<int> itemQuantity { get; set; }
        public Nullable<int> itemID { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Menu Menu1 { get; set; }
        public virtual Order Order { get; set; }
        public virtual Order Order1 { get; set; }
    }
}
