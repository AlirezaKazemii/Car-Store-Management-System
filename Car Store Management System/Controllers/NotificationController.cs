using Application.Commands.Notification;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpPost("Create-Notification")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationCommand command)
        {
            await _notificationService.CreateNotification(command);
            return Ok("Notification created successfully.");
        }

        [HttpGet("Send-Notification")]
        public async Task<IActionResult> SendAllNotifications()
        {
            await _notificationService.SendAllUnsentNotifications();
            return Ok("All unsent notifications have been sent.");
        }

    }
}
