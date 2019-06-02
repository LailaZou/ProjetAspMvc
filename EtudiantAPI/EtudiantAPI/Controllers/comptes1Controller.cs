using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EtudiantAPI.Models;

namespace EtudiantAPI.Controllers
{
    public class comptes1Controller : Controller
    {
        private Projet2Entities db = new Projet2Entities();

        // GET: comptes1
        public ActionResult Index()
        {
            return View(db.comptes.ToList());
        }

        // GET: comptes1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte compte = db.comptes.Find(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // GET: comptes1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: comptes1/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cne,cin,mdp,nom,prenom,email,date_naissance,lieu_naissance,adresse,code_postal,tel,filiere,option_bac,annee_bac,mention_bac,note_bac,etablissement_bac,academie_bac,intitule_dip,annee_dip,mention_dip,note_dip,etablissement_dip,ville_dip,classement_concours,note_concours,liste_concours")] compte compte)
        {
            if (ModelState.IsValid)
            {
                db.comptes.Add(compte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compte);
        }

        // GET: comptes1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte compte = db.comptes.Find(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: comptes1/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cne,cin,mdp,nom,prenom,email,date_naissance,lieu_naissance,adresse,code_postal,tel,filiere,option_bac,annee_bac,mention_bac,note_bac,etablissement_bac,academie_bac,intitule_dip,annee_dip,mention_dip,note_dip,etablissement_dip,ville_dip,classement_concours,note_concours,liste_concours")] compte compte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compte);
        }

        // GET: comptes1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte compte = db.comptes.Find(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: comptes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            compte compte = db.comptes.Find(id);
            db.comptes.Remove(compte);
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
