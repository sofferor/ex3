// ***********************************************************************
// Assembly         : WebApp
// Author           : Haim
// Created          : 06-29-2017
//
// Last Modified By : Haim
// Last Modified On : 06-29-2017
// ***********************************************************************
// <copyright file="WebAppContext.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Class WebAppContext.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class WebAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAppContext"/> class.
        /// </summary>
        public WebAppContext() : base("name=WebAppContext")
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public System.Data.Entity.DbSet<WebApp.Models.User> Users { get; set; }
    }
}
