﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using MusicStore.Models;
using MusicStore.Models.MusicStore;

namespace MusicStore.Controllers.API
{
    [EnableCors("*", "*", "*")]
    public class ArtistsController : ApiController
    {
        private MusicStoreDbContext db = new MusicStoreDbContext();

        // GET: api/Artists
        public IEnumerable<Artist> GetArtists()
        {
            return db.Artists.ToList();
        }

        // GET: api/Artists/5
        [ResponseType(typeof(Artist))]
        public IHttpActionResult GetArtist(int id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // PUT: api/Artists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtist(int id, Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            db.Entry(artist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        [ResponseType(typeof(Artist))]
        public IHttpActionResult PostArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artists.Add(artist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artist.ArtistId }, artist);
        }

        // DELETE: api/Artists/5
        [ResponseType(typeof(Artist))]
        public IHttpActionResult DeleteArtist(int id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return NotFound();
            }

            db.Artists.Remove(artist);
            db.SaveChanges();

            return Ok(artist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtistExists(int id)
        {
            return db.Artists.Count(e => e.ArtistId == id) > 0;
        }
    }
}