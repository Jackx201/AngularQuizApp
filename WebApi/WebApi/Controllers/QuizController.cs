using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class QuizController : ApiController
    {
        [HttpGet]
        [Route("api/Questions")]
        public HttpResponseMessage GetQuestions()
        {
            using (DBModel db = new DBModel())
            {
                var Qns = db.Questions
                    .Select(x => new { QnID = x.QuestionID, Qn = x.Question, ImageName = x.ImageName, x.Option1, x.Option2, x.Option3, x.Option4 })
                    .OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();
                var updated = Qns.AsEnumerable()
                    .Select(x => new
                    {
                        QnID = x.QnID,
                        Qn = x.Qn,
                        ImageName = x.ImageName,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                    }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);
            }
        }

        [HttpPost]
        [Route("api/Answers")]
        public HttpResponseMessage GetAnswers(int[] qIds)
        {
            using (DBModel db = new DBModel())
            {
                var result = db.Questions
                    .AsEnumerable()
                    .Where(y => qIds.Contains(y.QuestionID))
                    .OrderBy(x => { return Array.IndexOf(qIds, x.QuestionID); })
                    .Select(z => z.Answer)
                    .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}
