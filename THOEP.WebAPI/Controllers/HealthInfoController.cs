using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;
using THOEP.WebAPI.ViewModels;

namespace THOEP.WebAPI.Controllers
{
    [Route("api")]
    public class HealthInfoController : ControllerBase
    {
        private readonly IHealthInfoService _healthInfoService;
        private readonly IPatientService _patientService;

        public HealthInfoController(IHealthInfoService healthInfoService, IPatientService patientService)
        {
            _healthInfoService = healthInfoService;
            _patientService = patientService;
        }

        [HttpGet("healthInfos/patient/{patientId}")]
        public IActionResult GetHealthInfos(int patientId)
        {
            return Ok(_healthInfoService.GetHealthInfo(patientId));
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("healthInfos-encoded/patient/{patientId}")]
        public IActionResult GetHealthInfosEncoded(int patientId)
        {
            HealthInfoViewModel model = new HealthInfoViewModel
            {
                Patient = _patientService.GetPatientByIdEncoded(patientId),
                HealthInfo = _healthInfoService.GetHealthInfoEncoded(patientId)
            };

            return Ok(model);
        }

        [HttpGet("healthInfo/{healthInfoId}")]
        public IActionResult GetHealthInfoById(int healthInfoId)
        {
            return Ok(_healthInfoService.GetHealthInfoById(healthInfoId));
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("healthInfo-encoded/{healthInfoId}")]
        public IActionResult GetHealthInfoByIdEncoded(int healthInfoId)
        {
            return Ok(_healthInfoService.GetHealthInfoByIdEncoded(healthInfoId));
        }

        [Authorize(Policy = "ApiUser")]
        [HttpPost("healthInfo")]
        public IActionResult AddHealthInfo([FromBody]HealthInfoViewModelDto healthInfoDto)
        {
            if (ModelState.IsValid)
            {
                healthInfoDto.Time = DateTime.Now;
                _healthInfoService.AddHealthInfo(healthInfoDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        [Authorize(Policy = "ApiUser")]
        [HttpPut("healthInfo")]
        public IActionResult EditHealthInfo([FromBody]HealthInfoViewModelDto healthInfoDto)
        {
            if (ModelState.IsValid)
            {
                healthInfoDto.Time = DateTime.Now;
                _healthInfoService.EditHealthInfo(healthInfoDto);
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Policy = "ApiUser")]
        [HttpDelete("healthInfo/{healthInfoId}")]
        public IActionResult DeleteHealthInfo(int healthInfoId)
        {
            return Ok(_healthInfoService.DeleteHealthInfo(healthInfoId));
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("healthInfo/average/{patientId}")]
        public IActionResult GetAverageHealthMetrics(int patientId)
        {
            HealthInfoMetricsViewModel model = new HealthInfoMetricsViewModel
            {
                AverageHeartRate = _healthInfoService.GetAverageHeartRate(patientId),
                AverageBloodPressure = _healthInfoService.GetAverageBloodPressure(patientId),
                AverageTemperature = _healthInfoService.GetAverageTemperature(patientId),
                AverageWeight = _healthInfoService.GetAverageWeight(patientId)
            };
            return Ok(model);
        }

        [Authorize(Policy = "ApiUser")]
        [HttpGet("healthInfo/{healthInfoId}/checkpatient/{patientId}")]
        public IActionResult CheckPatient(int patientId, int healthInfoId)
        {
            return Ok(_healthInfoService.CheckHealthInfo(patientId, healthInfoId));
        }
    }
}