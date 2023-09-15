using AutoMapper;
using Bank.Common;
using Bank.Common.Parameters;
using FluentValidation.Results;
using Helper.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Response;
using WebAPI.Validators;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IMapper _mapper;
        IRepositoryWrapper _repository;
        public AccountController(
            IMapper mapper,
            IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add Account",
            Description = "Create and saves an Account data"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<AccountDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] AccountDTO value)
        {
            try
            {
                ValidationResult response = new ValidationResult();
                response = new AccountValidator(_repository.GetContext()).Validate(value);
                if (!response.IsValid)
                {
                    response.AddToModelState(ModelState);
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }
                _repository.Account.Create(_mapper.Map<Account>(value));

                await _repository.SaveAsync();
                return Ok(new ApiOkResponse<AccountDTO>(value));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500));
            }
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "List Account",
            Description = "Returns list of accounts",
            OperationId = "accountGET"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<List<AccountDTO>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Get([FromQuery]AccountParameters parameter)
        {
            try
            {
                var list = await _repository.Account.GetAll(parameter);
                if (list == null)
                    return NoContent();
                else
                {
                    var metadata = new PageMetadata
                    {
                        TotalCount = list.TotalCount,
                        PageSize = list.PageSize,
                        CurrentPage = list.CurrentPage,
                        TotalPages = list.TotalPages,
                        HasNext = list.HasNext,
                        HasPrevious = list.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(new ApiOkResponse(_mapper.Map<List<AccountDTO>>(list)));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500));
            }
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(
            Summary = "Get Account by Id",
            Description = " Retrieve account details"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<AccountDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByID(string Id)
        {
            try
            {
                var account = await _repository.Account.GetById(Guid.Parse(Id));
                if (account == null) return NotFound(new ApiResponse(404));

                return Ok(new ApiOkResponse<AccountDTO>(_mapper.Map<AccountDTO>(account)));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500));
            }
        }

        [HttpPut("{Id}")]
        [SwaggerOperation(
            Summary = "Update Account Information",
            Description = "Modify an Account's information data and saves changes to the database"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiOkResponse<AccountDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> Put(string Id, [FromBody] AccountDTO value)
        {
            if (Guid.Parse(Id) != value.Id) return BadRequest(new ApiResponse(400));
            try
            {
                var validation = new AccountValidator(_repository.GetContext()).Validate(value);
                if (!validation.IsValid)
                {
                    validation.AddToModelState(ModelState);
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }
                _repository.Account.Update(_mapper.Map<Account>(value));
                await _repository.SaveAsync();
                return Ok(new ApiOkResponse<AccountDTO>(value));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500));

            }
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Delete Account",
            Description = "Removes an Account based on Id and saves changes to the database"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> Delete(string Id)
        {
            try
            {
                var account = await _repository.Account.GetById(Guid.Parse(Id));
                if (account == null) return NotFound(new ApiResponse(404));

                _repository.Account.Delete(account);

                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse(500));
            }
        }
    }
}
