//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestGit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.reservations = new HashSet<reservation>();
        }
    
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string card_id { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string town { get; set; }
        public string county { get; set; }
        public string post_code { get; set; }
        public int country { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public System.DateTime date { get; set; }
        public byte active { get; set; }
        public string hash { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservation> reservations { get; set; }
    }
}
