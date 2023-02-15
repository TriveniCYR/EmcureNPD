namespace EmcureCERI.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BaseResponse<TEntity>
    {
        public BaseResponse()
        {
            Messages = new List<Message>();
        }
        public bool Success { get; set; }

        public List<Message> Messages { get; private set; }
    }

    public class ServiceResponseList<TEntity> : BaseResponse<TEntity>
    {
        public ServiceResponseList()
            : base()
        {
            Results = new List<TEntity>();
        }
        public List<TEntity> Results { get; set; }
    }

    public class ServiceResponse<TEntity> : BaseResponse<TEntity>
    {
        public ServiceResponse() : base() { }

        public TEntity Result { get; set; }
    }
    public class Message
    {
        public string Detail { get; set; }
        public MessageType Status { get; set; }
    }

    public enum MessageType
    {
        Information,
        Success,
        Warning,
        Error
    }
}
