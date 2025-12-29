using Microsoft.JSInterop;
using negocio;
using System.Data;

namespace Almacen.Data
{
	public enum AlertType
	{
		Basic,
		Success,
		Error,
		Warning,
		Question,
		Actions,
		ActionsSuccess,
		Confirm
	}

	public class BaseService
	{
		public NEGOCIO ObjConsulta { get; set; } = new NEGOCIO();
		public NEGOCIO ObjQuery { get; set; } = new NEGOCIO();
		public DataTable? TblDet { get; set; }

		private readonly IJSRuntime _jsRuntime;
		private Func<bool, Task>? _callback; // Campo para almacenar el callback
		private DotNetObjectReference<BaseService>? _currentObjectReference;

		public BaseService(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public async Task ShowSweetAlert(
	  AlertType alertType,
	  string title = "",
	  string message = "",
	  string url = "",
	  Func<bool, Task>? callback = null)
		{
			_callback = callback; // Guardamos el callback para su uso posterior
			var methodName = callback == null ? string.Empty : nameof(HandleConfirmation);
			if (alertType == AlertType.Confirm && callback != null)
			{
				_currentObjectReference = DotNetObjectReference.Create(this);
				await _jsRuntime.InvokeVoidAsync("Alert", alertType.ToString(), title, message, url, _currentObjectReference, methodName);
			}
			else
			{
				await _jsRuntime.InvokeVoidAsync("Alert", alertType.ToString(), title, message, url);
			}
		}

		[JSInvokable]
		public async Task HandleConfirmation(bool confirmed)
		{
			//     Console.WriteLine($"HandleConfirmation called with {confirmed}");
			if (_callback != null)
			{
				await _callback.Invoke(confirmed);
			}
			DisposeObjectReference();
		}

		public void DisposeObjectReference()
		{
			_currentObjectReference?.Dispose();
		}
	}
}
