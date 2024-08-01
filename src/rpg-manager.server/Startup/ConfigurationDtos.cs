namespace rpg_manager.server.Startup;

public record AuthenticationSection(string PublicKey, string PrivateKey, int TokenLifetimeInMinutes);
