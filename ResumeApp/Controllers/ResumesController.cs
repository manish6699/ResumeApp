using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ResumeApp.Models.ResumeAppModels;

namespace ResumeApp.Models
{
    public class ResumesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Resumes
        public ActionResult Index()
        {
            var resumes = db.Resumes.Include(r => r.User);
            return View(resumes.ToList());
        }

        // GET: Resumes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // GET: Resumes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resume resume)
        {
            if (ModelState.IsValid)
            {
                resume.UserId = User.Identity.GetUserId();
                db.Resumes.Add(resume);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", resume.UserId);
            return View(resume);
        }

        // GET: Resumes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", resume.UserId);
            return View(resume);
        }

        // POST: Resumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Title,Skills,UserId")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resume).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", resume.UserId);
            return View(resume);
        }

        // GET: Resumes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resume resume = db.Resumes.Find(id);
            db.Resumes.Remove(resume);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
