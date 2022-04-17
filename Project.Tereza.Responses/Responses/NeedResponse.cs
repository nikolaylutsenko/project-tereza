using System;

namespace Project.Tereza.Responses.Responses;
public record NeedResponse(Guid id, string Name, string Description, int Count, bool IsCovered);
