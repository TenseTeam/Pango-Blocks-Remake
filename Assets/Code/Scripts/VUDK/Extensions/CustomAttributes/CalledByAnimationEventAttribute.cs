namespace VUDK.Extensions.CustomAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class CalledByAnimationEventAttribute : Attribute
    {
        public string Description { get; }

        public CalledByAnimationEventAttribute()
        {
            Description = string.Empty;
        }

        public CalledByAnimationEventAttribute(string description)
        {
            Description = description;
        }
    }
}
