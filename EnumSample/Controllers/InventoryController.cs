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
    /// Inventory Basic info Management controller
    /// </summary>
    public class InventoryController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: /Inventory/
        public ActionResult Index()
        {
            return View(db.Inventories.ToList());
        }

        // GET: /Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: /Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }        

        // POST: /Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Description")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        // GET: /Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: /Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "Id,Name,Description")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: /Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: /Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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

        /// <summary>
        /// View stock list of current Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: /Inventory/ViewStocks/5
        public ActionResult ViewStocks(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            ViewBag.InventoryName = inventory.Name;

            return View(db.InventoryStocks.Where(x => x.InventoryId == id).ToList());
        }

        /// <summary>
        /// Page of managing stocks : Receive/Release Items
        /// </summary>
        /// <param name="id"></param>
        /// <param name="InOut"></param>
        /// <returns></returns>
        // GET: /Inventory/ReceiveStocks/5
        public ActionResult ManageStocks(int? id, bool InOut)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            ViewBag.Items = new SelectList(db.Items.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");
            ViewBag._InventoryId = inventory.Id.ToString();
            ViewBag.InventoryName = inventory.Name;
            ViewBag.InOut = InOut;

            return View();
        }

        /// <summary>
        /// Main logic action for receipt/release of Items for current Inventory
        ///   In case of Release, if requested amount exceed current stock : fails
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitStocks([Bind(Include = "Id,InventoryId,ItemId,Count")] InventoryLog log)
        {
            if(Request.Params.Get("InOut") == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            bool InOut = Request.Params.Get("InOut").ToString().Equals("True") ? true : false;
            Inventory inventory = db.Inventories.Find(log.InventoryId);
            var stocks = db.InventoryStocks.Where(x => (x.InventoryId == log.InventoryId) && (x.ItemId == log.ItemId));

            // In case of relase : check whether stock amount is enough
            if (!InOut) {
                if(stocks.Count() <= 0 || stocks.First().Count < log.Count)
                {
                    ModelState.AddModelError("Count", "Stock is not enough!");

                    ViewBag.Items = new SelectList(db.Items.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");
                    ViewBag._InventoryId = inventory.Id.ToString();
                    ViewBag.InventoryName = inventory.Name;
                    ViewBag.InOut = InOut;

                    return View("ManageStocks");
                }
            }

            if (ModelState.IsValid)
            {
                log.InOrOut = InOut;
                log.DateTime = DateTime.Now;
                db.InventoryLogs.Add(log);

                if (stocks.Count() <= 0 && InOut)
                {
                    InventoryStock newStock = new InventoryStock();
                    newStock.InventoryId = log.InventoryId;
                    newStock.ItemId = log.ItemId;
                    newStock.Count = log.Count;
                    db.InventoryStocks.Add(newStock);
                }
                else
                {
                    var stock = stocks.First();
                    if (InOut)
                        stock.Count = stock.Count + log.Count;
                    else
                        stock.Count = stock.Count - log.Count;

                    db.Entry(stock).State = EntityState.Modified;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Items = new SelectList(db.Items.ToList().Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }
                    ), "Value", "Text");
            ViewBag._InventoryId = inventory.Id.ToString();
            ViewBag.InventoryName = inventory.Name;
            ViewBag.InOut = InOut;
            return View("ManageStocks");
        }
    }
}
