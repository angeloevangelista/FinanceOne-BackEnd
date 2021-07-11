using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FinanceOne.Domain.ViewModels.UserViewModels
{
  public class UpdateAvatarViewModel : BaseViewModel
  {
    public string UserId { get; set; }
    public IFormFile File { get; set; }

    public override void DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.UserId?.Trim()))
        this._brokenRules.Add("Id is required.");

      if (!Guid.TryParse(this.UserId, out var parsedGuid))
        this._brokenRules.Add("Id is not a valid UUID.");

      var fileWasNotSent = this.File == null;

      if (fileWasNotSent)
        this._brokenRules.Add("File is required.");
      else
      {
        if (this.File.Length <= 0)
          this._brokenRules.Add("File is corrupted.");

        if (this.File.Length > 1_048_576)
          this._brokenRules.Add("File must be smaller than 1MB.");

        var extension = Path
          .GetExtension(this.File.FileName)
          .ToUpper();

        var validExtensions = new string[] { ".jpg", ".jpeg", ".png" };

        var validFormat = validExtensions.Any(p => p.ToUpper() == extension);

        if (!validFormat)
          this._brokenRules.Add("File was in unsupported format.");
      }
    }
  }
}
