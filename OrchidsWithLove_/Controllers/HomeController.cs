using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OrchidsWithLove_.Models;

namespace OrchidsWithLove_.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			IEnumerable<Flower> flowers = new List<Flower>();
			using (var context = OrchidsContext.Create())
			{
				flowers = context.Flowers.ToList();
			}
			return View(flowers);
		}

		[HttpGet]
		public async Task<ActionResult> ShowFlower(string id)
		{
			Guid flowerId = Guid.Parse(id);
			List<byte[]> flowerPhotos = new List<byte[]>();
			Flower flower;
			using (var context = OrchidsContext.Create())
			{
				flower = await context.Flowers.FindAsync(flowerId);
				flowerPhotos.Add(flower.Image);
				var flowersPhotos = context.FlowersPhotos.Where(fl => fl.FlowerId == flowerId).ToList();
				foreach(var photo in flowersPhotos)
				{
					flowerPhotos.Add(photo.Image);
				}
				ViewBag.FlowerPhotos = flowerPhotos;
			}
			return View(flower);
		}

		public FileContentResult GetFile(Guid id)
		{
			FileContentResult fileContent;
			using (var context = OrchidsContext.Create())
			{
				var file = context.Flowers.Find(id).Image;
				fileContent = new FileContentResult(file, "image/jpeg");
			}
			return fileContent;
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}