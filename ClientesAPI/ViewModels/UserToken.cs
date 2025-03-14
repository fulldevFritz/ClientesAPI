using System;

namespace ClientesAPI.ViewModels
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
