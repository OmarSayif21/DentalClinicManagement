using System.Linq;
using System.Web.Http;
using AutoMapper;
using ClinicManagement.Core;
using ClinicManagement.Core.Dto;
using ClinicManagement.Core.Models;
using Microsoft.AspNet.Identity;

namespace ClinicManagement.Controllers.Api
{
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetPatients()
        {
            var patientsQuery = _unitOfWork.Patients.GetPatients();
            var patientDto = patientsQuery.ToList().Select(Mapper.Map<Patient, PatientDto>).ToList();
            return Ok(patientDto);
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var patient = _unitOfWork.Patients.GetPatient(id);
            _unitOfWork.Patients.Remove(patient);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("GetPatientsByUser")]
        public IHttpActionResult GetPatientsByUser()
        {
            var userId = User.Identity.GetUserId();
            var patients = _unitOfWork
                .Patients.GetPatientz() // Get the IQueryable<Patient>
                .Where(p => p.AspNetUsersID == userId)
                .Select(p => new
                {
                    p.Id, // Ensure Id is returned
                    p.Token,
                    p.Name,
                    p.Phone,
                    p.Address,
                    CityName = p.Cities.Name,
                })
                .ToList();

            return Ok(patients);
        }

    }
}
