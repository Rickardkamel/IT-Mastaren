using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using BusinessLogic.DataHandler;
using Contracts;
using Newtonsoft.Json.Linq;

namespace ITMApp_WebAPI.Controllers
{
    //[Authorize]
    //[RequireHttps]
    public class LunchController : ApiController
    {

        private LunchHandler _lunchHandler = new LunchHandler();

        public List<LunchModel> Get()
        {
            return _lunchHandler.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var lunch = _lunchHandler.Get(id);

            if (lunch == null) return NotFound();

            return Ok(lunch);
        }

        public IHttpActionResult Post(JObject data)
        {
            var employee = data["employee"]?.ToObject<EmployeeModel>();
            var lunch = data["lunch"]?.ToObject<LunchModel>();
            //lunch.LunchTime = lunch.LunchTime.ToLocalTime();
            if (lunch?.Restaurant == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            if (!_lunchHandler.Post(lunch, employee)) return BadRequest("Incorrect Datainput");

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_lunchHandler.Update(id)) return BadRequest();

            return Ok();
        }

    }
}
