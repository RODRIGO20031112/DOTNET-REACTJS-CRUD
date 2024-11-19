using Microsoft.AspNetCore.Mvc;

namespace rodrigo_server 
{
    [ApiController]

    [Tags("Insurance Card")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            try
            {
                var addedUser = await _userService.AddAsync(user);

                var userDto = new UserDto
                {
                    Id = addedUser.Id,
                    Name = addedUser.Name,
                    Email = addedUser.Email,
                    Phone = addedUser.Phone,
                    CreatedAt = addedUser.CreatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (EmptyNameException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (EmptyEmailException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            try
            {
                await _userService.UpdateAsync(user, id);
                return Ok(new { message = $"Usuário com ID {id} atualizado com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (EmptyNameException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (EmptyEmailException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok(new { message = $"Usuário com ID {id} deletado com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao processar a solicitação.", details = ex.Message });
            }
        }
    }
}