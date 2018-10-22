using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    

        [Serializable]
        public class GameObjectException : ApplicationException
        {
            public DateTime ErrorTimeStamp { get; set; }
            public string messageDetails = String.Empty;
            public string CauseOfError { get; set; }
            public GameObjectException() { }
            public GameObjectException(string message) : base(message) { }
            public GameObjectException(string message, ApplicationException inner) : base(message, inner) { }
            public GameObjectException(string message, string cause,DateTime time)
                {
                    messageDetails = message;
                    CauseOfError = cause;
                    ErrorTimeStamp = time;
                }
        public override string Message => $"ВОЗНИКЛА ОШИБКА: {messageDetails}";



        protected GameObjectException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    
}
