using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THOEP.DAL.DbContext;
using THOEP.DAL.Models;

namespace THOEP.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly Context _appDbContext;

        public DashboardController(UserManager<AppUser> userManager, Context appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// User dashboard.
        /// </summary>
        // GET api/dashboard/home
        [Authorize(Policy = "ApiUser")]
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var user = await _appDbContext.Users.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                user.Identity.FirstName,
                user.Identity.LastName,
                user.Location,
                user.Locale
            });
        }
        /// <summary>
        /// Admin dashboard.
        /// </summary>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> HomeAdmin()
        {
            var userId = _caller.Claims.Single(c => c.Type == "password");
            var user = await _appDbContext.Users.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

            return new OkObjectResult(new
            {
                Message = "This is admin!",
                user.Identity.FirstName,
                user.Identity.LastName,
                user.Location,
                user.Locale
            });
        }
    }
}