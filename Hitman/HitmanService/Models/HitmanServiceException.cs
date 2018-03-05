using System;

/// <summary>
/// Custome exception for Hitman service
/// </summary>
namespace HitmanService.Models
{
    public class HitmanServiceException : Exception
    {
        public HitmanServiceException()
        {
        }

        public HitmanServiceException(string message)
            : base(message)
        {
        }

        public HitmanServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
