// ***********************************************************************
// Assembly         : WebApp
// Author           : Haim
// Created          : 06-29-2017
//
// Last Modified By : Haim
// Last Modified On : 07-02-2017
// ***********************************************************************
// <copyright file="User.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models {
    /// <summary>
    /// Class User.
    /// </summary>
    public class User {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Key]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the wins.
        /// </summary>
        /// <value>The wins.</value>
        [Range(0, int.MaxValue)]
        public int Wins { get; set; }
        /// <summary>
        /// Gets or sets the loses.
        /// </summary>
        /// <value>The loses.</value>
        [Range(0, int.MaxValue)]
        public int Loses { get; set; }
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>The rank.</value>
        [Range(1, int.MaxValue)]
        public int Rank { get; set; }

    }
}