using CrudAppWithImages.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudAppWithImages.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ExampleDBEntities db = new ExampleDBEntities();
        public ActionResult Index()
        {
            var data = db.employees.ToList();
            return View(data);
        }
        public ActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Create(employee e)
        {      
            if(ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
                string extension = Path.GetExtension(e.ImageFile.FileName);
                HttpPostedFileBase postedFile = e.ImageFile;
                int length = postedFile.ContentLength;

                if(extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    if(length <= 1000000)
                    {
                        fileName = fileName + extension;
                        e.image_path = "~/images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/images/"),fileName);
                        e.ImageFile.SaveAs(fileName);
                        db.employees.Add(e);
                        int a = db.SaveChanges();
                        if(a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Inserted Successfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Not Inserted !!')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Imaze size should be 1 MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var EmployeeRow = db.employees.Where(model => model.id == id).FirstOrDefault();
            Session["image"] = EmployeeRow.image_path;
            return View(EmployeeRow);

        }

        [HttpPost]
        public ActionResult Edit(employee e)
        {
            if (ModelState.IsValid == true)
            {
                if(e.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(e.ImageFile.FileName);
                    string extension = Path.GetExtension(e.ImageFile.FileName);
                    HttpPostedFileBase postedFile = e.ImageFile;
                    int length = postedFile.ContentLength;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            e.image_path = "~/images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                            e.ImageFile.SaveAs(fileName);
                            db.Entry(e).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data Not Updated !!')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Imaze size should be 1 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }
                }
                else
                {
                    e.image_path = Session["image"].ToString();
                    db.Entry(e).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Not Updated !!')</script>";
                    }
                }
            }
            return View();
        }
    }
}