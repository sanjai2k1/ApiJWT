﻿using ApiJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiJWT.Controllers
{

    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;

        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository,JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
            };
            return Created("Success",_repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _repository.GetByEmail(dto.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new
            {
                message = "success"

            });

        }

        [HttpGet("user")]

        public IActionResult User()
        {
            var jwt = Request.Cookies["jwt"];
            var token  = _jwtService.Verify(jwt);

            int userId = int.Parse( token.Issuer);

            var user = _repository.GetById(userId);

            return Ok(user);
        }

        [HttpPost("Logout")]

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
