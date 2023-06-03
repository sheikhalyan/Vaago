using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vaago.ViewModels;
using Vaago.Models;
using System.Data.SqlClient;

namespace Vaago.Controllers
{
    public class ChefreqController : ApiController
    {


        private readonly string connectionString = "data source=DESKTOP-8IKJ0OT;initial catalog=VaagoProject;integrated security=True;"; // Replace with your actual connection string

        [HttpPost]
        public IHttpActionResult PostChef(Chef chef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Chef (Chef_Name, Chef_Email, Chef_Contact, Msg_Body) VALUES (@ChefName, @ChefEmail, @ChefContact, @MsgBody)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ChefName", chef.Chef_Name);
                command.Parameters.AddWithValue("@ChefEmail", chef.Chef_Email);
                command.Parameters.AddWithValue("@ChefContact", chef.Chef_Contact);
                command.Parameters.AddWithValue("@MsgBody", chef.Msg_Body);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to insert data into the database.");
                }
            }
        }

















        //public IHttpActionResult PostNewChef(ChefViewModel chef)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Invalid data.");

        //    using (var ctx = new VaagoProjectEntities1())
        //    {
        //        ctx.Chefs.Add(new Chef()
        //        {
        //            Chef_Name = chef.Chefname,
        //            Chef_Email = chef.Chefemail,
        //            Chef_Contact = chef.chefcontact,
        //            Msg_Body = chef.msg,


        //        });

        //        ctx.SaveChanges();
        //    }

        //    return Ok();
        //}













        //public IHttpActionResult PostNewChef(Chef chef)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data.");
        //    }

        //    using (var ctx = new VaagoProjectEntities1()) // Replace YourDbContext with your actual DbContext class
        //    {
        //        ctx.Chefs.Add(chef);
        //        ctx.SaveChanges();
        //    }

        //    return Ok();
        //}

    }
}
