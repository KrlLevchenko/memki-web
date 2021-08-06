using System;
using Dodo.Primitives;

namespace Memki.Model.Auth.Exceptions
{
    public class UserDuplicateException: Exception
    {
        public UserDuplicateException(Uuid userId, string login)
        {
            
        }
    }
}