using KramarDev.Quiz.DAL.Database.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KramarDev.Quiz.WebAPI.Controllers;

public class AppController(
    IAppCacheService cacheService,
    UserManager<User> userManager,
    TokenService tokenService) : BaseController
{
    readonly IAppCacheService _cacheService = cacheService;
    readonly UserManager<User> _userManager = userManager;
    readonly TokenService _tokenService = tokenService;


    [HttpGet("init-state")]
    public async Task<ActionResult<AppStateModel>> GetInitialStateAsync()
    {
        AppStateModel model = new();

        TopicDto[] topics = await _cacheService.GetTopicsAsync();
        model.Topics = DtoMapper.FromBLL(topics);

        if (UserName != null)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            if (user != null)
            {
                model.User = new UserModel
                {
                    Email = user.Email,
                    Token = await _tokenService.GenerateTokenAsync(user),
                    Login = user.UserName
                };
            }
        }

        return model;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserModel>> Login(LoginModel login)
    {
        var user = await _userManager.FindByNameAsync(login.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
        {
            return Unauthorized();
        }

        return new UserModel
        {
            Email = user.Email,
            Token = await _tokenService.GenerateTokenAsync(user),
            Login = user.UserName
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserModel>> Register(RegisterModel register)
    {
        var user = new User { UserName = register.Username, Email = register.Email };

        var result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        await _userManager.AddToRoleAsync(user, "Member");

        return new UserModel
        {
            Email = user.Email,
            Token = await _tokenService.GenerateTokenAsync(user),
            Login = user.UserName
        };
    }
}
