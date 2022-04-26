using JobOfferWebSite_V1._1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int jobId)
        {
            var job = db.Jobs.Find(jobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }


        //Get : Apply
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }

        //Post : Apply
        //I can get all data , except "Message"
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId(); // regestered user
            var JobId = (int)Session["JobId"]; // 

            // التأكد من ان نفس اليوزر مقدمش على نفس الوظيفة قبل كده
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                //عمل أوبجكت وحفظ البيانات فيه
                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                //حفظ البيانات فى الداتابيز
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تم التقدم إلى الوظيفة بنجاح";
            }
            else
            {
                ViewBag.Result = "المعذرة ، لقد سبق وتقدمت إلى نفس الوظيفة بالفعل !";
            }

            return View();
        }

        //get jobs for a user
        [Authorize]
        public ActionResult GetJobsByUser() 
        {
            var UserId = User.Identity.GetUserId();
            var j= db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(j.ToList());
        }

        //details about job
        public ActionResult DetailsAboutJob(int id) {
            var job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        // GET: ApplyForJob/Edit/5
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: ApplyForJob/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }


        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            var j = db.ApplyForJobs.Find(job.Id);
            if (j != null)
            {
                db.ApplyForJobs.Remove(j);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);

        }

        //get jobs for one publisher
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();

            var jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == app.UserId
                       select app;

            var grouped = from j in jobs
                          group j by j.jobs.JobTitle
                          into gr
                          select new JobsViewModel {
                              JobTitle = gr.Key,
                              items = gr
                          };

            return View(grouped.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchBar)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchBar)
            || a.JobContent.Contains(searchBar)
            || a.Categories.Category.Contains(searchBar)
            || a.Categories.Description.Contains(searchBar)).ToList();
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        //get
        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        //post
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            // تعريف الرسالة والمرسل
            MailMessage mail = new MailMessage();
            var loginInfo = new NetworkCredential("muhammadaly1996@gmail.com", "myMasH16");

            //محتوى الرسالة
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("muhammadaly1996@gmail.com"));
            mail.IsBodyHtml = true;
            mail.Subject = contact.Subject;
            string body = "اسم المرسل : " + contact.Name + "<br>" +
                "بريد المرسل : " + contact.Email + "<br>" +
                "عنوان الرسالة : " + contact.Subject + "<br>" +
                "نص الرسالة : " + contact.Message + "<br>" +
                "رقم تليفون المرسل : " + contact.PhoneNumber + "<br>" +
                "دولة المرسل : <b>" + contact.Country + "</b>";
            mail.Body = body;
            //mail.Body = contact.Message;

            //لا نستطيع إرسال الإيميل الا من خلال ال SmtpClient
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465); //  , port // 587
            smtpClient.EnableSsl = true; //safety
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }
    }
}