using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using THOEP.DAL.DbContext;
using THOEP.DAL.Models;
using THOEP.Services.Helpers;
using THOEP.WebAPI.ViewModels;

namespace THOEP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly Context _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper, Context appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// User Registration.
        /// </summary>
        // POST api/account
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            //checking for model registration
            if (!ModelState.IsValid)
            {
                //exception throw if model is not valid
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);
            //creating new user to the system
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            //adding new data to database
            await _appDbContext.Users.AddAsync(new User { IdentityId = userIdentity.Id, Location = model.Location });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}