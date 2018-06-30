using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FifaWorldCup2018.Models;

namespace FifaWorldCup2018.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create(int groupId)
        {
            ViewBag.Teams = db.Teams.Where(t => t.Group.GroupID == groupId);
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                // fali nam grupa ovde
                game.HomeTeam = db.Teams.Find(game.HomeTeam.TeamID);
                game.AwayTeam = db.Teams.Find(game.AwayTeam.TeamID);

                // hocemo da napravimo skor
                // treba da se prebaci na neko lepse mesto

                Team winningTeam = game.GetWinningTeam();
                // mogla bi jos logika da se sredi, npr da score klasa ima konstruktor koji
                // prihvata utakmicu i tim i automatski pravi rezultat (0,1 ili 3 poena)
                if (winningTeam != null)
                {
                    var score = new Score()
                    {
                        Game = game,
                        Points = 3,
                        Team = winningTeam
                    };

                    db.Scores.Add(score);
                }
                else
                {
                    var scores = new List<Score>();

                    scores.Add(new Score()
                    {
                        Game = game,
                        Points = 1,
                        Team = game.HomeTeam
                    });

                    scores.Add(new Score()
                    {
                        Game = game,
                        Points = 1,
                        Team = game.AwayTeam
                    });

                    db.Scores.AddRange(scores);
                }

                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,HomeGoals,AwayGoals")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
