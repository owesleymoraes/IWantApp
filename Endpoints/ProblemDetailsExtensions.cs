using Flunt.Notifications;

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

    }
}