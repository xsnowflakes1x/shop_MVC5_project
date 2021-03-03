namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        [Key]
        public int PhanQuyenID { get; set; }

        [StringLength(50)]
        public string TenQuyen { get; set; }
    }
}
