using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SportSupplements_API.Core.Models;
using SportSupplements_API.Infrastructure.Commands;
using SportSupplements_API.Infrastructure.Queries;
using SportSupplements_API.Infrastructure.Services;
using SportSupplements_API.Presentation.Dtos;
using SportSupplements_API.Presentation.Models;
using SportSupplements_API.Presentation.Options;

namespace SportSupplements_API.Presentation.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class SportSupplementController : ControllerBase
{

    private readonly ISender sender;
    private readonly BlobContainerService blobContainerService;

    public SportSupplementController(ISender sender, IOptions<BlobOptions> blobOptions)
    {
        this.sender = sender;

        this.blobContainerService = new BlobContainerService(blobOptions.Value.Url, blobOptions.Value.ContainerName);
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var exercises = await this.sender.Send(getAllQuery);

        return base.Ok(exercises.Where(sportsupplement => sportsupplement.IsApproved));
    }

    [HttpGet]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var sportSupplement = await this.sender.Send(getByIdQuery);

        return base.Ok(sportSupplement);
    }

    [HttpPost]
    public async Task<IActionResult> Create(object sportSupplementContentJson)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        var sportSupplementContent = JsonConvert.DeserializeObject<SportSupplementContent>(sportSupplementContentJson.ToString()!, settings);

        var imageFileName = sportSupplementContent!.ImageFileName;

        var imageFileData = sportSupplementContent.ImageFileContent;

        var rawPath = Guid.NewGuid().ToString() + imageFileName;

        var path = rawPath.Replace(" ", "%20");

        var sportSupplement = new SportSupplement
        {
            Name = sportSupplementContent.SportSupplement!.Name,

            Description = sportSupplementContent.SportSupplement.Description,

            ManufactureCountry = sportSupplementContent.SportSupplement.ManufactureCountry,

            Quantity = sportSupplementContent.SportSupplement.Quantity,

            IsApproved = false,

            ImageUrl = "https://4fitbodystorage.blob.core.windows.net/images/" + path
        };

        await this.blobContainerService.UploadAsync(new MemoryStream(imageFileData!), rawPath);

        var createCommand = new CreateCommand(sportSupplement);

        await this.sender.Send(createCommand);

        return base.Ok();
    }

    [HttpDelete]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        var createCommand = new DeleteCommand(id);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

    [HttpPut]
    [Route("/api/[controller]/[action]/{id}")]
    public async Task<IActionResult> Update(int? id, [FromBody] SportSupplementDto sportSupplementDto)
    {
        var sportSupplement = new SportSupplement
        {
            Name = sportSupplementDto.Name,

            Description = sportSupplementDto.Description,
            
            ManufactureCountry = sportSupplementDto.ManufactureCountry,
            
            Quantity = sportSupplementDto.Quantity,
            
            IsApproved = false,
            
            ImageUrl = sportSupplementDto.ImageUrl
        };

        var createCommand = new UpdateCommand(id, sportSupplement);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

}
