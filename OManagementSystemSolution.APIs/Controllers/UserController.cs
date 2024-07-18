using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OManagementSystemSolution.APIs.DTOs;
using OMS.Core.Entities;
using OMS.Core.Services.Interfaces;

namespace OManagementSystemSolution.APIs.Controllers
{
    
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokennService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokennService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokennService = tokennService;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new User()
            {
                UserName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                

            };
            var result = await _userManager.CreateAsync(user, password: model.Password);

            if (!result.Succeeded)
            {
                //return BadRequest(error:400);
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }
            var returnedUser = new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await _tokennService.CreateTokenAsync(user, _userManager)
            };
            return Ok(returnedUser);
        }

        //Login
        [HttpPost(template: "Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (!result.Succeeded) return Unauthorized();

            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await _tokennService.CreateTokenAsync(user, _userManager)
            }
            );

        }


    }
}
