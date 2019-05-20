using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using reader2reader.Models;
using reader2reader.ViewModels;
using PagedList;
using Microsoft.AspNet.Identity;

namespace reader2reader.Controllers
{
    public class BooksController : Controller
    {
        private BooksContext db = new BooksContext();

        // GET: Books
        public ActionResult Index(string search, int? page)
        {
            var viewModel = new BooksViewModel();
            var books = (IQueryable<Book>)db.Books;

            // Search
            if (!String.IsNullOrEmpty(search))
            {
                books = books.Where(p => p.Title.Contains(search) ||
                                         p.Author.Contains(search) ||
                                         p.Genre.Contains(search));
                viewModel.Search = search;
            }

            // Order
            books = books.OrderBy(x => x.Title);

            // Paging
            int currentPage = (page ?? 1);
            viewModel.Books = books.ToPagedList(currentPage, Constants.BooksPerPage);

            return View(viewModel);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin,User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Genre,Price,ImageURL")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.CreatedDateTime = DateTime.Now;
                book.CreatedBy = User.Identity.GetUserId();
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Genre,Price,ImageURL")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.CreatedDateTime == DateTime.MinValue)
                {
                    book.CreatedDateTime = DateTime.Now;
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
