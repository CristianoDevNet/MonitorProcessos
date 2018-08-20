using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FortesAPI.Controllers
{
    public class OccurrenceController : ApiController
    {
        // GET: api/Occurrence
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Occurrence/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Occurrence
        public IHttpActionResult Post([FromBody]dynamic value)
        {
            using (testeftEntities db = new testeftEntities())
            {
                occurrences o = new occurrences();

                o.station_id = value.station_id;

                o.occurred_when = value.occurred_when;
                
                foreach (var proc in value.proc)
                {
                    processes p = new processes();

                    p.name = proc;

                    o.processes.Add(p);
                }

                db.occurrences.Add(o);
                
                db.SaveChanges();

                return Ok(o.id);
            }
        }

        // PUT: api/Occurrence/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Occurrence/5
        //public void Delete(int id)
        //{
        //}
    }
}
