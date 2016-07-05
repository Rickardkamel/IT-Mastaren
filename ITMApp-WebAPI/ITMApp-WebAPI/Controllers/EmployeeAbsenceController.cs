using System.Collections.Generic;
using System.Web.Http;
using BusinessLogic.DataHandler;
using Contracts;

namespace ITMApp_WebAPI.Controllers
{
    //[Authorize]
    public class EmployeeAbsenceController : ApiController
    {
        private EmployeeAbsenceHandler _employeeAbsenceHandler = new EmployeeAbsenceHandler();

        public List<EmployeeAbsenceModel> Get()
        {
            return _employeeAbsenceHandler.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var employeeAbsence = _employeeAbsenceHandler.Get(id);

            if (employeeAbsence == null) return NotFound();

            return Ok();
        }

        public IHttpActionResult Post(EmployeeAbsenceModel employeeAbsence)
        {
            if (employeeAbsence.EndDate != null) employeeAbsence.EndDate = employeeAbsence.EndDate.Value.ToLocalTime();
            employeeAbsence.StartDate = employeeAbsence.StartDate.ToLocalTime();

            if (!ModelState.IsValid) return BadRequest();

            if (!_employeeAbsenceHandler.Post(employeeAbsence)) return BadRequest("Incorrect Datainput");

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_employeeAbsenceHandler.Update(id)) return BadRequest();

            return Ok();
        }
    }
}
