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
    
    public partial class UserTrophy
    {
        public int userTrophyId { get; set; }
        public Nullable<int> trophyId { get; set; }
        public Nullable<int> userId { get; set; }
    
        public virtual Trophy Trophy { get; set; }
        public virtual User User { get; set; }
    }
}
