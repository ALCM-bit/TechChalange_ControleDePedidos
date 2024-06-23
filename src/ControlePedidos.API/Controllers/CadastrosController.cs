﻿using CadastroPedidos.Pedido.Application.UseCases.ObterPedido;
using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.DTO;
using ControlePedidos.Cadastro.Application.UseCases.GravarCadastro;
using ControlePedidos.Cadastro.Application.UseCases.ObterCadastro;
using ControlePedidos.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ControlePedidos.API.Controllers;

[Route("api/cadastros")]
public class CadastrosController : BaseController
{
    private readonly IUseCase<ObterCadastroRequest, ObterCadastroResponse> _obterCadastroUseCase;
    private readonly IUseCase<GravarCadastroRequest> _gravarCadastroUseCase;

    public CadastrosController(
        IUseCase<ObterCadastroRequest, 
        ObterCadastroResponse> obterCadastroUseCase,
        IUseCase<GravarCadastroRequest> gravarCadastroUseCase)
    {
        _obterCadastroUseCase = obterCadastroUseCase;
        _gravarCadastroUseCase = gravarCadastroUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<ObterCadastroResponse>> ObterCadastro([FromQuery] ObterCadastroRequest request)
    {
        try
        {
            ObterCadastroResponse cadastro = await _obterCadastroUseCase.ExecuteAsync(request);
            
            if (cadastro is null)
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
    public async Task<ActionResult<string>> Cadastrar(GravarCadastroRequest cadastroRequest)
    {
        try
        {
            var cadastro = await _obterCadastroUseCase.ExecuteAsync(new ObterCadastroRequest() { CPF = cadastroRequest.CPF });

            if (cadastro is not null)
            {
                return Conflict(new { error = "Cadastro já existente." });
            }

            await _gravarCadastroUseCase.ExecuteAsync(cadastroRequest);

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
