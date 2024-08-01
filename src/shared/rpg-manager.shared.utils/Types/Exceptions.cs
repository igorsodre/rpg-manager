namespace rpg_manager.shared.utils.Types;

public class ApplicationException(string message, int? code) : Exception(message)
{
    public required int Code { get; set; } = code ?? 500;
}
