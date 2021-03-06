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
    
    public partial class reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public reservation()
        {
            this.feedback_comments = new HashSet<feedback_comments>();
            this.loans = new HashSet<loan>();
            this.reservation_items = new HashSet<reservation_items>();
        }
    
        public int id { get; set; }
        public int user { get; set; }
        public System.DateTime dateRevesed { get; set; }
        public Nullable<int> pickuplocation { get; set; }
        public Nullable<System.DateTime> until { get; set; }
        public byte canceled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<feedback_comments> feedback_comments { get; set; }
        public virtual library library { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<loan> loans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservation_items> reservation_items { get; set; }
        public virtual user user1 { get; set; }
    }
}
