using FinanceOne.Domain.Enumerators;
using FinanceOne.Domain.Providers;
using Microsoft.AspNetCore.Http;

namespace FinanceOne.Tests.Mocks.Providers
{
  public class StorageProviderMock : IStorageProvider
  {
    public string UploadFile(
      IFormFile file,
      FileType fileType = FileType.GeneralAsset
    )
    {
      return "https://http.cat/404";
    }
  }
}
