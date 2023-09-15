using AutoMapper;
using Bank.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Response;
using WebAPI.Repository.Transaction.Command;
using WebAPI.Repository;

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
        TransferCommandHandler _transferCommandHandler;
        public TransactionController(
            IMapper mapper,
            IRepositoryWrapper repository,
            DepositCommandHandler depositCommandHandler,
            WithdrawCommandHandler withdrawCommandHandler,
            TransferCommandHandler transferCommandHandler)
        {
            _mapper = mapper;
            _depositCommandHandler = depositCommandHandler;
            _withdrawCommandHandler = withdrawCommandHandler;
            _transferCommandHandler = transferCommandHandler;
            _repository = repository;
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

        [HttpPost("balance")]
        [SwaggerOperation(
            Summary = "Balance Inquiry into an account",
            Description = "Withdraw funds into the specified account."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<decimal>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Balance(string accountNumber)
        {
            try
            {
                var result = await _repository.Account.GetAccountBalance(accountNumber);

                return Ok(new ApiOkResponse<decimal>(result));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500, "An error occurred while processing the balance inquiry."));
            }
        }

        [HttpPost("transfer")]
        [SwaggerOperation(
            Summary = "Balance Inquiry into an account",
            Description = "Withdraw funds into the specified account."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<decimal>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Transfer(string sourceAccountNumber,string destinationAccountNumber, decimal amount)
        {
            try
            {
                var command = new TransferCommand
                {
                    SourceAccountNumber = sourceAccountNumber,
                    DestinationAccountNumber = destinationAccountNumber,
                    Amount = amount
                };

                var result = await _transferCommandHandler.HandleAsync(command);

                return Ok(new ApiOkResponse<TransactionDTO>(_mapper.Map<TransactionDTO>(result)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500, "An error occurred while processing the transfer."));
            }
        }
    }
}
