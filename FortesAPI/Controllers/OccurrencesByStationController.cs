using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FortesAPI.Controllers
{
    public class OccurrencesByStationController : ApiController
    {
        // GET: api/OccurrencesByStation
        //public IHttpActionResult Get()
        //{
        //    return NotFound();
        //}

        // GET: api/OccurrencesByStation/5
        public IHttpActionResult Get(int id)
        {
            using (testeftEntities db = new testeftEntities())
            {
                var occurrences = db.occurrences.Where(o => o.station_id == id).Select(o => new
                {
                    o.id,
                    o.occurred_when
                }).ToList();

                if (occurrences.Count == 0)
                {
                    return NotFound();
                }

                //var lastOccurrence = db.occurrences.Where(c => c.station_id == id).ToList().LastOrDefault().processes;

                return Ok(occurrences);
            }
        }

        // POST: api/OccurrencesByStation
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/OccurrencesByStation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OccurrencesByStation/5
        public void Delete(int id)
        {
        }
    }
}
