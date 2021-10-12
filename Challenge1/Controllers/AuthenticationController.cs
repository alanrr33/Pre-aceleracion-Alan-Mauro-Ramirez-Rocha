using Challenge1.Entities;
using Challenge1.Interfaces;
using Challenge1.ViewModels.Auth.Login;
using Challenge1.ViewModels.Auth.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //sendgrid
        private readonly IMailService _mailService;

        public AuthenticationController(UserManager<User> userManager,SignInManager<User> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }

        //Registro
        [HttpPost]
        [Route("registro")]

        public async Task<IActionResult> Register(RegisterRequestModel model)
        {

            //Reviso si existe
            var userExists = await _userManager.FindByNameAsync(model.Username);

            //Si existe, devolver error
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            //Caso contrario registrar al usuario
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Message = $"User creation failed! Errors: {string.Join(",", result.Errors.Select(x => x.Description))}"
                    });

            }

            if (result.Succeeded)
            {
                await _mailService.SendEmailAsync(model.Email,"Nuevo registro","<h1>Hey, Bienvenida a la api para buscar personajes y peliculas!</h1><p>Te registraste a la hora: "+DateTime.Now+"</p>");
            }

            return Ok(new
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }

        //Login
        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login(LoginRequestViewModel model)
        {
            //Chequear que el usuario exista y la pass sea correcta
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password,false,false);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.Username);
                if (currentUser.IsActive)
                {
                    //Si se cumplen las condiciones, generar token
                    //Devolver token creado    
                    return Ok(await GetToken(currentUser));

                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized,
                    new
                    {
                        Status = "Error",
                        Message = $"El usuario {model.Username} no esta autorizado!"
                    });

        }

        private async Task<LoginResponseViewModel> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            //agregamos todos los roles que tenga el usuario asignado al momento
            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            //generar key
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeysecretaSuperLargadeAUTORIZACIon"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

        }
    }
}
