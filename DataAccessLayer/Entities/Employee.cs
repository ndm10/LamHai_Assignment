using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Employee : BaseEntity
    {
        [Required(ErrorMessage = "Vui lòng nhập mã nhân viên")]
        public string EmployeeCode { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; } = null!;

        [MaxLength(10, ErrorMessage = "Số điện thoại giới hạn 10 chữ số")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nơi ở hiện tại")]
        public string Address { get; set; } = null!;

        public string ManageById { get; set; } = null!;
        public IdentityUser ManageBy { get; set; } = null!;
    }
}
