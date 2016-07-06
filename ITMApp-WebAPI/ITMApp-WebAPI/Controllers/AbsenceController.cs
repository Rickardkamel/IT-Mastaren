using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using BusinessLogic.DataHandler;
using Contracts;

namespace ITMApp_WebAPI.Controllers
{
    //[Authorize]
    //[RequireHttps]
    public class AbsenceController : ApiController
    {
        private AbsenceHandler _absenceHandler = new AbsenceHandler();

        public List<AbsenceModel> Get()
        {
            return _absenceHandler.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var absence = _absenceHandler.Get(id);

            if (absence == null) return NotFound();

            return Ok(absence);
        }

        [System.Web.Http.HttpPut]
        public IHttpActionResult Post(AbsenceModel absence)
        {
            if (!ModelState.IsValid) return BadRequest();

            if(!_absenceHandler.Post(absence)) return BadRequest("Incorrect Datainput");

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if(!_absenceHandler.Delete(id)) return BadRequest();

            return Ok();
        }
    }
}
