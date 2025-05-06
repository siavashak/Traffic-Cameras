using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrafficCameras.Controllers
{
    public class DataController : Controller
    {
        private readonly HttpClient _httpClient;

        public DataController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index(string url)
        {

            if (string.IsNullOrEmpty(url) == false)
            {
                string content = string.Empty;
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Throw an exception for bad status codes

                    content = await response.Content.ReadAsStringAsync();

                    List<CameraInfo> cameraInfoList = JsonSerializer.Deserialize<List<CameraInfo>>(content);

                    if (cameraInfoList != null)
                    {

                        Dictionary<string, string> dropdownData = new Dictionary<string, string>();
                        foreach (CameraInfo c in cameraInfoList)
                        {
                            dropdownData.Add(c.SourceId + "-" + c.Roadway, c.Views.FirstOrDefault().VideoUrl);
                        }
                        ViewBag.DropdownData = dropdownData;

                        string listOfCameras = "";
                        foreach (var camera in cameraInfoList)
                        {
                            //Console.WriteLine($"ID: {camera.Id}, Roadway: {camera.Roadway}, Source: {camera.Source}");
                            foreach (var view in camera.Views)
                            {
                                listOfCameras += view.VideoUrl;
                                //Console.WriteLine($"  View ID: {view.Id}, URL: {view.Url}, Status: {view.Status}");
                            }
                            //Console.WriteLine();
                        }
                        ViewBag.FetchedContent = content + listOfCameras;

                    }
                    else
                    {
                        Console.WriteLine("Failed to deserialize JSON.");
                    }

                }
                catch (HttpRequestException ex)
                {
                    content = $"Error fetching data: {ex.Message}";
                }
                
            }

            return View("Index"); // Return to the same view to display the data
        }

        [HttpPost]
        public async Task<IActionResult> FetchData(string url)
        {
            string content = string.Empty;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throw an exception for bad status codes

                content = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                content = $"Error fetching data: {ex.Message}";
            }

            ViewBag.FetchedContent = content;
            return View("Index"); // Return to the same view to display the data
        }
    }
}