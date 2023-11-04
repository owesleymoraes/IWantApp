using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace _4_IWantApp.Endpoints
{
    public static class ProblemDetailsExtensions
    {
        public static Dictionary<string, string[]> ConvertToProblemDetail(this IReadOnlyCollection<Notification> notifications)
        {
            return notifications
                .GroupBy(groupError => groupError.Key)
                .ToDictionary(groupError => groupError.Key, keyError => keyError.Select(typeError => typeError.Message)
                .ToArray());
        }

        public static Dictionary<string, string[]> ConvertToProblemDetail(this IEnumerable<IdentityError> notifications)
        {
            return notifications
                .GroupBy(groupError => groupError.Code)
                .ToDictionary(groupError => groupError.Key, keyError => keyError.Select(typeError => typeError.Description)
                .ToArray());
        }

    }
}