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
    public class VendorSupplyItemController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: /VendorSupplyItem/
        public ActionResult Index()
        {
            ViewBag.Items = db.Items.ToList();
            ViewBag.Vendors = db.Vendors.ToList();
            return View(db.VendorSupplyItems.ToList());
        }

        // GET: /VendorSupplyItem/Create
        public ActionResult Create()
        {
            ViewBag.Items = new SelectList( db.Items.ToList().Select(
                x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }
            ), "Value", "Text");

            ViewBag.Vendors = new SelectList(db.Vendors.ToList().Select(
                x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }
            ), "Value", "Text");

            return View();
        }

        // GET: /VendorSupplyItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorSupplyItem item = db.VendorSupplyItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /VendorSupplyItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "Id,ItemId,VendorId")] VendorSupplyItem item)
        {
            if (ModelState.IsValid)
            {
                item.Item = db.Items.Find(item.ItemId);
                item.Vendor = db.Vendors.Find(item.VendorId);

                if(IsExistItemVendor(item.ItemId, item.VendorId, null))
                {
                    ModelState.AddModelError("ItemId", "Already exist for Item and Vendor!");

                    ViewBag.Items = new SelectList(db.Items.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");

                    ViewBag.Vendors = new SelectList(db.Vendors.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");

                    return View(item);
                }

                db.VendorSupplyItems.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // check existence of Item+Vendor
        public bool IsExistItemVendor(int? ItemId, int? VendorId, int? Id)
        {
            if(Id == null)
                return db.VendorSupplyItems.Where(x => (x.ItemId == ItemId) && (x.VendorId == VendorId)).Count() > 0;
            return db.VendorSupplyItems.Where(x => (x.ItemId == ItemId) && (x.VendorId == VendorId) && (x.Id != Id)).Count() > 0;
        }

        // GET: /VendorSupplyItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Items = new SelectList(db.Items.ToList().Select(
                x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }
            ), "Value", "Text");

            ViewBag.Vendors = new SelectList(db.Vendors.ToList().Select(
                x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }
            ), "Value", "Text");

            VendorSupplyItem item = db.VendorSupplyItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /VendorSupplyItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "Id,ItemId,VendorId")] VendorSupplyItem item)
        {
            if (ModelState.IsValid)
            {
                item.Item = db.Items.Find(item.ItemId);
                item.Vendor = db.Vendors.Find(item.VendorId);

                if (IsExistItemVendor(item.ItemId, item.VendorId, item.Id))
                {
                    ModelState.AddModelError("ItemId", "Already exist for Item and Vendor!");

                    ViewBag.Items = new SelectList(db.Items.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");

                    ViewBag.Vendors = new SelectList(db.Vendors.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");

                    return View(item);
                }

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: /VendorSupplyItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorSupplyItem item = db.VendorSupplyItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /VendorSupplyItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendorSupplyItem item = db.VendorSupplyItems.Find(id);
            db.VendorSupplyItems.Remove(item);
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
