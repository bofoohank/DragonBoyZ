using System.Threading.Tasks;
using DragonBoyZ.Application.Interfaces.Map;
using DragonBoyZ.Application.IO;

namespace DragonBoyZ.Application.Interfaces.Client
{
    public interface IMessageHandler
    {
        void OnConnectionFail(ISession_ME client, bool isMain);

        void OnConnectOK(ISession_ME client, bool isMain);

        void OnDisconnected(ISession_ME client, bool isMain);

        Task OnMessage(Message message);
    }
}