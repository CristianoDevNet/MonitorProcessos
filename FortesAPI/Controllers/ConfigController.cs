using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FortesAPI.Controllers
{
    public class ConfigController : ApiController
    {
        // GET: api/Config
        public IHttpActionResult Get()
        {
            using (testeftEntities db = new testeftEntities())
            {
                var c = db.configurations.FirstOrDefault();

                if (c == null)
                {
                    return NotFound();
                }

                return Ok(c);
            }
        }

        //// GET: api/Config/5
        //public IHttpActionResult Get(int id)
        //{
        //    //using (testeftEntities db = new testeftEntities())
        //    //{
        //    //    configurations c = db.configurations.Find(id);

        //    //    if (c == null)
        //    //    {
        //    //        return NotFound();
        //    //    }

        //    //    return Ok(c);
        //    //}

        //    return NotFound();
        //}

        // POST: api/Config
        //public IHttpActionResult Post([FromBody]dynamic value)
        //{
        //    //using (testeftEntities db = new testeftEntities())
        //    //{
        //    //    configurations c = new configurations();

        //    //    c.interval = value.interval;

        //    //    db.configurations.Add(c);

        //    //    db.SaveChanges();

        //    //    return Ok(c.id);
        //    //}

        //    return NotFound();
        //}

        // PUT: api/Config/5
        //public IHttpActionResult Put(int id, [FromBody]dynamic value)
        //{
        //    //using (testeftEntities db = new testeftEntities())
        //    //{
        //    //    configurations s = db.configurations.Find(id);

        //    //    s.interval = value.inerval;

        //    //    db.SaveChanges();

        //    //    return Ok();
        //    //}

        //    return NotFound();
        //}

        //// DELETE: api/Config/5
        //public IHttpActionResult Delete(int id)
        //{
        //    return NotFound();
        //}
    }
}
