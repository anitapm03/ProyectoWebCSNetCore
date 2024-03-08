using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

namespace ProyectoWebCSNetCore.Helpers
{

    public class HelperPathProvider
    {
        private IServer server;

        public HelperPathProvider(IServer server)
        {
            this.server = server;
        }

        public string MapUrlServerPath()
        {
            var addresses =
                server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault();
            return serverUrl;
        }

    }
}
