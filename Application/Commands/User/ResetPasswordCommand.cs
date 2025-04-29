namespace Application.Commands.User
{
    public class ResetPasswordCommand
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
