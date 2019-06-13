using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeSupply.Models;

namespace OfficeSupply.Controllers
{
    /// <summary>
    /// Item management controller
    /// </summary>
    public class ItemController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View(db.Items.ToList());
        }

        // GET: /Home/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList( db.Categories.ToList().Select(
                x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }
            ), "Value", "Text");
            return View();
        }

        // GET: /Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "Id,CategoryId,Name,Variety")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Category = db.Categories.Find(item.CategoryId);

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: /Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Categories = new SelectList(db.Categories.ToList().Select(
                x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }
            ), "Value", "Text");

            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "Id,CategoryId,Name,Variety")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Category = db.Categories.Find(item.CategoryId);

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: /Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
