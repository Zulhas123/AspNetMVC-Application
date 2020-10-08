using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rollin.Models;

namespace Rollin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderDetailsController : Controller
    {
        private SalesInvoicesEntities db = new SalesInvoicesEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Customer).Include(o => o.Vehicle);
            return View(orderDetails.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,VehicleId,CustomerID,OrderDate,QTY,TotalPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderDetail.CustomerID);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName", orderDetail.VehicleId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderDetail.CustomerID);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName", orderDetail.VehicleId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,VehicleId,CustomerID,OrderDate,QTY,TotalPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", orderDetail.CustomerID);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "VehicleName", orderDetail.VehicleId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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


        public ActionResult getRelatedGuest( int? CustomerId, int? OrderId)
        {
            var customerVehicleOrder = new CustomerVehicleOrder();
            customerVehicleOrder.Customers = db.Customers.ToList();

            if (CustomerId == null)
            {
                if (Session["CustomerId"] != null)
                {
                    CustomerId = Convert.ToInt32(Session["CustomerId"].ToString());
                }
            }

            if (CustomerId != null)
            {

                Session["CustomerId"] = CustomerId;
                customerVehicleOrder.OrderDetails = db.OrderDetails.Where(w => w.CustomerID == CustomerId.Value).ToList();


            }

            if (OrderId != null)
            {

                customerVehicleOrder.Vehicles = db.Vehicles.Where(w => w.VehicleId == OrderId.Value).ToList();

            }

            return View(customerVehicleOrder);
        }
    }
}
