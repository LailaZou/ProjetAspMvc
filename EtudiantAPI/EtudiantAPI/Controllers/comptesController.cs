using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EtudiantAPI.Models;

namespace EtudiantAPI.Controllers
{
    public class comptesController : ApiController
    {
        private Projet2Entities db = new Projet2Entities();

        // GET: api/comptes
        public IQueryable<compte> Getcomptes()
        {
            return db.comptes;
        }

        // GET: api/comptes/5
        [ResponseType(typeof(compte))]
        public IHttpActionResult Getcompte(string id)
        {
            compte compte = db.comptes.Find(id);
            if (compte == null)
            {
                return NotFound();
            }

            return Ok(compte);
        }

        // PUT: api/comptes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcompte(string id, compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compte.cne)
            {
                return BadRequest();
            }

            db.Entry(compte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!compteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/comptes
        [ResponseType(typeof(compte))]
        public IHttpActionResult Postcompte(compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.comptes.Add(compte);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (compteExists(compte.cne))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = compte.cne }, compte);
        }

        // DELETE: api/comptes/5
        [ResponseType(typeof(compte))]
        public IHttpActionResult Deletecompte(string id)
        {
            compte compte = db.comptes.Find(id);
            if (compte == null)
            {
                return NotFound();
            }

            db.comptes.Remove(compte);
            db.SaveChanges();

            return Ok(compte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool compteExists(string id)
        {
            return db.comptes.Count(e => e.cne == id) > 0;
        }

        // DELETE: api/comptes/5
        [ResponseType(typeof(compte))]
        public IHttpActionResult authenfication(string cne , string cin , string mdp )
        {
            
            if (compteExists(cne))
            {
                compte compte = db.comptes.Find(cne);
                return Ok(compte);
            }
            return NotFound();
        }
    }
}