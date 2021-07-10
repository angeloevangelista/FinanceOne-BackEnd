using FinanceOne.Domain.Enumerators;
using Microsoft.AspNetCore.Http;

namespace FinanceOne.Domain.Providers
{
  public interface IStorageProvider
  {
    string UploadFile(
      IFormFile file,
      FileType fileType = FileType.GeneralAsset
    );
  }
}
