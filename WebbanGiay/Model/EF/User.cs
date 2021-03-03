namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int MaUser { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Tên Tài Khoản")]
       
        public string TenTaiKhoan { get; set; }

        [Required]
        [Display(Name ="Mật Khẩu")]
        [StringLength(50,MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự")]
        public string MatKhau { get; set; }

        [StringLength(50)]
        [Display(Name ="Họ Tên")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage ="Bạn chưa nhập")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public string SDT { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public string CMND { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        [Display(Name = "Địa Chỉ Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không đúng!")]
        public string Email { get; set; }
        [Display(Name = "Địa Chỉ")]
        [Required(ErrorMessage = "Bạn chưa nhập")]
        public string DiaChi { get; set; }

        public bool? LaAdmin { get; set; }

        public string AnhDaiDien { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
