

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

namespace CapaEntidad
{
    public class StripeConfig
    {
        public StripeConfig()
        {
            // Obtener la clave secreta de Stripe desde el archivo de configuración
            string stripeSecretKey = System.Configuration.ConfigurationManager.AppSettings["StripeSecretKey"];

            // Establecer la clave secreta de Stripe
            StripeConfiguration.ApiKey = stripeSecretKey;
        }
    }
}


