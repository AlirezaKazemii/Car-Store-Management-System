namespace Application.DTOs.User
{
    public class GetUserByIdDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
