//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAN_LX_Kristina_Garcia_Francisco.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            this.tblUser1 = new HashSet<tblUser>();
        }
    
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string IDCard { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> ManagerID { get; set; }
        public int SectorID { get; set; }
        public int LocationID { get; set; }
    
        public virtual tblLocation tblLocation { get; set; }
        public virtual tblSector tblSector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUser> tblUser1 { get; set; }
        public virtual tblUser tblUser2 { get; set; }
    }
}
