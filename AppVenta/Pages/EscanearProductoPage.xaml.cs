using AppVenta.DataAccess;
using AppVenta.DTOs;
using AppVenta.Modelos;
using AppVenta.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.ApplicationModel;

namespace AppVenta.Pages;

public partial class EscanearProductoPage : ContentPage
{
    private readonly VentaDbContext _context;
	public EscanearProductoPage(VentaDbContext context)
	{
		InitializeComponent();
		_context = context;
	}

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(new Action(async () =>
            {

                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            }));
        }
    }

    private async void cameraView_BarcodeDetected(object sender, object args)
    {
        dynamic ev = args;
        try
        {
            string codigo = ev.Result[0].Text;
            Producto dbProducto = await _context.Productos.Include(c => c.RefCategoria).FirstOrDefaultAsync(p => p.Codigo == codigo);
            if (dbProducto != null)
            {
                ProductoDTO producto = new ProductoDTO()
                {
                    IdProducto = dbProducto.IdProducto,
                    Codigo = dbProducto.Codigo,
                    Nombre = dbProducto.Nombre,
                    Categoria = new CategoriaDTO()
                    {
                        IdCategoria = dbProducto.IdCategoria,
                        Nombre = dbProducto.RefCategoria.Nombre
                    },
                    Cantidad = dbProducto.Cantidad,
                    Precio = dbProducto.Precio
                };
                WeakReferenceMessenger.Default.Send(new ProductoVentaMessage(producto));
            }
        }
        catch
        {
            // ignore runtime differences
        }

        await Shell.Current.Navigation.PopModalAsync();
    }
}