using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs.Request
{
    public class RequestUpdateEmployeeDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã nhân viên")]
        public string EmployeeCode { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        [Required(ErrorMessage = "Vui lòng nhập email nhân viên")]
        public string Email { get; set; } = null!;

        [MaxLength(10, ErrorMessage = "Số điện thoại giới hạn 10 chữ số")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại nhân viên")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nơi ở hiện tại")]
        public string Address { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
