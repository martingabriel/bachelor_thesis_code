using System;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace StudentsNotifier.MobileAppService.NotificationHubs
{
    public interface INotificationHub
    {
        Task<string> CreateRegistrationId();
        Task DeleteRegistration(string registrationId);
        Task<HubResponse> RegisterForPushNotifications(string id, DeviceRegistration deviceUpdate);
        Task<HubResponse<NotificationOutcome>> SendNotification(Notification newNotification);
    }
}
