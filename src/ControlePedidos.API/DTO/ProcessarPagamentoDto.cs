using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ControlePedidos.API.DTO;

public class SituacaoPagamentoDto
{
    [Required]
    [ModelBinder(Name = "external_reference")]
    [JsonPropertyName("external_reference")]
    public string IdPedido { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("status")]
    [ModelBinder(Name = "status")]
    public string Status { get; set; } = string.Empty;
}
