namespace PersonDirectory.Application.Resources
{
    public static class ResourceExtensions
    {
        public static string GetLocalizedResource(this string key)
        {
            var value = Resource.ResourceManager.GetString(key);

            return value == null ? string.Empty : value;
        }
    }
}
