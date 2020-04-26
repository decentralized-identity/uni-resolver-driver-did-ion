using System;
using Microsoft.AspNetCore.Http;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Gets or generates identifiers that
    /// can be used for correlation.
    /// </summary>
    public static class CorrelationIdentifier
    {  
        /// <summary>
        /// Gets the correlation id to use in errors.
        /// </summary>
        /// <returns>A string containing the correlation id.</returns>
        public static string Get() 
        {
            return CorrelationIdentifier.Get(null);
        }

        /// <summary>
        /// Gets the correlation id to use in errors. If 
        /// <paramref name="httpContextAccessor"> contains
        /// a trace identifier that is returned, otherwise
        /// a string guid is returned.
        /// </summary>
        /// <returns>A string containing the correlation id.</returns>
        public static string Get(IHttpContextAccessor httpContextAccessor) 
        {
            if (httpContextAccessor != null && 
                httpContextAccessor.HttpContext != null && 
                !string.IsNullOrWhiteSpace(httpContextAccessor.HttpContext.TraceIdentifier))
                {
                    return httpContextAccessor.HttpContext.TraceIdentifier;
                }

            return Guid.NewGuid().ToString();
        }
    }
}