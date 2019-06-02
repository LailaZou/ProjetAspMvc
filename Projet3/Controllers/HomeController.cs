using Projet3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using projet1ASPNET.Controllers;
using projet1ASPNET.Models;

namespace Projet3.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeCompte]
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Subscribe()
        {
            return View();
        }

        public ActionResult SaveCompte ([Bind(Include = "cne,cin,mdp,nom,prenom,email,date_naissance,lieu_naissance,adresse,code_postal,tel,filiere,annee_bac,mention_bac,note_bac,etablissement_bac,academie_bac,annee_dip,mention_dip,note_dip,etablissement_dip,ville_dip,option_bac,intitule_dip")] compte1 compte , HttpPostedFileBase photo)
        {
            if (ModelState.IsValid && photo != null)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:54975");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("api/comptes", compte).Result;

                ViewData["error"] = "Erreur d'inscription. Veuillez réessayer!!";
               
               


                if (response.IsSuccessStatusCode)
                {
                    if (photo.ContentLength > 0)
                        try
                        {
                            string path = Path.Combine(Server.MapPath("~/Content/images"),
                                                       Path.GetFileName(compte.cne + ".jpg"));
                            photo.SaveAs(path);
                            ViewData["error"] = "Votre etes bien inscrit, connectez-vous!!";
                        }
                        catch (Exception ex)
                        {
                            ViewData["error"] = "Erreur dd "+ex.ToString();
                            //ViewData["error"] = "Erreur d'inscription. Veuillez réessayer!!";

                        }

                    return View("Authentification");
                }
                return View("Authentification");
            }
            if (photo == null) ViewData["imgError"] = "Ce champ est requis";
            compte.mdp = null;
            return View("Subscribe" , compte);
        }

        [AuthorizeCompte]
        public ActionResult EditCompte([Bind(Include = "cne,cin,nom,prenom,email,mdp,date_naissance,lieu_naissance,adresse,code_postal,tel,filiere,annee_bac,mention_bac,note_bac,etablissement_bac,academie_bac,annee_dip,mention_dip,note_dip,etablissement_dip,ville_dip,option_bac,intitule_dip")] compte1 compte)
        {
            compte.mdp = ((compte1)Session["compte1"]).mdp;
            if (ModelState.IsValid)
            {
                string cne = ((compte1)Session["compte1"]).cne;
                compte.mdp = ((compte1)Session["compte1"]).mdp;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:54975");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("api/comptes/" + cne, compte).Result;
                if (response.IsSuccessStatusCode)
                {
                    Random r = new Random();
                    Double alea = r.NextDouble();
                    ViewData["src"] = "/Content/images/" + ((compte1)Session["compte1"]).cne + ".jpg?alea=" + alea;
                    Session["compte1"] = compte;
                    return View("Profil", compte);
                }
                return View("Profil", compte);
            }
            return View("Edit", compte);
        }

        [AuthorizeCompte]
        public ActionResult Result()
        {
            compte1 c = (compte1)Session["compte1"];
            ViewData["result"]= "Résultats pas encor affichés";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54975");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/comptes/" + c.cne).Result;
            if (response.IsSuccessStatusCode)
            {
                c = response.Content.ReadAsAsync<compte1>().Result;
               if(c.note_concours != null)
                {
                    ViewData["result"] = "Résultats affichés: \n vous figurez dans la liste "+c.liste_concours+" avec le rang "+c.classement_concours +" et la note "+c.classement_concours ;
                    return View(c);
                }
                return View(c);
            }
            return View(c);
        
        }

        [AuthorizeCompte]
        public ActionResult Profil()
        {
            compte1 c =(compte1) Session["compte1"];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54975");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/comptes/" + c.cne).Result;
            if (response.IsSuccessStatusCode)
            {
                c = response.Content.ReadAsAsync<compte1>().Result;
                Random r = new Random();
            Double alea = r.NextDouble();
            ViewData["src"] = "/Content/images/" + ((compte1)Session["compte1"]).cne + ".jpg?alea="+alea;
                return View(c);
            }
            return View("Index");
            
        }
        
        public ActionResult Authentification()
        {
            return View();
        }

        public ActionResult Connect(string cne , string cin , string mdp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54975");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/comptes/"+cne).Result;
            if (response.IsSuccessStatusCode)
            {
                compte1 x = response.Content.ReadAsAsync<compte1>().Result;
                if (x.mdp.Equals(mdp) && x.cin.Equals(cin))
                {
                    Session["compte1"] = x;
                    return View("Index");
                }
                ViewData["error"] = "Erreur d'authentification. Veuillez réessayer!!";
                return View("Authentification");
            }
            else
            {
                ViewData["error"] = "Erreur d'authentification. Veuillez réessayer!!";
                return View("Authentification");
            }

        }

        public ActionResult Load()
        {
            if (Session["compte1"] != null) return View("Index");
            else return View("Authentification");

        }

        [AuthorizeCompte]
        public ActionResult Edit()
        {
            compte1 c = (compte1) Session["compte1"];
            return View("Edit" , c);
        }

        [AuthorizeCompte]
        public ActionResult EditPhoto()
        {
            ViewData["href"] = "/Content/images/" + ((compte1)Session["compte1"]).cne + ".jpg";
            return View();
        }

        [AuthorizeCompte]
        [NoCache]
        public ActionResult EditPhoto1(HttpPostedFileBase photo)
        {
            compte1 compte = (compte1) Session["compte1"];
            if (photo.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/images"),
                                               Path.GetFileName(compte.cne + ".jpg"));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    photo.SaveAs(path);
                }
                catch (Exception ex)
                {
                    //ViewData["error"] = "Erreur d'inscription. Veuillez réessayer!!";
                    ViewData["error"] = "Erreur "+ex.Message;
                }
            Random r = new Random();
            Double alea = r.NextDouble();
            ViewData["src"] = "/Content/images/" + ((compte1)Session["compte1"]).cne + ".jpg?alea="+alea;
            return View("Profil" , compte);
        }

        [AuthorizeCompte]
        public ActionResult Imprimer()
        {
            compte1 compte = (compte1)Session["compte1"];

            Document pdfDoc = new Document(PageSize.A4, 45, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            // add image; PdfPCell() overload sizes image to fit cell
            Image logo = Image.GetInstance(Server.MapPath("~/Content/images/logoensa1.png"));
            logo.SetAbsolutePosition(30f, 785f);
            logo.ScaleAbsolute(130f, 50f);
            pdfDoc.Add(logo);

            //Add date
            Paragraph date = new Paragraph("Le "+ DateTime.Today );
            date.Alignment = Element.ALIGN_RIGHT;
            pdfDoc.Add(date);

            Image img = Image.GetInstance(Server.MapPath("~/Content/images/"+compte.cne+".jpg"));
            img.SetAbsolutePosition(465f, 615f);
            img.ScaleAbsolute(100f, 100f);
            pdfDoc.Add(img);

            //Title
            Paragraph chunk = new Paragraph("Reçu d'inscription en ligne\n \n" , FontFactory.GetFont("Bell MT", 20, Font.BOLD));
            chunk.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(chunk);

            //Horizontal Line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            Paragraph filiere = new Paragraph("Filière: "+compte.filiere+"", FontFactory.GetFont("Bell MT", 10, Font.UNDERLINE));
            filiere.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(filiere);

            //Table
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 80;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.SpacingBefore = 50f;
            table.SpacingAfter = 5f;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.Padding = 5;

            //Cell
            PdfPCell cell = new PdfPCell();
            Chunk chunk1 = new Chunk("Informations personnelles: " , FontFactory.GetFont("Bell MT", 15, Font.BOLDITALIC));
            cell.AddElement(chunk1);
            cell.Colspan = 2;
            cell.PaddingBottom = 19;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            table.AddCell("CNE: ");
            table.AddCell(compte.cne);

            table.AddCell("Nom: ");
            table.AddCell(compte.nom);

            table.AddCell("Prénom: ");
            table.AddCell(compte.prenom);

            table.AddCell("CIN: ");
            table.AddCell(compte.cin);

            table.AddCell("Date de naissance: ");
            table.AddCell(compte.date_naissance.ToString());

            table.AddCell("Lieu de naissance: ");
            table.AddCell(compte.lieu_naissance);

            table.AddCell("Adresse: ");
            table.AddCell(compte.adresse);

            table.AddCell("Code postal: ");
            table.AddCell(compte.code_postal);

            table.AddCell("Téléphone: ");
            table.AddCell(compte.tel);

            table.AddCell("Email: ");
            table.AddCell(compte.email);

            pdfDoc.Add(table);


            table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.SpacingBefore = 50f;
            table.SpacingAfter = 30f;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.Padding = 5;

            cell = new PdfPCell();
            chunk1 = new Chunk("Notes & diplomes: ", FontFactory.GetFont("Bell MT", 15, Font.BOLDITALIC));
            cell.AddElement(chunk1);
            cell.Colspan = 4;
            cell.PaddingBottom = 19;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell();
            //chunk1 = new Chunk("Baccalauréat", FontFactory.GetFont("Bell MT", 15, Font.ITALIC));
            Paragraph p = new Paragraph("Baccalauréat "+compte.option_bac, FontFactory.GetFont("Bell MT", 15, Font.ITALIC));
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);
            cell.Colspan = 2;
            cell.PaddingBottom = 19;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell();
            //chunk1 = new Chunk("Baccalauréat", FontFactory.GetFont("Bell MT", 15, Font.ITALIC));
            p = new Paragraph("Diplome "+compte.intitule_dip, FontFactory.GetFont("Bell MT", 15, Font.ITALIC));
            p.Alignment = Element.ALIGN_CENTER;
            cell.AddElement(p);
            cell.Colspan = 2;
            cell.PaddingBottom = 19;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            table.AddCell("Année d'obtention: ");
            table.AddCell(compte.annee_bac+"");

            table.AddCell("Année d'obtention: ");
            table.AddCell(compte.annee_dip+"");

            table.AddCell("Mention: ");
            table.AddCell(compte.mention_bac);

            table.AddCell("Mention: ");
            table.AddCell(compte.mention_dip);

            table.AddCell("Moyenne générale: ");
            table.AddCell(compte.note_bac+"");

            table.AddCell("Moyenne générale: ");
            table.AddCell(compte.note_dip + "");

            table.AddCell("Etablissement: ");
            table.AddCell(compte.etablissement_bac);

            table.AddCell("Etablissement: ");
            table.AddCell(compte.etablissement_dip);

            table.AddCell("Académie: ");
            table.AddCell(compte.academie_bac);

            table.AddCell("Ville: ");
            table.AddCell(compte.ville_dip);

            pdfDoc.Add(table);

            p = new Paragraph("Signature:       ", FontFactory.GetFont("Bell MT", 15, Font.NORMAL));
            p.Alignment = Element.ALIGN_RIGHT;
            pdfDoc.Add(p);

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=recu-inscription.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            return View("Index");
        }

        [AuthorizeCompte]
        public ActionResult Disconnect()
        {
            Session["compte1"] = null;
            return View("Authentification");
        }
    }

}