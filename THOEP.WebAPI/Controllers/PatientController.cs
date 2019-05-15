using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;

namespace THOEP.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPatientCoordinatesService _patientCoordinatesService;
        public PatientController(IPatientService patientService, IPatientCoordinatesService patientCoordinatesService)
        {
            _patientService = patientService;
            _patientCoordinatesService = patientCoordinatesService;
        }

        [HttpGet("patients")]
        public IActionResult GetPatients()
        {
            return Ok(_patientService.GetPatients());
        }

        [HttpGet("patients/{patientId}")]
        public IActionResult GetPatientById(int patientId)
        {
            return Ok(_patientService.GetPatientById(patientId));
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("patient-encoded/{patientId}")]
        public IActionResult GetPatientEncodedById(int patientId)
        {
            return Ok(_patientService.GetPatientByIdEncoded(patientId));
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("patientcoord/{patientId}")]
        public IActionResult GetPatientCoordinates(int patientId)
        {
            return Ok(_patientCoordinatesService.GetPatientCoordinates(patientId));
        }

        //[Authorize(Policy = "ApiUser")]
        //[HttpGet("patients")]
        //public IActionResult GetPatientsEncoded()
        //{
        //    return Ok(_patientService.GetPatientsEncoded());
        //}

        [Authorize(Policy = "ApiUser")]
        [HttpPost("patients")]
        public IActionResult AddPatient([FromBody]PatientDto patientDto)
        {
            if (ModelState.IsValid)
            {
                _patientService.AddPatient(patientDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }
        /// <summary>
        /// Update patient by id.
        /// </summary>
        [Authorize(Policy = "ApiUser")]
        [HttpPut("patients")]
        public IActionResult EditPatient([FromBody]PatientDto patientDto)
        {
            if (ModelState.IsValid)
            {
                _patientService.EditPatient(patientDto);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete patient by id.
        /// </summary>
        [Authorize(Policy = "ApiUser")]
        [HttpDelete("patients/{patientId}")]
        public IActionResult DeletePatient(int patientId)
        {
            return Ok(_patientService.DeletePatient(patientId));
        }
    }
}