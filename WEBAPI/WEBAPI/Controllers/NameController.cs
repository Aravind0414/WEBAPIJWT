using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;
        

        public NameController(IJWTAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        // GET: api/Name
        
        [HttpGet]
        [Route("api/Name")]
        public IEnumerable<string> Get()
        {
            return new string[] { "New York", "New Jersey" };
        }

        // GET: api/Name/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "New Jersey";
        }
       
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(string Username, string Password)
        {
            var token = jWTAuthenticationManager.Authenticate(Username, Password);
            if (token == null)
                return Ok(new { Token = "" });

            return Ok(new { Token = token });
        } 
        
    }
}
