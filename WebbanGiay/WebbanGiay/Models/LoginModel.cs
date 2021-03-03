using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebbanGiay.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name ="Tên Đăng Nhập")]
        [Required(ErrorMessage ="Bạn phải nhập tên đăng nhập")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [Display(Name ="Mật Khẩu")]
        public string Password { set; get; }
    }
}