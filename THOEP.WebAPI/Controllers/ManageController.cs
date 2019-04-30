using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THOEP.DAL.DbContext;
using THOEP.DAL.Models;
using THOEP.Services.Interfaces;

namespace THOEP.WebAPI.Controllers
{
    [Route("api")]
    public class ManageController : Controller
    {
        private readonly Context _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDBManagerService _dBManagerService;
        private readonly IMapper _mapper;
        public ManageController(UserManager<AppUser> userManager, IMapper mapper, Context appDbContext, IDBManagerService dBManagerService)
        {
            _userManager = userManager;
            _dBManagerService = dBManagerService;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Get user by id ̣(for admin).
        /// </summary>
        [Authorize(Policy = "Admin")]
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        /// <summary>
        /// Get all users ̣(for admin).
        /// </summary>
        [Authorize(Policy = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userManager.Users.ToListAsync();
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        /// <summary>
        /// Lock user by Id ̣(for admin).
        /// </summary>
        [HttpPost("lockuser/{id}")]
        public async Task<IActionResult> LockedOut(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            if (model == null)
            {
                return BadRequest();
            }
            model.LockoutEnabled = true;
            await _userManager.SetLockoutEnabledAsync(model, true);
            await _userManager.SetLockoutEndDateAsync(model, DateTime.Today.AddYears(10));
            await _userManager.UpdateAsync(model);

            return new OkObjectResult("Account Lockedout");
        }
        /// <summary>
        /// Delete user by Id ̣(for admin).
        /// </summary>
        [HttpPost("users/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            User user = _appDbContext.Users.SingleOrDefault(x => x.IdentityId == id);
            if (model == null)
            {
                return BadRequest();
            }
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
            await _userManager.DeleteAsync(model);
            return new OkObjectResult("Account Deleted");
        }
        /// <summary>
        /// Unlock user by id ̣(for admin).
        /// </summary>
        [HttpPost("unlockuser/{id}")]
        public async Task<IActionResult> Unlock(string id)
        {

            var model = await _userManager.FindByIdAsync(id);
            if (model == null)
            {
                return BadRequest();
            }
            await _userManager.SetLockoutEndDateAsync(model, null);
            await _userManager.UpdateAsync(model);

            return new OkObjectResult("Account unlocked");
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("backup")]
        public IActionResult BackUpDB()
        {
            _dBManagerService.BackUpDB();
            return Ok();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("restore")]
        public IActionResult RestoreDB()
        {
            _dBManagerService.RestoreDB();
            return Ok();
        }
    }
}