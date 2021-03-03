namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public string TenSanPham { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường Dẫn")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public string MoTa { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình Ảnh")]
        public string HinhAnh { get; set; }
        [Display(Name = "Giá Bán")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public decimal? GiaBan { get; set; }
        [Display(Name = "Số Lượng")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public int SoLuong { get; set; }
        [Display(Name = "Loại Sản Phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public long? MaLoaiSP { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi Tiết")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public string ChiTiet { get; set; }
        [Display(Name = "Bảo Hành")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public int? BaoHanh { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(250)]
        public string TuKhoaMieuTa { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
