using System.Collections.Generic;
using System.Web.Http;
using BusinessLogic.DataHandler;
using Contracts;

namespace ITMApp_WebAPI.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeHandler _employeeHandler = new EmployeeHandler();

        [Route("")]
        public List<EmployeeModel> Get()
        {
            return _employeeHandler.GetAll();
        }
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var employee = _employeeHandler.Get(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }
        [Route("{userName}")]
        public IHttpActionResult Get(string userName)
        {
            var employee = _employeeHandler.GetUserName(userName);

            if (employee == null) return NotFound();

            return Ok(employee);
        }
        [Route("")]
        public IHttpActionResult Post(EmployeeModel employee)
        {
            if (!ModelState.IsValid) return BadRequest();

            if(!_employeeHandler.Post(employee)) return BadRequest("Incorrect Datainput");

            return Ok();
        }
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (!_employeeHandler.Delete(id)) return BadRequest();

            return Ok();
        }


    }
}
