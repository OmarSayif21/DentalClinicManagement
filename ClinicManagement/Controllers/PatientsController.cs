using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicManagement.Core;
using ClinicManagement.Core.Models;
using ClinicManagement.Core.ViewModel;
using Microsoft.AspNet.Identity;

namespace ClinicManagement.Controllers
{
    [Authorize(
        Roles = RoleName.DoctorRoleName
            + ","
            + RoleName.AdministratorRoleName
            + ","
            + RoleName.PatientRoleName
    )]
    public class PatientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var viewModel = new PatientDetailViewModel()
            {
                Patient = _unitOfWork.Patients.GetPatient(id),
                Appointments = _unitOfWork.Appointments.GetAppointmentWithPatient(id),
                Attendances = _unitOfWork.Attandences.GetAttendance(id),
                CountAppointments = _unitOfWork.Appointments.CountAppointments(id),
                CountAttendance = _unitOfWork.Attandences.CountAttendances(id),
            };
            return View("Details", viewModel);
        }

        public ActionResult PatientUserDetails(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var patient = _unitOfWork.Patients.GetPatient(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PatientDetailViewModel()
            {
                Patient = patient,
                Appointments = _unitOfWork.Appointments.GetAppointmentWithPatient(id),
                Attendances = _unitOfWork.Attandences.GetAttendance(id),
                CountAppointments = _unitOfWork.Appointments.CountAppointments(id),
                CountAttendance = _unitOfWork.Attandences.CountAttendances(id)
            };

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new PatientFormViewModel
            {
                Cities = _unitOfWork.Cities.GetCities(),
                Heading = "New Patient",
            };
            return View("PatientForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View("PatientForm", viewModel);
            }

            var patient = new Patient
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                DateTime = DateTime.Now,
                BirthDate = viewModel.GetBirthDate(),
                Height = viewModel.Height,
                Weight = viewModel.Weight,
                CityId = viewModel.City,
                Sex = viewModel.Sex,
                Token = ("2024" + _unitOfWork.Patients.GetPatients().Count())
                    .ToString()
                    .PadLeft(7, '0'),
                AspNetUsersID = "Administrator",
            };

            _unitOfWork.Patients.Add(patient);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Patients");

            // TODO: BUG redirect to detail
            //return RedirectToAction("Details", new { id = viewModel.Id });
        }

        [Authorize]
        public ActionResult PatientCreate()
        {
            var viewModel = new PatientFormViewModel
            {
                Cities = _unitOfWork.Cities.GetCities(),
                Heading = "New Patient",
                AspNetUsersID = User.Identity.GetUserId()
            };
            return View("PatientUserCreate", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientCreate(PatientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View("PatientUserCreate", viewModel);
            }

            var patient = new Patient
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                DateTime = DateTime.Now,
                BirthDate = viewModel.GetBirthDate(),
                Height = viewModel.Height,
                Weight = viewModel.Weight,
                CityId = viewModel.City,
                Sex = viewModel.Sex,
                Token = ("2024" + _unitOfWork.Patients.GetPatients().Count())
                    .ToString()
                    .PadLeft(7, '0'),
                AspNetUsersID = viewModel.AspNetUsersID,
            };

            _unitOfWork.Patients.Add(patient);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Patients");

            // TODO: BUG redirect to detail
            //return RedirectToAction("Details", new { id = viewModel.Id });
        }

        public ActionResult Edit(int id)
        {
            var patient = _unitOfWork.Patients.GetPatient(id);

            var viewModel = new PatientFormViewModel
            {
                Heading = "Edit Patient",
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone,
                Date = patient.DateTime,
                //Date = patient.DateTime.ToString("d MMM yyyy"),
                BirthDate = patient.BirthDate.ToString("dd/MM/yyyy"),
                Address = patient.Address,
                Height = patient.Height,
                Weight = patient.Weight,
                Sex = patient.Sex,
                City = patient.CityId,
                Cities = _unitOfWork.Cities.GetCities(),
            };
            return View("PatientForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PatientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View("PatientForm", viewModel);
            }

            var patientInDb = _unitOfWork.Patients.GetPatient(viewModel.Id);
            patientInDb.Id = viewModel.Id;
            patientInDb.Name = viewModel.Name;
            patientInDb.Phone = viewModel.Phone;
            patientInDb.BirthDate = viewModel.GetBirthDate();
            patientInDb.Address = viewModel.Address;
            patientInDb.Height = viewModel.Height;
            patientInDb.Weight = viewModel.Weight;
            patientInDb.Sex = viewModel.Sex;
            patientInDb.CityId = viewModel.City;

            _unitOfWork.Complete();
            return RedirectToAction("Index", "Patients");
        }

        public ActionResult GetPatientByUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var patients = _unitOfWork.Patients.GetPatientz()
                                .Where(p => p.AspNetUsersID == userId)
                                .ToList();

            if (!patients.Any())
            {
                return HttpNotFound("No patients found for this user.");
            }

            return View("GetPatientByUser", patients); // Ensure the view name matches
        }

    }
}
