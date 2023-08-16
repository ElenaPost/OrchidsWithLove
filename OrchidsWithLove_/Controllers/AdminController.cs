using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OrchidsWithLove_.Models;
using System.Data.Entity;
using System.Linq;

namespace OrchidsWithLove_.Controllers
{
	public class AdminController : Controller
	{

		[Authorize(Roles = "admin")]
		public async Task<ActionResult> Index()
		{
			List<Flower> flowers;
			using (var context = OrchidsContext.Create())
			{
				flowers = await context.Flowers.ToListAsync();
			}
			return View(flowers);
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult AddFlower()
		{
			ViewBag.MainImage = "";
			return PartialView();
		}

		[HttpPost]
		public async Task<ActionResult> AddFlower(Flower flower, HttpPostedFileBase[] uploadImage)
		{
			if (ModelState.IsValid && uploadImage != null)
			{
				using (var context = OrchidsContext.Create())
				{
					flower.Id = Guid.NewGuid();
					foreach(var img in uploadImage)
					{
						if(img.FileName == "main.jpg")
						{
							flower.Image = ReadFile(img);
							context.Flowers.Add(flower);
						}
						else
						{
							FlowersPhoto flowersPhoto = new FlowersPhoto { Id = Guid.NewGuid(), FlowerId = flower.Id, Image = ReadFile(img) };
							context.FlowersPhotos.Add(flowersPhoto);
						}
					}
					await context.SaveChangesAsync();
				}
			}
			return RedirectToAction("Index");
		}

		private byte[] ReadFile(HttpPostedFileBase uploadImage)
		{
			byte[] imageData = null;
			using (var binaryReader = new BinaryReader(uploadImage.InputStream))
			{
				imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
			}
			return imageData;
		}

		[HttpPost]
		public async Task<ActionResult> DeleteFlower(Guid flowerId)
		{
			using(var context = OrchidsContext.Create())
			{
				List<FlowersPhoto> photos = await context.FlowersPhotos.Where(ph => ph.FlowerId == flowerId).ToListAsync();
				context.FlowersPhotos.RemoveRange(photos);
				Flower flower = await context.Flowers.FirstOrDefaultAsync(f => f.Id == flowerId);
				context.Flowers.Remove(flower);
				await context.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult> EditFlower(Guid flowerId)
		{
			Flower flower;
			using (var context = OrchidsContext.Create())
			{
				flower = await context.Flowers.FirstOrDefaultAsync(f => f.Id == flowerId);
				List<FlowersPhoto> flowersPhotos = await context.FlowersPhotos.Where(fl => fl.FlowerId == flowerId).ToListAsync();
				ViewBag.FlowersPhotos = flowersPhotos;
			}
			return View(flower);
		}

		[HttpPost]
		public async Task<ActionResult> EditFlower(Flower flower, HttpPostedFileBase[] uploadImage, string action)
		{
			if(action == "cancel")
			{
				return RedirectToAction("Index");
			}
			if (ModelState.IsValid && uploadImage != null)
			{
				using (var context = OrchidsContext.Create())
				{
					context.Entry(flower).State = EntityState.Modified;
					foreach (var img in uploadImage)
					{
						if(img != null)
						{
							if (img.FileName == "main.jpg")
							{
								flower.Image = ReadFile(img);
							}
							else
							{
								FlowersPhoto flowersPhoto = new FlowersPhoto { Id = Guid.NewGuid(), FlowerId = flower.Id, Image = ReadFile(img) };
								context.FlowersPhotos.Add(flowersPhoto);
							}
						}
					}

					await context.SaveChangesAsync();
				}
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task DeleteFlowersPhoto(Guid id)
		{
			using (var context = OrchidsContext.Create())
			{
				context.FlowersPhotos.Remove(await context.FlowersPhotos.FindAsync(id));
				await context.SaveChangesAsync();
			}
		}
	}
}