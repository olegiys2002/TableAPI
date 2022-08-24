using Core.IServices;
using Core.Models.Storage;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FireStorage : IStorage
    {
        private readonly FireStorageOptions _storageOptions;
        public FireStorage(IOptions<FireStorageOptions> options)
        {
            _storageOptions = options.Value;
        }
        public async Task CreateAvatarAsync(IFormFile formFile)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_storageOptions.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(_storageOptions.AuthEmail, _storageOptions.AuthPassword);
            var cancelletion = new CancellationTokenSource();

            var upload = new FirebaseStorage(
                _storageOptions.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child("avatars").Child(formFile.FileName).PutAsync(formFile.OpenReadStream(), cancelletion.Token);

        }
        //public async Task<IFormFile> GetAvatarAsync(IFormFile formFile)
        //{
   
        //}

    }

}
