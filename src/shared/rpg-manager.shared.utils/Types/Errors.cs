using System.Net;

namespace rpg_manager.shared.utils.Types;

public record ApplicationError(
    string ErrorMessage,
    Dictionary<string, List<string>> ErrorMessages,
    HttpStatusCode StatusCode
);

public record NotFoundError(string ErrorMessage) : ApplicationError(ErrorMessage, [], HttpStatusCode.NotFound);
