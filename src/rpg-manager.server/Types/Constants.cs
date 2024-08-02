namespace rpg_manager.server.Types;

public static class Constants
{
    public static class Jwt
    {
        public const string TokenServiceForAccessJwt = nameof(TokenServiceForAccessJwt);
        public const string TokenServiceForRefreshJwt = nameof(TokenServiceForRefreshJwt);
    }

    public static class TokenClaims
    {
        public const string Id = "Id";
        public const string TokenVersion = "TokenVersion";
    }
}
