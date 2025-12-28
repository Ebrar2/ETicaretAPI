using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions.User
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException()
        {
        }

        public PasswordChangeFailedException(string? message="Şifre değiştirme işlemi başarısız") : base(message)
        {
        }

        public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PasswordChangeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
