using MongoDB.Bson;

namespace Tcc.Core.Models.Facebook
{
    public class Comment : FacebookEntity
    {
        public string Message { get; set; }
        public int LikeCount { get; set; }
        public ObjectId CidadeId { get; set; }
        public ObjectId PostId { get; set; }
        public ObjectId PageId { get; set; }
    }
}
