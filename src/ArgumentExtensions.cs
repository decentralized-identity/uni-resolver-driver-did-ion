using System;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Provides extension methods for performing operations arguments.
    /// </summary>
    public static class ArgumentExtensions
    {
        /// <summary>
        /// Checks to see if <paramref name="argument" /> is null, empty or 
        /// whitespace.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The argument name to include in thrown exceptions.</param>
        /// <returns>The <paramref name="argument"/> if populated.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"> is empty or is whitespace.</exception>
        public static string IsPopulated(this string argument, string argumentName) {
            // Check for null
            if (argument == null) {
                throw new ArgumentNullException(argumentName);
            }

            // Check for whitespace
            if (string.IsNullOrWhiteSpace(argument) || string.IsNullOrEmpty(argument)) {
                throw new ArgumentException(argumentName);
            }

            // Argument is populated so return
            return argument;
        }

        /// <summary>
        /// Checks to see if <paramref name="argument" /> is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument to check for null.</typeparam>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The argument name to include in thrown exceptions.</param>
        /// <returns>The <paramref name="argument"/> if populated.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"> is null.</exception>
        public static T IsNull<T>(this T argument, string argumentName) {
            // Check for null
            if (argument == null) {
                throw new ArgumentNullException(argumentName);
            }
            
            // Argument is not null
            return argument;
        }

        /// <summary>
        /// Checks to see if <paramref name="argument" /> is the default
        /// for the type.
        /// </summary>
        /// <typeparam name="T">The type of the argument to check for default.</typeparam>
        /// <param name="argument">The argument to check.</param>
        /// <returns>True if the type is the default, otherwise false.</returns>
        public static bool IsDefault<T>(this T argument) {
            return argument.Equals(default(T));
        }
    }
}
