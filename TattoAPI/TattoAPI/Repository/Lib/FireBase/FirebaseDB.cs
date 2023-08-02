using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace TattoAPI.Repository.Lib.FireBase
{
    class FirebaseDB
    {
        private string RootNode { get; set; }

        public FirebaseDB(string baseURL)
        {
            this.RootNode = baseURL;
        }

        public FirebaseDB Node(string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException("Node must not contain '/', use NodePath instead.");
            }
            return new FirebaseDB(this.RootNode + '/' + node);
        }
        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, this.RootNode).Execute();
        }
        public FirebaseResponse Put(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Put, this.RootNode, jsonData).Execute();
        }
        public FirebaseResponse Post(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Post, this.RootNode, jsonData).Execute();
        }
        public FirebaseResponse Patch(string jsonData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), this.RootNode, jsonData).Execute();
        }
        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, this.RootNode).Execute();
        }

        public override string ToString()
        {
            return this.RootNode;
        }

        internal object NodePath(string v)
        {
            throw new NotImplementedException();
        }
    }
}
