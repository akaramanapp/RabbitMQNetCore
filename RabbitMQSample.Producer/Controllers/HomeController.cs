using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQSample.Producer.Models;
using RabbitMQ.Client;

namespace RabbitMQSample.Producer.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            string url = "" // rmq.cloudamqp.com user and password
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(url.Replace("amqp://", "amqps://"));

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()){
                channel.QueueDeclare(
                    queue: "Customer",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                string message = "Merhaba";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "Customer",
                    basicProperties: null,
                    body: body
                );
            };

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
