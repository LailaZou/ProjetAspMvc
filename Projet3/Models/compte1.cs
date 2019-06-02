using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projet3.Models
{
    
        public partial class compte1
        {
            [Required]
            public string cne { get; set; }
            [Required]
            public string cin { get; set; }
            public string mdp { get; set; }
            [Required]
            public string nom { get; set; }
            [Required]
            public string prenom { get; set; }
            [Required]
            public string email { get; set; }
            [Required]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> date_naissance { get; set; }
            [Required]
            public string lieu_naissance { get; set; }
            [Required]
            public string adresse { get; set; }
            [Required]
            public string code_postal { get; set; }
            [Required]
            public string tel { get; set; }
            [Required]
            public string filiere { get; set; }
            [Required]
            public string option_bac { get; set; }
            [Required]
            public Nullable<int> annee_bac { get; set; }
            [Required]
            public string mention_bac { get; set; }
            [Required]
            public Nullable<float> note_bac { get; set; }
            [Required]
            public string etablissement_bac { get; set; }
            [Required]
            public string academie_bac { get; set; }
            [Required]
            public string intitule_dip { get; set; }
            [Required]
            public Nullable<int> annee_dip { get; set; }
            [Required]
            public string mention_dip { get; set; }
            [Required]
            public Nullable<float> note_dip { get; set; }
            [Required]
            public string etablissement_dip { get; set; }
            [Required]
            public string ville_dip { get; set; }
            public Nullable<int> classement_concours { get; set; }
            public Nullable<float> note_concours { get; set; }
            public string liste_concours { get; set; }
        }
    }
