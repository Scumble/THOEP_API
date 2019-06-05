using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THOEP.Services.DTO;
using THOEP.Services.Interfaces;

namespace THOEP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArduinoController : ControllerBase
    {
        private readonly IPatientCoordinatesService _patientCoordinatesService;
        private SerialPort arduino;

        public ArduinoController(IPatientCoordinatesService patientCoordinatesService)
        {
            _patientCoordinatesService = patientCoordinatesService;
            arduino = new SerialPort();
            arduino.PortName = "COM4";
            arduino.BaudRate = 115200;
        }

        [HttpGet("heartRate")]
        public string HeartRate()
        {
            arduino.Open();
            string res = arduino.ReadLine();
            arduino.Close();
            return res;
        }

        [HttpGet("getCoord/{patientId}")]
        public string[] GetCoordinates(int patientId)
        {
            Random rnd = new Random();
            int r = rnd.Next(1, 6);
            string red = r + "";
            arduino.Open();
            arduino.Write(red);

            System.Threading.Thread.Sleep(1000);
            string s = arduino.ReadLine().Replace("\r", string.Empty);
            string[] s1 = s.Split(",");
            float a1 = float.Parse(s1[1].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            float a2 = float.Parse(s1[0].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            PatientCoordiantesDto patientCoordintesDto = new PatientCoordiantesDto();
            patientCoordintesDto.PatientId = patientId;
            patientCoordintesDto.Longtitude = a1;
            patientCoordintesDto.Latitude = a2;
            _patientCoordinatesService.AddPatientCoordinates(patientCoordintesDto);
            arduino.Close();
            return s1;

        }

    }
}