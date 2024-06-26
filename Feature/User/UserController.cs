﻿using JWTAuthDotNet7.Models.RequestModels;
using JWTAuthDotNet7.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace JWTAuthDotNet7.Feature.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestModel request)
        {
            string responseMessage = string.Empty;
            try
            {
                responseMessage = await _userService.Save(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(responseMessage);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            RegisterResponeModel responseModel = new();
            try
            {
                responseModel = await _userService.GetUserById(id, accessToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(responseModel);
        }
    }
}
