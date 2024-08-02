namespace rpg_manager.server.Startup;

public record JwtConfigOptions(string PublicKey, string PrivateKey, int TokenLifetimeInMinutes);
