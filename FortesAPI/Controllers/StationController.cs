using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FortesAPI.Controllers
{
    public class StationController : ApiController
    {
        // GET: api/Station
        public IHttpActionResult Get()
        {
            using (testeftEntities db = new testeftEntities())
            {
                dynamic allStations = db.stations.Select(s => new
                {
                    s.id,
                    s.company_name,
                    s.cnpj,
                    s.is_active
                }).ToList();

                if (allStations.Count == 0)
                {
                    return NotFound();
                }

                return Ok(allStations);
            }
        }

        // GET: api/Station/5
        public IHttpActionResult Get(int id)
        {
            using (testeftEntities db = new testeftEntities())
            {
                dynamic oneStation = db.stations.Where(s => s.id == id).Select(s => new
                {
                    s.id,
                    s.company_name,
                    s.cnpj,
                    s.is_active
                }).FirstOrDefault();

                if (oneStation == null)
                {
                    return NotFound();
                }

                return Ok(oneStation);
            }
        }

        // POST: api/Station
        public IHttpActionResult Post([FromBody]dynamic value)
        {
            using (testeftEntities db = new testeftEntities())
            {
                stations s = new stations();

                s.cnpj = value.cnpj;

                s.company_name = value.company_name;

                s.is_active = true;

                db.stations.Add(s);

                db.SaveChanges();
                                             
                return Ok(s.id);
            }
        }

        // PUT: api/Station/5
        public IHttpActionResult Put(int id, [FromBody]dynamic value)
        {
            using (testeftEntities db = new testeftEntities())
            {
                stations s = db.stations.Find(id);

                s.is_active = value.is_active;

                db.SaveChanges();

                return Ok();
            }
        }

        // DELETE: api/Station/5
        public IHttpActionResult Delete(int id)
        {
            using (testeftEntities db = new testeftEntities())
            {
                db.stations.Remove(db.stations.Find(id));

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
