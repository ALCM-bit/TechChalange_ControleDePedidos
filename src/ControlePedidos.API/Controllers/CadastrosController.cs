using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.DTO;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/cadastros")]
public class CadastrosController : BaseController
{
    private readonly ICadastroService _cadastroService;

    public CadastrosController(ICadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpGet]
    public async Task<ActionResult<CadastroResponse>> ObterCadastro(string cpf)
    {
        //Melhorar a validação de existencia.
        try
        {
            CadastroResponse cadastro = await _cadastroService.ObterCadastroAsync(cpf);

            if(cadastro is null)
            {
                return NotFound();
            }

            return Ok(cadastro);
        }
        catch (NotificationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch(Exception ex)
        {
            Console.WriteLine($"[{nameof(CadastrosController)}[{ObterCadastro}] - Unexpected Error - [{ex.Message}]]");
            return BadRequest(new { message = "Ocorreu um erro inesperado." });
        }

    }

    [HttpPost]
    public async Task<ActionResult<string>> Cadastrar(CadastroRequest cadastro)
    {
        try
        {
            var cadastro = await _cadastroService.ObterCadastroAsync(cadastro.CPF);

            if (cadastro is not null)
            {
                return Conflict(new { error = "Cadastro já existente." });
            }

            await _cadastroService.CadastrarAsync(cadastro);

            return Created();
        }
        catch(NotificationException ex)
        {
            return BadRequest(new {error = ex.Message});
        }
        catch(Exception ex)
        {
            Console.WriteLine($"[{nameof(CadastrosController)}[{Cadastrar}] - Unexpected Error - [{ex.Message}]]");
            return BadRequest(new { error = "Ocorreu um erro inesperado." });
        }
    }
}
