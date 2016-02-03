using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ZwhAid
{
    /// <summary>
    /// MongoDB操作
    /// </summary>
    public class MongoDBAid: ZwhBase
    {
        protected string connectionString;
        /// <summary>
        /// MongoDB数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            set { connectionString = value; }
            get { return connectionString; }
        }

        protected string db;
        /// <summary>
        /// 
        /// </summary>
        public string DB
        {
            set { db = value; }
            get { return db; }
        }

        protected string collection;
        public string Collection
        {
            set { collection = value; }
            get { return collection; }
        }

        public MongoDBAid()
        {
            try
            {
                connectionString = GetDB.GetMongoDBString(null);
                GetMC();
            }
            catch { }
        }

        public MongoDBAid(string cs)
        {
            try
            {
                if (!string.IsNullOrEmpty(cs))
                {
                    connectionString = cs;
                }
                else
                {
                    connectionString = GetDB.GetMongoDBString(null);
                }
                GetMC();
            }
            catch { }
        }

        protected MongoCollection mc;
        protected void GetMC()
        {
            try
            {
                MongoClient client = new MongoClient(ConnectionString);
                MongoServer server = client.GetServer();
                MongoDatabase data = server.GetDatabase(DB);
                mc = data.GetCollection(Collection);
            }
            catch { }
        }
        
        public bool Add<Model>(Model model)
        {
            try
            {
                WriteConcernResult wcr = mc.Insert<Model>(model);
                zBool = wcr.Ok;
            }
            catch { }
            return ZBool;
        }

        public bool Del(Dictionary<string, object> kv)
        {
            try
            {
                IMongoQuery imq = null;
                foreach (KeyValuePair<string, object> kvp in kv)
                {
                    imq=Query.And(Query.EQ(kvp.Key, BsonValue.Create(kvp.Value)));
                }
                WriteConcernResult wcr = mc.Remove(imq);
                zBool = wcr.Ok;
            }
            catch { }
            return ZBool;
        }

        public bool Mod(Dictionary<string, object> kv, Dictionary<string, object> kvWhere)
        {
            try
            {
                IMongoQuery imq = null;
                foreach (KeyValuePair<string, object> kvp in kvWhere)
                {
                    imq=Query.And(Query.EQ(kvp.Key, BsonValue.Create(kvp.Value)));
                }
                IMongoUpdate imu=null;
                foreach (KeyValuePair<string, object> kvp in kv)
                {
                    imu = Update.AddToSet(kvp.Key, BsonValue.Create(kvp.Value));
                }
                WriteConcernResult wcr=mc.Update(imq,imu);
                zBool = wcr.Ok;
            }
            catch { }
            return ZBool;
        }

        public Model QModel<Model>(string objID)
        {
            Model model = default(Model);
            try
            {
                mc.FindOneAs(typeof(Model),Query.EQ("_id",BsonValue.Create(objID)));
            }
            catch { }
            return model;
        }

        public DataSet Qy<Model>()
        {
            try
            {
                MongoCursor cursor;
                cursor=mc.FindAllAs(typeof(Model));
            }
            catch { }
            return ZSet;
        }
    }
}
