//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FitMind.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trophy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trophy()
        {
            this.UserTrophies = new HashSet<UserTrophy>();
        }
    
        public int trophyId { get; set; }
        public string description { get; set; }
        public string path { get; set; }
        public Nullable<int> pointsAwarded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTrophy> UserTrophies { get; set; }
    }
}
