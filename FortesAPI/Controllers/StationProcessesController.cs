using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FortesAPI.Controllers
{
    public class StationProcessesController : ApiController
    {
        //// GET: api/StationProcesses
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/StationProcesses/5
        public IHttpActionResult Get(int id)
        {
            using (testeftEntities db = new testeftEntities())
            {
                //var occurrences = db.occurrences.Where(o => o.station_id == id).Select(o => new
                //{
                //    o.id,
                //    o.occurred_when
                //}).ToList();

                var latestProcesses = db.occurrences
                                        .Where(c => c.station_id == id)
                                        .ToList()
                                        .LastOrDefault()
                                        .processes
                                        .Select(p => new
                                        {
                                            p.id,
                                            p.name
                                        });

                if (latestProcesses.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(latestProcesses);
            }
        }

        //// POST: api/StationProcesses
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/StationProcesses/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/StationProcesses/5
        //public void Delete(int id)
        //{
        //}
    }
}
