//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rollin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Vehicle Name")]
        [StringLength(50)]
        public string VehicleName { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> UnitPrice { get; set; }
        [Required]
        [Display(Name = "Made Year")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> MadeYear { get; set; }
        public string ImageUrl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}