namespace Le6pergram.Web.Utilities
{
    using System.Linq;

    public static class TagUtilities
    {
        public static bool IsTagExisting(string tag, Le6pergramDatabase context)
        {
            return context.Tags.Any(t => t.Name == tag);
        }
    }
}