namespace VUDK.Extensions.Strings
{
    using System;

    public static class StringExtension
    {
        /// <summary>
        /// Gets a random generated string using Guid.
        /// </summary>
        /// <returns>Random generated string.</returns>
        public static string Random()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets a random generated string using Guid with a given length.
        /// </summary>
        /// <param name="length">String length.</param>
        /// <returns>Random generated string.</returns>
        public static string Random(int length)
        {
            return Guid.NewGuid().ToString().Substring(0, length);
        }
    }
}
