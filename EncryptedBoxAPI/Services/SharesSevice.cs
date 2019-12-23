﻿using EncryptedBoxAPI.Helpers;
using EncryptedBoxAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncryptedBoxAPI.Services
{
    public class SharesSevice
    {
        private readonly IMongoCollection<ShareModel> _shares;
        private IMongoDatabase _db;

        public SharesSevice(IConfiguration config)
        {
            this._db = ((MongoClientBase)new MongoClient(ConfigurationExtensions.GetConnectionString(config, "StoreDb"))).GetDatabase("EncryptedBoxCollection", (MongoDatabaseSettings)null);
            this._shares = (IMongoCollection<ShareModel>)this._db.GetCollection<ShareModel>("Shares", (MongoCollectionSettings)null);
        }

        public ApiResult GetAll(string source, string target)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.Data = this._shares.AsQueryable().Where(f => f.target == target).OrderByDescending(o => o.date).ToList();
            return apiResult;
        }

        public ShareModel Get(string id)
        {
            return this._shares.Find<ShareModel>(i => i.id == id).FirstOrDefault();
        }

        public ShareModel Create(ShareModel share)
        {
            this._shares.InsertOne(share);
            return share;
        }

        public void Remove(ShareModel shares)
        {
            this._shares.DeleteOne<ShareModel>(i => i.id == shares.id);
        }

        public void Remove(string id)
        {
            this._shares.DeleteOne<ShareModel>(i => i.id == id);
        }
    }
}
