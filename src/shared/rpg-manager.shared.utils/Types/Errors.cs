using System.Net;

namespace rpg_manager.shared.utils.Types;

public record ApplicationError(string Message, HttpStatusCode StatusCode);

public record NotFoundError(string Message) : ApplicationError(Message, HttpStatusCode.NotFound);
