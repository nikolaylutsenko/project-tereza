﻿using System.Text.Json.Serialization;

namespace Project.Tereza.Responses.Responses;
public record ErrorResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PropertyName { get; set; }
    public string? Message { get; init; }

    public ErrorResponse()
    {
    }

    public ErrorResponse(string message, string propertyName = null)
    {
        PropertyName = propertyName;
        Message = message;
    }
}