﻿using Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamsungStars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "chana", "efrat" };
        }

        [HttpPost("login")]
        public async Task<ActionResult> Post([FromBody] Entities.LoginRequest request)
        {
            User res = await _userService.login(request);
            if (res == null)
                return NotFound();
            else
                return Ok(res);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            User user =await  _userService.findById(id);
            if (user == null)
                return NotFound();
            else
                return Ok(user);

        }

        [HttpPost("checkPassword")]
        public IActionResult Post([FromBody] string passward)
        {
            int res = _userService.checkPassword(passward);
            string rank = res switch
            {
                0 => "חלשה מאד",
                1 => "חלשה",
                2 => "בינונית",
                3 => "חזקה",
                4 => "חזקה מאד",
                _ => "לא ידוע"
            };
            return Ok(rank);
        }


        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            int password = _userService.checkPassword(user.Password);
            if (password < 2)
                return BadRequest("Password is not strong enough");
            Console.WriteLine(user+"contr");
            User res = await _userService.Register(user);
            if (res == null)
                return NotFound();
            else
                return Ok(res);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            if (user.Password != null)
            {
                int passward = _userService.checkPassword(user.Password);
                if (passward < 2)
                    return BadRequest("Password is not strong enough");
            }
            
            try
            {
              User res= await _userService.update(id,user);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
