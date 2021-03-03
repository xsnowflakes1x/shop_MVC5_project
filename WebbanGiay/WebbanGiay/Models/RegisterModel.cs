using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebbanGiay.Models
{
    public class RegisterModel
    {
        [Key]
        public int MaUser { get; set; }
        [Display(Name="Tên Đăng Nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string TenTaiKhoan { set; get; }
        [Display(Name = "Mật Khẩu")]
        [StringLength(30,MinimumLength =6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string MatKhau { set; get; }
        [Display(Name = "Xác Nhận Mật Khẩu")]
        [Compare("MatKhau",ErrorMessage ="Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string HoTen { set; get; }
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { set; get; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        [EmailAddress(ErrorMessage ="Địa chỉ Email không đúng!")]
        public string Email { set; get; }
        [Display(Name = "Số Điện Thoại")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public string SDT { set; get; }
        [Display(Name ="CMND")]
        [Required(ErrorMessage ="Bạn chưa nhập CMND")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Bạn phải nhập số")]
        public string CMND { set; get; }
    }
}