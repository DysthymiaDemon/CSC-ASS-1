using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Task2.Models;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Task2.Controllers
{
    public class StudentController : ApiController
    {

        static readonly IStudentRepository _repository = new StudentRepository();


        // localhost:58102/api/v1/students
        [HttpGet]
        [Route("api/v1/students")]
        public IEnumerable<Student> GetAllStudentsFromRepository()
        {
            return _repository.GetAll();
        }

        // localhost:58102/api/v1/students/1
        [HttpGet]
        [Route("api/v1/students/{id:int:min(1)}", Name = "getStudentByIdv1")]
        public HttpResponseMessage GetStudentFromRepository(int id)
        {
            Student student = _repository.Get(id);
            if(student == null)
            {
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to process GET request");
            }
            return Request.CreateResponse<Student>(HttpStatusCode.Created, student);
        }

        // localhost:58102/api/v1/students?course=Diploma in Information Technology
        [HttpGet]
        [Route("api/v1/students", Name = "getStudentsByCoursev1")]
        public IEnumerable<Student> GetStudentsByCourse(string course)
        {
            return _repository.GetAll().Where(
        s => string.Equals(s.Course, course, StringComparison.OrdinalIgnoreCase));
        }

        // localhost:58102/api/v1/students
        [HttpPost]
        [Route("api/v1/students")]
        public HttpResponseMessage PostStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                student = _repository.Add(student);
                var response = Request.CreateResponse<Student>(HttpStatusCode.Created, student);

                string uri = Url.Link("getStudentByIdv1", new { id = student.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("api/v1/students/{id:int:min(1)}")]
        public HttpResponseMessage PutStudent(int id, Student student)
        {
            student.Id = id;
            if (!_repository.Update(student))
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to process PUT request");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully updated a student");
        }

        [HttpDelete]
        [Route("api/v1/students/{id:int:min(1)}")]
        public HttpResponseMessage DeleteStudent(int id)
        {
            Student item = _repository.Get(id);
            if (item == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to process DELETE request");
            }
            _repository.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully deleted a student");
        }
    }
}