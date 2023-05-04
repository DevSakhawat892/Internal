using Microsoft.AspNetCore.Mvc;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Utilities.Constants;

/*
 * Created by: Sakhawat
 * Date created: 04.09.2022
 * Last modified: 04.09.2022, 10.09.2022, 17.09.2022 
 * Modified by: Sakhawat, Bithy, Rakib
 */
namespace TUSO.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class ModuleController : ControllerBase
   {
      private readonly IUnitOfWork context;

      /// <summary>
      /// Default contructor
      /// </summary>
      /// <param name="context"></param>
      public ModuleController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URl: tuso-api/module
      /// </summary>
      /// <param name="entity">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      [HttpPost]
      [Route(RouteConstants.CreateModule)]
      public async Task<IActionResult> CreateModule(Module module)
      {
         try
         {
            if (await IsModuleDuplicate(module) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.ModuleRepository.Add(module);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadModuleByKey", new { key = module.OID }, module);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URl: tuso-api/modules
      /// </summary>
      /// <returns>List of table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadModules)]
      public async Task<IActionResult> ReadModules()
      {
         try
         {
            var moduleInDb = await context.ModuleRepository.GetModules();
            return Ok(moduleInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL : tuso-api/module/key/{key}
      /// </summary>
      /// <param name="key">Primary key of the table Countries</param>
      /// <returns>Instance of a table object.</returns>
      [HttpGet]
      [Route(RouteConstants.ReadModuleByKey)]
      public async Task<IActionResult> ReadModuleByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var moduleInDb = await context.ModuleRepository.GetModuleByKey(key);

            if (moduleInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(moduleInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/module/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <param name="module">Object to be updated</param>
      /// <returns>Update row in the table.</returns>
      [HttpPut]
      [Route(RouteConstants.UpdateModule)]
      public async Task<IActionResult> UpdateModule(int key, Module module)
      {
         try
         {
            if (key != module.OID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsModuleDuplicate(module) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.ModuleRepository.Update(module);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: tuso-api/module/{key}
      /// </summary>
      /// <param name="key">Primary key of the table</param>
      /// <returns>Deletes a row from the table.</returns>
      [HttpDelete]
      [Route(RouteConstants.DeleteModule)]
      public async Task<IActionResult> DeleteModule(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var moduleInDb = await context.ModuleRepository.GetModuleByKey(key);

            if (moduleInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            moduleInDb.IsDeleted = true;

            context.ModuleRepository.Update(moduleInDb);
            await context.SaveChangesAsync();

            return Ok(moduleInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // <summary>
      /// Checks whether the module is duplicate?
      /// </summary>
      /// <param name="module">Module object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsModuleDuplicate(Module module)
      {
         try
         {
            var moduleInDb = await context.ModuleRepository.GetModuleByName(module.ModuleName);

            if (moduleInDb != null)
               if (moduleInDb.OID != module.OID)
                  return true;

            return false;
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}