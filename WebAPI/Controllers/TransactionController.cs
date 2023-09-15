using AutoMapper;
using Bank.Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Repository;
using WebAPI.Response;
using WebAPI.Validators;
using Helper.Common;
using WebAPI.Models;
using WebAPI.Repository.Account.Command;
using WebAPI.Repository.Transaction.Command;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        IMapper _mapper;
        IRepositoryWrapper _repository;
        DepositCommandHandler _depositCommandHandler;
        WithdrawCommandHandler _withdrawCommandHandler;
        public TransactionController(
            IMapper mapper,
            IRepositoryWrapper repository,
            DepositCommandHandler depositCommandHandler,
            WithdrawCommandHandler withdrawCommandHandler)
        {
            _mapper = mapper;
            _repository = repository;
            _depositCommandHandler = depositCommandHandler;
            _withdrawCommandHandler = withdrawCommandHandler;
        }

        [HttpPost("deposit")]
        [SwaggerOperation(
            Summary = "Deposit money into an account",
            Description = "Deposit funds into the specified account."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<TransactionDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Deposit(string accountNumber, decimal amount)
        {
            try
            {
                var command = new DepositCommand
                {
                    AccountNumber = accountNumber,
                    Amount = amount
                };

                var result = await _depositCommandHandler.HandleAsync(command);

                return Ok(new ApiOkResponse<TransactionDTO>(_mapper.Map<TransactionDTO>(result)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500, "An error occurred while processing the deposit."));
            }
        }

        [HttpPost("withdraw")]
        [SwaggerOperation(
            Summary = "Withdraw money into an account",
            Description = "Withdraw funds into the specified account."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<TransactionDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Withdraw(string accountNumber, decimal amount)
        {
            try
            {
                var command = new WithdrawCommand
                {
                    AccountNumber = accountNumber,
                    Amount = amount
                };

                var result = await _withdrawCommandHandler.HandleAsync(command);

                return Ok(new ApiOkResponse<TransactionDTO>(_mapper.Map<TransactionDTO>(result)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500, "An error occurred while processing the withdraw."));
            }
        }
    }
}
