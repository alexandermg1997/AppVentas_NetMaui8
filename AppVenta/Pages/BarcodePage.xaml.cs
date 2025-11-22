using AppVenta.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.ApplicationModel;

namespace AppVenta.Pages;

public partial class BarcodePage : ContentPage
{
	public BarcodePage()
	{
		InitializeComponent();
	}

	private void cameraView_CamerasLoaded(object sender,EventArgs e)
	{
		if(cameraView.Cameras.Count > 0)
		{
			cameraView.Camera = cameraView.Cameras.First();
			MainThread.BeginInvokeOnMainThread(new Action(async () =>
			{

				await cameraView.StopCameraAsync();
				await cameraView.StartCameraAsync();
			}));
		}
	}

	private void cameraView_BarcodeDetected(object sender, object args)
	{
		dynamic ev = args;
		try
		{
			string text = ev.Result[0].Text;
			BarcodeResult barcodeResult = new BarcodeResult { BarcodeValue = text };
			WeakReferenceMessenger.Default.Send(new BarcodeScannedMessage(barcodeResult));

			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.Navigation.PopModalAsync();
			});
		}
		catch
		{
		}

	}


}