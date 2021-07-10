using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Domain.Providers;
using FinanceOne.Domain.Services;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FinanceOne.Implementation.Providers
{
  public class FirebaseStorageProvider : IStorageProvider
  {
    private readonly IHashService _hashService;
    private readonly IConfiguration _configuration;

    public FirebaseStorageProvider(
      IHashService hashService,
      IConfiguration configuration
      )
    {
      this._hashService = hashService;
      this._configuration = configuration;
    }

    public string UploadFile(
      IFormFile file,
      FileType fileType = FileType.GeneralAsset
    )
    {
      var firebaseConfigSection = this._configuration.GetSection("Firebase");

      var directoriesConfigSection = this._configuration
        .GetSection("Directories");

      var tempPath = directoriesConfigSection.GetSection("TempPath").Value;

      var firebaseFolderName = directoriesConfigSection
        .GetSection("FirebaseFolderName")
        .Value;

      var firebasePath = Path.Combine(tempPath, firebaseFolderName);

      if (!Directory.Exists(firebasePath))
        Directory.CreateDirectory(firebasePath);

      string uploadedFileLink;
      string fileName = $"{this._hashService.GenerateRandomHash()}-{file.FileName}";
      string uploadedFilePath = Path.Combine(firebasePath, fileName);

      using (var fileStream = new FileStream(uploadedFilePath, FileMode.Create))
        file.CopyTo(fileStream);

      var apiKey = firebaseConfigSection
        .GetSection("ApiKey")
        .Value;

      var authEmail = firebaseConfigSection
        .GetSection("AuthEmail")
        .Value;

      var authPassword = firebaseConfigSection
        .GetSection("AuthPassword")
        .Value;

      var authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));

      var firebaseAuthLink = authProvider
        .SignInWithEmailAndPasswordAsync(authEmail, authPassword)
        .GetAwaiter()
        .GetResult();

      var cancellationTokenSource = new CancellationTokenSource();

      var bucket = firebaseConfigSection.GetSection("Bucket").Value;

      var assetFolderName = this.GetAssetFolderName(fileType);

      using (
        var openFileStream = new FileStream(uploadedFilePath, FileMode.Open)
      )
      {
        var upload = new FirebaseStorage(
          bucket,
          new FirebaseStorageOptions()
          {
            AuthTokenAsyncFactory = () => Task.FromResult(
              firebaseAuthLink.FirebaseToken
            ),
            ThrowOnCancel = true
          }
        )
        .Child("assets")
        .Child(assetFolderName)
        .Child(fileName)
        .PutAsync(openFileStream, cancellationTokenSource.Token);

        uploadedFileLink = upload.GetAwaiter().GetResult();

        File.Delete(uploadedFilePath);
      }

      return uploadedFileLink;
    }

    private string GetAssetFolderName(FileType fileType)
    {
      var folderName = "not_predicted";

      switch (fileType)
      {
        case FileType.GeneralAsset:
          folderName = "general";
          break;

        case FileType.ProfileAvatar:
          folderName = "avatars";
          break;
      }

      return folderName;
    }
  }
}
