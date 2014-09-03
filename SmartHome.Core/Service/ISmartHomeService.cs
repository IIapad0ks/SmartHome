using System.ServiceModel;

namespace SmartHome.Core.Service
{
    [ServiceContract]
    public interface ISmartHomeService
    {
        [OperationContract]
        bool Start();

        [OperationContract]
        bool Stop();

        [OperationContract]
        bool Restart();

        [OperationContract]
        bool IsOn();
    }
}
