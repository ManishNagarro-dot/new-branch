using Newtonsoft.Json;

namespace GOI.Seeker.Master.Shared.Response
{
    public class Response<TEntity>
    {
        [JsonProperty(PropertyName = "data")]
        public TEntity Data { get; set; }

        [JsonProperty(PropertyName = "error")]
        public Error Error { get; set; }
    }
}