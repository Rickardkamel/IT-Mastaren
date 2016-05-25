using System.Collections.Generic;
using System.Web.Http;
using BusinessLogic.DataHandler;
using Contracts;

namespace ITMApp_WebAPI.Controllers
{
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

            return Ok();
        }

        public IHttpActionResult Post(LunchModel lunch)
        {
            lunch.LunchTime = lunch.LunchTime.ToLocalTime();
            if (!ModelState.IsValid) return BadRequest();

            if (!_lunchHandler.Post(lunch)) return BadRequest("Incorrect Datainput");

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_lunchHandler.Update(id)) return BadRequest();

            return Ok();
        }

    }
}
