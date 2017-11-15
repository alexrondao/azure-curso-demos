using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadCli.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CadCli.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using CadCli.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.WindowsAzure.Storage;

namespace CadCli.Controllers
{
    public class ClienteController : Controller
    {
        private readonly CadCliDataContext _ctx;
        private readonly Keys _keys;

        public ClienteController(CadCliDataContext ctx, IOptions<Keys> keys)
        {
            _ctx = ctx;
            _keys = keys.Value;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _ctx.Clientes
                .OrderByDescending(c=>c.Id)
                .Take(_keys.LimiteClientes)
                .ToListAsync();

            return View(model);
        }

        public IActionResult Add() => View();

        public async Task<IActionResult> Save(IFormFile imagem, [FromServices] IHostingEnvironment env, Cliente cliente)
        {
            string folder = env.ContentRootPath + @"\Uploads\";

            if(imagem.Length > 0)
            {
                try
                {
                    using (var fs = new FileStream(folder + imagem.FileName, FileMode.Create))
                    {
                        await imagem.CopyToAsync(fs);
                    }
                    cliente.NomeArquivo = imagem.FileName;
                    cliente.UrlArquivo = folder;
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }

            }
            _ctx.Clientes.Add(cliente);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Save2(IFormFile imagem, [FromServices] IHostingEnvironment env, Cliente cliente)
        {
            try
            {

                if (imagem.Length > 0)
                {
                    //string folder = env.ContentRootPath + @"\Uploads\";
                    string folder = @"f:\files\";
                    var connString = "DefaultEndpointsProtocol=https;AccountName=stxcorpvm;AccountKey=pMYKxUsIG0kq39FGqOO6rAd2w8nqBVhTVntrrC7DSd3Cr2w6pSN2hMIj6dMctmzpfkOIOHmHupjUFoHe4uQT1g==;EndpointSuffix=core.windows.net";

                    var conn = CloudStorageAccount.Parse(connString);

                    var client = conn.CreateCloudBlobClient();
                    var container = client.GetContainerReference("imagens");
                    await container.CreateIfNotExistsAsync();

                    string file = $"img-{new Random().Next(int.MaxValue)}.jpg";
                    var blob = container.GetBlockBlobReference(file);
                    
                    using (var fs = System.IO.File.OpenRead(folder + imagem.FileName))
                    {
                        await blob.UploadFromStreamAsync(fs);
                    }
                    
                    cliente.NomeArquivo = imagem.FileName;
                    cliente.UrlArquivo = folder;
                }
                _ctx.Clientes.Add(cliente);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public string Folder([FromServices] IHostingEnvironment env)
        {
            return env.ContentRootPath;
        }

        public async Task<ViewResult> Todos([FromServices] IDistributedCache cache)
        {
            var model = await _ctx.Clientes
                .ToListAsync();

            ViewBag.Foto = string.Empty;

            var connString = "DefaultEndpointsProtocol=https;AccountName=stxcorpvm;AccountKey=pMYKxUsIG0kq39FGqOO6rAd2w8nqBVhTVntrrC7DSd3Cr2w6pSN2hMIj6dMctmzpfkOIOHmHupjUFoHe4uQT1g==;EndpointSuffix=core.windows.net";

            var conn = CloudStorageAccount.Parse(connString);

            var client = conn.CreateCloudBlobClient();
            var container = client.GetContainerReference("imagens");
            await container.CreateIfNotExistsAsync();

            model.ForEach(cli => {

                if (!string.IsNullOrEmpty(cli.NomeArquivo) && !string.IsNullOrEmpty(cli.UrlArquivo))
                {
                    var file = cli.UrlArquivo;
                    var blob = container.GetBlockBlobReference(file);

                    try
                    {
                        byte[] imageArray = new byte[blob.StreamWriteSizeInBytes];
                        blob.DownloadToByteArrayAsync(imageArray, 0).Wait();
                        ViewBag.Foto = Convert.ToBase64String(imageArray);
                    }
                    catch
                    {
                        if (!String.IsNullOrEmpty(cli.NomeArquivo) && !String.IsNullOrEmpty(cli.UrlArquivo))
                        {
                            var imagemArray = System.IO.File.ReadAllBytes($@"{cli.UrlArquivo}{cli.NomeArquivo}");
                            ViewBag.Foto = Convert.ToBase64String(imagemArray);
                        }
                    }
                }

            });

            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    }
}