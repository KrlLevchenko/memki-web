namespace Memki.Api.Auth.Register
{
    public class Response
    {
        private Response(bool userExist, string token)
        {
            UserExist = userExist;
            Token = token;
        }

        public static Response Success(string token) => new Response(false, token);

        public static Response UserAlreadyExist() => new Response(true, "");
        
        public bool UserExist { get; }

        public string Token { get; }
    }
}