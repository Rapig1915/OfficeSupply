﻿using System;
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

        // GET: /Item/
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View(db.Items.ToList());
        }

        // GET: /Item/Create
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

        // GET: /Item/Details/5
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

        // POST: /Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "Id,CategoryId,Name,Variety")] Item item, HttpPostedFileBase ItemImage)
        {
            if (ModelState.IsValid)
            {
                item.Category = db.Categories.Find(item.CategoryId);

                if(ItemImage != null)
                {
                    string newFileName = Helpers.Helper.RandomString() + ".jpg";
                    ItemImage.SaveAs(HttpContext.Server.MapPath("~/Images/") + newFileName);
                    item.ItemImage = newFileName;
                }

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: /Item/Edit/5
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

        // POST: /Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "Id,CategoryId,Name,Variety,PrevItemImage")] Item item, HttpPostedFileBase ItemImage)
        {
            if (ModelState.IsValid)
            {
                item.Category = db.Categories.Find(item.CategoryId);

                // Save Image from file input
                if (ItemImage != null)
                {
                    // delete previous image for current item
                    if (item.PrevItemImage != null && item.PrevItemImage != "")
                        if(System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/") + item.PrevItemImage))
                            System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/") + item.PrevItemImage);

                    string newFileName = Helpers.Helper.RandomString() + ".jpg";
                    ItemImage.SaveAs(HttpContext.Server.MapPath("~/Images/") + newFileName);
                    item.ItemImage = newFileName;
                }

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: /Item/Delete/5
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

        // POST: /Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);

            if (item.ItemImage != null && item.ItemImage != "")
                if (System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/") + item.ItemImage))
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/") + item.ItemImage);

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
