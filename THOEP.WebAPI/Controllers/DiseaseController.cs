using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;

namespace THOEP.WebAPI.Controllers
{
    [Route("api")]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;
        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        /// <summary>
        /// Get diseases by healthInfoId.
        /// </summary>
        [Authorize(Policy = "ApiUser")]
        [HttpGet("diseases/healthInfo/{healthInfoId}")]
        public IActionResult GetDiseases(int healthInfoId)
        {
            return Ok(_diseaseService.GetDiseases(healthInfoId));
        }

        /// <summary>
        /// Get diseases by Id.
        /// </summary>
        [HttpGet("diseases/{diseaseId}")]
        public IActionResult GetDiseasesById(int diseaseId)
        {
            return Ok(_diseaseService.GetDiseaseById(diseaseId));
        }
        /// <summary>
        /// Add new disease.
        /// </summary>
        [Authorize(Policy = "ApiUser")]
        [HttpPost("diseases")]
        public IActionResult AddDisease([FromBody]DiseaseDto diseaseDto)
        {
            if (ModelState.IsValid)
            {
                _diseaseService.AddDisease(diseaseDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }
        /// <summary>
        /// Edit disease.
        /// </summary>
        [Authorize(Policy = "ApiUser")]
        [HttpPut("diseases")]
        public IActionResult EditDisease([FromBody]DiseaseDto diseaseDto)
        {
            if (ModelState.IsValid)
            {
                _diseaseService.EditDisease(diseaseDto);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete disease by Id.
        /// </summary>
        [Authorize(Policy = "ApiUser")]
        [HttpDelete("diseases/{diseaseId}")]
        public IActionResult DeleteDisease(int diseaseId)
        {
            return Ok(_diseaseService.DeleteDisease(diseaseId));
        }
    }
}