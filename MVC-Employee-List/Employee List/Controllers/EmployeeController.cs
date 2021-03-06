﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employee_List.Models;
using System.IO;

namespace Employee_List.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            return Json(db.Employees, JsonRequestBehavior.AllowGet);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employeeModel = db.Employees.Find(id);

            ViewBag.id = id;
            return View(employeeModel);
        }      

        public JsonResult GetOne(int? id)
        {
            if(id == null)
            {
                return null;
            }

            Employee employeeModel = db.Employees.Find(id);
            EmployeeDisplay emp = new EmployeeDisplay();

            emp.ID = employeeModel.ID;
            emp.name = employeeModel.name;
            emp.start_date = employeeModel.start_date;
            emp.end_date = employeeModel.end_date;
            emp.title = employeeModel.title;

            byte[] img = employeeModel.image;
            if (img != null)
            {
                string path = byteArrayToImage(img);
                emp.imageString = "data:image/jpeg;base64," + path;
            }

            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,title,start_date,end_date")] Employee employee, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    byte[] img = imageToByteArray(image);

                    if (img != null)
                        employee.image = img;
                    else
                        employee.image = null;
                }
                else
                {
                    employee.image = null;
                }

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.id = id;
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,title,start_date,end_date")] Employee employee, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    byte[] img = imageToByteArray(image);

                    if (img != null)
                        employee.image = img;
                    else
                        employee.image = null;
                }
                else
                {
                    employee.image = null;
                }

                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private byte[] imageToByteArray(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);

                file.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    return array;
                }
            }
            return null;
        }

        private string byteArrayToImage(byte[] byteArrayIn)
        {
            string temp = Convert.ToBase64String(byteArrayIn, 0, byteArrayIn.Length);
            return temp;
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
