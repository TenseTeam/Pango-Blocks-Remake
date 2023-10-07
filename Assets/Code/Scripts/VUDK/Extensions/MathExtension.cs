namespace VUDK.Extensions.Mathematics
{
    using UnityEngine;

    public static class MathExtension
    {
        /// <summary>
        /// Returns the percent of the number based on the given max number
        /// </summary>
        /// <param name="n">number</param>
        /// <param name="max">max number</param>
        /// <returns>percentage</returns>
        public static float Percent(float n, float max)
        {
            return (n / max) * 100f;
        }

        /// <summary>
        /// Gets the percent of the number based on the given max number
        /// </summary>
        /// <param name="n">number</param>
        /// <param name="max">max number</param>
        /// <returns>percentage</returns>
        public static float AsPercentOf(this float n, float max)
        {
            return (n / max) * 100f;
        }

        /// <summary>
        /// Allows to see if a number is approximately the same compared with an other number.
        /// </summary>
        /// <param name="a">This number.</param>
        /// <param name="b">Other number.</param>
        /// <param name="tollerance">Tollerance Threshold.</param>
        /// <returns>True if they are approximately the same, False if not.</returns>
        public static bool IsApproximatelyEqual(this float myNumber, float number, float tollerance = 0.01f)
        {
            myNumber = Mathf.Max(myNumber, number);
            number = Mathf.Min(myNumber, number);

            return (myNumber - number < tollerance);
        }

        /// <summary>
        /// Gets the closest multiple angle of a given angle in degree.
        /// </summary>
        /// <param name="angle">Given angle in degree.</param>
        /// <param name="multiple">Multiple</param>
        /// <returns>Closest multiple angle.</returns>
        public static float GetClosestMultipleAngle(this float angle, float multiple)
        {
            return Mathf.Round(angle / multiple) * multiple;
        }
    }
}