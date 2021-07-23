namespace Memki.Api.Auth.Login
{
    public class Response
    {
        private Response(bool ok, string token)
        {
            Ok = ok;
            Token = token;
        }

        public static Response Success(string token) => new Response(true, token);

        public static Response Fail() => new Response(false, "");
        
        public bool Ok { get; }

        public string Token { get; }
    }
}