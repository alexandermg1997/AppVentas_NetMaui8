using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using System.IO;

namespace AppVenta.Utilidades
{
    public static class ConexionDb
    {
        public static string DevolverRuta(string nombreBaseDatos)
        {
            // Use MAUI FileSystem.AppDataDirectory which maps to a suitable app-specific folder on each platform
            string appData = FileSystem.AppDataDirectory;

            // Ensure directory exists
            if (!Directory.Exists(appData))
            {
                Directory.CreateDirectory(appData);
            }

            string rutaBaseDatos = Path.Combine(appData, nombreBaseDatos);

            return rutaBaseDatos;

        }
    }
}
