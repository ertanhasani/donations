using System;

namespace Donations.Common.Helpers
{
    public static class Guard
    {
        public static void GuidNotEmpty(Guid value, string fieldName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException($"{fieldName} is empty Guid!");
            }
        }

        public static void NotNull<T>(T value, string fieldName)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{fieldName} is null!");
            }
        }

        public static void StringNotEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} is empty!");
            }
        }
    }
}
