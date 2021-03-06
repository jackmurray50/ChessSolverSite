using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using chess_solver_site.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chess_solver_site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger _logger;
        public AccountController(ILogger<AccountController> logger) {
            _logger = logger;
        }

        // GET: api/<AccountController>/{id}
        [HttpGet("id/{id}")]
        [Produces("application/json")]
        public IActionResult GetById(int id)
        {
            try
            {
                AccountViewModel vm = new AccountViewModel();
                vm.Id = id;
                vm.GetById();
                return Ok(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut]
        public IActionResult Put(AccountViewModel viewmodel)
        {
            try
            {
                viewmodel.Add();

                return Ok(viewmodel.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
