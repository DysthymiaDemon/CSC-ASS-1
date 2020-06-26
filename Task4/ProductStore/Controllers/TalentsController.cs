using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ProductStore.Controllers
{
    [RequireHttps]
    public class TalentsController : ApiController
    {
        static readonly ITalentRepository _repository = new TalentRepository();

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/v1/talents")]
        public IEnumerable<Talent> GetAllTalentsFromRepository()
        {
            try
            {
                return _repository.GetAll();
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Route("api/v1/talents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent item = _repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
    }
}