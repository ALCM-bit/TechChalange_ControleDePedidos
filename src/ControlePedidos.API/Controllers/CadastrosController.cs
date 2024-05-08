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
            var cadastradoJaExiste = await _cadastroService.ObterCadastroAsync(cadastro.CPF);

            if (cadastradoJaExiste is not null)
            {
                return BadRequest("Cadastro já existe.");
            }

            await _cadastroService.CadastrarAsync(cadastro);

            var cadastrado = await _cadastroService.ObterCadastroAsync(cadastro.CPF);

            if (cadastrado is null)
            {
                return BadRequest("Ocorreu um erro inesperado ao salvar o cadastro. Tente novamente mais tarde.");
            }


            return Ok($"{cadastro.Nome} - Cadastrado com sucesso!");
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
