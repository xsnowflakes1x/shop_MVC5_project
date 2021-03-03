namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long ID { get; set; }
        [Display(Name ="Ngày Tạo")]
        public DateTime? NgayTao { get; set; }

        public int? MaUser { get; set; }

        [StringLength(50)]
        [Display(Name ="Người Nhận")]
        public string NguoiNhan { get; set; }

        [StringLength(50)]
        [Display(Name ="Địa Chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Display(Name ="Địa Chỉ Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name ="Số Điện Thoại")]
        public string SDT { get; set; }

        public decimal? ThanhTien { get; set; }

        public bool TrangThai { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
