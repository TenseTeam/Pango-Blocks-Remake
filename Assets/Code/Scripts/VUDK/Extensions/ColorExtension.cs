namespace VUDK.Extensions.Colors
{
    using UnityEngine;

    public static class ColorExtension
    {
        /// <summary>
        /// Checks if two color are similar.
        /// </summary>
        /// <param name="a">Color A.</param>
        /// <param name="b">Color B.</param>
        /// <param name="tolerance">Tolerance value.</param>
        /// <returns>True if they are similar, False if they are not.</returns>
        public static bool ColorEquals(this Color a, Color b, float tolerance = 0.01f)
        {
            return Mathf.Abs(a.r - b.r) < tolerance
                && Mathf.Abs(a.g - b.g) < tolerance
                && Mathf.Abs(a.b - b.b) < tolerance
                && Mathf.Abs(a.a - b.a) < tolerance;
        }

        /// <summary>
        /// Gets a random color.
        /// </summary>
        /// <returns>Random color.</returns>
        public static Color RandomColor()
        {
            return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }

        /// <summary>
        /// Copies a color.
        /// </summary>
        /// <param name="color">Color to copy.</param>
        /// <returns>Copied color.</returns>
        public static Color Copy(this Color color)
        {
            return new Color(color.r, color.g, color.b, color.a);
        }
    }
}